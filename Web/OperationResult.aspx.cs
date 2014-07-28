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

public partial class OperationResult : System.Web.UI.Page
{
    //public String SourceUrl
    //{
    //    set
    //    {
    //        ViewState["sourceurl"] = value;
    //    }
    //    get
    //    {
    //        if (ViewState["sourceurl"] == null)
    //        {
    //            return "#";
    //        }
    //        else
    //        {
    //            return ViewState["sourceurl"].ToString();
    //        }
    //    }
    //}
    protected void Page_Load(object sender, EventArgs e)
    {
        String strInfo = String.Format("Operation Successfully!Click <a href=\"{0}\">here</a> to see the update.", Request.Url);
        litMessage.Text = strInfo;
    }
}
