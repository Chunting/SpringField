/*****************************************************************************

Microsoft Research Asia
Copyright (c) 2006 Microsoft Corporation

Module Name: 
		RelatedReferrer.cs

Author:
        Yuan Chen (f-yuchen@msrchina.research.microsoft.com)  
        MSRA/MS^2.3/Compass Team
 
Abstract:
        Class used to store information for a related referrer
 
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
    public class RelatedReferrer
    {
        #region RelatedReferrer
        private int relaterId;
        private int referralId;
        private string email;
        private string firstName;
        private string lastName;
        private GenderEnum gender; 
        #endregion

        #region Constructors
        public RelatedReferrer()
        {
            relaterId = 0;
            referralId = 0;
            email = string.Empty;
            firstName = string.Empty;
            lastName = string.Empty;
        }

        //public RelatedReferrer(int id)
        //{

        //}
        #endregion

        #region Methods
        public void Insert()
        {
            IDataProvider dp = DataProviderFactory.GetDataProvider();
            relaterId = dp.InsertRelatedReferrer(this);
        }

        public void Update()
        {
            IDataProvider dp = DataProviderFactory.GetDataProvider();
            dp.UpdateRelatedReferrer(this);
        } 
        #endregion

        #region Static Helper Methods
        public static void DeleteRelaterById(int id)
        {
            IDataProvider dp = DataProviderFactory.GetDataProvider();
            dp.DeleteRelatedReferrer(id);
        }

        public static RelatedReferrer GetRelaterById(int id)
        {
            IDataProvider dp = DataProviderFactory.GetDataProvider();
            return dp.GetRelatedReferrer(id);
        }

        public static List<RelatedReferrer> GetRelatedReferrerForReferral(int referralId)
        {
            IDataProvider dp = DataProviderFactory.GetDataProvider();
            return dp.GetRelatedReferrerForReferral(referralId);
        } 
        #endregion

        #region Properties
        public int RelaterId
        {
            get { return relaterId; }
            set { relaterId = value; }
        }

        public int ReferralId
        {
            get { return referralId; }
            set { referralId = value; }
        }

        public string Email
        {
            get { return email; }
            set { email = ((value == null) ? string.Empty : value); }
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

        public GenderEnum Gender
        {
            get { return gender; }
            set { gender = value; }
        } 
        #endregion
    }
}
