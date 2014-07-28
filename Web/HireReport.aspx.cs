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
using System.Collections.Generic;

public class HireItem
{
    private DateTime m_StartDate, m_EndDate;
    private string m_DefaultFilter, m_DefaultKey;

    public HireItem(DateTime startDate, DateTime endDate)
    {
        m_StartDate = startDate;
        m_EndDate = endDate;
        m_DefaultFilter = "StartDate >= #" + m_StartDate.ToString("MM/dd/yyyy") + "# AND StartDate <= #" + m_EndDate.AddDays(1).ToString("MM/dd/yyyy") + "# ";
        m_DefaultKey = string.Format("hire_report_{0}_{1}_", m_StartDate.ToString("yyyyMMdd"), m_EndDate.AddDays(1).ToString("yyyyMMdd"));
    }

    public string GenerateHireReport()
    {
        StringBuilder sb = new StringBuilder();
        DataTable hiringManagers = Interview.GetDurationInterviewedSiteUser(m_StartDate, m_EndDate).Tables[0];
        Guid curId;
        DataSet curInterviews;
        string curSiteUser;
        DataView dv;
        if (hiringManagers.Rows.Count > 0)
        {
            foreach (DataRow row in hiringManagers.Rows)
            {
                curId = (Guid)row["HiringManagerId"];
                curSiteUser = SiteUser.GetAliasByUserId(curId);
                sb.AppendLine("<tr>");
                sb.AppendLine(string.Format("<td>{0}</td>", curSiteUser));
                //curInterviews = Interview.GetInterviewForSiteUser(curId);
                curInterviews = Interview.GetDurationInterview(m_StartDate, m_EndDate.AddDays(1));
                dv = new DataView(curInterviews.Tables[0]);
                sb.AppendLine(GenerateInterviewCount(dv, curId));
                sb.AppendLine(GenerateHireCount(dv, curId));
                sb.AppendLine(GenerateRejectCount(dv, curId));
                sb.AppendLine(GenerateDeclineOfferCount(dv, curId));
                sb.AppendLine("</tr>");
            }
        }
        else
        {
            sb.AppendLine("<tr><td colspan='5'>There is no interview during this period!</td><tr>");
        }
        //generate total
        return sb.ToString();
    }

    private string GenerateCount(DataView dv, Guid curId, string filter, string key)
    {
        string rowFilter = m_DefaultFilter + filter;
        dv.RowFilter = rowFilter;
        string cacheKey = m_DefaultKey + key;
        if (SiteCache.Get(cacheKey) == null)
        {
            SiteCache.Insert(cacheKey, rowFilter);
        }
        string count = String.Format("<td><a href='ShowCandidates.aspx?type=hire&StartDate={0}&EndDate={1}&key={2}&id={3}'>{4}</a></td>", m_StartDate.ToString("MM/dd/yyyy"), m_EndDate.ToString("MM/dd/yyyy"), cacheKey, curId.ToString(), dv.Count.ToString());
        return count;
    }

    private string GenerateHireCount(DataView dv, Guid id)
    {
        string filter = "AND InterviewStatus =" + EnumHelper.EnumToInteger(InterviewStatusEnum.Hired) + " AND HiringManagerId = '" + id.ToString() + "'";
        return GenerateCount(dv, id, filter, id.ToString() + "_hire");
    }

    private string GenerateInterviewCount(DataView dv, Guid id)
    {
        string filter = "AND HiringManagerId = '" + id.ToString() + "'";
        return GenerateCount(dv, id, filter, id.ToString() + "_interview");
    }

    private string GenerateRejectCount(DataView dv, Guid id)
    {
        string filter = "AND InterviewStatus = " + EnumHelper.EnumToInteger(InterviewStatusEnum.Rejected) + " AND HiringManagerId = '" + id.ToString() + "'";
        return GenerateCount(dv, id, filter, id.ToString() + "_reject");
    }

    private string GenerateDeclineOfferCount(DataView dv, Guid id)
    {
        string filter = "AND InterviewStatus = " + EnumHelper.EnumToInteger(InterviewStatusEnum.OfferDeclined) + " AND HiringManagerId = '" + id.ToString() + "'";
        return GenerateCount(dv, id, filter, id.ToString() + "_decline");
    }

    public string HireReport
    {
        get {
            return GenerateHireReport();    
        }
    }

    public string Month
    {
        get {
            return m_StartDate.ToString("MMM.yyyy");
        }
    }
}

public partial class HireReport : System.Web.UI.Page
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
                dlHireTable.DataSource = GenerateHireList(3);
                dlHireTable.DataBind();
            }
            else
            {
                List<HireItem> list = new List<HireItem>();
                list.Add(new HireItem(m_StartDate, m_EndDate));
                dlHireTable.DataSource = list;
                dlHireTable.DataBind();
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
                //m_EndDate.AddDays(1);
            }
            catch
            {
                return true;
            }
            return false;
        }
        return true;
    }

    private List<HireItem> GenerateHireList(int count)
    {
        List<HireItem> list = new List<HireItem>();
        int curMonth = DateTime.Now.Month;
        int curYear = DateTime.Now.Year;
        for (int i = 0; i < count; i++)
        {
            DateTime startDate = new DateTime(curYear, curMonth - i, 1);
            DateTime endDate = new DateTime(curYear, curMonth - i, DateTime.DaysInMonth(curYear, curMonth - i));
            list.Add(new HireItem(startDate, endDate));
        }
        return list;
    }
}
