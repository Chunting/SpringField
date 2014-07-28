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

public partial class FeedbackList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData();
        }
    }

    private void BindData()
    {
        DataSet dsFeedback = Feedback.GetIncompleteFeedbackByAlias(SiteUser.Current.FullName);
        gvFeedbackList.DataSource = dsFeedback;
        gvFeedbackList.DataBind();
    }

    protected string ParseName(object dataItem)
    {
        DataRowView dr = dataItem as DataRowView;
        string firstName = dr["FirstName"].ToString();
        string lastName = dr["LastName"].ToString().ToUpper();

        return (firstName + " " + lastName);
    }

    protected string ParseDate(object dataItem)
    {
        DataRowView dr = dataItem as DataRowView;
        return Convert.ToDateTime(dr["DueDate"]).ToShortDateString();
    }

    protected void gvFeedbackList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvFeedbackList.PageIndex = e.NewPageIndex;
        BindData();
    }
}
