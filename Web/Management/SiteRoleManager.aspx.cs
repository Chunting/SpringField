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

public partial class Management_SiteRoleManager : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Response.Write(HttpContext.Current.User.IsInRole(RoleType.GroupManager));
        //foreach (string role in SiteUser.Current.GetRoles())
        //{
        //    Response.Write(role);
        //}
        
        if(!SiteUser.Current.IsInRole(RoleType.InternRecruiter))
        {
            Server.Transfer("~/AccessDeny.htm", true);
        }

        if (!IsPostBack)
        {
            BindData(ddlRoleType.SelectedValue);
        }
    }

    private void BindData(string roleName)
    {
        string[] userList = Roles.GetUsersInRole(roleName);
        gvUsers.DataSource = userList;
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
                GridViewRow curRow = (GridViewRow)((Button)e.CommandSource).Parent.Parent;
                //CR:can't find the alias
                //string alias = ((DataBoundLiteralControl)curRow.Cells[1].Controls[0]).Text.Trim();
                Literal litUserAlias = (Literal)curRow.FindControl("litUserAlias");
                string alias = litUserAlias.Text;
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
        if (tbNewUser.Text.Trim() != string.Empty)
        {
            if (tbNewUser.Text.Split('\\').Length != 2)
            {
                throw new Exception("Invalid user name");
            }
            if (!Roles.IsUserInRole(tbNewUser.Text.ToLower(), ddlRoleType.SelectedValue))
            {
                Roles.AddUserToRole(tbNewUser.Text.ToLower(), ddlRoleType.SelectedValue);
                SiteUser newUser = new SiteUser(tbNewUser.Text.ToLower());
                newUser.RealName = tbRealName.Text;
                newUser.GroupName = tbGroupName.Text;
                newUser.UpdateCommentInfo();
                BindData(ddlRoleType.SelectedValue);
                JSUtility.Alert(this, "Add user to role successfully!");
            }
            else
            {
                SiteUser newUser = new SiteUser(tbNewUser.Text.ToLower());
                newUser.RealName = tbRealName.Text;
                newUser.GroupName = tbGroupName.Text;
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
            Label lbRealName = (Label)e.Row.Cells[1].FindControl("lbRealName");
            Literal litUserAlias = (Literal)e.Row.Cells[1].FindControl("litUserAlias");
            litUserAlias.Visible = false;
            Label lbGroupName = (Label)e.Row.Cells[0].FindControl("lbGroupName");

            string curUserName = Convert.ToString(e.Row.DataItem);
            litUserAlias.Text = curUserName;
            MembershipUser curUser = Membership.GetUser(curUserName);
            if (curUser == null)
            {
                lbRealName.Text = "N/A";
                lbGroupName.Text = "N/A";
            }
            else
            {
                string[] strArr = SiteUser.SplitComment(curUser.Comment);

                if (String.IsNullOrEmpty(strArr[0].Trim()))
                {
                    lbRealName.Text = "N/A";
                }
                else
                {
                    lbRealName.Text = strArr[0];
                }

                if (String.IsNullOrEmpty(strArr[1].Trim()))
                {
                    lbGroupName.Text = "N/A";
                }
                else
                {
                    lbGroupName.Text = strArr[1];
                }
            }
        }
    }
}
