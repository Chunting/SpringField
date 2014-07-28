using System;
using System.Web.Security;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using MSRA.SpringField.Components;
using MSRA.SpringField.Components.Enumerations;
using MSRA.SpringField.Components.BizObjects;
using MSRA.SpringField.Application.Config.Schemas;
using System.Linq;
using System.Reflection;

namespace MSRA.SpringField.Application.Management
{
    public partial class Management_SiteAccessControl : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!SiteUser.Current.IsInRole(RoleType.InternRecruiter))//验证权限
            {
                Server.Transfer("~/AccessDeny.htm", true);
            }

            if (!IsPostBack)
            {
                InitRoleList();
                InitGridView();

                this.tb_alias.Text = "";    
            }       
        }
        protected void btn_AddRole_Click(object sender, EventArgs e)
        {
            string domainAlias = tb_alias.Text.Trim();
            string group = ddl_Roles.SelectedValue;

            string[] sections = null;

            if (domainAlias.IndexOf('\\') != -1)
            {
                sections = domainAlias.Split('\\');
            }
            else
            {
                lb_msg.Text = String.Format("{0} is not a valid domain alias.", domainAlias);
                return;
            }
            
            //check if the domainAlias is in the specific group
            if (!Roles.IsUserInRole(domainAlias, group))
            {
                //Remove the anonymousUser role from the domainAlias if the domainAlias is in anonymousUser already.
                if (Roles.IsUserInRole(domainAlias, RoleType.AnonymousUser.ToString()))
                {
                    Roles.RemoveUserFromRole(domainAlias, RoleType.AnonymousUser.ToString());
                }

                //check if the alias is in the FTE list in SiteResources.xml file.
                //Add the alias to FTE list if it is not in the list.
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
                        JSUtility.Alert(this, "the alias has already in the resource list.");
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

        /// <summary>
        /// @Author: Yin.P
        /// </summary>
        private void InitGridView()
        {
            try
            {
                SpringFieldDataContext membershipCtx = new SpringFieldDataContext();
                if (this.txtConditionValue.Text.Length > 0)
                {
                    if (this.ddlFilterCondition.SelectedValue.Equals("UserName"))
                    {
                        var result = from user in membershipCtx.aspnet_Users
                                     join userrole in membershipCtx.aspnet_UsersInRoles on user.UserId equals userrole.UserId
                                     join role in membershipCtx.aspnet_Roles on userrole.RoleId equals role.RoleId
                                     where user.UserName.Contains(this.txtConditionValue.Text) &&
                                            role.RoleName.Equals("GroupManager") == false &&
                                            role.RoleName.Equals("UnivRelation") == false &&
                                            role.RoleName.Equals("OnBoardManager") == false &&
                                            role.RoleName.Equals("InternRecruiter") == false && 
                                            role.RoleName.Equals("Applicant") == false
                                     select new { user.UserId, role.RoleId, user.UserName, role.RoleName, role.Description };
                        this.gvRoleList.DataSource = result;
                        this.gvRoleList.DataBind();
                        this.lblRecordSize.Text = result.Count().ToString();
                    }
                    else
                    {
                        var result = from user in membershipCtx.aspnet_Users
                                     join userrole in membershipCtx.aspnet_UsersInRoles on user.UserId equals userrole.UserId
                                     join role in membershipCtx.aspnet_Roles on userrole.RoleId equals role.RoleId
                                     where role.RoleName.Contains(this.txtConditionValue.Text) &&
                                                role.RoleName.Equals("GroupManager") == false &&
                                                role.RoleName.Equals("UnivRelation") == false &&
                                                role.RoleName.Equals("OnBoardManager") == false &&
                                                role.RoleName.Equals("InternRecruiter") == false &&
                                            role.RoleName.Equals("Applicant") == false
                                     select new { user.UserId, role.RoleId, user.UserName, role.RoleName, role.Description };
                        this.gvRoleList.DataSource = result;
                        this.gvRoleList.DataBind();
                        this.lblRecordSize.Text = result.Count().ToString();
                    }
                }
                else
                {
                    var resu = from user in membershipCtx.aspnet_Users
                               join userrole in membershipCtx.aspnet_UsersInRoles on user.UserId equals userrole.UserId
                               join role in membershipCtx.aspnet_Roles on userrole.RoleId equals role.RoleId
                               where role.RoleName.Equals("GroupManager") == false &&
                                        role.RoleName.Equals("UnivRelation") == false &&
                                        role.RoleName.Equals("OnBoardManager") == false &&
                                        role.RoleName.Equals("InternRecruiter") == false &&
                                            role.RoleName.Equals("Applicant") == false
                               select new { user.UserId, role.RoleId, user.UserName, role.RoleName, role.Description };
                    this.gvRoleList.DataSource = resu;
                    this.gvRoleList.DataBind();
                    this.lblRecordSize.Text = resu.Count().ToString();
                }
            }
            catch (Exception)
            {
                throw;
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

        protected void gvRoleList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvRoleList.PageIndex = e.NewPageIndex;
            InitGridView();
        }

        /// <summary>
        /// @Author: Yin.P
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void btnSearch_Click(object sender, EventArgs e)
        {
            InitGridView();            
        }

        protected void gvGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                
            }
        }

        /*
         * Add by Yuanqin, 2011.5.7
         * For delete button
         */
        protected void gvRoleList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Remove":
                    //Remove from the list
                    GridViewRow curRow = (GridViewRow)((LinkButton)e.CommandSource).Parent.Parent;
                    //CR:can't find the alias
                    //string alias = ((DataBoundLiteralControl)curRow.Cells[1].Controls[0]).Text.Trim();

                    //Literal litUserAlias = (Literal)curRow.FindControl("litUserAlias");
                    string alias = curRow.Cells[0].Text;
                    string roleName = curRow.Cells[1].Text;
                    Roles.RemoveUserFromRole(alias, roleName);
                    JSUtility.Alert(this, "Remove successfully!");
                    break;
                default:
                    break;
            }
            InitGridView();
        }

    }
}