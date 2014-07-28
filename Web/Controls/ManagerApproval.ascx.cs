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

public partial class Controls_ManagerApproval : System.Web.UI.UserControl
{
    public Int32 InterviewID
    {
        get
        {
            if (ViewState["interviewid"] == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(ViewState["interviewid"]);
            }
        }

        set
        {
            ViewState["interviewid"] = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!IsPostBack)
        //{
            ChangeHint();
        //}
    }

    protected void btnGMApproval_Click(object sender, EventArgs e)
    {
        //change interview status to InterviewComplete
        //send mail to Hiring Manager
        ApplicantBasicInfo abi;
        SiteConfiguration config;
        Interview interview = Interview.GetInterviewById(InterviewID);
        Int16 gmSuggestion = Convert.ToInt16(ddlGMSuggestion.SelectedValue);
        if (gmSuggestion == 1)
        {
            //Hire
            ApplicantBasicInfo.ChangeApplicantStatus(interview.ApplicantId, ApplicationStatusEnum.Hired);
            interview.InterviewStatus = InterviewStatusEnum.Hired;
            interview.GroupManagerResult = true;
            interview.GroupManagerComment = GlobalHelper.ClearInput(tbGMComment.Text, 4000, true);
            interview.EndDate = DateTime.Now;

            //CR:add the manager decision time.
            interview.ManagerDecisionTime = DateTime.Now;
            interview.Update();

            abi = ApplicantBasicInfo.GetApplicantBasicInfoById(interview.ApplicantId);

            config = SiteConfiguration.GetConfig();
            
            // If the applicant is referred by emplyee in MSRA, a mail should sent to the referrer
            // To: Referrer CC: Intern Recruiter
            MailHelper mailHelper = new MailHelper();
            mailHelper.AddInterviewVariables(interview.InterviewId);
            
            if (abi.ReferralId > 0)
            {
                mailHelper.SendMail(MailType.HireReferral);
            }
            mailHelper.SendMail(MailType.OnBoardReminder);

            Interview.DeleteIncompleteFeedbackForInterview(interview.InterviewId);
        }
        else if (gmSuggestion == -1)
        {
            //Reject
            ApplicantBasicInfo.ChangeApplicantStatus(interview.ApplicantId, ApplicationStatusEnum.Rejected);
            interview.InterviewStatus = InterviewStatusEnum.Rejected;
            interview.GroupManagerResult = false;
            interview.GroupManagerComment = GlobalHelper.ClearInput(tbGMComment.Text, 4000, true);
            interview.EndDate = DateTime.Now;

            //CR:add the manager decision time.
            interview.ManagerDecisionTime = DateTime.Now;
            interview.Update();

            Interview.DeleteIncompleteFeedbackForInterview(interview.InterviewId);
            
            MailHelper mailHelper = new MailHelper();
            mailHelper.AddInterviewVariables( interview.InterviewId);
            mailHelper.SendMail(MailType.RejectMailToHM);
        }
        else
        {
            //Decline Offer
            interview.InterviewStatus = InterviewStatusEnum.OfferDeclined;
            interview.ManagerDecisionTime = DateTime.Now;
            interview.EndDate = DateTime.Now;
            ApplicantBasicInfo.ChangeApplicantStatus(interview.ApplicantId, ApplicationStatusEnum.OfferDeclined);
            interview.Update();
            Interview.DeleteIncompleteFeedbackForInterview(interview.InterviewId); 
        }
        Server.Transfer("OperationResult.aspx", true);
    }



    protected void OkButton_Click(object sender, EventArgs e)
    {
        btnGMApproval_Click(btnGMApproval, null);
    }
    protected void ddlGMSuggestion_SelectedIndexChanged(object sender, EventArgs e)
    {
        ChangeHint();
    }

    private void ChangeHint()
    {
        Interview interview = Interview.GetInterviewById(InterviewID);
        ApplicantBasicInfo curApp = ApplicantBasicInfo.GetApplicantBasicInfoById(interview.ApplicantId);
        litHint.Text = "you want to " + ddlGMSuggestion.Items[ddlGMSuggestion.SelectedIndex].Text + "  " + curApp.FirstName + " " + curApp.LastName;
    }
}
