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

public partial class MultiReferral : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //lbxApplicantList.Enabled = false;
        if (!IsPostBack)
        { 
            if (Session["referrals"] != null)
            {
                ApplicantBasicInfo abi = null;
                string[] idArr = (string[])Session["referrals"];
                foreach (string applicantId in idArr)
                {
                    abi = ApplicantBasicInfo.GetApplicantBasicInfoById(new Guid(applicantId));
                    literalApplicantList.Text += abi.FirstName;
                    literalApplicantList.Text += " ";
                    literalApplicantList.Text += abi.LastName;
                    literalApplicantList.Text += "<br/>";
                    //lbxApplicantList.Items.Add(abi.FirstName + abi.LastName);
                }
            }
            else
            {
                btnRefer.Enabled = false;
                JSUtility.Alert(this, "You can't use this function without selecting applicants from the main page!");
                JSUtility.CloseWindow(this);
                btnRefer.Visible = false;
            }        
        }
    }

    protected void btnRefer_Click(object sender, EventArgs e)
    {
        if (Session["referrals"] != null)
        {
            if (string.IsNullOrEmpty(tbAccepters.Text.Trim()))
            {
                JSUtility.Alert(this, "Accepter alias should not be empty!");
            }
            else
            {
                Guid applicantId = Guid.Empty;
                string[] referreeArr = (string[])Session["referrals"];

                List<string> acceptersArr = GlobalHelper.FormatAlias(tbAccepters.Text);


                SiteConfiguration config = SiteConfiguration.GetConfig();

                foreach (string referree in referreeArr)
                {
                    applicantId = new Guid(referree);

                    foreach (string accpter in acceptersArr)
                    {
                        //Referral curReferral = new Referral();
                        //curReferral.ReferredTime = DateTime.Now;
                        //curReferral.ApplicantId = applicantId;
                        //curReferral.ReferrerId = SiteUser.Current.SiteUserId;
                        //curReferral.Insert();

                        //send email to accepter
                        MailHelper mailHelper = new MailHelper();
                        mailHelper.AddApplicantVariables(applicantId);
                        mailHelper.AddReferToAliasVariables(accpter);
                        mailHelper.SendMail(MailType.ReferTo);
                    }
                }
            }

            Session["referrals"] = null;
            btnRefer.Visible = false;
            tbAccepters.ReadOnly = true;
            JSUtility.Alert(this, @"Referral email has been sent.\nthis window will be closed.");
            JSUtility.CloseWindow(this);
        }
    }
}
