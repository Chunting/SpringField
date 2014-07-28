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
using System.Collections.Generic;
using Springfield.Components;

public partial class Management_SiteAccessControl : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!SiteUser.Current.IsInRole(RoleType.InternRecruiter))
        {
            Server.Transfer("~/AccessDeny.htm", true);
        }

        if (!IsPostBack)
        {
            InitRoleList();
 
        }
    }
    protected void btn_AddRole_Click(object sender, EventArgs e)
    {
        string domainAlias = tb_alias.Text.Trim();
        string group = ddl_Roles.SelectedValue;

        string[] sections = null;
        if(domainAlias.IndexOf('\\') != -1)
        {
            sections = domainAlias.Split('\\');
        }
        else
        {
            lb_msg.Text = String.Format("{0} is not a valid domain alias.", domainAlias);
            return;
        }
        
        if(!Roles.IsUserInRole(domainAlias, group))
        {
            if(Roles.IsUserInRole(domainAlias, RoleType.AnonymousUser.ToString()))
            {
                Roles.RemoveUserFromRole(domainAlias, RoleType.AnonymousUser.ToString());
            }

            if (!StaticData.FTEDict.ContainsKey(sections[1]))
            { 
                // add alias to site resource
                try
                {
                    // Add to resource by alphabetic order
                    ResourceManager.AddAlias(sections[1]);
                }
                catch
                {
                    // the alias has already in the resource list.
                }
            }

            Roles.AddUserToRole(domainAlias, group);
            lb_msg.Text = String.Format("{0} has been added in the {1} group successfully.", domainAlias, group);
        }
        else
        {
            lb_msg.Text = String.Format("{0} is already in the {1} group.", domainAlias, group);
        }

    }

    private void InitRoleList()
    {
        List<string> rgRoles = new List<string>(Roles.GetAllRoles());
        rgRoles.Remove("GroupManager");
        rgRoles.Remove("UnivRelation");
        rgRoles.Remove("OnBoardManager");
        rgRoles.Remove("InternRecruiter");
        ddl_Roles.DataSource = rgRoles;
        ddl_Roles.DataBind();
         
    }
}
