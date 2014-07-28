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
using MSRA.SpringField.Application.Controls;
using MSRA.SpringField.Components;

/*
 * Checkout Survey Report
 * Add by Yuanqin, 2011.6.7
 */

namespace MSRA.SpringField.Application
{
    public partial class SurveyReport1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                tbStartDate.Attributes.Add("onclick", "popUpCalendar(this,document.all." + tbStartDate.ClientID + ",'yyyy-mm-dd');");
                tbEndDate.Attributes.Add("onclick", "popUpCalendar(this,document.all." + tbEndDate.ClientID + ",'yyyy-mm-dd');");
                tbStartDate.Attributes.Add("readonly", "true");
                tbEndDate.Attributes.Add("readonly", "true");
                InitDate();

                //Bind group list
                ddlGroup.DataSource = CheckInFormResourceManager.GetTypeDataSet("Groups").Tables[0];
                ddlGroup.DataValueField = "id";
                ddlGroup.DataTextField = "name";
                ddlGroup.DataBind();
                ListItem allGroup = new ListItem("All", "0");
                ddlGroup.Items.Insert(0, allGroup);
                allGroup.Selected = true;
            }

            BindReport();
        }

        private void BindReport()
        {
            Control reportCon;

            reportCon = LoadControl(@"~/Controls/SurveyOverallView.ascx");
            ((SurveyOverallView)reportCon).m_StartDate = Convert.ToDateTime(tbStartDate.Text.Trim()).AddSeconds(-1);
            ((SurveyOverallView)reportCon).m_EndDate = Convert.ToDateTime(tbEndDate.Text.Trim()).AddDays(1);
            ((SurveyOverallView)reportCon).GroupId = Convert.ToInt32(ddlGroup.SelectedValue.Trim());        //Group
            ((SurveyOverallView)reportCon).Duration = Convert.ToInt32(ddlTimeSpan.SelectedValue.Trim());    //InternshipDuration
            
            //switch (ddlReportType.SelectedValue)
            //{
            //    case "OverAllView":
            //        reportCon = LoadControl(@"~/Controls/SurveyOverallView.ascx");
            //        ((SurveyOverallView)reportCon).m_StartDate = Convert.ToDateTime(tbStartDate.Text.Trim()).AddSeconds(-1);
            //        ((SurveyOverallView)reportCon).m_EndDate = Convert.ToDateTime(tbEndDate.Text.Trim()).AddDays(1);
            //        ((SurveyOverallView)reportCon).GroupId = Convert.ToInt32(ddlGroup.SelectedValue.Trim());        //Group
            //        ((SurveyOverallView)reportCon).Duration = Convert.ToInt32(ddlTimeSpan.SelectedValue.Trim());    //InternshipDuration
            //        break;
            //    case "Group":
            //        reportCon = LoadControl(@"~/Controls/SurveyGroup.ascx");
            //        //((SurveyGroup)reportCon).m_StartDate = Convert.ToDateTime(tbStartDate.Text.Trim()).AddSeconds(-1);
            //        //((SurveyGroup)reportCon).m_EndDate = Convert.ToDateTime(tbEndDate.Text.Trim()).AddDays(1);
            //        break;
            //    case "Duration":
            //        reportCon = LoadControl(@"~/Controls/SurveyDuration.ascx");
            //        //((SurveyDuration)reportCon).m_StartDate = Convert.ToDateTime(tbStartDate.Text.Trim()).AddSeconds(-1);
            //        //((SurveyDuration)reportCon).m_EndDate = Convert.ToDateTime(tbEndDate.Text.Trim()).AddDays(1);
            //        break;
            //    default:
            //        reportCon = LoadControl(@"~/Controls/SurveyOverallView.ascx");
            //        ((SurveyOverallView)reportCon).m_StartDate = Convert.ToDateTime(tbStartDate.Text.Trim()).AddSeconds(-1);
            //        ((SurveyOverallView)reportCon).m_EndDate = Convert.ToDateTime(tbEndDate.Text.Trim()).AddDays(1);
            //        ((SurveyOverallView)reportCon).GroupId = Convert.ToInt32(ddlGroup.SelectedValue.Trim());        //Group
            //        ((SurveyOverallView)reportCon).Duration = Convert.ToInt32(ddlTimeSpan.SelectedValue.Trim());    //InternshipDuration
            //        break;
            //}
            phReport.Controls.Add(reportCon);
            //--------------------------bin2011-9-7------------------------------
            lnkSurveyComments.NavigateUrl = "~/Modules/Reports/AllCommentsReport.aspx?type=Detail&StartDate=" + tbStartDate.Text.Trim() + "&EndDate=" + tbEndDate.Text.Trim()
                + "&GroupId=" + ddlGroup.SelectedValue.Trim() + "&Duration=" + ddlTimeSpan.SelectedValue.Trim();
             
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {

        }
        private void InitDate()
        {
            if (Request["StartDate"] != null && Request["EndDate"] != null)
            {
                try
                {
                    tbStartDate.Text = Convert.ToDateTime(Request["StartDate"]).ToString("yyyy-MM-dd");
                    tbEndDate.Text = Convert.ToDateTime(Request["EndDate"]).ToString("yyyy-MM-dd");
                }
                catch
                {
                    tbStartDate.Text = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
                    tbEndDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                }
            }
            else
            {
                tbStartDate.Text = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
                tbEndDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
            lbDateSpan.Text = "Date From " + tbStartDate.Text + " To " + tbEndDate.Text;
        }

        protected void Export_Click(object sender, EventArgs e)
        {
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename=Sourcing Report - by Channel and Sourcing" + DateTime.Now.ToString() + ".xls");
            Response.ContentType = "application/excel";
            Response.Charset = "utf-8";
            Response.Write("<meta http-equiv=Content-Type content=text/html;charset=utf-8>");
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            System.IO.StringWriter sw = new System.IO.StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            phReport.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
        }
    }
}
