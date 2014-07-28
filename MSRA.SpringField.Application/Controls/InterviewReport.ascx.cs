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

namespace MSRA.SpringField.Application.Controls
{
    public partial class Controls_InterviewReport : System.Web.UI.UserControl
    {
        public DateTime m_StartDate, m_EndDate;
        private DataView AllCandidates;
        DataTable hiringManagers;
        DataTable TBSource;

        protected void Page_Load(object sender, EventArgs e)
        {
            BindgvReport();
            if (Request["HireManagerID"] != null)
            {
                BindgvDetailCandidates();
            }
        }

        #region gvReport
        private void BindgvReport()
        {
            AllCandidates = new DataView(Interview.GetDurationInterview(m_StartDate, m_EndDate).Tables[0]);
            hiringManagers = Interview.GetDurationInterviewedSiteUser(m_StartDate, m_EndDate).Tables[0];

            TBSource = new DataTable();
            TBSource.Columns.Add("Mentor Alias", typeof(string));
            TBSource.Columns.Add("Interviewed Cadt #", typeof(string));
            TBSource.Columns.Add("Cadt # of Waiting for interview feedback", typeof(string));
            TBSource.Columns.Add("Cadt # of Waiting for mentor's decision", typeof(string));
            TBSource.Columns.Add("Cadt # of waiting for Group Manager's approval", typeof(string));
            TBSource.Columns.Add("Hired #", typeof(string));
            TBSource.Columns.Add("QualifiedButNotMatched #", typeof(string)); //2011.5.5
            TBSource.Columns.Add("Reject #", typeof(string));
            TBSource.Columns.Add("Decline #", typeof(string));

            Guid curId;
            string curSiteUser;
            foreach (DataRow row in hiringManagers.Rows)
            {
                curId = (Guid)row["HiringManagerId"];
                try
                {
                    curSiteUser = SiteUser.GetAliasByUserId(curId);
                }
                catch
                {
                    curSiteUser = "N/A";
                }
                TBSource.Rows.Add(curSiteUser, "0", "0", "0", "0", "0", "0", "0");
            }
            TBSource.Rows.Add("Grant Total", "0", "0", "0", "0", "0", "0", "0");

            generateColumn("", "Interviewed Cadt #");
            generateColumn("(InterviewStatus =" + EnumHelper.EnumToInteger(InterviewStatusEnum.WaitingForInterviewFeedback) + ")", "Cadt # of Waiting for interview feedback");
            generateColumn("(InterviewStatus =" + EnumHelper.EnumToInteger(InterviewStatusEnum.WaitingForMentorDecision) + ")", "Cadt # of Waiting for mentor's decision");
            generateColumn("(InterviewStatus =" + EnumHelper.EnumToInteger(InterviewStatusEnum.WaitingForGroupManagerDecision) + ")", "Cadt # of waiting for Group Manager's approval");
            generateColumn("(InterviewStatus =" + EnumHelper.EnumToInteger(InterviewStatusEnum.Hired) + ")", "Hired #");
            generateColumn("(InterviewStatus =" + EnumHelper.EnumToInteger(InterviewStatusEnum.QualifiedButNotMatched) + ")", "QualifiedButNotMatched #"); //2011.5.5
            generateColumn("(InterviewStatus =" + EnumHelper.EnumToInteger(InterviewStatusEnum.Rejected) + ")", "Reject #");
            generateColumn("(InterviewStatus =" + EnumHelper.EnumToInteger(InterviewStatusEnum.OfferDeclined) + ")", "Decline #");

            gvReport.DataSource = TBSource.DefaultView;
            gvReport.DataBind();
        }
        private void generateColumn(string statusFilter, string columnName)
        {
            string strFilter;
            string count;
            for (int i = 0; i < hiringManagers.Rows.Count; i++)
            {
                if (String.IsNullOrEmpty(statusFilter))
                    strFilter = "(HiringManagerId = '" + hiringManagers.Rows[i]["HiringManagerId"].ToString() + "')";
                else
                    strFilter = "(HiringManagerId = '" + hiringManagers.Rows[i]["HiringManagerId"].ToString() + "') AND " + statusFilter;
                AllCandidates.RowFilter = strFilter;
                count = String.Format("<a href='HireReport_New.aspx?StartDate={0}&EndDate={1}&HireManagerID={2}&statusFilter={3}'>{4}</a>", m_StartDate.ToString("MM/dd/yyyy"), m_EndDate.ToString("MM/dd/yyyy"), hiringManagers.Rows[i]["HiringManagerId"].ToString(), statusFilter, AllCandidates.Count.ToString());
                TBSource.Rows[i][columnName] = count;
            }
            AllCandidates.RowFilter = statusFilter;
            count = String.Format("<a href='HireReport_New.aspx?StartDate={0}&EndDate={1}&HireManagerID={2}&statusFilter={3}'>{4}</a>", m_StartDate.ToString("MM/dd/yyyy"), m_EndDate.ToString("MM/dd/yyyy"), Guid.Empty.ToString(), statusFilter, AllCandidates.Count.ToString());
            TBSource.Rows[TBSource.Rows.Count - 1][columnName] = count;
        }
        protected void gvReport_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvReport.PageIndex = e.NewPageIndex;
            BindgvReport();
        }
        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            gvReport.AllowPaging = false;
            BindgvReport();
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename=Interview Report by Mentor" + DateTime.Now.ToString() + ".xls");
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
        #endregion

