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
using MSRA.SpringField.Components.BizObjects;
using MSRA.SpringField.Application.Config.Schemas;
using System.Linq;

namespace MSRA.SpringField.Application.Controls
{
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
            BindUserControlUI();
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

        protected void dlInterviewHistory_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView dr = e.Item.DataItem as DataRowView;
                HtmlTableRow row = e.Item.FindControl("trUpdateFeedback") as HtmlTableRow;
                HtmlGenericControl label = e.Item.FindControl("lblUpdate") as HtmlGenericControl;
                ImageButton ib = e.Item.FindControl("ibUpdate") as ImageButton;

                if (ib != null && label != null)
                {
                    label.Attributes.Add("for", ib.ClientID);
                }

                SpringFieldDataContext ctx = new SpringFieldDataContext();
                int interviewId = dr.Row.Field<int>("InterviewId");
                sf_Interview interview = ctx.sf_Interviews.FirstOrDefault<sf_Interview>(p => p.InterviewId == interviewId);

                if (dr != null && row != null && interview != null)
                {
                    //The feedback content could only be changed by whom submitted it, and while the interview is processing.
                    row.Visible = dr["InterviewerAlias"].ToString().Equals(SiteUser.Current.Alias) && interview.InterviewStatus <= 2;
                }
            }
        }

        protected void dlInterviewHistory_ItemCommand(object source, DataListCommandEventArgs e)
        {
            SpringFieldDataContext ctx = new SpringFieldDataContext();            
            switch (e.CommandName)
            {
                case "UpdateFeedback":
                    {
                        sf_Feedback currFb = ctx.sf_Feedbacks.FirstOrDefault<sf_Feedback>(p => p.FeedbackId.Equals(e.CommandArgument));

                        if (currFb != null)
                        {
                            TextBox content = e.Item.FindControl("tbFeedbackContent") as TextBox;
                            if (content != null)
                            {
                                currFb.FeedbackContent = content.Text;
                            }
                        }
                        ctx.SubmitChanges();

                        BindUserControlUI();
                        break;
                    }
                default: break;
            }
        }

        private void BindUserControlUI()
        {
            DataSet dsInterview = Feedback.GetFeedbackByInterview(InterviewID);
            if (dsInterview == null || dsInterview.Tables[0].Rows.Count == 0)
            {
                this.Visible = false;
                //return;
            }
            else
            {
                dlInterviewHistory.DataSource = dsInterview;
                dlInterviewHistory.DataBind();
            }
        }
    }
}