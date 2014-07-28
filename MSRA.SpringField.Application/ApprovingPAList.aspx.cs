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
using System.IO;
using System.Text;
using System.Linq;
using MSRA.SpringField.Application.Config.Schemas;
using MSRA.SpringField.Components.BizObjects;
using MSRA.SpringField.Components.Enumerations;
using MSRA.SpringField.Components;
using MSRA.SpringField.Components.Configuration;

namespace MSRA.SpringField.Application
{
    public partial class ApprovingPAList : System.Web.UI.Page
    {
        private SpringFieldDataContext ctx = new SpringFieldDataContext();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                DataBindByCondition(5);
                this.toolbar.Visible = false;

                /*
                 * Add Send PARemind Mail by Yuanqin
                 * 2011.5.25
                 */
                if (Roles.IsUserInRole(SiteUser.Current.FullName, RoleType.InternRecruiter.ToString()))
                {
                    tdBtnPARemind.Visible = true;
                    trMentorName.Visible = true;
                }
                else
                {
                    tdBtnPARemind.Visible = false;
                    trMentorName.Visible = false;
                }
            }

            this.dtFrom.Attributes.Add("onclick",
                "popUpCalendar(this,document.all." + dtFrom.ClientID + ",'yyyy-mm-dd');");
            this.dtTo.Attributes.Add("onclick",
                "popUpCalendar(this,document.all." + dtTo.ClientID + ",'yyyy-mm-dd');");
        }

        protected void ddlApprovalStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataBindByCondition(int.Parse(this.ddlApprovalStatus.SelectedValue));
            if (ddlApprovalStatus.SelectedValue.Equals("0"))
            {
                this.toolbar.Visible = false;
            }
            else
            {
                this.toolbar.Visible = false;
            }
        }

        //绑定默认查询结果
        public void DataBindByCondition(int approvalStatus)
        {
            var result = new object();
            if (SiteUser.Current.IsInRole(RoleType.InternRecruiter) == false)//普通人
            {
                if (approvalStatus != 5)
                {
                    result = from list in ctx.sf_PerformanceAssessments
                             where list.IsApproved == approvalStatus &&
                                (list.MentorAlias == SiteUser.Current.Alias || list.GroupMgrId == SiteUser.Current.SiteUserId)
                             orderby list.CheckOutDate descending
                             select list;
                }
                else// Approval Status=5 all
                {
                    result = from list in ctx.sf_PerformanceAssessments
                             where (list.MentorAlias == SiteUser.Current.Alias || list.GroupMgrId == SiteUser.Current.SiteUserId)
                             orderby list.CheckOutDate descending
                             select list;
                }
            }
            else//InternRecruiter
            {
                if (approvalStatus != 5)
                {
                    result = from list in ctx.sf_PerformanceAssessments
                             where list.IsApproved == approvalStatus
                             orderby list.CheckOutDate descending
                             select list;
                }
                else
                {
                    result = from list in ctx.sf_PerformanceAssessments
                             orderby list.CheckOutDate descending
                             select list;
                }
            }

            this.gvApprovingList.DataSource = result;
            this.gvApprovingList.DataBind();
        }

        /*
         * Add by Yuanqin for PageIndexChanging
         * 2011.5.23
         */
        public void DataBindByCondition(int approvalStatus, int paResult)
        {
            var result = new object();
            DateTime mdtFrom = Convert.ToDateTime("1/1/1753 12:00:00 AM"), mdtTo = Convert.ToDateTime("12/31/9999 11:59:59 PM");
            if (this.dtFrom.Text.Length != 0) mdtFrom = Convert.ToDateTime(dtFrom.Text);
            if (this.dtTo.Text.Length != 0) mdtTo = Convert.ToDateTime(dtTo.Text);

            /*
             * Modify by Yuanqin, 2011.4.23
             */
            if (SiteUser.Current.IsInRole(RoleType.InternRecruiter) == false)
            {
                if (this.ddlApprovalStatus.SelectedValue != "5")
                {
                    if (this.ddlPAResult.SelectedValue != "7")
                    {
                        result = from list in ctx.sf_PerformanceAssessments
                                 where list.IsApproved == int.Parse(this.ddlApprovalStatus.SelectedValue)
                                 && (list.MentorAlias == SiteUser.Current.Alias || list.GroupMgrId == SiteUser.Current.SiteUserId)
                                 && (list.CheckOutDate > mdtFrom && list.CheckOutDate < mdtTo)
                                 && list.InternName.Contains(this.txtName.Text)
                                 && list.OverrallEvaluation == int.Parse(this.ddlPAResult.SelectedValue)
                                 orderby list.CheckOutDate descending
                                 select list;
                    }
                    else
                    {
                        result = from list in ctx.sf_PerformanceAssessments
                                 where list.IsApproved == int.Parse(this.ddlApprovalStatus.SelectedValue)
                                 && (list.MentorAlias == SiteUser.Current.Alias || list.GroupMgrId == SiteUser.Current.SiteUserId)
                                 && (list.CheckOutDate > mdtFrom && list.CheckOutDate < mdtTo)
                                 && list.InternName.Contains(this.txtName.Text)
                                 orderby list.CheckOutDate descending
                                 select list;
                    }
                }
                else
                {
                    if (this.ddlPAResult.SelectedValue != "7")
                    {
                        result = from list in ctx.sf_PerformanceAssessments
                                 where (list.MentorAlias == SiteUser.Current.Alias || list.GroupMgrId == SiteUser.Current.SiteUserId)
                                 && (list.CheckOutDate > mdtFrom && list.CheckOutDate < mdtTo)
                                 && list.InternName.Contains(this.txtName.Text)
                                 && list.OverrallEvaluation == int.Parse(this.ddlPAResult.SelectedValue)
                                 orderby list.CheckOutDate descending
                                 select list;
                    }
                    else
                    {
                        result = from list in ctx.sf_PerformanceAssessments
                                 where (list.MentorAlias == SiteUser.Current.Alias || list.GroupMgrId == SiteUser.Current.SiteUserId)
                                 && (list.CheckOutDate > mdtFrom && list.CheckOutDate < mdtTo)
                                 && list.InternName.Contains(this.txtName.Text)
                                 orderby list.CheckOutDate descending
                                 select list;
                    }
                }
            }
            else
            {
                if (this.ddlApprovalStatus.SelectedValue != "5")
                {
                    if (this.ddlPAResult.SelectedValue != "7")
                    {
                        result = from list in ctx.sf_PerformanceAssessments
                                 where list.IsApproved == int.Parse(this.ddlApprovalStatus.SelectedValue)
                                 && (list.CheckOutDate > mdtFrom && list.CheckOutDate < mdtTo)
                                 && list.InternName.Contains(this.txtName.Text)
                                 && list.OverrallEvaluation == int.Parse(this.ddlPAResult.SelectedValue)
                                 orderby list.CheckOutDate descending
                                 select list;
                    }
                    else
                    {
                        result = from list in ctx.sf_PerformanceAssessments
                                 where list.IsApproved == int.Parse(this.ddlApprovalStatus.SelectedValue)
                                 && (list.CheckOutDate > mdtFrom && list.CheckOutDate < mdtTo)
                                 && list.InternName.Contains(this.txtName.Text)
                                 orderby list.CheckOutDate descending
                                 select list;
                    }
                }
                else
                {
                    if (this.ddlPAResult.SelectedValue != "7")
                    {
                        result = from list in ctx.sf_PerformanceAssessments
                                 where (list.CheckOutDate > mdtFrom && list.CheckOutDate < mdtTo)
                                 && list.InternName.Contains(this.txtName.Text)
                                 && list.OverrallEvaluation == int.Parse(this.ddlPAResult.SelectedValue)
                                 orderby list.CheckOutDate descending
                                 select list;
                    }
                    else
                    {
                        result = from list in ctx.sf_PerformanceAssessments
                                 where (list.CheckOutDate > mdtFrom && list.CheckOutDate < mdtTo)
                                 && list.InternName.Contains(this.txtName.Text)
                                 orderby list.CheckOutDate descending
                                 select list;
                    }
                }

            }

            this.gvApprovingList.DataSource = result;
            this.gvApprovingList.DataBind();
        }

        protected void gvApprovingList_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {

        }

        protected void gvApprovingList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvApprovingList.PageIndex = e.NewPageIndex;
            //DataBindByCondition(int.Parse(this.ddlApprovalStatus.SelectedValue));
            DataBindByCondition(int.Parse(this.ddlApprovalStatus.SelectedValue), int.Parse(this.ddlPAResult.SelectedValue));
        }

        //int to string
        public string GetApprovalStatusString(int status)
        {
            string ret = string.Empty;
            switch (status)
            {
                /*
                 * Modify to only too status
                 * Author: Yuanqin
                 * Date: 2011.3.14
                 */
                //case 0: ret = "Pending"; break;
                //case 1: ret = "Approved"; break;
                //case 2: ret = "Rejected"; break;
                //case 3: ret = "Invalid"; break;

                case 1: ret = "Complete"; break;
                case 4: ret = "Incomplete"; break;
                case 9: ret = "Complete By UR"; break;
                default: ret = "Inprocess"; break;

            }
            return ret;
        }

        protected void btnFind_Clicked(object sender, EventArgs e)
        {
            var result = new object();
            DateTime mdtFrom = Convert.ToDateTime("1/1/1753 12:00:00 AM"), mdtTo = Convert.ToDateTime("12/31/9999 11:59:59 PM");
            if (this.dtFrom.Text.Length != 0) mdtFrom = Convert.ToDateTime(dtFrom.Text);
            if (this.dtTo.Text.Length != 0) mdtTo = Convert.ToDateTime(dtTo.Text);

            /*
             * Modify by Yuanqin, 2011.4.23
             */
            //ddlApprovalStatus.SelectedValue = 5 代表all 
            if (SiteUser.Current.IsInRole(RoleType.InternRecruiter) == false)//只能查他自己的intern
            {
                if (this.ddlApprovalStatus.SelectedValue != "5")
                {
                    if (this.ddlPAResult.SelectedValue != "7")
                    {
                        result = from list in ctx.sf_PerformanceAssessments
                                 where list.IsApproved == int.Parse(this.ddlApprovalStatus.SelectedValue)
                                 && (list.MentorAlias == SiteUser.Current.Alias || list.GroupMgrId == SiteUser.Current.SiteUserId)
                                 && (list.CheckOutDate > mdtFrom && list.CheckOutDate < mdtTo)
                                 && list.InternName.Contains(this.txtName.Text)
                                 && list.OverrallEvaluation == int.Parse(this.ddlPAResult.SelectedValue)
                                 orderby list.CheckOutDate descending
                                 select list;
                    }
                    else
                    {
                        result = from list in ctx.sf_PerformanceAssessments
                                 where list.IsApproved == int.Parse(this.ddlApprovalStatus.SelectedValue)
                                 && (list.MentorAlias == SiteUser.Current.Alias || list.GroupMgrId == SiteUser.Current.SiteUserId)
                                 && (list.CheckOutDate > mdtFrom && list.CheckOutDate < mdtTo)
                                 && list.InternName.Contains(this.txtName.Text)
                                 orderby list.CheckOutDate descending
                                 select list;
                    }
                }
                else
                {
                    if (this.ddlPAResult.SelectedValue != "7")
                    {
                        result = from list in ctx.sf_PerformanceAssessments
                                 where (list.MentorAlias == SiteUser.Current.Alias || list.GroupMgrId == SiteUser.Current.SiteUserId)
                                 && (list.CheckOutDate > mdtFrom && list.CheckOutDate < mdtTo)
                                 && list.InternName.Contains(this.txtName.Text)
                                 && list.OverrallEvaluation == int.Parse(this.ddlPAResult.SelectedValue)
                                 orderby list.CheckOutDate descending
                                 select list;
                    }
                    else
                    {
                        result = from list in ctx.sf_PerformanceAssessments
                                 where (list.MentorAlias == SiteUser.Current.Alias || list.GroupMgrId == SiteUser.Current.SiteUserId)
                                 && (list.CheckOutDate > mdtFrom && list.CheckOutDate < mdtTo)
                                 && list.InternName.Contains(this.txtName.Text)
                                 orderby list.CheckOutDate descending
                                 select list;
                    }
                }
            }
            else//InternRecruiter
            {
                //(list.MentorAlias == SiteUser.Current.Alias || list.GroupMgrId == SiteUser.Current.SiteUserId)
                if (this.ddlApprovalStatus.SelectedValue != "5")
                {
                    if (this.ddlPAResult.SelectedValue != "7")
                    {
                        result = from list in ctx.sf_PerformanceAssessments
                                 where list.IsApproved == int.Parse(this.ddlApprovalStatus.SelectedValue)
                                 && (list.CheckOutDate > mdtFrom && list.CheckOutDate < mdtTo)
                                 && list.OverrallEvaluation == int.Parse(this.ddlPAResult.SelectedValue)
                                 && list.InternName.Contains(this.txtName.Text)
                                     //&& list.MentorAlias.Contains(this.txtMentorName.Text)
                                 && list.MentorName.Contains(this.txtMentorName.Text)
                                 orderby list.CheckOutDate descending
                                 select list;
                    }
                    else
                    {
                        result = from list in ctx.sf_PerformanceAssessments
                                 where list.IsApproved == int.Parse(this.ddlApprovalStatus.SelectedValue)
                                 && (list.CheckOutDate > mdtFrom && list.CheckOutDate < mdtTo)
                                 && list.InternName.Contains(this.txtName.Text)
                                     //&& list.MentorAlias.Contains(this.txtMentorName.Text)
                                 && list.MentorName.Contains(this.txtMentorName.Text)
                                 orderby list.CheckOutDate descending
                                 select list;
                    }
                }
                else//全部Status
                {
                    if (this.ddlPAResult.SelectedValue != "7")
                    {
                        result = from list in ctx.sf_PerformanceAssessments
                                 where (list.CheckOutDate > mdtFrom && list.CheckOutDate < mdtTo)
                                 && list.InternName.Contains(this.txtName.Text)
                                     //&& list.MentorAlias.Contains(this.txtMentorName.Text)
                                 && list.MentorName.Contains(this.txtMentorName.Text)
                                 && list.OverrallEvaluation == int.Parse(this.ddlPAResult.SelectedValue)
                                 orderby list.CheckOutDate descending
                                 select list;
                    }
                    else
                    {
                        result = from list in ctx.sf_PerformanceAssessments
                                 where (list.CheckOutDate > mdtFrom && list.CheckOutDate < mdtTo)
                                 && list.InternName.Contains(this.txtName.Text)
                                     //&& list.MentorAlias.Contains(this.txtMentorName.Text)
                                 && list.MentorName.Contains(this.txtMentorName.Text)
                                 orderby list.CheckOutDate descending
                                 select list;
                    }
                }

            }

            this.gvApprovingList.DataSource = result;
            this.gvApprovingList.DataBind();
        }

        /*
         * Add PARemind mail 
         * Author: Yuanqin
         * Date: 2011.5.25
         */
        protected void btnPARemind_Clicked(object sender, EventArgs e)
        {
            if (Request.Params["__selPA__"] != null)
            {
                string[] ids = Request.Params["__selPA__"].Split(new char[] { ',' });

                for (int i = 0; i < ids.Length; i++)
                {
                    sf_PerformanceAssessment pa =
                        ctx.sf_PerformanceAssessments.FirstOrDefault<sf_PerformanceAssessment>(
                        p => p.id.Equals(ids[i]));
                    if (pa != null)
                    {
                        //pa.IsApproved = 4;
                        ctx.SubmitChanges();

                        if (pa.GroupMgrId != null && pa.GroupMgrId != Guid.Empty)
                        {
                            //send remind mail
                            MailHelper mailHelper = new MailHelper();
                            mailHelper.AddPerformanceAssessmentVariables(pa.id);
                            mailHelper.SendMail(MailType.PAIncompleteRemind);
                        }
                    }
                }
            }
        }


        /*
         * Modify 
         * Author: Yuanqin
         * Date: 2011.3.14
         */
        protected void btnPass_Click(object sender, EventArgs e)
        {
            //if (Request.Params["__selPA__"] != null)
            //{
            //    string[] ids = Request.Params["__selPA__"].Split(new char[] { ',' });

            //    for (int i = 0; i < ids.Length; i++)
            //    {
            //        sf_PerformanceAssessment pa =
            //            ctx.sf_PerformanceAssessments.FirstOrDefault<sf_PerformanceAssessment>(
            //            p => p.id.Equals(ids[i]));
            //        if (pa != null)
            //        {
            //            pa.IsApproved = 1;
            //            ctx.SubmitChanges();

            //            if (pa.GroupMgrId != null && pa.GroupMgrId != Guid.Empty)
            //            {
            //                //Send mail to group manager
            //                MailHelper mailHelper = new MailHelper();
            //                mailHelper.AddPerformanceAssessmentVariables(pa.id);
            //                mailHelper.SendMail(MailType.PANotice);
            //            }
            //        }
            //    }
            //}
        }

        protected void btnReject_Click(object sender, EventArgs e)
        {
            //if (Request.Params["__selPA__"] != null)
            //{
            //    string[] ids = Request.Params["__selPA__"].Split(new char[] { ',' });

            //    for (int i = 0; i < ids.Length; i++)
            //    {
            //        sf_PerformanceAssessment pa =
            //            ctx.sf_PerformanceAssessments.FirstOrDefault<sf_PerformanceAssessment>(
            //            p => p.id.Equals(ids[i]));
            //        if (pa != null)
            //        {
            //            pa.IsApproved = 2;  //IsApproved is 2 means REJECT.
            //            ctx.SubmitChanges();

            //            if (pa.GroupMgrId != null && pa.GroupMgrId != Guid.Empty)
            //            {
            //                //Send mail to group manager
            //                MailHelper mailHelper = new MailHelper();
            //                mailHelper.AddPAApprovalRejectedVariables(pa.id);
            //                mailHelper.SendMail(MailType.PAApprovalRejected);

            //                //JSUtility.Alert(this,
            //                //    "The approval of current performance assessment was rejected. A mail has been sent to relevant mentor.");
            //            }
            //        }
            //    }
            //}
        }

        protected void btnInvalid_Click(object sender, EventArgs e)
        {
            //if (Request.Params["__selPA__"] != null)
            //{
            //    string[] ids = Request.Params["__selPA__"].Split(new char[] { ',' });

            //    for (int i = 0; i < ids.Length; i++)
            //    {
            //        sf_PerformanceAssessment pa =
            //            ctx.sf_PerformanceAssessments.FirstOrDefault<sf_PerformanceAssessment>(
            //            p => p.id.Equals(ids[i]));
            //        if (pa != null)
            //        {
            //            pa.IsApproved = 3;  //IsApproved is 3 means INVALID.
            //            ctx.SubmitChanges();

            //            if (pa.GroupMgrId != null && pa.GroupMgrId != Guid.Empty)
            //            {
            //                //JSUtility.Alert(this, "Performance Assessment has been set invalid,and a mail was sent to Group Manager");
            //            }
            //        }
            //    }
            //}
        }

        /*
         * Add by Yuanqin
         * 2011.4.23
         */
        protected string GetDeadline(string InternPAtime)
        {
            string strDeadline = String.Empty;
            DateTime dtInternPAtime = DateTime.MinValue;
            try
            {
                dtInternPAtime = Convert.ToDateTime(InternPAtime);
            }
            catch
            {
                return strDeadline;
            }

            SiteConfiguration config = SiteConfiguration.GetConfig();
            int Timeslice = Convert.ToInt32(config.SiteAttributes["MentorPADays"]);
            strDeadline = dtInternPAtime.AddDays(Timeslice).ToString("yyyy-MM-dd");
            return strDeadline;
        }
        protected string GetPerformance(string Performance)
        {
            string strPerformance = "N/A";
            try
            {
                strPerformance = PAResourceManager.IdToText("PerformanceLevel", Convert.ToInt32(Performance));
            }
            catch
            {
            }

            return strPerformance;
        }

    }
}
