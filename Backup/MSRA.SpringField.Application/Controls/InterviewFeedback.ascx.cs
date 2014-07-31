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
using MSRA.SpringField.Components.BizObjects;
using MSRA.SpringField.Components;
using MSRA.SpringField.Components.Enumerations;

namespace MSRA.SpringField.Application.Controls
{
    public partial class Controls_InterviewFeedback : System.Web.UI.UserControl
    {
        public Int32 FeedbackId
        {
            get
            {
                if (ViewState["feedbackid"] == null)
                {
                    return 0;
                }
                else
                {
                    return Convert.ToInt32(ViewState["feedbackid"]);
                }
            }

            set
            {
                ViewState["feedbackid"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            //if(!IsPostBack)
            //{
            Feedback curFeedback = Feedback.GetFeedbackById(FeedbackId);
            ApplicantBasicInfo curApp = ApplicantBasicInfo.GetApplicantBasicInfoById(curFeedback.ApplicantId);
            litHint.Text = curApp.FirstName + " " + curApp.LastName;
            //}
        }

        protected void btnAddFeedback_Click(object sender, EventArgs e)
        {
            if (tbFeedbackContent.Text.Trim().Length == 0)
            {
                lbMessage.Text = "Comment is required";
                return;
            }
            //update curFeedback
            Feedback curFeedback = Feedback.GetFeedbackById(FeedbackId);
            curFeedback.InterviewerId = SiteUser.Current.SiteUserId;
            curFeedback.InterviewerAlias = SiteUser.Current.Alias;
            curFeedback.Suggestion = (FeedbackSuggestionEnum)(Convert.ToInt32(ddlSuggestion.SelectedValue));
            string feedbackContent = GlobalHelper.ClearInput(tbFeedbackContent.Text, 4000, true);
            curFeedback.FeedbackContent = feedbackContent;
            curFeedback.InterviewDate = DateTime.Now;
            curFeedback.IsComplete = true;
            curFeedback.Update();
            
            MailHelper mailHelper = new MailHelper();
            mailHelper.AddFeedbackVariables(curFeedback.FeedBackId);
            mailHelper.SendMail(MailType.FeedbackComplete);

            Server.Transfer("OperationResult.aspx", true);
        }

        protected void btnChangeInterviewer_Click(object sender, EventArgs e)
        {
            //String strUrl = String.Format("~/ChangeInterviewer.aspx?feedback={0}", FeedbackId);
            JSUtility.OpenNewWindow(this.Page, "ChangeInterviewer.aspx?feedback=" + FeedbackId, null);
            //Server.Transfer(strUrl);
        }
        protected void OkButton_Click(object sender, EventArgs e)
        {
            btnAddFeedback_Click(btnAddFeedback, null);
        }
    }
}