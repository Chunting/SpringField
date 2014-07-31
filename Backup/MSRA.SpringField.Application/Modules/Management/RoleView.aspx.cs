using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MSRA.SpringField.Application.Config.Schemas;

namespace MSRA.SpringField.Application.Modules.Management
{
    public partial class RoleView : System.Web.UI.Page
    {
        private SpringFieldDataContext context = new SpringFieldDataContext();

        public aspnet_Role Role
        {
            get;
            set;
        }

        public string RoleID
        {
            get
            {
                if (Request.QueryString["roleid"] != null)
                {
                    return Request.QueryString["roleid"].ToString();
                }
                else if (ViewState["roleid"] != null)
                {
                    return ViewState["roleid"].ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
            set
            {
                ViewState["roleid"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            InitData();
            if (!Page.IsPostBack)
            {
                InitUI();
            }
            else
            {

            }
        }

        private void InitData()
        {
            Role = context.aspnet_Roles.FirstOrDefault<aspnet_Role>(p => p.RoleId.Equals(RoleID));
        }

        private void InitUI()
        {
            if (Role != null)
            {
                this.lblRoleName.Text = Role.RoleName;
                this.lblDesc.Text = Role.Description;
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                context.aspnet_Roles.DeleteOnSubmit(Role);
                context.SubmitChanges();
            }
            catch (Exception ex)
            {
                
            }

            Response.Redirect("RoleList.aspx");
        }
    }
}
