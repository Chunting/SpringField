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
using Springfield.Components.Configuration;
using Springfield.Components;

public partial class ApplicantRegister : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    //protected void StepNextButton_Click(object sender, EventArgs e)
    //{
    //    applicant = new Applicant();
    //    if (Membership.GetUser(this.TextBoxEmail.Text) != null)
    //    {
    //        this.LabelError.Visible = true;
    //        return;
    //    }

    //    // Comments by Xuejin 
    //    // Why the third parameter is TexBoxEmail rather not ConfirmPassword?
    //    Membership.CreateUser(this.TextBoxEmail.Text, this.ConfirmPassword.Text, this.TextBoxEmail.Text);
    //    Roles.AddUserToRole(this.TextBoxEmail.Text, "Applicant");
    //    applicant.ApplicantId = SiteUser.GetIdByFullName(this.TextBoxEmail.Text);
    //    Session["Applicant"] = applicant;
        
    //    // Send Mail To Applicant, Modified by Xuejin
    //    Email email = new Email();
    //    SiteConfiguration config = SiteConfiguration.GetConfig();

    //    email.From = config.SiteAttributes["systemSender"];
    //    email.To = this.TextBoxEmail.Text;
    //    email.Subject = @"Request Register for MIATS";

    //    string emailTemplate = EmailTemplateFactory.GetTemplate(MailType.RequestRegister);
    //    emailTemplate = emailTemplate.Replace("//Applicant Name//", this.TextBoxEmail.Text);
        
    //    emailTemplate = emailTemplate.Replace("//RegisterLink//", 
    //                    SiteConfiguration.GetConfig().SiteUrl + "Default.aspx" );
    //    emailTemplate = emailTemplate.Replace("//Date//", DateTime.Now.ToShortDateString() );
    //    emailTemplate = emailTemplate.Replace("//email//", this.TextBoxEmail.Text );
    //    emailTemplate = emailTemplate.Replace("//password//", this.ConfirmPassword.Text);
        

    //    email.Body = emailTemplate;
    //    email.Insert();
    //    Server.Transfer("RegisterComplete.aspx");
    //}

    protected void btnRegister_Click(object sender, EventArgs e)
    {
        if(Membership.GetUser(tbEmail.Text.Trim()) != null)
        {
            lbMsg.Text = "Email identity has existed!";
            return;
        }

        try
        {
            Membership.CreateUser(tbEmail.Text.Trim(), tbPassword.Text.Trim(), tbEmail.Text.Trim());
            Roles.AddUserToRole(tbEmail.Text.Trim(), RoleType.Applicant.ToString());
        }
        catch(Exception ex)
        {
            JSUtility.Alert(this, ex.Message);
            return;
        }
        lbMsg.Text = "Create account successfully! Please return to main page!";
        btnRegister.Visible = false;
        btnRegister.Enabled = false;
    }
}
