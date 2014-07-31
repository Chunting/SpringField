/*****************************************************************************

Microsoft Research Asia
Copyright (c) 2006 Microsoft Corporation

Module Name: 
		Feedback.cs

Author:
        Yuan Chen (f-yuchen@msrchina.research.microsoft.com)  
        MSRA/MS^2.3/Compass Team
 
Abstract:
        Class used to store feedback information for Applicants
 
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
using System.Data;
using MSRA.SpringField.Components.Enumerations;
using MSRA.SpringField.Components.Configuration;

namespace MSRA.SpringField.Components.BizObjects
{
    public class Feedback
    {
        #region Private Members
        private int feedbackId;
        private Guid interviewerId;
        private Guid applicantId;
        private int interviewId;
        private FeedbackSuggestionEnum suggestion;
        private string feedbackContent;
        private DateTime interviewDate;
        private bool isComplete;
        private DateTime dueDate;
        private string interviewerAlias;
        #endregion

        #region Constructors
        public Feedback()
        {
            feedbackId = 0;
            interviewerId = Guid.Empty;
            applicantId = Guid.Empty;
            interviewId = 0;
            feedbackContent = string.Empty;
            interviewDate = DateTime.MaxValue;
            isComplete = false;
            dueDate = DateTime.MaxValue;
            interviewerAlias = string.Empty;
        }

        //public Feedback(int id)
        //{

        //} 
        #endregion

        #region Methods
        public void Insert()
        {
            IDataProvider dp = DataProviderFactory.GetDataProvider();
            feedbackId = dp.InsertFeedback(this);
        }

        public void Update()
        {
            IDataProvider dp = DataProviderFactory.GetDataProvider();
            dp.UpdateFeedback(this);
        } 
        #endregion

        #region Static Helper Methods
        public static void DeleteFeedbackById(int id)
        {
            IDataProvider dp = DataProviderFactory.GetDataProvider();
            dp.DeleteFeedback(id);
        }

        public static Feedback GetFeedbackById(int id)
        {
            IDataProvider dp = DataProviderFactory.GetDataProvider();
            return dp.GetFeedbackById(id);
        }

        public static DataSet GetFeedbackByInterview(int interviewId)
        {
            IDataProvider dp = DataProviderFactory.GetDataProvider();
            return dp.GetFeedbackByInterview(interviewId, true);
        }

        public static DataSet GetIncompleteFeedbackByInterview(int interviewId)
        {
            //string cacheKey = "incomplete_feedbacks_" + interviewId.ToString();
            //if (SiteCache.Get(cacheKey) == null)
            //{
            //    IDataProvider dp = DataProviderFactory.GetDataProvider();
            //    //special: we don't cache this object
            //    SiteCache.Insert(cacheKey, dp.GetFeedbackByInterview(interviewId, false), new TimeSpan(0, 0, 1));
            //}
            //return SiteCache.Get(cacheKey) as DataSet;
            IDataProvider dp = DataProviderFactory.GetDataProvider();
            //special: we don't cache this object
            DataSet ds = dp.GetFeedbackByInterview(interviewId, false);
            return ds;
        }

        public static DataSet GetIncompleteFeedbackByAlias(string alias)
        {
            IDataProvider dp = DataProviderFactory.GetDataProvider();
            return dp.GetIncompleteFeedbackByAlias(alias.Trim());
        }

        public static Boolean CheckInterviewSameAsMentor(Int32 interviewId, String mentorAlias)
        {
            IDataProvider dp = DataProviderFactory.GetDataProvider();
            return dp.CheckInterviewSameAsMentor(interviewId, mentorAlias);
        }
        #endregion

        #region Properties
        public int FeedBackId
        {
            get { return feedbackId; }
            set { feedbackId = value; }
        }

        public Guid InterviewerId
        {
            get { return interviewerId; }
            set { interviewerId = value; }
        }

        public Guid ApplicantId
        {
            get { return applicantId; }
            set { applicantId = value; }
        }

        public int InterviewId
        {
            get { return interviewId; }
            set { interviewId = value; }
        }

        public FeedbackSuggestionEnum Suggestion
        {
            get { return suggestion; }
            set { suggestion = value; }
        }

        public string FeedbackContent
        {
            get { return feedbackContent; }
            set { feedbackContent = ((value == null) ? string.Empty : value); }
        }

        public DateTime InterviewDate
        {
            get { return interviewDate; }
            set { interviewDate = value; }
        }

        public bool IsComplete
        {
            get { return isComplete; }
            set { isComplete = value; }
        }

        public DateTime DueDate
        {
            get { return dueDate; }
            set { dueDate = value; }
        }

        public string InterviewerAlias
        {
            get { return interviewerAlias; }
            set { interviewerAlias = ((value == null) ? string.Empty : value); }
        }
        #endregion
    }
}
