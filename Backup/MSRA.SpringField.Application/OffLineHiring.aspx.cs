/*****************************************************************************
 * Modified by Yi Shao at 2009-06-08
 * Abstract:Update to support uploading Group approval Email
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
using MSRA.SpringField.Components.Enumerations;
using MSRA.SpringField.Components.Configuration;
using MSRA.SpringField.Components.BizObjects;
using MSRA.SpringField.Components;

namespace MSRA.SpringField.Application
{
    public partial class OffLineHiring : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                /*
                 * For recruiter to modify incomplete data
                 * Author: Yuanqin
                 * Date: 2011.3.25
                 */
                if (Roles.IsUserInRole(SiteUser.Current.FullName, RoleType.InternRecruiter.ToString()))
                {
                    CheckBox1.Visible = true;
                }
                else
                {
                    CheckBox1.Visible = false;
                }


                string[] gmArr = Roles.GetUsersInRole(RoleType.GroupManager.ToString());
                ListItem item;
                SiteUser curUser;

                foreach (string curStr in gmArr)
                {
                    curUser = new SiteUser(curStr);
                    item = new ListItem(curUser.RealName, curStr);
                    ddlGroupManager.Items.Add(item);
                }
                string[] GenderArray = EnumHelper.GetEnumStrings(typeof(GenderEnum));
                for (int i = 0; i < GenderArray.Length; i++)
                {
                    ddlGender.Items.Add(new ListItem(GenderArray[i], i.ToString()));
                }
                if (SiteUser.Current.IsInRole(RoleType.OnBoardManager))
                    lbTitle.Text = "Keyin Candidate Information";
            }

            btnEnrollDate.Attributes.Add("onclick", "popUpCalendar(this,document.all." + tbEnrollDate.ClientID + ",'yyyy-mm-dd');");
            btnGraduateDate.Attributes.Add("onclick", "popUpCalendar(this,document.all." + tbGraduateDate.ClientID + ",'yyyy-mm-dd');");
            tbEnrollDate.Attributes.Add("onclick", "popUpCalendar(this,document.all." + tbEnrollDate.ClientID + ",'yyyy-mm-dd');");
            tbGraduateDate.Attributes.Add("onclick", "popUpCalendar(this,document.all." + tbGraduateDate.ClientID + ",'yyyy-mm-dd');");
            tbEnrollDate.Attributes.Add("readonly", "true");
            tbGraduateDate.Attributes.Add("readonly", "true");
        }

        private Document UploadFile(string Name, Guid ApplicantId, FileUpload fuControl, string Postfix, DocumentEnum docType)
        {
            Document curDoc = null;

            /*
             * Modified - 
             * fuControl.HasFile would be false although there is a file in the fileupload control. The reason maybe the size of the file is 0KB.
             * So we must add a condition of fuControl.FileName.Length>0 into the if statement below.
             * @Author: Yin.Pu
             * @Date: 2009-11-17
             */
            if (fuControl.FileName.Length > 0 || fuControl.HasFile)
            {
                string ext = System.IO.Path.GetExtension(fuControl.FileName).ToLower();
                string newFileName = Name + Postfix + ext;
                fuControl.SaveAs(SiteConfiguration.GetConfig().SiteAttributes["docPath"] + newFileName);//存入硬盘
                curDoc = new Document();
                curDoc.SaveName = newFileName;
                curDoc.OriginalName = fuControl.FileName;
                curDoc.DocType = docType;
                curDoc.ApplicantId = ApplicantId;
                curDoc.Insert();//存入数据库信息
            }
            return curDoc;
        }

        private bool uploadResume(Guid applicantId, ref Document curResume)
        {
            curResume = UploadFile(applicantId.ToString(), applicantId, fuResume, "_cv", DocumentEnum.CV);
            if (curResume == null || curResume.DocId == 0)
            {
                lbResume.Text = "Fail to upload resume!";
                return false;
            }
            lbResume.Text = "Successfully uploaded resume!";
            return true;
        }

        private bool uploadApproval(int InterviewId, Guid applicantId, ref Document curApprovalEmail)
        {
            if (fuApprovalEmail.Visible == true)
            {
                curApprovalEmail = UploadFile(InterviewId.ToString(), applicantId, fuApprovalEmail, "_offlinehire_GMApproval", DocumentEnum.Approval);
                if (curApprovalEmail == null || curApprovalEmail.DocId == 0)
                {
                    lbGMApproval.Text = "Fail to upload aprroval email!";
                    return false;
                }
                lbGMApproval.Text = "Successfully uploaded aprroval email!";
            }

            return true;
        }

        protected void btnHire_Click(object sender, EventArgs e)
        {
            if (!StaticData.FTEDict.ContainsKey(((TextBox)CheckInFormEdit1.FindControl("tbMentor")).Text.ToLower().Trim()))
            {
                ((Label)CheckInFormEdit1.FindControl("lbErrorReportMentor")).Text = "Mentor alias isn't correct, Please input again!";
                JSUtility.Alert(this.Page, "Mentor alias isn't correct, Please input again!");
                return;
            }

            if (IsValid)
            {
                //add appliant to database
                string email = tbEmail.Text.Trim();
                Guid applicantId = SiteUser.GetIdByFullName(email);
                ApplicantBasicInfo abi;
                ApplicantEduBackground aeb;
                ApplicantRelatedInfo ari;
                SiteConfiguration config = SiteConfiguration.GetConfig();
                Document curResume = new Document();
                Document curApprovalEmail = new Document(); // added by Yi Shao at 2009-06-09, used for uploading group manager's approval

                if (applicantId != null && applicantId != Guid.Empty)
                {
                    //applicant exist
                    //update applicant
                    bool isSuccess = uploadResume(applicantId, ref curResume);
                    if (!isSuccess)
                    {
                        JSUtility.Alert(this, "Resume upload error!");
                        return;
                    }

                    /*
                     * abi would be null if file upload process failed in the previous time. This will cause a null exception.
                     */
                    abi = ApplicantBasicInfo.GetApplicantBasicInfoById(applicantId);
                    if (abi != null)
                    {
                        abi.FirstName = tbFirstName.Text.Trim();
                        abi.LastName = tbLastName.Text.Trim();
                        abi.Gender = (GenderEnum)(Int32.Parse(ddlGender.SelectedValue));
                        abi.Email = email;
                        abi.NameInChinese = tbChineseName.Text.Trim();
                        if (ddlGMApproval.SelectedValue == "email")
                            abi.Status = ApplicationStatusEnum.Hired;
                        else
                        {
                            /*
                             * For recruiter to modify incomplete data
                             * Author: Yuanqin
                             * Date: 2011.3.10
                             */
                            if (CheckBox1.Checked == false)
                            {
                                abi.Status = ApplicationStatusEnum.InterviewinProcess;
                            }
                            else
                            {
                                abi.Status = ApplicationStatusEnum.Hired;
                            }
                        }
                        abi.PhoneNumber = tbPhone.Text;
                        //abi.ApplicationTime = DateTime.Now;
                        /*
                         * Add by Yuanqin
                         * 2011.2.21
                         * For Offline
                         */
                        //abi.IsOffline = IsOfflineEnum.Offline;                       
                        abi.Update();

                        aeb = ApplicantEduBackground.GetApplicantEduBackgroundById(applicantId);
                        aeb.MajorCategory = (MajorCategoryEnum)Convert.ToInt32(ddlMajor.SelectedValue);
                        aeb.Major = tbMajor.Text;
                        aeb.Degree = (DegreeEnum)Convert.ToInt32(ddlDegree.SelectedValue);
                        aeb.YearOfStudy = ddlGrade.SelectedIndex + 1;
                        aeb.EnrollDate = Convert.ToDateTime(tbEnrollDate.Text);
                        aeb.GraduateDate = Convert.ToDateTime(tbGraduateDate.Text);
                        aeb.Resume = curResume;
                        String resumeImagePath = SiteConfiguration.GetConfig().SiteAttributes["docPath"] + aeb.Resume.SaveName;
                        Byte[] resumeImage = GlobalHelper.GetFileImage(resumeImagePath);

                        aeb.ResumeImage = resumeImage;
                        if (aeb.Resume.SaveName.IndexOf('.') == -1)
                        {
                            aeb.ResumeExt = ".doc";
                        }
                        else
                        {
                            string[] temp = aeb.Resume.SaveName.Split('.');
                            aeb.ResumeExt = String.Format(".{0}", temp[temp.Length - 1]);
                        }

                        aeb.HighestEduInstitution = cs.CollegeName;
                        aeb.Update();
                    }
                    else
                    {
                        JSUtility.Alert(this, "Error, the file was failed to upload.");
                    }
                }
                else
                {
                    //create applicant
                    Membership.CreateUser(email, GlobalHelper.PasswordGenerator(7, true, true, true, false, false), email);
                    Roles.AddUserToRole(email, RoleType.Applicant.ToString());
                    applicantId = SiteUser.GetIdByFullName(email);
                    //upload resume and approval
                    bool isSuccess = uploadResume(applicantId, ref curResume);
                    if (!isSuccess)
                    {
                        JSUtility.Alert(this, "Resume upload error!");
                        return;
                    }
                    //add information
                    abi = new ApplicantBasicInfo();
                    abi.ApplicantId = applicantId;
                    abi.FirstName = tbFirstName.Text.Trim();
                    abi.LastName = tbLastName.Text.Trim();
                    abi.Gender = (GenderEnum)(Int32.Parse(ddlGender.SelectedValue));
                    abi.Email = email;
                    abi.NameInChinese = tbChineseName.Text.Trim();
                    if (ddlGMApproval.SelectedValue == "email")
                        abi.Status = ApplicationStatusEnum.Hired;
                    else
                    {
                        /*
                         * For recruiter to modify incomplete data
                         * Author: Yuanqin
                         * Date: 2011.3.25
                         */
                        if (CheckBox1.Checked == false)
                        {
                            abi.Status = ApplicationStatusEnum.InterviewinProcess;
                        }
                        else
                        {
                            abi.Status = ApplicationStatusEnum.Hired;
                        }
                    }
                    abi.PhoneNumber = tbPhone.Text;
                    abi.ApplicationTime = DateTime.Now;
                    /*
                     * For recruiter to modify incomplete data
                     * Author: Yuanqin
                     * Date: 2011.3.25
                     */
                    if (CheckBox1.Checked == true)
                    {
                        abi.IsOffline = IsOfflineEnum.Offline;
                    }

                    abi.Insert();

                    aeb = new ApplicantEduBackground();
                    aeb.ApplicantId = applicantId;
                    aeb.MajorCategory = (MajorCategoryEnum)Convert.ToInt32(ddlMajor.SelectedValue);
                    aeb.Major = tbMajor.Text;

                    aeb.Degree = (DegreeEnum)Convert.ToInt32(ddlDegree.SelectedValue);
                    aeb.YearOfStudy = ddlGrade.SelectedIndex + 1;
                    aeb.EnrollDate = Convert.ToDateTime(tbEnrollDate.Text);
                    aeb.GraduateDate = Convert.ToDateTime(tbGraduateDate.Text);
                    aeb.Resume = curResume;

                    String resumeImagePath = SiteConfiguration.GetConfig().SiteAttributes["docPath"] + aeb.Resume.SaveName;
                    Byte[] resumeImage = GlobalHelper.GetFileImage(resumeImagePath);

                    aeb.ResumeImage = resumeImage;
                    if (aeb.Resume.SaveName.IndexOf('.') == -1)
                    {
                        aeb.ResumeExt = ".doc";
                    }
                    else
                    {
                        string[] temp = aeb.Resume.SaveName.Split('.');
                        aeb.ResumeExt = String.Format(".{0}", temp[temp.Length - 1]);
                    }

                    aeb.HighestEduInstitution = cs.CollegeName;
                    aeb.Insert();

                    ari = new ApplicantRelatedInfo();
                    ari.ApplicantId = applicantId;
                    ari.Insert();
                }

                Interview curInterview = new Interview();
                curInterview.ApplicantId = applicantId;
                curInterview.StartDate = DateTime.Now;

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

                bool isApprovalSuccess = uploadApproval(interviewId, applicantId, ref curApprovalEmail);
                if (!isApprovalSuccess)
                {
                    Interview.DeleteInterviewById(interviewId);
                    abi.Status = ApplicationStatusEnum.Available;
                    abi.Update();
                    JSUtility.Alert(this, "Approval email upload error,please try again!");
                    return;
                }

                if (ddlGMApproval.SelectedValue == "email")
                {
                    curInterview.GMApproval = curApprovalEmail;
                    curInterview.InterviewStatus = InterviewStatusEnum.Hired;
                    curInterview.GroupManagerResult = true;
                    curInterview.ManagerDecisionTime = DateTime.Now;
                    curInterview.EndDate = DateTime.Now;
                    if (curInterview.GMApproval.SaveName.IndexOf('.') == -1)
                    {
                        //default is ".msg"
                        curInterview.GMApprovalExt = ".msg";
                    }
                    else
                    {
                        string[] temp = curInterview.GMApproval.SaveName.Split('.');
                        curInterview.GMApprovalExt = String.Format(".{0}", temp[temp.Length - 1]);
                    }
                }
                else
                {
                    /*
                     * For recruiter to modify incomplete data
                     * Author: Yuanqin
                     * Date: 2011.3.25
                     */
                    if (CheckBox1.Checked == false)
                    {
                        curInterview.InterviewStatus = InterviewStatusEnum.WaitingForGroupManagerDecision;
                    }
                    else
                    {
                        curInterview.InterviewStatus = InterviewStatusEnum.Hired;
                        curInterview.GroupManagerResult = true;
                        curInterview.ManagerDecisionTime = DateTime.Now;
                        curInterview.EndDate = DateTime.Now;
                    }
                }
                //curInterview = Interview.GetInterviewById(interviewId);
                curInterview.MentorDecisionTime = DateTime.Now;
                curInterview.Update();

                /*
                 * For recruiter to modify incomplete data
                 * Author: Yuanqin
                 * Date: 2011.3.25
                 */
                if (CheckBox1.Checked == false)
                {
                    MailHelper mailHelper = new MailHelper();
                    mailHelper.AddInterviewVariables(curInterview.InterviewId);
                    if (ddlGMApproval.SelectedValue == "email")
                    {
                        mailHelper.SendMail(MailType.OnBoardReminder);
                    }
                    else
                    {
                        mailHelper.SendMail(MailType.AskForApproval);
                    }
                }
                // //MailHelper.SendAskForApprovalMail(curInterview.InterviewId);
                //////////////////////////////////////////////////////////////////////////////////

                //notification
                btnOfflineHire.Visible = false;
                btnOfflineHire.Enabled = false;
                btnPreviewRequest.Visible = false;
                btnPreviewRequest.Enabled = false;
                btnBack.Visible = false;
                //btnBack.Enabled = false;
                if (ddlGMApproval.SelectedValue == "email")
                    JSUtility.Alert(this, "Candidate has been hired successfully!");
                else
                    JSUtility.Alert(this, "Approval request email has been successfully send to Group Manager!");

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
        protected void ddlGMApproval_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlGMApproval.SelectedValue == "email")
            {
                fuApprovalEmail.Visible = true;
                lbApprovalEmail.Visible = true;
            }
            else
            {
                fuApprovalEmail.Visible = false;
                lbApprovalEmail.Visible = false;
            }
        }

    }
}