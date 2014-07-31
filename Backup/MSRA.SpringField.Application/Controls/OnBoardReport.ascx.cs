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
using MSRA.SpringField.Components.Enumerations;

/*
 * OnBoardReport Added by: Yuanqin
 * Date:2011.3.6
 * 
 */

namespace MSRA.SpringField.Application.Controls
{
    public partial class Controls_OnBoardReport : System.Web.UI.UserControl
    {
        public DateTime m_StartDate = DateTime.Now.AddMonths(-1), m_EndDate = DateTime.Now;
        //DataView allCheckInForm;
        DataView AllCandidateByDate;

        protected void Page_Load(object sender, EventArgs e)
        {
            //allCheckInForm = CheckInForm.GetAllCheckinFormforHiringReport(m_StartDate, m_EndDate).Tables[0].DefaultView;
            AllCandidateByDate = Applicant.GetApplicantsByCheckInDate(m_StartDate, m_EndDate).Tables[0].DefaultView;
            BindHringReport();
            if (Request["IsOffline"] != null)
                BindDetailedReport();
        }

        protected void gvDetailedReport_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDetailedReport.PageIndex = e.NewPageIndex;
            if (Request["IsOffline"] != null)
                BindDetailedReport();
        }

        private void BindHringReport()
        {
            DataTable tbHringReport = new DataTable();
            //tbHringReport.Columns.Add("GroupName", typeof(string));
            tbHringReport.Columns.Add("Way Of Incruit", typeof(string));
            tbHringReport.Columns.Add("On Board Cadt #", typeof(string));

            string[] OfflineArray = EnumHelper.GetEnumStrings(typeof(IsOfflineEnum));
            for (int i = 0; i < OfflineArray.Length; i++)
            {
                tbHringReport.Rows.Add(OfflineArray[i], "0");
            }
            tbHringReport.Rows.Add("Grand Total", "0");

            string statusFilter = "(Status = 8)";
            string strCount;
            for (int i = 0; i < OfflineArray.Length; i++)
            {
                string OfflineFilter = " AND (IsOffline = " + EnumHelper.EnumToInteger(EnumHelper.StringToEnum(typeof(IsOfflineEnum),OfflineArray[i])) +")";
                AllCandidateByDate.RowFilter = statusFilter + OfflineFilter;
                strCount = String.Format("<a href='HireReport_New.aspx?StartDate={0}&EndDate={1}&IsOffline={2}'>{3}</a>", m_StartDate.ToString("MM/dd/yyyy"), m_EndDate.ToString("MM/dd/yyyy"), OfflineArray[i].ToString(), AllCandidateByDate.Count.ToString());
                tbHringReport.Rows[i]["On Board Cadt #"] = strCount;
            }
            AllCandidateByDate.RowFilter = statusFilter;
            strCount = String.Format("<a href='HireReport_New.aspx?StartDate={0}&EndDate={1}&IsOffline={2}'>{3}</a>", m_StartDate.ToString("MM/dd/yyyy"), m_EndDate.ToString("MM/dd/yyyy"), "All", AllCandidateByDate.Count.ToString());
            tbHringReport.Rows[tbHringReport.Rows.Count - 1]["On Board Cadt #"] = strCount;
            gvReport.DataSource = tbHringReport;
            gvReport.DataBind();

            //string statusFilter = "(InterviewStatus = 3)";
            //string strCount;
            //for (int i = 0; i < GroupList.Count; i++)
            //{
            //    string GroupFilter = " AND (GroupId = " + CheckInFormResourceManager.TextToId("Groups", GroupList[i].ToString()).ToString() + ")";
            //    allCheckInForm.RowFilter = statusFilter + GroupFilter;
            //    strCount = String.Format("<a href='HireReport_New.aspx?StartDate={0}&EndDate={1}&Group={2}'>{3}</a>", m_StartDate.ToString("MM/dd/yyyy"), m_EndDate.ToString("MM/dd/yyyy"), GroupList[i].ToString(), allCheckInForm.Count.ToString());
            //    tbHringReport.Rows[i]["Hired Cadt #"] = strCount;
            //}
            //allCheckInForm.RowFilter = statusFilter;
            //strCount = String.Format("<a href='HireReport_New.aspx?StartDate={0}&EndDate={1}&Group={2}'>{3}</a>", m_StartDate.ToString("MM/dd/yyyy"), m_EndDate.ToString("MM/dd/yyyy"), "All", allCheckInForm.Count.ToString());
            //tbHringReport.Rows[tbHringReport.Rows.Count - 1]["Hired Cadt #"] = strCount;

            //gvReport.DataSource = tbHringReport;
            //gvReport.DataBind();
        }

        private void BindDetailedReport()
        {
            if (Request["IsOffline"].ToString().ToLower() == "all")
            {
                AllCandidateByDate.RowFilter = "(Status = 8)";
            }
            else
            {
                //string GroupId;
                //try
                //{
                //    GroupId = CheckInFormResourceManager.TextToId("Groups", Request["Group"].ToString()).ToString();
                //}
                //catch
                //{
                //    JSUtility.Alert(this.Page, "Incorrect Parameters!");
                //    return;
                //}

                //allCheckInForm.RowFilter = "(InterviewStatus = 3) AND (GroupId = " + GroupId + ")";
                //int IsOffline;
                try
                {
                    AllCandidateByDate.RowFilter = "(Status = 8) " + "AND (IsOffline = " + EnumHelper.EnumToInteger(EnumHelper.StringToEnum(typeof(IsOfflineEnum), Request["IsOffline"].ToString())) + ")";
                }
                catch
                {
                    JSUtility.Alert(this.Page, "Incorrect Parameters!");
                    return;
                }
                
            }
            gvDetailedReport.DataSource = AllCandidateByDate;
            gvDetailedReport.DataBind();
        }

        protected string ParseApplyDate(object dataItem)
        {
            DataRowView dr = (DataRowView)dataItem;
            if (!String.IsNullOrEmpty(dr["ApplicationDate"].ToString()))
                return Convert.ToDateTime(dr["ApplicationDate"]).ToShortDateString();
            else
                return "N/A";
        }
        protected string ParseDegree(object dataItem)
        {
            DataRowView dr = (DataRowView)dataItem;
            string strDegree = dr["Degree"].ToString();
            if (!String.IsNullOrEmpty(strDegree))
                return StaticData.DegreeList[Convert.ToInt32(dr["Degree"])];
            else
                return "N/A";
        }
        protected string ParseInstitution(object dataItem)
        {
            DataRowView dr = (DataRowView)dataItem;
            return dr["HighestEducationalInstitution"] as string;
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
            Response.AddHeader("content-disposition", "attachment; filename=On Board Report by IsOffline" + DateTime.Now.ToString() + ".xls");
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
            if (Request["IsOffline"] != null)
                BindDetailedReport();
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename=On Board Report by IsOffline - Detail" + DateTime.Now.ToString() + ".xls");
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