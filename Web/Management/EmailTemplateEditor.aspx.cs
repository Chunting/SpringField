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
using System.Text.RegularExpressions;
using Springfield.Components;

public partial class Management_EmailTemplateEditor : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        ArrayList templateList = new ArrayList();
        
        templateList.Add(new ListItem("Hire Applicant", EnumHelper.EnumToString(MailType.HireApplicant)));
        templateList.Add(new ListItem("Reject Applicant", EnumHelper.EnumToString(MailType.RejectApplicant)));
        templateList.Add(new ListItem("Ask For Approval", EnumHelper.EnumToString(MailType.AskForApproval)));
        templateList.Add(new ListItem("Arrange Interview", EnumHelper.EnumToString(MailType.ArrangeInterview)));
        templateList.Add(new ListItem("Interviewer Change", EnumHelper.EnumToString(MailType.InterviewerChange)));
        templateList.Add(new ListItem("Reject Mail To Hiring Manager", EnumHelper.EnumToString(MailType.RejectMailToHM)));
        templateList.Add(new ListItem("Hire Referral", EnumHelper.EnumToString(MailType.HireReferral)));
        templateList.Add(new ListItem("Feedback Complete", EnumHelper.EnumToString(MailType.FeedbackComplete)));
        templateList.Add(new ListItem("Request Register", EnumHelper.EnumToString(MailType.RequestRegister)));
        templateList.Add(new ListItem("Refer To", EnumHelper.EnumToString(MailType.ReferTo)));
        templateList.Add(new ListItem("On Board Reminder", EnumHelper.EnumToString(MailType.OnBoardReminder)));
        templateList.Add(new ListItem("Interviewer Reminder", EnumHelper.EnumToString(MailType.Interviewreminder)));
        templateList.Add(new ListItem("PA Reminder", EnumHelper.EnumToString(MailType.PAReminder)));
        templateList.Add(new ListItem("PA Notice", EnumHelper.EnumToString(MailType.PANotice)));
        templateList.Add(new ListItem("Daily PA Report", EnumHelper.EnumToString(MailType.DailyPAReport)));

        foreach (ListItem item in templateList)
        {
            ddlMailTemplate.Items.Add(item);
        }

        tbSubject.Attributes["onchange"] = "SetChanged()";
        tbBody.Attributes["onchange"] = "SetChanged()";
        btnSave.Attributes["onclick"] = "SetSaving()";
        tbFrom.Attributes["onchange"] = "SetChanged()";
        tbTo.Attributes["onchange"] = "SetChanged()";
        tbCc.Attributes["onchange"] = "SetChanged()";
        tbBcc.Attributes["onchange"] = "SetChanged()";
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!SiteUser.Current.IsInRole(RoleType.InternRecruiter))
        {
            Server.Transfer("~/AccessDeny.htm", true);
        }
        if (!Page.IsPostBack)
        {
            ddlMailTemplate.SelectedValue = EnumHelper.EnumToString(MailType.HireApplicant);
            LoadTemplate(MailType.HireApplicant);
            LoadPossibleVariables(MailType.HireApplicant);
        }
    }
    protected void LoadTemplate( MailType mailType)
    {
        EmailTemplate emailTemplate = EmailTemplate.GetEmailTemplateByType(mailType);
        tbFrom.Text = emailTemplate.From;
        tbTo.Text = emailTemplate.To;
        tbCc.Text = emailTemplate.CC;
        tbBcc.Text = emailTemplate.BCC;
        tbBody.Text = emailTemplate.Body;
        tbSubject.Text = emailTemplate.Subject;
        if (emailTemplate.EmailType == MailType.Interviewreminder)
        {
            tbFrom.Enabled = false;
            tbTo.Enabled = false;
            tbCc.Enabled = false;
            tbBcc.Enabled = false;
        }
        else
        {
            tbFrom.Enabled = true;
            tbTo.Enabled = true;
            tbCc.Enabled = true;
            tbBcc.Enabled = true;
        }
    }
    
    protected void LoadPossibleVariables(MailType mailType)
    {
        string strVars="";
        bool bFirst = true;
        ArrayList list = MailHelper.FetchAvailableVariableName(mailType);

        foreach ( string strVar in list)
        {
            if (!bFirst) strVars += ", ";
            strVars += strVar;
            bFirst = false;
        }

        lblSubjectVar.Text = strVars;
    }

    protected void ddlMailTemplate_SelectedIndexChanged(object sender, EventArgs e)
    {
        MailType mailType = (MailType)EnumHelper.StringToEnum(typeof(MailType), ddlMailTemplate.SelectedValue);
        LoadTemplate( mailType);
        LoadPossibleVariables(mailType);
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        MailType mailType = (MailType)EnumHelper.StringToEnum(typeof(MailType), ddlMailTemplate.SelectedValue);
        EmailTemplate mailTemplate = new EmailTemplate();
        mailTemplate.EmailType = mailType;
        mailTemplate.Subject = tbSubject.Text;
        mailTemplate.Body = tbBody.Text;
        mailTemplate.From = tbFrom.Text;
        mailTemplate.To = tbTo.Text;
        mailTemplate.CC = tbCc.Text;
        mailTemplate.BCC = tbBcc.Text;
        
        //Here fix for case
        ArrayList listVars = MailHelper.FetchAvailableVariableName(mailType);

        foreach (string strVar in listVars)
        {
            mailTemplate.From = Regex.Replace(mailTemplate.From, "//" + strVar + "//", "//" + strVar + "//", RegexOptions.IgnoreCase);
            mailTemplate.To = Regex.Replace(mailTemplate.To, "//" + strVar + "//", "//" + strVar + "//", RegexOptions.IgnoreCase);
            mailTemplate.CC = Regex.Replace(mailTemplate.CC, "//" + strVar + "//", "//" + strVar + "//", RegexOptions.IgnoreCase);
            mailTemplate.BCC = Regex.Replace(mailTemplate.BCC, "//" + strVar + "//", "//" + strVar + "//", RegexOptions.IgnoreCase);
            mailTemplate.Body = Regex.Replace(mailTemplate.Body, "//" + strVar + "//", "//" + strVar + "//", RegexOptions.IgnoreCase);  
        }

        if (mailType == MailType.Interviewreminder)
        {
            listVars.Add("SystemSender");
            listVars.Add("Applicant Email");
            listVars.Add("Interviewer Alias");
            listVars.Add("Hiring Manager Alias");
        }

        CheckVariables(mailTemplate.From, listVars, "From");
        CheckVariables(mailTemplate.To, listVars, "To");
        CheckVariables(mailTemplate.CC, listVars, "Cc");
        CheckVariables(mailTemplate.BCC, listVars, "Bcc");
        if (mailType == MailType.Interviewreminder)
        {
            listVars.Remove("SystemSender");
            listVars.Remove("Applicant Email");
            listVars.Remove("Interviewer Alias");
            listVars.Remove("Hiring Manager Alias");
        }

        CheckVariables(mailTemplate.Subject, listVars, "Subject");
        CheckVariables(mailTemplate.Body, listVars, "Body");

       
        //Here fix for tiny_mce's format and change of href
        mailTemplate.Body = "<html><body>" + mailTemplate.Body + "</body></html>";
        ArrayList listLinks = MailHelper.FetchLinkVariableName();
        
        foreach (string strVar in listLinks)
        {
            mailTemplate.Body = mailTemplate.Body.Replace("http://" + strVar + "//", "//" + strVar + "//");
        }

        tbSubject.Text = mailTemplate.Subject ;
        tbBody.Text = mailTemplate.Body;
        tbFrom.Text = mailTemplate.From;
        tbTo.Text = mailTemplate.To;
        tbCc.Text = mailTemplate.CC;
        tbBcc.Text = mailTemplate.BCC;
        mailTemplate.Update();
    }

    private static void CheckVariables(string strToCheck, ArrayList listVars, string fieldName)
    {
        //Here check for not available variables
        string strNotMatch = "";
        MatchCollection matchCollection = Regex.Matches(strToCheck, "//([^<>\"/]*?)//");
        foreach (Match match in matchCollection)
        {
            if (!listVars.Contains(match.Groups[1].ToString()))
            {
                strNotMatch += "//" + match.Groups[1] + "// ";
            }
        }
        if (strNotMatch != "")
        {
            throw new Exception("Cannot replace "+ strNotMatch + " in \"" + fieldName + "\" field!");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        MailType mailType = (MailType)EnumHelper.StringToEnum(typeof(MailType), ddlMailTemplate.SelectedValue);
        LoadTemplate(mailType);
    }
}
