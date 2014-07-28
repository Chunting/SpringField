/*****************************************************************************

Microsoft Research Asia
Copyright (c) 2006 Microsoft Corporation

Module Name: 
		Referral.cs

Author:
        Yuan Chen (f-yuchen@msrchina.research.microsoft.com)  
        MSRA/MS^2.3/Compass Team
 
Abstract:
        Class used to store information for a referral
 
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
    public class Referral
    {
        #region Private Members
        private int referralId;
        private ReferralType type;
        private Guid applicantId;
        private Guid referrerId;
        private DateTime referredTime;
        private List<RelatedReferrer> relaters; //This item is seted for the refferer who is out of MSRA
        #endregion

        #region Constuctors
        public Referral()
        {
            referralId = 0;
            applicantId = Guid.Empty;
            referrerId = Guid.Empty;
            relaters = null;
            referredTime = DateTime.Now;
            relaters = new List<RelatedReferrer>();
        }

        //public Referral(int id)
        //{

        //} 
        #endregion

        #region Methods
        public void Insert()
        {
            IDataProvider dp = DataProviderFactory.GetDataProvider();
            referralId = dp.InsertReferral(this);
            if (relaters != null)
            {
                foreach (RelatedReferrer rr in relaters)
                {
                    rr.ReferralId = referralId;
                    rr.Insert();
                }
            }
        }

        public void Update()
        {
            if (relaters != null)
            {
                foreach (RelatedReferrer rr in relaters)
                {
                    rr.Update();
                }
            }
            IDataProvider dp = DataProviderFactory.GetDataProvider();
            dp.UpdateReferral(this);
        } 
        #endregion

        #region Static Helper Methods
        public static void DeleteReferralById(int id)
        {
            IDataProvider dp = DataProviderFactory.GetDataProvider();
            dp.DeleteReferral(id);
        }

        public static Referral GetReferralById(int id)
        {
            IDataProvider dp = DataProviderFactory.GetDataProvider();
            return dp.GetReferralById(id);
        }

        #endregion

        #region Properties
        public int ReferralId
        {
            get { return referralId; }
            set { referralId = value; }
        }

        public ReferralType Type
        {
            get { return type; }
            set { type = value; }
        }

        public Guid ApplicantId
        {
            get { return applicantId; }
            set { applicantId = value; }
        }

        public Guid ReferrerId
        {
            get { return referrerId; }
            set { referrerId = value; }
        }

        public List<RelatedReferrer> Relaters
        {
            get { return relaters; }
            set { relaters = value; }
        }

        public DateTime ReferredTime
        {
            get { return referredTime; }
            set { referredTime = value; }
        }
        #endregion
    }
}
