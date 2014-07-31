/*****************************************************************************
 * Modified by Yi Shao at 2009-06-08
 * updated it to support uploading mentor approval Email
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
using MSRA.SpringField.Components.BizObjects;
using MSRA.SpringField.Components.Enumerations;
using MSRA.SpringField.Components.Configuration;
using MSRA.SpringField.Components;

namespace MSRA.SpringField.Application.Controls
{
    public partial class Controls_MentorResult : System.Web.UI.UserControl
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
            {
                Interview interview = Interview.GetInterviewById(InterviewID);
                /*
                 * Modify Interview Process
                 * Author: Yin.P
                 * Date: 2010-1-5
                 */
                CheckInForm form = CheckInForm.GetCheckInFormById(interview.CheckInFormId);

                lbHMAlias.Text = SiteUser.GetAliasByUserId(interview.HiringManagerId);

                /*
                 * Modify Interview Process
                 * Author: Yin.P
                 * Date: 2010-1-5
                 */
                if (form != null)
                {
                    lblMentorAlias.Text = form.MentorAlias;
                }

                if (interview.MentorApproval != null
                    && interview.MentorApproval.DocId != 0
                    && (
                        SiteUser.Current.IsInRole(RoleType.OnBoardManager.ToString()) ||
                        SiteUser.Current.IsInRole(RoleType.InternRecruiter.ToString()) ||
                        SiteUser.Current.SiteUserId == interview.HiringManagerId ||
                        SiteUser.Current.SiteUserId == interview.GroupManagerId
                       )
                    )
                {
                    this.tr_ApprovalEmail.Visible = true;
                    SiteConfiguration config = SiteConfiguration.GetConfig();
                    link_download.HRef = config.SiteAttributes["docUrl"] + interview.MentorApproval.SaveName;
                }

                //lbHMSuggestion.Text = interview.HiringManagerResult.ToString();
                if (interview.InterviewStatus == InterviewStatusEnum.OfferDeclined)
                {
                    lbHMSuggestion.Text = "Decline Offer";
                }
                /*
                 * Add by Yuanqin, 2011.5.5
                 * For Qualified but not mateched status
                 */
                else if (interview.InterviewStatus == InterviewStatusEnum.QualifiedButNotMatched)
                {
                    lbHMSuggestion.Text = "Qualified but not matched";
                }
                else if (interview.HiringManagerResult)
                {
                    lbHMSuggestion.Text = "Hire";
                }
                else if (!interview.HiringManagerResult)
                {
                    lbHMSuggestion.Text = "Reject";
                }
                lbHMComment.Text = GlobalHelper.FormatOutput(interview.HiringManagerComment);
                //CR:add time.
                lbHMDecisionTime.Text = interview.MentorDecisionTime.ToShortDateString() + " " + interview.MentorDecisionTime.ToLongTimeString();

            }
            SwitchToView();
        }
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            SwitchToEdit();
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            //Update CheckInForm
            Interview interview = Interview.GetInterviewById(InterviewID);
            CheckInForm form = CheckInFormEdit1.GetCheckInForm();
            form.FormId = interview.CheckInFormId;
            form.Update();
            CheckInFormView1.SetCheckInFormId(interview.CheckInFormId);
            SwitchToView();
        }

        void SwitchToEdit()
        {
            CheckInFormView1.Visible = false;
            Interview interview = Interview.GetInterviewById(InterviewID);
            CheckInForm form = CheckInForm.GetCheckInFormById(interview.CheckInFormId);
            CheckInFormEdit1.SetCheckInForm(form);
            CheckInFormEdit1.Visible = true;
            btnEdit.Visible = false;
            btnUpdate.Visible = true;
            btnCancel.Visible = true;
        }

        void SwitchToView()
        {
            if (HasCheckInFormView())
            {
                Interview interview = Interview.GetInterviewById(InterviewID);
                if (interview.CheckInFormId == 0)
                {
                    CheckInFormView1.Visible = false;
                    CheckInFormEdit1.Visible = false;
                    btnEdit.Visible = false;
                    btnUpdate.Visible = false;
                    btnCancel.Visible = false;
                }
                else
                {
                    btnEdit.Visible = IsHiringManager();
                    btnUpdate.Visible = false;
                    btnCancel.Visible = false;
                    CheckInForm form = CheckInForm.GetCheckInFormById(interview.CheckInFormId);
                    CheckInFormView1.SetCheckInForm(form);
                    CheckInFormEdit1.Visible = false;
                    CheckInFormView1.Visible = true;
                }
            }
            else
            {
                CheckInFormEdit1.Visible = false;
                CheckInFormView1.Visible = false;
                btnEdit.Visible = false;
                btnUpdate.Visible = false;
                btnCancel.Visible = false;
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            SwitchToView();
        }

        //let IR and On Board Manager have the right to modify New Intern On Board Request
        //modified by Yi Shao at 2009-6-11
        private bool IsHiringManager()
        {
            Interview interview = Interview.GetInterviewById(InterviewID);
            if (SiteUser.Current.SiteUserId == interview.HiringManagerId)
                return true;
            if (SiteUser.Current.IsInRole(RoleType.InternRecruiter) || SiteUser.Current.IsInRole(RoleType.OnBoardManager))
                return true;
            return false;
        }

        private bool IsGroupManager()
        {
            Interview interview = Interview.GetInterviewById(InterviewID);
            return (SiteUser.Current.SiteUserId == interview.GroupManagerId);
        }

        bool HasCheckInFormView()
        {
            if (SiteUser.Current.IsInRole(RoleType.InternRecruiter)
                || SiteUser.Current.IsInRole(RoleType.OnBoardManager)
                || IsHiringManager()
                || IsGroupManager())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}