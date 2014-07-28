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
using System.Data;
using Springfield.Components;
using System.Collections;
using System.Data.SqlClient;
using System.IO;
using System.Text;

public partial class Controls_SRSpecialProgram : System.Web.UI.UserControl
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

        DataTable gvSourceTB = new DataTable();
        gvSourceTB.Columns.Add("Special Program", typeof(string));
        gvSourceTB.Columns.Add("Cadt #", typeof(string));
        gvSourceTB.Columns.Add("Interviewed Cadt #", typeof(string));
        gvSourceTB.Columns.Add("Hired #", typeof(string));
        gvSourceTB.Columns.Add("Rejected #", typeof(string));
        gvSourceTB.Columns.Add("Decline Offer #", typeof(string));

        gvSourceTB.Rows.Add("Microsoft Jointlab Program", "0", "0", "0", "0", "0");
        gvSourceTB.Rows.Add("Microsoft Internship Program", "0", "0", "0", "0", "0");
        gvSourceTB.Rows.Add("MS Young Fellowship Program", "0", "0", "0", "0", "0");
        gvSourceTB.Rows.Add("MS Fellowship Program", "0", "0", "0", "0", "0");
        gvSourceTB.Rows.Add("MSRA Student Exchange Program", "0", "0", "0", "0", "0");
        gvSourceTB.Rows.Add("MSRA Undergraduate Research Prog", "0", "0", "0", "0", "0");
        gvSourceTB.Rows.Add("IJARC Program", "0", "0", "0", "0", "0");
        gvSourceTB.Rows.Add("MS PhD Program", "0", "0", "0", "0", "0");
        gvSourceTB.Rows.Add("MSRA Fuji Program", "0", "0", "0", "0", "0");
        gvSourceTB.Rows.Add("None", "0", "0", "0", "0", "0");
        gvSourceTB.Rows.Add("Candidate Number", "0", "0", "0", "0", "0");

        //string strApplicationDateFilter = "(ApplicationDate >= '" + m_StartDate.ToString() + "' AND ApplicationDate <= '" + m_EndDate.AddDays(1).ToString() +"') AND";
        string strFilter;
        string Key;
        #region //Cadt #
        for (int i = 0; i < gvSourceTB.Rows.Count - 2; i++)
        {
            strFilter = "(SpecialProgram like '%" + gvSourceTB.Rows[i]["Special Program"] + "%')";
            Key = "SP_Cadt_" + i.ToString() + "_" + m_StartDate.ToShortDateString() + "_" + m_EndDate.ToShortDateString();
            if (SiteCache.Get(Key) == null)
            {
                SiteCache.Insert(Key, strFilter, SiteCache.DefaultExpiration);
            }
            AllCandidates.RowFilter = strFilter;
            gvSourceTB.Rows[i]["Cadt #"] = String.Format("<a href='ShowCandidates.aspx?type=source&StartDate=" + m_StartDate.ToString("MM/dd/yyyy") + "&EndDate=" + m_EndDate.ToString("MM/dd/yyyy") + "&key=" + Key + "'>{0}</a>", AllCandidates.Count.ToString());
        }
        strFilter = "(SpecialProgram is NULL OR SpecialProgram = '' OR SpecialProgram = 'None of the above;' )";
        Key = "SP_Cadt_None_" + m_StartDate.ToShortDateString() + "_" + m_EndDate.ToShortDateString();
        if (SiteCache.Get(Key) == null)
        {
            SiteCache.Insert(Key, strFilter, SiteCache.DefaultExpiration);
        }
        AllCandidates.RowFilter = strFilter;
        gvSourceTB.Rows[gvSourceTB.Rows.Count - 2]["Cadt #"] = String.Format("<a href='ShowCandidates.aspx?type=source&StartDate=" + m_StartDate.ToString("MM/dd/yyyy") + "&EndDate=" + m_EndDate.ToString("MM/dd/yyyy") + "&key=" + Key + "'>{0}</a>", AllCandidates.Count.ToString());

        strFilter = "";
        Key = "SP_Cadt_CandidateNum_" + m_StartDate.ToShortDateString() + "_" + m_EndDate.ToShortDateString();
        if (SiteCache.Get(Key) == null)
        {
            SiteCache.Insert(Key, strFilter, SiteCache.DefaultExpiration);
        }
        AllCandidates.RowFilter = strFilter;
        gvSourceTB.Rows[gvSourceTB.Rows.Count - 1]["Cadt #"] = String.Format("<a href='ShowCandidates.aspx?type=source&StartDate=" + m_StartDate.ToString("MM/dd/yyyy") + "&EndDate=" + m_EndDate.ToString("MM/dd/yyyy") + "&key=" + Key + "'>{0}</a>", AllCandidates.Count.ToString());
        #endregion

        #region //Interviewed Cadt #
        for (int i = 0; i < gvSourceTB.Rows.Count - 2; i++)
        {
            strFilter = "(SpecialProgram like '%" + gvSourceTB.Rows[i]["Special Program"] + "%' AND InterviewStatus > -1)";
            Key = "SP_Interviewed_" + i.ToString() + "_" + m_StartDate.ToShortDateString() + "_" + m_EndDate.ToShortDateString();
            if (SiteCache.Get(Key) == null)
            {
                SiteCache.Insert(Key, strFilter, SiteCache.DefaultExpiration);
            }
            AllCandidates.RowFilter = strFilter;
            gvSourceTB.Rows[i]["Interviewed Cadt #"] = String.Format("<a href='ShowCandidates.aspx?type=source&StartDate=" + m_StartDate.ToString("MM/dd/yyyy") + "&EndDate=" + m_EndDate.ToString("MM/dd/yyyy") + "&key=" + Key + "'>{0}</a>", AllCandidates.Count.ToString());
        }
        strFilter = "((SpecialProgram is NULL OR SpecialProgram = '' OR SpecialProgram = 'None of the above;') AND InterviewStatus > -1)";
        Key = "SP_Interviewed_None_" + m_StartDate.ToShortDateString() + "_" + m_EndDate.ToShortDateString();
        if (SiteCache.Get(Key) == null)
        {
            SiteCache.Insert(Key, strFilter, SiteCache.DefaultExpiration);
        }
        AllCandidates.RowFilter = strFilter;
        gvSourceTB.Rows[gvSourceTB.Rows.Count - 2]["Interviewed Cadt #"] = String.Format("<a href='ShowCandidates.aspx?type=source&StartDate=" + m_StartDate.ToString("MM/dd/yyyy") + "&EndDate=" + m_EndDate.ToString("MM/dd/yyyy") + "&key=" + Key + "'>{0}</a>", AllCandidates.Count.ToString());

        strFilter = "(InterviewStatus > -1)";
        Key = "SP_Interviewed_CandidateNum_" + m_StartDate.ToShortDateString() + "_" + m_EndDate.ToShortDateString();
        if (SiteCache.Get(Key) == null)
        {
            SiteCache.Insert(Key, strFilter, SiteCache.DefaultExpiration);
        }
        AllCandidates.RowFilter = strFilter;
        gvSourceTB.Rows[gvSourceTB.Rows.Count - 1]["Interviewed Cadt #"] = String.Format("<a href='ShowCandidates.aspx?type=source&StartDate=" + m_StartDate.ToString("MM/dd/yyyy") + "&EndDate=" + m_EndDate.ToString("MM/dd/yyyy") + "&key=" + Key + "'>{0}</a>", AllCandidates.Count.ToString());
        #endregion

        #region //Hired #
        for (int i = 0; i < gvSourceTB.Rows.Count - 2; i++)
        {
            strFilter = "(SpecialProgram like '%" + gvSourceTB.Rows[i]["Special Program"] + "%' AND InterviewStatus = " + ((int)InterviewStatusEnum.Hired).ToString() + ")";
            Key = "SP_Hired_" + i.ToString() + "_" + m_StartDate.ToShortDateString() + "_" + m_EndDate.ToShortDateString();
            if (SiteCache.Get(Key) == null)
            {
                SiteCache.Insert(Key, strFilter, SiteCache.DefaultExpiration);
            }
            AllCandidates.RowFilter = strFilter;
            gvSourceTB.Rows[i]["Hired #"] = String.Format("<a href='ShowCandidates.aspx?type=source&StartDate=" + m_StartDate.ToString("MM/dd/yyyy") + "&EndDate=" + m_EndDate.ToString("MM/dd/yyyy") + "&key=" + Key + "'>{0}</a>", AllCandidates.Count.ToString());
        }
        strFilter = "((SpecialProgram is NULL OR SpecialProgram = '' OR SpecialProgram = 'None of the above;') AND InterviewStatus = " + ((int)InterviewStatusEnum.Hired).ToString() + ")";
        Key = "SP_Hired_None_" + m_StartDate.ToShortDateString() + "_" + m_EndDate.ToShortDateString();
        if (SiteCache.Get(Key) == null)
        {
            SiteCache.Insert(Key, strFilter, SiteCache.DefaultExpiration);
        }
        AllCandidates.RowFilter = strFilter;
        gvSourceTB.Rows[gvSourceTB.Rows.Count - 2]["Hired #"] = String.Format("<a href='ShowCandidates.aspx?type=source&StartDate=" + m_StartDate.ToString("MM/dd/yyyy") + "&EndDate=" + m_EndDate.ToString("MM/dd/yyyy") + "&key=" + Key + "'>{0}</a>", AllCandidates.Count.ToString());

        strFilter = "(InterviewStatus = " + ((int)InterviewStatusEnum.Hired).ToString() + ")";
        Key = "SP_Hired_CandidateNum_" + m_StartDate.ToShortDateString() + "_" + m_EndDate.ToShortDateString();
        if (SiteCache.Get(Key) == null)
        {
            SiteCache.Insert(Key, strFilter, SiteCache.DefaultExpiration);
        }
        AllCandidates.RowFilter = strFilter;
        gvSourceTB.Rows[gvSourceTB.Rows.Count - 1]["Hired #"] = String.Format("<a href='ShowCandidates.aspx?type=source&StartDate=" + m_StartDate.ToString("MM/dd/yyyy") + "&EndDate=" + m_EndDate.ToString("MM/dd/yyyy") + "&key=" + Key + "'>{0}</a>", AllCandidates.Count.ToString());
        #endregion

        #region //Rejected #
        for (int i = 0; i < gvSourceTB.Rows.Count - 2; i++)
        {
            strFilter = "(SpecialProgram like '%" + gvSourceTB.Rows[i]["Special Program"] + "%' AND InterviewStatus = " + ((int)InterviewStatusEnum.Rejected).ToString() + ")";
            Key = "SP_Rejected_" + i.ToString() + "_" + m_StartDate.ToShortDateString() + "_" + m_EndDate.ToShortDateString();
            if (SiteCache.Get(Key) == null)
            {
                SiteCache.Insert(Key, strFilter, SiteCache.DefaultExpiration);
            }
            AllCandidates.RowFilter = strFilter;
            gvSourceTB.Rows[i]["Rejected #"] = String.Format("<a href='ShowCandidates.aspx?type=source&StartDate=" + m_StartDate.ToString("MM/dd/yyyy") + "&EndDate=" + m_EndDate.ToString("MM/dd/yyyy") + "&key=" + Key + "'>{0}</a>", AllCandidates.Count.ToString());
        }
        strFilter = "((SpecialProgram is NULL OR SpecialProgram = '' OR SpecialProgram = 'None of the above;') AND InterviewStatus = " + ((int)InterviewStatusEnum.Rejected).ToString() + ")";
        Key = "SP_Rejected_None_" + m_StartDate.ToShortDateString() + "_" + m_EndDate.ToShortDateString();
        if (SiteCache.Get(Key) == null)
        {
            SiteCache.Insert(Key, strFilter, SiteCache.DefaultExpiration);
        }
        AllCandidates.RowFilter = strFilter;
        gvSourceTB.Rows[gvSourceTB.Rows.Count - 2]["Rejected #"] = String.Format("<a href='ShowCandidates.aspx?type=source&StartDate=" + m_StartDate.ToString("MM/dd/yyyy") + "&EndDate=" + m_EndDate.ToString("MM/dd/yyyy") + "&key=" + Key + "'>{0}</a>", AllCandidates.Count.ToString());

        strFilter = "(InterviewStatus = " + ((int)InterviewStatusEnum.Rejected).ToString() + ")";
        Key = "SP_Rejected_CandidateNum_" + m_StartDate.ToShortDateString() + "_" + m_EndDate.ToShortDateString();
        if (SiteCache.Get(Key) == null)
        {
            SiteCache.Insert(Key, strFilter, SiteCache.DefaultExpiration);
        }
        AllCandidates.RowFilter = strFilter;
        gvSourceTB.Rows[gvSourceTB.Rows.Count - 1]["Rejected #"] = String.Format("<a href='ShowCandidates.aspx?type=source&StartDate=" + m_StartDate.ToString("MM/dd/yyyy") + "&EndDate=" + m_EndDate.ToString("MM/dd/yyyy") + "&key=" + Key + "'>{0}</a>", AllCandidates.Count.ToString());
        #endregion

        #region //Decline Offer #
        for (int i = 0; i < gvSourceTB.Rows.Count - 2; i++)
        {
            strFilter = "(SpecialProgram like '%" + gvSourceTB.Rows[i]["Special Program"] + "%' AND InterviewStatus = " + ((int)InterviewStatusEnum.OfferDeclined).ToString() + ")";
            Key = "SP_DeclineOffer_" + i.ToString() + "_" + m_StartDate.ToShortDateString() + "_" + m_EndDate.ToShortDateString();
            if (SiteCache.Get(Key) == null)
            {
                SiteCache.Insert(Key, strFilter, SiteCache.DefaultExpiration);
            }
            AllCandidates.RowFilter = strFilter;
            gvSourceTB.Rows[i]["Decline Offer #"] = String.Format("<a href='ShowCandidates.aspx?type=source&StartDate=" + m_StartDate.ToString("MM/dd/yyyy") + "&EndDate=" + m_EndDate.ToString("MM/dd/yyyy") + "&key=" + Key + "'>{0}</a>", AllCandidates.Count.ToString());
        }
        strFilter = "((SpecialProgram is NULL OR SpecialProgram = '' OR SpecialProgram = 'None of the above;') AND InterviewStatus = " + ((int)InterviewStatusEnum.OfferDeclined).ToString() + ")";
        Key = "SP_DeclineOffer_None" + m_StartDate.ToShortDateString() + "_" + m_EndDate.ToShortDateString();
        if (SiteCache.Get(Key) == null)
        {
            SiteCache.Insert(Key, strFilter, SiteCache.DefaultExpiration);
        }
        AllCandidates.RowFilter = strFilter;
        gvSourceTB.Rows[gvSourceTB.Rows.Count - 2]["Decline Offer #"] = String.Format("<a href='ShowCandidates.aspx?type=source&StartDate=" + m_StartDate.ToString("MM/dd/yyyy") + "&EndDate=" + m_EndDate.ToString("MM/dd/yyyy") + "&key=" + Key + "'>{0}</a>", AllCandidates.Count.ToString());
        
        strFilter = "(InterviewStatus = " + ((int)InterviewStatusEnum.OfferDeclined).ToString() + ")";
        Key = "SP_DeclineOffer_CandidateNum" + m_StartDate.ToShortDateString() + "_" + m_EndDate.ToShortDateString();
        if (SiteCache.Get(Key) == null)
        {
            SiteCache.Insert(Key, strFilter, SiteCache.DefaultExpiration);
        }
        AllCandidates.RowFilter = strFilter;
        gvSourceTB.Rows[gvSourceTB.Rows.Count - 1]["Decline Offer #"] = String.Format("<a href='ShowCandidates.aspx?type=source&StartDate=" + m_StartDate.ToString("MM/dd/yyyy") + "&EndDate=" + m_EndDate.ToString("MM/dd/yyyy") + "&key=" + Key + "'>{0}</a>", AllCandidates.Count.ToString());
        #endregion

        gvSpecialProgram.DataSource = gvSourceTB.DefaultView;
        gvSpecialProgram.DataBind();
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
        Response.AddHeader("content-disposition", "attachment; filename= Sourcing Report - by Special Program" + DateTime.Now.ToString() + ".xls");
        Response.ContentType = "application/excel";
        Response.Charset = "utf-8";
        Response.Write("<meta http-equiv=Content-Type content=text/html;charset=utf-8>");
        Response.ContentEncoding = Encoding.UTF8;//.GetEncoding("UTF-8");
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        gvSpecialProgram.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.End();
    }



}
