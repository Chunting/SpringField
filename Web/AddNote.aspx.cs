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

public partial class AddNote : System.Web.UI.Page
{
    private Guid applicantId = Guid.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request["applicant"] == null)
        {
            JSUtility.Alert(this, "Invalid Parameter!");
            JSUtility.CloseWindow(this);
            return;
        }
        else
        {
            try
            {
                applicantId = new Guid(Convert.ToString(Request["applicant"]));
            }
            catch
            {
                JSUtility.Alert(this, "Invalid parameter!");
                JSUtility.CloseWindow(this);
                return;
            }
        }

        commentList.ApplicantId = applicantId;

        if (!IsPostBack)
        {
            commentList.BindData();
        }
    }
}
