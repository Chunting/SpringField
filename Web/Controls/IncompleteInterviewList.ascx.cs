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

public partial class Controls_IncompleteInterviewList : System.Web.UI.UserControl
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
    protected void Page_Init(object sender, EventArgs e)
    {
        //if (!this.IsPostBack)
        //{
            DataSet dsIncomplete = Feedback.GetIncompleteFeedbackByInterview(InterviewID);
            dlIncompleteFeedback.DataSource = dsIncomplete;
            dlIncompleteFeedback.DataBind();
            if (dsIncomplete.Tables[0].Rows.Count == 0)
            {
                panAll.Visible = false;
            }
        //}
    }

    protected void dlIncompleteFeedback_ItemCommand(object source, DataListCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "CancelInterview":
                //Interview interview = Interview.GetInterviewById(InterviewID);
                //if (SiteUser.Current.SiteUserId == interview.HiringManagerId || SiteUser.Current.IsInRole(RoleType.InternRecruiter))
                //{
                //    int feedbackId = Convert.ToInt32(e.CommandArgument);
                //    Feedback.DeleteFeedbackById(feedbackId);
                //    SiteCache.Remove("incomplete_feedbacks_" + interview.InterviewId.ToString());
                //    DataSet dsIncomplete = Feedback.GetIncompleteFeedbackByInterview(interview.InterviewId);
                //    if (dsIncomplete.Tables[0].Rows.Count <= 0)
                //    {
                //        Interview.DeleteInterviewById(interview.InterviewId);//if all feedbacks are deleted, interview should be cancelled.
                //        ApplicantBasicInfo.ChangeApplicantStatus(interview.ApplicantId, ApplicationStatusEnum.ApplicationComplete);// set the status to avaiable.
                //    }
                //    Server.Transfer("OperationResult.aspx", true);
                //}
                break;
            default:
                break;
        }
    }

    protected void OkButton_Click(Object sender, CommandEventArgs e) 
    {
        Interview interview = Interview.GetInterviewById(InterviewID);
        if (SiteUser.Current.SiteUserId == interview.HiringManagerId || SiteUser.Current.IsInRole(RoleType.InternRecruiter.ToString()))
        {
            int feedbackId = Convert.ToInt32(e.CommandArgument);
            Feedback.DeleteFeedbackById(feedbackId);
            SiteCache.Remove("incomplete_feedbacks_" + interview.InterviewId.ToString());
            DataSet dsIncomplete = Feedback.GetIncompleteFeedbackByInterview(interview.InterviewId);
            DataSet dsComplete = Feedback.GetFeedbackByInterview(interview.InterviewId);
            int Count = dsIncomplete.Tables[0].Rows.Count + dsComplete.Tables[0].Rows.Count;
            if (Count <= 0)
            {
                Interview.DeleteInterviewById(interview.InterviewId);//if all feedbacks are deleted, interview should be cancelled.
                ApplicantBasicInfo.ChangeApplicantStatus(interview.ApplicantId, ApplicationStatusEnum.Available);// set the status to avaiable.
            }
            Server.Transfer("OperationResult.aspx", true);
        }
    }
}
