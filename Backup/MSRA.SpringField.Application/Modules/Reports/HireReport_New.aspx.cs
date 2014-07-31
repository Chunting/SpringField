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
using System.IO;

namespace MSRA.SpringField.Application
{
    public partial class HireReport_New : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                tbStartDate.Attributes.Add("onclick", "popUpCalendar(this,document.all." + tbStartDate.ClientID + ",'yyyy-mm-dd');");
                tbEndDate.Attributes.Add("onclick", "popUpCalendar(this,document.all." + tbEndDate.ClientID + ",'yyyy-mm-dd');");
                //tbStartDate.Attributes.Add("readonly", "true");
                //tbEndDate.Attributes.Add("readonly", "true");
                InitDate();
                if (Request["Group"] != null)
                    ddlReportType.SelectedValue = "Group";

                //Add by Yuanqin,2011.3.7  For OnBoardReport
                if (Request["IsOffline"] != null)
                    ddlReportType.SelectedValue = "IsOffline";
            }

            BindReport();
        }

        private void BindReport()
        {
            Control reportCon;
            switch (ddlReportType.SelectedValue)
            {
                case "Mentor":
                    reportCon = LoadControl(@"~/Controls/InterviewReport.ascx");
                    reportCon.ID = "InterviewReportCon";
                    ((Controls_InterviewReport)reportCon).m_StartDate = Convert.ToDateTime(tbStartDate.Text.Trim());
                    ((Controls_InterviewReport)reportCon).m_EndDate = Convert.ToDateTime(tbEndDate.Text.Trim());
                    break;
                case "Group":
                    reportCon = LoadControl(@"~/Controls/HiringReport.ascx");
                    reportCon.ID = "HiringReportCon";
                    ((Controls_HiringReport)reportCon).m_StartDate = Convert.ToDateTime(tbStartDate.Text.Trim());
                    ((Controls_HiringReport)reportCon).m_EndDate = Convert.ToDateTime(tbEndDate.Text.Trim());
                    break;
                case "IsOffline":
                    reportCon = LoadControl(@"~/Controls/OnBoardReport.ascx");
                    reportCon.ID = "OnBoardReportCon";
                    ((Controls_OnBoardReport)reportCon).m_StartDate = Convert.ToDateTime(tbStartDate.Text.Trim());
                    ((Controls_OnBoardReport)reportCon).m_EndDate = Convert.ToDateTime(tbEndDate.Text.Trim());
                    break;
                default:
                    reportCon = LoadControl(@"~/Controls/InterviewReport.ascx");
                    reportCon.ID = "InterviewReportCon";
                    ((Controls_InterviewReport)reportCon).m_StartDate = Convert.ToDateTime(tbStartDate.Text.Trim());
                    ((Controls_InterviewReport)reportCon).m_EndDate = Convert.ToDateTime(tbEndDate.Text.Trim());
                    break;
            }
            reportCon.EnableViewState = false;
            phReport.Controls.Add(reportCon);
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            lbDateSpan.Text = "Date From " + tbStartDate.Text + " To " + tbEndDate.Text;
            if (ddlReportType.SelectedValue == "Mentor")
            {
                this.phReport.FindControl("InterviewReportCon").FindControl("gvDetailCandidates").Visible = false;
                if (this.phReport.FindControl("InterviewReportCon").FindControl("btngvDetailReportExportExcel") != null)
                {
                    this.phReport.FindControl("InterviewReportCon").FindControl("btngvDetailReportExportExcel").Visible = false;
                }

                if (this.phReport.FindControl("InterviewReportCon").FindControl("btngvReportExportExcel") != null)
                {
                    this.phReport.FindControl("InterviewReportCon").FindControl("btngvReportExportExcel").Visible = false;
                }
            }
            else if (ddlReportType.SelectedValue == "Group")
            {
                this.phReport.FindControl("HiringReportCon").FindControl("gvDetailedReport").Visible = false;
                if (this.phReport.FindControl("HiringReportCon").FindControl("btngvDetailReportExportExcel") != null)
                {
                    this.phReport.FindControl("HiringReportCon").FindControl("btngvDetailReportExportExcel").Visible = false;
                }

                if (this.phReport.FindControl("HiringReportCon").FindControl("btngvReportExportExcel") != null)
                {
                    this.phReport.FindControl("HiringReportCon").FindControl("btngvReportExportExcel").Visible = false;
                }
            }
            else if (ddlReportType.SelectedValue == "IsOffline")
            {
                this.phReport.FindControl("OnBoardReportCon").FindControl("gvDetailedReport").Visible = false;
                if (this.phReport.FindControl("OnBoardReportCon").FindControl("btngvDetailReportExportExcel") != null)
                {
                    this.phReport.FindControl("OnBoardReportCon").FindControl("btngvDetailReportExportExcel").Visible = false;
                }

                if (this.phReport.FindControl("OnBoardReportCon").FindControl("btngvReportExportExcel") != null)
                {
                    this.phReport.FindControl("OnBoardReportCon").FindControl("btngvReportExportExcel").Visible = false;
                }
            }
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
        public override void VerifyRenderingInServerForm(Control control)
        {
        }

        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename=Sourcing Report - by Channel and Sourcing" + DateTime.Now.ToString() + ".xls");
            Response.ContentType = "application/excel";
            Response.Charset = "utf-8";
            Response.Write("<meta http-equiv=Content-Type content=text/html;charset=utf-8>");
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            phReport.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }
    }
}