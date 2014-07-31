/**********************************************************************
 * SourcingReport_New.aspx had raplaced this page
 * Yi Shao 2009-9-15
 * *******************************************************************/
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
using System.Text;
using System.Collections.Generic;
using MSRA.SpringField.Components;
using MSRA.SpringField.Components.BizObjects;

namespace MSRA.SpringField.Application
{
    public class SourceItem
    {
        private DateTime m_StartDate, m_EndDate;
        public SourceItem(DateTime startDate, DateTime endDate)
        {
            m_StartDate = startDate;
            m_EndDate = endDate;
        }

        private string GenerateSourceTitle()
        {
            StringBuilder sb = new StringBuilder();
            foreach (string source in GetSourceList())
            {
                if (source == "")
                    sb.AppendLine(String.Format("<td>{0}</td>", "Blank"));
                else
                    sb.AppendLine(String.Format("<td>{0}</td>", source));
            }
            sb.AppendLine("<td>Total</td>");
            return sb.ToString();
        }

        private string GenerateReceiveCount()
        {
            StringBuilder sb = new StringBuilder();

            string defaultRowFilter = "ApplicationDate >= #" + m_StartDate.ToString("MM/dd/yyyy") + "# AND ApplicationDate <= #" + m_EndDate.AddDays(1.0).ToString("MM/dd/yyyy") + "#";
            DataSet applicants = Applicant.GetAllApplicants();
            DataView dv = new DataView(applicants.Tables[0]);
            string cacheKey = m_StartDate.ToString("yyyyMMdd") + "_" + m_EndDate.ToString("yyyyMMdd") + "_receive";
            if (SiteCache.Get(cacheKey + "_total") == null)
            {
                SiteCache.Insert(cacheKey + "_total", defaultRowFilter, SiteCache.DefaultExpiration);
            }
            int total = 0;
            foreach (string source in GetSourceList())
            {
                string rowFilter = defaultRowFilter + " AND JobInfoSource = '" + source + "'";
                dv.RowFilter = rowFilter;
                if (SiteCache.Get(cacheKey + "_" + source) == null)
                {
                    SiteCache.Insert(cacheKey + "_" + source, rowFilter, SiteCache.DefaultExpiration);
                }
                sb.AppendLine(String.Format("<td><a href='ShowCandidates.aspx?type=source&StartDate=" + m_StartDate.ToString("MM/dd/yyyy") + "&EndDate=" + m_EndDate.ToString("MM/dd/yyyy") + "&ds=all&key=" + cacheKey + "_" + source + "'>{0}</a></td>", dv.Count.ToString()));
                total += dv.Count;
            }
            sb.AppendLine(String.Format("<td><a href='ShowCandidates.aspx?type=source&StartDate=" + m_StartDate.ToString("MM/dd/yyyy") + "&EndDate=" + m_EndDate.ToString("MM/dd/yyyy") + "&ds=all&key=" + cacheKey + "_total'>{0}</a></td>", total.ToString()));
            return sb.ToString();
        }

        protected ArrayList GetSourceList()
        {
            ArrayList list = new ArrayList();
            list.Add("Referral");
            list.Add("Microsoft Student Technical Club");
            list.Add("Microsoft Campus Event");
            list.Add("BBS");
            list.Add("Web Site");
            list.Add("Poster/Flyer/Newspaper");
            list.Add("My advisor recommend me to MSRA");
            list.Add("Talent Futherance Program");
            list.Add("Other");
            list.Add("");
            return list;
        }
        private string GenerateItemCount(string defaultRowFilter, string key)
        {
            StringBuilder sb = new StringBuilder();
            DataSet applicants = Applicant.GetAllInterviewApplicants();
            DataView dv = new DataView(applicants.Tables[0]);
            int total = 0;
            string cacheKey = m_StartDate.ToString("yyyyMMdd") + "_" + m_EndDate.ToString("yyyyMMdd") + key;
            if (SiteCache.Get(cacheKey + "_total") == null)
            {
                SiteCache.Insert(cacheKey + "_total", defaultRowFilter, SiteCache.DefaultExpiration);
            }

            foreach (string source in GetSourceList())
            {
                string rowFilter = defaultRowFilter + " AND JobInfoSource = '" + source + "'";
                dv.RowFilter = rowFilter;
                if (SiteCache.Get(cacheKey + "_" + source) == null)
                {
                    SiteCache.Insert(cacheKey + "_" + source, rowFilter, SiteCache.DefaultExpiration);
                }
                sb.AppendLine(String.Format("<td><a href='ShowCandidates.aspx?type=source&StartDate=" + m_StartDate.ToString("MM/dd/yyyy") + "&EndDate=" + m_EndDate.ToString("MM/dd/yyyy") + "&ds=interview&key=" + cacheKey + "_" + source + "'>{0}</td>", dv.Count.ToString()));
                total += dv.Count;
            }
            sb.AppendLine(String.Format("<td><a href='ShowCandidates.aspx?type=source&StartDate=" + m_StartDate.ToString("MM/dd/yyyy") + "&EndDate=" + m_EndDate.ToString("MM/dd/yyyy") + "&ds=interview&key=" + cacheKey + "_total'>{0}</td>", total.ToString()));
            return sb.ToString();
        }

