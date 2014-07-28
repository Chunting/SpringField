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
using Springfield.Components;

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
        string Continent = tbContinent.Text.Trim();
        string Country = tbCountry.Text.Trim();
        string University = tbUniversity.Text.Trim();
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
            lbMessage.Text = fRet==true ? "Add university successfully." : "The university is already in the resource.";
            tbContinent.Text = "";
            tbCountry.Text = "";
            tbUniversity.Text = "";

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
        gvUsers.DataSource = ResourceManager.GetUniversityDataSet(Continent, Country).Tables[0].DefaultView;
        gvUsers.DataBind();
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
        gvUsers.PageIndex = e.NewPageIndex;
        BindData(ddlContinent.SelectedValue, ddlCountry.SelectedValue);
    }
}
