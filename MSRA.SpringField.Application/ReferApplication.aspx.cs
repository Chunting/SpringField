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
using MSRA.SpringField.Components.BizObjects;
using MSRA.SpringField.Components.Enumerations;
using MSRA.SpringField.Components.Configuration;
using MSRA.SpringField.Components;

namespace MSRA.SpringField.Application
{
    public partial class ReferApplication : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!SiteUser.Current.IsInRole(RoleType.InternRecruiter))
            {
                Server.Transfer("~/AccessDeny.htm", true);
            }

            if (!IsPostBack)
            {
                //string[] colleges = EnumHelper.GetEnumStrings(typeof(CollegeEnum));
                //ddlUniversity.DataSource = colleges;
                //ddlUniversity.DataBind();
            }

            btnEnrollDate.Attributes.Add("onclick", "popUpCalendar(this,document.all." + tbEnrollDate.ClientID + ",'mm/dd/yyyy');");
            btnGraduateDate.Attributes.Add("onclick", "popUpCalendar(this,document.all." + tbGraduateDate.ClientID + ",'mm/dd/yyyy');");
            tbEnrollDate.Attributes.Add("onclick", "popUpCalendar(this,document.all." + tbEnrollDate.ClientID + ",'mm/dd/yyyy');");
            tbGraduateDate.Attributes.Add("onclick", "popUpCalendar(this,document.all." + tbGraduateDate.ClientID + ",'mm/dd/yyyy');");
            tbEnrollDate.Attributes.Add("readonly", "true");
            tbGraduateDate.Attributes.Add("readonly", "true");
        }

        private Document UploadFile(Guid id)
        {
            Document curDoc = null;
            if (fuResume.HasFile)
            {
                string ext = System.IO.Path.GetExtension(fuResume.FileName).ToLower();
                if (ext == ".pdf" || ext == ".doc" || ext == ".docx")
                {
                    string newFileName = id.ToString() + "_cv" + ext;
                    fuResume.SaveAs(SiteConfiguration.GetConfig().SiteAttributes["docPath"] + newFileName);
                    curDoc = new Document();
                    curDoc.SaveName = newFileName;
                    curDoc.OriginalName = fuResume.FileName;
                    curDoc.DocType = DocumentEnum.CV;
                    curDoc.ApplicantId = id;
                    curDoc.Insert();
                }
                else
                {
                    return null;
                }
            }
            return curDoc;
        }

        private RelatedReferrer GenerateRelater()
        {
            RelatedReferrer relater = new RelatedReferrer();
            relater.Email = tbReferrerEmail.Text;
            relater.FirstName = tbReferrerFirstName.Text;
            relater.LastName = tbReferrerLastName.Text;
            return relater;
        }

        private Referral AddKeyReferral(Guid id)
        {
            Referral curReferral = new Referral();
            curReferral.ApplicantId = id;
            curReferral.ReferredTime = DateTime.Now;
            curReferral.ReferrerId = SiteUser.Current.SiteUserId;
            curReferral.Type = ReferralType.KeyReferral;
            curReferral.Relaters.Add(GenerateRelater());
            curReferral.Insert();
            return curReferral;
        }

        protected void btnRefer_Click(object sender, EventArgs e)
        {
            //add appliant to database
            string email = tbEmail.Text.Trim();
            Guid applicantId;
            applicantId = SiteUser.GetIdByFullName(email);
            ApplicantBasicInfo abi;
            ApplicantEduBackground aeb;
            ApplicantRelatedInfo ari;
            SiteConfiguration config = SiteConfiguration.GetConfig();
            Document curResume;
            Referral referral;

            if (applicantId != null && applicantId != Guid.Empty)
            {
                //applicant exist
                //update applicant
                curResume = UploadFile(applicantId);
                if (curResume == null)
                {
                    JSUtility.Alert(this, "File upload error!");
                    return;
                }

                abi = ApplicantBasicInfo.GetApplicantBasicInfoById(applicantId);
                abi.FirstName = tbFirstName.Text.Trim();
                abi.LastName = tbLastName.Text.Trim();
                abi.Email = email;
                abi.NameInChinese = tbChineseName.Text.Trim();
                abi.Status = ApplicationStatusEnum.KeyReferring;
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
                //aeb.HighestEduInstitution = (CollegeEnum)ddlUniversity.SelectedIndex;
                aeb.HighestEduInstitution = cs.CollegeName;
                aeb.Update();

                //add key referral
                referral = AddKeyReferral(applicantId);
                abi.ReferralId = referral.ReferralId;
                abi.Update();
            }
            else
            {
                //create applicant
                Membership.CreateUser(email, GlobalHelper.PasswordGenerator(7, true, true, true, false, false), email);
                Roles.AddUserToRole(email, RoleType.Applicant.ToString());
                applicantId = SiteUser.GetIdByFullName(email);

                //applicant does not exist
                curResume = UploadFile(applicantId);
                if (curResume == null)
                {
                    Roles.RemoveUserFromRole(email, RoleType.Applicant.ToString());
                    Membership.DeleteUser(email);
                    JSUtility.Alert(this, "File upload error!");
                    return;
                }

                //add information
                abi = new ApplicantBasicInfo();
                abi.ApplicantId = applicantId;
                abi.FirstName = tbFirstName.Text.Trim();
                abi.LastName = tbLastName.Text.Trim();
                abi.Email = email;
                abi.NameInChinese = tbChineseName.Text.Trim();
                abi.Status = ApplicationStatusEnum.KeyReferring;
                abi.PhoneNumber = tbPhone.Text;
                //fill the blank to the property that not in the page.
                //abi.CurrentCountry = "China";
                abi.Insert();

                aeb = new ApplicantEduBackground();
                aeb.ApplicantId = applicantId;
                aeb.MajorCategory = (MajorCategoryEnum)Convert.ToInt32(ddlMajor.SelectedValue);
                aeb.Major = tbMajor.Text;
                aeb.Degree = (DegreeEnum)Convert.ToInt32(ddlDegree.SelectedValue);
                aeb.YearOfStudy = ddlGrade.SelectedIndex + 1;
                aeb.Resume = curResume;
                //aeb.ResumeImage = GlobalHelper.GetFileImage(curResume.SaveName);
                //aeb.HighestEduInstitution = (CollegeEnum)ddlUniversity.SelectedIndex;
                aeb.HighestEduInstitution = cs.CollegeName;
                aeb.Insert();

                ari = new ApplicantRelatedInfo();
                ari.ApplicantId = applicantId;
                ari.Insert();

                //add key referral
                referral = AddKeyReferral(applicantId);
                abi.ReferralId = referral.ReferralId;
                abi.Update();
                MailHelper mailHelper = new MailHelper();
                mailHelper.AddApplicantEmailVariables(email);
                mailHelper.SendMail(MailType.RequestRegister);
                //MailHelper.SendRequestRegisterMail(email,applicantId);
                ///////////////////////////////////////////////////////////////////////////////////
            }

            //send mail to [Refer to] people
            //Send email to the employee whom is refer to
            List<string> aliasArr = GlobalHelper.FormatAlias(tbAccepters.Text);

            MailHelper mailHelperRefer = new MailHelper();
            mailHelperRefer.AddApplicantVariables(applicantId);
            foreach (string alias in aliasArr)
            {
                mailHelperRefer.AddReferToAliasVariables(alias);
                mailHelperRefer.SendMail(MailType.ReferTo);
                //MailHelper.SendReferToMail(alias,applicantId);
            }

            //notification
            btnRefer.Visible = false;
            btnRefer.Enabled = false;
            JSUtility.Alert(this, "You have successfully refer this applicant!");
            //JSUtility.CloseWindow(this);
        }
    }
}