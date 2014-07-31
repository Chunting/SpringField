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

namespace MSRA.SpringField.Application
{
    public partial class SourcingReport_New : System.Web.UI.Page
    {
        //private DateTime m_StartDate, m_EndDate;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                tbStartDate.Attributes.Add("onclick", "popUpCalendar(this,document.all." + tbStartDate.ClientID + ",'yyyy-mm-dd');");
                tbEndDate.Attributes.Add("onclick", "popUpCalendar(this,document.all." + tbEndDate.ClientID + ",'yyyy-mm-dd');");
                tbStartDate.Attributes.Add("readonly", "true");
                tbEndDate.Attributes.Add("readonly", "true");
                InitDate();
            }

            BindReport();
        }

        private void BindReport()
        {
            Control reportCon;
            switch (ddlReportType.SelectedValue)
            {
                case "ChannelSourcing":
                    reportCon = LoadControl(@"~/Controls/SRChannelSource.ascx");
                    ((Controls_SRChannelSource)reportCon).m_StartDate = Convert.ToDateTime(tbStartDate.Text.Trim()).AddSeconds(-1);
                    ((Controls_SRChannelSource)reportCon).m_EndDate = Convert.ToDateTime(tbEndDate.Text.Trim()).AddDays(1);
                    break;
                case "PreferredPosition":
                    reportCon = LoadControl(@"~/Controls/SRPreferredPosition.ascx");
                    ((Controls_SRPreferredPosition)reportCon).m_StartDate = Convert.ToDateTime(tbStartDate.Text.Trim()).AddSeconds(-1);
                    ((Controls_SRPreferredPosition)reportCon).m_EndDate = Convert.ToDateTime(tbEndDate.Text.Trim()).AddDays(1); 
                    break;
                case "SpecialProgram":
                    reportCon = LoadControl(@"~/Controls/SRSpecialProgram.ascx");
                    ((Controls_SRSpecialProgram)reportCon).m_StartDate = Convert.ToDateTime(tbStartDate.Text.Trim()).AddSeconds(-1);
                    ((Controls_SRSpecialProgram)reportCon).m_EndDate = Convert.ToDateTime(tbEndDate.Text.Trim()).AddDays(1);
                    break;
                case "Degree":
                    reportCon = LoadControl(@"~/Controls/SRDegree.ascx");
                    ((Controls_SRDegree)reportCon).m_StartDate = Convert.ToDateTime(tbStartDate.Text.Trim()).AddSeconds(-1);
                    ((Controls_SRDegree)reportCon).m_EndDate = Convert.ToDateTime(tbEndDate.Text.Trim()).AddDays(1);
                    break;
                case "ResearchGroup":
                    reportCon = LoadControl(@"~/Controls/SRResearchGroup.ascx");
                    ((Controls_SRResearchGroup)reportCon).m_StartDate = Convert.ToDateTime(tbStartDate.Text.Trim()).AddSeconds(-1);
                    ((Controls_SRResearchGroup)reportCon).m_EndDate = Convert.ToDateTime(tbEndDate.Text.Trim()).AddDays(1);
                    break;
                default:
                    reportCon = LoadControl(@"~/Controls/SRChannelSource.ascx");
                    ((Controls_SRChannelSource)reportCon).m_StartDate = Convert.ToDateTime(tbStartDate.Text.Trim()).AddSeconds(-1);
                    ((Controls_SRChannelSource)reportCon).m_EndDate = Convert.ToDateTime(tbEndDate.Text.Trim()).AddDays(1);
                    break;
            }
            phReport.Controls.Add(reportCon);
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