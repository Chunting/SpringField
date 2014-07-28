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

namespace MSRA.SpringField.Application
{
    public partial class UncompletedPAList : System.Web.UI.Page
    {
        private string Filter = "(MentorAlias = '" + SiteUser.Current.Alias.ToString() + "') AND (OverrallEvaluation < 1)";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (SiteUser.Current.IsInRole(MSRA.SpringField.Components.Enumerations.RoleType.InternRecruiter))
            {
                Filter = "(OverrallEvaluation < 1) or (IsApproved = 0)";
            }
            else
            {
                Filter = "(MentorAlias = '" + SiteUser.Current.Alias.ToString() + "') AND ((OverrallEvaluation < 1)  or (IsApproved = 0))";
            }
            DataSet dsPA = PerformanceAssessment.GetPerformanceAssessment();
            DataView dvPA = new DataView(dsPA.Tables[0]);
            dvPA.RowFilter = Filter;

            gvCompletedPA.DataSource = dvPA;
            gvCompletedPA.DataBind();
        }

        protected string GetApplicantLink(string ID)
        {
            /*
             * PA Approval
             * Author: Yin.P
             * Date: 2010-1-5
             */
            return "~/ShowApplication.aspx?applicant=" + ID + "&tab=2";
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
    }
}