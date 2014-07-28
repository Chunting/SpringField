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
using MSRA.SpringField.Components.Enumerations;
using MSRA.SpringField.Components.Configuration;
using MSRA.SpringField.Components;

namespace MSRA.SpringField.Application.Controls
{
    public partial class Controls_MentorSuggestion : System.Web.UI.UserControl
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

        /*
         * Modify Interview Process
         * Author: Yin.P
         * Date: 2010-1-5
         */
        public void SetCheckInFormMentor(string mentoralias)
        {
            this.CheckInFormEdit1.MentorAlias = mentoralias;
        }

        private void LoadData()
        {
            //ChangeHint();
            string[] gmArr = Roles.GetUsersInRole(RoleType.GroupManager.ToString());
            ListItem item;
            SiteUser curUser;

            foreach (string curStr in gmArr)
            {
                curUser = new SiteUser(curStr);
                item = new ListItem(curUser.RealName, curStr);
                ddlGroupManager.Items.Add(item);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            {
                Panel2.Visible = false;
                Panel3.Visible = false;
                
                SwitchToOther();

            }
            if (ddlGroupManager.Items.Count == 0)
            {
                LoadData();
            }

            btnScheduleInterview.Attributes.Add("onclick", "OpenArrangeInterview('" + Interview.GetInterviewById(InterviewID).ApplicantId.ToString() + "')");
        }

        //private void ChangeHint()
        //{
        //    Interview interview = Interview.GetInterviewById(InterviewID);
        //    ApplicantBasicInfo curApp = ApplicantBasicInfo.GetApplicantBasicInfoById(interview.ApplicantId);
        //    string strAction = "";
        //    if (ddlHMSuggestion.Items[ddlHMSuggestion.SelectedIndex].Value == "-1")
        //    {
        //        //final reject
        //        strAction = "Final Reject " + curApp.FirstName + " " + curApp.LastName;
        //    }
        //    else if (ddlHMSuggestion.Items[ddlHMSuggestion.SelectedIndex].Value == "1")
        //    {
        //        //hire
        //        strAction = "Hire " + curApp.FirstName + " " + curApp.LastName;
        //    }
        //    else
        //    {
        //        //decline offer
        //        strAction = curApp.FirstName + " " + curApp.LastName + " Declined Offer";
        //    }

        //    //litHint.Text = "Your decision is:<br>" + "<p align=\"center\">" + strAction + "</p>";
        //}
        protected void btnHMApproval_Click(object sender, EventArgs e)
        {
            //change interview status to WaitingForGroupManagerDecision
            //and send mail to Group Manager
            //or change interview status to InterviewComplete
            SiteConfiguration config = SiteConfiguration.GetConfig();
            Interview interview = Interview.GetInterviewById(InterviewID);
            Int16 hmSuggestion = Convert.ToInt16(ddlHMSuggestion.SelectedValue);
            if (hmSuggestion == 1)
            {
                if (!StaticData.FTEDict.ContainsKey(((TextBox)CheckInFormEdit1.FindControl("tbMentor")).Text.ToLower().Trim()))
                {
                    ((Label)CheckInFormEdit1.FindControl("lbErrorReportMentor")).Text = "Mentor alias isn't correct, Please input again!";
                    SwitchToEdit();
                    JSUtility.Alert(this.Page, "Mentor alias isn't correct, Please input again!");
                    return;
                }
            }
            interview.HiringManagerResult = (hmSuggestion == 1) ? true : false;
            interview.HiringManagerComment = GlobalHelper.ClearInput(tbHMComment.Text, 4000, true);
            //if there is one interviewer, he/she is mentor.
            if (Feedback.CheckInterviewSameAsMentor(interview.InterviewId, SiteUser.Current.Alias))
            {
                DataSet dsFeedback = Feedback.GetIncompleteFeedbackByInterview(interview.InterviewId);
                if (dsFeedback.Tables[0].Rows.Count != 0)
                {
                    Int32 feedbackId = Convert.ToInt32(dsFeedback.Tables[0].Rows[0]["FeedbackId"]);
                    Feedback curFeedback = Feedback.GetFeedbackById(feedbackId);
                    curFeedback.InterviewerId = SiteUser.Current.SiteUserId;
                    curFeedback.InterviewerAlias = SiteUser.Current.Alias;
                    //curFeedback.Suggestion = (FeedbackSuggestionEnum)(Convert.ToInt32(ddlHMSuggestion.SelectedValue));
                    /*
                     * Modify by Yuanqin, 2011.5.7
                     * For Qualified but not matched status
                     */ 
                    //FeedbackSuggestionEnum suggest = (ddlHMSuggestion.SelectedValue == "-1") ? FeedbackSuggestionEnum.Reject : FeedbackSuggestionEnum.Hire;
                    FeedbackSuggestionEnum suggest = (ddlHMSuggestion.SelectedValue == "1") ? FeedbackSuggestionEnum.Hire : FeedbackSuggestionEnum.Reject;
                    curFeedback.Suggestion = suggest;
                    string feedbackContent = GlobalHelper.ClearInput(tbHMComment.Text, 4000, true);
                    curFeedback.FeedbackContent = feedbackContent;
                    curFeedback.InterviewDate = DateTime.Now;
                    curFeedback.IsComplete = true;
                    curFeedback.Update();
                }
            }
            if (hmSuggestion == 1)
            {
                interview.InterviewStatus = InterviewStatusEnum.WaitingForGroupManagerDecision;
                interview.GroupManagerId = SiteUser.GetIdByFullName(ddlGroupManager.SelectedValue);

                //Save CheckInForm
                CheckInFormEdit formEdit = Panel3.FindControl("CheckInFormEdit1") as CheckInFormEdit;
                CheckInForm form = formEdit.GetCheckInForm();
                form.Insert();

                //CR:add the mentor decision time.
                interview.MentorDecisionTime = DateTime.Now;

                //TODO: DateTime.MaxValue may lead to some problems.
                interview.ManagerDecisionTime = DateTime.MaxValue;
                interview.CheckInFormId = form.FormId;
                interview.Update();

                MailHelper mailHelper = new MailHelper();
                mailHelper.AddInterviewVariables(interview.InterviewId);
                mailHelper.SendMail(MailType.AskForApproval);
            }
            else if (hmSuggestion == -1)
            {
                interview.InterviewStatus = InterviewStatusEnum.Rejected;
                interview.GroupManagerResult = false;
                interview.ManagerDecisionTime = DateTime.Now;
                interview.MentorDecisionTime = DateTime.Now;
                interview.EndDate = DateTime.Now;
                interview.Update();
                ApplicantBasicInfo.ChangeApplicantStatus(interview.ApplicantId, ApplicationStatusEnum.Rejected);
                Interview.DeleteIncompleteFeedbackForInterview(interview.InterviewId);

                //delete interview's imcomplete feedbacks
            }
            /*
             * Add by Yuanqin, 2011.5.5
             * For Qualified but not mateched status
             */
            else if (hmSuggestion == 2)
            {
                interview.InterviewStatus = InterviewStatusEnum.QualifiedButNotMatched;
                interview.GroupManagerResult = false;
                interview.ManagerDecisionTime = DateTime.Now;
                interview.MentorDecisionTime = DateTime.Now;
                interview.EndDate = DateTime.Now;
                interview.Update();
                ApplicantBasicInfo.ChangeApplicantStatus(interview.ApplicantId, ApplicationStatusEnum.QualifiedButNotMatched);
                Interview.DeleteIncompleteFeedbackForInterview(interview.InterviewId);
            }
            else if (hmSuggestion == 0)
            {
                //Decline Offer
                interview.InterviewStatus = InterviewStatusEnum.OfferDeclined;
                interview.MentorDecisionTime = DateTime.Now;
                interview.EndDate = DateTime.Now;
                ApplicantBasicInfo.ChangeApplicantStatus(interview.ApplicantId, ApplicationStatusEnum.OfferDeclined);
                interview.Update();
                Interview.DeleteIncompleteFeedbackForInterview(interview.InterviewId);
            }

            Server.Transfer("OperationResult.aspx", true);
        }


        //protected void OkButton_Click(object sender, EventArgs e)
        //{
        //    //_sureHire = true;
        //    btnHMApproval_Click(btnHMApproval, null);
        //}

        protected void ddlHMSuggestion_SelectedIndexChanged(object sender, EventArgs e)
        {
            Int16 hmSuggestion = Convert.ToInt16(ddlHMSuggestion.SelectedValue);
            if (hmSuggestion == 1)
            {

                SwitchToEdit();
            }
            else
            {

                SwitchToOther();
            }
            //ChangeHint();
        }

        private void SwitchToEdit()
        {
            Panel2.Visible = true;
            Panel3.Visible = true;
            btnHMApproval.Visible = true;
            btnPreview.Visible = true;
            btnBack.Visible = false;
            CheckInFormEdit1.Visible = true;
            CheckInFormView1.Visible = false;
            Panel2.Visible = true;
            Panel3.Visible = true;
            btnScheduleInterview.Visible = true;
        }

        private void SwitchToPreview()
        {
            Panel2.Visible = true;
            Panel3.Visible = true;

            CheckInFormView1.SetCheckInForm(CheckInFormEdit1.GetCheckInForm());
            btnHMApproval.Visible = true;
            btnPreview.Visible = false;
            btnBack.Visible = true;
            CheckInFormEdit1.Visible = false;
            CheckInFormView1.Visible = true;
            btnScheduleInterview.Visible = false;
        }

        private void SwitchToOther()
        {
            btnHMApproval.Visible = true;
            btnPreview.Visible = false;
            btnBack.Visible = false;
            Panel2.Visible = false;
            Panel3.Visible = false;
            btnScheduleInterview.Visible = true;
        }
        protected void btnScheduleInterview_ServerClick(object sender, EventArgs e)
        {
            Interview interview = Interview.GetInterviewById(InterviewID);
            String strUrl = String.Format("ArrangeInterview.aspx?applicant={0}", interview.ApplicantId);
            JSUtility.OpenNewWindow(this.Page, strUrl, string.Empty);
        }

        protected void btnPreview_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                SwitchToPreview();
            }
            else
            {
                SwitchToEdit();
            }
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                SwitchToEdit();
            }
        }
    }
}