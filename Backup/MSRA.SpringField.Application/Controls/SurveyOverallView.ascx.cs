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

/*
 * Survey Report of OverallView 
 * Add by Yuanqin, 2011.6.7
 */
 
namespace MSRA.SpringField.Application.Controls
{
    public partial class SurveyOverallView : System.Web.UI.UserControl
    {
        private DataView AllSurveyReport;
        public DateTime m_StartDate, m_EndDate;
        public Int32 GroupId;
        public Int32 Duration;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            AllSurveyReport = new DataView(MSRA.Springfield.Components.BizObjects.CheckoutSurvey.GetCheckoutSurvey().Tables[0]);
            
            DataTable TBSurvey = new DataTable();
            TBSurvey.Columns.Add("SurveyTitle", typeof(string));
            TBSurvey.Columns.Add("Strongly agree #", typeof(string));
            TBSurvey.Columns.Add("Somewhat agree #", typeof(string));
            TBSurvey.Columns.Add("Neither agree nor disagree #", typeof(string));
            TBSurvey.Columns.Add("Somewhat disagree #", typeof(string));
            TBSurvey.Columns.Add("Strongly disagree #", typeof(string));
            TBSurvey.Columns.Add("N/A #", typeof(string));
            
            TBSurvey.Rows.Add("A.Overall View about MSRA internship");
            TBSurvey.Rows.Add("1.Good experience", "0", "0", "0", "0", "0", "0");
            TBSurvey.Rows.Add("B.Work at MSRA");
            TBSurvey.Rows.Add("1.Like work i do", "0", "0", "0", "0", "0", "0");
            TBSurvey.Rows.Add("2.Enough background", "0", "0", "0", "0", "00", "0");
            TBSurvey.Rows.Add("3.Work amount is appropriate", "0", "0", "0", "0", "0", "0");
            TBSurvey.Rows.Add("4.Work match objectives", "0", "0", "0", "0", "0", "0");
            TBSurvey.Rows.Add("5.Develop skills", "0", "0", "0", "0", "0", "0");
            TBSurvey.Rows.Add("  Research", "0", "0", "0", "0", "0", "0");
            TBSurvey.Rows.Add("  Software development", "0", "0", "0", "0", "0", "0");
            //TBSurvey.Rows.Add("  Design", "0", "0", "0", "0", "0", "0");
            //TBSurvey.Rows.Add("  Project management", "0", "0", "0", "0", "0", "0");
            TBSurvey.Rows.Add("  Project management", "0", "0", "0", "0", "0", "0");
            TBSurvey.Rows.Add("  Design", "0", "0", "0", "0", "0", "0");
            TBSurvey.Rows.Add("  Presentations", "0", "0", "0", "0", "0", "0");
            TBSurvey.Rows.Add("  Teamwork", "0", "0", "0", "0", "0", "0");
            TBSurvey.Rows.Add("C.Mentor and work group");
            TBSurvey.Rows.Add("1.Set goals from beginning", "0", "0", "0", "0", "0", "0");
            TBSurvey.Rows.Add("2.Help&coaching", "0", "0", "0", "0", "0", "0");
            TBSurvey.Rows.Add("3.Good use of my skills", "0", "0", "0", "0", "0", "0");
            TBSurvey.Rows.Add("4.Group member respect", "0", "0", "0", "0", "0", "0");
            TBSurvey.Rows.Add("D.Training and activity");
            TBSurvey.Rows.Add("1.Overall experience", "0", "0", "0", "0", "0", "0");
            TBSurvey.Rows.Add("2.Frequency is suitable", "0", "0", "0", "0", "0", "0");
            TBSurvey.Rows.Add("3.Trainings are essential", "0", "0", "0", "0", "0", "0");
            TBSurvey.Rows.Add("4.Activities are interesting", "0", "0", "0", "0", "0", "0");
            TBSurvey.Rows.Add("E.Life");
            TBSurvey.Rows.Add("1.Work/life balance", "0", "0", "0", "0", "0", "0");
            TBSurvey.Rows.Add("2.Work environment is suitable", "0", "0", "0", "0", "0", "0");
            TBSurvey.Rows.Add("3.Compensation", "0", "0", "0", "0", "0", "0");
            TBSurvey.Rows.Add("4.Satisfied with support", "0", "0", "0", "0", "0", "0");
            TBSurvey.Rows.Add("  On Board", "0", "0", "0", "0", "0", "0");
            TBSurvey.Rows.Add("  Accommodation", "0", "0", "0", "0", "0", "0");
            TBSurvey.Rows.Add("  Salary&meal", "0", "0", "0", "0", "0", "0");
            TBSurvey.Rows.Add("  Reimbursement", "0", "0", "0", "0", "0", "0");
            TBSurvey.Rows.Add("  IT support", "0", "0", "0", "0", "0", "0");
            TBSurvey.Rows.Add("  Daily support&help", "0", "0", "0", "0", "0", "0");
            TBSurvey.Rows.Add("F.Opinion of MS/MSRA");
            TBSurvey.Rows.Add("1.MS lead trend", "0", "0", "0", "0", "0", "0");
            TBSurvey.Rows.Add("2.MS has innovative environment", "0", "0", "0", "0", "0", "0");
            TBSurvey.Rows.Add("3.Return for internship", "0", "0", "0", "0", "0", "0");
            TBSurvey.Rows.Add("4.Want to join MS", "0", "0", "0", "0", "0", "0");
            TBSurvey.Rows.Add("5.Recommend MSRA to friend", "0", "0", "0", "0", "0", "0");

            string strFilter = GetFilter(); 
            string Key;

