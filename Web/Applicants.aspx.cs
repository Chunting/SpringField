//Create by ChenYuan

using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Security.Principal;
using Springfield.Components;


public partial class Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //btnSelectDate.Attributes.Add("onclick", "popUpCalendar(this,document.all." + tbBeginDate.ClientID + ",'mm/dd/yyyy');");
        //btnSelectStartDate.Attributes.Add("onclick", "popUpCalendar(this,document.all." + tbApplyStartDate.ClientID + ",'mm/dd/yyyy');");
        //btnSelectEndDate.Attributes.Add("onclick", "popUpCalendar(this,document.all." + tbApplyEndDate.ClientID + ",'mm/dd/yyyy');");
        if (!IsPostBack)
        {
            FilterGenerator filterGenerator = new FilterGenerator();
            filterGenerator.Area = tbArea.Text;
            filterGenerator.Degree = ddlDegree.SelectedValue;
            filterGenerator.MajorCategory = ddlMajor.SelectedValue;
            filterGenerator.Status = ddlStatus.SelectedValue;
            //filterGenerator.BeginDate = tbBeginDate.Text;
            FilterExpression = filterGenerator.BuildFilterExpression();

            //bind group list
            ddlGroup.DataSource = StaticData.GroupList;
            ddlGroup.DataBind();
            ListItem allGroup = new ListItem("All", string.Empty);
            ddlGroup.Items.Insert(0, allGroup);
            allGroup.Selected = true;

            //Bind Data
           BindData(0, 20, true);
        }

        if (SiteUser.Current.IsInRole(RoleType.InternRecruiter))
        {
            //btnMultiDelete.Visible = true;
            //btnMultiRefer.Visible = true;
            ////btnRecommend.Visible = true;
            //btnDeleteSelection.Visible = true;
        }
    }

    protected void btnApplyFilter_Click(object sender, EventArgs e)
    {
        FilterGenerator filterGenerator = new FilterGenerator();
        filterGenerator.Area = tbArea.Text;
        filterGenerator.Degree = ddlDegree.SelectedValue;
        filterGenerator.MajorCategory = ddlMajor.SelectedValue;
        filterGenerator.Status = ddlStatus.SelectedValue;
        filterGenerator.FullName = tbName.Text;
        filterGenerator.University = cs.CollegeName;
        //filterGenerator.BeginDate = tbBeginDate.Text;
        //filterGenerator.ApplyStartDate = tbApplyStartDate.Text;
        //filterGenerator.ApplyEndDate = tbApplyEndDate.Text;

        if (ddlGroup.SelectedItem.Text == "All")
        {
            filterGenerator.InterestedGroup = string.Empty;
        }
        else if (ddlGroup.SelectedIndex == ddlGroup.Items.Count - 1)
        {
            if(div_OtherGorup.Visible == true && !(String.IsNullOrEmpty(tb_OtherGorup.Text.Trim())))
                filterGenerator.InterestedGroup = tb_OtherGorup.Text.Trim();
            else
                filterGenerator.InterestedGroup = string.Empty;
        }
        else
        {
            filterGenerator.InterestedGroup = ddlGroup.SelectedItem.Text;
        }
        FilterExpression = filterGenerator.BuildFilterExpression();
        BindData(0, 20, true);
    }

    public string FilterExpression
    {
        get
        {
            if (ViewState["filter"] != null)
            {
                return ViewState["filter"].ToString();
            }
            else
            {
                return string.Empty;
            }
        }
        set { ViewState["filter"] = value; }
    }

    private void BindData(Int32 pageIndex, Int32 pageSize, Boolean isRefresh)
    {
        DataView dv;
        if (tbResume.Text.Trim().Length == 0)
        {
            dv = new DataView(Applicant.GetAllApplicants().Tables[0]);
        }
        else
        {
            dv = new DataView(Applicant.GetApplicantsByResume(tbResume.Text.Trim()).Tables[0]);
        }

        //Filter
        if (this.FilterExpression != string.Empty)
        {
            dv.RowFilter = this.FilterExpression;
        }
        Int32 startIndex = pageIndex * pageSize;
        if (startIndex > dv.Count)
        {
            ApplicantsList1.DataSource = null;
            ApplicantsList1.DataBind();
            return;
        }

        Int32 endIndex = startIndex + pageSize;
        DataTable dtPager = dv.Table.Clone();

        Int32 count = -1;
        Int32 index = (endIndex > dv.Count) ? dv.Count : endIndex;
        foreach (DataRowView drv in dv)
        {
            count++;
            if (count < startIndex)
            {
                continue;
            }
            if (count >= index)
            {
                break;
            }
            DataRow dr = dtPager.NewRow();
            for (int j = 0; j < dv.Table.Columns.Count; j++)
            {
                dr[dv.Table.Columns[j].ColumnName] = drv[dv.Table.Columns[j].ColumnName];
            }
            dtPager.Rows.Add(dr);
        }
        
        //for (int i = startIndex; i < index; i++)
        //{

        //    DataRow dr = dtPager.NewRow();
        //    for (int j = 0; j < dv.Table.Columns.Count; j++)
        //    {
        //        dr[dv.Table.Columns[j].ColumnName] = dv.Table.Rows[i][j];
        //    }
        //    dtPager.Rows.Add(dr);

        //}
        ApplicantsList1.DataSource = dtPager;
        if (isRefresh)
        {
            ApplicantsList1.TotalCount = dv.Count;
        }
        
        ApplicantsList1.DataBind();
    }
    protected void ApplicantsList1_PagerClick(object sender, MSRA.ServerControls.PagerEventArgs e)
    {
        BindData(e.PageIndex, e.PageSize, false);
    }
    protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlGroup.SelectedIndex == ddlGroup.Items.Count - 1)
        {
            div_OtherGorup.Visible = true;
        }
        else
        {
            div_OtherGorup.Visible = false;
        }
    }
}