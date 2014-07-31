using System.Workflow.Activities;
using System.ComponentModel;
using System.Workflow.ComponentModel;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;
using System.Text;
using System.Threading;
using MSRA.SpringField.Components.BizObjects;
using System.Data.Linq;
using log4net;
using MSRA.Springfield.Workflows;
using System.Configuration;

/***********************************************
 * 
 * Workflow for importing data
 * 
 * Create Date: 3.26,2010
 * Author: Yin.P (v-yipu)
 * 
 ***********************************************/
namespace MSRA.Springfield.IntegrationWorkflow
{
    public sealed partial class IntegrationWorkflow : SequentialWorkflowActivity
    {
        /// <summary>
        /// log object(use log4net component)
        /// </summary>
        private static ILog logger = LogManager.GetLogger(typeof(IntegrationWorkflow));

        private SpringfieldDataContext DataDelgator = null;

        /// <summary>
        /// The current applicantid
        /// </summary>
        private Guid CurrentApplicantID = Guid.Empty;

        /// <summary>
        /// Check if the applicant record from PERSON's data is exist in springfield
        /// </summary>
        private bool IsApplicantMatched = true;

        /// <summary>
        /// Check if the applicant record has the relevant PA information.
        /// </summary>
        private bool HasPA = true;

        private int failedCount = 0;

        public int checkIndex = 0;

        public DataRow CurrentCheckingRow
        {
            get
            {
                if (ExportedData != null && checkIndex < ExportedData.Rows.Count)
                {
                    return ExportedData.Rows[checkIndex];
                }
                else
                    return null;
            }
        }

        #region For Import Log Data
        public int BatchNo
        {
            get;
            set;
        }
        #endregion

        public DataTable ExportedData
        {
            get;
            set;
        }

        /// <summary>
        /// The applicant that is importing
        /// </summary>
        public DataRow CurrentRow
        {
            get
            {
                if (ExportedData != null && currentIndex < ExportedData.Rows.Count)
                {
                    return ExportedData.Rows[currentIndex];
                }
                else
                    return null;
            }
        }

        /// <summary>
        /// records has been processed
        /// </summary>
        public int currentIndex = 0;

        public IntegrationWorkflow()
        {
            InitializeComponent();

            DataDelgator = new SpringfieldDataContext();

            this.IsApplicantExistActivity.Executing +=
                new EventHandler<ActivityExecutionStatusChangedEventArgs>(IsApplicantExistActivity_Executing);

            this.JudgeHasPAActivity.Executing +=
                new EventHandler<ActivityExecutionStatusChangedEventArgs>(JudgeHasPAActivity_Executing);

            this.HasRecordActivity.Executing +=
                new EventHandler<ActivityExecutionStatusChangedEventArgs>(HasRecordActivity_Executing);
        }

        void HasRecordActivity_Executing(object sender, ActivityExecutionStatusChangedEventArgs e)
        {

        }

        void JudgeHasPAActivity_Executing(object sender, ActivityExecutionStatusChangedEventArgs e)
        {
            JudgeHasPAByIdentityNumber(Convert.ToString(CurrentRow["IdentityNumber"]));
        }

        /// <summary>
        /// TODO: Update IsApplicantMatched
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void IsApplicantExistActivity_Executing(object sender, ActivityExecutionStatusChangedEventArgs e)
        {
            JudgeApplicantByIdentityNumber(Convert.ToString(CurrentRow["IdentityNumber"]));
        }

        #region Node Processes
        private void EmptyActivity_ExecuteCode(object sender, EventArgs e)
        {
            //Do nothing.
        }

        /// <summary>
        /// TODO: Get the applicant id for current existed applicant
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExistedApplicantActivity_ExecuteCode(object sender, EventArgs e)
        {
            if (CurrentRow.Field<string>("email") != null)
            {
                this.CurrentApplicantID = SiteUser.GetIdByFullName(CurrentRow.Field<string>("email"));
            }

            if (this.CurrentApplicantID == Guid.Empty)
            {
                if (CurrentRow["IdentityNumber"] != null && CurrentRow["IdentityNumber"] != DBNull.Value)
                {
                    this.CurrentApplicantID =
                        (DataDelgator.BasicInfos.FirstOrDefault<BasicInfo>(
                        p => p.IdentityNumber == CurrentRow.Field<string>("IdentityNumber"))).ApplicantId;
                }
            }
        }

