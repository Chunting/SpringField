/*****************************************************************************
 * entity class for data table CheckoutSurvey
 * added by Yuanqin, 2011.5.30
*****************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using MSRA.SpringField.Components.Configuration;

namespace MSRA.Springfield.Components.BizObjects
{
    /// <summary>
    /// This object represents the properties and methods of a CheckoutSurvey.
    /// </summary>
    public class CheckoutSurvey
    {

        #region private Properties
        protected Guid _id;
        protected Guid _applicantId = Guid.Empty;
        protected int _interviewId;
        protected string _internName = String.Empty;
        protected int _groupId;
        protected int _internshipDuration;
        protected DateTime _checkInDate = Convert.ToDateTime("12/29/9999");
        protected DateTime _checkOutDate = Convert.ToDateTime("12/30/9999");
        protected int _overallView;
        protected string _overallComments = String.Empty;
        protected int _likeWork;
        protected int _background;
        protected int _workAmount;
        protected int _objects;
        protected int _developmentSkill;
        protected int _researchSkill;
        protected int _SDESkill;
        protected int _designSkill;
        protected int _projectSkill;
        protected int _communicationSkill;
        protected int _teamwork;
        protected string _workComments = String.Empty;
        protected int _mentorSetGoal;
        protected int _helpFromMentor;
        protected int _makeGoodUse;
        protected int _respect;
        protected string _mentorComments = String.Empty;
        protected int _trainingView;
        protected int _trainingSuitable;
        protected int _trainingEssential;
        protected int _activityInterest;
        protected string _trainingComments = String.Empty;
        protected int _balance;
        protected int _workEnvironment;
        protected int _compensation;
        protected int _satisfaction;
        protected int _onBoard;
        protected int _accommodation;
        protected int _salaryAndMeal;
        protected int _reimbursement;
        protected int _itSupport;
        protected int _dailySupport;
        protected string _lifeComments = String.Empty;
        protected int _leading;
        protected int _msCulture;
        protected int _returnAsIntern;
        protected int _joinMS;
        protected int _recommend;
        protected string _msraComments = String.Empty;
        protected string _comments = String.Empty;
        protected DateTime _insertDate = Convert.ToDateTime("12/29/9999");
        #endregion

        #region Construtor
        public CheckoutSurvey()
        {
        }
        #endregion

        #region Public Properties
        public Guid id
        {
            get { return _id; }
            set { _id = value; }
        }

        public Guid ApplicantId
        {
            get { return _applicantId; }
            set { _applicantId = value; }
        }

        public int InterviewId
        {
            get { return _interviewId; }
            set { _interviewId = value; }
        }

        public string InternName
        {
            get { return _internName; }
            set { _internName = value; }
        }

        public int GroupId
        {
            get { return _groupId; }
            set { _groupId = value; }
        }

        public int InternshipDuration
        {
            get { return _internshipDuration; }
            set { _internshipDuration = value; }
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

        public int OverallView
        {
            get { return _overallView; }
            set { _overallView = value; }
        }

        public string OverallComments
        {
            get { return _overallComments; }
            set { _overallComments = value; }
        }

        public int LikeWork
        {
            get { return _likeWork; }
            set { _likeWork = value; }
        }

        public int Background
        {
            get { return _background; }
            set { _background = value; }
        }

        public int WorkAmount
        {
            get { return _workAmount; }
            set { _workAmount = value; }
        }

        public int Objects
        {
            get { return _objects; }
            set { _objects = value; }
        }

        public int DevelopmentSkill
        {
            get { return _developmentSkill; }
            set { _developmentSkill = value; }
        }

        public int ResearchSkill
        {
            get { return _researchSkill; }
            set { _researchSkill = value; }
        }

        public int SDESkill
        {
            get { return _SDESkill; }
            set { _SDESkill = value; }
        }

        public int DesignSkill
        {
            get { return _designSkill; }
            set { _designSkill = value; }
        }

        public int ProjectSkill
        {
            get { return _projectSkill; }
            set { _projectSkill = value; }
        }

        public int CommunicationSkill
        {
            get { return _communicationSkill; }
            set { _communicationSkill = value; }
        }

        public int Teamwork
        {
            get { return _teamwork; }
            set { _teamwork = value; }
        }

        public string WorkComments
        {
            get { return _workComments; }
            set { _workComments = value; }
        }

        public int MentorSetGoal
        {
            get { return _mentorSetGoal; }
            set { _mentorSetGoal = value; }
        }

        public int HelpFromMentor
        {
            get { return _helpFromMentor; }
            set { _helpFromMentor = value; }
        }

        public int MakeGoodUse
        {
            get { return _makeGoodUse; }
            set { _makeGoodUse = value; }
        }

        public int Respect
        {
            get { return _respect; }
            set { _respect = value; }
        }

        public string MentorComments
        {
            get { return _mentorComments; }
            set { _mentorComments = value; }
        }

        public int TrainingView
        {
            get { return _trainingView; }
            set { _trainingView = value; }
        }

        public int TrainingSuitable
        {
            get { return _trainingSuitable; }
            set { _trainingSuitable = value; }
        }

        public int TrainingEssential
        {
            get { return _trainingEssential; }
            set { _trainingEssential = value; }
        }

        public int ActivityInterest
        {
            get { return _activityInterest; }
            set { _activityInterest = value; }
        }

        public string TrainingComments
        {
            get { return _trainingComments; }
            set { _trainingComments = value; }
        }

        public int Balance
        {
            get { return _balance; }
            set { _balance = value; }
        }

        public int WorkEnvironment
        {
            get { return _workEnvironment; }
            set { _workEnvironment = value; }
        }

        public int Compensation
        {
            get { return _compensation; }
            set { _compensation = value; }
        }

        public int Satisfaction
        {
            get { return _satisfaction; }
            set { _satisfaction = value; }
        }

        public int OnBoard
        {
            get { return _onBoard; }
            set { _onBoard = value; }
        }

        public int Accommodation
        {
            get { return _accommodation; }
            set { _accommodation = value; }
        }

        public int SalaryAndMeal
        {
            get { return _salaryAndMeal; }
            set { _salaryAndMeal = value; }
        }

        public int Reimbursement
        {
            get { return _reimbursement; }
            set { _reimbursement = value; }
        }

        public int ITSupport
        {
            get { return _itSupport; }
            set { _itSupport = value; }
        }

        public int DailySupport
        {
            get { return _dailySupport; }
            set { _dailySupport = value; }
        }

        public string LifeComments
        {
            get { return _lifeComments; }
            set { _lifeComments = value; }
        }

        public int Leading
        {
            get { return _leading; }
            set { _leading = value; }
        }

        public int MSCulture
        {
            get { return _msCulture; }
            set { _msCulture = value; }
        }

        public int ReturnAsIntern
        {
            get { return _returnAsIntern; }
            set { _returnAsIntern = value; }
        }

        public int JoinMS
        {
            get { return _joinMS; }
            set { _joinMS = value; }
        }

        public int Recommend
        {
            get { return _recommend; }
            set { _recommend = value; }
        }

        public string MSRAComments
        {
            get { return _msraComments; }
            set { _msraComments = value; }
        }

        public string Comments
        {
            get { return _comments; }
            set { _comments = value; }
        }

        public DateTime InsertDate
        {
            get { return _insertDate; }
            set { _insertDate = value; }
        }
        #endregion

        #region Public method
        public bool Update()
        {
            IDataProvider dp = DataProviderFactory.GetDataProvider();
            return dp.UpdateCheckoutSurvey(this);
        }

        public Guid Insert()
        {
            IDataProvider dp = DataProviderFactory.GetDataProvider();
            return dp.InsertCheckoutSurvey(this);
        }
        #endregion

        #region public static method
        public static CheckoutSurvey GetCheckoutSurveyById(Guid id)
        {
            IDataProvider dp = DataProviderFactory.GetDataProvider();
            return dp.GetCheckoutSurveyById(id);
        }

        public static DataSet GetCheckoutSurvey()
        {
            IDataProvider dp = DataProviderFactory.GetDataProvider();
            return dp.GetCheckoutSurvey();
        }

        public static DataSet GetCheckoutSurveyByApplicantId(Guid ApplicantId)
        {
            IDataProvider dp = DataProviderFactory.GetDataProvider();
            return dp.GetCheckoutSurveyByApplicantId(ApplicantId);
        }

        public static DataSet GetSurveyReport()
        {
            IDataProvider dp = DataProviderFactory.GetDataProvider();
            return dp.GetSurveyReport();
        }

        public static bool DeleteCheckoutSurveybyId(Guid Id)
        {
            IDataProvider dp = DataProviderFactory.GetDataProvider();
            return dp.DeleteCheckoutSurveyById(Id);
        }

        public static DataSet GetSurveyBasicInfoByApplicantId(Guid id)
        {
            IDataProvider dp = DataProviderFactory.GetDataProvider();
            return dp.GetSurveyBasicInfoByApplicantId(id);
        }

        #endregion
    }
}