        #region gvDetailCandidates
        private void BindgvDetailCandidates()
        {
            Guid HireManagerID = new Guid(Request["HireManagerID"].ToString());
            string filter;
            if (!String.IsNullOrEmpty(Request["statusFilter"].ToString()))
            {
                if (HireManagerID != Guid.Empty)
                {
                    filter = "(HiringManagerId = '" + HireManagerID.ToString() + "') AND " + Request["statusFilter"].ToString();
                }
                else
                {
                    filter = Request["statusFilter"].ToString();
                }
            }
            else
            {
                if (HireManagerID != Guid.Empty)
                {
                    filter = "(HiringManagerId = '" + HireManagerID.ToString() + "')";
                }
                else
                {
                    filter = "";
                }
            }
            AllCandidates.RowFilter = filter;

            gvDetailCandidates.DataSource = AllCandidates;
            gvDetailCandidates.DataBind();
            //btngvDetailCandidatesExportExcel.Visible = true;
        }
        protected void btngvDetailCandidatesExportExcel_Click(object sender, EventArgs e)
        {
            gvDetailCandidates.AllowPaging = false;
            if (Request["HireManagerID"] == null)
            {
                JSUtility.Alert(this.Page, "improper operation!");
                return;
            }
            BindgvDetailCandidates();
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename=Interview Report by Mentor - Detail" + DateTime.Now.ToString() + ".xls");
            Response.ContentType = "application/excel";
            Response.Charset = "utf-8";
            Response.Write("<meta http-equiv=Content-Type content=text/html;charset=utf-8>");
            Response.ContentEncoding = Encoding.UTF8;
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            gvDetailCandidates.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }
        protected void gvDetailCandidates_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDetailCandidates.PageIndex = e.NewPageIndex;
            if (Request["HireManagerID"] != null)
                BindgvDetailCandidates();
        }
        protected string ParseStatus(object dataItem)
        {
            DataRowView dr = (DataRowView)dataItem;
            if (!String.IsNullOrEmpty(dr["Status"].ToString()))
                return StaticData.AppStatusDict[(ApplicationStatusEnum)dr["Status"]];
            else
                return "N/A";
        }
        protected string ParseInstitution(object dataItem)
        {
            DataRowView dr = (DataRowView)dataItem;
            return dr["HighestEducationalInstitution"] as string;
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
        protected string ParseHireManager(object dataItem)
        {
            DataRowView dr = (DataRowView)dataItem;
            string HireManagerAlias;
            if (!String.IsNullOrEmpty(dr["HiringManagerId"].ToString()))
            {
                Guid HireManagerId = (Guid)dr["HiringManagerId"];
                try
                {
                    HireManagerAlias = SiteUser.GetAliasByUserId(HireManagerId);
                }
                catch
                {
                    HireManagerAlias = "N/A";
                }
            }
            else
                HireManagerAlias = "N/A";
            return HireManagerAlias;
        }
        protected string ParseApplyDate(object dataItem)
        {
            DataRowView dr = (DataRowView)dataItem;
            if (!String.IsNullOrEmpty(dr["ApplicationDate"].ToString()))
                return Convert.ToDateTime(dr["ApplicationDate"]).ToShortDateString();
            else
                return "N/A";
        }
        protected string ParseInterviewStatus(object dataItem)
        {
            DataRowView dr = (DataRowView)dataItem;
            Guid curId = (Guid)dr["ApplicantId"];
            return Interview.GetRecentInterviewStatus(curId);
        }
        //interview开始的时间
        protected string ParseLastActionDate(object dataItem)
        {
            DataRowView dr = (DataRowView)dataItem;
            Guid curId = (Guid)dr["ApplicantId"];
            return Interview.GetRecentInterviewDate(curId);
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
        #endregion


    }
}
