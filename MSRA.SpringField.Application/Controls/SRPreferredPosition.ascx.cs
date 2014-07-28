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
using System.Data.SqlClient;
using MSRA.SpringField.Components;
using MSRA.SpringField.Components.Enumerations;
using MSRA.SpringField.Components.BizObjects;

namespace MSRA.SpringField.Application.Controls
{
    public partial class Controls_SRPreferredPosition : System.Web.UI.UserControl
    {
        private DataView AllCandidates;
        public DateTime m_StartDate, m_EndDate;
        protected void Page_Load(object sender, EventArgs e)
        {
            AllCandidates = new DataView(CashedApplicantInfo.GetAllApplcantsforSourcingReport(m_StartDate, m_EndDate).Tables[0]);

            DataTable tbPreferredPosition = new DataTable();
            tbPreferredPosition.Columns.Add("Preferred Position", typeof(string));
            tbPreferredPosition.Columns.Add("Cadt #", typeof(string));
            tbPreferredPosition.Columns.Add("Interviewed Cadt #", typeof(string));
            tbPreferredPosition.Columns.Add("Hired #", typeof(string));
            tbPreferredPosition.Columns.Add("QualifiedButNotMatched #", typeof(string)); //Add by Yuanqin
            tbPreferredPosition.Columns.Add("Rejected #", typeof(string));
            tbPreferredPosition.Columns.Add("Decline Offer #", typeof(string));

            tbPreferredPosition.Rows.Add("Research Oriented", "0", "0", "0", "0", "0");
            tbPreferredPosition.Rows.Add("Engineering Oriented", "0", "0", "0", "0", "0");
            tbPreferredPosition.Rows.Add("Unknown", "0", "0", "0", "0", "0");
            tbPreferredPosition.Rows.Add("Grand Total", "0", "0", "0", "0", "0");
            Hashtable filterHT = new Hashtable();
            filterHT.Add("Research Oriented", "(PreferredPosition = " + ((int)PositionTypeEnum.ResearchIntern).ToString() + ")");
            filterHT.Add("Engineering Oriented", "(PreferredPosition = " + ((int)PositionTypeEnum.EngineeringIntern).ToString() + ")");
            filterHT.Add("Unknown", "(PreferredPosition IS NULL OR PreferredPosition = " + ((int)PositionTypeEnum.Unknown).ToString() + ")");

            //string strApplicationDateFilter = "(ApplicationDate >= '" + m_StartDate.ToString() + "' AND ApplicationDate <= '" + m_EndDate.AddDays(1).ToString() + "') ";
            string strFilter;
            string Key;
            #region //Cadt #
            for (int i = 0; i < tbPreferredPosition.Rows.Count - 1; i++)
            {
                strFilter = filterHT[tbPreferredPosition.Rows[i]["Preferred Position"].ToString()].ToString();
                Key = "PP_Cadt_" + i.ToString() + "_" + m_StartDate.ToShortDateString() + "_" + m_EndDate.ToShortDateString();
                if (SiteCache.Get(Key) == null)
                {
                    SiteCache.Insert(Key, strFilter, SiteCache.DefaultExpiration);
                }
                AllCandidates.RowFilter = strFilter;
                tbPreferredPosition.Rows[i]["Cadt #"] = String.Format("<a href='ShowCandidates.aspx?type=source&StartDate=" + m_StartDate.ToString("MM/dd/yyyy") + "&EndDate=" + m_EndDate.ToString("MM/dd/yyyy") + "&key=" + Key + "'>{0}</a>", AllCandidates.Count.ToString());
            }
            strFilter = "";
            Key = "SP_Cadt_GrandTotal_" + m_StartDate.ToShortDateString() + "_" + m_EndDate.ToShortDateString();
            if (SiteCache.Get(Key) == null)
            {
                SiteCache.Insert(Key, strFilter, SiteCache.DefaultExpiration);
            }
            AllCandidates.RowFilter = strFilter;
            tbPreferredPosition.Rows[tbPreferredPosition.Rows.Count - 1]["Cadt #"] = String.Format("<a href='ShowCandidates.aspx?type=source&StartDate=" + m_StartDate.ToString("MM/dd/yyyy") + "&EndDate=" + m_EndDate.ToString("MM/dd/yyyy") + "&key=" + Key + "'>{0}</a>", AllCandidates.Count.ToString());
            #endregion
            #region //Interviewed Cadt #
            for (int i = 0; i < tbPreferredPosition.Rows.Count - 1; i++)
            {
                strFilter = filterHT[tbPreferredPosition.Rows[i]["Preferred Position"].ToString()].ToString()
                    + " AND (InterviewStatus > -1)";
                Key = "PP_Interviewed_" + i.ToString() + "_" + m_StartDate.ToShortDateString() + "_" + m_EndDate.ToShortDateString();
                if (SiteCache.Get(Key) == null)
                {
                    SiteCache.Insert(Key, strFilter, SiteCache.DefaultExpiration);
                }
                AllCandidates.RowFilter = strFilter;
                tbPreferredPosition.Rows[i]["Interviewed Cadt #"] = String.Format("<a href='ShowCandidates.aspx?type=source&StartDate=" + m_StartDate.ToString("MM/dd/yyyy") + "&EndDate=" + m_EndDate.ToString("MM/dd/yyyy") + "&key=" + Key + "'>{0}</a>", AllCandidates.Count.ToString());
            }
            strFilter = " (InterviewStatus > -1)";
            Key = "SP_Interviewed_GrandTotal_" + m_StartDate.ToShortDateString() + "_" + m_EndDate.ToShortDateString();
            if (SiteCache.Get(Key) == null)
            {
                SiteCache.Insert(Key, strFilter, SiteCache.DefaultExpiration);
            }
            AllCandidates.RowFilter = strFilter;
            tbPreferredPosition.Rows[tbPreferredPosition.Rows.Count - 1]["Interviewed Cadt #"] = String.Format("<a href='ShowCandidates.aspx?type=source&StartDate=" + m_StartDate.ToString("MM/dd/yyyy") + "&EndDate=" + m_EndDate.ToString("MM/dd/yyyy") + "&key=" + Key + "'>{0}</a>", AllCandidates.Count.ToString());
            #endregion
            #region //Hired #
            for (int i = 0; i < tbPreferredPosition.Rows.Count - 1; i++)
            {
                strFilter = filterHT[tbPreferredPosition.Rows[i]["Preferred Position"].ToString()].ToString() + " AND (InterviewStatus = " + ((int)InterviewStatusEnum.Hired).ToString() + ")";
                Key = "PP_Hired_" + i.ToString() + "_" + m_StartDate.ToShortDateString() + "_" + m_EndDate.ToShortDateString();
                if (SiteCache.Get(Key) == null)
                {
                    SiteCache.Insert(Key, strFilter, SiteCache.DefaultExpiration);
                }
                AllCandidates.RowFilter = strFilter;
                tbPreferredPosition.Rows[i]["Hired #"] = String.Format("<a href='ShowCandidates.aspx?type=source&StartDate=" + m_StartDate.ToString("MM/dd/yyyy") + "&EndDate=" + m_EndDate.ToString("MM/dd/yyyy") + "&key=" + Key + "'>{0}</a>", AllCandidates.Count.ToString());
            }
            strFilter = "(InterviewStatus = " + ((int)InterviewStatusEnum.Hired).ToString() + ")";
            Key = "SP_Hired_GrandTotal_" + m_StartDate.ToShortDateString() + "_" + m_EndDate.ToShortDateString();
            if (SiteCache.Get(Key) == null)
            {
                SiteCache.Insert(Key, strFilter, SiteCache.DefaultExpiration);
            }
            AllCandidates.RowFilter = strFilter;
            tbPreferredPosition.Rows[tbPreferredPosition.Rows.Count - 1]["Hired #"] = String.Format("<a href='ShowCandidates.aspx?type=source&StartDate=" + m_StartDate.ToString("MM/dd/yyyy") + "&EndDate=" + m_EndDate.ToString("MM/dd/yyyy") + "&key=" + Key + "'>{0}</a>", AllCandidates.Count.ToString());
            #endregion

            /*
             * Add by Yuanqin,2011.5.5
             * For QualifiedButNotMatched
             */
            #region //QualifiedButNotMatched #
            for (int i = 0; i < tbPreferredPosition.Rows.Count - 1; i++)
            {
                strFilter = filterHT[tbPreferredPosition.Rows[i]["Preferred Position"].ToString()].ToString() + " AND (InterviewStatus = " + ((int)InterviewStatusEnum.QualifiedButNotMatched).ToString() + ")";
                Key = "PP_QualifiedButNotMatched_" + i.ToString() + "_" + m_StartDate.ToShortDateString() + "_" + m_EndDate.ToShortDateString();
                if (SiteCache.Get(Key) == null)
                {
                    SiteCache.Insert(Key, strFilter, SiteCache.DefaultExpiration);
                }
                AllCandidates.RowFilter = strFilter;
                tbPreferredPosition.Rows[i]["QualifiedButNotMatched #"] = String.Format("<a href='ShowCandidates.aspx?type=source&StartDate=" + m_StartDate.ToString("MM/dd/yyyy") + "&EndDate=" + m_EndDate.ToString("MM/dd/yyyy") + "&key=" + Key + "'>{0}</a>", AllCandidates.Count.ToString());
            }
            strFilter = " (InterviewStatus = " + ((int)InterviewStatusEnum.QualifiedButNotMatched).ToString() + ")";
            Key = "SP_QualifiedButNotMatched_GrandTotal_" + m_StartDate.ToShortDateString() + "_" + m_EndDate.ToShortDateString();
            if (SiteCache.Get(Key) == null)
            {
                SiteCache.Insert(Key, strFilter, SiteCache.DefaultExpiration);
            }
            AllCandidates.RowFilter = strFilter;
            tbPreferredPosition.Rows[tbPreferredPosition.Rows.Count - 1]["Rejected #"] = String.Format("<a href='ShowCandidates.aspx?type=source&StartDate=" + m_StartDate.ToString("MM/dd/yyyy") + "&EndDate=" + m_EndDate.ToString("MM/dd/yyyy") + "&key=" + Key + "'>{0}</a>", AllCandidates.Count.ToString());
            #endregion

            #region //Rejected #
            for (int i = 0; i < tbPreferredPosition.Rows.Count - 1; i++)
            {
                strFilter = filterHT[tbPreferredPosition.Rows[i]["Preferred Position"].ToString()].ToString() + " AND (InterviewStatus = " + ((int)InterviewStatusEnum.Rejected).ToString() + ")";
                Key = "PP_Rejected_" + i.ToString() + "_" + m_StartDate.ToShortDateString() + "_" + m_EndDate.ToShortDateString();
                if (SiteCache.Get(Key) == null)
                {
                    SiteCache.Insert(Key, strFilter, SiteCache.DefaultExpiration);
                }
                AllCandidates.RowFilter = strFilter;
                tbPreferredPosition.Rows[i]["Rejected #"] = String.Format("<a href='ShowCandidates.aspx?type=source&StartDate=" + m_StartDate.ToString("MM/dd/yyyy") + "&EndDate=" + m_EndDate.ToString("MM/dd/yyyy") + "&key=" + Key + "'>{0}</a>", AllCandidates.Count.ToString());
            }
            strFilter = " (InterviewStatus = " + ((int)InterviewStatusEnum.Rejected).ToString() + ")";
            Key = "SP_Rejected_GrandTotal_" + m_StartDate.ToShortDateString() + "_" + m_EndDate.ToShortDateString();
            if (SiteCache.Get(Key) == null)
            {
                SiteCache.Insert(Key, strFilter, SiteCache.DefaultExpiration);
            }
            AllCandidates.RowFilter = strFilter;
            tbPreferredPosition.Rows[tbPreferredPosition.Rows.Count - 1]["Rejected #"] = String.Format("<a href='ShowCandidates.aspx?type=source&StartDate=" + m_StartDate.ToString("MM/dd/yyyy") + "&EndDate=" + m_EndDate.ToString("MM/dd/yyyy") + "&key=" + Key + "'>{0}</a>", AllCandidates.Count.ToString());
            #endregion
            #region //Decline Offer #
            for (int i = 0; i < tbPreferredPosition.Rows.Count - 1; i++)
            {
                strFilter = filterHT[tbPreferredPosition.Rows[i]["Preferred Position"].ToString()].ToString() + " AND (InterviewStatus = " + ((int)InterviewStatusEnum.OfferDeclined).ToString() + ")";
                Key = "PP_DeclineOffer_" + i.ToString() + "_" + m_StartDate.ToShortDateString() + "_" + m_EndDate.ToShortDateString();
                if (SiteCache.Get(Key) == null)
                {
                    SiteCache.Insert(Key, strFilter, SiteCache.DefaultExpiration);
                }
                AllCandidates.RowFilter = strFilter;
                tbPreferredPosition.Rows[i]["Decline Offer #"] = String.Format("<a href='ShowCandidates.aspx?type=source&StartDate=" + m_StartDate.ToString("MM/dd/yyyy") + "&EndDate=" + m_EndDate.ToString("MM/dd/yyyy") + "&key=" + Key + "'>{0}</a>", AllCandidates.Count.ToString());
            }
            strFilter = "(InterviewStatus = " + ((int)InterviewStatusEnum.OfferDeclined).ToString() + ")";
            Key = "SP_DeclineOffer_GrandTotal_" + m_StartDate.ToShortDateString() + "_" + m_EndDate.ToShortDateString();
            if (SiteCache.Get(Key) == null)
            {
                SiteCache.Insert(Key, strFilter, SiteCache.DefaultExpiration);
            }
            AllCandidates.RowFilter = strFilter;
            tbPreferredPosition.Rows[tbPreferredPosition.Rows.Count - 1]["Decline Offer #"] = String.Format("<a href='ShowCandidates.aspx?type=source&StartDate=" + m_StartDate.ToString("MM/dd/yyyy") + "&EndDate=" + m_EndDate.ToString("MM/dd/yyyy") + "&key=" + Key + "'>{0}</a>", AllCandidates.Count.ToString());
            #endregion

            gvPreferredPosition.DataSource = tbPreferredPosition.DefaultView;
            gvPreferredPosition.DataBind();
        }

        public void btnExportExcel_Click(object sender, EventArgs e)
        {
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename= Sourcing Report - by Preferred Position" + DateTime.Now.ToString() + ".xls");
            Response.ContentType = "application/excel";
            Response.Charset = "utf-8";
            Response.Write("<meta http-equiv=Content-Type content=text/html;charset=utf-8>");
            Response.ContentEncoding = Encoding.UTF8;
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            gvPreferredPosition.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }

    }
}