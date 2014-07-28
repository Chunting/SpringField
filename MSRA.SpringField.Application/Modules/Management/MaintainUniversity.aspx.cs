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
using MSRA.SpringField.Components.Enumerations;
using MSRA.SpringField.Components.BizObjects;

namespace MSRA.SpringField.Application.Management
{
    public partial class Management_MaintainUniversity : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!SiteUser.Current.IsInRole(RoleType.InternRecruiter))
            {
                Server.Transfer("~/AccessDeny.htm", true);
            }

            if (!IsPostBack)
            {
                ddlContinent.DataSource = ResourceManager.GetContinentData();
                ddlContinent.DataBind();

                ddlCountry.DataSource = ResourceManager.GetCountryData(ddlContinent.SelectedValue);
                ddlCountry.DataBind();

                BindData("All", "All");
            }
        }
        protected void ddlContinent_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlCountry.DataSource = ResourceManager.GetCountryData(ddlContinent.SelectedValue);
            ddlCountry.DataBind();

            BindData(ddlContinent.SelectedValue, ddlCountry.SelectedValue);
        }

        protected void btnAddUniversity_Click(object sender, EventArgs e)
        {
            string Continent = this.txtContinent.Text.Trim();
            string Country = this.txtRegion.Text.Trim();
            string University = this.txtUniversity.Text.Trim();
            bool fRet = false;
            try
            {
                fRet = ResourceManager.AddUniversity(Continent, Country, University);
            }
            catch (Exception)
            {
                // We won't get there.
            }
            finally
            {
                lbMessage.Text = fRet == true ? "Add university successfully." : "The university is already in the resource.";
                this.txtContinent.Text = "";
                this.txtRegion.Text = "";
                this.txtUniversity.Text = "";

                InitData();
            }
        }
        protected void btnDeleteRecords_Click(object sender, EventArgs e)
        {
            string universityString = Request.Form["cb_ischeck"];
            if (universityString != null)
            {
                string[] universityArray = universityString.Split(',');
                foreach (string idString in universityArray)
                {
                    string[] infoArray = idString.Split(':');
                    ResourceManager.DeleteUniversity(infoArray[0], infoArray[1], infoArray[2]);
                }
                JSUtility.Alert(this, "All selected items have been deleted permanently!");
                InitData();
            }
            else
            {
                JSUtility.Alert(this, "None of university has been deleted!");
            }
        }


        private void BindData(string Continent, string Country)
        {
            this.GridView1.DataSource = ResourceManager.GetUniversityDataSet(Continent, Country).Tables[0].DefaultView;
            GridView1.DataBind();
        }

        protected void gvUsers_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Remove":
                    //Remove from the list
                    GridViewRow curRow = (GridViewRow)((LinkButton)e.CommandSource).Parent.Parent;
                    ResourceManager.DeleteUniversity(curRow.Cells[1].Text, curRow.Cells[2].Text, curRow.Cells[3].Text);
                    JSUtility.Alert(this, "Remove successfully!");
                    break;
                default:
                    break;
            }

            InitData();
        }


        private void InitData()
        {
            ddlContinent.DataSource = ResourceManager.GetContinentData();
            ddlContinent.DataBind();

            ddlCountry.DataSource = ResourceManager.GetCountryData(ddlContinent.SelectedValue);
            ddlCountry.DataBind();
            BindData(ddlContinent.SelectedValue, ddlCountry.SelectedValue);
        }
        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindData(ddlContinent.SelectedValue, ddlCountry.SelectedValue);
        }
        protected void gvUsers_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            BindData(ddlContinent.SelectedValue, ddlCountry.SelectedValue);
        }
    }
}