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

namespace MSRA.SpringField.Application
{
    public partial class URReferral : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!SiteUser.Current.IsInRole(RoleType.UnivRelation))
            {
                Server.Transfer("~/AccessDeny.html", true);
            }
        }

        protected void btnRefer_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbReferree.Text))
            {
                JSUtility.Alert(this, "Referree email address is empty!");
            }
            else
            {
                string referreeEmails = GlobalHelper.ClearInput(tbReferree.Text, 2048, false).Replace(" ", string.Empty);
                string[] referreeArr = referreeEmails.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (string referree in referreeArr)
                {
                    //Add Referral
                    //Create User and generate a password for him
                    //Send email to referree
                    //The email should contain a link with the referral id
                    string applicantEmail = referree.Trim();
                    if (!GlobalHelper.ValidateEmail(applicantEmail))
                    {
                        JSUtility.Alert(this, "Illegal email address detected!");
                        return;
                    }

                    Guid referreeId = SiteUser.GetIdByFullName(applicantEmail);
                    if (referreeId == null || referreeId == Guid.Empty)
                    {
                        //Generat password
                        Membership.CreateUser(applicantEmail, GlobalHelper.PasswordGenerator(7, true, true, true, false, false), applicantEmail);
                        Roles.AddUserToRole(applicantEmail, RoleType.Applicant.ToString());
                        referreeId = SiteUser.GetIdByFullName(applicantEmail);
                    }

                    Referral curReferral = new Referral();
                    curReferral.ReferrerId = SiteUser.Current.SiteUserId;
                    curReferral.ReferredTime = DateTime.Now;
                    curReferral.ApplicantId = referreeId;
                    curReferral.Type = ReferralType.URReferral;
                    curReferral.Insert();

                    MailHelper mailHelper = new MailHelper();
                    mailHelper.AddApplicantEmailVariables(applicantEmail);
                    mailHelper.SendMail(MailType.RequestRegister);
                    //MailHelper.SendRequestRegisterMail( applicantEmail, curReferral.ApplicantId ); 
                }

                btnRefer.Visible = false;
                btnRefer.Enabled = false;
                JSUtility.Alert(this, "Email has been sent to these referrees.");
                //JSUtility.CloseWindow(this);
            }
        }
    }
}