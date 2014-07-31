using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Security;
using MSRA.SpringField.Components.Enumerations;

namespace MSRA.SpringField.Components
{
    public static class StaticData
    {
        private static Dictionary<MajorCategoryEnum, string> majorDict;
        private static Dictionary<ApplicationStatusEnum, string> appStatusDict;
        private static Dictionary<InterviewStatusEnum, string> interviewStatusDict;

        public static readonly string OTHER_COLLEGE = "Other Univ., please specify";

        static StaticData()
        {
            InitRoles();

            InitMajorData();

            InitAppStatusData();

            InitInterviewStatusData();
        }

        static void InitRoles()
        {
            CreateRole(RoleType.AnonymousUser.ToString());
            CreateRole(RoleType.Applicant.ToString());
            CreateRole(RoleType.DefaultUser.ToString());
            CreateRole(RoleType.Employee.ToString());
            CreateRole(RoleType.GroupManager.ToString());
            CreateRole(RoleType.HiringManager.ToString());
            CreateRole(RoleType.Intern.ToString());
            CreateRole(RoleType.InternRecruiter.ToString());
            CreateRole(RoleType.SuperAdministrator.ToString());
            CreateRole(RoleType.UnivRelation.ToString());
        }

        static void CreateRole(string roleName)
        {
            if (!Roles.RoleExists(roleName))
            {
                Roles.CreateRole(roleName);
            }
        }

        static void InitMajorData()
        {
            majorDict = new Dictionary<MajorCategoryEnum, string>();
            majorDict.Add(MajorCategoryEnum.AutomationRelated, "Automation Related");
            majorDict.Add(MajorCategoryEnum.ComputerEERelated, "Computer & EE Related");
            majorDict.Add(MajorCategoryEnum.MathRelated, "Math Related");
            majorDict.Add(MajorCategoryEnum.PhysicalRelated, "Physical Related");
            majorDict.Add(MajorCategoryEnum.LiberalRelated, "Liberal Related");
            majorDict.Add(MajorCategoryEnum.Other, "Other");
        }

        static void InitAppStatusData()
        {
            appStatusDict = new Dictionary<ApplicationStatusEnum, string>();
            appStatusDict.Add(ApplicationStatusEnum.Available, "Available");
            appStatusDict.Add(ApplicationStatusEnum.ApplicationIncomplete, "Application Started");
            appStatusDict.Add(ApplicationStatusEnum.Hired, "Hired");
            appStatusDict.Add(ApplicationStatusEnum.InterviewinProcess, "Interview in Process");
            appStatusDict.Add(ApplicationStatusEnum.KeyReferring, "Key Referring");
            appStatusDict.Add(ApplicationStatusEnum.OfferDeclined, "Offer Declined");
            appStatusDict.Add(ApplicationStatusEnum.Rejected, "Rejected");
            appStatusDict.Add(ApplicationStatusEnum.WaitforAdvisorsAppraisal, "Wait For Advisors Appraisal");
            appStatusDict.Add(ApplicationStatusEnum.OnBoard, "On Board");
            //add by yuanqin, 2011.5.5
            appStatusDict.Add(ApplicationStatusEnum.QualifiedButNotMatched, "Qualified But Not Matched");
        }

        static void InitInterviewStatusData()
        {
            interviewStatusDict = new Dictionary<InterviewStatusEnum, string>();
            interviewStatusDict.Add(InterviewStatusEnum.Hired, "Hired");
            interviewStatusDict.Add(InterviewStatusEnum.OfferDeclined, "Offer Declined");
            interviewStatusDict.Add(InterviewStatusEnum.Rejected, "Rejected");
            interviewStatusDict.Add(InterviewStatusEnum.WaitingForGroupManagerDecision, "Waiting For GroupManager Decision");
            interviewStatusDict.Add(InterviewStatusEnum.WaitingForInterviewFeedback, "Waiting For Interview Feedback");
            interviewStatusDict.Add(InterviewStatusEnum.WaitingForMentorDecision, "Waiting For Mentor Decision");
            //add by yuanqin, 2011.5.5
            InterviewStatusDict.Add(InterviewStatusEnum.QualifiedButNotMatched, "Qualified But Not Matched");
        }

        static List<string> GetList(string listXPATH)
        {
            List<string> rgList = SiteCache.Get(listXPATH) as List<string>;
            if (rgList == null)
            {
                ResourceManager.GetDataFromResource(listXPATH);
                return SiteCache.Get(listXPATH) as List<string>;
            }
            else
            {
                return rgList;
            }
        }

        static Dictionary<string, Boolean> GetFTEDict()
        {
            Dictionary<string, Boolean> FTEDict = SiteCache.Get(ResourceManager.MSRA_FTE_PATH) as Dictionary<string, Boolean>;
            if (FTEDict == null)
            {
                ResourceManager.GetDataFromResource(ResourceManager.MSRA_FTE_PATH);
                return SiteCache.Get(ResourceManager.MSRA_FTE_PATH) as Dictionary<string, Boolean>;
            }
            else
            {
                return FTEDict;
            }
        }

        #region Properties
        public static List<string> CollegeNameList
        {
            get 
            {
                return GetList(ResourceManager.UNIV_LIST_PATH);
            }
        }

        public static List<string> NationalityList
        {
            get 
            {
                return GetList(ResourceManager.COUNTRY_LIST_PATH);
            }
        }

        public static List<string> InfoCategory
        {
            get
            {
                return GetList(ResourceManager.CATEGORY_PATH);
            }
        }

        public static List<string> InfoSource
        {
            get
            {
                return GetList(ResourceManager.SOURCE_PATH);
            }
        }

        public static List<string> InfoChannel
        {
            get
            {
                return GetList(ResourceManager.CHANNEL_PATH);
            }
        }

        public static List<string> GroupList
        {
            get
            {
                return GetList(ResourceManager.GROUP_LIST_PATH);
            }
        }

        public static List<string> DegreeList
        {
            get
            {
                return GetList(ResourceManager.DEGREE_LIST_PATH);
            }
        }

        public static Dictionary<string, Boolean> FTEDict
        {
            get
            {
                return GetFTEDict();
            }
        }

        public static Dictionary<MajorCategoryEnum, string> MajorDict
        {
            get { return majorDict; }
        }

        public static Dictionary<ApplicationStatusEnum, string> AppStatusDict
        {
            get { return appStatusDict; }
        }

        public static Dictionary<InterviewStatusEnum, string> InterviewStatusDict
        {
            get { return interviewStatusDict; }
        }
        #endregion
       
    }
}
