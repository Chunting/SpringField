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
using MSRA.SpringField.Components;

namespace MSRA.SpringField.Application.Keyin
{
    public partial class UserControls_CountrySelector : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindCountryList();
            }
        }

        private void BindCountryList()
        {
            ddlCountryList.DataSource = StaticData.NationalityList;
            ddlCountryList.DataBind();
        }
    }
}