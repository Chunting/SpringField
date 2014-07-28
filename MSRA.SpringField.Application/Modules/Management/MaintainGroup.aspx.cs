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
using System.Text;
using MSRA.SpringField.Components;
using MSRA.SpringField.Components.BizObjects;
using MSRA.SpringField.Components.Enumerations;
using System.Linq;

namespace MSRA.SpringField.Application.Management
{
    public partial class Management_MaintainGroup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!SiteUser.Current.IsInRole(RoleType.InternRecruiter))
            {
                Server.Transfer("~/AccessDeny.htm", true);
            }

            if (!IsPostBack)
            {
                BindData();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            DataTable dt = ResourceManager.GetGroupDataSet().Tables[0];
            EnumerableRowCollection<DataRow> dtRows = dt.AsEnumerable();
            if (this.txtGroupFilterValue.Text.Trim().Length == 0)
            {
                InitData();
            }
            else
            {
                var result = from row in dtRows
                             where row.Field<string>("Group").Contains(this.txtGroupFilterValue.Text.Trim())
                             select row;
                this.gvGroup.DataSource = result.AsDataView<DataRow>();
                this.gvGroup.DataBind();
            }
        }

        protected void btnAddGroup_Click(object sender, EventArgs e)
        {
            string Group = txtGroup.Text.Trim();
            bool fRet = false;
            try
            {
                fRet = ResourceManager.AddGroup(Group, this.txtShortName.Text);

                CheckInFormResourceManager.CreateGroupItem(this.txtShortName.Text);
            }
            catch (Exception)
            {
                // We won't get there.
            }
            finally
            {
                lblMessage.Text = fRet == true ? "Add group successfully." : "The group is already in the resource.";
                txtGroup.Text = "";
                InitData();
            }
        }

        protected void gvGroup_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Remove":
                    //Remove from the list
                    GridViewRow curRow = (GridViewRow)((LinkButton)e.CommandSource).Parent.Parent;
                    ResourceManager.DeleteGroup(curRow.Cells[1].Text);
                    lblMessage.Text = "Remove successfully!";
                    JSUtility.Alert(this, "Remove successfully!");
                    break;
                default:
                    break;
            }

            InitData();
        }

        protected void btnDeleteRecords_Click(object sender, EventArgs e)
        {
            string groupString = Request.Form["cb_ischeck"];
            if (groupString != null)
            {
                string[] groupArray = groupString.Split(',');
                foreach (string idString in groupArray)
                {
                    ResourceManager.DeleteGroup(idString);
                }
                lblMessage.Text = "All selected items have been deleted permanently!";
                JSUtility.Alert(this, "All selected items have been deleted permanently!");
                InitData();
            }
            else
            {
                lblMessage.Text = "None of group has been deleted!";
                JSUtility.Alert(this, "None of group has been deleted!");
            }
        }

        private void BindData()
        {
            gvGroup.DataSource = ResourceManager.GetGroupDataSet().Tables[0].DefaultView;
            gvGroup.DataBind();
        }
        
        private void InitData()
        {
            BindData();
        }

        protected void gvGroup_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvGroup.PageIndex = e.NewPageIndex;
            BindData();
        }
    }
}