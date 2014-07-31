using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MSRA.SpringField.Application.Config.Schemas;
using MSRA.SpringField.Components;

namespace MSRA.SpringField.Application.Modules.Management
{
    public partial class RoleList : System.Web.UI.Page
    {
        private SpringFieldDataContext context = new SpringFieldDataContext();

        private IQueryable<aspnet_Role> roleList = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                InitData();
                InitUI();
            }
        }

        private void InitUI()
        {
            this.gvRoles.DataSource = roleList;
            this.gvRoles.DataBind();
        }

        private void InitData()
        {
            roleList = from role in context.aspnet_Roles select role;
        }

        protected void gvRoles_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Remove":
                    //Remove from the list
                    context.aspnet_Roles.DeleteOnSubmit(
                        context.aspnet_Roles.FirstOrDefault<aspnet_Role>(p => p.RoleId.Equals(e.CommandArgument.ToString())));
                    context.SubmitChanges();
                    break;
                case "Edit":
                    //Edit current record
                    Response.Redirect("RoleEdit.aspx?roleid=" + e.CommandArgument.ToString());
                    break;
                default:
                    break;
            }

            InitData();
            InitUI();
        }

        protected void ibSearch_Click(object sender, EventArgs e)
        { 
            //this.txtRoleName.Text
            roleList = from role in context.aspnet_Roles
                        where role.RoleName.Contains(this.txtRoleName.Text) || role.LoweredRoleName.Contains(this.txtRoleName.Text)
                        select role;

            InitUI();
        }

        protected void gvRoles_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvRoles.PageIndex = e.NewPageIndex;
            InitData();
            InitUI();
        }

        protected void btnDeleteRecords_Click(object sender, EventArgs e)
        {
            if (Request.Form["cb_ischeck"] != null)
            {
                string[] idArr = Request.Form["cb_ischeck"].ToString().Split(',');
                foreach (string idStr in idArr)
                {
                    context.aspnet_Roles.DeleteOnSubmit(
                        context.aspnet_Roles.FirstOrDefault<aspnet_Role>(p => p.RoleId.Equals(idStr)));
                }

                context.SubmitChanges();
                InitData();
                InitUI();
            }
            else
            {
                JSUtility.Alert(this, "None of role has been deleted!");
            }
        }
    }
}
