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

public partial class MultiInterview : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //lbxApplicantList.Enabled = false;
        if (!IsPostBack)
        { 
            if (Session["interviewees"] != null)
            {
                ApplicantBasicInfo abi = null;
                string[] idArr = (string[])Session["interviewees"];
                foreach (string applicantId in idArr)
                {
                    abi = ApplicantBasicInfo.GetApplicantBasicInfoById(new Guid(applicantId));
                    if (abi.Status == ApplicationStatusEnum.InterviewinProcess && Interview.GetCurrentInterview(abi.ApplicantId, SiteUser.Current.SiteUserId) == null)
                    {
                        JSUtility.Alert(this, "one applicant is processing by another people, please contact Intern Recruiter to solve the conflict!");
                        JSUtility.CloseWindow(this);
                        return;
                    }
                    else if (abi.Status == ApplicationStatusEnum.OnBoard)
                    {
                        JSUtility.Alert(this, "you can't schedule an interview for an on board intern!");
                        JSUtility.CloseWindow(this);
                        return;
                    }
                    else if (abi.Status == ApplicationStatusEnum.Hired)
                    {
                        JSUtility.Alert(this, "you can't schedule an interview for an hired intern!");
                        JSUtility.CloseWindow(this);
                        return;
                    }
                    //lbxApplicantList.Items.Add(abi.FirstName + abi.LastName);
                    literalApplicantList.Text += abi.FirstName;
                    literalApplicantList.Text += " ";
                    literalApplicantList.Text += abi.LastName;
                    literalApplicantList.Text += "<br/>";
                }
            }
            else
            {
                btnArrange.Enabled = false;
                JSUtility.Alert(this, "You can't use this function without selecting applicants from the main page!");
                JSUtility.CloseWindow(this);
                btnArrange.Visible = false;
            }        
        }
    }

    protected void btnArrange_Click(object sender, EventArgs e)
    {
        if (Request["txt_due_date"] != null && tbInterviewer.Text != string.Empty)
        {
            DateTime dueDate;
            try
            {
                dueDate = Convert.ToDateTime(Request["txt_due_date"]);
                if (dueDate < DateTime.Now)
                {
                    JSUtility.Alert(this, "Due date should not be in the past!");
                    return;
                }
            }
            catch
            {
                JSUtility.Alert(this,"Input is uncorrect!");
                return;
            }

            if (Session["interviewees"] != null)
            {
                Guid applicantId = Guid.Empty;
                Feedback newFeedback = null;

                List<string> interviewers = null;

                try
                {
                    interviewers = GlobalHelper.FormatAlias(tbInterviewer.Text);
                }
                catch
                {
                    JSUtility.Alert(this, "Illegal email alias detected!");
                }

                string[] idArr = (string[])Session["interviewees"];

                foreach (string id in idArr)
                {
                    applicantId = new Guid(id);
                    Interview curInterview = Interview.GetCurrentInterview(applicantId, SiteUser.Current.SiteUserId);

                    //curInterview does not exist
                    if (curInterview == null)
                    {
                        //Create a new interview
                        Interview newInterview = new Interview();
                        newInterview.ApplicantId = applicantId;
                        newInterview.StartDate = DateTime.Now;
                        newInterview.HiringManagerId = SiteUser.Current.SiteUserId;
                        newInterview.InterviewStatus = InterviewStatusEnum.WaitingForInterviewFeedback;
                        newInterview.Insert();
                        curInterview = newInterview;
                        //cashing candidate's current information to another data table for reporting
                        CashedApplicantInfo.AddCashedApplicant(applicantId, newInterview.InterviewId);
                    }

                    //Change Applicant Status
                    ApplicantBasicInfo.ChangeApplicantStatus(applicantId, ApplicationStatusEnum.InterviewinProcess);

                    foreach (string interviewer in interviewers)
                    {
                        //Create a new Feedback
                        newFeedback = new Feedback();
                        newFeedback.ApplicantId = applicantId;
                        newFeedback.DueDate = Convert.ToDateTime(Request["txt_due_date"]);
                        newFeedback.IsComplete = false;
                        newFeedback.InterviewId = curInterview.InterviewId;
                        newFeedback.InterviewerAlias = interviewer.Trim();
                        newFeedback.Insert();
                        MailHelper mailHelper = new MailHelper();
                        mailHelper.AddFeedbackVariables(newFeedback.FeedBackId);
                        mailHelper.SendMail(MailType.ArrangeInterview);
                    }
                }

                Session["interviewees"] = null;
                btnArrange.Visible = false;
                tbInterviewer.ReadOnly = true;
                JSUtility.Alert(this, @"Interview is arranged.\nA notification email has been sent to interviewer.\nPlease close this window.");
                JSUtility.CloseWindow(this);
            }
        }
    }
}
