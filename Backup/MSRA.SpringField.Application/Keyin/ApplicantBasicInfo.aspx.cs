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
using MSRA.SpringField.Components.BizObjects;
using MSRA.SpringField.Components.Enumerations;

namespace MSRA.SpringField.Application.Keyin
{
    public partial class PageApplicantBasicInfo : System.Web.UI.Page
    {
        bool isUpdate = false;
        Guid curId = Guid.Empty;
        ApplicantBasicInfo abi = null;
        MembershipUser curUser = null;
        string email;

        protected void Page_Load(object sender, EventArgs e)
        {
            email = Request.QueryString["email"].ToString();
            curUser = Membership.GetUser(Server.UrlDecode(email));
            curId = SiteUser.GetIdByFullName(curUser.UserName);
            abi = ApplicantBasicInfo.GetApplicantBasicInfoById(curId);

            if (!IsPostBack)
            {
                ddlCountry.DataSource = StaticData.NationalityList;
                ddlCountry.SelectedValue = "China";
                //ddlCountry.SelectedIndex = 43;
                ddlCountry.DataBind();

                ddlNation.DataSource = StaticData.NationalityList;
                ddlNation.SelectedValue = "China";
                //ddlNation.SelectedIndex = 43;
                ddlNation.DataBind();
            }

            //basic info exist
            if (abi != null)
            {
                isUpdate = true;
                if (!IsPostBack)
                {
                    PopulateBasicInfo();
                }
            }
        }

        protected void PopulateBasicInfo()
        {
            tbFirstName.Text = abi.FirstName;
            tbLastName.Text = abi.LastName;
            tbChineseName.Text = abi.NameInChinese;
            tbCity.Text = abi.CurrentCity;
            tbPhone.Text = abi.PhoneNumber;
            //removed
            //tbProvince.Text = abi.CurrentProvince;
            tbWebPage.Text = abi.WebPage;
            tbIdNum.Text = abi.IdentityNumber;
            //tbIdNum.Enabled = false;
            tbAddress.Text = abi.Address;
            ddlGender.SelectedIndex = (int)abi.Gender;
            //ddlNation.SelectedIndex = (int)abi.Nationality;
            //ddlCountry.SelectedIndex = (int)abi.CurrentCountry;
            ddlNation.SelectedIndex = StaticData.NationalityList.IndexOf(abi.Nationality);
            ddlCountry.SelectedIndex = StaticData.NationalityList.IndexOf(abi.CurrentCountry);
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //if (Applicant.IsIdNumUsed(GlobalHelper.ClearInput(tbIdNum.Text.Trim(), 256, false)))
            //{
            //    lbMsg.Text = "An ID num can only be registered once!";
            //    return;
            //}

            if (isUpdate)
            {
                abi.FirstName = GlobalHelper.ClearInput(tbFirstName.Text.Trim(), 256, false);
                abi.LastName = GlobalHelper.ClearInput(tbLastName.Text.Trim(), 256, false);
                abi.NameInChinese = GlobalHelper.ClearInput(tbChineseName.Text.Trim(), 256, false);
                //abi.Nationality = (CountryEnum)ddlNation.SelectedIndex;
                //abi.Nationality = ddlNation.SelectedIndex;
                abi.Nationality = ddlNation.SelectedItem.Text;
                abi.PhoneNumber = GlobalHelper.ClearInput(tbPhone.Text.Trim(), 256, false);
                abi.Gender = (GenderEnum)ddlGender.SelectedIndex;
                abi.CurrentCity = GlobalHelper.ClearInput(tbCity.Text.Trim(), 256, false);
                //abi.CurrentCountry = (CountryEnum)ddlCountry.SelectedIndex;
                //abi.CurrentCountry = ddlCountry.SelectedIndex;
                abi.CurrentCountry = ddlCountry.SelectedItem.Text;
                //removed
                //abi.CurrentProvince = GlobalHelper.ClearInput(tbProvince.Text.Trim(), 256, false);
                abi.Address = GlobalHelper.ClearInput(tbAddress.Text.Trim(), 256, false);
                abi.WebPage = GlobalHelper.ClearInput(tbWebPage.Text.Trim(), 256, false);
                abi.IdentityNumber = GlobalHelper.ClearInput(tbIdNum.Text.Trim(), 256, false);
                abi.Update();
            }
            else
            {
                abi = new ApplicantBasicInfo();

                abi.FirstName = GlobalHelper.ClearInput(tbFirstName.Text.Trim(), 256, false);
                abi.LastName = GlobalHelper.ClearInput(tbLastName.Text.Trim(), 256, false);
                abi.NameInChinese = GlobalHelper.ClearInput(tbChineseName.Text.Trim(), 256, false);
                //abi.Nationality = (CountryEnum)ddlNation.SelectedIndex;
                //abi.Nationality = ddlNation.SelectedIndex;
                abi.Nationality = ddlNation.SelectedItem.Text;
                abi.PhoneNumber = GlobalHelper.ClearInput(tbPhone.Text.Trim(), 256, false);
                abi.Gender = (GenderEnum)ddlGender.SelectedIndex;
                abi.CurrentCity = GlobalHelper.ClearInput(tbCity.Text.Trim(), 256, false);
                //abi.CurrentCountry = (CountryEnum)ddlCountry.SelectedIndex;
                //abi.CurrentCountry = ddlCountry.SelectedIndex;
                abi.CurrentCountry = ddlCountry.SelectedItem.Text;

                //removed
                //abi.CurrentProvince = GlobalHelper.ClearInput(tbProvince.Text.Trim(), 256, false);

                abi.Address = GlobalHelper.ClearInput(tbAddress.Text.Trim(), 256, false);
                abi.WebPage = GlobalHelper.ClearInput(tbWebPage.Text.Trim(), 256, false);
                abi.IdentityNumber = GlobalHelper.ClearInput(tbIdNum.Text.Trim(), 256, false);

                abi.ApplicationTime = DateTime.Now;

                abi.ApplicantId = curId;
                abi.Email = curUser.Email;
                abi.Insert();
            }

            btnSubmit.Visible = false;
            btnSubmit.Enabled = false;
            Response.Redirect(string.Format("ApplicantEduBackground.aspx?email={0}", Server.UrlEncode(email)));


            //lbMsg.Text = "Your data has up to date, Please go back to main page and complete the rest part of application!";

            ////check to change status?
            //if (Applicant.CheckComplete(curId))
            //{
            //    abi.Status = ApplicationStatusEnum.ApplicationComplete;
            //    abi.Update();
            //    lbMsg.Text = "You have completed your application! A email will send to your register email address!";
            //    //send email
            //}

        }
    }
}