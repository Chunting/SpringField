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
using MSRA.SpringField.Components;
using MSRA.SpringField.Components.Enumerations;

namespace MSRA.SpringField.Application.Keyin
{
    public partial class ApplicantRegister : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            if (Membership.GetUser(tbEmail.Text.Trim()) != null)
            {
                lbMsg.Text = "Email identity has existed!";
                return;
            }

            try
            {
                Membership.CreateUser(tbEmail.Text.Trim(), tbPassword.Text.Trim(), tbEmail.Text.Trim());
                Roles.AddUserToRole(tbEmail.Text.Trim(), RoleType.Applicant.ToString());
            }
            catch (Exception ex)
            {
                JSUtility.Alert(this, ex.Message);
                return;
            }
            lbMsg.Text = "Create account successfully! Please return to main page!";
            btnRegister.Visible = false;
            btnRegister.Enabled = false;
        }
    }
}