/*****************************************************************************

Microsoft Research Asia
Copyright (c) 2006 Microsoft Corporation

Module Name: 
		SiteGeneralInfo.cs

Author:
        Yuan Chen (f-yuchen@msrchina.research.microsoft.com)  
        MSRA/MS^2.3/Compass Team
 
Abstract:
        Class used to store Site General information

Remarks:
        This class should be used out of the Springfield.Components assembly, so it should be declared as an public class.
             
Environment:
		Class Library/.NET Framework 2.0 

Project:
        Springfield.Components for MSRA Intern Application Tracking System (MIATS), codename: Springfield;
        MSRA/MS^2.3/Compass Team

Revision History:			
		23/May/2006 Created by Yuan Chen

*****************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using MSRA.SpringField.Components.Configuration;

namespace MSRA.SpringField.Components.BizObjects
{
    public class SiteGeneralInfo
    {
        private int applicationCount;
        private int userFeedbackCount;
        private int userProcessingCount;
        private int irDecisionCount;
        private int completeFeedbackCount;
        private int incompleteFeedbackCount;
        private int completeInterviewCount;
        private int hiredInterviewCount;
        private int rejectedInterviewCount;

        public SiteGeneralInfo()
        {
            applicationCount = 0;
            userFeedbackCount = 0;
            userProcessingCount = 0;
            irDecisionCount = 0;
        }

        public static SiteGeneralInfo GetGeneralInfo()
        {
            IDataProvider dp = DataProviderFactory.GetDataProvider();
            return dp.GetSiteGeneralInfo();
        }

        public int ApplicationCount
        {
            get { return applicationCount; }
            set { applicationCount = value; }
        }

        public int UserFeedbackCount
        {
            get { return userFeedbackCount; }
            set { userFeedbackCount = value; }
        }

        public int UserProcssingCount
        {
            get { return userProcessingCount; }
            set { userProcessingCount = value; }
        }

        public int IRDecisionCount
        {
            get { return irDecisionCount; }
            set { irDecisionCount = value; }
        }

        public int IncompleteFeedbackCount
        {
            get { return incompleteFeedbackCount; }
            set { incompleteFeedbackCount = value; }
        }

        public int CompleteFeedbackCount
        {
            get { return completeFeedbackCount; }
            set { completeFeedbackCount = value; }
        }

        public int HiredInterviewCount
        {
            get { return hiredInterviewCount; }
            set { hiredInterviewCount = value; }
        }

        public int RejectedInterviewCount
        {
            get { return rejectedInterviewCount; }
            set { rejectedInterviewCount = value; }
        }

        public int CompleteInterviewCount
        {
            get { return completeInterviewCount; }
            set { completeInterviewCount = value; }
        }
    }
}
