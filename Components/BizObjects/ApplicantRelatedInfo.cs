/*****************************************************************************

Microsoft Research Asia
Copyright (c) 2006 Microsoft Corporation

Module Name: 
		ApplicantRelatedInfo.cs

Author:
        Yuan Chen (f-yuchen@msrchina.research.microsoft.com)  
        MSRA/MS^2.3/Compass Team
 
Abstract:
        Class used to store related information of Applicant
 
Remarks:
        This class should be used out of the Springfield.Components assembly, so it should be declared as an public class.
             
Environment:
		Class Library/.NET Framework 2.0 

Project:
        Springfield.Components for MSRA Intern Application Tracking System (MIATS), codename: Springfield;
        MSRA/MS^2.3/Compass Team

Revision History:			
		30/Apr/2006 Created by Yuan Chen

*****************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using MSRA.SpringField.Components.Enumerations;
using MSRA.SpringField.Components.Configuration;

namespace MSRA.SpringField.Components.BizObjects
{
    [Serializable]
    public class ApplicantRelatedInfo
    {
        #region Private Member
        private Guid applicantId;
        private DateTime preferredAvailStartDate;
        //secondaryAvailStartDate is change to the internship till date
        private DateTime secondaryAvailStartDate;

        //private GroupsEnum interestedGroup;
        private string interestedGroup;
        private PositionTypeEnum preferredPosition;
        private string interestedArea;
        private InternshipTypeEnum internshipType;
        private string specialProgram;

        //private InfoSourceEnum infoSource;
        //private InfoSourceDetailEnum infoSourceDetail;
        //private string infoSourceText;
        private string jobInfoCategory;
        private string jobInfoSource;
        private string jobInfoChannel;
        private string jobInfoDetail; 
        #endregion

        #region Constructors
        public ApplicantRelatedInfo()
        {
            internshipType = InternshipTypeEnum.FullTime;
            preferredPosition = PositionTypeEnum.ResearchIntern;
            //infoSource = InfoSourceEnum.JobPosting;
            //infoSourceDetail = InfoSourceDetailEnum.MSRAHomepage;
            jobInfoCategory = string.Empty;
            jobInfoSource = string.Empty;
            jobInfoChannel = string.Empty;
            jobInfoDetail = string.Empty;

            //interestedGroup = GroupsEnum.TechnologyTransfer;
            interestedGroup = string.Empty;
            applicantId = Guid.Empty;
            preferredAvailStartDate = DateTime.Now;
            secondaryAvailStartDate = DateTime.Now;
            interestedArea = string.Empty;
            specialProgram = string.Empty;
            //infoSourceText = string.Empty;
        }

        //public ApplicantRelatedInfo(string id)
        //{

        //} 
        #endregion

        #region Methods
        public void Insert()
        {
            IDataProvider dp = DataProviderFactory.GetDataProvider();
            dp.InsertApplicantRelatedInfo(this);
        }

        public void Update()
        {
            IDataProvider dp = DataProviderFactory.GetDataProvider();
            dp.UpdateApplicantRelatedInfo(this);
        }

        //public void Delete()
        //{

        //} 
        #endregion

        #region Static Helper Methods
        public static void DeleteApplicantRelatedInfoById(Guid id)
        {
            IDataProvider dp = DataProviderFactory.GetDataProvider();
            dp.DeleteApplicantRelatedInfoById(id);
        }

        public static ApplicantRelatedInfo GetApplicantRelatedInfoById(Guid id)
        {
            IDataProvider dp = DataProviderFactory.GetDataProvider();
            return dp.GetApplicantRelatedInfoById(id);
        } 
        #endregion

        #region Properties
        public Guid ApplicantId
        {
            get { return applicantId; }
            set { applicantId = value; }
        }

        public DateTime PreferredAvailStartDate
        {
            get { return preferredAvailStartDate; }
            set { preferredAvailStartDate = value; }
        }

        public DateTime SecondaryAvailStartDate
        {
            get { return secondaryAvailStartDate; }
            set { secondaryAvailStartDate = value; }
        }

        //public GroupsEnum InterestedGroup
        //{
        //    get { return interestedGroup; }
        //    set { interestedGroup = value; }
        //}

        public string InterestedGroup
        {
            get { return interestedGroup; }
            set { interestedGroup = ((value == null) ? string.Empty : value); }
        }


        public string SpecialProgram
        {
            get { return specialProgram; }
            set { specialProgram = ((value == null) ? string.Empty : value); }
        }

        public InternshipTypeEnum InternshipType
        {
            get { return internshipType; }
            set { internshipType = value; }
        }

        public PositionTypeEnum PreferredPosition
        {
            get { return preferredPosition; }
            set { preferredPosition = value; }
        }


        public string InterestedAreas
        {
            get { return interestedArea; }
            set { interestedArea = ((value == null) ? string.Empty : value); }
        }

        //public InfoSourceEnum InfoSource
        //{
        //    get { return infoSource; }
        //    set { infoSource = value; }
        //}

        //public InfoSourceDetailEnum InfoSourceDetail
        //{
        //    get { return infoSourceDetail; }
        //    set { infoSourceDetail = value; }
        //}

        //public string InfoSourceText
        //{
        //    get { return infoSourceText; }
        //    set { infoSourceText = value; }
        //} 

        public string JobInfoCategory
        {
            get { return jobInfoCategory; }
            set { jobInfoCategory = ((value == null) ? string.Empty : value); }
        }

        public string JobInfoSource
        {
            get { return jobInfoSource; }
            set { jobInfoSource = ((value == null) ? string.Empty : value); }
        }

        public string JobInfoChannel
        {
            get { return jobInfoChannel; }
            set { jobInfoChannel = ((value == null) ? string.Empty : value); }
        }

        public string JobInfoDetail
        {
            get { return jobInfoDetail; }
            set { jobInfoDetail = ((value == null) ? string.Empty : value); }
        }
        #endregion
    }
}
