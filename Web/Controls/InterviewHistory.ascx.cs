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

public partial class Controls_InterviewHistory : System.Web.UI.UserControl
{
    public Int32 InterviewID
    {
        get
        {
            if (ViewState["interviewid"] == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(ViewState["interviewid"]);
            }
        }

        set
        {
            ViewState["interviewid"] = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!this.IsPostBack)
        //{
            DataSet dsInterview = Feedback.GetFeedbackByInterview(InterviewID);
            if (dsInterview == null || dsInterview.Tables[0].Rows.Count == 0)
            {
                this.Visible = false;
                return;
            }
            dlInterviewHistory.DataSource = dsInterview;
            dlInterviewHistory.DataBind();
        //}
    }

    protected string ParseFeedback(object dataItem)
    {
        DataRowView curData = dataItem as DataRowView;
        return GlobalHelper.FormatOutput(Convert.ToString(curData["FeedbackContent"]));
    }
    protected string ParseSuggestion(object dataItem)
    {
        DataRowView dr = dataItem as DataRowView;
        if (Convert.ToBoolean(dr["SuggestionResult"]))
        {
            return "Hire";
        }
        else
        {
            return "Reject";
        }
    }
}
