using System;
using MSRA.SpringField.Components.BizObjects;
using MSRA.SpringField.Components;
using MSRA.SpringField.Components.Enumerations;
using MSRA.SpringField.Components.Configuration;

namespace MSRA.SpringField.Application.Controls
{
    public partial class Controls_ApplicantInfoPanel : System.Web.UI.UserControl
    {
        public Guid ApplicantID
        {
            set { ViewState["applicantid"] = value; }
            get
            {
                if (ViewState["applicantid"] != null) return new Guid(ViewState["applicantid"].ToString());
                else
                    return Guid.Empty;
            }
        }
        private readonly string DocPath = SiteConfiguration.GetConfig().SiteAttributes["docUrl"];
        protected void Page_Load(object sender, EventArgs e)
        {
            if (ApplicantID != Guid.Empty)
            {
                Applicant applicant = new Applicant(ApplicantID);

                PopulateBasicInfo(applicant);
                PopulateEduBackground(applicant);
                PopulateRelatedInfo(applicant);
            }
        }

        private void PopulateBasicInfo(Applicant applicant)
        {
            ApplicantBasicInfo abi = applicant.BasicInfo;
            lbChineseName.Text = abi.NameInChinese;
            lbGender.Text = EnumHelper.EnumToString(abi.Gender);            
            lbNationality.Text = abi.Nationality;
            lbEmail.Text = abi.Email;
            lbWebPage.Text = abi.WebPage;
            lbPhoneNum.Text = abi.PhoneNumber;
            lbCity.Text = abi.CurrentCity;
            lbCountry.Text = abi.CurrentCountry;
        }

        private void PopulateEduBackground(Applicant applicant)
        {
            ApplicantEduBackground aeb = applicant.EduBackground;
            lbEduIns.Text = aeb.HighestEduInstitution;
            lbMajorCategory.Text = StaticData.MajorDict[aeb.MajorCategory];
            lbMajor.Text = aeb.Major;
            lbYearOfStudy.Text = aeb.YearOfStudy.ToString();
            lbRank.Text = EnumHelper.EnumToString(aeb.Rank);
            lbAdvisorFirstName.Text = aeb.InternAdvisor.FirstName;
            lbAdvisorLastName.Text = aeb.InternAdvisor.LastName;
            lbAdvisorEmail.Text = aeb.InternAdvisor.Email;
            lbOrg.Text = aeb.InternAdvisor.Organization;
            lbEnrollDate.Text = (aeb.EnrollDate.Date == DateTime.MaxValue.Date) ? "" : aeb.EnrollDate.ToShortDateString();
            lbGraduateDate.Text = (aeb.GraduateDate.Date == DateTime.MaxValue.Date) ? "" : aeb.GraduateDate.ToShortDateString();

            if (aeb.Resume.DocId != 0)
            {
                lnkCV.Text = "Download";
                lnkCV.NavigateUrl = DocPath + aeb.Resume.SaveName;
            }
            if (aeb.Papers[0].DocId != 0)
            {
                lnkPaperA.Text = "Paper 1";
                lnkPaperA.NavigateUrl = DocPath + aeb.Papers[0].SaveName;
            }
            if (aeb.Papers[1].DocId != 0)
            {
                lnkPaperB.Text = "Paper 2";
                lnkPaperB.NavigateUrl = DocPath + aeb.Papers[1].SaveName;
            }
        }

        private void PopulateRelatedInfo(Applicant applicant)
        {
            ApplicantRelatedInfo ari = applicant.RelatedInfo;
            lbFirstAvail.Text = ari.PreferredAvailStartDate.ToShortDateString();
            lbSecondAvail.Text = ari.SecondaryAvailStartDate.ToShortDateString();
            lbGroup.Text = ari.InterestedGroup;
            lbAreas.Text = ari.InterestedAreas;
            lbInternType.Text = EnumHelper.EnumToString(ari.InternshipType);
            string preferredPosition = "";
            switch (ari.PreferredPosition)
            {
                case PositionTypeEnum.EngineeringIntern:
                    preferredPosition = "Engineering Oriented";
                    break;
                case PositionTypeEnum.ResearchIntern:
                    preferredPosition = "Research Oriented";
                    break;
                case PositionTypeEnum.Unknown:
                    preferredPosition = "Unknown";
                    break;
            }

            lbPreferredPosition.Text = preferredPosition;
            lbSpecialProgram.Text = ari.SpecialProgram;
            lbInfoSource.Text = "Source:[" + ari.JobInfoSource + "] Channel:[" + ari.JobInfoChannel + "] Detail:[" + ari.JobInfoDetail + "]";
        }
    }
}