        private string GenerateInterviewCount()
        {
            string defaultRowFilter = "StartDate >= #" + m_StartDate.ToString("MM/dd/yyyy") + "# AND StartDate <= #" + m_EndDate.AddDays(1.0).ToString("MM/dd/yyyy") + "#";
            return GenerateItemCount(defaultRowFilter, "_interview");
        }

        private string GenerateHireCount()
        {
            string defaultRowFilter = "StartDate >= #" + m_StartDate.ToString("MM/dd/yyyy") + "# AND StartDate <= #" + m_EndDate.AddDays(1.0).ToString("MM/dd/yyyy") + "# AND InterviewStatus = 3";
            return GenerateItemCount(defaultRowFilter, "_hire");
        }

        private string GenerateDeclineOfferCount()
        {
            string defaultRowFilter = "StartDate >= #" + m_StartDate.ToString("MM/dd/yyyy") + "# AND StartDate <= #" + m_EndDate.AddDays(1.0).ToString("MM/dd/yyyy") + "# AND InterviewStatus = 5";
            return GenerateItemCount(defaultRowFilter, "_defline");
        }

        private string GenerateRejectedCount()
        {
            string defaultRowFilter = "StartDate >= #" + m_StartDate.ToString("MM/dd/yyyy") + "# AND StartDate <= #" + m_EndDate.AddDays(1.0).ToString("MM/dd/yyyy") + "# AND InterviewStatus = 4";
            return GenerateItemCount(defaultRowFilter, "_reject");
        }

        /*
         * Add by Yuanqin, 2011.5.5
         */
        private string GenerateQualifiedButNotMatchedCount()
        {
            string defaultRowFilter = "StartDate >= #" + m_StartDate.ToString("MM/dd/yyyy") + "# AND StartDate <= #" + m_EndDate.AddDays(1.0).ToString("MM/dd/yyyy") + "# AND InterviewStatus = 7";
            return GenerateItemCount(defaultRowFilter, "_qualifiedButNotMatched");
        }

        public string StartDate
        {
            get { return m_StartDate.ToString("MMM.dd.yyyy"); }
        }

        public string EndDate
        {
            get { return m_EndDate.ToString("MMM.dd.yyyy"); }
        }

        public string SourceTitle
        {
            get
            {
                return GenerateSourceTitle();
            }
        }

        public string ReceiveCount
        {
            get
            {
                return GenerateReceiveCount();
            }
        }

        public string InterviewCount
        {
            get
            {
                return GenerateInterviewCount();
            }
        }

        public string HireCount
        {
            get
            {
                return GenerateHireCount();
            }
        }

        /*
         * Add by Yuanqin, 2011.5.5
         * For QualifiedButNotMatched
         */
        public string QualifiedButNotMatchedCount
        {
            get
            {
                return GenerateQualifiedButNotMatchedCount();
            }
        }

        public string RejectCount
        {
            get
            {
                return GenerateRejectedCount();
            }
        }

        public string DeclineOfferCount
        {
            get
            {
                return GenerateDeclineOfferCount();
            }
        }

        public string Month
        {
            get
            {
                return m_StartDate.ToString("yyyy-MMM");
            }
        }
    }

    public partial class SourcingReport : System.Web.UI.Page
    {
        private DateTime m_StartDate, m_EndDate;
        protected void Page_Load(object sender, EventArgs e)
        {
            bool isDefault = true;
            if (!IsPostBack)
            {
                isDefault = InitDate();
                if (isDefault)
                {
                    dlSourceTable.DataSource = GenerateSourceList(3);
                    dlSourceTable.DataBind();
                }
                else
                {
                    List<SourceItem> list = new List<SourceItem>();
                    //list.Add(new SourceItem(m_StartDate, m_EndDate.AddDays(1)));
                    list.Add(new SourceItem(m_StartDate, m_EndDate));
                    dlSourceTable.DataSource = list;
                    dlSourceTable.DataBind();
                }
            }
        }

        private bool InitDate()
        {
            if (Request["StartDate"] != null && Request["EndDate"] != null)
            {
                try
                {
                    m_StartDate = Convert.ToDateTime(Request["StartDate"]);
                    m_EndDate = Convert.ToDateTime(Request["EndDate"]);
                }
                catch
                {
                    return true;
                }
                return false;
            }
            return true;
        }

        private List<SourceItem> GenerateSourceList(int count)
        {
            List<SourceItem> list = new List<SourceItem>();
            int curMonth = DateTime.Now.Month;
            int curYear = DateTime.Now.Year;
            for (int i = 0; i < count; i++)
            {
                DateTime startDate = new DateTime(curYear, curMonth - i, 1);
                DateTime endDate = new DateTime(curYear, curMonth - i, DateTime.DaysInMonth(curYear, curMonth - i));
                list.Add(new SourceItem(startDate, endDate));
            }
            return list;
        }
    }
}