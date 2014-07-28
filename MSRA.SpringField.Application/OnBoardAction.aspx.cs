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
using MSRA.SpringField.Components.Enumerations;
using MSRA.SpringField.Components.Configuration;
using MSRA.SpringField.Components.BizObjects;
using MSRA.SpringField.Components;

/*
 * HireAction for incruiter to turn applicant's status to Hire/Onboard
 * Author: Yuanqin
 * Date: 2011.3.8 
 */ 
namespace MSRA.SpringField.Application
{
    public partial class OnBoardAction : System.Web.UI.Page
    {
        private Guid applicantId;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string[] gmArr = Roles.GetUsersInRole(RoleType.GroupManager.ToString());
                ListItem item;
                SiteUser curUser;
                foreach (string curStr in gmArr)
                {
                    curUser = new SiteUser(curStr);
                    item = new ListItem(curUser.RealName, curStr);
                    ddlGroupManager.Items.Add(item);
                }

                if (Request["applicant"] != null)
                {
                    string idStr = GlobalHelper.ClearInput(Request["applicant"].ToString(), 36, false);
                    if (!string.IsNullOrEmpty(idStr))
                    {
                        try
                        {
                            applicantId = new Guid(idStr);
                        }
                        catch
                        {
                            JSUtility.Alert(this, "Invalid parameter!");
                            JSUtility.CloseWindow(this);
                            return;
                        }

                        ApplicantBasicInfo abi = ApplicantBasicInfo.GetApplicantBasicInfoById(applicantId);
                        ApplicantEduBackground aeb = ApplicantEduBackground.GetApplicantEduBackgroundById(applicantId);
                        
                        tbFirstName.Text = abi.FirstName;
                        tbLastName.Text = abi.LastName;
                        tbChineseName.Text = abi.NameInChinese;
                        tbUniversity.Text = aeb.HighestEduInstitution;
                        tbMajor.Text = aeb.Major;
                    }
                }
            }
            //日历js
            btnEnrollDate.Attributes.Add("onclick", "popUpCalendar(this,document.all." + tbEnrollDate.ClientID + ",'yyyy-mm-dd');");
            btnGraduateDate.Attributes.Add("onclick", "popUpCalendar(this,document.all." + tbGraduateDate.ClientID + ",'yyyy-mm-dd');");
            tbEnrollDate.Attributes.Add("onclick", "popUpCalendar(this,document.all." + tbEnrollDate.ClientID + ",'yyyy-mm-dd');");
            tbGraduateDate.Attributes.Add("onclick", "popUpCalendar(this,document.all." + tbGraduateDate.ClientID + ",'yyyy-mm-dd');");
            tbEnrollDate.Attributes.Add("readonly", "true");
            tbGraduateDate.Attributes.Add("readonly", "true");
        }

        protected void btnHire_Click(object sender, EventArgs e)//Submit Request
        {
            if (!StaticData.FTEDict.ContainsKey(((TextBox)CheckInFormEdit1.FindControl("tbMentor")).Text.ToLower().Trim()))
            {
                ((Label)CheckInFormEdit1.FindControl("lbErrorReportMentor")).Text = "Mentor alias isn't correct, Please input again!";
                JSUtility.Alert(this.Page, "Mentor alias isn't correct, Please input again!");
                return;
            }

            if (IsValid)
            {
                applicantId = new Guid(Request["applicant"].ToString());
                ApplicantBasicInfo abi = ApplicantBasicInfo.GetApplicantBasicInfoById(applicantId);

                if (abi != null)
                {
                    abi.Status = ApplicationStatusEnum.Hired;
                    abi.Update();
                }

                //aeb.EnrollDate = Convert.ToDateTime(tbEnrollDate.Text);
                //aeb.GraduateDate = Convert.ToDateTime(tbGraduateDate.Text);
                

                Interview curInterview = new Interview();
                curInterview.ApplicantId = applicantId;
                curInterview.StartDate = Convert.ToDateTime(tbEnrollDate.Text);

                try
                {
                    curInterview.HiringManagerId = new Guid(SiteUser.GetUserIdByAlias(CheckInFormEdit1.GetCheckInForm().MentorAlias.Trim()));//SiteUser.Current.SiteUserId;
                }
                catch
                {
                    curInterview.HiringManagerId = SiteUser.Current.SiteUserId;
                }
                curInterview.HiringManagerResult = true;
                curInterview.HiringManagerComment = GlobalHelper.ClearInput(tbComment.Text, 4000, true);
                curInterview.GroupManagerId = SiteUser.GetIdByFullName(ddlGroupManager.SelectedValue);

                CheckInForm form = CheckInFormEdit1.GetCheckInForm();
                form.Insert();
                curInterview.MentorAlias = form.MentorAlias;
                curInterview.CheckInFormId = form.FormId;
                Int32 interviewId = curInterview.Insert();

                curInterview.InterviewStatus = InterviewStatusEnum.Hired; //curInterview.InterviewStatus = InterviewStatusEnum.WaitingForGroupManagerDecision;
                curInterview.GroupManagerResult = true;
                curInterview.ManagerDecisionTime = Convert.ToDateTime(tbGraduateDate.Text);
                curInterview.EndDate = Convert.ToDateTime(tbGraduateDate.Text);

                curInterview.MentorDecisionTime = Convert.ToDateTime(tbEnrollDate.Text);
                curInterview.Update();

                Response.Redirect("ShowApplication.aspx?applicant=" + applicantId.ToString());
            }
        }

        protected void btnPreview_Click(object sender, EventArgs e)
        {
            //if (Page.IsValid)
            {
                SwitchToPreview();
            }
        }

        private void SwitchToPreview()
        {
            CheckInFormView1.SetCheckInForm(CheckInFormEdit1.GetCheckInForm());
            CheckInFormEdit1.Visible = false;
            CheckInFormView1.Visible = true;
            this.btnOfflineHire.Visible = true;
            btnPreviewRequest.Visible = false;
            divPreview.Visible = false;
            btnBack.Visible = true;
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            SwitchToEdit();
        }

        private void SwitchToEdit()
        {
            CheckInFormEdit1.Visible = true;
            CheckInFormView1.Visible = false;
            this.btnOfflineHire.Visible = true;
            btnPreviewRequest.Visible = true;
            divPreview.Visible = true;
            btnBack.Visible = false;
        }

    }
}
