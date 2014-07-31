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
using MSRA.SpringField.Controls;
using MSRA.SpringField.Components.BizObjects;

namespace MSRA.SpringField.Application
{
    public partial class FavoritesList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData(0, this.ApplicantsList1.PageSize, true);
            }
        }

        protected void ApplicantsList1_PagerClick(object sender, PagerEventArgs e)
        {
            BindData(e.PageIndex, e.PageSize, false);
        }

        private void BindData(Int32 pageIndex, Int32 pageSize, Boolean isRefresh)
        {
            DataView dv = new DataView(Favorite.GetUserFavorite(SiteUser.Current.SiteUserId).Tables[0]);
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

            ApplicantsList1.DataSource = dtPager;
            if (isRefresh)
            {
                this.ApplicantsList1.TotalCount = dv.Count;
            }
            ApplicantsList1.DataBind();
        }
    }
}