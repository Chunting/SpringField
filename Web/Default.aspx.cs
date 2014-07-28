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

public partial class GeneralInfo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (SiteUser.Current.IsInRole(RoleType.InternRecruiter))
        {
            pnlIRInfo.Visible = true;
        }

        if (!IsPostBack)
        {
            SiteGeneralInfo myInfo = SiteGeneralInfo.GetGeneralInfo();
            lnkApplicantCount.Text = myInfo.ApplicationCount.ToString();
            lnkProcssing.Text = myInfo.UserProcssingCount.ToString();
            lnkDecision.Text = myInfo.IRDecisionCount.ToString();
            //lnkFeedbackList.Text = myInfo.UserFeedbackCount.ToString();

            lbCompleteFeedback.Text = myInfo.CompleteFeedbackCount.ToString();
            lbIncompleteFeedback.Text = myInfo.IncompleteFeedbackCount.ToString();
            lbHiredInterview.Text = myInfo.HiredInterviewCount.ToString();
            lbRejectedInterview.Text = myInfo.RejectedInterviewCount.ToString();
            lbCompleteInterview.Text = myInfo.CompleteInterviewCount.ToString();

            lbWeeklyNewCandidates.Text = GenerateWeeklyLinks();
        }
    }

    private string GenerateWeeklyLinks()
    {
        string itemTemplate = "<a href='CandidatesReport.aspx?StartDate={0}&EndDate={1}'>From {0} To {1}</a><br />";
        int weekCount = 12;
        DateTime[] endDateArr = new DateTime[weekCount];
        DateTime lastEndDate = DateTime.Now - new TimeSpan((Convert.ToInt32(DateTime.Now.DayOfWeek) + 1), 0, 0, 0);
        StringBuilder sb = new StringBuilder();
        DateTime endDate = lastEndDate, startDate;
        for (int i = 0; i < weekCount; i++)
        {
            startDate = endDate - new TimeSpan(6,0,0,0);
            sb.Append(string.Format(itemTemplate, startDate.ToString("MM/dd/yyyy"), endDate.ToString("MM/dd/yyyy")));
            endDate -= new TimeSpan(7, 0, 0, 0);
        }
        return sb.ToString();
    }
}
