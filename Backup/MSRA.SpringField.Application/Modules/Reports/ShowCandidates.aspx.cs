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
using MSRA.SpringField.Components;
using MSRA.SpringField.Components.BizObjects;
using MSRA.SpringField.Components.Enumerations;
using MSRA.SpringField.Application.Config.Schemas;
using System.Linq;
using System.Drawing;

namespace MSRA.SpringField.Application
{
    public partial class WeeklyCandidates : System.Web.UI.Page
    {
        private DateTime m_BeginDate, m_EndDate;
        //    private string m_ReportType;
        private string m_CacheKey;
        private string m_DSType;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["StartDate"] != null && Request["EndDate"] != null)
            {
                try
                {
                    m_BeginDate = Convert.ToDateTime(Request["StartDate"]);
                    m_EndDate = Convert.ToDateTime(Request["EndDate"]).AddDays(1).AddSeconds(-1);
                    m_CacheKey = Convert.ToString(Request["key"]);
                    m_DSType = Convert.ToString(Request["ds"]);
                }
                catch
                {
                    JSUtility.Alert(this, "Invalid Parameters!");
                    JSUtility.CloseWindow(this);
                    return;
                }
            }
            else
            {
                JSUtility.Alert(this, "Invalid Parameters!");
                JSUtility.CloseWindow(this);
                return;
            }

