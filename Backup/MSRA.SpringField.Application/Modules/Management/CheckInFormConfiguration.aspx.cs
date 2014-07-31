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
using System.Threading;
using System.Xml;
using MSRA.SpringField.Components;

namespace MSRA.SpringField.Application.Management
{
    public partial class Management_CheckInFormConfiguration : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlType.DataSource = CheckInFormResourceManager.GetTypes();
                ddlType.DataBind();
                BindData("Groups");
            }
        }

        private void BindData(string type)
        {
            gvItems.DataSource = CheckInFormResourceManager.GetTypeDataSet(type).Tables[0].DefaultView;
            gvItems.DataKeyNames = new string[] { "Id" };//key
            gvItems.Sort("Id", SortDirection.Ascending);
            gvItems.DataBind();
        }
        private string ConvertSortDirectionToSql(SortDirection sortDirection)
        {
            string m_SortDirection = String.Empty;

            switch (sortDirection)
            {
                case SortDirection.Ascending:
                    m_SortDirection = "ASC";
                    break;

                case SortDirection.Descending:
                    m_SortDirection = "DESC";
                    break;
            }
            return m_SortDirection;
        }
        protected void gvItems_Sorting(object sender, GridViewSortEventArgs e)
        {
            DataView dv = gvItems.DataSource as DataView;

            if (dv != null)
            {
                dv.Sort = e.SortExpression + " " + ConvertSortDirectionToSql(e.SortDirection);

                // gvItems.DataSource = m_DataView;
                // gvItems.DataBind();
            }
        }

        //Delete
        protected void gvItems_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            CheckInFormResourceManager.RemoveItem(ddlType.SelectedValue, gvItems.DataKeys[e.RowIndex].Value.ToString());
            BindData(ddlType.SelectedValue);
        }

        protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindData(ddlType.SelectedValue);
        }
        protected void btnNewItem_Click(object sender, EventArgs e)
        {
            CheckInFormResourceManager.NewItem(ddlType.SelectedValue);            
            BindData(ddlType.SelectedValue);
            //gvItems.PageIndex = gvItems.PageCount - 1;
        }
        //Update
        protected void gvItems_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string type = ddlType.SelectedValue;
            string id = ((Label)(gvItems.Rows[e.RowIndex].Cells[0].Controls[1])).Text.ToString().Trim();
            string name = ((TextBox)(gvItems.Rows[e.RowIndex].Cells[1].Controls[1])).Text.ToString().Trim();
            string display = ((CheckBox)(gvItems.Rows[e.RowIndex].Cells[2].Controls[1])).Checked.ToString().ToLower();
            CheckInFormResourceManager.UpdateItem(type, id, name, display);
            /*
            sqlcon = new SqlConnection(strCon);
            string sqlstr = "update ±í set ×Ö¶Î1='"
                + ((TextBox)(GridView1.Rows[e.RowIndex].Cells[1].Controls[0])).Text.ToString().Trim() + "',×Ö¶Î2='"
                + ((TextBox)(GridView1.Rows[e.RowIndex].Cells[2].Controls[0])).Text.ToString().Trim() + "',×Ö¶Î3='"
                + ((TextBox)(GridView1.Rows[e.RowIndex].Cells[3].Controls[0])).Text.ToString().Trim() + "' where id='"
                + GridView1.DataKeys[e.RowIndex].Value.ToString() + "'";
            sqlcom = new SqlCommand(sqlstr, sqlcon);
            sqlcon.Open();
            sqlcom.ExecuteNonQuery();
            sqlcon.Close();
            GridView1.EditIndex = -1;
            bind();
             */
            gvItems.EditIndex = -1;
            BindData(ddlType.SelectedValue);
        }

        //Cancel
        protected void gvItems_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvItems.EditIndex = -1;
            BindData(ddlType.SelectedValue);
        }

        //Editing
        protected void gvItems_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvItems.EditIndex = e.NewEditIndex;
            BindData(ddlType.SelectedValue);
        }

        protected void gvItems_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvItems.PageIndex = e.NewPageIndex;
            BindData(ddlType.SelectedValue);
        }
    }
}