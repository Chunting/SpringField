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
using MSRA.SpringField.Components;
using MSRA.SpringField.Application.Config.Schemas;
using System.Linq;

namespace MSRA.SpringField.Application.Management
{
    public partial class Management_SiteRoleManager : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!SiteUser.Current.IsInRole(RoleType.InternRecruiter))
            {
                Server.Transfer("~/AccessDeny.htm", true);
            }

            if (!IsPostBack)
            {
                BindData(ddlRoleType.SelectedValue);
            }

            this.txtAlias.Attributes.Add("onblur", "SetButtonText()");
        }

        private void BindData(string roleName)
        {
            SpringFieldDataContext ctx = new SpringFieldDataContext();
            var users = from member in ctx.aspnet_Memberships
                        join ur in ctx.aspnet_UsersInRoles on member.UserId equals ur.UserId
                        join r in ctx.aspnet_Roles on ur.RoleId equals r.RoleId
                        join u in ctx.aspnet_Users on ur.UserId equals u.UserId
                        where r.RoleName.Equals(roleName)
                        select new { member.Comment, u.UserId, u.UserName,r.RoleName };

            gvUsers.DataSource = users;
            gvUsers.DataBind();
        }


        protected void ddlRoleType_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindData(ddlRoleType.SelectedValue);
        }

        protected void gvUsers_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Remove":
                    //Remove from the list
                    GridViewRow curRow = (GridViewRow)((LinkButton)e.CommandSource).Parent.Parent;
                    //CR:can't find the alias
                    //string alias = ((DataBoundLiteralControl)curRow.Cells[1].Controls[0]).Text.Trim();
                    
                    //Literal litUserAlias = (Literal)curRow.FindControl("litUserAlias");
                    string alias = curRow.Cells[3].Text;
                    Roles.RemoveUserFromRole(alias, ddlRoleType.SelectedValue);
                    JSUtility.Alert(this, "Remove successfully!");
                    break;
                default:
                    break;
            }

            BindData(ddlRoleType.SelectedValue);
        }

        protected void btnAddUser_Click(object sender, EventArgs e)
        {
            if (this.txtAlias.Text.Trim() != string.Empty)
            {
                if (this.txtAlias.Text.Split('\\').Length != 2)
                {
                    throw new Exception("Invalid user name");
                }
                
                /*
                 * Modify by Yuanqin, 2011.5.7
                 * For adding new role, 
                 */
                //if (!Roles.IsUserInRole(this.txtAlias.Text.ToLower(), ddlRoleType.SelectedValue))
                if (!Roles.IsUserInRole(this.txtAlias.Text.ToLower(), ddlRoles.SelectedValue))
                {
                    //Roles.AddUserToRole(this.txtAlias.Text.ToLower(), ddlRoleType.SelectedValue);
                    Roles.AddUserToRole(this.txtAlias.Text.ToLower(), ddlRoles.SelectedValue);
                    SiteUser newUser = new SiteUser(this.txtAlias.Text.ToLower());
                    newUser.RealName = this.txtRealName.Text;
                    newUser.GroupName = this.ddlRoles.SelectedItem.Text;
                    newUser.UpdateCommentInfo();
                    //BindData(ddlRoleType.SelectedValue);
                    BindData(ddlRoles.SelectedValue);
                    JSUtility.Alert(this, "Add user to role successfully!");
                }
                else
                {
                    SiteUser newUser = new SiteUser(this.txtAlias.Text.ToLower());
                    newUser.RealName = this.txtRealName.Text;
                    newUser.GroupName = this.ddlRoles.SelectedItem.Text;
                    newUser.UpdateCommentInfo();
                    BindData(ddlRoleType.SelectedValue);
                    JSUtility.Alert(this, "This alias is already in current role and the user info has been updated!");
                }
            }
        }

        protected void btnDeleteRecords_Click(object sender, EventArgs e)
        {
            if (Request.Form["cb_ischeck"] != null)
            {
                string[] idArr = Request.Form["cb_ischeck"].ToString().Split(',');
                foreach (string idStr in idArr)
                {
                    Roles.RemoveUserFromRole(idStr.Trim(), ddlRoleType.SelectedValue);
                }
                JSUtility.Alert(this, "All selected items have been deleted permanently!");
                BindData(ddlRoleType.SelectedValue);
            }
            else
            {
                JSUtility.Alert(this, "None of applicant has been deleted!");
            }
        }

        protected void gvUsers_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvUsers.PageIndex = e.NewPageIndex;
            BindData(ddlRoleType.SelectedValue);
        }

        protected void gvUsers_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink lbRealName = (HyperLink)e.Row.Cells[1].FindControl("lbRealName");
                Literal litUserAlias = (Literal)e.Row.Cells[1].FindControl("litUserAlias");
                LinkButton lbEdit = (LinkButton)e.Row.Cells[gvUsers.Columns.Count - 1].FindControl("btnEdit");
                litUserAlias.Visible = false;
                Label lbGroupName = (Label)e.Row.Cells[0].FindControl("lbGroupName");
                
                string curUserName = Convert.ToString(e.Row.DataItem);
                litUserAlias.Text = curUserName;

                string comment = e.Row.DataItem.GetType()
                    .GetProperties().FirstOrDefault(p => p.Name.Equals("Comment")).GetValue(e.Row.DataItem, null).ToString();
                string rolename = e.Row.DataItem.GetType()
                    .GetProperties().FirstOrDefault(p => p.Name.Equals("RoleName")).GetValue(e.Row.DataItem, null).ToString();

                string[] strArr = SiteUser.SplitComment(comment);

                if (String.IsNullOrEmpty(strArr[0].Trim()))
                {
                    lbRealName.Text = "N/A";
                }
                else
                {
                    lbRealName.Text = strArr[0];
                }

                //if (String.IsNullOrEmpty(strArr[1].Trim()))
                //{
                //    lbGroupName.Text = "N/A";
                //}
                //else
                //{
                //    lbGroupName.Text = strArr[1];
                //}
                lbGroupName.Text = rolename;

                lbRealName.Attributes.Add("href",
                        "javascript:SetCurrentUser({'alias':'" + e.Row.Cells[3].Text.Replace("\\", "\\\\") + "','name':'" + lbRealName.Text + "','role':'" + rolename + "'})");
                lbEdit.Attributes.Add("href",
                        "javascript:SetCurrentUser({'alias':'" + e.Row.Cells[3].Text.Replace("\\", "\\\\") + "','name':'" + lbRealName.Text + "','role':'" + rolename + "'})");
                //}
            }
        }
    }
}