            if (!IsPostBack)
            {
                lbDateDuration.Text = string.Format("Date From {0} To {1}", m_BeginDate.ToShortDateString(), m_EndDate.ToShortDateString());
                if (Request["type"] == null)
                {
                    lbReportTitle.Text = "New Candidates Report";
                    BindNewCandidatesData();
                }
                else if (Request["type"].ToLower() == "source")
                {
                    lbReportTitle.Text = "Candidates Sourcing Report";
                    BindSourceCandidatesData();
                }
                else if (Request["type"].ToLower() == "hire")
                {
                    lbReportTitle.Text = "Hiring Report";
                    BindHireCandidatesData();
                }
            }
        }

        private void BindHireCandidatesData()
        {
            if (SiteCache.Get(m_CacheKey) != null)
            {
                DataSet interviews = Interview.GetDurationInterview(m_BeginDate, m_EndDate);
                DataView dv = new DataView(interviews.Tables[0]);
                dv.RowFilter = SiteCache.Get(m_CacheKey) as string;
                gvApplicants.DataSource = dv;
                gvApplicants.DataBind();
                lbCount.Text = Convert.ToString(dv.Count);
            }
            else
            {
                JSUtility.Alert(this, "Invalid Parameters!");
                JSUtility.CloseWindow(this);
                JSUtility.RedirectLocation(this, "ReportGenerator.aspx");
                return;
            }
        }

        private void BindNewCandidatesData()
        {
            FilterGenerator filter = new FilterGenerator();
            filter.ApplyStartDate = m_BeginDate.ToShortDateString();
            filter.ApplyEndDate = m_EndDate.AddDays(1.0).ToShortDateString();
            string filterStr = filter.BuildFilterExpression();
            DataView dv = new DataView(Applicant.GetAllApplicants().Tables[0]);
            if (!String.IsNullOrEmpty(filterStr))
            {
                dv.RowFilter = filterStr;
            }
            gvApplicants.DataSource = dv;
            gvApplicants.DataBind();
            lbCount.Text = dv.Count.ToString();
        }

        private void BindSourceCandidatesData()
        {
            if (SiteCache.Get(m_CacheKey) != null)
            {
                DataView dv = new DataView(CashedApplicantInfo.GetAllApplcantsforSourcingReport(m_BeginDate, m_EndDate).Tables[0]);
                dv.RowFilter = SiteCache.Get(m_CacheKey) as string;
                gvApplicants.DataSource = dv;
                gvApplicants.DataBind();
                lbCount.Text = dv.Count.ToString();
            }
            else
            {
                JSUtility.Alert(this, "Invalid Parameters!");
                JSUtility.CloseWindow(this);
                JSUtility.RedirectLocation(this, "ReportGenerator.aspx");
                return;
            }
        }

        protected string ParseStatus(object dataItem)
        {
            DataRowView dr = (DataRowView)dataItem;
            return StaticData.AppStatusDict[(ApplicationStatusEnum)dr["Status"]];
        }

        protected string ParseInstitution(object dataItem)
        {
            DataRowView dr = (DataRowView)dataItem;
            return dr["HighestEducationalInstitution"] as string;
        }

        protected string ParseDegree(object dataItem)
        {
            DataRowView dr = (DataRowView)dataItem;
            return StaticData.DegreeList[Convert.ToInt32(dr["Degree"])];
        }

        protected string ParseMentor(object dataItem)
        {
            DataRowView dr = (DataRowView)dataItem;
            string curInterviewId = Interview.GetRecentInterviewIdByApplicant(new Guid(dr["ApplicantId"].ToString()));
            string strHiringManagerAlias = "N/A";
            String status = StaticData.AppStatusDict[(ApplicationStatusEnum)dr["Status"]];

            if (status.ToLower() != "available" && status.ToLower() != "Key Referring" && curInterviewId != string.Empty)
            {
                Interview curInterview = Interview.GetInterviewById(Convert.ToInt32(curInterviewId));
                try
                {
                    strHiringManagerAlias = SiteUser.GetAliasByUserId(curInterview.HiringManagerId);
                }
                catch
                {
                    strHiringManagerAlias = "N/A";
                }
            }
            return "<b>" + strHiringManagerAlias + "</b>";
        }
        //简历入库的时间
        protected string ParseDate(object dataItem)
        {
            DataRowView dr = (DataRowView)dataItem;
            return Convert.ToDateTime(dr["ApplicationDate"]).ToShortDateString();
        }

        protected string ParseInterviewStatus(object dataItem)
        {
            DataRowView dr = (DataRowView)dataItem;
            Guid curId = (Guid)dr["ApplicantId"];
            return Interview.GetRecentInterviewStatus(curId);
        }

        //interview开始的时间
        protected string ParseLastActionDate(object dataItem)
        {
            DataRowView dr = (DataRowView)dataItem;
            Guid curId = (Guid)dr["ApplicantId"];
            return Interview.GetRecentInterviewDate(curId);
        }

        protected void gvApplicants_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TableCell curCell = e.Row.Cells[10];
                DataRowView curData = (DataRowView)e.Row.DataItem;
                string curArg = Convert.ToString(curData["ApplicantId"]);
                ImageButton btnAddFavorite = (ImageButton)curCell.FindControl("btnAddFavorite");

                btnAddFavorite.CommandArgument = curArg;
                if (curData["ReferralType"] != null && curData["ReferralType"] != DBNull.Value)
                {
                    ReferralType referralType = (ReferralType)Convert.ToInt32(curData["ReferralType"]);
                    if (referralType == ReferralType.KeyReferral)
                    {
                        e.Row.BackColor = System.Drawing.Color.Honeydew;
                    }
                }

                SpringFieldDataContext ctx = new SpringFieldDataContext();
                var result = from applicant in ctx.sf_ApplicantBasicInfos
                             where applicant.ApplicantId.ToString() == curArg
                             select applicant;
                
                if (result.Count<sf_ApplicantBasicInfo>() == 0)
                {
                    e.Row.BackColor = ColorTranslator.FromHtml("#FEE7E9");

                    HyperLink link = e.Row.FindControl("hlApplication") as HyperLink;
                    if (link != null)
                    {
                        link.NavigateUrl = "";
                    }
                }
            }
        }

        protected void gvApplicants_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "AddFavorite":
                    {
                        Guid applicantId = new Guid(e.CommandArgument.ToString());
                        Guid ownerId = SiteUser.Current.SiteUserId;
                        if (Favorite.IsFavoriteExists(ownerId, applicantId))
                        {
                            JSUtility.Alert(this, "Favorite item is exists!");
                        }
                        else
                        {
                            Favorite favorite = new Favorite();
                            favorite.ApplicantId = applicantId;
                            favorite.OwnerId = ownerId;
                            favorite.Insert();
                            JSUtility.Alert(this, "Add To Favorite List Successfully!");
                        }
                        JSUtility.RedirectSelf(this);
                        break;
                    }
                default:
                    break;
            }
        }
    }
}