        /// <summary>
        /// TODO: Add a new applicant record into database accroding to current record 
        /// from PERSON's exported data.
        /// TODO: Process of create an applicant: Create in membership -> create in applicantbasicinfo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddApplicantActivity_ExecuteCode(object sender, EventArgs e)
        {
            try
            {
                if (Membership.FindUsersByEmail(CurrentRow.Field<string>("email")).Count == 0)
                {
                    //Add user into membership and users
                    Membership.CreateUser(CurrentRow.Field<string>("email"),
                        PasswordGenerator(7, true, true, true, false, false), CurrentRow.Field<string>("email"));
                }
                BasicInfo basicInfo = new BasicInfo();
                EduBackground eduInfo = new EduBackground();

                //Retrieve the current applicant id after adding a new membership
                this.CurrentApplicantID = SiteUser.GetIdByFullName(CurrentRow.Field<string>("email"));

                if (this.CurrentApplicantID != Guid.Empty && this.CurrentApplicantID != null)
                {
                    #region Filling data into entity object
                    basicInfo.ApplicantId = this.CurrentApplicantID;

                    basicInfo.Gender = int.Parse(CurrentRow["Gender"].ParseString());
                    basicInfo.Nationality = CurrentRow.Field<string>("Nationality");
                    basicInfo.IdentityNumber = CurrentRow.Field<string>("IdentityNumber");
                    basicInfo.Address = CurrentRow.Field<string>("Address");
                    basicInfo.PhoneNumber = CurrentRow.Field<string>("PhoneNumber");
                    basicInfo.CurrentCity = CurrentRow.Field<string>("CurrentCity");
                    basicInfo.Email = CurrentRow.Field<string>("Email");
                    basicInfo.FirstName = CurrentRow.Field<string>("FirstName");
                    basicInfo.LastName = CurrentRow.Field<string>("LastName");
                    basicInfo.NameInChinese = CurrentRow.Field<string>("Nameinchinese");
                    if (CurrentRow.Field<DateTime>("End Date") < DateTime.Now)
                    {
                        basicInfo.Status = 2;
                    }
                    else
                    {
                        basicInfo.Status = 8;
                    }

                    eduInfo.ApplicantId = this.CurrentApplicantID;
                    eduInfo.College = CurrentRow.Field<string>("College");
                    eduInfo.AdvisorEmail = CurrentRow.Field<string>("AdvisorEmail");
                    eduInfo.AdvisorFirstName = CurrentRow.Field<string>("AdvisorFirstName");
                    eduInfo.AdvisorLastName = CurrentRow.Field<string>("AdvisorLastName");
                    eduInfo.AdvisorFullName = CurrentRow.Field<string>("AdvisorFullName");
                    eduInfo.AdvisorOrganization = CurrentRow.Field<string>("AdvisorOrganization");
                    eduInfo.Degree = int.Parse(CurrentRow["Degree"].ParseString().Length == 0 ? 
                        "0" : CurrentRow["Degree"].ParseString());

                    eduInfo.HighestEducationalInstitution = CurrentRow.Field<string>("HighestEducationalInstitution");
                    if (CurrentRow["EnrollDate"] == DBNull.Value)
                    {
                        eduInfo.EnrollDate = null;
                    }
                    else
                    {
                        eduInfo.EnrollDate = CurrentRow.Field<DateTime>("EnrollDate");
                    }

                    eduInfo.GraduatedDate = CurrentRow["GraduationDate"] == DBNull.Value ? DateTime.MaxValue : (DateTime)CurrentRow["GraduationDate"];
                    eduInfo.YearOfStudy = int.Parse(CurrentRow["YearOfStudy"].ParseString().Length == 0 ?
                        "0" : CurrentRow["YearOfStudy"].ParseString()); ;
                    eduInfo.Major = CurrentRow["Major"].ParseString();
                    eduInfo.MajorCategory = 1;
                    #endregion

                    //Update to database

                    DataDelgator.BasicInfos.InsertOnSubmit(basicInfo);
                    DataDelgator.EduBackgrounds.InsertOnSubmit(eduInfo);
                    //DataDelgator.SubmitChanges();
                }

            }
            catch (Exception ex)
            {
                failedCount++;
                logger.DebugFormat("[EXCEPTION] - Row No.:{0} - Intern ID:{1} - Error Message:{2}"
                , currentIndex.ToString(), CurrentRow.Field<string>("InternNo"), ex.Message);
            }
        }

