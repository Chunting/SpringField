using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Springfield.Components;
using Springfield.Components.Configuration;

public partial class EmploymentDecision : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!SiteUser.Current.IsInRole(RoleType.InternRecruiter))
        {
            Server.Transfer("~/AccessDeny.htm", true);
        }

        //only intern recruiter can visit this page
        if (!IsPostBack)
        {
            BindData();
        }
    }

    private void BindData()
    {
        if (ddlStatus.SelectedValue == Convert.ToInt32(ApplicationStatusEnum.Hired).ToString())//hired
        {
            btnHire.Visible = true;
            btnMultiReject.Visible = false;
            litHeader.Text = "Hired Applicant List";
            litHint.Text = "hire";
            gvDecidedApplicants.Columns[3].HeaderText = "Hire Date";
        }
        else if (ddlStatus.SelectedValue == Convert.ToInt32(ApplicationStatusEnum.Rejected).ToString())//rejected
        {
            btnHire.Visible = false;
            btnMultiReject.Visible = true;
            litHeader.Text = "Rejected Applicant List";
            litHint.Text = "reject";
            gvDecidedApplicants.Columns[3].HeaderText = "Reject Date";
        }
        gvDecidedApplicants.DataSource = Applicant.GetAllDecidedApplicants(Convert.ToInt16(ddlStatus.SelectedValue));
        gvDecidedApplicants.DataBind();
    }

    protected string ParseDecision(object dataItem)
    {
        string decision = "Error";
        DataRowView dr = dataItem as DataRowView;
        //int approvalResult = Convert.ToInt32(dr["GroupManagerResult"]);
        int approvalResult = Convert.ToInt32(dr["Status"]);
        switch (approvalResult)
        { 
            case 4:
                //Hire
                decision = "Hire";
                break;
            case 5:
                decision = "Reject";
                //Reject
                break;
        }

        return decision;
    }

    protected string ParseName(object dataItem)
    {
        DataRowView dr = dataItem as DataRowView;
        string firstName = dr["FirstName"].ToString();
        string lastName = dr["LastName"].ToString().ToUpper();

        return (firstName + " " + lastName);
    }

    protected string ParseMentor(object dataItem)
    {
        DataRowView dr = (DataRowView)dataItem;
        string curInterviewId = Interview.GetRecentInterviewIdByApplicant(new Guid(dr["ApplicantId"].ToString()));
        string strHiringManagerAlias = "N/A";
        String status = StaticData.AppStatusDict[(ApplicationStatusEnum)dr["Status"]];

        if (status.ToLower() != "available")
        {
            Interview curInterview = Interview.GetInterviewById(Convert.ToInt32(curInterviewId));
            strHiringManagerAlias = SiteUser.GetAliasByUserId(curInterview.HiringManagerId);
        }
        return strHiringManagerAlias;
    }

    protected string ParseGroupManager(object dataItem)
    {
        DataRowView dr = (DataRowView)dataItem;
        Guid curId = (Guid)dr["ApplicantId"];
        string status = Interview.GetRecentInterviewStatus(curId);
        string curInterviewId = Interview.GetRecentInterviewIdByApplicant(curId);
        Interview curInterview = Interview.GetInterviewById(Convert.ToInt32(curInterviewId));
        string strGrpMgrAlias = "N/A";
        if (curInterview.GroupManagerId != Guid.Empty)
        {
            strGrpMgrAlias = SiteUser.GetAliasByUserId(curInterview.GroupManagerId);
        }
        return strGrpMgrAlias;
    }

    protected string ParseDateTime(object dataItem)
    {
        DataRowView dr = dataItem as DataRowView;
        return Convert.ToDateTime(dr["EndDate"]).ToShortDateString();
    }

    protected void OkButton_Click(object sender, EventArgs e)
    {
        if (ddlStatus.SelectedValue == Convert.ToInt32(ApplicationStatusEnum.Hired).ToString())//hired
        {
            btnHire_Click(btnHire, null);
        }
        else if (ddlStatus.SelectedValue == Convert.ToInt32(ApplicationStatusEnum.Rejected).ToString())//rejected
        {
            btnMultiReject_Click(btnMultiReject, null);
        }
    }
    protected void btnMultiReject_Click(object sender, EventArgs e)
    {
        if (Request.Form["cbChecked"] != null)
        {
            string[] idArr = Request.Form["cbChecked"].ToString().Split(',');
            foreach (string id in idArr)
            { 
                //change the status of interview to InterviewComplete
                //send rejection letter
                if (Request.Form[id] != null)
                {
                    Guid curApplicantId = new Guid(id);
                    string[] interviewIdArr = Request.Form[id].Split(',');
                    foreach (string interviewId in interviewIdArr)
                    {
                        Interview interview = Interview.GetInterviewById(Convert.ToInt32(interviewId));
                        interview.InterviewStatus = InterviewStatusEnum.Rejected;
                        interview.Update();

                        ApplicantBasicInfo.ChangeApplicantStatus(interview.ApplicantId, ApplicationStatusEnum.Rejected);
                        MailHelper mailHelper = new MailHelper();
                        mailHelper.AddInterviewVariables(interview.InterviewId);
                        mailHelper.SendMail(MailType.RejectApplicant);
                        if (ApplicantBasicInfo.GetApplicantBasicInfoById(interview.ApplicantId).ReferralId > 0)
                        {
                            mailHelper.SendMail(MailType.RejectReferral);
                        }
                        //MailHelper.SendRejectMailToApplicant(interview.InterviewId);
                        Interview.UpdateDecisionMailStatus(interview.InterviewId);
                    }
                }
                else
                {
                    throw new Exception("Can't find related interview!");
                }
            }
            BindData();
            JSUtility.Alert(this, "Send the reject letters successfully!");
        }
        else
        {
            JSUtility.Alert(this, "None of applicant has been selected!");
        }

        BindData();
    }
    protected void gvDecidedApplicants_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvDecidedApplicants.PageIndex = e.NewPageIndex;
        BindData();
    }

    protected void gvDecidedApplicants_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //switch (e.CommandName)
        //{ 
        //    case "Reject":
        //        RejectApplicant(Convert.ToInt32(e.CommandArgument));
        //        break;
        //    default:
        //        break;
        //}
        //BindData();
    }

    protected void gvDecidedApplicants_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    TableCell curCell = e.Row.Cells[3];
        //    DataRowView curData = (DataRowView)e.Row.DataItem;
        //    string curArg = Convert.ToString(curData["InterviewId"]);
        //    Button btnReject = (Button)curCell.FindControl("btnReject");
        //    btnReject.CommandArgument = curArg;
        //}
    }

    protected void btnMultiRecommend_Click(object sender, EventArgs e)
    {
        if (Session["referrals"] != null)
        {
            Session["referrals"] = null;
        }

        if (Request.Form["cbChecked"] != null)
        {
            string[] idArr = Request.Form["cbChecked"].ToString().Split(',');
            Session["referrals"] = idArr;
            JSUtility.OpenNewWindow(this, "MultiReferral.aspx", string.Empty);
            BindData();
        }
        else
        {
            JSUtility.Alert(this, "None of applicant has been selected!");
        }
    }
    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }

    protected void btnHire_Click(object sender, EventArgs e)
    {
        if (Request.Form["cbChecked"] != null)
        {
            string[] idArr = Request.Form["cbChecked"].ToString().Split(',');
            foreach (string id in idArr)
            {
                //change the status of interview to InterviewComplete
                //send rejection letter
                if (Request.Form[id] != null)
                {
                    Guid curApplicantId = new Guid(id);
                    string[] interviewIdArr = Request.Form[id].Split(',');
                    foreach (string strInterviewId in interviewIdArr)
                    {
                        Int32 interviewId = Convert.ToInt32(strInterviewId);

                        //MailHelper.SendHireMailToApplicant(interviewId);
                        MailHelper mailHelper = new MailHelper();
                        mailHelper.AddInterviewVariables(interviewId);
                        mailHelper.SendMail(MailType.HireApplicant);
                        Interview.UpdateDecisionMailStatus(interviewId);
                    }
                }
                else
                {
                    throw new Exception("Can't find related interview!");
                }
            }
            BindData();
            JSUtility.Alert(this, "Send the hire letters successfully!");
        }
        else
        {
            JSUtility.Alert(this, "None of applicant has been selected!");
        }

        BindData();
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (Request.Form["cbChecked"] != null)
        {
            string[] idArr = Request.Form["cbChecked"].ToString().Split(',');
            foreach (string id in idArr)
            {
                //change the status of interview to InterviewComplete
                //send rejection letter
                if (Request.Form[id] != null)
                {
                    Guid curApplicantId = new Guid(id);
                    string[] interviewIdArr = Request.Form[id].Split(',');
                    foreach (string strInterviewId in interviewIdArr)
                    {
                        Int32 interviewId = Convert.ToInt32(strInterviewId);
                        Interview.UpdateDecisionMailStatus(interviewId);
                    }
                }
                else
                {
                    throw new Exception("Can't find related interview!");
                }
            }
            BindData();
            JSUtility.Alert(this, "Deleted successfully!");
        }
        else
        {
            JSUtility.Alert(this, "None of applicant has been selected!");
        }

        BindData();
    }
}
