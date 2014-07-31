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
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Drawing;
using MSRA.SpringField.Components.BizObjects;
using MSRA.SpringField.Components;
using MSRA.SpringField.Components.Enumerations;

namespace MSRA.SpringField.Application.Controls
{
    public partial class Controls_SRChannelSource : System.Web.UI.UserControl
    {
        private DataView AllCandidates;
        public DateTime m_StartDate, m_EndDate;
        private const string FILTER_JOBPOSTING = "(JobInfoSource IN ('BBS','Web Site','Poster/Flyer/Newspaper','Internship Program Flyer','Campus Poster','Newsletter'))";
        private const string FILTER_JOBPOSTING_BBS = "(JobInfoSource = 'BBS')";
        private const string FILTER_JOBPOSTING_WEBSITE = "(JobInfoSource = 'Web Site')";
        private const string FILTER_JOBPOSTING_PFN = "(JobInfoSource IN ('Poster/Flyer/Newspaper','Internship Program Flyer','Campus Poster','Newsletter'))";
        private const string FILTER_REFERRAL = "(JobInfoSource = 'Referral')";
        /*
         * JobInfoChannel = 'MS Employee' => JobInforChannel IN ('MS Employee', 'Employee')
         * JobInfoChannel = 'MS Intern' => JobInforChannel IN ('MS Intern','Intern')
         */
        private const string FILTER_REFERRAL_EMPLOYEE = "(JobInfoSource = 'Referral' AND JobInfoChannel IN ('MS Employee', 'Employee'))";
        private const string FILTER_REFERRAL_INTERN = "(JobInfoSource = 'Referral' AND JobInfoChannel IN ('MS Intern','Intern'))";
        private const string FILTER_REFERRAL_OTHER = "(JobInfoSource = 'Referral' AND JobInfoChannel NOT IN ('MS Intern','MS Employee', 'Employee', 'Intern'))";

        private const string FILTER_ADVISORRECOMMENDATION = "(JobInfoSource IN ('My advisor recommend me to MSRA','Advisor Recommend','Prof Recommendation'))";
        private const string FILTER_URPROJECT = "(JobInfoSource IN ('Microsoft Campus Event','Campus Event','Microsoft Student Technical Club','Microsoft Studnet Technical Club','Student Technical Club','Talent Futherance Program'))";
        private const string FILTER_URPROJECT_CAMPUSEVENT = "(JobInfoSource IN ('Microsoft Campus Event','Campus Event'))";
        private const string FILTER_URPROJECT_MSTC = "(JobInfoSource IN ('Microsoft Student Technical Club','Microsoft Studnet Technical Club','Student Technical Club'))";
        private const string FILTER_URPROJECT_TALENT = "(JobInfoSource = 'Talent Futherance Program')";
        private const string FILTER_OTHERCHANNEL = "(JobInfoSource NOT IN ('BBS','Web Site','Poster/Flyer/Newspaper','Internship Program Flyer','Campus Poster','Newsletter','Referral','My advisor recommend me to MSRA','Advisor Recommend','Prof Recommendation','Microsoft Campus Event','Campus Event','Microsoft Student Technical Club','Microsoft Studnet Technical Club','Student Technical Club','Talent Futherance Program'))";

