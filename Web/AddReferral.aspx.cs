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
using Springfield.Components;
using Springfield.Components.Configuration;

public partial class AddReferral : System.Web.UI.Page
{
    Guid applicantId = Guid.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
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
            //btnAddReferral.Visible = false;
            JSUtility.Alert(this, "Recipient's alias should not be empty!");
        }
        else
        {
            List<string> aliasArr = GlobalHelper.FormatAlias(tbAccepters.Text);

            //Change applicant status to Referring
            //Add Referral
            //Send Referral email to Accepters
            //Referral curReferral = new Referral();
            //curReferral.ReferredTime = DateTime.Now;
            //curReferral.ApplicantId = applicantId;
            //curReferral.ReferrerId = SiteUser.Current.SiteUserId;
            //if(SiteUser.Current.IsInRole(RoleType.InternRecruiter))
            //{
            //    curReferral.Type = ReferralType.KeyReferral;
            //}
            //else
            //{
            //    curReferral.Type = ReferralType.PersonalReferral;
            //}
            //curReferral.Insert();

            //ApplicantBasicInfo.ChangeApplicantStatus(applicantId, ApplicationStatusEnum.KeyReferring);
            SiteConfiguration config = SiteConfiguration.GetConfig();

            //Send email to the employee whom is refer to
            MailHelper mailHelper = new MailHelper();
            mailHelper.AddApplicantVariables(applicantId);
            foreach (string alias in aliasArr)
            {
                mailHelper.AddReferToAliasVariables(alias);
                mailHelper.SendMail(MailType.ReferTo);
                //MailHelper.SendReferToMail(alias, applicantId);
            }

            btnAddReferral.Visible = false;
            tbAccepters.ReadOnly = true;
            JSUtility.Alert(this, "A referral email has been sent to each alias.\nPlease close this window!");
            JSUtility.CloseWindow(this);
        }
    }
}
