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
using System.Collections.Generic;
using Springfield.Components;
using System.IO;
using System.Text;
using System.Data.SqlClient;

public partial class Controls_SRResearchGroup : System.Web.UI.UserControl
{
    private List<string> FullNameGroupList = StaticData.GroupList;
    private ArrayList ShortNameGroupList = CheckInFormResourceManager.GetTypeDisplayItems("Groups");
    private DataView AllCandidates;
    public DateTime m_StartDate, m_EndDate;

    protected void Page_Load(object sender, EventArgs e)
    {
        //InitDate();
        FullNameGroupList.Remove("Other");

        //测试用GetAllApplicants
        //string SQL = "SELECT sf_ApplicantRelatedInfo.InterestedGroup,  sf_vw_AllApplicants.*, sf_Referral.ReferralType,sf_interview.InterviewStatus FROM  ((sf_vw_AllApplicants inner join sf_ApplicantRelatedInfo ON sf_ApplicantRelatedInfo.ApplicantId = sf_vw_AllApplicants.ApplicantId) LEFT JOIN sf_Referral ON sf_vw_AllApplicants.ReferralId = sf_Referral.ReferralId) left join (select sf_interview.* from sf_interview,(select applicantid,max(startdate) as startdate from sf_interview group by applicantid)a where sf_interview.applicantid=a.applicantid and a.startdate=sf_interview.startdate)sf_interview on sf_vw_AllApplicants.applicantid=sf_interview.applicantid	ORDER BY ReferralType DESC, ApplicationDate DESC";
        //SqlConnection conn = new SqlConnection("Persist Security Info=False;Integrated Security=false;user id=compass;password=We're#1!;database=springfield;server=MSRA-SPFIELD;");
        //DataSet ds = new DataSet();
        //SqlDataAdapter da = new SqlDataAdapter(SQL, conn);
        //da.Fill(ds);
        //AllCandidates = new DataView(ds.Tables[0]);
        //实际代码
        AllCandidates = new DataView(CashedApplicantInfo.GetAllApplcantsforSourcingReport(m_StartDate, m_EndDate).Tables[0]);

        DataTable tbGroup = new DataTable();
        tbGroup.Columns.Add("Group", typeof(string));
        tbGroup.Columns.Add("Cadt #", typeof(string));
        foreach (string groupName in FullNameGroupList)
        {
            tbGroup.Rows.Add(groupName,  "0");
        }
        tbGroup.Rows.Add("Candidate Number", "0");

        //string strApplicationDateFilter = "(ApplicationDate >= '" + m_StartDate.ToString() + "' AND ApplicationDate <= '" + m_EndDate.AddDays(1).ToString() + "') ";
        string strFilter;
        string Key;

        for (int i = 0; i < FullNameGroupList.Count; i++)
        {
            strFilter =  " (InterestedGroup like '%" + tbGroup.Rows[i]["Group"].ToString() + "%')";
            Key = "Group_Cadt_" + i.ToString() + "_" + m_StartDate.ToShortDateString() + "_" + m_EndDate.ToShortDateString();
            if (SiteCache.Get(Key) == null)
            {
                SiteCache.Insert(Key, strFilter, SiteCache.DefaultExpiration);
            }
            AllCandidates.RowFilter = strFilter;
            tbGroup.Rows[i]["Cadt #"] = String.Format("<a href='ShowCandidates.aspx?type=source&StartDate=" + m_StartDate.ToString("MM/dd/yyyy") + "&EndDate=" + m_EndDate.ToString("MM/dd/yyyy") + "&key=" + Key + "'>{0}</a>", AllCandidates.Count.ToString());
        }
        strFilter = "";
        Key = "Group_Cadt_CandidateNumber_" + m_StartDate.ToShortDateString() + "_" + m_EndDate.ToShortDateString();
        if (SiteCache.Get(Key) == null)
        {
            SiteCache.Insert(Key, strFilter, SiteCache.DefaultExpiration);
        }
        AllCandidates.RowFilter = strFilter;
        tbGroup.Rows[tbGroup.Rows.Count - 1]["Cadt #"] = String.Format("<a href='ShowCandidates.aspx?type=source&StartDate=" + m_StartDate.ToString("MM/dd/yyyy") + "&EndDate=" + m_EndDate.ToString("MM/dd/yyyy") + "&key=" + Key + "'>{0}</a>", AllCandidates.Count.ToString());

        gvGroupCadt.DataSource = tbGroup.DefaultView;
        gvGroupCadt.DataBind();
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
        Response.AddHeader("content-disposition", "attachment; filename= Sourcing Report - by Group" + DateTime.Now.ToString() + ".xls");
        Response.ContentType = "application/excel";
        Response.Charset = "utf-8";
        Response.Write("<meta http-equiv=Content-Type content=text/html;charset=utf-8>");
        Response.ContentEncoding = Encoding.UTF8;
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        gvGroupCadt.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.End();
    }
}