        protected void Page_Load(object sender, EventArgs e)
        {
            #region
            AllCandidates = new DataView(CashedApplicantInfo.GetAllApplcantsforSourcingReport(m_StartDate, m_EndDate).Tables[0]);

            DataTable TBChannel = new DataTable();
            TBChannel.Columns.Add("Channel", typeof(string));
            TBChannel.Columns.Add("Cadt #", typeof(string));
            TBChannel.Columns.Add("Interviewed Cadt #", typeof(string));
            TBChannel.Columns.Add("Hired #", typeof(string));
            TBChannel.Columns.Add("QualifiedButNotMatched #", typeof(string));  //Add by Yuanqin, 2011.5.5
            TBChannel.Columns.Add("Rejected #", typeof(string));
            TBChannel.Columns.Add("Decline Offer #", typeof(string));

            TBChannel.Rows.Add("Job Posting", "0", "0", "0", "0", "0", "0");
            TBChannel.Rows.Add("BBS", "0", "0", "0", "0", "0", "0");
            TBChannel.Rows.Add("Website", "0", "0", "0", "0", "0");
            TBChannel.Rows.Add("Poster/Flyer/Newspaper", "0", "0", "0", "0", "0", "0");
            TBChannel.Rows.Add("Referral", "0", "0", "0", "0", "0", "0");
            TBChannel.Rows.Add("Employee", "0", "0", "0", "0", "0", "0");
            TBChannel.Rows.Add("Intern", "0", "0", "0", "0", "0", "0");
            TBChannel.Rows.Add("Other", "0", "0", "0", "0", "0", "0");
            TBChannel.Rows.Add("Advisor Recommendation", "0", "0", "0", "0", "0", "0");
            TBChannel.Rows.Add("UR Project", "0", "0", "0", "0", "0", "0");
            TBChannel.Rows.Add("Campus Event", "0", "0", "0", "0", "0", "0");
            TBChannel.Rows.Add("MSTC", "0", "0", "0", "0", "0", "0");
            TBChannel.Rows.Add("Talent Furtherance Program", "0", "0", "0", "0", "0", "0");
            TBChannel.Rows.Add("Other Channel", "0", "0", "0", "0", "0", "0");
            TBChannel.Rows.Add("Grand Total", "0", "0", "0", "0", "0", "0");
            #endregion
            Hashtable filterHT = new Hashtable();
            filterHT.Add("Job Posting", FILTER_JOBPOSTING);
            filterHT.Add("BBS", FILTER_JOBPOSTING_BBS);
            filterHT.Add("Website", FILTER_JOBPOSTING_WEBSITE);
            filterHT.Add("Poster/Flyer/Newspaper", FILTER_JOBPOSTING_PFN);
            filterHT.Add("Referral", FILTER_REFERRAL);
            filterHT.Add("Employee", FILTER_REFERRAL_EMPLOYEE);
            filterHT.Add("Intern", FILTER_REFERRAL_INTERN);
            filterHT.Add("Other", FILTER_REFERRAL_OTHER);
            filterHT.Add("Advisor Recommendation", FILTER_ADVISORRECOMMENDATION);
            filterHT.Add("UR Project", FILTER_URPROJECT);
            filterHT.Add("Campus Event", FILTER_URPROJECT_CAMPUSEVENT);
            filterHT.Add("MSTC", FILTER_URPROJECT_MSTC);
            filterHT.Add("Talent Furtherance Program", FILTER_URPROJECT_TALENT);
            filterHT.Add("Other Channel", FILTER_OTHERCHANNEL);
            filterHT.Add("Grand Total", "(1=1)");

            //string strApplicationDateFilter = "(ApplicationDate >= '" + m_StartDate.ToString() + "' AND ApplicationDate <= '" + m_EndDate.AddDays(1).ToString() + "') ";
            string strFilter;
            string Key;

            #region //Cadt #
            for (int i = 0; i < TBChannel.Rows.Count; i++)
            {
                strFilter = filterHT[TBChannel.Rows[i]["Channel"]].ToString();
                Key = "Channel_Cadt_" + i.ToString() + "_" + m_StartDate.ToShortDateString() + "_" + m_EndDate.ToShortDateString();
                if (SiteCache.Get(Key) == null)
                {
                    SiteCache.Insert(Key, strFilter, SiteCache.DefaultExpiration);
                }
                AllCandidates.RowFilter = strFilter;
                TBChannel.Rows[i]["Cadt #"] = String.Format("<a href='ShowCandidates.aspx?type=source&StartDate=" + m_StartDate.AddSeconds(1).ToString("MM/dd/yyyy") + "&EndDate=" + m_EndDate.AddSeconds(-1).ToString("MM/dd/yyyy") + "&key=" + Key + "'>{0}</a>", AllCandidates.Count.ToString());
            }
            #endregion
            #region //Interviewed Cadt #
            for (int i = 0; i < TBChannel.Rows.Count; i++)
            {
                strFilter = filterHT[TBChannel.Rows[i]["Channel"]].ToString() + " AND (InterviewStatus > -1)";
                Key = "Channel_Interviewed_" + i.ToString() + "_" + m_StartDate.ToShortDateString() + "_" + m_EndDate.ToShortDateString();
                if (SiteCache.Get(Key) == null)
                {
                    SiteCache.Insert(Key, strFilter, SiteCache.DefaultExpiration);
                }
                AllCandidates.RowFilter = strFilter;
                TBChannel.Rows[i]["Interviewed Cadt #"] = String.Format("<a href='ShowCandidates.aspx?type=source&StartDate=" + m_StartDate.AddSeconds(1).ToString("MM/dd/yyyy") + "&EndDate=" + m_EndDate.AddSeconds(-1).ToString("MM/dd/yyyy") + "&key=" + Key + "'>{0}</a>", AllCandidates.Count.ToString());
            }
            #endregion
            #region //Hired #
            for (int i = 0; i < TBChannel.Rows.Count; i++)
            {
                strFilter = filterHT[TBChannel.Rows[i]["Channel"]].ToString() + " AND (InterviewStatus = " + ((int)InterviewStatusEnum.Hired).ToString() + ")";
                Key = "Channel_Hired_" + i.ToString() + "_" + m_StartDate.ToShortDateString() + "_" + m_EndDate.ToShortDateString();
                if (SiteCache.Get(Key) == null)
                {
                    SiteCache.Insert(Key, strFilter, SiteCache.DefaultExpiration);
                }
                AllCandidates.RowFilter = strFilter;
                TBChannel.Rows[i]["Hired #"] = String.Format("<a href='ShowCandidates.aspx?type=source&StartDate=" + m_StartDate.AddSeconds(1).ToString("MM/dd/yyyy") + "&EndDate=" + m_EndDate.AddSeconds(-1).ToString("MM/dd/yyyy") + "&key=" + Key + "'>{0}</a>", AllCandidates.Count.ToString());
            }
            #endregion
            /*
             * Add by Yuanqin,2011.5.5
             * For QualifiedButNotMatched
             */
            #region //QualifiedButNotMatched #
            for (int i = 0; i < TBChannel.Rows.Count; i++)
            {
                strFilter = filterHT[TBChannel.Rows[i]["Channel"]].ToString() + " AND (InterviewStatus = " + ((int)InterviewStatusEnum.QualifiedButNotMatched).ToString() + ")";
                Key = "Channel_QualifiedButNotMatched_" + i.ToString() + "_" + m_StartDate.ToShortDateString() + "_" + m_EndDate.ToShortDateString();
                if (SiteCache.Get(Key) == null)
                {
                    SiteCache.Insert(Key, strFilter, SiteCache.DefaultExpiration);
                }
                AllCandidates.RowFilter = strFilter;
                TBChannel.Rows[i]["QualifiedButNotMatched #"] = String.Format("<a href='ShowCandidates.aspx?type=source&StartDate=" + m_StartDate.AddSeconds(1).ToString("MM/dd/yyyy") + "&EndDate=" + m_EndDate.AddSeconds(-1).ToString("MM/dd/yyyy") + "&key=" + Key + "'>{0}</a>", AllCandidates.Count.ToString());
            }
            #endregion
            #region //Rejected #
            for (int i = 0; i < TBChannel.Rows.Count; i++)
            {
                strFilter = filterHT[TBChannel.Rows[i]["Channel"]].ToString() + " AND (InterviewStatus = " + ((int)InterviewStatusEnum.Rejected).ToString() + ")";
                Key = "Channel_Rejected_" + i.ToString() + "_" + m_StartDate.ToShortDateString() + "_" + m_EndDate.ToShortDateString();
                if (SiteCache.Get(Key) == null)
                {
                    SiteCache.Insert(Key, strFilter, SiteCache.DefaultExpiration);
                }
                AllCandidates.RowFilter = strFilter;
                TBChannel.Rows[i]["Rejected #"] = String.Format("<a href='ShowCandidates.aspx?type=source&StartDate=" + m_StartDate.AddSeconds(1).ToString("MM/dd/yyyy") + "&EndDate=" + m_EndDate.AddSeconds(-1).ToString("MM/dd/yyyy") + "&key=" + Key + "'>{0}</a>", AllCandidates.Count.ToString());
            }
            #endregion
            #region //Decline Offer #
            for (int i = 0; i < TBChannel.Rows.Count; i++)
            {
                strFilter = filterHT[TBChannel.Rows[i]["Channel"]].ToString() + " AND (InterviewStatus = " + ((int)InterviewStatusEnum.OfferDeclined).ToString() + ")";
                Key = "Channel_DeclineOffer_" + i.ToString() + "_" + m_StartDate.ToShortDateString() + "_" + m_EndDate.ToShortDateString();
                if (SiteCache.Get(Key) == null)
                {
                    SiteCache.Insert(Key, strFilter, SiteCache.DefaultExpiration);
                }
                AllCandidates.RowFilter = strFilter;
                TBChannel.Rows[i]["Decline Offer #"] = String.Format("<a href='ShowCandidates.aspx?type=source&StartDate=" + m_StartDate.AddSeconds(1).ToString("MM/dd/yyyy") + "&EndDate=" + m_EndDate.AddSeconds(-1).ToString("MM/dd/yyyy") + "&key=" + Key + "'>{0}</a>", AllCandidates.Count.ToString());
            }
            #endregion

            gvChannelSource.DataSource = TBChannel.DefaultView;
            gvChannelSource.DataBind();

            ModifyStyle();
            //gvChannelSource.PreRender += new EventHandler(gvChannelSource_PreRender);
        }

