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

namespace MSRA.SpringField.Application
{
    public partial class SendEmailRegular : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            MailHelper mailHelper = new MailHelper();
            mailHelper.AddDailyPAReportVariables(DateTime.Now.AddDays(-1), DateTime.Now);
            mailHelper.SendMail(MailType.DailyPAReport);
        }
    }
}