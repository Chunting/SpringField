using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using MSRA.SpringField.Components;
using MSRA.SpringField.Components.Enumerations;

namespace MSRA.SpringField.Application.Keyin
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            if (Membership.GetUser(tbEmail.Text.Trim()) == null)
            {
                try
                {
                    Membership.CreateUser(tbEmail.Text.Trim(), GlobalHelper.PasswordGenerator(7, true, true, true, false, false), tbEmail.Text.Trim());
                    Roles.AddUserToRole(tbEmail.Text.Trim(), RoleType.Applicant.ToString());
                    Response.Redirect(string.Format("ApplicantBasicInfo.aspx?email={0}", Server.UrlEncode(tbEmail.Text.Trim())));
                }
                catch (Exception ex)
                {
                    lbMsg.Text = ex.Message;
                }
            }
            else
            {
                Response.Redirect(string.Format("ApplicantBasicInfo.aspx?email={0}", Server.UrlEncode(tbEmail.Text.Trim())));
            }
        }
        //protected void btnTest_Click(object sender, EventArgs e)
        //{
        //    lbTest.Text = "Category:" + isg.Category + " Source:" + isg.Source + " Channel:" + isg.Channel + " Detail:" + isg.Detail;
        //}
    }
}