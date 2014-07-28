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
}