        protected void gvChannelSource_PreRender(object sender, EventArgs e)
        {
            ModifyStyle();
        }
        private void ModifyStyle()
        {
            if (gvChannelSource.Rows.Count > 0)
            {
                gvChannelSource.Rows[0].BackColor = System.Drawing.Color.FromName("#eeeeee");
                gvChannelSource.Rows[0].Font.Bold = true;
                gvChannelSource.Rows[4].BackColor = System.Drawing.Color.FromName("#eeeeee");
                gvChannelSource.Rows[4].Font.Bold = true;
                gvChannelSource.Rows[8].BackColor = System.Drawing.Color.FromName("#eeeeee");
                gvChannelSource.Rows[8].Font.Bold = true;
                gvChannelSource.Rows[9].BackColor = System.Drawing.Color.FromName("#eeeeee");
                gvChannelSource.Rows[9].Font.Bold = true;
                gvChannelSource.Rows[13].BackColor = System.Drawing.Color.FromName("#eeeeee");
                gvChannelSource.Rows[13].Font.Bold = true;
                gvChannelSource.Rows[14].Font.Bold = true;
                gvChannelSource.Rows[14].Font.Italic = true;
            }
        }

        public void btnExportExcel_Click(object sender, EventArgs e)
        {
            ModifyStyle();
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename=Sourcing Report - by Channel and Sourcing" + DateTime.Now.ToString() + ".xls");
            Response.ContentType = "application/excel";
            Response.Charset = "utf-8";
            Response.Write("<meta http-equiv=Content-Type content=text/html;charset=utf-8>");
            Response.ContentEncoding = Encoding.UTF8;
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            gvChannelSource.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }
    }
}