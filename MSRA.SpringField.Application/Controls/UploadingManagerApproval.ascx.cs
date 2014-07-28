/*****************************************************************************
 * Created by Yi Shao at 2009-06-04
 * Abstract:used for uploading manager approval Email
*****************************************************************************/

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
using MSRA.SpringField.Components;
using MSRA.SpringField.Components.Configuration;

namespace MSRA.SpringField.Application.Controls
{
    public partial class Controls_UploadingManagerApproval : System.Web.UI.UserControl
    {
        private Interview interview;
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

            /*
            * For recruiter to modify incomplete data
            * Author: Yuanqin
            * Date: 2011.3.10
            */
            if (Roles.IsUserInRole(SiteUser.Current.FullName, RoleType.InternRecruiter.ToString()))
            {
                CheckBox1.Visible = true;
            }
            else
            {
                CheckBox1.Visible = false;
            }
        }

        //Upload Group Manager's Approval
        //Added by Yi Shao at 2009-06-04
        private bool UploadGMApproval()
        {
            //interview = Interview.GetInterviewById(InterviewID);

            Document curDoc = null;
            if (fuGMApproval.HasFile)
            {
                string ext = System.IO.Path.GetExtension(fuGMApproval.FileName).ToLower();
                if (ext == ".msg" || ext == ".txt" || ext == ".docx" || ext == ".doc" || ext == ".htm" || ext == ".html" || ext == ".mht")
                {
                    int id = interview.InterviewId;
                    string newFileName = id.ToString() + "_GMApproval" + ext;
                    try
                    {
                        fuGMApproval.SaveAs(SiteConfiguration.GetConfig().SiteAttributes["docPath"] + newFileName);
                    }
                    catch
                    {
                        lbGMApproval.Text = curDoc.OriginalName + " uploaded failed!";
                        return false;
                    }

                    if (interview.GMApproval != null && interview.GMApproval.DocId != 0)
                    {
                        curDoc = interview.GMApproval;
                    }
                    else
                    {
                        curDoc = new Document();
                    }
                    curDoc.SaveName = newFileName;
                    curDoc.OriginalName = fuGMApproval.FileName;
                    curDoc.DocType = DocumentEnum.Approval;
                    curDoc.ApplicantId = interview.ApplicantId;

                    if (interview.GMApproval != null && interview.GMApproval.DocId != 0)
                    {
                        curDoc.Update();
                    }
                    else
                    {
                        curDoc.Insert();
                    }

                    interview.GMApproval = curDoc;
                    if (curDoc.SaveName.IndexOf('.') == -1)
                    {
                        //default is ".msg"
                        interview.GMApprovalExt = ".msg";
                    }
                    else
                    {
                        string[] temp = curDoc.SaveName.Split('.');
                        interview.GMApprovalExt = String.Format(".{0}", temp[temp.Length - 1]);
                    }
                    lbGMApproval.Text = curDoc.OriginalName + " uploaded successfully!";
                    //interview.Update();
                }
                else
                {
                    lbGMApproval.Text = "system don't allow to upload file in this type.legal type: .msg .txt .html .htm .mht .docx .doc";
                    return false;
                }
            }
            return true;
        }

        protected void btnGMApproval_Click(object sender, EventArgs e)
        {
            //upload approval Email
            //change interview status to InterviewComplete
            //send mail to Hiring Manager
            ApplicantBasicInfo abi;
            SiteConfiguration config;
            interview = Interview.GetInterviewById(InterviewID);

            bool isUploaded = UploadGMApproval();
            if (!isUploaded)
                return;

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

                /*
                * For recruiter to modify incomplete data
                * Author: Yuanqin
                * Date: 2011.3.10
                */
                if (CheckBox1.Checked == false)
                {
                    if (abi.ReferralId > 0)
                    {
                        mailHelper.SendMail(MailType.HireReferral);
                    }
                    mailHelper.SendMail(MailType.OnBoardReminder);
                }

                Interview.DeleteIncompleteFeedbackForInterview(interview.InterviewId);
            }
            /*
             * Add by Yuanqin, 2011.5.5
             * For qualifiedbutnotmatched status
             */ 
            else if (gmSuggestion == 2)
            {
                ApplicantBasicInfo.ChangeApplicantStatus(interview.ApplicantId, ApplicationStatusEnum.QualifiedButNotMatched);
                interview.InterviewStatus = InterviewStatusEnum.QualifiedButNotMatched;
                interview.GroupManagerResult = false;
                interview.GroupManagerComment = GlobalHelper.ClearInput(tbGMComment.Text, 4000, true);
                interview.EndDate = DateTime.Now;

                //CR:add the manager decision time.
                interview.ManagerDecisionTime = DateTime.Now;
                interview.Update();

                Interview.DeleteIncompleteFeedbackForInterview(interview.InterviewId);

                MailHelper mailHelper = new MailHelper();
                mailHelper.AddInterviewVariables(interview.InterviewId);
                mailHelper.SendMail(MailType.RejectMailToHM);
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
                mailHelper.AddInterviewVariables(interview.InterviewId);
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
}