/*****************************************************************************

Microsoft Research Asia
Copyright (c) 2006 Microsoft Corporation

Module Name: 
		ApplicantBasicInfo.cs

Author:
        Yuan Chen (f-yuchen@msrchina.research.microsoft.com)  
        MSRA/MS^2.3/Compass Team
 
Abstract:
        Class used to store basic information of Applicant

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
    public class ApplicantBasicInfo
    {
        #region Private Members
        private Guid applicantId;
        private string firstName;
        private string lastName;
        private string email;
        private GenderEnum gender;

        //private CountryEnum nationality;
        private string nationnality;
        //private int nationnality;

        private string identityNumber;
        private string webpage;
        private string address;
        private string phoneNumber;
        private string currentCity;
        private string currentProvince;

        //private CountryEnum currentCountry;
        private string currentCountry;
        //private int currentCountry;

        private int priority;
        private ApplicationStatusEnum status;
        private DateTime applicationTime;
        private int referralId;
        private string chineseName;

        /*
         * Add by Yuanqin
         * 2011.2.21
         * For Offline
         */
        private IsOfflineEnum isOffline;

        #endregion

        #region Constructors
        public ApplicantBasicInfo()
        {
            gender = GenderEnum.Male;

            //nationality = CountryEnum.China;
            //currentCountry = CountryEnum.China;
            nationnality = string.Empty;
            currentCity = string.Empty;
            //nationnality = 43; //china
            //CurrentCountry = 43; //china

            status = ApplicationStatusEnum.Available;
            applicantId = Guid.Empty;
            firstName = string.Empty;
            lastName = string.Empty;
            email = string.Empty;
            identityNumber = string.Empty;
            webpage = string.Empty;
            address = string.Empty;
            phoneNumber = string.Empty;
            currentCountry = string.Empty;
            currentCity = string.Empty;
            currentProvince = string.Empty;
            priority = 0;
            applicationTime = DateTime.Now;
            referralId = 0;
            chineseName = string.Empty;

            //Add by Yuanqin
            isOffline = IsOfflineEnum.Online;
        }

        //public ApplicantBasicInfo(string applicantId)
        //{
        //    //Add member to RoleManager
        //} 
        #endregion

        #region Methods
        public void Insert()
        {
            IDataProvider dp = DataProviderFactory.GetDataProvider();
            dp.InsertApplicantBasicInfo(this);
        }

        public void Update()
        {
            IDataProvider dp = DataProviderFactory.GetDataProvider();
            dp.UpdateApplicantBasicInfo(this);
        }

        //public void Delete()
        //{

        //} 
        #endregion

        #region Static Helper Methods
        public static ApplicantBasicInfo GetApplicantBasicInfoById(Guid id)
        {
            IDataProvider dp = DataProviderFactory.GetDataProvider();
            return dp.GetApplicantBasicInfoById(id);
        }

        public static void DeleteApplicantBasicInfoById(Guid id)
        {
            IDataProvider dp = DataProviderFactory.GetDataProvider();
            dp.DeleteApplicantBasicInfoById(id);
        }

        public static void ChangeApplicantStatus(Guid id, ApplicationStatusEnum status)
        {
            IDataProvider dp = DataProviderFactory.GetDataProvider();
            dp.ChangeApplicantStatus(id, status);
        }
        #endregion

        #region Properties
        public Guid ApplicantId
        {
            get { return applicantId; }
            set { applicantId = value; }
        }

        public string FirstName
        {
            get { return firstName; }
            set { firstName = ((value == null) ? string.Empty : value); }
        }

        public string LastName
        {
            get { return lastName; }
            set { lastName = ((value == null) ? string.Empty : value); }
        }

        public string Email
        {
            get { return email; }
            set { email = ((value == null) ? string.Empty : value); }
        }

        public GenderEnum Gender
        {
            get { return gender; }
            set { gender = value; }
        }

        //public CountryEnum Nationality
        //{
        //    get { return nationality; }
        //    set { nationality = value; }
        //}

        public string Nationality
        {
            get { return nationnality; }
            set { nationnality = ((value == null) ? string.Empty : value); }
        }

        public string IdentityNumber
        {
            get { return identityNumber; }
            set { identityNumber = ((value == null) ? string.Empty : value); }
        }

        public string WebPage
        {
            get { return webpage; }
            set { webpage = ((value == null) ? string.Empty : value); }
        }

        public string Address
        {
            get { return address; }
            set { address = ((value == null) ? string.Empty : value); }
        }

        public string PhoneNumber
        {
            get { return phoneNumber; }
            set { phoneNumber = ((value == null) ? string.Empty : value); }
        }

        public string CurrentCity
        {
            get { return currentCity; }
            set { currentCity = ((value == null) ? string.Empty : value); }
        }

        public string CurrentProvince
        {
            get { return currentProvince; }
            set { currentProvince = ((value == null) ? string.Empty : value); }
        }

        //public CountryEnum CurrentCountry
        //{
        //    get { return currentCountry; }
        //    set { currentCountry = value; }
        //}

        public string CurrentCountry
        {
            get { return currentCountry; }
            set { currentCountry = ((value == null) ? string.Empty : value); }
        }

        public int Priority
        {
            get { return priority; }
            set { priority = value; }
        }

        public ApplicationStatusEnum Status
        {
            get { return status; }
            set { status = value; }
        }

        public DateTime ApplicationTime
        {
            get { return applicationTime; }
            set { applicationTime = value; }
        }

        public int ReferralId
        {
            get { return referralId; }
            set { referralId = value; }
        }

        public string NameInChinese
        {
            get { return chineseName; }
            set { chineseName = ((value == null) ? string.Empty : value); }
        }

        //Add by Yuanqin
        public IsOfflineEnum IsOffline
        {
            get { return isOffline; }
            set { isOffline = value; }
        }
        #endregion
    }
}
