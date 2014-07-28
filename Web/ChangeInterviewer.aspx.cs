using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Springfield.Components;
using Springfield.Components.Configuration;

public partial class ChangeInterviewer : System.Web.UI.Page
{
    private int feedbackId = 0;
    private Feedback curFeedback;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request["feedback"] != null)
        {
            try
            {
                feedbackId = Convert.ToInt32(Request["feedback"]);
            }
            catch
            {
                JSUtility.Alert(this, "Invalid parameter!");
                JSUtility.CloseWindow(this);
                btnChange.Visible = false;
                return;
            }

            curFeedback = Feedback.GetFeedbackById(feedbackId);
            if (null == curFeedback)
            {
                JSUtility.Alert(this, "Invalid parameter!");
                JSUtility.CloseWindow(this);
                btnChange.Visible = false;
                return;
            }
            if (SiteUser.Current.Alias.ToLower() != curFeedback.InterviewerAlias.ToLower())
            {
                Interview curInterview = Interview.GetInterviewById(curFeedback.InterviewId);
                if ((curInterview.HiringManagerId != SiteUser.Current.SiteUserId || curFeedback.IsComplete == true) && !SiteUser.Current.IsInRole(RoleType.InternRecruiter))
                {
                    btnChange.Visible = false;
                    JSUtility.Alert(this, "You don't have the right for this operation!");
                    JSUtility.CloseWindow(this);
                }
            }
            basicInfo.ApplicantId = curFeedback.ApplicantId;
            basicInfo.BindData();
        }
        else
        {
            JSUtility.Alert(this,"Invalid parameter!");
            btnChange.Visible = false;
        }
    }

    protected void btnChange_Click(object sender, EventArgs e)
    {
        string newInterviewer = GlobalHelper.ClearInput(tbInterviewer.Text, 256, false);
        List<string> aliasArr;

        try
        {
            aliasArr = GlobalHelper.FormatAlias(newInterviewer);
        }
        catch
        {
            JSUtility.Alert(this, @"Illegal email address detected!\nYou can only type in one email alias or email address!");
            return;
        }

        if (aliasArr.Count != 1)
        {
            JSUtility.Alert(this, @"Illegal email address detected!\nYou can only type in one email alias or email address!");
            return;
        }

        //DateTime dueDate;
        if (newInterviewer == string.Empty)
        {
            JSUtility.Alert(this, "The new interviewer's alias should not be empty!");
        }
        else
        {
            //try
            //{
            //    dueDate = Convert.ToDateTime(Request["txt_due_date"]);
            //}
            //catch
            //{
            //    JSUtility.Alert(this,"Invalid date time!");
            //    return;
            //}

            //curFeedback.DueDate = dueDate;
            curFeedback.InterviewerAlias = newInterviewer;
            curFeedback.Update();

            MailHelper mailHelper = new MailHelper();
            mailHelper.AddFeedbackVariables(feedbackId);
            mailHelper.SendMail(MailType.InterviewerChange);

            btnChange.Visible = false;
            tbInterviewer.ReadOnly = true;
            JSUtility.Alert(this, @"A notification email has been sent to Hiring Manager and the new Interviewer.\nPlease close this window.");
            JSUtility.CloseWindow(this);
        }
    }


}
