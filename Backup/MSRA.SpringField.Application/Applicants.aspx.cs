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
using MSRA.SpringField.Controls;
using MSRA.SpringField.Components.BizObjects;
using MSRA.SpringField.Components;
using MSRA.SpringField.Components.Enumerations;

namespace MSRA.SpringField.Application
{
    public partial class Applicants : System.Web.UI.Page
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
                /*
                 * Add TimeSpan by Yuanqin, 2011.3.15
                 */
                filterGenerator.TimeSpan = ddlTimeSpan.SelectedValue;
                /*
                 * Add Position by Yuanqin, 2011.3.15
                 */
                filterGenerator.Position = ddlPosition.SelectedValue;

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

            /*
             * Add by Yuanqin
             * 2011.1.24
             */
            //btnEnrollDate.Attributes.Add("onclick", "popUpCalendar(this,document.all." + tbBeginDate.ClientID + ",'yyyy-mm-dd');");
            tbBeginDate.Attributes.Add("onclick", "popUpCalendar(this,document.all." + tbBeginDate.ClientID + ",'yyyy-mm-dd');");

            /*
             * Add by Yuanqin
             * 2011.3.15
             */
            tbStartApplyDate.Attributes.Add("onclick", "popUpCalendar(this,document.all." + tbStartApplyDate.ClientID + ",'yyyy-mm-dd');");
            tbEndApplyDate.Attributes.Add("onclick", "popUpCalendar(this,document.all." + tbEndApplyDate.ClientID + ",'yyyy-mm-dd');");

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
            filterGenerator.University = cs.CollegeName.Replace("'","''"); //Finish the bug by Mingming 2012-09-07 in dataview just us '' instead of '

            /*
             * Add by Yuanqin
             * 2011.1.24
             */
            filterGenerator.BeginDate = tbBeginDate.Text;

            /*
             * Add by Yuanqin
             * 2011.3.15
             */
            filterGenerator.ApplyStartDate = tbStartApplyDate.Text;
            filterGenerator.ApplyEndDate = tbEndApplyDate.Text;
            filterGenerator.TimeSpan = ddlTimeSpan.SelectedValue;
            /*
             * Add by Yuanqin
             * 2011.4.22
             */
            filterGenerator.Position = ddlPosition.SelectedValue;

            if (ddlGroup.SelectedItem.Text == "All")
            {
                filterGenerator.InterestedGroup = string.Empty;
            }
            else if (ddlGroup.SelectedIndex == ddlGroup.Items.Count - 1)
            {
                if (div_OtherGorup.Visible == true && !(String.IsNullOrEmpty(tb_OtherGorup.Text.Trim())))
                    filterGenerator.InterestedGroup = tb_OtherGorup.Text.Trim();
                else
                    filterGenerator.InterestedGroup = string.Empty;
            }
            else
            {               
                //filterGenerator.InterestedGroup = ddlGroup.SelectedItem.Text;

                //Modify by Yuanqin, 2011.2.16
                filterGenerator.InterestedGroup = ddlGroup.SelectedItem.Text.Replace(@"'", "''");
            }
            if (this.ddlStatus.SelectedValue.Equals("1"))
            {
                filterGenerator.filterTable["status"] =
                   "(" + filterGenerator.filterTable["status"] + "OR (status=9)" + " OR (status=5))";
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
                //All
                if (this.ddlStatus.SelectedValue.Length == 0)
                {
                    dv = new DataView(Applicant.GetAllApplicantsWithoutPermissionFilter().Tables[0]);
                }
                else
                {
                    if (SiteUser.Current.IsInRole(RoleType.InternRecruiter) == false)
                    {
                        dv = new DataView(Applicant.GetApplicantsByCondition().Tables[0]);
                    }
                    else
                    {
                        dv = new DataView(Applicant.GetAllApplicantsWithoutPermissionFilter().Tables[0]);
                    }
                }
            }
            else
            {
                dv = new DataView(Applicant.GetApplicantsByResume(tbResume.Text.Trim()).Tables[0]);
            }

            //Add by Yuanqin,2011.1.24
            dv.Sort = "ApplicationDate DESC";

            if (tbResume.Text.Trim().Length == 0 &&
                SiteUser.Current.IsInRole(RoleType.InternRecruiter) == false &&
                int.Parse(this.ddlStatus.SelectedValue.Length == 0 ? "0" : this.ddlStatus.SelectedValue) >= 2)
            {
                this.FilterExpression += string.Format(" AND (IsComplete={0})", int.Parse(this.ddlStatus.SelectedValue) > 2 ? 1 : 0);
                if (SiteUser.Current.IsInRole(RoleType.HiringManager))
                {
                    //this.FilterExpression += string.Format(" AND (IsComplete={0})", int.Parse(this.ddlStatus.SelectedValue) > 2 ? 1 : 0);
                    this.FilterExpression += string.Format(" AND (HiringManagerId='{0}')", SiteUser.Current.SiteUserId.ToString());
                }

                if (SiteUser.Current.IsInRole(RoleType.GroupManager))
                {
                    this.FilterExpression += string.Format(" AND (GroupManagerId='{0}')", SiteUser.Current.SiteUserId.ToString());
                }

                if (SiteUser.Current.IsInRole(RoleType.DefaultUser))
                {
                    this.FilterExpression += string.Format(" AND (intervieweralias='{0}')", SiteUser.Current.Alias);
                }
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
        protected void ApplicantsList1_PagerClick(object sender, PagerEventArgs e)
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
}