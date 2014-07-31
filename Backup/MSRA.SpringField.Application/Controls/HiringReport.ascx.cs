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
using System.IO;
using System.Text;
using MSRA.SpringField.Components.BizObjects;
using MSRA.SpringField.Components;

namespace MSRA.SpringField.Application.Controls
{
    public partial class Controls_HiringReport : System.Web.UI.UserControl
    {
        public DateTime m_StartDate = DateTime.Now.AddMonths(-1), m_EndDate = DateTime.Now;
        DataView allCheckInForm;
        protected void Page_Load(object sender, EventArgs e)
        {
            allCheckInForm = CheckInForm.GetAllCheckinFormforHiringReport(m_StartDate, m_EndDate).Tables[0].DefaultView;
            BindHringReport();
            if (Request["Group"] != null)
                BindDetailedReport();
        }

        protected void gvDetailedReport_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDetailedReport.PageIndex = e.NewPageIndex;
            if (Request["Group"] != null)
                BindDetailedReport();
        }

        private void BindHringReport()
        {
            DataTable tbHringReport = new DataTable();
            //tbHringReport.Columns.Add("GroupName", typeof(string));
            tbHringReport.Columns.Add("ShortName", typeof(string));
            tbHringReport.Columns.Add("Hired Cadt #", typeof(string));

            /*
             * Modified By: Yin.P
             * Date: 2010-1-6
             */
            ArrayList GroupList = CheckInFormResourceManager.GetTypeDisplayItems("Groups");
            for (int i = 0; i < GroupList.Count; i++)
            {
                tbHringReport.Rows.Add(
                    /*ResourceManager.GetGroupFullNameByShoutName(GroupList[i].ToString()),*/
                    GroupList[i], 
                    "0");
            }
            tbHringReport.Rows.Add("Grand Total", "0");

            string statusFilter = "(InterviewStatus = 3)";
            string strCount;
            for (int i = 0; i < GroupList.Count; i++)
            {
                string GroupFilter = " AND (GroupId = " + CheckInFormResourceManager.TextToId("Groups", GroupList[i].ToString()).ToString() + ")";
                allCheckInForm.RowFilter = statusFilter + GroupFilter;
                strCount = String.Format("<a href='HireReport_New.aspx?StartDate={0}&EndDate={1}&Group={2}'>{3}</a>", m_StartDate.ToString("MM/dd/yyyy"), m_EndDate.ToString("MM/dd/yyyy"), GroupList[i].ToString(), allCheckInForm.Count.ToString());
                tbHringReport.Rows[i]["Hired Cadt #"] = strCount;
            }
            allCheckInForm.RowFilter = statusFilter;
            strCount = String.Format("<a href='HireReport_New.aspx?StartDate={0}&EndDate={1}&Group={2}'>{3}</a>", m_StartDate.ToString("MM/dd/yyyy"), m_EndDate.ToString("MM/dd/yyyy"), "All", allCheckInForm.Count.ToString());
            tbHringReport.Rows[tbHringReport.Rows.Count - 1]["Hired Cadt #"] = strCount;

            gvReport.DataSource = tbHringReport;
            gvReport.DataBind();
        }

        private void BindDetailedReport()
        {
            if (Request["Group"].ToString().ToLower() == "all")
            {
                allCheckInForm.RowFilter = "(InterviewStatus = 3)";
            }
            else
            {
                string GroupId;
                try
                {
                    GroupId = CheckInFormResourceManager.TextToId("Groups", Request["Group"].ToString()).ToString();
                }
                catch
                {
                    JSUtility.Alert(this.Page, "Incorrect Parameters!");
                    return;
                }

                allCheckInForm.RowFilter = "(InterviewStatus = 3) AND (GroupId = " + GroupId + ")";
            }
            gvDetailedReport.DataSource = allCheckInForm;
            gvDetailedReport.DataBind();
        }
        protected string ParseGroup(object dataItem)
        {
            DataRowView dr = (DataRowView)dataItem;
            return CheckInFormResourceManager.IdToText("Groups", Int32.Parse(dr["GroupId"].ToString()));
        }
        protected string ParseApplyDate(object dataItem)
        {
            DataRowView dr = (DataRowView)dataItem;
            if (!String.IsNullOrEmpty(dr["ApplicationDate"].ToString()))
                return Convert.ToDateTime(dr["ApplicationDate"]).ToShortDateString();
            else
                return "N/A";
        }
        protected string ParseManagerDecisionDate(object dataItem)
        {
            DataRowView dr = (DataRowView)dataItem;
            if (!String.IsNullOrEmpty(dr["ManagerDecisionTime"].ToString()))
                return Convert.ToDateTime(dr["ManagerDecisionTime"]).ToShortDateString();
            else
                return "N/A";
        }
        protected string ParseChannel(object dataItem)
        {
            DataRowView dr = (DataRowView)dataItem;
            return dr["JobInfoChannel"].ToString();
        }
        protected string ParseSourcing(object dataItem)
        {
            DataRowView dr = (DataRowView)dataItem;
            return dr["JobInfoSource"].ToString();
        }
        protected void btngvReportExportExcel_Click(object sender, EventArgs e)
        {
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename=Hiring Report by Group" + DateTime.Now.ToString() + ".xls");
            Response.ContentType = "application/excel";
            Response.Charset = "utf-8";
            Response.Write("<meta http-equiv=Content-Type content=text/html;charset=utf-8>");
            Response.ContentEncoding = Encoding.UTF8;
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            gvReport.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }
        protected void btngvDetailReportExportExcel_Click(object sender, EventArgs e)
        {
            gvDetailedReport.AllowPaging = false;
            if (Request["Group"] != null)
                BindDetailedReport();
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename=Hiring Report by Group - Detail" + DateTime.Now.ToString() + ".xls");
            Response.ContentType = "application/excel";
            Response.Charset = "utf-8";
            Response.Write("<meta http-equiv=Content-Type content=text/html;charset=utf-8>");
            Response.ContentEncoding = Encoding.UTF8;
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            gvDetailedReport.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }
    }
}