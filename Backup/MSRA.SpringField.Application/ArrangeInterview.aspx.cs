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
using MSRA.SpringField.Components;
using MSRA.SpringField.Components.BizObjects;
using MSRA.SpringField.Components.Enumerations;

namespace MSRA.SpringField.Application
{
    public partial class ArrangeInterview : System.Web.UI.Page
    {
        private Guid applicantId;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (tbInterviewer.Attributes["id"] == null)
            {
                tbInterviewer.Attributes.Add("id", "txtInterviewer");
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

                    basicInfo.ApplicantId = applicantId;
                    basicInfo.BindData();

                    ApplicantBasicInfo abi = ApplicantBasicInfo.GetApplicantBasicInfoById(applicantId);
                    if (abi.Status == ApplicationStatusEnum.InterviewinProcess && Interview.GetCurrentInterview(applicantId, SiteUser.Current.SiteUserId) == null)
                    {
                        btnArrange.Visible = false;
                        JSUtility.Alert(this, "This applicant is processing by another people, please contact Intern Recruiter to solve the conflict!");
                        JSUtility.CloseWindow(this);
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
                    return;
                }
            }

            btnArrange.Visible = false;
            JSUtility.Alert(this, "Invalid parameter!");
            JSUtility.CloseWindow(this);
        }

        protected void btnArrange_Click(object sender, EventArgs e)
        {
            if (Request["txt_due_date"] != null && !string.IsNullOrEmpty(tbInterviewer.Text.Trim()))
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
                    JSUtility.Alert(this, "Input is incorrect!");
                    return;
                }

                List<string> interviewers;
                try
                {
                    interviewers = GlobalHelper.FormatAlias(tbInterviewer.Text);
                }
                catch
                {
                    JSUtility.Alert(this, "Illegal email alias detected!");
                    return;
                }

                Interview curInterview = Interview.GetCurrentInterview(applicantId, SiteUser.Current.SiteUserId);

                //curInterview does not exist
                if (curInterview == null)
                {
                    //Create a new interview
                    Interview newInterview = new Interview();
                    newInterview.ApplicantId = applicantId;
                    newInterview.StartDate = DateTime.Now;
                    newInterview.HiringManagerId = SiteUser.Current.SiteUserId;// LMM 2012-09-04 这里是不是有问题？ans:没问题，招聘经理不是group manager,就是当前正在使用的用户
                    newInterview.InterviewStatus = InterviewStatusEnum.WaitingForInterviewFeedback;

                   /*
                    * Modify Interview Process
                    * Author: Yin.P
                    * Date: 2010-1-5
                    */
                    newInterview.MentorAlias = this.txtMentor.Text;
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
                    Feedback newFeedback = new Feedback();
                    newFeedback.ApplicantId = applicantId;
                    newFeedback.DueDate = dueDate;
                    newFeedback.IsComplete = false;
                    newFeedback.InterviewId = curInterview.InterviewId;
                    newFeedback.InterviewerAlias = interviewer.Trim();
                    newFeedback.Insert();

                    MailHelper mailHelper = new MailHelper();
                    mailHelper.AddFeedbackVariables(newFeedback.FeedBackId);
                    mailHelper.SendMail(MailType.ArrangeInterview);
                    //MailHelper.SendArrangeInterviewMail(dueDate,newFeedback.FeedBackId);
                }

                btnArrange.Visible = false;
                tbInterviewer.ReadOnly = true;
                JSUtility.Alert(this, @"A notification email has been sent to interviewer.\nPlease close this window.");
                JSUtility.RedirectOpenerLocation(this);
                JSUtility.CloseWindow(this);
            }
        }


    }
}