            //good experience
            for (int i = 1; i < TBSurvey.Columns.Count; i++)
            {
                string strFilter1 = " and (OverallView = '" + (i-1).ToString() + "') ";
                Key = "Survey_OverallView_" + i.ToString() + "_" + m_StartDate.ToShortDateString() + "_" + m_EndDate.ToShortDateString();
                if(SiteCache.Get(Key) == null)
                {
                    SiteCache.Insert(Key, (strFilter + strFilter1), SiteCache.DefaultExpiration);
                }
                AllSurveyReport.RowFilter = strFilter + strFilter1;
                TBSurvey.Rows[1][i] = String.Format("<a href='SurveyCommentsReport.aspx?type=Detail&StartDate=" + m_StartDate.AddSeconds(1).ToString("MM/dd/yyyy") 
                    + "&EndDate=" + m_EndDate.AddDays(-1).ToString("MM/dd/yyyy") + "&GroupId=" + GroupId.ToString() + "&Duration=" + Duration.ToString() 
                    + "&Row=1"+"&Column="+i.ToString()+"'>{0}</a>", AllSurveyReport.Count.ToString());
            }
            //Like Work i do
            for (int i = 1; i < TBSurvey.Columns.Count; i++)
            {
                string strFilter1 = " and (LikeWork = '" + (i - 1).ToString() + "') ";
                Key = "Survey_LikeWork_" + i.ToString() + "_" + m_StartDate.ToShortDateString() + "_" + m_EndDate.ToShortDateString();
                if (SiteCache.Get(Key) == null)
                {
                    SiteCache.Insert(Key, strFilter, SiteCache.DefaultExpiration);
                }
                AllSurveyReport.RowFilter = strFilter + strFilter1;
                TBSurvey.Rows[3][i] = String.Format("<a href='SurveyCommentsReport.aspx?type=Detail&StartDate=" + m_StartDate.AddSeconds(1).ToString("MM/dd/yyyy") + "&EndDate=" + m_EndDate.AddDays(-1).ToString("MM/dd/yyyy") + "&GroupId=" + GroupId.ToString() + "&Duration=" + Duration.ToString() + "&Row=3" + "&Column=" + i.ToString() + "'>{0}</a>", AllSurveyReport.Count.ToString());
            }
            //Enough background 
            for (int i = 1; i < TBSurvey.Columns.Count; i++)
            {
                string strFilter1 = " and (Background = '" + (i - 1).ToString() + "') ";

                Key = "Survey_Background_" + i.ToString() + "_" + m_StartDate.ToShortDateString() + "_" + m_EndDate.ToShortDateString();
                if (SiteCache.Get(Key) == null)
                {
                    SiteCache.Insert(Key, strFilter, SiteCache.DefaultExpiration);
                }
                AllSurveyReport.RowFilter = strFilter + strFilter1;
                TBSurvey.Rows[4][i] = String.Format("<a href='SurveyCommentsReport.aspx?type=Detail&StartDate=" + m_StartDate.AddSeconds(1).ToString("MM/dd/yyyy") + "&EndDate=" + m_EndDate.AddDays(-1).ToString("MM/dd/yyyy") + "&GroupId=" + GroupId.ToString() + "&Duration=" + Duration.ToString() + "&Row=4" + "&Column=" + i.ToString() + "'>{0}</a>", AllSurveyReport.Count.ToString());
            }
            //Work amount is appropriate
            for (int i = 1; i < TBSurvey.Columns.Count; i++)
            {
                string strFilter1 = " and (WorkAmount = '" + (i - 1).ToString() + "') ";
                Key = "Survey_WorkAmount_" + i.ToString() + "_" + m_StartDate.ToShortDateString() + "_" + m_EndDate.ToShortDateString();
                if (SiteCache.Get(Key) == null)
                {
                    SiteCache.Insert(Key, strFilter, SiteCache.DefaultExpiration);
                }
                AllSurveyReport.RowFilter = strFilter + strFilter1;
                TBSurvey.Rows[5][i] = String.Format("<a href='SurveyCommentsReport.aspx?type=Detail&StartDate=" + m_StartDate.AddSeconds(1).ToString("MM/dd/yyyy") + "&EndDate=" + m_EndDate.AddDays(-1).ToString("MM/dd/yyyy") + "&GroupId=" + GroupId.ToString() + "&Duration=" + Duration.ToString() + "&Row=5" + "&Column=" + i.ToString() + "'>{0}</a>", AllSurveyReport.Count.ToString());
            }
            //Work match objectives
            for (int i = 1; i < TBSurvey.Columns.Count; i++)
            {
                string strFilter1 = " and (Objects = '" + (i - 1).ToString() + "') ";
                Key = "Survey_Objects_" + i.ToString() + "_" + m_StartDate.ToShortDateString() + "_" + m_EndDate.ToShortDateString();
                if (SiteCache.Get(Key) == null)
                {
                    SiteCache.Insert(Key, strFilter, SiteCache.DefaultExpiration);
                }
                AllSurveyReport.RowFilter = strFilter + strFilter1;
                TBSurvey.Rows[6][i] = String.Format("<a href='SurveyCommentsReport.aspx?type=Detail&StartDate=" + m_StartDate.AddSeconds(1).ToString("MM/dd/yyyy") + "&EndDate=" + m_EndDate.AddDays(-1).ToString("MM/dd/yyyy") + "&GroupId=" + GroupId.ToString() + "&Duration=" + Duration.ToString() + "&Row=6" + "&Column=" + i.ToString() + "'>{0}</a>", AllSurveyReport.Count.ToString());
            }
            //Develop skills
            for (int i = 1; i < TBSurvey.Columns.Count; i++)
            {
                string strFilter1 = " and (DevelopmentSkill = '" + (i - 1).ToString() + "') ";
                Key = "Survey_DevelopmentSkill_" + i.ToString() + "_" + m_StartDate.ToShortDateString() + "_" + m_EndDate.ToShortDateString();
                if (SiteCache.Get(Key) == null)
                {
                    SiteCache.Insert(Key, strFilter, SiteCache.DefaultExpiration);
                }
                AllSurveyReport.RowFilter = strFilter + strFilter1;
                TBSurvey.Rows[7][i] = String.Format("<a href='SurveyCommentsReport.aspx?type=Detail&StartDate=" + m_StartDate.AddSeconds(1).ToString("MM/dd/yyyy") + "&EndDate=" + m_EndDate.AddDays(-1).ToString("MM/dd/yyyy") + "&GroupId=" + GroupId.ToString() + "&Duration=" + Duration.ToString() + "&Row=7" + "&Column=" + i.ToString() + "'>{0}</a>", AllSurveyReport.Count.ToString());
            }
            //Research
            for (int i = 1; i < TBSurvey.Columns.Count; i++)
            {
                string strFilter1 = " and (ResearchSkill = '" + (i - 1).ToString() + "') ";
                Key = "Survey_ResearchSkill_" + i.ToString() + "_" + m_StartDate.ToShortDateString() + "_" + m_EndDate.ToShortDateString();
                if (SiteCache.Get(Key) == null)
                {
                    SiteCache.Insert(Key, strFilter, SiteCache.DefaultExpiration);
                }
                AllSurveyReport.RowFilter = strFilter + strFilter1;
                TBSurvey.Rows[8][i] = String.Format("<a href='SurveyCommentsReport.aspx?type=Detail&StartDate=" + m_StartDate.AddSeconds(1).ToString("MM/dd/yyyy") + "&EndDate=" + m_EndDate.AddDays(-1).ToString("MM/dd/yyyy") + "&GroupId=" + GroupId.ToString() + "&Duration=" + Duration.ToString() + "&Row=8" + "&Column=" + i.ToString() + "'>{0}</a>", AllSurveyReport.Count.ToString());
            }
            //Software development
            for (int i = 1; i < TBSurvey.Columns.Count; i++)
            {
                string strFilter1 = " and (SDESkill = '" + (i - 1).ToString() + "') ";
                Key = "Survey_SDESkill_" + i.ToString() + "_" + m_StartDate.ToShortDateString() + "_" + m_EndDate.ToShortDateString();
                if (SiteCache.Get(Key) == null)
                {
                    SiteCache.Insert(Key, strFilter, SiteCache.DefaultExpiration);
                }
                AllSurveyReport.RowFilter = strFilter + strFilter1;
                TBSurvey.Rows[9][i] = String.Format("<a href='SurveyCommentsReport.aspx?type=Detail&StartDate=" + m_StartDate.AddSeconds(1).ToString("MM/dd/yyyy") + "&EndDate=" + m_EndDate.AddDays(-1).ToString("MM/dd/yyyy") + "&GroupId=" + GroupId.ToString() + "&Duration=" + Duration.ToString() + "&Row=9" + "&Column=" + i.ToString() + "'>{0}</a>", AllSurveyReport.Count.ToString());
            }
            //Project management
            for (int i = 1; i < TBSurvey.Columns.Count; i++)
            {
                string strFilter1 = " and (ProjectSkill = '" + (i - 1).ToString() + "') ";
                Key = "Survey_ProjectSkill_" + i.ToString() + "_" + m_StartDate.ToShortDateString() + "_" + m_EndDate.ToShortDateString();
                if (SiteCache.Get(Key) == null)
                {
                    SiteCache.Insert(Key, strFilter, SiteCache.DefaultExpiration);
                }
                AllSurveyReport.RowFilter = strFilter + strFilter1;
                TBSurvey.Rows[11][i] = String.Format("<a href='SurveyCommentsReport.aspx?type=Detail&StartDate=" + m_StartDate.AddSeconds(1).ToString("MM/dd/yyyy") + "&EndDate=" + m_EndDate.AddDays(-1).ToString("MM/dd/yyyy") + "&GroupId=" + GroupId.ToString() + "&Duration=" + Duration.ToString() + "&Row=10" + "&Column=" + i.ToString() + "'>{0}</a>", AllSurveyReport.Count.ToString());
            }

