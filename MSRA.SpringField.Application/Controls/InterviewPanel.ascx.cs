/*****************************************************************************
 * Modified by Yi Shao at 2009-06-04
 * updated it to support uploading manager approval Email
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

namespace MSRA.SpringField.Application.Controls
{
    public partial class Controls_InterviewPanel : System.Web.UI.UserControl
    {
        public Guid ApplicantId
        {
            set
            {
                ViewState["applicantid"] = value;
            }
            get
            {
                if (ViewState["applicantid"] == null)
                {
                    return Guid.Empty;
                }
                else
                {
                    Guid newId = new Guid(ViewState["applicantid"].ToString());
                    return newId;
                }
            }
        }

        public String InterviewTime
        {
            set
            {
                lbVersion.Text = value;
            }
            get
            {
                return lbVersion.Text;
            }
        }
        public Int32 InterviewId
        {
            set
            {
                ViewState["interviewid"] = value;
            }
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
        }
        private Interview interview
        {
            set
            {
                ViewState["interview"] = value;
            }
            get
            {
                return ViewState["interview"] as Interview;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{
            ResetControls();
            //}
        }
        //初始化control内所有元素
        public void ResetControls()
        {
            phInterview.Controls.Clear();
            interview = Interview.GetInterviewById(InterviewId);
            InitInterviewInfo(ApplicantId);
        }

        #region Load User Control
        private void LoadMentorResult()
        {
            if (phInterview.FindControl("ucMentorResult") == null)
            {
                Controls_MentorResult uc = (Controls_MentorResult)this.LoadControl("MentorResult.ascx");
                uc.InterviewID = interview.InterviewId;
                uc.ID = "ucMentorResult";
                phInterview.Controls.Add(uc);
            }
        }

        private void LoadInterviewFeedback(Int32 FeedbackId)
        {
            if (phInterview.FindControl("ucInterviewFeedback") == null)
            {
                Controls_InterviewFeedback uc = (Controls_InterviewFeedback)this.LoadControl("InterviewFeedback.ascx");
                uc.FeedbackId = FeedbackId;
                uc.ID = "ucInterviewFeedback";
                phInterview.Controls.Add(uc);
            }
        }

        private void LoadManagerApproval()
        {
            if (phInterview.FindControl("ucManagerApproval") == null)
            {
                Controls_ManagerApproval uc = (Controls_ManagerApproval)this.LoadControl("ManagerApproval.ascx");
                uc.InterviewID = interview.InterviewId;
                uc.ID = "ucManagerApproval";
                phInterview.Controls.Add(uc);
            }
        }

        private void LoadUploadingManagerApproval()
        {
            if (phInterview.FindControl("ucUploadingManagerApproval") == null)
            {
                Controls_UploadingManagerApproval uc = (Controls_UploadingManagerApproval)this.LoadControl("UploadingManagerApproval.ascx");
                uc.InterviewID = interview.InterviewId;
                uc.ID = "ucUploadingManagerApproval";
                phInterview.Controls.Add(uc);
            }
        }

        private void LoadManagerResult()
        {
            if (phInterview.FindControl("ucManagerResult") == null)
            {
                Controls_ManagerResult uc = (Controls_ManagerResult)this.LoadControl("ManagerResult.ascx");
                uc.InterviewID = interview.InterviewId;
                uc.ID = "ucManagerResult";
                phInterview.Controls.Add(uc);
            }
        }

        private void LoadIncompleteList()
        {
            if (phInterview.FindControl("ucIncompleteList") == null)
            {
                Controls_IncompleteInterviewList uc = (Controls_IncompleteInterviewList)this.LoadControl("IncompleteInterviewList.ascx");
                uc.InterviewID = interview.InterviewId;
                uc.ID = "ucIncompleteList";
                phInterview.Controls.Add(uc);
            }
        }

        private void LoadInterviewHistory()
        {
            if (phInterview.FindControl("ucInterviewHistory") == null)
            {
                Controls_InterviewHistory ucInterviewHistory = (Controls_InterviewHistory)this.LoadControl("InterviewHistory.ascx");
                ucInterviewHistory.ID = "ucInterviewHistory";
                ucInterviewHistory.InterviewID = interview.InterviewId;
                phInterview.Controls.Add(ucInterviewHistory);
            }
        }

        private void LoadMentorSuggestion()
        {
            if (phInterview.FindControl("ucMentorSuggestion") == null)
            {
                Controls_MentorSuggestion uc = (Controls_MentorSuggestion)this.LoadControl("MentorSuggestion.ascx");
                uc.ID = "ucMentorSuggestion";                
                uc.InterviewID = interview.InterviewId;

                /*
                 * Modify Interview Process
                 * Author: Yin.P
                 * Date: 2010-1-8
                 */
                uc.SetCheckInFormMentor(interview.MentorAlias);
                phInterview.Controls.Add(uc);
            }
        }

        private void LoadUploadingMentorSuggestion()
        {
            if (phInterview.FindControl("ucUploadingMentorSuggestion") == null)
            {
                Controls_UploadingMentorSuggestion uc = (Controls_UploadingMentorSuggestion)this.LoadControl("UploadingMentorSuggestion.ascx");
                uc.ID = "ucUploadingMentorSuggestion";
                uc.InterviewID = interview.InterviewId;
                phInterview.Controls.Add(uc);
            }
        }
        #endregion

        //初始化面试信息
        private void InitInterviewInfo(Guid applicantId)
        {
            ApplicantRelatedInfo ari = ApplicantRelatedInfo.GetApplicantRelatedInfoById(applicantId);
            if (ari == null)
            {
                pMessage.Visible = true;
                litMessage.Text = "Can not find the interview history related to this candidate!";
                return;
            }
            //recommender have no right to see corresponding candidate's interview information
            if (ari.JobInfoDetail == SiteUser.Current.Alias)
            {
                pMessage.Visible = true;
                litMessage.Text = "You have no right to see the interview history!";
                return;
            }
            Boolean isLoadInterviewHistory = false;
            Boolean isLoadInterviewFeedback = false;
            Boolean isLoadIncompleteList = false;
            Int32 FeedbackId = 0;
            Boolean isLoadMentorSuggestion = false;
            Boolean isLoadMentorResult = false;
            Boolean isLoadUploadingMentorSuggestion = false;
            Boolean isLoadManagerApproval = false;
            Boolean isLoadUploadingManagerApproval = false;
            Boolean isLoadManagerResult = false;
            Boolean MentorisInterview = false;
            Boolean isEmpty = true;
            //more than one interview


            //check if interviewer
            /*
             * TODO: If an interviewer should access(read) the feedbacks of other interview process for one person, 
             * here would be modified to meet this requirement.
             */
            DataSet dsFeedback = Feedback.GetFeedbackByInterview(interview.InterviewId);
            String tempAlias = "";
            for (Int32 j = 0; j < dsFeedback.Tables[0].Rows.Count; j++)
            {
                tempAlias = dsFeedback.Tables[0].Rows[j]["InterviewerAlias"].ToString();
                if (tempAlias == SiteUser.Current.Alias)//one interviewer,interviewed
                {
                    isLoadInterviewHistory = true;
                    isEmpty = false;
                    break;
                }
            }
            DataSet dsIFeedback = Feedback.GetIncompleteFeedbackByInterview(interview.InterviewId);
            for (Int32 j = 0; j < dsIFeedback.Tables[0].Rows.Count; j++)
            {
                tempAlias = dsIFeedback.Tables[0].Rows[j]["InterviewerAlias"].ToString();
                if (tempAlias == SiteUser.Current.Alias)//one interviewer, not interview
                {
                    isLoadInterviewHistory = true;
                    isLoadInterviewFeedback = true;
                    FeedbackId = Convert.ToInt32(dsIFeedback.Tables[0].Rows[j]["FeedbackId"]);
                    isEmpty = false;
                    break;
                }
            }

            /**
             * Modified By: Yin.P
             * Date: 2010-1-8
             * Reason: Mentor can also access the checkinform.
             */
            if (SiteUser.Current.SiteUserId == interview.HiringManagerId || SiteUser.Current.Alias == interview.MentorAlias)//Mentor
            {

                //Mentor and interviewer is the same one
                if (Feedback.CheckInterviewSameAsMentor(interview.InterviewId, SiteUser.Current.Alias))
                {
                    MentorisInterview = true;
                    //litTips.Text = "Mentor and Interviewer is the same one.";
                    if (interview.InterviewStatus == InterviewStatusEnum.WaitingForInterviewFeedback)
                    {
                        //LoadMentorSuggestion();
                        isLoadIncompleteList = true;
                        isLoadMentorSuggestion = true;
                        isEmpty = false;
                    }
                    else if (interview.InterviewStatus == InterviewStatusEnum.WaitingForGroupManagerDecision)
                    {
                        //LoadInterviewHistory();
                        //LoadMentorResult();
                        isLoadInterviewFeedback = true;
                        isLoadInterviewHistory = true;
                        isLoadMentorResult = true;
                        isEmpty = false;
                    }
                    else if (interview.InterviewStatus == InterviewStatusEnum.Hired || interview.InterviewStatus == InterviewStatusEnum.Rejected || interview.InterviewStatus == InterviewStatusEnum.QualifiedButNotMatched)
                    {
                        //LoadInterviewHistory();
                        //LoadMentorResult();
                        //LoadManagerResult();
                        isLoadInterviewHistory = true;
                        isLoadMentorResult = true;
                        isLoadManagerResult = true;
                    }
                }
                else
                {
                    if (interview.InterviewStatus == InterviewStatusEnum.WaitingForInterviewFeedback)
                    {
                        //LoadInterviewHistory();
                        //LoadIncompleteList();
                        //LoadMentorSuggestion();
                        isLoadInterviewHistory = true;
                        isLoadIncompleteList = true;
                        if (!isLoadInterviewFeedback)
                        {
                            isLoadMentorSuggestion = true;
                        }
                        else
                        {
                            isLoadMentorSuggestion = false;
                        }
                        isEmpty = false;
                    }
                    else if (interview.InterviewStatus == InterviewStatusEnum.WaitingForMentorDecision)
                    {
                        //LoadInterviewHistory();
                        //LoadMentorSuggestion();
                        isLoadInterviewHistory = true;
                        if (!isLoadInterviewFeedback)
                        {
                            isLoadMentorSuggestion = true;
                        }
                        else
                        {
                            isLoadMentorSuggestion = false;
                        }
                        isEmpty = false;
                    }
                    else if (interview.InterviewStatus == InterviewStatusEnum.WaitingForGroupManagerDecision || (interview.InterviewStatus == InterviewStatusEnum.OfferDeclined && interview.GroupManagerId == Guid.Empty))
                    {
                        //LoadInterviewHistory();
                        //LoadMentorResult();
                        isLoadInterviewHistory = true;
                        isLoadMentorResult = true;
                        isEmpty = false;
                    }
                    else if (interview.InterviewStatus == InterviewStatusEnum.Hired || interview.InterviewStatus == InterviewStatusEnum.Rejected || interview.InterviewStatus == InterviewStatusEnum.QualifiedButNotMatched || (interview.InterviewStatus == InterviewStatusEnum.OfferDeclined && interview.GroupManagerId != Guid.Empty))
                    {
                        //LoadInterviewHistory();
                        //LoadMentorResult();
                        //LoadManagerResult();
                        isLoadInterviewHistory = true;
                        isLoadMentorResult = true;
                        isLoadManagerResult = true;
                        isEmpty = false;
                    }

                }
            }
            if (SiteUser.Current.SiteUserId == interview.GroupManagerId)//Manager
            {
                isEmpty = false;
                if (interview.InterviewStatus == InterviewStatusEnum.WaitingForGroupManagerDecision)
                {
                    isLoadManagerApproval = true;
                }
            }

            //是经理 || 招聘人员
            Boolean isGM = SiteUser.Current.IsInRole(RoleType.GroupManager.ToString());
            Boolean isIR = SiteUser.Current.IsInRole(RoleType.InternRecruiter.ToString());
            if (isGM || isIR)
            {
                isLoadInterviewHistory = true;
                isEmpty = false;
                if (interview.InterviewStatus == InterviewStatusEnum.WaitingForGroupManagerDecision || (interview.InterviewStatus == InterviewStatusEnum.OfferDeclined && interview.GroupManagerId == Guid.Empty))
                {
                    isLoadMentorResult = true;
                }
                else if (interview.InterviewStatus == InterviewStatusEnum.Hired || interview.InterviewStatus == InterviewStatusEnum.Rejected || interview.InterviewStatus == InterviewStatusEnum.QualifiedButNotMatched || (interview.InterviewStatus == InterviewStatusEnum.OfferDeclined && interview.GroupManagerId != Guid.Empty))
                {
                    isLoadMentorResult = true;
                    isLoadManagerResult = true;
                }
            }
            if (isIR)
            {
                isLoadIncompleteList = true;
                if (interview.InterviewStatus == InterviewStatusEnum.WaitingForGroupManagerDecision && interview.GroupManagerId != SiteUser.Current.SiteUserId)
                    isLoadUploadingManagerApproval = true;
                if ((interview.InterviewStatus == InterviewStatusEnum.WaitingForMentorDecision || interview.InterviewStatus == InterviewStatusEnum.WaitingForInterviewFeedback) && interview.HiringManagerId != SiteUser.Current.SiteUserId)
                {
                    isLoadInterviewHistory = true;
                    isLoadUploadingMentorSuggestion = true;
                    isEmpty = false;
                }
            }

            String[] ss = SiteUser.Current.GetRoles();
            Boolean isOnBoardManager = SiteUser.Current.IsInRole(RoleType.OnBoardManager.ToString());

            if (isOnBoardManager)
            {
                if (interview.InterviewStatus == InterviewStatusEnum.Hired || interview.InterviewStatus == InterviewStatusEnum.Rejected || interview.InterviewStatus == InterviewStatusEnum.QualifiedButNotMatched || interview.InterviewStatus == InterviewStatusEnum.OfferDeclined)
                {
                    isLoadInterviewHistory = true;
                    isLoadMentorResult = true;
                    isLoadManagerResult = true;
                    isEmpty = false;
                    //isLoadUploadingManagerApproval = true;
                }
                if (interview.InterviewStatus == InterviewStatusEnum.WaitingForGroupManagerDecision && interview.GroupManagerId != SiteUser.Current.SiteUserId)
                {
                    isLoadInterviewHistory = true;
                    isLoadMentorResult = true;
                    isEmpty = false;
                    isLoadUploadingManagerApproval = true;
                }
                if ((interview.InterviewStatus == InterviewStatusEnum.WaitingForMentorDecision || interview.InterviewStatus == InterviewStatusEnum.WaitingForInterviewFeedback) && interview.HiringManagerId != SiteUser.Current.SiteUserId)
                {
                    isLoadInterviewHistory = true;
                    isLoadUploadingMentorSuggestion = true;
                    isEmpty = false;
                }
            }
            //if (isLoadInterviewFeedback)//is mentor and interviewer is the same one.need to do interview 
            //{
            //    isLoadMentorSuggestion = false;
            //}
            if (isLoadInterviewHistory)
            {
                LoadInterviewHistory();
            }
            if (isLoadIncompleteList)
            {
                LoadIncompleteList();
            }
            if (isLoadInterviewFeedback && !MentorisInterview)
            {
                LoadInterviewFeedback(FeedbackId);
            }
            if (isLoadMentorSuggestion)
            {
                LoadMentorSuggestion();//只有这里可以选group manager 的下拉菜单
            }
            if (isLoadMentorResult)
            {
                LoadMentorResult();
            }
            if (isLoadUploadingMentorSuggestion)
            {
                LoadUploadingMentorSuggestion();
            }
            if (isLoadManagerApproval)
            {
                LoadManagerApproval();
            }
            if (isLoadManagerResult)
            {
                LoadManagerResult();
            }
            if (isLoadUploadingManagerApproval)
            {
                LoadUploadingManagerApproval();
            }
            if (isEmpty)
            {
                pMessage.Visible = true;
                litMessage.Text = "You have no right to see the interview History!";
            }
            else
            {
                pMessage.Visible = false;
            }
        }
    }
}