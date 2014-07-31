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
using System.Text;
using MSRA.SpringField.Components.Configuration;

namespace MSRA.SpringField.Application
{
    public partial class Error : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpContext ctx = HttpContext.Current;
            Exception ex = ctx.Server.GetLastError();
            if (ex != null)
            {
                lbErrorMsg.Text = ex.Message;
            }
            else
            {
                lbErrorMsg.Text = "Unable to complete your action! Please report this the situation to site administrator, Thanks!";
            }
            ctx.Server.ClearError();
        }

        protected void lnkbtnHome_Click(object sender, EventArgs e)
        {
            SiteConfiguration config = SiteConfiguration.GetConfig();
            string homeURL = config.SiteUrl + "default.aspx";

            StringBuilder sb = new StringBuilder();
            sb.Append("<script language=\"JavaScript\">");
            sb.Append("\n<!--");
            sb.Append("\nwindow.open('");
            sb.Append(homeURL);
            sb.Append("');");
            sb.Append("\nwindow.opener =null;window.open('','_self','');window.close();");
            sb.Append("\n//-->");
            sb.Append("\n</script>");

            Response.Write(sb.ToString());
        }
    }
}