            //Design
            for (int i = 1; i < TBSurvey.Columns.Count; i++)
            {
                string strFilter1 = " and (DesignSkill = '" + (i - 1).ToString() + "') ";
                Key = "Survey_DesignSkill_" + i.ToString() + "_" + m_StartDate.ToShortDateString() + "_" + m_EndDate.ToShortDateString();
                if (SiteCache.Get(Key) == null)
                {
                    SiteCache.Insert(Key, strFilter, SiteCache.DefaultExpiration);
                }
                AllSurveyReport.RowFilter = strFilter + strFilter1;
                TBSurvey.Rows[10][i] = String.Format("<a href='SurveyCommentsReport.aspx?type=Detail&StartDate=" + m_StartDate.AddSeconds(1).ToString("MM/dd/yyyy") + "&EndDate=" + m_EndDate.AddDays(-1).ToString("MM/dd/yyyy") + "&GroupId=" + GroupId.ToString() + "&Duration=" + Duration.ToString() + "&Row=11" + "&Column=" + i.ToString() + "'>{0}</a>", AllSurveyReport.Count.ToString());
            }
            ////Project management
            //for (int i = 1; i < TBSurvey.Columns.Count; i++)
            //{
            //    string strFilter1 = " and (ProjectSkill = '" + (i - 1).ToString() + "') ";
            //    Key = "Survey_ProjectSkill_" + i.ToString() + "_" + m_StartDate.ToShortDateString() + "_" + m_EndDate.ToShortDateString();
            //    if (SiteCache.Get(Key) == null)
            //    {
            //        SiteCache.Insert(Key, strFilter, SiteCache.DefaultExpiration);
            //    }
            //    AllSurveyReport.RowFilter = strFilter + strFilter1;
            //    TBSurvey.Rows[11][i] = String.Format("<a href='SurveyCommentsReport.aspx?type=Detail&StartDate=" + m_StartDate.AddSeconds(1).ToString("MM/dd/yyyy") + "&EndDate=" + m_EndDate.AddDays(-1).ToString("MM/dd/yyyy") + "&GroupId=" + GroupId.ToString() + "&Duration=" + Duration.ToString() + "&Row=11" + "&Column=" + i.ToString() + "'>{0}</a>", AllSurveyReport.Count.ToString());
            //}


