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
using MSRA.SpringField.Components.BizObjects;
using MSRA.SpringField.Components.Enumerations;

namespace MSRA.SpringField.Application
{
    public partial class GeneralInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (SiteUser.Current.IsInRole(RoleType.InternRecruiter))
            {
                pnlIRInfo.Visible = true;
                this.sitesummary.Visible = true;
            }

            if (!IsPostBack)
            {
                SiteGeneralInfo myInfo = SiteGeneralInfo.GetGeneralInfo();
                lnkApplicantCount.Text = myInfo.ApplicationCount.ToString();
                if (SiteUser.Current.IsInRole(RoleType.InternRecruiter))
                {
                    lnkProcssing.Text = Interview.GetAllProcessingInterview().Tables[0].Rows.Count.ToString();
                }
                else
                {
                    lnkProcssing.Text = myInfo.UserProcssingCount.ToString();
                }
                
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
            string itemTemplate = "<a href='Modules/Reports/CandidatesReport.aspx?StartDate={0}&EndDate={1}'> {0} - {1}</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
            int weekCount = 12;
            DateTime[] endDateArr = new DateTime[weekCount];
            DateTime lastEndDate = DateTime.Now - new TimeSpan((Convert.ToInt32(DateTime.Now.DayOfWeek) + 1), 0, 0, 0);
            StringBuilder sb = new StringBuilder();
            DateTime endDate = lastEndDate, startDate;
            for (int i = 0; i < weekCount; i+=2)
            {
                startDate = endDate - new TimeSpan(6, 0, 0, 0);
                sb.Append(string.Format(itemTemplate, startDate.ToString("MM/dd/yyyy"), endDate.ToString("MM/dd/yyyy")));
                endDate -= new TimeSpan(7, 0, 0, 0);

                startDate = endDate - new TimeSpan(6, 0, 0, 0);
                sb.Append(string.Format(itemTemplate, startDate.ToString("MM/dd/yyyy"), endDate.ToString("MM/dd/yyyy")));
                endDate -= new TimeSpan(7, 0, 0, 0);

                sb.Append("<br/>");
            }
            return sb.ToString();
        }
    }
}