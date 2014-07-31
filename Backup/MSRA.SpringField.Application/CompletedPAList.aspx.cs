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
using MSRA.SpringField.Components.BizObjects;
using MSRA.SpringField.Components;
using MSRA.SpringField.Components.Enumerations;

namespace MSRA.SpringField.Application
{
    public partial class CompletedPAList : System.Web.UI.Page
    {
        private string Filter = "(MentorAlias = '" + SiteUser.Current.Alias.ToString() + "') AND (OverrallEvaluation > 0) AND IsApproved = 1";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (SiteUser.Current.IsInRole(MSRA.SpringField.Components.Enumerations.RoleType.InternRecruiter))
            {
                Filter = "(OverrallEvaluation > 0) AND IsApproved = 1";
            }
            else
            {
                Filter = "(MentorAlias = '" + SiteUser.Current.Alias.ToString() + "') AND (OverrallEvaluation > 0) AND IsApproved = 1";
            }
            DataSet dsPA = PerformanceAssessment.GetPerformanceAssessment();
            DataView dvPA = new DataView(dsPA.Tables[0]);
            dvPA.RowFilter = Filter;

            gvCompletedPA.DataSource = dvPA;
            gvCompletedPA.DataBind();
        }



        protected string GetApplicantLink(string ID)
        {
            return "~/ShowApplication.aspx?applicant=" + ID;
        }

        protected string GetViewLink(string ApplicantID, string PAID)
        {
            return "~/ShowApplication.aspx?applicant=" + ApplicantID + "&tab=2&PAID=" + PAID;
        }

        protected string GetEditLink(string ID)
        {
            return "~/MentorPA.aspx?PAId=" + ID;
        }

        protected string GetGroupNameByID(string ID)
        {
            string Group = "";
            try
            {
                Group = CheckInFormResourceManager.IdToText("Groups", Convert.ToInt32(ID));
            }
            catch
            {
            }

            return Group;
        }

        protected string GetPerformance(string Performance)
        {
            string strPerformance = "";
            try
            {
                strPerformance = PAResourceManager.IdToText("PerformanceLevel", Convert.ToInt32(Performance));
            }
            catch
            {
            }

            return strPerformance;
        }

        protected void gvCompletedPA_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (SiteUser.Current.IsInRole(RoleType.InternRecruiter) || SiteUser.Current.IsInRole(RoleType.OnBoardManager))
            { 
                
            }
            else
            {
                DataRowView dvRow = e.Row.DataItem as DataRowView;
                if (dvRow != null)
                {
                    Guid paid = dvRow.Row.Field<Guid>("id");
                    PerformanceAssessment pa = PerformanceAssessment.GetPerformanceAssessmentById(paid);
                    if (pa != null)
                    {
                        if (pa.IsApproved == 1)
                        {
                            e.Row.FindControl("lnkEdit").Visible = false;
                        }
                    }
                }
            }
        }

        protected void gvCompletedPA_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvCompletedPA.PageIndex = e.NewPageIndex;

            if (SiteUser.Current.IsInRole(MSRA.SpringField.Components.Enumerations.RoleType.InternRecruiter))
            {
                Filter = "(OverrallEvaluation > 0) AND IsApproved = 1";
            }
            else
            {
                Filter = "(MentorAlias = '" + SiteUser.Current.Alias.ToString() + "') AND (OverrallEvaluation > 0) AND IsApproved = 1";
            }
            DataSet dsPA = PerformanceAssessment.GetPerformanceAssessment();
            DataView dvPA = new DataView(dsPA.Tables[0]);
            dvPA.RowFilter = Filter;

            gvCompletedPA.DataSource = dvPA;
            gvCompletedPA.DataBind();
        }
    }
}