            //Presentations
            for (int i = 1; i < TBSurvey.Columns.Count; i++)
            {
                string strFilter1 = " and (CommunicationSkill = '" + (i - 1).ToString() + "') ";
                Key = "Survey_CommunicationSkill_" + i.ToString() + "_" + m_StartDate.ToShortDateString() + "_" + m_EndDate.ToShortDateString();
                if (SiteCache.Get(Key) == null)
                {
                    SiteCache.Insert(Key, strFilter, SiteCache.DefaultExpiration);
                }
                AllSurveyReport.RowFilter = strFilter + strFilter1;
                TBSurvey.Rows[12][i] = String.Format("<a href='SurveyCommentsReport.aspx?type=Detail&StartDate=" + m_StartDate.AddSeconds(1).ToString("MM/dd/yyyy") + "&EndDate=" + m_EndDate.AddDays(-1).ToString("MM/dd/yyyy") + "&GroupId=" + GroupId.ToString() + "&Duration=" + Duration.ToString() + "&Row=12" + "&Column=" + i.ToString() + "'>{0}</a>", AllSurveyReport.Count.ToString());
            }
            //Teamwork
            for (int i = 1; i < TBSurvey.Columns.Count; i++)
            {
                string strFilter1 = " and (Teamwork = '" + (i - 1).ToString() + "') ";
                Key = "Survey_Teamwork_" + i.ToString() + "_" + m_StartDate.ToShortDateString() + "_" + m_EndDate.ToShortDateString();
                if (SiteCache.Get(Key) == null)
                {
                    SiteCache.Insert(Key, strFilter, SiteCache.DefaultExpiration);
                }
                AllSurveyReport.RowFilter = strFilter + strFilter1;
                TBSurvey.Rows[13][i] = String.Format("<a href='SurveyCommentsReport.aspx?type=Detail&StartDate=" + m_StartDate.AddSeconds(1).ToString("MM/dd/yyyy") + "&EndDate=" + m_EndDate.AddDays(-1).ToString("MM/dd/yyyy") + "&GroupId=" + GroupId.ToString() + "&Duration=" + Duration.ToString() + "&Row=13" + "&Column=" + i.ToString() + "'>{0}</a>", AllSurveyReport.Count.ToString());
            }
            //Set goals from beginning
            for (int i = 1; i < TBSurvey.Columns.Count; i++)
            {
                string strFilter1 = " and (MentorSetGoal = '" + (i - 1).ToString() + "') ";
                Key = "Survey_MentorSetGoal_" + i.ToString() + "_" + m_StartDate.ToShortDateString() + "_" + m_EndDate.ToShortDateString();
                if (SiteCache.Get(Key) == null)
                {
                    SiteCache.Insert(Key, strFilter, SiteCache.DefaultExpiration);
                }
                AllSurveyReport.RowFilter = strFilter + strFilter1;
                TBSurvey.Rows[15][i] = String.Format("<a href='SurveyCommentsReport.aspx?type=Detail&StartDate=" + m_StartDate.AddSeconds(1).ToString("MM/dd/yyyy") + "&EndDate=" + m_EndDate.AddDays(-1).ToString("MM/dd/yyyy") + "&GroupId=" + GroupId.ToString() + "&Duration=" + Duration.ToString() + "&Row=15" + "&Column=" + i.ToString() + "'>{0}</a>", AllSurveyReport.Count.ToString());
            }
            //
            for (int i = 1; i < TBSurvey.Columns.Count; i++)
            {
                string strFilter1 = " and (HelpFromMentor = '" + (i - 1).ToString() + "') ";
                Key = "Survey_HelpFromMentor_" + i.ToString() + "_" + m_StartDate.ToShortDateString() + "_" + m_EndDate.ToShortDateString();
                if (SiteCache.Get(Key) == null)
                {
                    SiteCache.Insert(Key, strFilter, SiteCache.DefaultExpiration);
                }
                AllSurveyReport.RowFilter = strFilter + strFilter1;
                TBSurvey.Rows[16][i] = String.Format("<a href='SurveyCommentsReport.aspx?type=Detail&StartDate=" + m_StartDate.AddSeconds(1).ToString("MM/dd/yyyy") + "&EndDate=" + m_EndDate.AddDays(-1).ToString("MM/dd/yyyy") + "&GroupId=" + GroupId.ToString() + "&Duration=" + Duration.ToString() + "&Row=16" + "&Column=" + i.ToString() + "'>{0}</a>", AllSurveyReport.Count.ToString());
            }
            //
            for (int i = 1; i < TBSurvey.Columns.Count; i++)
            {
                string strFilter1 = " and (MakeGoodUse = '" + (i - 1).ToString() + "') ";
                Key = "Survey_MakeGoodUse_" + i.ToString() + "_" + m_StartDate.ToShortDateString() + "_" + m_EndDate.ToShortDateString();
                if (SiteCache.Get(Key) == null)
                {
                    SiteCache.Insert(Key, strFilter, SiteCache.DefaultExpiration);
                }
                AllSurveyReport.RowFilter = strFilter + strFilter1;
                TBSurvey.Rows[17][i] = String.Format("<a href='SurveyCommentsReport.aspx?type=Detail&StartDate=" + m_StartDate.AddSeconds(1).ToString("MM/dd/yyyy") + "&EndDate=" + m_EndDate.AddDays(-1).ToString("MM/dd/yyyy") + "&GroupId=" + GroupId.ToString() + "&Duration=" + Duration.ToString() + "&Row=17" + "&Column=" + i.ToString() + "'>{0}</a>", AllSurveyReport.Count.ToString());
            }
            //
            for (int i = 1; i < TBSurvey.Columns.Count; i++)
            {
                string strFilter1 = " and (Respect = '" + (i - 1).ToString() + "') ";
                Key = "Survey_Respect_" + i.ToString() + "_" + m_StartDate.ToShortDateString() + "_" + m_EndDate.ToShortDateString();
                if (SiteCache.Get(Key) == null)
                {
                    SiteCache.Insert(Key, strFilter, SiteCache.DefaultExpiration);
                }
                AllSurveyReport.RowFilter = strFilter + strFilter1;
                TBSurvey.Rows[18][i] = String.Format("<a href='SurveyCommentsReport.aspx?type=Detail&StartDate=" + m_StartDate.AddSeconds(1).ToString("MM/dd/yyyy") + "&EndDate=" + m_EndDate.AddDays(-1).ToString("MM/dd/yyyy") + "&GroupId=" + GroupId.ToString() + "&Duration=" + Duration.ToString() + "&Row=18" + "&Column=" + i.ToString() + "'>{0}</a>", AllSurveyReport.Count.ToString());
            }
            //
            for (int i = 1; i < TBSurvey.Columns.Count; i++)
            {
                string strFilter1 = " and (TrainingView = '" + (i - 1).ToString() + "') ";
                Key = "Survey_TrainingView_" + i.ToString() + "_" + m_StartDate.ToShortDateString() + "_" + m_EndDate.ToShortDateString();
                if (SiteCache.Get(Key) == null)
                {
                    SiteCache.Insert(Key, strFilter, SiteCache.DefaultExpiration);
                }
                AllSurveyReport.RowFilter = strFilter + strFilter1;
                TBSurvey.Rows[20][i] = String.Format("<a href='SurveyCommentsReport.aspx?type=Detail&StartDate=" + m_StartDate.AddSeconds(1).ToString("MM/dd/yyyy") + "&EndDate=" + m_EndDate.AddDays(-1).ToString("MM/dd/yyyy") + "&GroupId=" + GroupId.ToString() + "&Duration=" + Duration.ToString() + "&Row=20" + "&Column=" + i.ToString() + "'>{0}</a>", AllSurveyReport.Count.ToString());
            }
            //
            for (int i = 1; i < TBSurvey.Columns.Count; i++)
            {
                string strFilter1 = " and (TrainingSuitable = '" + (i - 1).ToString() + "') ";
                Key = "Survey_TrainingSuitable_" + i.ToString() + "_" + m_StartDate.ToShortDateString() + "_" + m_EndDate.ToShortDateString();
                if (SiteCache.Get(Key) == null)
                {
                    SiteCache.Insert(Key, strFilter, SiteCache.DefaultExpiration);
                }
                AllSurveyReport.RowFilter = strFilter + strFilter1;
                TBSurvey.Rows[21][i] = String.Format("<a href='SurveyCommentsReport.aspx?type=Detail&StartDate=" + m_StartDate.AddSeconds(1).ToString("MM/dd/yyyy") + "&EndDate=" + m_EndDate.AddDays(-1).ToString("MM/dd/yyyy") + "&GroupId=" + GroupId.ToString() + "&Duration=" + Duration.ToString() + "&Row=21" + "&Column=" + i.ToString() + "'>{0}</a>", AllSurveyReport.Count.ToString());
            }
            //
            for (int i = 1; i < TBSurvey.Columns.Count; i++)
            {
                string strFilter1 = " and (TrainingEssential = '" + (i - 1).ToString() + "') ";
                Key = "Survey_TrainingEssential_" + i.ToString() + "_" + m_StartDate.ToShortDateString() + "_" + m_EndDate.ToShortDateString();
                if (SiteCache.Get(Key) == null)
                {
                    SiteCache.Insert(Key, strFilter, SiteCache.DefaultExpiration);
                }
                AllSurveyReport.RowFilter = strFilter + strFilter1;
                TBSurvey.Rows[22][i] = String.Format("<a href='SurveyCommentsReport.aspx?type=Detail&StartDate=" + m_StartDate.AddSeconds(1).ToString("MM/dd/yyyy") + "&EndDate=" + m_EndDate.AddDays(-1).ToString("MM/dd/yyyy") + "&GroupId=" + GroupId.ToString() + "&Duration=" + Duration.ToString() + "&Row=22" + "&Column=" + i.ToString() + "'>{0}</a>", AllSurveyReport.Count.ToString());
            }
            //
            for (int i = 1; i < TBSurvey.Columns.Count; i++)
            {
                string strFilter1 = " and (ActivityInterest = '" + (i - 1).ToString() + "') ";
                Key = "Survey_ActivityInterest_" + i.ToString() + "_" + m_StartDate.ToShortDateString() + "_" + m_EndDate.ToShortDateString();
                if (SiteCache.Get(Key) == null)
                {
                    SiteCache.Insert(Key, strFilter, SiteCache.DefaultExpiration);
                }
                AllSurveyReport.RowFilter = strFilter + strFilter1;
                TBSurvey.Rows[23][i] = String.Format("<a href='SurveyCommentsReport.aspx?type=Detail&StartDate=" + m_StartDate.AddSeconds(1).ToString("MM/dd/yyyy") + "&EndDate=" + m_EndDate.AddDays(-1).ToString("MM/dd/yyyy") + "&GroupId=" + GroupId.ToString() + "&Duration=" + Duration.ToString() + "&Row=23" + "&Column=" + i.ToString() + "'>{0}</a>", AllSurveyReport.Count.ToString());
            }
            //
            for (int i = 1; i < TBSurvey.Columns.Count; i++)
            {
                string strFilter1 = " and (Balance = '" + (i - 1).ToString() + "') ";
                Key = "Survey_Balance_" + i.ToString() + "_" + m_StartDate.ToShortDateString() + "_" + m_EndDate.ToShortDateString();
                if (SiteCache.Get(Key) == null)
                {
                    SiteCache.Insert(Key, strFilter, SiteCache.DefaultExpiration);
                }
                AllSurveyReport.RowFilter = strFilter + strFilter1;
                TBSurvey.Rows[25][i] = String.Format("<a href='SurveyCommentsReport.aspx?type=Detail&StartDate=" + m_StartDate.AddSeconds(1).ToString("MM/dd/yyyy") + "&EndDate=" + m_EndDate.AddDays(-1).ToString("MM/dd/yyyy") + "&GroupId=" + GroupId.ToString() + "&Duration=" + Duration.ToString() + "&Row=25" + "&Column=" + i.ToString() + "'>{0}</a>", AllSurveyReport.Count.ToString());
            }
            //
            for (int i = 1; i < TBSurvey.Columns.Count; i++)
            {
                string strFilter1 = " and (WorkEnvironment = '" + (i - 1).ToString() + "') ";
                Key = "Survey_WorkEnvironment_" + i.ToString() + "_" + m_StartDate.ToShortDateString() + "_" + m_EndDate.ToShortDateString();
                if (SiteCache.Get(Key) == null)
                {
                    SiteCache.Insert(Key, strFilter, SiteCache.DefaultExpiration);
                }
                AllSurveyReport.RowFilter = strFilter + strFilter1;
                TBSurvey.Rows[26][i] = String.Format("<a href='SurveyCommentsReport.aspx?type=Detail&StartDate=" + m_StartDate.AddSeconds(1).ToString("MM/dd/yyyy") + "&EndDate=" + m_EndDate.AddDays(-1).ToString("MM/dd/yyyy") + "&GroupId=" + GroupId.ToString() + "&Duration=" + Duration.ToString() + "&Row=26" + "&Column=" + i.ToString() + "'>{0}</a>", AllSurveyReport.Count.ToString());
            }
            //
            for (int i = 1; i < TBSurvey.Columns.Count; i++)
            {
                string strFilter1 = " and (Compensation = '" + (i - 1).ToString() + "') ";
                Key = "Survey_Compensation_" + i.ToString() + "_" + m_StartDate.ToShortDateString() + "_" + m_EndDate.ToShortDateString();
                if (SiteCache.Get(Key) == null)
                {
                    SiteCache.Insert(Key, strFilter, SiteCache.DefaultExpiration);
                }
                AllSurveyReport.RowFilter = strFilter + strFilter1;
                TBSurvey.Rows[27][i] = String.Format("<a href='SurveyCommentsReport.aspx?type=Detail&StartDate=" + m_StartDate.AddSeconds(1).ToString("MM/dd/yyyy") + "&EndDate=" + m_EndDate.AddDays(-1).ToString("MM/dd/yyyy") + "&GroupId=" + GroupId.ToString() + "&Duration=" + Duration.ToString() + "&Row=27" + "&Column=" + i.ToString() + "'>{0}</a>", AllSurveyReport.Count.ToString());
            }
            //
            for (int i = 1; i < TBSurvey.Columns.Count; i++)
            {
                string strFilter1 = " and (Satisfaction = '" + (i - 1).ToString() + "') ";
                Key = "Survey_Satisfaction_" + i.ToString() + "_" + m_StartDate.ToShortDateString() + "_" + m_EndDate.ToShortDateString();
                if (SiteCache.Get(Key) == null)
                {
                    SiteCache.Insert(Key, strFilter, SiteCache.DefaultExpiration);
                }
                AllSurveyReport.RowFilter = strFilter + strFilter1;
                TBSurvey.Rows[28][i] = String.Format("<a href='SurveyCommentsReport.aspx?type=Detail&StartDate=" + m_StartDate.AddSeconds(1).ToString("MM/dd/yyyy") + "&EndDate=" + m_EndDate.AddDays(-1).ToString("MM/dd/yyyy") + "&GroupId=" + GroupId.ToString() + "&Duration=" + Duration.ToString() + "&Row=28" + "&Column=" + i.ToString() + "'>{0}</a>", AllSurveyReport.Count.ToString());
            }
            //
            for (int i = 1; i < TBSurvey.Columns.Count; i++)
            {
                string strFilter1 = " and (OnBoard = '" + (i - 1).ToString() + "') ";
                Key = "Survey_OnBoard_" + i.ToString() + "_" + m_StartDate.ToShortDateString() + "_" + m_EndDate.ToShortDateString();
                if (SiteCache.Get(Key) == null)
                {
                    SiteCache.Insert(Key, strFilter, SiteCache.DefaultExpiration);
                }
                AllSurveyReport.RowFilter = strFilter + strFilter1;
                TBSurvey.Rows[29][i] = String.Format("<a href='SurveyCommentsReport.aspx?type=Detail&StartDate=" + m_StartDate.AddSeconds(1).ToString("MM/dd/yyyy") + "&EndDate=" + m_EndDate.AddDays(-1).ToString("MM/dd/yyyy") + "&GroupId=" + GroupId.ToString() + "&Duration=" + Duration.ToString() + "&Row=29" + "&Column=" + i.ToString() + "'>{0}</a>", AllSurveyReport.Count.ToString());
            }
            //
            for (int i = 1; i < TBSurvey.Columns.Count; i++)
            {
                string strFilter1 = " and (Accommodation = '" + (i - 1).ToString() + "') ";
                Key = "Survey_Accommodation_" + i.ToString() + "_" + m_StartDate.ToShortDateString() + "_" + m_EndDate.ToShortDateString();
                if (SiteCache.Get(Key) == null)
                {
                    SiteCache.Insert(Key, strFilter, SiteCache.DefaultExpiration);
                }
                AllSurveyReport.RowFilter = strFilter + strFilter1;
                TBSurvey.Rows[30][i] = String.Format("<a href='SurveyCommentsReport.aspx?type=Detail&StartDate=" + m_StartDate.AddSeconds(1).ToString("MM/dd/yyyy") + "&EndDate=" + m_EndDate.AddDays(-1).ToString("MM/dd/yyyy") + "&GroupId=" + GroupId.ToString() + "&Duration=" + Duration.ToString() + "&Row=30" + "&Column=" + i.ToString() + "'>{0}</a>", AllSurveyReport.Count.ToString());
            }
            //
            for (int i = 1; i < TBSurvey.Columns.Count; i++)
            {
                string strFilter1 = " and (SalaryAndMeal = '" + (i - 1).ToString() + "') ";
                Key = "Survey_SalaryAndMeal_" + i.ToString() + "_" + m_StartDate.ToShortDateString() + "_" + m_EndDate.ToShortDateString();
                if (SiteCache.Get(Key) == null)
                {
                    SiteCache.Insert(Key, strFilter, SiteCache.DefaultExpiration);
                }
                AllSurveyReport.RowFilter = strFilter + strFilter1;
                TBSurvey.Rows[31][i] = String.Format("<a href='SurveyCommentsReport.aspx?type=Detail&StartDate=" + m_StartDate.AddSeconds(1).ToString("MM/dd/yyyy") + "&EndDate=" + m_EndDate.AddDays(-1).ToString("MM/dd/yyyy") + "&GroupId=" + GroupId.ToString() + "&Duration=" + Duration.ToString() + "&Row=31" + "&Column=" + i.ToString() + "'>{0}</a>", AllSurveyReport.Count.ToString());
            }
            //
            for (int i = 1; i < TBSurvey.Columns.Count; i++)
            {
                string strFilter1 = " and (Reimbursement = '" + (i - 1).ToString() + "') ";
                Key = "Survey_Reimbursement_" + i.ToString() + "_" + m_StartDate.ToShortDateString() + "_" + m_EndDate.ToShortDateString();
                if (SiteCache.Get(Key) == null)
                {
                    SiteCache.Insert(Key, strFilter, SiteCache.DefaultExpiration);
                }
                AllSurveyReport.RowFilter = strFilter + strFilter1;
                TBSurvey.Rows[32][i] = String.Format("<a href='SurveyCommentsReport.aspx?type=Detail&StartDate=" + m_StartDate.AddSeconds(1).ToString("MM/dd/yyyy") + "&EndDate=" + m_EndDate.AddDays(-1).ToString("MM/dd/yyyy") + "&GroupId=" + GroupId.ToString() + "&Duration=" + Duration.ToString() + "&Row=32" + "&Column=" + i.ToString() + "'>{0}</a>", AllSurveyReport.Count.ToString());
            }
            //
            for (int i = 1; i < TBSurvey.Columns.Count; i++)
            {
                string strFilter1 = " and (ITSupport = '" + (i - 1).ToString() + "') ";
                Key = "Survey_ITSupport_" + i.ToString() + "_" + m_StartDate.ToShortDateString() + "_" + m_EndDate.ToShortDateString();
                if (SiteCache.Get(Key) == null)
                {
                    SiteCache.Insert(Key, strFilter, SiteCache.DefaultExpiration);
                }
                AllSurveyReport.RowFilter = strFilter + strFilter1;
                TBSurvey.Rows[33][i] = String.Format("<a href='SurveyCommentsReport.aspx?type=Detail&StartDate=" + m_StartDate.AddSeconds(1).ToString("MM/dd/yyyy") + "&EndDate=" + m_EndDate.AddDays(-1).ToString("MM/dd/yyyy") + "&GroupId=" + GroupId.ToString() + "&Duration=" + Duration.ToString() + "&Row=33" + "&Column=" + i.ToString() + "'>{0}</a>", AllSurveyReport.Count.ToString());
            }
            //
            for (int i = 1; i < TBSurvey.Columns.Count; i++)
            {
                string strFilter1 = " and (DailySupport = '" + (i - 1).ToString() + "') ";
                Key = "Survey_DailySupport_" + i.ToString() + "_" + m_StartDate.ToShortDateString() + "_" + m_EndDate.ToShortDateString();
                if (SiteCache.Get(Key) == null)
                {
                    SiteCache.Insert(Key, strFilter, SiteCache.DefaultExpiration);
                }
                AllSurveyReport.RowFilter = strFilter + strFilter1;
                TBSurvey.Rows[34][i] = String.Format("<a href='SurveyCommentsReport.aspx?type=Detail&StartDate=" + m_StartDate.AddSeconds(1).ToString("MM/dd/yyyy") + "&EndDate=" + m_EndDate.AddDays(-1).ToString("MM/dd/yyyy") + "&GroupId=" + GroupId.ToString() + "&Duration=" + Duration.ToString() + "&Row=34" + "&Column=" + i.ToString() + "'>{0}</a>", AllSurveyReport.Count.ToString());
            }
            //
            for (int i = 1; i < TBSurvey.Columns.Count; i++)
            {
                string strFilter1 = " and (Leading = '" + (i - 1).ToString() + "') ";
                Key = "Survey_Leading_" + i.ToString() + "_" + m_StartDate.ToShortDateString() + "_" + m_EndDate.ToShortDateString();
                if (SiteCache.Get(Key) == null)
                {
                    SiteCache.Insert(Key, strFilter, SiteCache.DefaultExpiration);
                }
                AllSurveyReport.RowFilter = strFilter + strFilter1;
                TBSurvey.Rows[36][i] = String.Format("<a href='SurveyCommentsReport.aspx?type=Detail&StartDate=" + m_StartDate.AddSeconds(1).ToString("MM/dd/yyyy") + "&EndDate=" + m_EndDate.AddDays(-1).ToString("MM/dd/yyyy") + "&GroupId=" + GroupId.ToString() + "&Duration=" + Duration.ToString() + "&Row=36" + "&Column=" + i.ToString() + "'>{0}</a>", AllSurveyReport.Count.ToString());
            }
            //
            for (int i = 1; i < TBSurvey.Columns.Count; i++)
            {
                string strFilter1 = " and (MSCulture = '" + (i - 1).ToString() + "') ";
                Key = "Survey_MSCulture_" + i.ToString() + "_" + m_StartDate.ToShortDateString() + "_" + m_EndDate.ToShortDateString();
                if (SiteCache.Get(Key) == null)
                {
                    SiteCache.Insert(Key, strFilter, SiteCache.DefaultExpiration);
                }
                AllSurveyReport.RowFilter = strFilter + strFilter1;
                TBSurvey.Rows[37][i] = String.Format("<a href='SurveyCommentsReport.aspx?type=Detail&StartDate=" + m_StartDate.AddSeconds(1).ToString("MM/dd/yyyy") + "&EndDate=" + m_EndDate.AddDays(-1).ToString("MM/dd/yyyy") + "&GroupId=" + GroupId.ToString() + "&Duration=" + Duration.ToString() + "&Row=37" + "&Column=" + i.ToString() + "'>{0}</a>", AllSurveyReport.Count.ToString());
            }
            //
            for (int i = 1; i < TBSurvey.Columns.Count; i++)
            {
                string strFilter1 = " and (ReturnAsIntern = '" + (i - 1).ToString() + "') ";
                Key = "Survey_ReturnAsIntern_" + i.ToString() + "_" + m_StartDate.ToShortDateString() + "_" + m_EndDate.ToShortDateString();
                if (SiteCache.Get(Key) == null)
                {
                    SiteCache.Insert(Key, strFilter, SiteCache.DefaultExpiration);
                }
                AllSurveyReport.RowFilter = strFilter + strFilter1;
                TBSurvey.Rows[38][i] = String.Format("<a href='SurveyCommentsReport.aspx?type=Detail&StartDate=" + m_StartDate.AddSeconds(1).ToString("MM/dd/yyyy") + "&EndDate=" + m_EndDate.AddDays(-1).ToString("MM/dd/yyyy") + "&GroupId=" + GroupId.ToString() + "&Duration=" + Duration.ToString() + "&Row=38" + "&Column=" + i.ToString() + "'>{0}</a>", AllSurveyReport.Count.ToString());
            }
            //
            for (int i = 1; i < TBSurvey.Columns.Count; i++)
            {
                string strFilter1 = " and (JoinMS = '" + (i - 1).ToString() + "') ";
                Key = "Survey_JoinMS_" + i.ToString() + "_" + m_StartDate.ToShortDateString() + "_" + m_EndDate.ToShortDateString();
                if (SiteCache.Get(Key) == null)
                {
                    SiteCache.Insert(Key, strFilter, SiteCache.DefaultExpiration);
                }
                AllSurveyReport.RowFilter = strFilter + strFilter1;
                TBSurvey.Rows[39][i] = String.Format("<a href='SurveyCommentsReport.aspx?type=Detail&StartDate=" + m_StartDate.AddSeconds(1).ToString("MM/dd/yyyy") + "&EndDate=" + m_EndDate.AddDays(-1).ToString("MM/dd/yyyy") + "&GroupId=" + GroupId.ToString() + "&Duration=" + Duration.ToString() + "&Row=39" + "&Column=" + i.ToString() + "'>{0}</a>", AllSurveyReport.Count.ToString());
            }
            //
            for (int i = 1; i < TBSurvey.Columns.Count; i++)
            {
                string strFilter1 = " and (Recommend = '" + (i - 1).ToString() + "') ";
                Key = "Survey_Recommend_" + i.ToString() + "_" + m_StartDate.ToShortDateString() + "_" + m_EndDate.ToShortDateString();
                if (SiteCache.Get(Key) == null)
                {
                    SiteCache.Insert(Key, strFilter, SiteCache.DefaultExpiration);
                }
                AllSurveyReport.RowFilter = strFilter + strFilter1;
                TBSurvey.Rows[40][i] = String.Format("<a href='SurveyCommentsReport.aspx?type=Detail&StartDate=" + m_StartDate.AddSeconds(1).ToString("MM/dd/yyyy") + "&EndDate=" + m_EndDate.AddDays(-1).ToString("MM/dd/yyyy") + "&GroupId=" + GroupId.ToString() + "&Duration=" + Duration.ToString() + "&Row=40" + "&Column=" + i.ToString() + "'>{0}</a>", AllSurveyReport.Count.ToString());
            }

