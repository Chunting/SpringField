/*****************************************************************************
 * Created by Yi Shao at 2009-06-08
 * Abstract:used for uploading Mentor approval Email
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
using MSRA.SpringField.Components.Configuration;
using MSRA.SpringField.Components.BizObjects;
using MSRA.SpringField.Components;
using MSRA.SpringField.Components.Enumerations;

namespace MSRA.SpringField.Application.Controls
{
    public partial class Controls_UploadingMentorSuggestion : System.Web.UI.UserControl
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

        private Interview interview;

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


        /// <summary>
        /// upload Mentor approval email
        /// </summary>
        /// <returns></returns>
        private bool UploadMentorApproval()
        {
            //interview = Interview.GetInterviewById(InterviewID);

            Document curDoc = null;
            if (fuMentorApproval.HasFile)
            {
                string ext = System.IO.Path.GetExtension(fuMentorApproval.FileName).ToLower();
                if (ext == ".msg" || ext == ".txt" || ext == ".docx" || ext == ".doc" || ext == ".htm" || ext == ".html" || ext == ".mht")
                {
                    int id = interview.InterviewId;
                    string newFileName = id.ToString() + "_MentorApproval" + ext;
                    try
                    {
                        fuMentorApproval.SaveAs(SiteConfiguration.GetConfig().SiteAttributes["docPath"] + newFileName);
                    }
                    catch
                    {
                        lbMentorApproval.Text = curDoc.OriginalName + " uploaded failed!";
                        return false;
                    }

                    if (interview.MentorApproval != null && interview.MentorApproval.DocId != 0)
                    {
                        curDoc = interview.MentorApproval;
                    }
                    else
                    {
                        curDoc = new Document();
                    }
                    curDoc.SaveName = newFileName;
                    curDoc.OriginalName = fuMentorApproval.FileName;
                    curDoc.DocType = DocumentEnum.Approval;
                    curDoc.ApplicantId = interview.ApplicantId;

                    if (interview.MentorApproval != null && interview.MentorApproval.DocId != 0)
                    {
                        curDoc.Update();
                    }
                    else
                    {
                        curDoc.Insert();
                    }

                    interview.MentorApproval = curDoc;
                    if (curDoc.SaveName.IndexOf('.') == -1)
                    {
                        //default is ".msg"
                        interview.MentorApprovalExt = ".msg";
                    }
                    else
                    {
                        string[] temp = curDoc.SaveName.Split('.');
                        interview.MentorApprovalExt = String.Format(".{0}", temp[temp.Length - 1]);
                    }
                    lbMentorApproval.Text = curDoc.OriginalName + " uploaded successfully!";
                    //interview.Update();
                }
                else
                {
                    lbMentorApproval.Text = "system don't allow to upload file in this type.legal type: .msg .txt .html .htm .mht .docx .doc";
                    return false;
                }
            }
            return true;
        }

        protected void btnHMApproval_Click(object sender, EventArgs e)
        {
            //change interview status to hired
            //and send mail to On Boarding Manager
            //or change interview status to reject or declined
            SiteConfiguration config = SiteConfiguration.GetConfig();
            interview = Interview.GetInterviewById(InterviewID);
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

            bool isUploadSuccess = UploadMentorApproval();
            if (!isUploadSuccess)
            {
                lbMentorApproval.Text = "failed to upload. please try again!";
                return;
            }

            if (hmSuggestion == 1)
            {
                interview.InterviewStatus = InterviewStatusEnum.Hired;
                interview.GroupManagerId = SiteUser.GetIdByFullName(ddlGroupManager.SelectedValue);

                //Save CheckInForm
                CheckInFormEdit formEdit = Panel3.FindControl("CheckInFormEdit1") as CheckInFormEdit;
                CheckInForm form = formEdit.GetCheckInForm();
                form.Insert();

                //CR:add the mentor decision time.
                interview.MentorDecisionTime = DateTime.Now;
                interview.ManagerDecisionTime = DateTime.Now;
                interview.GroupManagerResult = true;
                interview.HiringManagerComment = GlobalHelper.ClearInput(tbHMComment.Text, 4000, true);
                interview.EndDate = DateTime.Now;
                interview.CheckInFormId = form.FormId;
                interview.Update();
                ApplicantBasicInfo abi = ApplicantBasicInfo.GetApplicantBasicInfoById(interview.ApplicantId);

                config = SiteConfiguration.GetConfig();

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
                    //mailHelper.SendMail(MailType.OnBoardReminder);
                    mailHelper.SendMail(MailType.AskForApproval);
                }
                else
                {
                    abi.Status = ApplicationStatusEnum.Hired;
                    abi.Update();
                }

                Interview.DeleteIncompleteFeedbackForInterview(interview.InterviewId);
            }
            /*
             * Add by Yuanqin, 2011.5.5
             * For qualifiedbutnotmatched status
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
            //btnScheduleInterview.Visible = true;
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
            //btnScheduleInterview.Visible = false;
        }

        private void SwitchToOther()
        {
            btnHMApproval.Visible = true;
            btnPreview.Visible = false;
            btnBack.Visible = false;
            Panel2.Visible = false;
            Panel3.Visible = false;
            //btnScheduleInterview.Visible = true;
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