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
using Springfield.Components;

public partial class ApplicantInformationView : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        MembershipUser curUser = Membership.GetUser();
        Guid curId = SiteUser.GetIdByFullName(curUser.UserName);

        ApplicantBasicInfo abi = ApplicantBasicInfo.GetApplicantBasicInfoById(curId);
        ApplicantEduBackground aeb = ApplicantEduBackground.GetApplicantEduBackgroundById(curId);
        ApplicantRelatedInfo ari = ApplicantRelatedInfo.GetApplicantRelatedInfoById(curId);

        if (abi == null)
        {
            lbBasicInfo.Text = "Incomplete";
        }
        else
        {
            lbBasicInfo.Text = "Completed";
        }

        if (aeb == null)
        {
            lbEduBackground.Text = "Incomplete";
        }
        else
        {
            lbEduBackground.Text = "Completed";
        }

        if (ari == null)
        {
            lbRelatedInfo.Text = "Incomplete";
        }
        else
        {
            lbRelatedInfo.Text = "Completed";
        }

        if (abi == null || aeb == null || ari == null)
        {
            lbStatus.Text = "Application Incomplete! You won't be screened out from the system!";
        }
        else
        {
            lbStatus.Text = "Congratulations! Application is completed!";
        }
    }
}
