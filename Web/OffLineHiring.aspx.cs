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
using Springfield.Components;
using Springfield.Components.Configuration;

public partial class OffLineHiring : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //bind ddlUniversity
        //bind ddlGroupManager
        if (!IsPostBack)
        {
            //string[] colleges = EnumHelper.GetEnumStrings(typeof(CollegeEnum));
            //ddlUniversity.DataSource = colleges;
            //ddlUniversity.DataBind();
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

    private Document UploadFile(string Name,Guid ApplicantId, FileUpload fuControl, string Postfix,DocumentEnum docType)
    {
        Document curDoc = null;
        if (fuControl.HasFile)
        {
            string ext = System.IO.Path.GetExtension(fuControl.FileName).ToLower();
            string newFileName = Name + Postfix + ext;
            fuControl.SaveAs(SiteConfiguration.GetConfig().SiteAttributes["docPath"] + newFileName);
            curDoc = new Document();
            curDoc.SaveName = newFileName;
            curDoc.OriginalName = fuControl.FileName;
            curDoc.DocType = docType;
            curDoc.ApplicantId = ApplicantId;
            curDoc.Insert();
        }
        return curDoc;
    }

    private bool uploadResume(Guid applicantId,ref Document curResume)
    {
        curResume = UploadFile(applicantId.ToString(),applicantId, fuResume, "_cv", DocumentEnum.CV);
        if (curResume == null || curResume.DocId == 0)
        {
            lbResume.Text = "Fail to upload resume!";
            return false;
        }
        lbResume.Text = "Successfully uploaded resume!";
        return true;
    }

    private bool uploadApproval(int InterviewId,Guid applicantId, ref Document curApprovalEmail)
    {
        if (fuApprovalEmail.Visible == true)
        {
            curApprovalEmail = UploadFile(InterviewId.ToString(),applicantId, fuApprovalEmail, "_offlinehire_GMApproval", DocumentEnum.Approval);
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
                bool isSuccess = uploadResume(applicantId,ref curResume);
                if (!isSuccess)
                {
                    JSUtility.Alert(this, "Resume upload error!");
                    return;
                }
                abi = ApplicantBasicInfo.GetApplicantBasicInfoById(applicantId);
                abi.FirstName = tbFirstName.Text.Trim();
                abi.LastName = tbLastName.Text.Trim();
                abi.Gender = (GenderEnum)(Int32.Parse(ddlGender.SelectedValue));
                abi.Email = email;
                abi.NameInChinese = tbChineseName.Text.Trim();
                if (ddlGMApproval.SelectedValue == "email")
                    abi.Status = ApplicationStatusEnum.Hired;
                else
                    abi.Status = ApplicationStatusEnum.InterviewinProcess;
                abi.PhoneNumber = tbPhone.Text;
                abi.ApplicationTime = DateTime.Now;
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
                    //default is ".doc"
                    aeb.ResumeExt = ".doc";
                }
                else
                {
                    string[] temp = aeb.Resume.SaveName.Split('.');
                    aeb.ResumeExt = String.Format(".{0}", temp[temp.Length - 1]);
                }

                //aeb.HighestEduInstitution = (CollegeEnum)ddlUniversity.SelectedIndex;
                aeb.HighestEduInstitution = cs.CollegeName;
                aeb.Update();

            }
            else
            {
                //create applicant
                Membership.CreateUser(email, GlobalHelper.PasswordGenerator(7, true, true, true, false, false), email);
                Roles.AddUserToRole(email, RoleType.Applicant.ToString());
                applicantId = SiteUser.GetIdByFullName(email);
                //upload resume and approval
                bool isSuccess = uploadResume(applicantId,ref curResume);
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
                    abi.Status = ApplicationStatusEnum.InterviewinProcess;
                abi.PhoneNumber = tbPhone.Text;
                abi.ApplicationTime = DateTime.Now;
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
                    //default is ".doc"
                    aeb.ResumeExt = ".doc";
                }
                else
                {
                    string[] temp = aeb.Resume.SaveName.Split('.');
                    aeb.ResumeExt = String.Format(".{0}", temp[temp.Length - 1]);
                }
                //aeb.HighestEduInstitution = (CollegeEnum)ddlUniversity.SelectedIndex;
                aeb.HighestEduInstitution = cs.CollegeName;
                aeb.Insert();

                ari = new ApplicantRelatedInfo();
                ari.ApplicantId = applicantId;                
                ari.Insert();

                //send email to applicant
                //tell him to fill up his application information
                //Email applicantMail = new Email();
            }

            //Interview curInterview = Interview.GetCurrentInterview(applicantId, SiteUser.Current.SiteUserId);
            //if (curInterview == null)
            //{
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
            curInterview.CheckInFormId = form.FormId;
            Int32 interviewId = curInterview.Insert();

            bool isApprovalSuccess = uploadApproval(interviewId,applicantId, ref curApprovalEmail);
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
                curInterview.InterviewStatus = InterviewStatusEnum.WaitingForGroupManagerDecision;
            //curInterview = Interview.GetInterviewById(interviewId);
            curInterview.MentorDecisionTime = DateTime.Now;
            curInterview.Update();


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
            //MailHelper.SendAskForApprovalMail(curInterview.InterviewId);
            //////////////////////////////////////////////////////////////////////////////////

            //notification
            btnHire.Visible = false;
            btnHire.Enabled = false;
            btnPreview.Visible = false;
            btnPreview.Enabled = false;
            btnBack.Visible = false;
            btnBack.Enabled = false;
            if (ddlGMApproval.SelectedValue == "email")
                JSUtility.Alert(this, "Candidate has been hired successfully!");
            else
                JSUtility.Alert(this, "Approval request email has been successfully send to Group Manager!");
            //JSUtility.CloseWindow(this);
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
        CheckInFormView1.SetCheckInForm( CheckInFormEdit1.GetCheckInForm() );
        CheckInFormEdit1.Visible = false;
        CheckInFormView1.Visible = true;
        btnHire.Visible = true;
        btnPreview.Visible = false;
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
        btnHire.Visible = true;
        btnPreview.Visible = true;
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
