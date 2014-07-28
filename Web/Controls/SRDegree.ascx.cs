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
using Springfield.Components;
using System.Text;
using System.IO;
using System.Data.SqlClient;

public partial class Controls_SRDegree : System.Web.UI.UserControl
{
    private DataView AllCandidates;
    public DateTime m_StartDate, m_EndDate;

    protected void Page_Load(object sender, EventArgs e)
    {
        //InitDate();
        //测试用GetAllApplicants
        //string SQL = "SELECT sf_ApplicantRelatedInfo.SpecialProgram,  sf_vw_AllApplicants.*, sf_Referral.ReferralType,sf_interview.InterviewStatus FROM  ((sf_vw_AllApplicants inner join sf_ApplicantRelatedInfo ON sf_ApplicantRelatedInfo.ApplicantId = sf_vw_AllApplicants.ApplicantId) LEFT JOIN sf_Referral ON sf_vw_AllApplicants.ReferralId = sf_Referral.ReferralId) left join (select sf_interview.* from sf_interview,(select applicantid,max(startdate) as startdate from sf_interview group by applicantid)a where sf_interview.applicantid=a.applicantid and a.startdate=sf_interview.startdate)sf_interview on sf_vw_AllApplicants.applicantid=sf_interview.applicantid	ORDER BY ReferralType DESC, ApplicationDate DESC";
        //SqlConnection conn = new SqlConnection("Persist Security Info=False;Integrated Security=false;user id=compass;password=We're#1!;database=springfield;server=MSRA-SPFIELD;");
        //DataSet ds = new DataSet();
        //SqlDataAdapter da = new SqlDataAdapter(SQL, conn);
        //da.Fill(ds);
        //AllCandidates = new DataView(ds.Tables[0]);
        //实际代码
        AllCandidates = new DataView(CashedApplicantInfo.GetAllApplcantsforSourcingReport(m_StartDate, m_EndDate).Tables[0]);

        DataTable TBDegree = new DataTable();
        TBDegree.Columns.Add("Degree", typeof(string));
        TBDegree.Columns.Add("Cadt #", typeof(string));
        TBDegree.Columns.Add("Interviewed Cadt #", typeof(string));
        TBDegree.Columns.Add("Hired #", typeof(string));
        TBDegree.Columns.Add("Rejected #", typeof(string));
        TBDegree.Columns.Add("Decline Offer #", typeof(string));

        for (int i = 0; i < StaticData.DegreeList.Count; i++)
        {
            TBDegree.Rows.Add(StaticData.DegreeList[i], "0", "0", "0", "0", "0");
        }
        TBDegree.Rows.Add("Grand Total", "0", "0", "0", "0", "0");

        //string strApplicationDateFilter = "(ApplicationDate >= '" + m_StartDate.ToString() + "' AND ApplicationDate <= '" + m_EndDate.AddDays(1).ToString() + "') AND";
        string strFilter;
        string Key;

        #region //Cadt #
        for (int i = 0; i < TBDegree.Rows.Count - 1; i++)
        {
            strFilter = "(Degree = " + i.ToString() + ")";
            Key = "Degree_Cadt_" + i.ToString() + "_" + m_StartDate.ToShortDateString() + "_" + m_EndDate.ToShortDateString();
            if (SiteCache.Get(Key) == null)
            {
                SiteCache.Insert(Key, strFilter, SiteCache.DefaultExpiration);
            }
            AllCandidates.RowFilter = strFilter;
            TBDegree.Rows[i]["Cadt #"] = String.Format("<a href='ShowCandidates.aspx?type=source&StartDate=" + m_StartDate.ToString("MM/dd/yyyy") + "&EndDate=" + m_EndDate.ToString("MM/dd/yyyy") + "&key=" + Key + "'>{0}</a>", AllCandidates.Count.ToString());
        }
        strFilter = "";
        Key = "Degree_Cadt_Total_" + m_StartDate.ToShortDateString() + "_" + m_EndDate.ToShortDateString();
        if (SiteCache.Get(Key) == null)
        {
            SiteCache.Insert(Key, strFilter, SiteCache.DefaultExpiration);
        }
        AllCandidates.RowFilter = strFilter;
        TBDegree.Rows[TBDegree.Rows.Count - 1]["Cadt #"] = String.Format("<a href='ShowCandidates.aspx?type=source&StartDate=" + m_StartDate.ToString("MM/dd/yyyy") + "&EndDate=" + m_EndDate.ToString("MM/dd/yyyy") + "&key=" + Key + "'>{0}</a>", AllCandidates.Count.ToString());
        #endregion
        #region //Interviewed Cadt #
        for (int i = 0; i < TBDegree.Rows.Count - 1; i++)
        {
            strFilter = "(Degree = " + i.ToString() + " AND InterviewStatus > -1)";
            Key = "Degree_Interviewed_" + i.ToString() + "_" + m_StartDate.ToShortDateString() + "_" + m_EndDate.ToShortDateString();
            if (SiteCache.Get(Key) == null)
            {
                SiteCache.Insert(Key, strFilter, SiteCache.DefaultExpiration);
            }
            AllCandidates.RowFilter = strFilter;
            TBDegree.Rows[i]["Interviewed Cadt #"] = String.Format("<a href='ShowCandidates.aspx?type=source&StartDate=" + m_StartDate.ToString("MM/dd/yyyy") + "&EndDate=" + m_EndDate.ToString("MM/dd/yyyy") + "&key=" + Key + "'>{0}</a>", AllCandidates.Count.ToString());
        }
        strFilter = "(InterviewStatus > -1)";
        Key = "Degree_Interviewed_Total_" + m_StartDate.ToShortDateString() + "_" + m_EndDate.ToShortDateString();
        if (SiteCache.Get(Key) == null)
        {
            SiteCache.Insert(Key, strFilter, SiteCache.DefaultExpiration);
        }
        AllCandidates.RowFilter = strFilter;
        TBDegree.Rows[TBDegree.Rows.Count - 1]["Interviewed Cadt #"] = String.Format("<a href='ShowCandidates.aspx?type=source&StartDate=" + m_StartDate.ToString("MM/dd/yyyy") + "&EndDate=" + m_EndDate.ToString("MM/dd/yyyy") + "&key=" + Key + "'>{0}</a>", AllCandidates.Count.ToString());

        #endregion
        #region //Hired #
        for (int i = 0; i < TBDegree.Rows.Count - 1; i++)
        {
            strFilter = "(Degree = " + i.ToString() + "AND InterviewStatus = " + ((int)InterviewStatusEnum.Hired).ToString() + ")";
            Key = "Degree_Hired_" + i.ToString() + "_" + m_StartDate.ToShortDateString() + "_" + m_EndDate.ToShortDateString();
            if (SiteCache.Get(Key) == null)
            {
                SiteCache.Insert(Key, strFilter, SiteCache.DefaultExpiration);
            }
            AllCandidates.RowFilter = strFilter;
            TBDegree.Rows[i]["Hired #"] = String.Format("<a href='ShowCandidates.aspx?type=source&StartDate=" + m_StartDate.ToString("MM/dd/yyyy") + "&EndDate=" + m_EndDate.ToString("MM/dd/yyyy") + "&key=" + Key + "'>{0}</a>", AllCandidates.Count.ToString());
        }
        strFilter = "(InterviewStatus = " + ((int)InterviewStatusEnum.Hired).ToString() + ")";
        Key = "Degree_Hired_Total_" + m_StartDate.ToShortDateString() + "_" + m_EndDate.ToShortDateString();
        if (SiteCache.Get(Key) == null)
        {
            SiteCache.Insert(Key, strFilter, SiteCache.DefaultExpiration);
        }
        AllCandidates.RowFilter = strFilter;
        TBDegree.Rows[TBDegree.Rows.Count - 1]["Hired #"] = String.Format("<a href='ShowCandidates.aspx?type=source&StartDate=" + m_StartDate.ToString("MM/dd/yyyy") + "&EndDate=" + m_EndDate.ToString("MM/dd/yyyy") + "&key=" + Key + "'>{0}</a>", AllCandidates.Count.ToString());

        #endregion
        #region //Rejected #
        for (int i = 0; i < TBDegree.Rows.Count - 1; i++)
        {
            strFilter = "(Degree = " + i.ToString() + "AND InterviewStatus = " + ((int)InterviewStatusEnum.Rejected).ToString() + ")";
            Key = "Degree_Rejected_" + i.ToString() + "_" + m_StartDate.ToShortDateString() + "_" + m_EndDate.ToShortDateString();
            if (SiteCache.Get(Key) == null)
            {
                SiteCache.Insert(Key, strFilter, SiteCache.DefaultExpiration);
            }
            AllCandidates.RowFilter = strFilter;
            TBDegree.Rows[i]["Rejected #"] = String.Format("<a href='ShowCandidates.aspx?type=source&StartDate=" + m_StartDate.ToString("MM/dd/yyyy") + "&EndDate=" + m_EndDate.ToString("MM/dd/yyyy") + "&key=" + Key + "'>{0}</a>", AllCandidates.Count.ToString());
        }
        strFilter = "(InterviewStatus = " + ((int)InterviewStatusEnum.Rejected).ToString() + ")";
        Key = "Degree_Rejected_Total_" + m_StartDate.ToShortDateString() + "_" + m_EndDate.ToShortDateString();
        if (SiteCache.Get(Key) == null)
        {
            SiteCache.Insert(Key, strFilter, SiteCache.DefaultExpiration);
        }
        AllCandidates.RowFilter = strFilter;
        TBDegree.Rows[TBDegree.Rows.Count - 1]["Rejected #"] = String.Format("<a href='ShowCandidates.aspx?type=source&StartDate=" + m_StartDate.ToString("MM/dd/yyyy") + "&EndDate=" + m_EndDate.ToString("MM/dd/yyyy") + "&key=" + Key + "'>{0}</a>", AllCandidates.Count.ToString());
        #endregion
        #region //Decline Offer #
        for (int i = 0; i < TBDegree.Rows.Count - 1; i++)
        {
            strFilter = "(Degree = " + i.ToString() + "AND InterviewStatus = " + ((int)InterviewStatusEnum.OfferDeclined).ToString() + ")";
            Key = "Degree_OfferDeclined_" + i.ToString() + "_" + m_StartDate.ToShortDateString() + "_" + m_EndDate.ToShortDateString();
            if (SiteCache.Get(Key) == null)
            {
                SiteCache.Insert(Key, strFilter, SiteCache.DefaultExpiration);
            }
            AllCandidates.RowFilter = strFilter;
            TBDegree.Rows[i]["Decline Offer #"] = String.Format("<a href='ShowCandidates.aspx?type=source&StartDate=" + m_StartDate.ToString("MM/dd/yyyy") + "&EndDate=" + m_EndDate.ToString("MM/dd/yyyy") + "&key=" + Key + "'>{0}</a>", AllCandidates.Count.ToString());
        }
        strFilter = "(InterviewStatus = " + ((int)InterviewStatusEnum.OfferDeclined).ToString() + ")";
        Key = "Degree_OfferDeclined_Total_" + m_StartDate.ToShortDateString() + "_" + m_EndDate.ToShortDateString();
        if (SiteCache.Get(Key) == null)
        {
            SiteCache.Insert(Key, strFilter, SiteCache.DefaultExpiration);
        }
        AllCandidates.RowFilter = strFilter;
        TBDegree.Rows[TBDegree.Rows.Count - 1]["Decline Offer #"] = String.Format("<a href='ShowCandidates.aspx?type=source&StartDate=" + m_StartDate.ToString("MM/dd/yyyy") + "&EndDate=" + m_EndDate.ToString("MM/dd/yyyy") + "&key=" + Key + "'>{0}</a>", AllCandidates.Count.ToString());
        #endregion

        gvDegree.DataSource = TBDegree.DefaultView;
        gvDegree.DataBind();
    }
    //private void InitDate()
    //{
    //    if (Request["StartDate"] != null && Request["EndDate"] != null)
    //    {
    //        try
    //        {
    //            m_StartDate = Convert.ToDateTime(Request["StartDate"]);
    //            m_EndDate = Convert.ToDateTime(Request["EndDate"]);
    //        }
    //        catch
    //        {
    //            m_StartDate = DateTime.Now.AddMonths(-1);// Convert.ToDateTime("1/1/1753 12:00:00");
    //            m_EndDate = DateTime.Now;// DateTime.MaxValue.AddDays(-1);
    //        }
    //    }
    //    else
    //    {
    //        m_StartDate = DateTime.Now.AddMonths(-1);// Convert.ToDateTime("1/1/1753 12:00:00");
    //        m_EndDate = DateTime.Now;// DateTime.MaxValue.AddDays(-1);
    //    }
    //    lbDateSpan.Text = "Date From " + m_StartDate.ToString("MM/dd/yyyy") + " To " + m_EndDate.ToString("MM/dd/yyyy");
    //}
    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        Response.ClearContent();
        Response.AddHeader("content-disposition", "attachment; filename=Sourcing Report - by Degree" + DateTime.Now.ToString() + ".xls");
        Response.ContentType = "application/excel";
        Response.Charset = "utf-8";
        Response.Write("<meta http-equiv=Content-Type content=text/html;charset=utf-8>");
        Response.ContentEncoding = Encoding.UTF8;
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        gvDegree.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.End();
    }
}
