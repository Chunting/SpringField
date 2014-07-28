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
using System.Text;
using MSRA.SpringField.Components.BizObjects;
using MSRA.SpringField.Components;
using MSRA.SpringField.Components.Enumerations;
using MSRA.SpringField.Application.Config.Schemas;
using System.Linq;

namespace MSRA.SpringField.Application.Keyin
{
    public partial class PageApplicantRelatedInfo : System.Web.UI.Page
    {
        bool isUpdate = false;
        Guid curId = Guid.Empty;
        protected ApplicantRelatedInfo ari = null;
        MembershipUser curUser = null;
        string email;

        protected void Page_Load(object sender, EventArgs e)
        {
            email = Request.QueryString["email"].ToString();

            tbFirstDate.Attributes.Add("onclick", "popUpCalendar(this,document.all." + tbFirstDate.ClientID + ",'mm/dd/yyyy');");
            tbSecondDate.Attributes.Add("onclick", "popUpCalendar(this,document.all." + tbSecondDate.ClientID + ",'mm/dd/yyyy');");

            curUser = Membership.GetUser(Server.UrlDecode(email));
            curId = SiteUser.GetIdByFullName(curUser.UserName);
            ari = ApplicantRelatedInfo.GetApplicantRelatedInfoById(curId);


            if (!IsPostBack)
            {
                cblGroup.DataSource = StaticData.GroupList;
                cblGroup.DataBind();
                Repeater1.DataSource = GetSpecialProgram();
                Repeater1.DataBind();
            }

            //related info exist
            if (ari != null)
            {
                isUpdate = true;
                if (!IsPostBack)
                {
                    PopulateRelatedInfo();
                }
            }
        }
        protected bool Check(string item)
        {
            return GetSpecialChoosedProgram().Contains(item);
        }
        protected void PopulateRelatedInfo()
        {
            tbFirstDate.Text = ari.PreferredAvailStartDate.ToShortDateString();
            tbSecondDate.Text = ari.SecondaryAvailStartDate.ToShortDateString();
            tbAreas.Text = ari.InterestedAreas;
            //tbInfoSourceText.Text = ari.InfoSourceText;
            //ddlGroup.SelectedIndex = StaticData.GroupList.IndexOf(ari.InterestedGroup) + 1;

            if (!String.IsNullOrEmpty(ari.InterestedGroup))
            {
                string[] groupList = ari.InterestedGroup.Split(new char[] { ';' });
                StringBuilder sbOther = new StringBuilder();
                if (groupList != null && groupList.Length > 0)
                {
                    //init the check box list
                    foreach (string curStr in groupList)
                    {
                        bool bFound = false;
                        string curStrTrim = curStr.Trim();
                        if (curStrTrim == string.Empty)
                        {
                            continue;
                        }

                        foreach (ListItem item in cblGroup.Items)
                        {
                            if (item.Text.ToLower() == curStrTrim.ToLower())
                            {
                                item.Selected = true;
                                bFound = true;
                                break;
                            }
                        }
                        if (!bFound)
                        {
                            sbOther.Append(curStrTrim);
                            sbOther.Append(";");
                        }
                    }
                }
                tbOtherGroup.Text = sbOther.ToString();
            }

            ddlInternType.SelectedIndex = (int)ari.InternshipType;

            lbInfoSource.Text = "Source:[" + ari.JobInfoSource + "] Channel:[" + ari.JobInfoChannel + "] Detail:[" + ari.JobInfoDetail + "]";

            isg.CurrentID = ari.ApplicantId;
            //ddlInfoSource.SelectedIndex = (int)ari.InfoSource;
            //Linkage();
            //ddlInfoSourceDetail.SelectedIndex = (int)ari.InfoSourceDetail;
        }

