/*****************************************************************************

Microsoft Research Asia
Copyright (c) 2006 Microsoft Corporation

Module Name: 
		Applicant.cs

Author:
        Yuan Chen (f-yuchen@msrchina.research.microsoft.com)  
        MSRA/MS^2.3/Compass Team
 
Abstract:
        Class used to store Applicant information

Remarks:
        This class should be used out of the MSRA.SpringField.Components.BizObjects assembly, so it should be declared as an public class.
             
Environment:
		Class Library/.NET Framework 2.0 

Project:
        MSRA.SpringField.Components.BizObjects for MSRA Intern Application Tracking System (MIATS), codename: Springfield;
        MSRA/MS^2.3/Compass Team

Revision History:			
		30/Apr/2006 Created by Yuan Chen

*****************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Web.Security;
using MSRA.SpringField.Components.Enumerations;
using MSRA.SpringField.Components.Configuration;

namespace MSRA.SpringField.Components.BizObjects
{
    [Serializable]
    public class Applicant
    {
        #region Private Members
        private Guid applicantId;
        private ApplicantRelatedInfo relatedInfo;
        private ApplicantBasicInfo basicInfo;
        private ApplicantEduBackground eduBackground;
        #endregion

        #region Constructors
        public Applicant()
        {
            applicantId = Guid.Empty;
        }

        public Applicant(Guid id)
        {
            applicantId = id;
            IDataProvider dp = DataProviderFactory.GetDataProvider();
            basicInfo = ApplicantBasicInfo.GetApplicantBasicInfoById(id);
            relatedInfo = ApplicantRelatedInfo.GetApplicantRelatedInfoById(id);
            eduBackground = ApplicantEduBackground.GetApplicantEduBackgroundById(id);

            if (basicInfo == null) 
            {
                basicInfo = new ApplicantBasicInfo();
            }

            if(relatedInfo == null)
            {
                relatedInfo = new ApplicantRelatedInfo();
            }

            if(eduBackground == null)
            {
                eduBackground = new ApplicantEduBackground();
                //throw new Exception("Incomplete application found:" + id.ToString());
            }
        }
        #endregion

        #region Method
        public void Insert()
        {
            basicInfo.Insert();
            relatedInfo.Insert();
            eduBackground.Insert();
        }

        public void Update()
        {
            basicInfo.Update();
            relatedInfo.Update();
            eduBackground.Update();
        }

        //public void Delete()
        //{
        //    IDataProvider dp = DataProviderFactory.GetDataProvider();
        //    dp.DeleteApplicant(this);
        //}
        #endregion

        #region Static Helper Methods
        public static void DeleteApplcantById(Guid applicantId)
        {
            #region previous code
            //ApplicantBasicInfo.DeleteApplicantBasicInfoById(applicantId);
            //ApplicantRelatedInfo.DeleteApplicantRelatedInfoById(applicantId);
            //ApplicantEduBackground.DeleteApplicantEduBackgroundById(applicantId);
            ////IDataProvider dp = DataProviderFactory.GetDataProvider();
            ////string fullName = dp.GetFullNameByUserId(applicantId);
            ////Membership.DeleteUser(fullName, true);
            IDataProvider dp = DataProviderFactory.GetDataProvider();
            dp.DeleteApplcantById(applicantId);
            #endregion 
        }

        public static DataSet GetAllApplicants()
        {
            IDataProvider dp = DataProviderFactory.GetDataProvider();
            DataSet allApplicants = dp.GetAllApplicants();
            return allApplicants;
        }

        /// <summary>
        /// Author: Yin.P
        /// Date: 2010-1-7
        /// </summary>
        /// <returns></returns>
        public static DataSet GetAllApplicantsWithoutPermissionFilter()
        {
            IDataProvider dp = DataProviderFactory.GetDataProvider();
            DataSet allApplicants = dp.GetAllApplicantsWithoutPermissionFilter();
            return allApplicants;
        }

        /// <summary>
        /// Author: Yin.P
        /// Date: 2010-1-8
        /// </summary>
        /// <param name="IsFeedbackCompleted"></param>
        /// <param name="mentorAlias"></param>
        /// <param name="interviewStatus"></param>
        /// <returns></returns>
        public static DataSet GetApplicantsByCondition(/*int IsFeedbackCompleted, string mentorAlias, int interviewStatus*/)
        {
            IDataProvider dp = DataProviderFactory.GetDataProvider();
            return dp.GetApplicantsByCondition(/*IsFeedbackCompleted, mentorAlias, interviewStatus*/);
        }

        public static DataSet GetApplicantsByDate(DateTime dtStart, DateTime dtEnd)
        {
            IDataProvider dp = DataProviderFactory.GetDataProvider();
            DataSet dsApplicants = dp.GetApplicantsByApplicationDate(dtStart, dtEnd);
            return dsApplicants;
        }

        public static DataSet GetAllInterviewApplicants()
        {
            //string cacheKey = "all_interview_applicants";
            //if (SiteCache.Get(cacheKey) == null)
            //{ 
            //    IDataProvider dp = DataProviderFactory.GetDataProvider();
            //    DataSet allInterviewApplicants = dp.GetAllInterviewApplicants();
            //    SiteCache.Insert(cacheKey, allInterviewApplicants, SiteCache.DefaultExpiration);
            //}
            //return SiteCache.Get(cacheKey) as DataSet;
            IDataProvider dp = DataProviderFactory.GetDataProvider();
            DataSet allInterviewApplicants = dp.GetAllInterviewApplicants();
            return allInterviewApplicants;
        }

        /// <summary>
        /// Get all applicants with their checkin form by their interview status
        /// Added by Yi Shao at 2009-6-10
        /// </summary>
        /// <param name="status">interview status</param>
        /// <returns></returns>
        public static DataSet GetApplicantsByStatus(InterviewStatusEnum status)
        {
            IDataProvider dp = DataProviderFactory.GetDataProvider();
            DataSet Applicants = dp.GetApplicantsByStatus(status);
            return Applicants;
        }
        /// <summary>
        /// Overload  GetApplicantsByStatus(InterviewStatusEnum status) function, get all 
        /// applicants whose status is "interview in proccess" with their checkin form
        /// Added by Yi Shao at 2009-6-10
        /// </summary>
        /// <returns></returns>
        public static DataSet GetApplicantsByStatus()
        {
            IDataProvider dp = DataProviderFactory.GetDataProvider();
            DataSet Applicants = dp.GetApplicantsByStatus();
            return Applicants;
        }

        /// <summary>
        /// Get all candidate whose status is "hired" with their checkin form 
        /// Added bu Yi Shao at 2009-6-14
        /// </summary>
        /// <returns></returns>
        public static DataSet GetHiredApplicants()
        {
            IDataProvider dp = DataProviderFactory.GetDataProvider();
            DataSet Applicants = dp.GetHiredApplicants();
            return Applicants;
        }


        public static DataSet GetApplicantsByResume(String condition)
        {
            IDataProvider dp = DataProviderFactory.GetDataProvider();
            DataSet dsApplicants = dp.GetApplicantsByResume(condition);
            return dsApplicants;
        }
        public static bool IsIdNumUsed(string idNum)
        {
            IDataProvider dp = DataProviderFactory.GetDataProvider();
            return dp.IsIdNumUsed(idNum);
        }

        public static DataSet GetAllDecidedApplicants(Int16 InterviewStatus)
        {
            IDataProvider dp = DataProviderFactory.GetDataProvider();
            return dp.GetDecidedApplicants(InterviewStatus);
        }

        public static bool CheckComplete(Guid id)
        {
            ApplicantBasicInfo abi = ApplicantBasicInfo.GetApplicantBasicInfoById(id);
            ApplicantEduBackground aeb = ApplicantEduBackground.GetApplicantEduBackgroundById(id);
            ApplicantRelatedInfo ari = ApplicantRelatedInfo.GetApplicantRelatedInfoById(id);

            if (abi == null || aeb == null || ari == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Get all candidats by check-in date 
        /// Added by Yuanqin at 2011-4-25
        /// </summary>
        /// <returns></returns>
        public static DataSet GetApplicantsByCheckInDate(DateTime dtStart, DateTime dtEnd)
        {
            IDataProvider dp = DataProviderFactory.GetDataProvider();
            DataSet dsApplicants = dp.GetApplicantsByCheckInDate(dtStart, dtEnd);
            return dsApplicants;
        }
        #endregion

        #region Properties
        public Guid ApplicantId
        {
            get { return applicantId; }
            set { applicantId = value; }
        }

        public ApplicantBasicInfo BasicInfo
        {
            get { return basicInfo; }
            set { basicInfo = value; }
        }

        public ApplicantEduBackground EduBackground
        {
            get { return eduBackground; }
            set { eduBackground = value; }
        }

        public ApplicantRelatedInfo RelatedInfo
        {
            get { return relatedInfo; }
            set { relatedInfo = value; }
        }
        #endregion

    } 
}