/*****************************************************************************
 * entity class for data table Performance Assessment
 * added by Yi Shao at 2009-06-19
*****************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using MSRA.SpringField.Components.Configuration;

namespace MSRA.SpringField.Components.BizObjects
{

    /// <summary>
    /// This object represents the properties and methods of a PerformanceAssessment.
    /// </summary>
    public class PerformanceAssessment
    {
        #region private Properties
        protected Guid _id;
        protected Guid _applicantId = Guid.Empty;
        protected string _internName = String.Empty;
        protected string _internPhone = String.Empty;
        protected string _internEmail = String.Empty;
        protected int _internPosition;
        protected DateTime _checkInDate = Convert.ToDateTime("12/29/9999");
        protected DateTime _checkOutDate = Convert.ToDateTime("12/30/9999");
        protected int _groupId;
        protected int _department;
        protected string _mentorName = String.Empty;
        protected string _mentorAlias = String.Empty;
        protected Guid _groupMgrId = Guid.Empty;
        protected int _projectId;
        protected DateTime _graduationDate = Convert.ToDateTime("12/30/9999");
        protected int _discipline;
        protected string _pipeline = String.Empty;
        protected string _objective = String.Empty;
        protected string _selfEvaluation = String.Empty;
        protected string _strengthsAndWeaknesses = String.Empty;
        protected int _overrallEvaluation;
        protected int _codingSkill;
        protected int _analyticalSkill;
        protected int _problemSolving;
        protected int _innovation;
        protected int _drivingForResults;
        protected int _dealingWithAmbiguity;
        protected int _quickOnLearing;
        protected int _english;
        protected int _communication;
        protected int _teamWork;
        protected int _attitude;
        protected int _integrityHonesty;
        protected int _openRespectful;
        protected int _bigChallenges;
        protected int _passion;
        protected int _accountable;
        protected int _selfCritical;
        protected string _mentorComments = String.Empty;
        protected string _mentorStrength = String.Empty;
        protected string _mentorWeakness = String.Empty;
        protected int _hiredAsFTE;
        protected int _onsiteInterviewNow;
        protected DateTime _expectedOnsiteInterviewDate = Convert.ToDateTime("12/30/9999");
        protected decimal _extendPeriod;
        private System.DateTime _insertDate = Convert.ToDateTime("12/30/9999");
        private System.DateTime _modifyDate = Convert.ToDateTime("12/30/9999");
        #endregion

        #region Construtor
        public PerformanceAssessment()
        {
        }
        #endregion
       
        #region Public Properties
        public Guid Id
        {
            get { return _id; }
            set { _id = value; }
        }

        /// <summary>
        /// PA Approval
        /// Author: Yin.P
        /// Date: 2010-1-8
        /// </summary>
        public int IsApproved
        {
            get;
            set;
        }

        public Guid ApplicantId
        {
            get { return _applicantId; }
            set { _applicantId = value; }
        }

        public string InternName
        {
            get { return _internName; }
            set { _internName = value; }
        }

        public string InternPhone
        {
            get { return _internPhone; }
            set { _internPhone = value; }
        }

        public string InternEmail
        {
            get { return _internEmail; }
            set { _internEmail = value; }
        }

        public int InternPosition
        {
            get { return _internPosition; }
            set { _internPosition = value; }
        }

        public DateTime CheckInDate
        {
            get { return _checkInDate; }
            set { _checkInDate = value; }
        }

        public DateTime CheckOutDate
        {
            get { return _checkOutDate; }
            set { _checkOutDate = value; }
        }

        public int GroupId
        {
            get { return _groupId; }
            set { _groupId = value; }
        }

        public int Department
        {
            get { return _department; }
            set { _department = value; }
        } 
        
        public string MentorName
        {
            get { return _mentorName; }
            set { _mentorName = value; }
        }

        public string MentorAlias
        {
            get { return _mentorAlias; }
            set { _mentorAlias = value; }
        }

        public Guid GroupMgrId
        {
            get { return _groupMgrId; }
            set { _groupMgrId = value; }
        }

        public int ProjectId
        {
            get { return _projectId; }
            set { _projectId = value; }
        }

        public DateTime GraduationDate
        {
            get { return _graduationDate; }
            set { _graduationDate = value; }
        }  
      
        public int Discipline
        {
            get { return _discipline; }
            set { _discipline = value; }
        }

        public string Pipeline
        {
            get { return _pipeline; }
            set { _pipeline = value; }
        }

        public string Objective
        {
            get { return _objective;
            }
            set { _objective = value; }
        }

        public string SelfEvaluation
        {
            get { return _selfEvaluation; }
            set { _selfEvaluation = value; }
        }

        public string StrengthsAndWeaknesses
        {
            get { return _strengthsAndWeaknesses; }
            set { _strengthsAndWeaknesses = value; }
        }

        public int OverrallEvaluation
        {
            get { return _overrallEvaluation; }
            set { _overrallEvaluation = value; }
        }

        public int CodingSkill
        {
            get { return _codingSkill; }
            set { _codingSkill = value; }
        }

        public int AnalyticalSkill
        {
            get { return _analyticalSkill; }
            set { _analyticalSkill = value; }
        }

        public int ProblemSolving
        {
            get { return _problemSolving; }
            set { _problemSolving = value; }
        }

        public int Innovation
        {
            get { return _innovation; }
            set { _innovation = value; }
        }

        public int DrivingForResults
        {
            get { return _drivingForResults; }
            set { _drivingForResults = value; }
        }

        public int DealingWithAmbiguity
        {
            get { return _dealingWithAmbiguity; }
            set { _dealingWithAmbiguity = value; }
        }

        public int QuickOnLearing
        {
            get { return _quickOnLearing; }
            set { _quickOnLearing = value; }
        }

        public int English
        {
            get { return _english; }
            set { _english = value; }
        }

        public int Communication
        {
            get { return _communication; }
            set { _communication = value; }
        }

        public int TeamWork
        {
            get { return _teamWork; }
            set { _teamWork = value; }
        }

        public int Attitude
        {
            get { return _attitude; }
            set { _attitude = value; }
        }

        public int IntegrityHonesty
        {
            get { return _integrityHonesty; }
            set { _integrityHonesty = value; }
        }

        public int OpenRespectful
        {
            get { return _openRespectful; }
            set { _openRespectful = value; }
        }

        public int BigChallenges
        {
            get { return _bigChallenges; }
            set { _bigChallenges = value; }
        }

        public int Passion
        {
            get { return _passion; }
            set { _passion = value; }
        }

        public int Accountable
        {
            get { return _accountable; }
            set { _accountable = value; }
        }

        public int SelfCritical
        {
            get { return _selfCritical; }
            set { _selfCritical = value; }
        }

        public string MentorComments
        {
            get { return _mentorComments; }
            set { _mentorComments = value; }
        }

        public string MentorStrength
        {
            get { return _mentorStrength; }
            set { _mentorStrength = value; }
        }

        public string MentorWeakness
        {
            get { return _mentorWeakness; }
            set { _mentorWeakness = value; }
        }

        public int HiredAsFTE
        {
            get { return _hiredAsFTE; }
            set { _hiredAsFTE = value; }
        }

        public int OnsiteInterviewNow
        {
            get { return _onsiteInterviewNow; }
            set { _onsiteInterviewNow = value; }
        }

        public DateTime ExpectedOnsiteInterviewDate
        {
            get { return _expectedOnsiteInterviewDate; }
            set { _expectedOnsiteInterviewDate = value; }
        }

        public decimal ExtendPeriod
        {
            get { return _extendPeriod; }
            set { _extendPeriod = value; }
        }
        public System.DateTime InsertDate
        {
            get { return _insertDate; }
            set { _insertDate = value; }
        }
        public System.DateTime ModifyDate
        {
            get { return _modifyDate; }
            set { _modifyDate = value; }
        }
        #endregion

        #region Public method
        public bool Update()
        {
            IDataProvider dp = DataProviderFactory.GetDataProvider();
            return dp.UpdatePerformanceAssessment(this);
        }
        public Guid Insert()
        {
            IDataProvider dp = DataProviderFactory.GetDataProvider();
            return dp.InsertPerformanceAssessment(this);
        }
        #endregion

        #region public static method
        public static PerformanceAssessment GetPerformanceAssessmentById(Guid id)
        {
            IDataProvider dp = DataProviderFactory.GetDataProvider();
            return dp.GetPerformanceAssessmentById(id);
        }
        public static DataSet GetPABasicInfoByApplicantId(Guid ApplicantId)
        {
            IDataProvider dp = DataProviderFactory.GetDataProvider();
            return dp.GetPABasicInfoByApplicantId(ApplicantId);
        }
        public static DataSet GetPerformanceAssessment()
        {
            IDataProvider dp = DataProviderFactory.GetDataProvider();
            return dp.GetPerformanceAssessment();
        }
        public static DataSet GetPerformanceAssessmentByApplicantId(Guid ApplicantId)
        {           
            IDataProvider dp = DataProviderFactory.GetDataProvider();
            return dp.GetPerformanceAssessmentByApplicantId(ApplicantId);
        }
        public static bool DeletePerformanceAssessmentbyId(Guid PAId)
        {
            IDataProvider dp = DataProviderFactory.GetDataProvider();
            return dp.DeletePerformanceAssessmentById(PAId);
        }
        public static DataSet GetPAReportForHR()
        {
            IDataProvider dp = DataProviderFactory.GetDataProvider();
            return dp.GetPAReportForHR();
        }
        public static DataSet GetPerformanceAssessmentByModifyTime(DateTime StartTime, DateTime EndTime)
        {
            IDataProvider dp = DataProviderFactory.GetDataProvider();
            return dp.GetPerformanceAssessmentByModifyTime(StartTime,EndTime);
        }
        public static DataSet GetUncompletedPAbyApplicantId(Guid ApplicantId, DateTime BeginTime)
        {
            IDataProvider dp = DataProviderFactory.GetDataProvider();
            return dp.GetUncompletedPAbyApplicantId(ApplicantId, BeginTime);
        }
        #endregion
    }

}