        protected ArrayList GetSpecialProgram()
        {
            ArrayList list = new ArrayList();            
            list.Add("Microsoft Jointlab Program");
            list.Add("Microsoft Internship Program");
            list.Add("MS Young Fellowship Program");
            list.Add("MS Fellowship Program");
            list.Add("Student Exchange Program");
            list.Add("Undergraduate Research Program");
            list.Add("IJARC Program");
            list.Add("MS PhD Program");
            list.Add("None of The Above");
            return list;
        }
        protected ArrayList GetSpecialChoosedProgram()
        {
            ArrayList list = new ArrayList();
            if (ari != null)
            {
                string[] choosedPrograms = ari.SpecialProgram.Split(';');

                foreach (string str in choosedPrograms)
                {
                    list.Add(str.Trim());
                }
            }
            return list;
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //if (ddlInfoSource.SelectedIndex == 6)
            //{
            //    lbMsg.Text = "You should select your information source first!";
            //    return;
            //}

            DateTime firstDate = DateTime.Now;
            DateTime secondDate = DateTime.Now;

            try
            {
                firstDate = Convert.ToDateTime(tbFirstDate.Text.Trim());
            }
            catch
            {
                lbMsg.Text = "Prefered datetime illegal, Please retry!";
                return;
            }

            if (!string.IsNullOrEmpty(tbSecondDate.Text.Trim()))
            {
                try
                {
                    secondDate = Convert.ToDateTime(tbSecondDate.Text.Trim());
                }
                catch
                {
                    lbMsg.Text = "Second prefered datetime illegal, Please retry!";
                    return;
                }
            }

            if (firstDate > secondDate)
            {
                lbMsg.Text = "Till date should later than begin date";
                return;
            }

            if (isUpdate)
            {
                SpringFieldDataContext ctx = new SpringFieldDataContext();
                sf_ApplicantBasicInfo result = 
                    ctx.sf_ApplicantBasicInfos.FirstOrDefault<sf_ApplicantBasicInfo>(p => p.ApplicantId == ari.ApplicantId);
                if (result == null)
                {
                    JSUtility.Alert(this.Page,"The current applicant is not exist.");
                }
                else
                {
                    FillRelatedInfo(firstDate, secondDate);
                    ari.Update();
                }
            }
            else
            {
                ari = new ApplicantRelatedInfo();
                FillRelatedInfo(firstDate, secondDate);
                ari.ApplicantId = curId;
                ari.Insert();

                ApplicantBasicInfo.ChangeApplicantStatus(curId, ApplicationStatusEnum.Available);
            }

            btnSubmit.Visible = false;
            btnSubmit.Enabled = false;

            Response.Redirect("~/ShowApplication.aspx?applicant=" + curId.ToString(), true);
            //Server.Transfer(string.Format("ApplicationComplete.aspx?email={0}", Server.UrlEncode(email)));


            //lbMsg.Text = "Your data has up to date, Please go back to main page and complete the rest part of application!";

            ////check to change status?
            //if (Applicant.CheckComplete(curId))
            //{
            //    ApplicantBasicInfo.ChangeApplicantStatus(curId, ApplicationStatusEnum.ApplicationComplete);
            //    lbMsg.Text = "You have completed your application! A email will send to your register email address!";
            //    //send email
            //}
        }
        void FillRelatedInfo(DateTime firstDate, DateTime secondDate)
        {
            ari.InterestedAreas = GlobalHelper.ClearInput(tbAreas.Text.Trim(), 4000, false);

            StringBuilder sbSpecialProgram = new StringBuilder();
            foreach (RepeaterItem ri in Repeater1.Items)
            {
                CheckBox cb = ri.FindControl("cbSpecialProgram") as CheckBox;
                if (cb.Checked)
                {
                    sbSpecialProgram.Append(cb.Text);
                    sbSpecialProgram.Append(";");
                }
            }

            ari.SpecialProgram = sbSpecialProgram.ToString();

            StringBuilder sb = new StringBuilder();
            foreach (ListItem item in cblGroup.Items)
            {
                if (item.Selected)
                {
                    sb.Append(item.Text);
                    sb.Append(";");
                }
            }
            sb.Append(tbOtherGroup.Text);
            ari.InterestedGroup = sb.ToString();

            ari.PreferredPosition = (PositionTypeEnum)ddlPreferredPositon.SelectedIndex;
            ari.InternshipType = (InternshipTypeEnum)ddlInternType.SelectedIndex;
            ari.PreferredAvailStartDate = firstDate;
            ari.SecondaryAvailStartDate = secondDate;
            ari.JobInfoCategory = isg.Category;
            ari.JobInfoChannel = isg.Channel;
            ari.JobInfoSource = isg.Source;
            ari.JobInfoDetail = isg.Detail;
        }
        //protected void ddlInfoSource_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    Linkage();
        //}

        //protected void ddlInfoSourceDetail_SelectedIndexChanged(object sender, EventArgs e)
        //{

        //}
    }
}