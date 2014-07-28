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

public partial class SpringfieldMaster : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
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
        }

        if (SiteUser.Current.IsInRole(RoleType.UnivRelation))
        {
            lnkURReferral.Visible = true;
        }

        if(SiteUser.Current.IsInRole(RoleType.OnBoardManager))
        {
            lnkPersonKeyin.Visible = true;
            lnkOffLineHring.Visible = false;           
        }
        if(SiteUser.Current.IsInRole(RoleType.OnBoardManager) || SiteUser.Current.IsInRole(RoleType.InternRecruiter))
        {
            lnkApprovalEmail.Visible = true;
            lnkHiredCandidates.Visible = true;
        }
        lbWelcome.Text = SiteUser.Current.FullName;
    }
}
