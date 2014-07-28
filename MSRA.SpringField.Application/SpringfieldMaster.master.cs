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
using MSRA.SpringField.Components.Enumerations;

namespace MSRA.SpringField.Application
{
    public partial class SpringfieldMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["contrid"] != null)
            {
                string id = Request.QueryString["contrid"].ToString();
                if (id.Length > 0)
                {
                    Session["CONTRID"] = id;
                }

                ScriptManager.RegisterStartupScript(this.Page, 
                    this.Page.GetType(), 
                    "hilight", 
                    "document.getElementById('" + Session["CONTRID"].ToString() + "').style.textDecoration='underline';", true);
            }
            else
            {
                if (Session["CONTRID"] != null)
                {
                    ScriptManager.RegisterStartupScript(this.Page,
                        this.Page.GetType(),
                        "hilight",
                        "document.getElementById('" + Session["CONTRID"].ToString() + "').style.textDecoration='underline';", true);
                }
            }

            if (SiteUser.Current.IsInRole(RoleType.InternRecruiter))
            {
                lnkDecision.Visible = true;
                lnkKeyReferral.Visible = true;
                lnkSiteRoleManager.Visible = true;
                lnkKeyin.Visible = true;
                lnkEmailTemplateEditor.Visible = true;
                lnkCheckInFormConfiguration.Visible = true;
                lnkGroupManagement.Visible = true;
                lnkUniversityManagement.Visible = true;
                lbManagement.Visible = true;
                lnkSiteAccessControl.Visible = true;
                lnkPAReport.Visible = true;
                lnkApprovingPA.Visible = true;
                lnkPaperReport.Visible = true;
                lnkHiringReport.Visible = true;
                lnkPAReportForHR.Visible = true;
                lnkSurveyReport.Visible = true;
                lnkUnsentMails.Visible = true;//查询未发邮件
            }

            if (SiteUser.Current.IsInRole(RoleType.UnivRelation))
            {
                lnkURReferral.Visible = true;//这个只有UR能看到
            }

            if (SiteUser.Current.IsInRole(RoleType.OnBoardManager))
            {
                lnkPAReport.Visible = true;
                lnkOffLineHring.Visible = false;
            }

            if (SiteUser.Current.IsInRole(RoleType.OnBoardManager) || SiteUser.Current.IsInRole(RoleType.InternRecruiter))
            {
                lnkApprovalEmail.Visible = true;
                lnkHiredCandidates.Visible = true;
                onboardmenu.Visible = true;
                lnkPAReport.Visible = true;
                lnkApprovingPA.Visible = true;
            }
            lbWelcome.Text = SiteUser.Current.FullName;

            //for performance assessment tool
            lnkPAKeyinTool.Visible = ConfigurationManager.AppSettings["enable_keyintool"].Equals("true");
        }
    }
}