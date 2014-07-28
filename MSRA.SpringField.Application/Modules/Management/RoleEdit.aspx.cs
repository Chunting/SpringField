using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Data;
using MSRA.SpringField.Application.Config.Schemas;

namespace MSRA.SpringField.Application.Modules.Management
{
    public partial class RoleEdit : System.Web.UI.Page
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

        private void InitUI()
        {
            if (RoleID.Length > 0)
            {
                this.txtRoleName.Text = Role.RoleName;
                this.txtDesc.Text = Role.Description;

                titleLable.InnerText = "Role Edit";
            }
            else
            {
                titleLable.InnerText = "Create Role";
            }
        }

        /// <summary>
        /// Load old data to edit or create new instance to add.
        /// </summary>
        private void InitData()
        {
            if (RoleID.Length > 0)
            {
                //Edit
                Role = context.aspnet_Roles.FirstOrDefault<aspnet_Role>(p => p.RoleId.Equals(RoleID));
            }
            else
            {
                //New
                Role = new aspnet_Role();
            }
        }

        private Guid GetApplicationID()
        {
            aspnet_Application app = context.aspnet_Applications.FirstOrDefault<aspnet_Application>(p => p.ApplicationName.Equals(Membership.ApplicationName));
            if (app != null)
            {
                return app.ApplicationId;
            }
            else
            {
                return Guid.Empty;
            }
        }

        protected void btnSaveRole_Click(object sender, EventArgs e)
        {
            try
            {
                SaveRole();
            }
            catch (Exception ex)
            { 
                
            }

            Response.Redirect("RoleView.aspx?roleid=" + RoleID);
        }

        protected void btnSaveNewRole_Click(object sender, EventArgs e)
        {
            try
            {
                SaveRole();
            }
            catch (Exception ex)
            {

            }

            Response.Redirect("RoleEdit.aspx");
        }

        private void SaveRole()
        {
            #region Get UI Data
            Role.RoleName = this.txtRoleName.Text;
            Role.Description = this.txtDesc.Text;
            Role.ApplicationId =
                (context.aspnet_Applications.FirstOrDefault<aspnet_Application>(
                    p => p.ApplicationName.Equals(Membership.ApplicationName))).ApplicationId;
            Role.LoweredRoleName = this.txtRoleName.Text.ToLower();
            #endregion

            if (RoleID.Length > 0)    //Edit
            {
                Role.RoleId = new Guid(RoleID);         //Keep the RoleID when edit                
            }
            else    //Create
            {
                Role.RoleId = Guid.NewGuid();           //Create a new GUID when create new role
                RoleID = Role.RoleId.ToString();
                context.aspnet_Roles.InsertOnSubmit(Role);
            }

            context.SubmitChanges();
        }
    }
}
