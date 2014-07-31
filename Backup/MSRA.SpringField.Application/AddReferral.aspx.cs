using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using MSRA.SpringField.Components;
using MSRA.SpringField.Components.Enumerations;
using MSRA.SpringField.Components.Configuration;

namespace MSRA.SpringField.Application
{
    public partial class AddReferral : System.Web.UI.Page
    {
        Guid applicantId = Guid.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.UrlReferrer != null)
                {
                    ViewState["PrevPage"] = Request.UrlReferrer.ToString();
                }
            }

            if (Request["applicant"] != null)
            {
                string idStr = GlobalHelper.ClearInput(Request["applicant"].ToString(), 36, false);
                if (!string.IsNullOrEmpty(idStr))
                {
                    applicantId = new Guid(idStr);
                    basicInfo.ApplicantId = applicantId;
                    basicInfo.BindData();
                    return;
                }
            }
            btnAddReferral.Visible = false;
            JSUtility.Alert(this, "Invalid parameter!");
            JSUtility.CloseWindow(this);

        }

        protected void btnAddReferral_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbAccepters.Text))
            {
                JSUtility.Alert(this, "Recipient's alias should not be empty!");
            }
            else
            {
                List<string> aliasArr = GlobalHelper.FormatAlias(tbAccepters.Text);
                SiteConfiguration config = SiteConfiguration.GetConfig();

                //Send email to the employee whom is refer to
                MailHelper mailHelper = new MailHelper();
                mailHelper.AddApplicantVariables(applicantId);
                foreach (string alias in aliasArr)
                {
                    mailHelper.AddReferToAliasVariables(alias);
                    mailHelper.SendMail(MailType.ReferTo);
                }

                btnAddReferral.Visible = false;
                tbAccepters.ReadOnly = true;
                //JSUtility.Alert(this, "A referral email has been sent to each alias.");
                //JSUtility.CloseWindow(this);
                Response.Redirect(ViewState["PrevPage"].ToString(), true);
            }
        }
    }
}