            gvSurveyOverallView.DataSource = TBSurvey.DefaultView;
            gvSurveyOverallView.DataBind();
            AllSurveyReport.RowFilter = strFilter;
            lbCount.Text = AllSurveyReport.Count.ToString();

            ModifyStyle();

        }

        private string GetFilter()
        {
            string strFilter = String.Empty;

            if (m_StartDate.ToString() != null)
            {
                strFilter += "(CheckOutDate > '";
                strFilter += m_StartDate.ToString();
                strFilter += "') and ";
            }
            if (m_EndDate.ToString() != null)
            {
                strFilter += "(CheckOutDate < '";
                strFilter += m_EndDate.ToString();
                strFilter += "') and ";
            }
            if (GroupId.ToString() != "0")
            {
                strFilter += "(GroupId = '";
                strFilter += GroupId.ToString();
                strFilter += "') and ";
            }
            if (Duration.ToString() != "0")
            {
                strFilter += "(InternshipDuration = '";
                strFilter += Duration.ToString();
                strFilter += "') and ";
            }

            if (strFilter.Length > 4)
                strFilter = strFilter.Substring(0, strFilter.Length - 4);
            return strFilter;
        }

        protected void gvSurveyOverallView_PreRender(object sender, EventArgs e)
        {
            ModifyStyle();
        }

        private void ModifyStyle()
        {
            if (gvSurveyOverallView.Rows.Count > 0)
            {
                gvSurveyOverallView.Rows[0].BackColor = System.Drawing.Color.FromName("#eeeeee");
                gvSurveyOverallView.Rows[0].Font.Bold = true;

                gvSurveyOverallView.Rows[2].BackColor = System.Drawing.Color.FromName("#eeeeee");
                gvSurveyOverallView.Rows[2].Font.Bold = true;

                gvSurveyOverallView.Rows[14].BackColor = System.Drawing.Color.FromName("#eeeeee");
                gvSurveyOverallView.Rows[14].Font.Bold = true;

                gvSurveyOverallView.Rows[19].BackColor = System.Drawing.Color.FromName("#eeeeee");
                gvSurveyOverallView.Rows[19].Font.Bold = true;

                gvSurveyOverallView.Rows[24].BackColor = System.Drawing.Color.FromName("#eeeeee");
                gvSurveyOverallView.Rows[24].Font.Bold = true;

                gvSurveyOverallView.Rows[35].BackColor = System.Drawing.Color.FromName("#eeeeee");
                gvSurveyOverallView.Rows[35].Font.Bold = true;
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
            gvSurveyOverallView.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }
    }
}