        /// <summary>
        /// TODO: Add a new PA record for current applicant if it has not one.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddApplicantPAActivity_ExecuteCode(object sender, EventArgs e)
        {
            try
            {
                //Only the students who have checked out have PA information.
                if ((this.CurrentApplicantID != Guid.Empty && this.CurrentApplicantID != null) &&
                    CurrentRow.Field<DateTime>("End Date") < DateTime.Now)
                {
                    #region Filling data into pa entity object
                    sf_PerformanceAssessment pa = new sf_PerformanceAssessment();
                    pa.InternEmail = CurrentRow.Field<string>("email");
                    pa.InternName = CurrentRow.Field<string>("FirstName") + " " + CurrentRow.Field<string>("LastName");
                    pa.InternPhone = CurrentRow.Field<string>("PhoneNumber");
                    pa.InternPosition = CurrentRow["Position"].Equals(DBNull.Value) ?
                        2 : int.Parse(CurrentRow["Position"].ToString());

                    pa.ApplicantId = this.CurrentApplicantID;
                    pa.CheckInDate = CurrentRow.Field<DateTime>("Start Date");
                    pa.CheckOutDate = CurrentRow.Field<DateTime>("End Date");
                    pa.MentorAlias = CurrentRow.Field<string>("MentorAlias");
                    pa.MentorName = CurrentRow.Field<string>("MentorName");
                    pa.GraduationDate = (CurrentRow["GraduationDate"] == DBNull.Value || CurrentRow["GraduationDate"] == null) 
                        ? DateTime.MaxValue : CurrentRow.Field<DateTime>("GraduationDate");
                    //pa.InternPosition = int.Parse(CurrentRow["Position"].ParseString());
                    pa.GroupId = int.Parse(CurrentRow["GroupId"].ParseString());
                    pa.Discipline = 1;
                    pa.InsertDate = DateTime.Now;
                    pa.ModifyDate = DateTime.Now;
                    pa.Department = 1;
                    pa.ExpectedOnsiteInterviewDate = DateTime.MaxValue;
                    pa.id = Guid.NewGuid();
                    #endregion

                    //Update to database
                    DataDelgator.sf_PerformanceAssessments.InsertOnSubmit(pa);
                    //DataDelgator.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                failedCount++;
                logger.DebugFormat("[EXCEPTION] - Row No.:{0} - Intern ID:{1} - Error Message:{2}"
                , currentIndex.ToString(), CurrentRow.Field<string>("InternNo"), ex.Message);
            }
        }

        /// <summary>
        /// TODO: record the log no matter how the process runs.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LogActivity_ExecuteCode(object sender, EventArgs e)
        {
            logger.DebugFormat("[IMPORTED] - Row No.:{0} - Intern ID:{1}"
                , currentIndex.ToString(), CurrentRow.Field<string>("InternNo"));

            //Make log
            ImportLog log = new ImportLog();
            log.ApplicantID = this.CurrentApplicantID.ToString();
            log.BatchNo = this.BatchNo;
            log.InternNo = CurrentRow.Field<string>("InternNo");
            log.IsMatched = this.IsApplicantMatched ? 1 : 0;

            DataDelgator.ImportLogs.InsertOnSubmit(log);
            //DataDelgator.SubmitChanges();
        }

        private void FailedActivity_ExecuteCode(object sender, EventArgs e)
        {
            //TODO: failed handler
            logger.DebugFormat("[EXCEPTION] - Row No.:{0} - Intern ID:{1} - Error Message:{2}"
                , currentIndex.ToString(), CurrentRow.Field<string>("InternNo"), this.faultHandlerActivity1.Fault.Message);
        }

        #endregion

        #region Helper Functions
        /// <summary>
        /// Judge if the specific applicant is existed.
        /// </summary>
        /// <param name="id">judge condition</param>
        public void JudgeApplicantByIdentityNumber(string id)
        {
            if (id.Length == 0)
            {
                IsApplicantMatched = false;
            }
            else
            {
                //If the Identity Number is not existed in ApplicantBasicInfo table, 
                //then search the applicant in aspnet_Membership by email. 
                //If the email is existed, the record will be marked matched
                IsApplicantMatched = (DataDelgator.aspnet_Memberships.Count<aspnet_Membership>(
                    p => p.Email == CurrentRow.Field<string>("Email")) > 0);
                if (IsApplicantMatched)
                {
                    this.CurrentApplicantID = SiteUser.GetIdByFullName(CurrentRow.Field<string>("email"));

                    //Search the intern in ApplicantBasicInfo table by Identity Number.
                    IsApplicantMatched = (DataDelgator.BasicInfos
                        .Count<BasicInfo>(p => p.IdentityNumber == id) > 0);
                }
            }
        }

        /// <summary>
        /// Judge if an applicant has the relevant performance assessment information
        /// </summary>
        /// <param name="id">judge condition</param>
        public void JudgeHasPAByIdentityNumber(string id)
        {
            if (this.CurrentApplicantID != Guid.Empty || this.IsApplicantMatched)
            {
                //If checkout date and checkin date for one person is not the same, then add new pa record.
                HasPA = (DataDelgator.sf_PerformanceAssessments
                    .Count<sf_PerformanceAssessment>(p => (p.ApplicantId == this.CurrentApplicantID &&
                        p.CheckInDate.Equals(CurrentRow.Field<DateTime>("Start Date")) &&
                        p.CheckOutDate.Equals(CurrentRow.Field<DateTime>("End Date")))) > 0);
            }
        }

        /// <summary>
        /// Generate a random password for membership
        /// </summary>
        /// <param name="length"></param>
        /// <param name="enableNum"></param>
        /// <param name="enableLowercase"></param>
        /// <param name="enableUppercase"></param>
        /// <param name="enableSpecialChar"></param>
        /// <param name="enableRepeat"></param>
        /// <returns></returns>
        private string PasswordGenerator(int length, bool enableNum, bool enableLowercase,
            bool enableUppercase, bool enableSpecialChar, bool enableRepeat)
        {
            if (!enableNum && !enableLowercase && !enableUppercase && !enableSpecialChar)
            {
                throw new Exception("no sample series selected");
            }

            string numbers = "0123456789";
            string lowercase = "abcdefghijklmnopqrstuvwxyz";
            string uppercase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string specialChars = "_-<>*^;%/*$";
            StringBuilder sampleSeries = new StringBuilder(string.Empty);
            StringBuilder rndPwd = new StringBuilder(length);

            if (enableNum)
            {
                sampleSeries.Append(numbers);
            }

            if (enableLowercase)
            {
                sampleSeries.Append(lowercase);
            }

            if (enableUppercase)
            {
                sampleSeries.Append(uppercase);
            }

            if (enableSpecialChar)
            {
                sampleSeries.Append(specialChars);
            }

            //avoid the same seed
            Thread.Sleep(1);
            Random rndObj = new Random();

            if (length > sampleSeries.Length)
            {
                enableRepeat = true;
            }

            if (enableRepeat)
            {
                for (int i = 0; i < length; i++)
                {
                    int nextLoc = rndObj.Next(sampleSeries.Length);
                    rndPwd.Append(sampleSeries[nextLoc]);
                }
            }
            else
            {
                //if (length > sampleSeries.Length)
                //{
                //    throw new Exception("random string length should less than sample series length");
                //}

                for (int count = 0; count < (sampleSeries.Length * 2); count++)
                {
                    int i = rndObj.Next(sampleSeries.Length);
                    int j = rndObj.Next(sampleSeries.Length);

                    //exchange sampleSeries[i] and sampleSeries[j]
                    char temp = sampleSeries[i];
                    sampleSeries[i] = sampleSeries[j];
                    sampleSeries[j] = temp;
                }

                char[] rndChars = new char[length];
                sampleSeries.CopyTo(0, rndChars, 0, length);
                rndPwd.Append(rndChars);
            }

            return rndPwd.ToString();
        }
        #endregion

        private void StatisticsActivity_ExecuteCode(object sender, EventArgs e)
        {
            logger.DebugFormat("[STATISTICS] - Row Processed.:{0} - Failed Count:{1}",
                currentIndex.ToString(), failedCount.ToString());
        }

        private void UpdateEnumeratorActivity_ExecuteCode(object sender, EventArgs e)
        {
            currentIndex++;

            //submit all the changes in data context
            DataDelgator.SubmitChanges();
        }

        private void RequiredFieldCheckActivity_ExecuteCode(object sender, EventArgs e)
        {
            //TODO: Check if there is any null value in required field.
            if (ConfigurationManager.AppSettings["IS_ENABLE_REQUIRED_CHECKING"].Equals("true"))
            {

            }
        }

        private void PositionFieldCheckActivity_ExecuteCode(object sender, EventArgs e)
        {
            //TODO: Check Position field
            if (ConfigurationManager.AppSettings["IS_ENABLE_POSITION_CHECKING"].Equals("true"))
            {

            }
        }

        private void EmailFieldCheckActivity_ExecuteCode(object sender, EventArgs e)
        {
            //TODO: Check email format
            if (ConfigurationManager.AppSettings["IS_ENABLE_EMAIL_CHECKING"].Equals("true"))
            {

            }
        }
    }

}
