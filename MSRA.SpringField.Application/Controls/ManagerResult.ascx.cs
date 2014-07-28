/*****************************************************************************
 * Modified by Yi Shao at 2009-06-08
 * updated it to support uploading manager approval Email
*****************************************************************************/
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
using MSRA.SpringField.Components.Configuration;
using MSRA.SpringField.Components.BizObjects;
using MSRA.SpringField.Components.Enumerations;
using MSRA.SpringField.Components;

namespace MSRA.SpringField.Application.Controls
{
    public partial class Controls_ManagerResult : System.Web.UI.UserControl
    {
        public Int32 InterviewID
        {
            get
            {
                if (ViewState["interviewid"] == null)
                {
                    return 0;
                }
                else
                {
                    return Convert.ToInt32(ViewState["interviewid"]);
                }
            }

            set
            {
                ViewState["interviewid"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!this.IsPostBack)
            //{
            Interview interview = Interview.GetInterviewById(InterviewID);
            if (interview.GroupManagerId == Guid.Empty || interview.InterviewStatus == InterviewStatusEnum.WaitingForGroupManagerDecision)//if manager no action.
            {
                this.Visible = false;
                return;
            }
            if (interview.GMApproval != null
                && interview.GMApproval.DocId != 0
                && (
                    SiteUser.Current.IsInRole(RoleType.OnBoardManager.ToString()) ||
                    SiteUser.Current.IsInRole(RoleType.InternRecruiter.ToString()) ||
                    SiteUser.Current.SiteUserId == interview.GroupManagerId
                   )
                )
            {
                this.tr_ApprovalEmail.Visible = true;
                SiteConfiguration config = SiteConfiguration.GetConfig();
                link_download.HRef = config.SiteAttributes["docUrl"] + interview.GMApproval.SaveName;
            }
            lbGMAlias.Text = SiteUser.GetAliasByUserId(interview.GroupManagerId);
            //lbGMSuggestion.Text = interview.GroupManagerResult.ToString();
            if (interview.InterviewStatus == InterviewStatusEnum.OfferDeclined)
            {
                lbGMSuggestion.Text = "Decline Offer";
            }
            /*
             * Add by Yuanqin, 2011.5.5
             * For Qualified but not mateched status
             */
            else if (interview.InterviewStatus == InterviewStatusEnum.QualifiedButNotMatched)
            {
                lbGMSuggestion.Text = "Qualified but not matched";
            }
            else if (interview.GroupManagerResult)
            {
                lbGMSuggestion.Text = "Hire";
            }
            else if (!interview.GroupManagerResult)
            {
                lbGMSuggestion.Text = "Reject";
            }
            lbGMComment.Text = GlobalHelper.FormatOutput(interview.GroupManagerComment);
            //CR:add time.
            lbGMDecisionTime.Text = interview.ManagerDecisionTime.ToShortDateString() + " " + interview.ManagerDecisionTime.ToLongTimeString();
            //}
        }
    }
}