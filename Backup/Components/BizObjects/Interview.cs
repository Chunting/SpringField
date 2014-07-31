/*****************************************************************************

Microsoft Research Asia
Copyright (c) 2006 Microsoft Corporation

Module Name: 
		Interview.cs

Author:
        Yuan Chen (f-yuchen@msrchina.research.microsoft.com)  
        MSRA/MS^2.3/Compass Team
 
Abstract:
        Class used to store Interview information for Applicants, Group Manager and Hiring Manager
 
Remarks:
        This class should be used out of the Springfield.Components assembly, so it should be declared as an public class.
             
Environment:
		Class Library/.NET Framework 2.0 

Project:
        Springfield.Components for MSRA Intern Application Tracking System (MIATS), codename: Springfield;
        MSRA/MS^2.3/Compass Team

Revision History:			
		30/Apr/2006 Created by Yuan Chen
        4/June/2009 Modified by Yi Shao
            Add GMApproval GMApprovalExt MentorApproval and MentorApprovalExt

*****************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using MSRA.SpringField.Components.Enumerations;
using MSRA.SpringField.Components.Configuration;

namespace MSRA.SpringField.Components.BizObjects
{
    [Serializable]
    public class Interview
    {
        #region Private Members
        private int interviewId;
        private Guid applicantId;
        private DateTime startDate;
        private InterviewStatusEnum interviewStatus;
        private DateTime endDate;
        private Guid hiringManagerId;
        private bool hiringManagerResult;
        private string hiringManagerComment;
        private Guid groupManagerId;
        private bool groupManagerResult;
        private string groupManagerComment;
        private int checkInFormId;

        //CR:need to see time of making the decision.
        //Hui Yang,2007-3-15
        private DateTime mentorDecisionTime = new DateTime(2000, 1, 1, 0, 0, 0);

        
        //Yi Shao, 2009-06-04
        private Document gmApproval;//Group Manager's Approval Email
        private string gmApprovalExt;//file extension name
        private Document mentorApproval;//Mentor's Approval Email ( Contain group manager's approval)
        private string mentorApprovalExt;

        /// <summary>
        /// Mentor Alias
        /// Author: Yin.P
        /// Date: 2010-1-5
        /// </summary>
        public string MentorAlias { get; set; }

        public Document GMApproval
        {
            get
            {
                return gmApproval;
            }
            set { gmApproval = value; }
        }
        public string GMApprovalExt
        {
            get
            {
                return gmApprovalExt;
            }
            set { gmApprovalExt = value; }
        }
        public Document MentorApproval
        {
            get
            {
                return mentorApproval;
            }
            set { mentorApproval = value; }
        }
        public string MentorApprovalExt
        {
            get
            {
                return mentorApprovalExt;
            }
            set { mentorApprovalExt = value; }
        }

        public DateTime MentorDecisionTime
        {
            get { 
                return mentorDecisionTime; 
            }
            set { mentorDecisionTime = value; }
        }
        private DateTime managerDecisionTime = new DateTime(2000, 1, 1, 0, 0, 0);
        public DateTime ManagerDecisionTime
        {
            get { return managerDecisionTime; }
            set { managerDecisionTime = value; }
        }
        #endregion

        #region Constructors
        public Interview()
        {
            interviewId = 0;
            applicantId = Guid.Empty;
            startDate = DateTime.MaxValue;
            endDate = DateTime.MaxValue;
            hiringManagerId = Guid.Empty;
            hiringManagerResult = false;
            hiringManagerComment = string.Empty;
            groupManagerId = Guid.Empty;
            groupManagerResult = false;
            groupManagerComment = string.Empty;
            gmApproval = new Document();
            gmApprovalExt = String.Empty;
            mentorApproval = new Document();
            MentorApprovalExt = String.Empty;
        }

        //public Interview(int id)
        //{

        //} 
        #endregion

        #region Methods
        public Int32 Insert()
        {
            IDataProvider dp = DataProviderFactory.GetDataProvider();
            interviewId = dp.InsertInterview(this);
            return interviewId;
        }

        public void Update()
        {
            IDataProvider dp = DataProviderFactory.GetDataProvider();
            dp.UpdateInterview(this);
        }
        #endregion

        #region Static Helper Methods
        public static DataSet GetAllProcessingInterview()
        {
            //string cacheKey = "all_processing_interview";
            //if (SiteCache.Get(cacheKey) == null)
            //{
            //    IDataProvider dp = DataProviderFactory.GetDataProvider();
            //    SiteCache.Insert(cacheKey, dp.GetAllProcessingInterview(), SiteCache.DefaultExpiration);
            //}
            //return SiteCache.Get(cacheKey) as DataSet;

            IDataProvider dp = DataProviderFactory.GetDataProvider();
            return dp.GetAllProcessingInterview();
        }

        public static void DeleteIncompleteFeedbackForInterview(int interviewId)
        {
            IDataProvider dp = DataProviderFactory.GetDataProvider();
            dp.DeleteIncompleteFeedbackForInterview(interviewId);
        }

        public static void DeleteInterviewById(int id)
        {
            IDataProvider dp = DataProviderFactory.GetDataProvider();
            dp.DeleteInterview(id);
        }

        public static Interview GetInterviewById(int id)
        {
            ////string cacheKey = "interview_" + id.ToString();
            ////if (SiteCache.Get(cacheKey) == null)
            //{
            //    IDataProvider dp = DataProviderFactory.GetDataProvider();
            //    Interview myInterview = dp.GetInterviewById(id);
            //    if (myInterview != null)
            //    {
            //        SiteCache.Insert(cacheKey, myInterview, SiteCache.DefaultExpiration);
            //    }
            //    else
            //    {
            //        return null;
            //    }
            //}
            //return SiteCache.Get(cacheKey) as Interview;
            IDataProvider dp = DataProviderFactory.GetDataProvider();
            Interview myInterview = dp.GetInterviewById(id);
            return myInterview;
        }

        public static DataSet GetInterviewForApplicant(Guid applicantId)
        {
            IDataProvider dp = DataProviderFactory.GetDataProvider();
            return dp.GetInterviewForApplicant(applicantId);
        }

        public static string GetRecentInterviewIdByApplicant(Guid applicantId)
        {
            IDataProvider dp = DataProviderFactory.GetDataProvider();
            return dp.GetRecentInterviewIdByApplicant(applicantId);
        }

        public static string GetInterviewIdByApplicant(Guid applicantId)
        {
            IDataProvider dp = DataProviderFactory.GetDataProvider();
            return dp.GetInterviewIdByApplicant(applicantId);
        }

        public static string GetRecentInterviewStatus(Guid applicantId)
        {
            IDataProvider dp = DataProviderFactory.GetDataProvider();
            string result = dp.GetRecentInterviewStatus(applicantId);
            return result;
        }

        public static string GetRecentInterviewDate(Guid applicantId)
        {
            //string cacheKey = "interview_date_" + applicantId.ToString();
            //if (SiteCache.Get(cacheKey) == null)
            //{
            //    IDataProvider dp = DataProviderFactory.GetDataProvider();
            //    SiteCache.Insert(cacheKey, dp.GetRecentInterviewDateTime(applicantId), SiteCache.DefaultExpiration);
            //}
            //return SiteCache.Get(cacheKey) as string;
            IDataProvider dp = DataProviderFactory.GetDataProvider();
            string result = dp.GetRecentInterviewDateTime(applicantId);
            return result;
        }

        public static Interview GetCurrentInterview(Guid applicantId, Guid hiringManagerId)
        {
            //string cacheKey = "current_interview_" + applicantId.ToString() + hiringManagerId.ToString();
            //if (SiteCache.Get(cacheKey) == null)
            //{
            //    IDataProvider dp = DataProviderFactory.GetDataProvider();
            //    Interview myInterview = dp.GetCurrentInterview(applicantId, hiringManagerId);
            //    if (myInterview != null)
            //    {
            //        SiteCache.Insert(cacheKey, myInterview, SiteCache.DefaultExpiration);
            //    }
            //    else
            //    {
            //        return null;
            //    }
            //}
            //return SiteCache.Get(cacheKey) as Interview;
            IDataProvider dp = DataProviderFactory.GetDataProvider();
            Interview myInterview = dp.GetCurrentInterview(applicantId, hiringManagerId);
            return myInterview;
        }

        //only incomplete status
        public static DataSet GetInterviewForCurrentUser(Guid hiringManagerId, String alias)
        {
            //string cacheKey = "current_user_interview_" + hiringManagerId.ToString();
            //if (SiteCache.Get(cacheKey) == null)
            //{
            //    IDataProvider dp = DataProviderFactory.GetDataProvider();
            //    SiteCache.Insert(cacheKey, dp.GetInterviewForCurrentUser(hiringManagerId), SiteCache.DefaultExpiration);
            //}
            //return SiteCache.Get(cacheKey) as DataSet;

            IDataProvider dp = DataProviderFactory.GetDataProvider();
            return dp.GetInterviewForCurrentUser(hiringManagerId, alias);
        }

        //all interview status, for report usage
        public static DataSet GetInterviewForSiteUser(Guid hiringManagerId)
        {
            //string cacheKey = "siteuser_interview_" + hiringManagerId.ToString();
            //if (SiteCache.Get(cacheKey) == null)
            //{
            //    IDataProvider dp = DataProviderFactory.GetDataProvider();
            //    SiteCache.Insert(cacheKey, dp.GetInterviewForSiteUser(hiringManagerId), SiteCache.DefaultExpiration);
            //}
            //return SiteCache.Get(cacheKey) as DataSet;
            IDataProvider dp = DataProviderFactory.GetDataProvider();
            DataSet ds = dp.GetInterviewForSiteUser(hiringManagerId);
            return ds;
        }

        public static DataSet GetDurationInterviewedSiteUser(DateTime startDate, DateTime endDate)
        {
            //string cacheKey = "intervied_siteuser_" + startDate.ToString("yyyyMMdd") + "_" + endDate.ToString("yyyyMMdd");
            ////if (SiteCache.Get(cacheKey) == null)
            ////{
            //    IDataProvider dp = DataProviderFactory.GetDataProvider();
            //    SiteCache.Insert(cacheKey, dp.GetDurationInterviewedSiteUser(startDate, endDate), SiteCache.DefaultExpiration);
            ////}
            //return SiteCache.Get(cacheKey) as DataSet;
            IDataProvider dp = DataProviderFactory.GetDataProvider();
            return dp.GetDurationInterviewedSiteUser(startDate, endDate);
        }

        public static DataSet GetDurationInterview(DateTime startDate, DateTime endDate)
        {
            //string cacheKey = "interview_during_" + startDate.ToString("yyyyMMdd") + "_" + endDate.ToString("yyyyMMdd");
            //if (SiteCache.Get(cacheKey) == null)
            //{
            //    IDataProvider dp = DataProviderFactory.GetDataProvider();
            //    SiteCache.Insert(cacheKey, dp.GetDurationInterview(startDate, endDate),SiteCache.DefaultExpiration);
            //}
            //return SiteCache.Get(cacheKey) as DataSet;
            IDataProvider dp = DataProviderFactory.GetDataProvider();
            return dp.GetDurationInterview(startDate, endDate);
        }
        public static Boolean UpdateDecisionMailStatus(Int32 interviewId)
        {
            IDataProvider dp = DataProviderFactory.GetDataProvider();
            return dp.UpdateDecisionMailStatus(interviewId);
        }
        #endregion

        #region Properties
        public int InterviewId
        {
            get { return interviewId; }
            set { interviewId = value; }
        }

        public Guid ApplicantId
        {
            get { return applicantId; }
            set { applicantId = value; }
        }

        public DateTime StartDate
        {
            get { return startDate; }
            set { startDate = value; }
        }

        public InterviewStatusEnum InterviewStatus
        {
            get { return interviewStatus; }
            set { interviewStatus = value; }
        }

        public DateTime EndDate
        {
            get { return endDate; }
            set { endDate = value; }
        }

        public Guid HiringManagerId
        {
            get { return hiringManagerId; }
            set { hiringManagerId = value; }
        }

        public bool HiringManagerResult
        {
            get { return hiringManagerResult; }
            set { hiringManagerResult = value; }
        }

        public string HiringManagerComment
        {
            get { return hiringManagerComment; }
            set { hiringManagerComment = ((value == null) ? string.Empty : value); }
        }

        public Guid GroupManagerId
        {
            get { return groupManagerId; }
            set { groupManagerId = value; }
        }

        public bool GroupManagerResult
        {
            get { return groupManagerResult; }
            set { groupManagerResult = value; }
        }

        public string GroupManagerComment
        {
            get { return groupManagerComment; }
            set { groupManagerComment = ((value == null) ? string.Empty : value); }
        }

        public int CheckInFormId
        {
            get { return checkInFormId; }
            set { checkInFormId = value; }
        }
        #endregion
    }
}
