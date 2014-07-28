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
    
    protected void btnAddGroup_Click(object sender, EventArgs e)
    {
        string Group = tbGroup.Text.Trim();
        bool fRet = false;
        try
        {
            fRet = ResourceManager.AddGroup(Group);
        }
        catch (Exception)
        {
            // We won't get there.
        }
        finally
        {
            lbMessage.Text = fRet == true ? "Add group successfully." : "The group is already in the resource.";
            tbGroup.Text = "";
            InitData();
        }
    }
    protected void btnDeleteRecords_Click(object sender, EventArgs e)
    {
        string groupString = Request.Form["cb_ischeck"];
        if (groupString != null)
        {
            string[] groupArray = groupString.Split(',');
            foreach (string idString in groupArray)
            {
                //string[] infoArray = idString.Split(':');
                ResourceManager.DeleteGroup(idString);
            }
            JSUtility.Alert(this, "All selected items have been deleted permanently!");
            InitData();
        }
        else
        {
            JSUtility.Alert(this, "None of group has been deleted!");
        }        
    }

    private void BindData()
    {
        gvUsers.DataSource = ResourceManager.GetGroupDataSet().Tables[0].DefaultView;
        gvUsers.DataBind();
    }


    private void InitData()
    {
        BindData();
    }

    protected void gvUsers_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvUsers.PageIndex = e.NewPageIndex;
        BindData();
    }
}
