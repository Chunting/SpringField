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
using Springfield.Components.Configuration;

public partial class Controls_BasicInfo : System.Web.UI.UserControl
{
//    private Guid applicantId;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            BindData();
        }
    }

    public void BindData()
    {

        Applicant applicant = new Applicant(ApplicantId);
        if((applicant.BasicInfo.Status == ApplicationStatusEnum.OnBoard) && (SiteUser.Current.IsInRole(RoleType.OnBoardManager) || SiteUser.Current.IsInRole(RoleType.InternRecruiter)))
            trPAURL.Visible = true;
        if (applicant == null)
        {
            JSUtility.Alert(this.Page, "Invalid parameter!");
            return;
        }
        SiteConfiguration config = SiteConfiguration.GetConfig();

        lnkApplicantName.Text = String.Format("{0} {1}", applicant.BasicInfo.FirstName, applicant.BasicInfo.LastName.ToUpper());
        lnkApplicantName.NavigateUrl = "~/ShowApplication.aspx?applicant=" + applicant.ApplicantId.ToString();

        lbArea.Text = applicant.RelatedInfo.InterestedAreas;
        lbDegree.Text = StaticData.DegreeList[(int)applicant.EduBackground.Degree] + applicant.EduBackground.YearOfStudy.ToString();
        lbMajor.Text = applicant.EduBackground.Major;
        lbApplyDate.Text = applicant.BasicInfo.ApplicationTime.ToShortDateString();
        //lbSchool.Text = EnumHelper.EnumToString(applicant.EduBackground.HighestEduInstitution);
        lbSchool.Text = applicant.EduBackground.HighestEduInstitution;

        //string interviewId = Interview.GetRecentInterviewIdByApplicant(applicant.ApplicantId);
        //if (interviewId != null && interviewId != string.Empty)
        //{
        //    Interview interview = Interview.GetInterviewById(Convert.ToInt32(interviewId));
        //    if (interview.InterviewStatus == InterviewStatusEnum.Hired)
        //    {
        //        if (SiteUser.Current.IsInRole(RoleType.InternRecruiter)
        //            || SiteUser.Current.IsInRole(RoleType.OnBoardManager))
        //        {
        //            hlNewHirePackage.Text = "Download";
        //            hlNewHirePackage.NavigateUrl = "~/NewHirePackage.ashx?applicantid=" + applicant.ApplicantId.ToString();
        //        }
        //    }
        //}

        if (applicant.BasicInfo.Status == ApplicationStatusEnum.Hired || applicant.BasicInfo.Status == ApplicationStatusEnum.OnBoard)
        {
            if (SiteUser.Current.IsInRole(RoleType.InternRecruiter)
                || SiteUser.Current.IsInRole(RoleType.OnBoardManager))
            {
                hlNewHirePackage.Text = "Download";
                hlNewHirePackage.NavigateUrl = "~/NewHirePackage.ashx?applicantid=" + applicant.ApplicantId.ToString();
            }
        }

        if (applicant.EduBackground.Resume.DocId != 0 && applicant.EduBackground.Resume != null)
        {
            //lnkResume.Text = applicant.EduBackground.Resume.OriginalName;
            lnkResume.Text = "Resume";
            lnkResume.NavigateUrl = config.SiteAttributes["docUrl"] + applicant.EduBackground.Resume.SaveName;
        }

        if (applicant.EduBackground.Papers[0].DocId != 0 && applicant.EduBackground.Papers[0] != null)
        {
            //lnkPaperA.Text = applicant.EduBackground.Papers[0].OriginalName;
            lnkPaperA.Text = "Paper A";
            lnkPaperA.NavigateUrl = config.SiteAttributes["docUrl"] + applicant.EduBackground.Papers[0].SaveName;
        }

        if (applicant.EduBackground.Papers[1].DocId != 0 && applicant.EduBackground.Papers[1] != null)
        {
            //lnkPaperB.Text = applicant.EduBackground.Papers[1].OriginalName;
            lnkPaperB.Text = "Paper B";
            lnkPaperB.NavigateUrl = config.SiteAttributes["docUrl"] + applicant.EduBackground.Papers[1].SaveName;
        }
        lbApplicant.Text = StaticData.AppStatusDict[applicant.BasicInfo.Status];

        lbPAURL.Text = config.SiteAttributes["InternPASite"] + "?ApplicantId=" + applicant.ApplicantId.ToString();
        //Bind Interview Status
        //DataSet ds = Interview.GetInterviewForApplicant(ApplicantId);
        //if (ds.Tables[0].Rows.Count == 0)
        //{
        //    lbInterviewStatus.Text = "Not Started a interview.";
        //    lbNeedToDo.Text = "Schedule Interview.";
        //}
        //else
        //{
        //    int interviewId = Convert.ToInt32(ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1]["interviewId"]);
        //    Interview interview = Interview.GetInterviewById(interviewId);
        //    if (interview.InterviewStatus == InterviewStatusEnum.InterviewStart)
        //    {
        //        lbInterviewStatus.Text = "Interview Started.";
        //        lbNeedToDo.Text = "Interviewers need to submit their feedbacks.";
        //    }
        //    else if (interview.InterviewStatus == InterviewStatusEnum.InterviewComplete)
        //    {
        //        lbInterviewStatus.Text = "all interviewers have submitted their feedbacks.";
        //        lbNeedToDo.Text = "mentor need to submit his/her suggestion.";
        //    }
        //    else if (interview.InterviewStatus == InterviewStatusEnum.WaitingForGroupManagerDecision)
        //    {
        //        lbInterviewStatus.Text = "mentor have submitted his/her suggestion.";
        //        lbNeedToDo.Text = "GroupManger need to submit his/her decision.";
        //    }
        //    else if (interview.InterviewStatus == InterviewStatusEnum.Hired)
        //    {
        //        lbInterviewStatus.Text = "Hired.";
        //        lbNeedToDo.Text = "Available now. waiting for next interview.";
        //    }
        //    else if (interview.InterviewStatus == InterviewStatusEnum.Rejected)
        //    {
        //        lbInterviewStatus.Text = "Rejected.";
        //        lbNeedToDo.Text = "Available now. waiting for next interview.";
        //    }
        //    else if (interview.InterviewStatus == InterviewStatusEnum.DeclineOffer)
        //    {
        //        lbInterviewStatus.Text = "Declined Offer.";
        //        lbNeedToDo.Text = "Available now. waiting for next interview.";
        //    }
        //}
    }

    public Guid ApplicantId
    {
        set
        {
            ViewState["applicantId"] = value;
        }
        get
        {
            return new Guid(ViewState["applicantId"].ToString());
        }
    }
}
