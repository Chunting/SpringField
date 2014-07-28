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
using MSRA.SpringField.Components.Enumerations;
using MSRA.SpringField.Components.BizObjects;

namespace MSRA.SpringField.Application.Controls
{
    public partial class Controls_TopMenu : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (SiteUser.Current.IsInRole(RoleType.InternRecruiter.ToString()))
            {
                lnkDecision.Visible = true;
                lnkKeyReferral.Visible = true;
                lnkManagement.Visible = true;
            }

            if (SiteUser.Current.IsInRole(RoleType.UnivRelation.ToString()))
            {
                lnkURReferral.Visible = true;
            }
            lbWelcome.Text = SiteUser.Current.FullName;
        }
    }
}