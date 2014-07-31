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
using MSRA.SpringField.Components;
using MSRA.SpringField.Components.Configuration;

namespace MSRA.SpringField.Application.Controls
{
    public partial class Controls_BasicInfo : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["applicant"] != null)
            {
                this.ApplicantId = new Guid(Request.QueryString["applicant"].ToString());
            }
            else
            {
                this.ApplicantId = new Guid(Convert.ToString(Session["ApplicantId"]));
            }
            if (!this.IsPostBack)
            {
                BindData();
            }
        }

        public void BindData()
        {

            Applicant applicant = new Applicant(ApplicantId);

            //--------------------------change by bin 2011-9-9--------------------------
            if ((applicant.BasicInfo.Status == ApplicationStatusEnum.OnBoard) && (SiteUser.Current.IsInRole(RoleType.OnBoardManager) || SiteUser.Current.IsInRole(RoleType.InternRecruiter)))
            {
                trPAURL.Visible = true;

                //Add by yuanqin, 2011.5.25
                trSurveyURL.Visible = true;
            }

            //if (true && (SiteUser.Current.IsInRole(RoleType.OnBoardManager) || SiteUser.Current.IsInRole(RoleType.InternRecruiter)))
            //{
            //    trPAURL.Visible = true;

            //    //Add by yuanqin, 2011.5.25
            //    trSurveyURL.Visible = true;
            //}

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
            if (applicant.BasicInfo.ApplicationTime.Equals(DateTime.MinValue))
            {
                lbApplyDate.Text = "";
            }
            else
            {
                lbApplyDate.Text = applicant.BasicInfo.ApplicationTime.ToShortDateString();
            }
            lbSchool.Text = applicant.EduBackground.HighestEduInstitution;

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
                lnkResume.Text = "Download or View";
                lnkResume.Target = "_blank";
                lnkResume.NavigateUrl = config.SiteAttributes["docUrl"] + applicant.EduBackground.Resume.SaveName;  
            }

            if (applicant.EduBackground.Papers[0].DocId != 0 && applicant.EduBackground.Papers[0] != null)
            {
                lnkPaperA.Text = "Paper A";
                lnkPaperA.NavigateUrl = config.SiteAttributes["docUrl"] + applicant.EduBackground.Papers[0].SaveName;
            }

            if (applicant.EduBackground.Papers[1].DocId != 0 && applicant.EduBackground.Papers[1] != null)
            {
                lnkPaperB.Text = "Paper B";
                lnkPaperB.NavigateUrl = config.SiteAttributes["docUrl"] + applicant.EduBackground.Papers[1].SaveName;
            }
            lbApplicant.Text = StaticData.AppStatusDict[applicant.BasicInfo.Status];

            lbPAURL.NavigateUrl =
                lbPAURL.Text = config.SiteAttributes["InternPASite"] + "?InterviewId=" + Convert.ToString(Session["InterviewId"]) + "&ApplicantId=" + applicant.ApplicantId.ToString();

            //Add by Yuanqin,2011.5.25
            lbSurveyURL.NavigateUrl =
                lbSurveyURL.Text = config.SiteAttributes["InternSurveySite"] + "?InterviewId=" + Convert.ToString(Session["InterviewId"]) + "&ApplicantId=" + applicant.ApplicantId.ToString();
            
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
}