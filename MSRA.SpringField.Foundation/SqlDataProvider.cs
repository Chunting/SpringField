using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using MSRA.SpringField.Components;
using MSRA.SpringField.Components.BizObjects;
using MSRA.SpringField.Components.Enumerations;
using MSRA.SpringField.Components.Configuration;

namespace MSRA.SpringField.Foundation
{
    public class SqlDataProvider : IDataProvider
    {
        #region Private Members
        private DataProviderConfiguration dpConfig;
        private readonly string connectionString;
        private readonly string dbOwner;
        #endregion

        #region Constructor
        public SqlDataProvider(DataProviderConfiguration config)
        {
            dpConfig = config;
            connectionString = config.Attributes["connectionString"];
            dbOwner = config.Attributes["dbOwner"];
        }
        #endregion

        #region GetSqlConnection
        protected SqlConnection GetSqlConnection()
        {
            try
            {
                return new SqlConnection(connectionString);
            }
            catch
            {
                throw new Exception("SQL Connection String is invalid.");
            }
        }
        #endregion

        #region ApplicantBasicInfo
        //(@ApplicantId uniqueidentifier
        //,@Gender int
        //,@Nationality int
        //,@IdentityNumber nvarchar(256)
        //,@Webpage nvarchar(256)
        //,@Address nvarchar(256)
        //,@PhoneNumber nvarchar(256)
        //,@CurrentCity nvarchar(256)
        //,@CurrentProvince nvarchar(256)
        //,@CurrentCountry int
        //,@Status int
        //,@Priority int
        //,@ApplicationDate datetime
        //,@Email nvarchar(256)
        //,@FirstName nvarchar(256)
        //,@LastName nvarchar(256))
        public void InsertApplicantBasicInfo(ApplicantBasicInfo obj)
        {
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(dbOwner + ".sf_InsertApplicantBasicInfo", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;

                //myCommand.Parameters.Clear();

                myCommand.Parameters.AddWithValue("@ApplicantId", obj.ApplicantId);
                myCommand.Parameters.AddWithValue("@Gender", EnumHelper.EnumToInteger(obj.Gender));
                //myCommand.Parameters.AddWithValue("@Nationality", EnumHelper.EnumToInteger(obj.Nationality));
                myCommand.Parameters.AddWithValue("@Nationality", obj.Nationality);
                myCommand.Parameters.AddWithValue("@IdentityNumber", obj.IdentityNumber);
                myCommand.Parameters.AddWithValue("@Webpage", obj.WebPage);
                myCommand.Parameters.AddWithValue("@Address", obj.Address);
                myCommand.Parameters.AddWithValue("@PhoneNumber", obj.PhoneNumber);
                myCommand.Parameters.AddWithValue("@CurrentCity", obj.CurrentCity);
                myCommand.Parameters.AddWithValue("@CurrentProvince", obj.CurrentProvince);
                //myCommand.Parameters.AddWithValue("@CurrentCountry", EnumHelper.EnumToInteger(obj.CurrentCountry));
                myCommand.Parameters.AddWithValue("@CurrentCountry", obj.CurrentCountry);
                myCommand.Parameters.AddWithValue("@Status", EnumHelper.EnumToInteger(obj.Status));
                myCommand.Parameters.AddWithValue("@Priority", obj.Priority);
                myCommand.Parameters.AddWithValue("@ApplicationDate", obj.ApplicationTime);
                myCommand.Parameters.AddWithValue("@Email", obj.Email);
                myCommand.Parameters.AddWithValue("@FirstName", obj.FirstName);
                myCommand.Parameters.AddWithValue("@LastName", obj.LastName);
                myCommand.Parameters.AddWithValue("@ReferralId", obj.ReferralId);
                myCommand.Parameters.AddWithValue("@NameInChinese", obj.NameInChinese);
                /*
                 * Add by Yuanqin, 
                 * 2011.2.23
                 */
                myCommand.Parameters.AddWithValue("@IsOffline", EnumHelper.EnumToInteger(obj.IsOffline));

                myConnection.Open();
                myCommand.ExecuteNonQuery();
                myConnection.Close();
            }
        }

        public void UpdateApplicantBasicInfo(ApplicantBasicInfo obj)
        {
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(dbOwner + ".sf_UpdateApplicantBasicInfo", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;

                myCommand.Parameters.AddWithValue("@ApplicantId", obj.ApplicantId);
                myCommand.Parameters.AddWithValue("@Gender", EnumHelper.EnumToInteger(obj.Gender));
                //myCommand.Parameters.AddWithValue("@Nationality", EnumHelper.EnumToInteger(obj.Nationality));
                myCommand.Parameters.AddWithValue("@Nationality", obj.Nationality);
                myCommand.Parameters.AddWithValue("@IdentityNumber", obj.IdentityNumber);
                myCommand.Parameters.AddWithValue("@Webpage", obj.WebPage);
                myCommand.Parameters.AddWithValue("@Address", obj.Address);
                myCommand.Parameters.AddWithValue("@PhoneNumber", obj.PhoneNumber);
                myCommand.Parameters.AddWithValue("@CurrentCity", obj.CurrentCity);
                myCommand.Parameters.AddWithValue("@CurrentProvince", obj.CurrentProvince);
                //myCommand.Parameters.AddWithValue("@CurrentCountry", EnumHelper.EnumToInteger(obj.CurrentCountry));
                myCommand.Parameters.AddWithValue("@CurrentCountry", obj.CurrentCountry);
                myCommand.Parameters.AddWithValue("@Status", EnumHelper.EnumToInteger(obj.Status));
                myCommand.Parameters.AddWithValue("@Priority", obj.Priority);
                if (obj.ApplicationTime == Convert.ToDateTime("1/1/0001 12:00:00 AM"))
                {
                    obj.ApplicationTime = Convert.ToDateTime(" 1/1/1753 12:00:00 AM");
                }
                myCommand.Parameters.AddWithValue("@ApplicationDate", obj.ApplicationTime);
                myCommand.Parameters.AddWithValue("@Email", obj.Email);
                myCommand.Parameters.AddWithValue("@FirstName", obj.FirstName);
                myCommand.Parameters.AddWithValue("@LastName", obj.LastName);
                myCommand.Parameters.AddWithValue("@ReferralId", obj.ReferralId);
                myCommand.Parameters.AddWithValue("@NameInChinese", obj.NameInChinese);
                /*
                 * Add by Yuanqin, 
                 * 2011.2.23
                 */
                myCommand.Parameters.AddWithValue("@IsOffline", EnumHelper.EnumToInteger(obj.IsOffline));

                myConnection.Open();
                myCommand.ExecuteNonQuery();
                myConnection.Close();
            }
        }

        public void DeleteApplicantBasicInfoById(Guid id)
        {
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(dbOwner + ".sf_DeleteApplicantBasicInfoById", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("@ApplicantId", id);
                myConnection.Open();
                myCommand.ExecuteNonQuery();
                myConnection.Close();
            }
        }

        public ApplicantBasicInfo GetApplicantBasicInfoById(Guid id)
        {
            ApplicantBasicInfo myInfo = null;

            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(dbOwner + ".sf_SelectApplicantBasicInfoById", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("@ApplicantId", id);
                myConnection.Open();

                using (SqlDataReader reader = myCommand.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (reader.Read())
                    {
                        myInfo = new ApplicantBasicInfo();

                        myInfo.ApplicantId = (Guid)reader["ApplicantId"];
                        myInfo.Gender = (GenderEnum)EnumHelper.IntegerToEnum(typeof(GenderEnum), Convert.ToInt32(reader["Gender"]));
                        //myInfo.Nationality = (CountryEnum)EnumHelper.IntegerToEnum(typeof(CountryEnum), Convert.ToInt32(reader["Nationality"]));
                        myInfo.Nationality = (string)reader["Nationality"];
                        myInfo.IdentityNumber = (string)reader["IdentityNumber"];
                        myInfo.WebPage = Convert.ToString(reader["Webpage"]);
                        myInfo.Address = Convert.ToString(reader["Address"]);
                        myInfo.PhoneNumber = Convert.ToString(reader["PhoneNumber"]);
                        myInfo.CurrentCity = Convert.ToString(reader["CurrentCity"]);
                        myInfo.CurrentProvince = Convert.ToString(reader["CurrentProvince"]);
                        //myInfo.CurrentCountry = (CountryEnum)EnumHelper.IntegerToEnum(typeof(CountryEnum), Convert.ToInt32(reader["CurrentCountry"]));
                        myInfo.CurrentCountry = Convert.ToString(reader["CurrentCountry"]);
                        myInfo.Status = (ApplicationStatusEnum)EnumHelper.IntegerToEnum(typeof(ApplicationStatusEnum), Convert.ToInt32(reader["Status"]));
                        myInfo.Priority = Convert.ToInt32(reader["Priority"]);
                        myInfo.ApplicationTime = reader["ApplicationDate"].ParseDateTime();
                        myInfo.Email = Convert.ToString(reader["Email"]);
                        myInfo.FirstName = Convert.ToString(reader["FirstName"]);
                        myInfo.LastName = Convert.ToString(reader["LastName"]);
                        myInfo.ReferralId = reader["ReferralId"].ParseInt();
                        myInfo.NameInChinese = Convert.ToString(reader["NameInChinese"]);
                        /*
                         * Add by Yuanqin, 
                         * 2011.2.23
                         */
                        myInfo.IsOffline = (IsOfflineEnum)EnumHelper.IntegerToEnum(typeof(IsOfflineEnum), Convert.ToInt32(reader["IsOffline"]));

                        reader.Close();
                    }
                }

                myConnection.Close();
            }

            return myInfo;
        }

        public void ChangeApplicantStatus(Guid id, ApplicationStatusEnum status)
        {
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(dbOwner + ".sf_ChangeApplicantStatus", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("@ApplicantId", id);
                myCommand.Parameters.AddWithValue("@Status", EnumHelper.EnumToInteger(status));

                myConnection.Open();
                myCommand.ExecuteNonQuery();
                myConnection.Close();
            }
        }
        #endregion

        #region ApplicantEduBackground
        //@ApplicantId uniqueidentifier
        //,@HighestEducationalInstitution int
        //,@MajorCategory int
        //,@Major nvarchar(256)
        //,@YearOfStudy int
        //,@Rank int
        //,@ResumeId int
        //,@PaperAId int
        //,@PaperBId int
        //,@GraduatedDate datetime
        //,@AdvisorFirstName nvarchar(256)
        //,@AdvisorLastName nvarchar(256)
        //,@AdvisorFullName nvarchar(256)
        //,@AdvisorEmail nvarchar(256)
        //,@AdvisorOrganization nvarchar(256)
        //,@ResearchApproach int
        public void InsertApplicantEduBackground(ApplicantEduBackground obj)
        {
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(dbOwner + ".sf_InsertApplicantEduBackground", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;

                myCommand.Parameters.AddWithValue("@ApplicantId", obj.ApplicantId);
                myCommand.Parameters.AddWithValue("@HighestEducationalInstitution", obj.HighestEduInstitution);
                myCommand.Parameters.AddWithValue("@MajorCategory", EnumHelper.EnumToInteger(obj.MajorCategory));
                myCommand.Parameters.AddWithValue("@Major", obj.Major);
                myCommand.Parameters.AddWithValue("@YearOfStudy", obj.YearOfStudy);
                myCommand.Parameters.AddWithValue("@Rank", EnumHelper.EnumToInteger(obj.Rank));
                myCommand.Parameters.AddWithValue("@ResumeId", obj.Resume.DocId);
                myCommand.Parameters.AddWithValue("@ResumeImage", obj.ResumeImage);
                myCommand.Parameters.AddWithValue("@ResumeExt", obj.ResumeExt);
                myCommand.Parameters.AddWithValue("@PaperAId", obj.Papers[0].DocId);
                myCommand.Parameters.AddWithValue("@PaperBId", obj.Papers[1].DocId);
                /*
                 * DateTime must be larger than 1753-1-1
                 * @Author: Yin Pu
                 * @Date: 2009-11-05
                 */
                if (obj.EnrollDate < DateTime.Parse("1753-1-1"))
                {
                    obj.EnrollDate = DateTime.Parse("1753-1-1");
                }
                myCommand.Parameters.AddWithValue("@EnrollDate", obj.EnrollDate);
                myCommand.Parameters.AddWithValue("@GraduatedDate", obj.GraduateDate);
                myCommand.Parameters.AddWithValue("@AdvisorFirstName", obj.InternAdvisor.FirstName);
                myCommand.Parameters.AddWithValue("@AdvisorLastName", obj.InternAdvisor.LastName);
                myCommand.Parameters.AddWithValue("@AdvisorFullName", obj.InternAdvisor.FullName);
                myCommand.Parameters.AddWithValue("@AdvisorEmail", obj.InternAdvisor.Email);
                myCommand.Parameters.AddWithValue("@AdvisorOrganization", obj.InternAdvisor.Organization);
                myCommand.Parameters.AddWithValue("@ResearchApproach", EnumHelper.EnumToInteger(obj.ResearchApproach));
                myCommand.Parameters.AddWithValue("@Degree", EnumHelper.EnumToInteger(obj.Degree));
                //myCommand.Parameters.AddWithValue("@Degree", obj.Degree);

                myConnection.Open();
                myCommand.ExecuteNonQuery();
                myConnection.Close();
            }
        }

        public void UpdateApplicantEduBackground(ApplicantEduBackground obj)
        {
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(dbOwner + ".sf_UpdateApplicantEduBackground", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;

                myCommand.Parameters.AddWithValue("@ApplicantId", obj.ApplicantId);
                myCommand.Parameters.AddWithValue("@HighestEducationalInstitution", obj.HighestEduInstitution);
                myCommand.Parameters.AddWithValue("@MajorCategory", EnumHelper.EnumToInteger(obj.MajorCategory));
                myCommand.Parameters.AddWithValue("@Major", obj.Major);
                myCommand.Parameters.AddWithValue("@YearOfStudy", obj.YearOfStudy);
                myCommand.Parameters.AddWithValue("@Rank", EnumHelper.EnumToInteger(obj.Rank));
                myCommand.Parameters.AddWithValue("@ResumeId", obj.Resume.DocId);
                myCommand.Parameters.AddWithValue("@ResumeImage", obj.ResumeImage);
                myCommand.Parameters.AddWithValue("@ResumeExt", obj.ResumeExt);
                myCommand.Parameters.AddWithValue("@PaperAId", obj.Papers[0].DocId);
                myCommand.Parameters.AddWithValue("@PaperBId", obj.Papers[1].DocId);
                /*
                 * DateTime must be larger than 1753-1-1
                 * @Author: Yin Pu
                 * @Date: 2009-11-05
                 */
                if (obj.EnrollDate < DateTime.Parse("1753-1-1"))
                {
                    obj.EnrollDate = DateTime.Parse("1753-1-1");
                }
                myCommand.Parameters.AddWithValue("@EnrollDate", obj.EnrollDate);
                myCommand.Parameters.AddWithValue("@GraduatedDate", obj.GraduateDate);
                myCommand.Parameters.AddWithValue("@AdvisorFirstName", obj.InternAdvisor.FirstName);
                myCommand.Parameters.AddWithValue("@AdvisorLastName", obj.InternAdvisor.LastName);
                myCommand.Parameters.AddWithValue("@AdvisorFullName", obj.InternAdvisor.FullName);
                myCommand.Parameters.AddWithValue("@AdvisorEmail", obj.InternAdvisor.Email);
                myCommand.Parameters.AddWithValue("@AdvisorOrganization", obj.InternAdvisor.Organization);
                myCommand.Parameters.AddWithValue("@ResearchApproach", EnumHelper.EnumToInteger(obj.ResearchApproach));
                myCommand.Parameters.AddWithValue("@Degree", EnumHelper.EnumToInteger(obj.Degree));
                //myCommand.Parameters.AddWithValue("@Degree", obj.Degree);

                myConnection.Open();
                myCommand.ExecuteNonQuery();
                myConnection.Close();
            }
        }

        public void DeleteApplicantEduBackgroundById(Guid id)
        {
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(dbOwner + ".sf_DeleteApplicantEduBackgroundById", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("@ApplicantId", id);
                myConnection.Open();
                myCommand.ExecuteNonQuery();
                myConnection.Close();
            }
        }

        public ApplicantEduBackground GetApplicantEduBackgroundById(Guid id)
        {
            ApplicantEduBackground myInfo = null;

            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(dbOwner + ".sf_SelectApplicantEduBackgroundById", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("@ApplicantId", id);
                myConnection.Open();

                using (SqlDataReader reader = myCommand.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (reader.Read())
                    {
                        myInfo = new ApplicantEduBackground();
                        myInfo.ApplicantId = (Guid)reader["ApplicantId"];
                        //myInfo.HighestEduInstitution = (CollegeEnum)EnumHelper.IntegerToEnum(typeof(CollegeEnum), Convert.ToInt32(reader["HighestEducationalInstitution"]));
                        myInfo.HighestEduInstitution = reader["HighestEducationalInstitution"].ParseString();
                        myInfo.MajorCategory = (MajorCategoryEnum)EnumHelper.IntegerToEnum(typeof(MajorCategoryEnum), Convert.ToInt32(reader["MajorCategory"]));
                        myInfo.Major = reader["Major"].ParseString();
                        myInfo.YearOfStudy = Convert.ToInt32(reader["YearOfStudy"]);
                        myInfo.Degree = (DegreeEnum)(int)reader["Degree"];
                        //myInfo.Degree = (string)reader["Degree"];
                        myInfo.Rank = (RankEnum)EnumHelper.IntegerToEnum(typeof(RankEnum), Convert.ToInt32(reader["Rank"]));

                        myInfo.Resume = Document.GetDocumentById(reader["ResumeId"].ParseInt());//Resume
                        myInfo.Papers[0] = Document.GetDocumentById(reader["PaperAId"].ParseInt());//PaperA
                        myInfo.Papers[1] = Document.GetDocumentById(reader["PaperBId"].ParseInt());//PaperB

                        if (reader["EnrollDate"] != System.DBNull.Value)
                        {
                            myInfo.EnrollDate = reader["EnrollDate"].ParseDateTime();
                        }
                        else
                        {
                            myInfo.EnrollDate = DateTime.MaxValue;
                        }

                        if (reader["GraduatedDate"] != System.DBNull.Value)
                        {
                            myInfo.GraduateDate = reader["GraduatedDate"].ParseDateTime();
                        }
                        else
                        {
                            myInfo.GraduateDate = DateTime.MaxValue;
                        }

                        myInfo.InternAdvisor = new Advisor();
                        myInfo.InternAdvisor.ApplicantId = (Guid)reader["ApplicantId"];
                        myInfo.InternAdvisor.FirstName = reader["AdvisorFirstName"].ParseString();
                        myInfo.InternAdvisor.LastName = reader["AdvisorLastName"].ParseString();
                        myInfo.InternAdvisor.Email = reader["AdvisorEmail"].ParseString();
                        myInfo.InternAdvisor.Organization = reader["AdvisorOrganization"].ParseString();
                        myInfo.ResearchApproach = (ResearchApproachEnum)Convert.ToInt32(reader["ResearchApproach"]);

                        myInfo.ResumeExt = reader["ResumeExt"].ToString();
                        if (reader["ResumeImage"] == System.DBNull.Value)
                        {
                            myInfo.ResumeImage = null;
                        }
                        else
                        {
                            myInfo.ResumeImage = (Byte[])reader["ResumeImage"];
                        }
                        reader.Close();
                    }
                }

                myConnection.Close();
            }

            return myInfo;
        }
        #endregion

        #region ApplicantRelatedInfo
        //@ApplicantId uniqueidentifier
        //,@PreferredStartDate datetime
        //,@SecondaryStartDate datetime
        //,@InterestedGroup int
        //,@InternshipType int
        //,@InterestedAreas nvarchar(256)
        //,@InforSource int
        //,@InfoSourceDetail int
        //,@InfoSourceText nvarchar(256)
        public void InsertApplicantRelatedInfo(ApplicantRelatedInfo obj)
        {
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(dbOwner + ".sf_InsertApplicantRelatedInfo", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;

                myCommand.Parameters.AddWithValue("@ApplicantId", obj.ApplicantId);
                myCommand.Parameters.AddWithValue("@PreferredStartDate", obj.PreferredAvailStartDate);
                myCommand.Parameters.AddWithValue("@SecondaryStartDate", obj.SecondaryAvailStartDate);
                //myCommand.Parameters.AddWithValue("@InterestedGroup", EnumHelper.EnumToInteger(obj.InterestedGroup));
                myCommand.Parameters.AddWithValue("@InterestedGroup", obj.InterestedGroup);
                myCommand.Parameters.AddWithValue("@PreferredPosition", EnumHelper.EnumToInteger(obj.PreferredPosition));
                myCommand.Parameters.AddWithValue("@InternshipType", EnumHelper.EnumToInteger(obj.InternshipType));
                myCommand.Parameters.AddWithValue("@InterestedAreas", obj.InterestedAreas);
                myCommand.Parameters.AddWithValue("@SpecialProgram", obj.SpecialProgram);
                //myCommand.Parameters.AddWithValue("@InforSource", EnumHelper.EnumToInteger(obj.InfoSource));
                //myCommand.Parameters.AddWithValue("@InfoSourceDetail", EnumHelper.EnumToInteger(obj.InfoSourceDetail));
                //myCommand.Parameters.AddWithValue("@InfoSourceText", obj.InfoSourceText);
                myCommand.Parameters.AddWithValue("@JobInfoCategory", obj.JobInfoCategory);
                myCommand.Parameters.AddWithValue("@JobInfoSource", obj.JobInfoSource);
                myCommand.Parameters.AddWithValue("@JobInfoChannel", obj.JobInfoChannel);
                myCommand.Parameters.AddWithValue("@JobInfoDetail", obj.JobInfoDetail);

                myConnection.Open();
                myCommand.ExecuteNonQuery();
                myConnection.Close();
            }
        }

        public void UpdateApplicantRelatedInfo(ApplicantRelatedInfo obj)
        {
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(dbOwner + ".sf_UpdateApplicantRelatedInfo", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;

                myCommand.Parameters.AddWithValue("@ApplicantId", obj.ApplicantId);
                myCommand.Parameters.AddWithValue("@PreferredStartDate", obj.PreferredAvailStartDate);
                myCommand.Parameters.AddWithValue("@SecondaryStartDate", obj.SecondaryAvailStartDate);
                //myCommand.Parameters.AddWithValue("@InterestedGroup", EnumHelper.EnumToInteger(obj.InterestedGroup));
                myCommand.Parameters.AddWithValue("@InterestedGroup", obj.InterestedGroup);
                myCommand.Parameters.AddWithValue("@PreferredPosition", EnumHelper.EnumToInteger(obj.PreferredPosition));
                myCommand.Parameters.AddWithValue("@InternshipType", EnumHelper.EnumToInteger(obj.InternshipType));
                myCommand.Parameters.AddWithValue("@InterestedAreas", obj.InterestedAreas);
                myCommand.Parameters.AddWithValue("@SpecialProgram", obj.SpecialProgram);
                //myCommand.Parameters.AddWithValue("@InforSource", EnumHelper.EnumToInteger(obj.InfoSource));
                //myCommand.Parameters.AddWithValue("@InfoSourceDetail", EnumHelper.EnumToInteger(obj.InfoSourceDetail));
                //myCommand.Parameters.AddWithValue("@InfoSourceText", obj.InfoSourceText);
                myCommand.Parameters.AddWithValue("@JobInfoCategory", obj.JobInfoCategory);
                myCommand.Parameters.AddWithValue("@JobInfoSource", obj.JobInfoSource);
                myCommand.Parameters.AddWithValue("@JobInfoChannel", obj.JobInfoChannel);
                myCommand.Parameters.AddWithValue("@JobInfoDetail", obj.JobInfoDetail);

                myConnection.Open();
                myCommand.ExecuteNonQuery();
                myConnection.Close();
            }
        }

        public void DeleteApplicantRelatedInfoById(Guid id)
        {
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(dbOwner + ".sf_DeleteApplicantRelatedInfoById", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("@ApplicantId", id);
                myConnection.Open();
                myCommand.ExecuteNonQuery();
                myConnection.Close();
            }
        }

        public ApplicantRelatedInfo GetApplicantRelatedInfoById(Guid id)
        {
            ApplicantRelatedInfo myInfo = null;

            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(dbOwner + ".sf_SelectApplicantRelatedInfoById", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("@ApplicantId", id);
                myConnection.Open();

                using (SqlDataReader reader = myCommand.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (reader.Read())
                    {
                        myInfo = new ApplicantRelatedInfo();

                        myInfo.ApplicantId = (Guid)reader["ApplicantId"];
                        myInfo.PreferredAvailStartDate = (DateTime)reader["PreferredStartDate"];
                        myInfo.SecondaryAvailStartDate = (DateTime)reader["SecondaryStartDate"];
                        //myInfo.InterestedGroup = (GroupsEnum)EnumHelper.IntegerToEnum(typeof(GroupsEnum), Convert.ToInt32(reader["InterestedGroup"]));
                        myInfo.InterestedGroup = (string)reader["InterestedGroup"];
                        myInfo.InterestedAreas = Convert.ToString(reader["InterestedAreas"]);
                        myInfo.InternshipType = (InternshipTypeEnum)EnumHelper.IntegerToEnum(typeof(InternshipTypeEnum), Convert.ToInt32(reader["InternshipType"]));
                        if (reader["PreferredPosition"] == DBNull.Value)
                        {
                            myInfo.PreferredPosition = PositionTypeEnum.Unknown;
                        }
                        else
                        {

                            myInfo.PreferredPosition = (PositionTypeEnum)EnumHelper.IntegerToEnum(typeof(PositionTypeEnum), Convert.ToInt32(reader["PreferredPosition"]));
                        }
                        if (reader["SpecialProgram"] == DBNull.Value)
                        {
                            myInfo.SpecialProgram = string.Empty;
                        }
                        else
                        {

                            myInfo.SpecialProgram = (string)(reader["SpecialProgram"]);
                        }
                        //myInfo.InfoSource = (InfoSourceEnum)EnumHelper.IntegerToEnum(typeof(InfoSourceEnum), Convert.ToInt32(reader["InforSource"]));
                        //myInfo.InfoSourceDetail = (InfoSourceDetailEnum)EnumHelper.IntegerToEnum(typeof(InfoSourceDetailEnum), Convert.ToInt32(reader["InfoSourceDetail"]));
                        //myInfo.InfoSourceText = Convert.ToString(reader["InfoSourceText"]);
                        myInfo.JobInfoCategory = Convert.ToString(reader["JobInfoCategory"]);
                        myInfo.JobInfoSource = Convert.ToString(reader["JobInfoSource"]);
                        myInfo.JobInfoChannel = Convert.ToString(reader["JobInfoChannel"]);
                        myInfo.JobInfoDetail = Convert.ToString(reader["JobInfoDetail"]);

                        reader.Close();
                    }
                }

                myConnection.Close();
            }

            return myInfo;
        }
        #endregion

        #region Applicant

        /// <summary>
        /// Fetch applicants by condition
        /// Author: Yin.P
        /// Date: 2010-1-8
        /// </summary>
        /// <param name="IsFeedbackCompleted">Is Feedback completed</param>
        /// <param name="mentorAlias">intern's mentor</param>
        /// <param name="interviewStatus">interview process status</param>
        /// <returns></returns>
        public DataSet GetApplicantsByCondition(/*int IsFeedbackCompleted, string mentorAlias, int interviewStatus*/)
        {
            DataSet dsApplicants;
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(dbOwner + ".sf_GetApplicantsByCondition", myConnection);
                SqlDataAdapter da = new SqlDataAdapter(myCommand);
                myCommand.CommandType = CommandType.StoredProcedure;

                //myCommand.Parameters.AddWithValue("@IsFeedbackCompleted", IsFeedbackCompleted);
                //myCommand.Parameters.AddWithValue("@MentorAlias", mentorAlias);
                //myCommand.Parameters.AddWithValue("@InterviewStatus", interviewStatus);

                myConnection.Open();
                dsApplicants = new DataSet("ApplicnatsWithCondition");
                da.Fill(dsApplicants, "Applicants");
                myConnection.Close();
            }
            return dsApplicants;
        }

        public DataSet GetAllApplicants()
        {
            DataSet dsApplicants;
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(dbOwner + ".sf_GetAllApplicants", myConnection);
                SqlDataAdapter da = new SqlDataAdapter(myCommand);
                myCommand.CommandType = CommandType.StoredProcedure;

                myConnection.Open();
                dsApplicants = new DataSet("AllApplicnats");
                da.Fill(dsApplicants, "Applicants");
                myConnection.Close();
            }
            return dsApplicants;
        }

        /// <summary>
        /// Get all applicants without permission filter
        /// Author: Yin.P
        /// Date: 2010-1-7
        /// </summary>
        /// <returns></returns>
        public DataSet GetAllApplicantsWithoutPermissionFilter()
        {
            DataSet dsApplicants;
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(dbOwner + ".sf_GetAllApplicantsWithoutPermissionFilter", myConnection);
                SqlDataAdapter da = new SqlDataAdapter(myCommand);
                myCommand.CommandType = CommandType.StoredProcedure;

                myConnection.Open();
                dsApplicants = new DataSet("AllApplicnats");
                da.Fill(dsApplicants, "Applicants");
                myConnection.Close();
            }
            return dsApplicants;
        }

        public DataSet GetApplicantsByApplicationDate(DateTime dtStart, DateTime dtEnd)
        {
            DataSet dsApplicants;
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(dbOwner + ".sf_GetApplicantsByDate", myConnection);
                SqlDataAdapter da = new SqlDataAdapter(myCommand);
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add(new SqlParameter("@StartDate", dtStart));
                myCommand.Parameters.Add(new SqlParameter("@EndDate", dtEnd));
                myConnection.Open();
                dsApplicants = new DataSet("Applicnats");
                da.Fill(dsApplicants, "Applicants");
                myConnection.Close();
            }
            return dsApplicants;
        }

        /// <summary>
        /// Get all candidats by check-in date 
        /// Added by Yuanqin at 2011-4-25
        /// </summary>
        /// <returns></returns>
        public DataSet GetApplicantsByCheckInDate(DateTime dtStart, DateTime dtEnd)
        {
            DataSet dsApplicants;
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(dbOwner + ".sf_GetOnBoardApplicantsByDate", myConnection);
                SqlDataAdapter da = new SqlDataAdapter(myCommand);
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add(new SqlParameter("@StartDate", dtStart));
                myCommand.Parameters.Add(new SqlParameter("@EndDate", dtEnd));
                myConnection.Open();
                dsApplicants = new DataSet("Applicnats");
                da.Fill(dsApplicants, "Applicants");
                myConnection.Close();
            }
            return dsApplicants;
        }

        public DataSet GetAllInterviewApplicants()
        {
            DataSet dsApplicants;
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(dbOwner + ".sf_GetAllInterviewApplicants", myConnection);
                SqlDataAdapter da = new SqlDataAdapter(myCommand);
                myCommand.CommandType = CommandType.StoredProcedure;

                myConnection.Open();
                dsApplicants = new DataSet("AllInterviewApplicnats");
                da.Fill(dsApplicants, "InterviewApplicants");
                myConnection.Close();
            }
            return dsApplicants;
        }

        public bool IsIdNumUsed(string idCard)
        {
            bool isUsed = true;
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(dbOwner + ".sf_IsIdentityNumberUsed", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("@IdentityNumber", idCard);
                myConnection.Open();
                object returnValue = myCommand.ExecuteScalar();
                if (returnValue != null || (Convert.ToInt32(returnValue) > 0))
                {
                    isUsed = true;
                }
                else
                {
                    isUsed = false;
                }
                myConnection.Close();
            }
            return isUsed;
        }

        public DataSet GetDecidedApplicants(Int16 InterviewStatus)
        {
            DataSet dsApplicants;
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(dbOwner + ".sf_unsendmailist", myConnection);
                SqlDataAdapter da = new SqlDataAdapter(myCommand);
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("@InterviewStatus", InterviewStatus);
                myConnection.Open();
                dsApplicants = new DataSet("AllApplicnats");
                da.Fill(dsApplicants, "Applicants");
                myConnection.Close();
            }
            return dsApplicants;
        }

        public void DeleteApplcantById(Guid applicantId)
        {
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(dbOwner + ".sf_DeleteApplicantById", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("@ApplicantId", applicantId);
                myConnection.Open();
                myCommand.ExecuteNonQuery();
                myConnection.Close();
            }
        }

        public DataSet GetApplicantsByResume(String condition)
        {
            condition = String.Format("\"{0}\"", condition);
            DataSet dsApplicants;
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(dbOwner + ".sf_SearchApplicantsByResume", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(myCommand);
                myCommand.Parameters.AddWithValue("@Condition", condition);
                myConnection.Open();
                dsApplicants = new DataSet("Applicnats");
                da.Fill(dsApplicants, "Applicants");
                myConnection.Close();
            }
            return dsApplicants;
        }

        /// <summary>
        /// Get all applicants with their checkin form
        /// Added by Yi Shao at 2009-6-10
        /// </summary>
        /// <param name="status">interview status</param>
        /// <returns></returns>
        public DataSet GetApplicantsByStatus(InterviewStatusEnum status)
        {
            DataSet dsApplicants;
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(dbOwner + ".sf_GetApplicantsByStatus", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(myCommand);
                myCommand.Parameters.AddWithValue("@Status", Convert.ToInt32(status));
                myConnection.Open();
                dsApplicants = new DataSet("Applicants");
                da.Fill(dsApplicants, "Applicants");
                myConnection.Close();
            }
            return dsApplicants;
        }
        public DataSet GetApplicantsByStatus()
        {
            DataSet dsApplicants;
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(dbOwner + ".sf_GetApplicantsByStatus", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(myCommand);
                myCommand.Parameters.AddWithValue("@Status", -1);
                myConnection.Open();
                dsApplicants = new DataSet("Applicants");
                da.Fill(dsApplicants, "Applicants");
                myConnection.Close();
            }
            return dsApplicants;
        }

        public DataSet GetHiredApplicants()
        {
            DataSet dsApplicants;
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(dbOwner + ".sf_GetHiredCandidates", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(myCommand);
                myConnection.Open();
                dsApplicants = new DataSet("HiredApplicants");
                da.Fill(dsApplicants, "HiredApplicants");
                myConnection.Close();
            }
            return dsApplicants;
        }

        #endregion

        #region EmailTemplate
        public EmailTemplate GetEmailTemplateByType(MailType mailType)
        {
            EmailTemplate emailTemplate = null;
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(dbOwner + ".sf_GetEmailTemplate", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("@EmailType", EnumHelper.EnumToInteger(mailType));
                myConnection.Open();

                using (SqlDataReader reader = myCommand.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (reader.Read())
                    {
                        emailTemplate = new EmailTemplate();

                        int i = (int)reader["EmailType"];
                        emailTemplate.EmailType = (MailType)EnumHelper.IntegerToEnum(typeof(MailType), (int)reader["EmailType"]);
                        emailTemplate.From = (string)reader["EmailFrom"];
                        emailTemplate.To = (string)reader["EmailTo"];
                        emailTemplate.CC = (string)reader["EmailCc"];
                        emailTemplate.BCC = (string)reader["EmailBcc"];
                        emailTemplate.Subject = (string)reader["EmailSubject"];
                        emailTemplate.Body = (string)reader["EmailTemplate"];
                        reader.Close();
                    }
                }
                myConnection.Close();
            }
            return emailTemplate;
        }

        public void UpdateEmailTemplate(EmailTemplate emailTemplate)
        {
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(dbOwner + ".sf_UpdateEmailTemplate", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;

                myCommand.Parameters.AddWithValue("@EmailType", EnumHelper.EnumToInteger(emailTemplate.EmailType));
                myCommand.Parameters.AddWithValue("@Body", emailTemplate.Body);
                myCommand.Parameters.AddWithValue("@From", emailTemplate.From);
                myCommand.Parameters.AddWithValue("@To", emailTemplate.To);
                myCommand.Parameters.AddWithValue("@Cc", emailTemplate.CC);
                myCommand.Parameters.AddWithValue("@Bcc", emailTemplate.BCC);
                myCommand.Parameters.AddWithValue("@Subject", emailTemplate.Subject);

                myConnection.Open();
                myCommand.ExecuteNonQuery();
                myConnection.Close();
            }
        }

        #endregion

        #region Email
        // @Priority int
        //,@BodyFormat int
        //,@From nvarchar(256)
        //,@To nvarchar(256)
        //,@Cc nvarchar(256)
        //,@Bcc nvarchar(256)
        //,@Subject nvarchar(256)
        //,@Body nvarchar(max)
        //,@NextTryTime datetime
        //,@NumberOfTries int
        //,@IsSend int
        //,@RelatedUserId uniqueidentifier
        public int InsertEmail(Email obj)
        {
            int emailId;
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(dbOwner + ".sf_InsertEmail", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;

                myCommand.Parameters.AddWithValue("@Priority", obj.Priority);
                myCommand.Parameters.AddWithValue("@BodyFormat", EnumHelper.EnumToInteger(obj.BodyFormat));
                myCommand.Parameters.AddWithValue("@From", obj.From);
                myCommand.Parameters.AddWithValue("@To", obj.To);
                myCommand.Parameters.AddWithValue("@Cc", obj.CC);
                myCommand.Parameters.AddWithValue("@Bcc", obj.BCC);
                myCommand.Parameters.AddWithValue("@Subject", obj.Subject);
                myCommand.Parameters.AddWithValue("@Body", obj.Body);
                myCommand.Parameters.AddWithValue("@NextTryTime", obj.NextTryTime);
                myCommand.Parameters.AddWithValue("@NumberOfTries", obj.NumberOfTries);
                myCommand.Parameters.AddWithValue("@IsSend", obj.IsSend);
                myCommand.Parameters.AddWithValue("@RelatedUserId", obj.RelatedUserId);

                myConnection.Open();
                //myCommand.ExecuteNonQuery();
                emailId = Convert.ToInt32(myCommand.ExecuteScalar());
                myConnection.Close();
            }
            return emailId;
        }

        public void UpdateEmail(Email obj)
        {
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(dbOwner + ".sf_UpdateEmail", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;

                myCommand.Parameters.AddWithValue("@EmailId", obj.EmailId);
                myCommand.Parameters.AddWithValue("@Priority", obj.Priority);
                myCommand.Parameters.AddWithValue("@BodyFormat", EnumHelper.EnumToInteger(obj.BodyFormat));
                myCommand.Parameters.AddWithValue("@From", obj.From);
                myCommand.Parameters.AddWithValue("@To", obj.To);
                myCommand.Parameters.AddWithValue("@Cc", obj.CC);
                myCommand.Parameters.AddWithValue("@Bcc", obj.BCC);
                myCommand.Parameters.AddWithValue("@Subject", obj.Subject);
                myCommand.Parameters.AddWithValue("@Body", obj.Body);
                myCommand.Parameters.AddWithValue("@NextTryTime", obj.NextTryTime);
                myCommand.Parameters.AddWithValue("@NumberOfTries", obj.NumberOfTries);
                myCommand.Parameters.AddWithValue("@IsSend", obj.IsSend);
                myCommand.Parameters.AddWithValue("@RelatedUserId", obj.RelatedUserId);

                myConnection.Open();
                myCommand.ExecuteNonQuery();
                myConnection.Close();
            }
        }
        public void DeleteEmail(int id)
        {
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(dbOwner + ".sf_DeleteEmailById", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("@EmailId", id);
                myConnection.Open();
                myCommand.ExecuteNonQuery();
                myConnection.Close();
            }
        }

        public Email GetEmailById(int emailId)
        {
            Email email = null;
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(dbOwner + ".sf_SelectEmailById", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("@EmailId", emailId);
                myConnection.Open();

                using (SqlDataReader reader = myCommand.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (reader.Read())
                    {
                        email = new Email();

                        email.EmailId = Convert.ToInt32(reader["EmailId"]);
                        email.Priority = Convert.ToInt32(reader["Priority"]);
                        email.BodyFormat = (EmailFormatEnum)Convert.ToInt32(reader["BodyFormat"]);
                        email.From = (string)reader["From"];
                        email.To = (string)reader["To"];
                        email.CC = (string)reader["Cc"];
                        email.BCC = (string)reader["Bcc"];
                        email.Subject = (string)reader["Subject"];
                        email.Body = (string)reader["Body"];
                        email.NextTryTime = (DateTime)reader["NextTryTime"];
                        email.NumberOfTries = Convert.ToInt32(reader["NumberOfTries"]);
                        email.IsSend = Convert.ToBoolean(reader["IsSend"]);
                        email.RelatedUserId = (Guid)reader["RelatedUserId"];

                        reader.Close();
                    }

                }
                myConnection.Close();
            }
            return email;
        }
        #endregion

        #region Feedback
        // @InterviewerId uniqueidentifier
        //,@ApplicantId uniqueidentifier
        //,@InterviewId int
        //,@SuggestionResult int
        //,@FeedbackContent nvarchar(4000)
        //,@InterviewDate datetime
        //,@IsComplete bit
        //,@DueDate datetime
        //,@InterviewerAlias nvarchar(256)
        public int InsertFeedback(Feedback obj)
        {
            int feedbackId;
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(dbOwner + ".sf_InsertFeedback", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;

                myCommand.Parameters.AddWithValue("@InterviewerId", obj.InterviewerId);
                myCommand.Parameters.AddWithValue("@ApplicantId", obj.ApplicantId);
                myCommand.Parameters.AddWithValue("@InterviewId", obj.InterviewId);
                myCommand.Parameters.AddWithValue("@SuggestionResult", EnumHelper.EnumToInteger(obj.Suggestion));
                myCommand.Parameters.AddWithValue("@FeedbackContent", obj.FeedbackContent);
                myCommand.Parameters.AddWithValue("@InterviewDate", obj.InterviewDate);
                myCommand.Parameters.AddWithValue("@IsComplete", Convert.ToInt32(obj.IsComplete));
                myCommand.Parameters.AddWithValue("@DueDate", obj.DueDate);
                myCommand.Parameters.AddWithValue("@InterviewerAlias", obj.InterviewerAlias);

                myConnection.Open();
                //myCommand.ExecuteNonQuery();
                feedbackId = Convert.ToInt32(myCommand.ExecuteScalar());
                myConnection.Close();
            }
            return feedbackId;
        }

        public void UpdateFeedback(Feedback obj)
        {
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(dbOwner + ".sf_UpdateFeedback", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;

                myCommand.Parameters.AddWithValue("@FeedbackId", obj.FeedBackId);
                myCommand.Parameters.AddWithValue("@InterviewerId", obj.InterviewerId);
                myCommand.Parameters.AddWithValue("@ApplicantId", obj.ApplicantId);
                myCommand.Parameters.AddWithValue("@InterviewId", obj.InterviewId);
                myCommand.Parameters.AddWithValue("@SuggestionResult", EnumHelper.EnumToInteger(obj.Suggestion));
                myCommand.Parameters.AddWithValue("@FeedbackContent", obj.FeedbackContent);
                myCommand.Parameters.AddWithValue("@InterviewDate", obj.InterviewDate);
                myCommand.Parameters.AddWithValue("@IsComplete", Convert.ToInt32(obj.IsComplete));
                myCommand.Parameters.AddWithValue("@DueDate", obj.DueDate);
                myCommand.Parameters.AddWithValue("@InterviewerAlias", obj.InterviewerAlias);

                myConnection.Open();
                myCommand.ExecuteNonQuery();
                myConnection.Close();
            }
        }

        public void DeleteFeedback(int id)
        {
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(dbOwner + ".sf_DeleteFeedbackById", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("@FeedbackId", id);
                myConnection.Open();
                myCommand.ExecuteNonQuery();
                myConnection.Close();
            }
        }

        public Feedback GetFeedbackById(int id)
        {
            Feedback fb = null;
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(dbOwner + ".sf_SelectFeedbackById", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("@FeedbackId", id);

                myConnection.Open();

                using (SqlDataReader reader = myCommand.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (reader.Read())
                    {
                        fb = new Feedback();

                        fb.FeedBackId = Convert.ToInt32(reader["FeedbackId"]);
                        fb.InterviewerId = (Guid)reader["InterviewerId"];
                        fb.ApplicantId = (Guid)reader["ApplicantId"];
                        fb.InterviewId = Convert.ToInt32(reader["InterviewId"]);
                        fb.Suggestion = (FeedbackSuggestionEnum)Convert.ToInt32(reader["SuggestionResult"]);
                        fb.FeedbackContent = (string)reader["FeedbackContent"];
                        fb.InterviewDate = (DateTime)reader["InterviewDate"];
                        fb.IsComplete = Convert.ToBoolean(reader["IsComplete"]);
                        fb.DueDate = (DateTime)reader["DueDate"];
                        fb.InterviewerAlias = Convert.ToString(reader["InterviewerAlias"]);

                        reader.Close();
                    }
                }

                myConnection.Close();
            }
            return fb;
        }

        public DataSet GetFeedbackByInterview(int interviewId, bool isComplete)
        {
            DataSet dsFeedbacks;
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(dbOwner + ".sf_GetFeedbackByInterview", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("@InterviewId", interviewId);
                myCommand.Parameters.AddWithValue("@IsComplete", Convert.ToInt32(isComplete));

                SqlDataAdapter da = new SqlDataAdapter(myCommand);
                myConnection.Open();
                dsFeedbacks = new DataSet("AllFeedbacks");
                da.Fill(dsFeedbacks, "Feedbacks");
                myConnection.Close();
            }
            return dsFeedbacks;
        }

        public DataSet GetIncompleteFeedbackByAlias(string alias)
        {
            DataSet dsFeedbacks;
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(dbOwner + ".sf_GetIncompleteFeedbackForUser", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("@InterviewerAlias", alias);

                SqlDataAdapter da = new SqlDataAdapter(myCommand);
                myConnection.Open();
                dsFeedbacks = new DataSet("AllFeedbacks");
                da.Fill(dsFeedbacks, "Feedbacks");
                myConnection.Close();
            }
            return dsFeedbacks;

        }

        public Boolean CheckInterviewSameAsMentor(Int32 interviewId, String mentorAlias)
        {
            DataSet dsFeedbacks;
            using (SqlConnection myConnection = GetSqlConnection())
            {
                String strSql = "SELECT * FROM [dbo].[sf_Feedback] WHERE [InterviewId] = @InterviewId";
                SqlCommand myCommand = new SqlCommand(strSql, myConnection);
                myCommand.CommandType = CommandType.Text;
                myCommand.Parameters.AddWithValue("@InterviewId", interviewId);

                SqlDataAdapter da = new SqlDataAdapter(myCommand);
                myConnection.Open();
                dsFeedbacks = new DataSet("AllFeedbacks");
                da.Fill(dsFeedbacks, "Feedbacks");
                myConnection.Close();
            }

            if (dsFeedbacks.Tables[0].Rows.Count == 1)
            {
                String tempAlias = dsFeedbacks.Tables[0].Rows[0]["InterviewerAlias"].ToString();
                if (mentorAlias == tempAlias)
                {
                    return true;
                }
                return false;
            }

            return false;
        }
        #endregion

        #region Interview
        // @StartDate datetime
        //,@InterviewStatus int
        //,@EndDate datetime
        //,@HiringManagerId uniqueidentifier
        //,@HiringManagerResult int
        //,@HiringManagerComment nvarchar(4000)
        //,@GroupManagerId uniqueidentifier
        //,@GroupManagerResult int
        //,@GroupManagerComment nvarchar(4000)
        //,@ApplicantId uniqueidentifier
        public int InsertInterview(Interview obj)
        {
            int interviewId;
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(dbOwner + ".sf_InsertInterview", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;

                myCommand.Parameters.AddWithValue("@StartDate", obj.StartDate);
                myCommand.Parameters.AddWithValue("@InterviewStatus", EnumHelper.EnumToInteger(obj.InterviewStatus));
                myCommand.Parameters.AddWithValue("@EndDate", obj.EndDate);
                myCommand.Parameters.AddWithValue("@HiringManagerId", obj.HiringManagerId);
                myCommand.Parameters.AddWithValue("@HiringManagerResult", Convert.ToInt32(obj.HiringManagerResult));
                myCommand.Parameters.AddWithValue("@HiringManagerComment", obj.HiringManagerComment);
                myCommand.Parameters.AddWithValue("@GroupManagerId", obj.GroupManagerId);
                myCommand.Parameters.AddWithValue("@GroupManagerResult", Convert.ToInt32(obj.GroupManagerResult));
                myCommand.Parameters.AddWithValue("@GroupManagerComment", obj.GroupManagerComment);
                myCommand.Parameters.AddWithValue("@ApplicantId", obj.ApplicantId);
                myCommand.Parameters.AddWithValue("@CheckInFormId", obj.CheckInFormId);

                #region for uploading Email Approval. Added by Yi Shao at 2009-06-04
                myCommand.Parameters.AddWithValue("@GMApprovalDocId", obj.GMApproval.DocId);
                myCommand.Parameters.AddWithValue("@GMApprovalExt", obj.GMApprovalExt);
                myCommand.Parameters.AddWithValue("@MentorApprovalDocId", obj.MentorApproval.DocId);
                myCommand.Parameters.AddWithValue("@MentorApprovalExt", obj.MentorApprovalExt);

                /*
                 * Author: Yin.P
                 * Date:2010-1-5
                 */
                myCommand.Parameters.AddWithValue("@MentorAlias", obj.MentorAlias);

                #endregion
                myConnection.Open();
                //myCommand.ExecuteNonQuery();
                interviewId = Convert.ToInt32(myCommand.ExecuteScalar());
                myConnection.Close();
            }
            return interviewId;
        }

        public void UpdateInterview(Interview obj)
        {
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(dbOwner + ".sf_UpdateInterview", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;

                myCommand.Parameters.AddWithValue("@InterviewId", obj.InterviewId);
                myCommand.Parameters.AddWithValue("@StartDate", obj.StartDate);
                myCommand.Parameters.AddWithValue("@InterviewStatus", EnumHelper.EnumToInteger(obj.InterviewStatus));
                myCommand.Parameters.AddWithValue("@EndDate", obj.EndDate);
                myCommand.Parameters.AddWithValue("@HiringManagerId", obj.HiringManagerId);
                myCommand.Parameters.AddWithValue("@HiringManagerResult", Convert.ToInt32(obj.HiringManagerResult));
                myCommand.Parameters.AddWithValue("@HiringManagerComment", obj.HiringManagerComment);
                myCommand.Parameters.AddWithValue("@GroupManagerId", obj.GroupManagerId);
                myCommand.Parameters.AddWithValue("@GroupManagerResult", Convert.ToInt32(obj.GroupManagerResult));
                myCommand.Parameters.AddWithValue("@GroupManagerComment", obj.GroupManagerComment);
                myCommand.Parameters.AddWithValue("@MentorDecisionTime", obj.MentorDecisionTime);
                myCommand.Parameters.AddWithValue("@ManagerDecisionTime", obj.ManagerDecisionTime);
                myCommand.Parameters.AddWithValue("@ApplicantId", obj.ApplicantId);
                myCommand.Parameters.AddWithValue("@CheckInFormId", obj.CheckInFormId);

                #region for uploading Email Approval. Added by Yi Shao at 2009-06-04
                myCommand.Parameters.AddWithValue("@GMApprovalDocId", obj.GMApproval.DocId);
                myCommand.Parameters.AddWithValue("@GMApprovalExt", obj.GMApprovalExt);
                myCommand.Parameters.AddWithValue("@MentorApprovalDocId", obj.MentorApproval.DocId);
                myCommand.Parameters.AddWithValue("@MentorApprovalExt", obj.MentorApprovalExt);
                #endregion

                myConnection.Open();
                myCommand.ExecuteNonQuery();
                myConnection.Close();
            }
        }

        public void DeleteInterview(int id)
        {
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(dbOwner + ".sf_DeleteInterviewById", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("@InterviewId", id);
                myConnection.Open();
                myCommand.ExecuteNonQuery();
                myConnection.Close();
            }
        }

        public Interview GetInterviewById(int interviewId)
        {
            Interview interview = null;
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(dbOwner + ".sf_SelectInterviewById", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("@InterviewId", interviewId);

                myConnection.Open();

                using (SqlDataReader reader = myCommand.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (reader.Read())
                    {
                        interview = new Interview();

                        interview.InterviewId = Convert.ToInt32(reader["InterviewId"]);
                        interview.StartDate = (DateTime)reader["StartDate"];
                        interview.InterviewStatus = (InterviewStatusEnum)Convert.ToInt32(reader["InterviewStatus"]);
                        interview.EndDate = (DateTime)reader["EndDate"];
                        interview.HiringManagerId = (Guid)reader["HiringManagerId"];
                        interview.HiringManagerResult = Convert.ToBoolean(reader["HiringManagerResult"]);
                        interview.HiringManagerComment = (string)reader["HiringManagerComment"];
                        interview.GroupManagerId = (Guid)reader["GroupManagerId"];
                        interview.GroupManagerResult = Convert.ToBoolean(reader["GroupManagerResult"]);
                        interview.GroupManagerComment = (string)reader["GroupManagerComment"];
                        /*
                        * Modify Interview Process
                        * Author: Yin.P
                        * Date: 2010-1-5
                        */
                        interview.MentorAlias = reader["MentorAlias"].ToString();
                        interview.ApplicantId = (Guid)reader["ApplicantId"];
                        if (reader["GMApprovalDocId"].ToString() != "" && reader["GMApprovalDocId"].ToString() != String.Empty)
                            interview.GMApproval = Document.GetDocumentById(Convert.ToInt32(reader["GMApprovalDocId"]));
                        else
                            interview.GMApproval = new Document();
                        interview.GMApprovalExt = Convert.ToString(reader["GMApprovalExt"]);
                        if (reader["MentorApprovalDocId"].ToString() != "" && reader["MentorApprovalDocId"].ToString() != String.Empty)
                            interview.MentorApproval = Document.GetDocumentById(Convert.ToInt32(reader["MentorApprovalDocId"]));
                        else
                            interview.MentorApproval = new Document();
                        interview.MentorApprovalExt = Convert.ToString(reader["MentorApprovalExt"]);

                        if (reader["CheckInFormId"] == System.DBNull.Value)
                        {
                            interview.CheckInFormId = 0;
                        }
                        else
                        {
                            interview.CheckInFormId = Convert.ToInt32(reader["CheckInFormId"]);
                        }

                        /*
                         * TODO: Modify Interview Process
                         * Author: Yin.P
                         * Date: 2010-1-5
                         */
                        interview.MentorAlias = reader["MentorAlias"].ToString();

                        //CR:get the Time of making decision.
                        if (reader["MentorDecisionTime"].ToString() != "")
                            interview.MentorDecisionTime = (DateTime)reader["MentorDecisionTime"];
                        if (reader["ManagerDecisionTime"].ToString() != "")
                            interview.ManagerDecisionTime = (DateTime)reader["ManagerDecisionTime"];
                        reader.Close();
                    }
                }

                myConnection.Close();
            }
            return interview;
        }

        public Interview GetCurrentInterview(Guid applicantId, Guid hiringManagerId)
        {
            Interview interview = null;
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(dbOwner + ".sf_GetCurrentInterview", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("@ApplicantId", applicantId);
                myCommand.Parameters.AddWithValue("@HiringManagerId", hiringManagerId);

                myConnection.Open();

                using (SqlDataReader reader = myCommand.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (reader.Read())
                    {
                        interview = new Interview();
                        interview.InterviewId = Convert.ToInt32(reader["InterviewId"]);
                        interview.StartDate = (DateTime)reader["StartDate"];
                        interview.InterviewStatus = (InterviewStatusEnum)Convert.ToInt32(reader["InterviewStatus"]);
                        interview.EndDate = (DateTime)reader["EndDate"];
                        interview.HiringManagerId = (Guid)reader["HiringManagerId"];
                        interview.HiringManagerResult = Convert.ToBoolean(reader["HiringManagerResult"]);
                        interview.HiringManagerComment = (string)reader["HiringManagerComment"];
                        interview.GroupManagerId = (Guid)reader["GroupManagerId"];
                        interview.GroupManagerResult = Convert.ToBoolean(reader["GroupManagerResult"]);
                        interview.GroupManagerComment = (string)reader["GroupManagerComment"];
                        interview.ApplicantId = (Guid)reader["ApplicantId"];

                        if (reader["GMApprovalDocId"].ToString() != "" && reader["GMApprovalDocId"].ToString() != String.Empty)
                            interview.GMApproval = Document.GetDocumentById(Convert.ToInt32(reader["GMApprovalDocId"]));
                        else
                            interview.GMApproval = new Document();
                        interview.GMApprovalExt = Convert.ToString(reader["GMApprovalExt"]);
                        if (reader["MentorApprovalDocId"].ToString() != "" && reader["MentorApprovalDocId"].ToString() != String.Empty)
                            interview.MentorApproval = Document.GetDocumentById(Convert.ToInt32(reader["MentorApprovalDocId"]));
                        else
                            interview.MentorApproval = new Document();
                        interview.MentorApprovalExt = Convert.ToString(reader["MentorApprovalExt"]);

                        if (reader["CheckInFormId"] == System.DBNull.Value)
                        {
                            interview.CheckInFormId = 0;
                        }
                        else
                        {
                            interview.CheckInFormId = Convert.ToInt32(reader["CheckInFormId"]);
                        }
                    }

                    reader.Close();
                }

                myConnection.Close();
            }
            return interview;
        }

        public DataSet GetInterviewForCurrentUser(Guid hiringManagerId, String alias)
        {
            DataSet dsInterview;
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(dbOwner + ".sf_GetInterviewForCurrentUser", myConnection);
                SqlDataAdapter da = new SqlDataAdapter(myCommand);
                myCommand.CommandType = CommandType.StoredProcedure;

                myCommand.Parameters.AddWithValue("@HiringManagerId", hiringManagerId);
                myCommand.Parameters.AddWithValue("@alias", alias);

                myConnection.Open();
                dsInterview = new DataSet("AllInterview");
                da.Fill(dsInterview, "Interviews");
                myConnection.Close();
            }
            return dsInterview;
        }

        public DataSet GetInterviewForSiteUser(Guid hiringManagerId)
        {
            DataSet dsInterview;
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(dbOwner + ".sf_GetInterviewForSiteUser", myConnection);
                SqlDataAdapter da = new SqlDataAdapter(myCommand);
                myCommand.CommandType = CommandType.StoredProcedure;

                myCommand.Parameters.AddWithValue("@HiringManagerId", hiringManagerId);

                myConnection.Open();
                dsInterview = new DataSet("SiteUserInterviews");
                da.Fill(dsInterview, "Interviews");
                myConnection.Close();
            }
            return dsInterview;
        }


        public DataSet GetDurationInterviewedSiteUser(DateTime startDate, DateTime endDate)
        {
            DataSet dsInterview;
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(dbOwner + ".sf_GetDurationInterviewedSiteUser", myConnection);
                SqlDataAdapter da = new SqlDataAdapter(myCommand);
                myCommand.CommandType = CommandType.StoredProcedure;

                myCommand.Parameters.AddWithValue("@StartDate", startDate);
                myCommand.Parameters.AddWithValue("@EndDate", endDate);

                myConnection.Open();
                dsInterview = new DataSet("InterviewedSiteUsers");
                da.Fill(dsInterview, "SiteUsers");
                myConnection.Close();
            }
            return dsInterview;
        }

        public DataSet GetDurationInterview(DateTime startDate, DateTime endDate)
        {
            DataSet dsInterview;
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(dbOwner + ".sf_GetDurationInterview", myConnection);
                SqlDataAdapter da = new SqlDataAdapter(myCommand);
                myCommand.CommandType = CommandType.StoredProcedure;

                myCommand.Parameters.AddWithValue("@StartDate", startDate);
                myCommand.Parameters.AddWithValue("@EndDate", endDate);

                myConnection.Open();
                dsInterview = new DataSet("InterviewedSiteUsers");
                da.Fill(dsInterview, "SiteUsers");
                myConnection.Close();
            }
            return dsInterview;
        }

        public DataSet GetAllProcessingInterview()
        {
            DataSet dsInterview;
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(dbOwner + ".sf_GetAllProcessingInterview", myConnection);
                SqlDataAdapter da = new SqlDataAdapter(myCommand);
                myCommand.CommandType = CommandType.StoredProcedure;

                myConnection.Open();
                dsInterview = new DataSet("InterviewedSiteUsers");
                da.Fill(dsInterview, "SiteUsers");
                myConnection.Close();
            }
            return dsInterview;
        }

        public string GetRecentInterviewStatus(Guid applicantId)
        {
            string status;
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(dbOwner + ".sf_GetRecentInterviewStatus", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;

                myCommand.Parameters.AddWithValue("@ApplicantId", applicantId);

                myConnection.Open();

                using (SqlDataReader myReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (myReader.Read())
                    {
                        try
                        {
                            status = StaticData.InterviewStatusDict[(InterviewStatusEnum)myReader["InterviewStatus"]];
                        }
                        catch
                        {
                            status = "N/A";
                        }
                    }
                    else
                    {
                        status = "N/A";
                    }
                    myReader.Close();
                }
                myConnection.Close();
            }
            return status;
        }

        public string GetRecentInterviewDateTime(Guid applicantId)
        {
            string date = "N/A";
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(dbOwner + ".sf_GetRecentInterviewStatus", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;

                myCommand.Parameters.AddWithValue("@ApplicantId", applicantId);

                myConnection.Open();

                using (SqlDataReader myReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (myReader.Read())
                    {
                        date = (Convert.ToDateTime(myReader["StartDate"])).ToShortDateString();
                    }
                    else
                    {
                        date = "N/A";
                    }
                    myReader.Close();
                }
                myConnection.Close();
            }
            return date;
        }

        public void DeleteIncompleteFeedbackForInterview(int interviewId)
        {
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(dbOwner + ".sf_DeleteIncompleteFeedbackForInterview", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("@InterviewId", interviewId);
                myConnection.Open();
                myCommand.ExecuteNonQuery();
                myConnection.Close();
            }
        }

        public string GetRecentInterviewIdByApplicant(Guid applicantId)
        {
            string InterviewId = "";
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(dbOwner + ".sf_GetRecentInterviewIdByApplicant", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("@applicantId", applicantId);
                myConnection.Open();
                using (SqlDataReader myReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (myReader.Read())
                    {
                        InterviewId = myReader["InterviewId"].ToString();
                    }
                    myReader.Close();
                }
                myConnection.Close();
            }
            return InterviewId;
        }


        public DataSet GetInterviewForApplicant(Guid applicantId)
        {

            DataSet dsInterview;
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(dbOwner + ".sf_GetInterviewForApplicant", myConnection);
                SqlDataAdapter da = new SqlDataAdapter(myCommand);
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("@ApplicantId", applicantId);
                myConnection.Open();
                dsInterview = new DataSet("Interview");
                da.Fill(dsInterview, "Interview");
                myConnection.Close();
            }
            return dsInterview;
        }
        public string GetInterviewIdByApplicant(Guid applicantId)
        {
            string InterviewId = "";
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(dbOwner + ".sf_GetInterviewIdByApplicant", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("@applicantId", applicantId);
                myConnection.Open();
                using (SqlDataReader myReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (myReader.Read())
                    {
                        InterviewId = myReader["InterviewId"].ToString();
                    }
                    myReader.Close();
                }
                myConnection.Close();
            }
            return InterviewId;
        }

        public Boolean UpdateDecisionMailStatus(Int32 interviewId)
        {
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(dbOwner + ".sf_UpdateDecisionMailStatus", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("@interviewId", interviewId);
                myConnection.Open();
                myCommand.ExecuteNonQuery();
                myConnection.Close();
            }
            return true;
        }
        #endregion

        #region Document
        // @DocumentType int
        //,@OriginalName nvarchar(256)
        //,@ApplicantId uniqueidentifier
        //,@FileName nvarchar(256)
        public int InsertDocument(Document obj)
        {
            int docId;
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(dbOwner + ".sf_InsertDocument", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;

                myCommand.Parameters.AddWithValue("@DocumentType", EnumHelper.EnumToInteger(obj.DocType));
                myCommand.Parameters.AddWithValue("@OriginalName", obj.OriginalName);
                myCommand.Parameters.AddWithValue("@ApplicantId", obj.ApplicantId);
                myCommand.Parameters.AddWithValue("@FileName", obj.SaveName);

                myConnection.Open();
                //myCommand.ExecuteNonQuery();
                docId = Convert.ToInt32(myCommand.ExecuteScalar());
                myConnection.Close();
            }
            return docId;
        }

        public void UpdateDocument(Document obj)
        {
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(dbOwner + ".sf_UpdateDocument", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;

                myCommand.Parameters.AddWithValue("@DocumentId", obj.DocId);
                myCommand.Parameters.AddWithValue("@DocumentType", EnumHelper.EnumToInteger(obj.DocType));
                myCommand.Parameters.AddWithValue("@OriginalName", obj.OriginalName);
                myCommand.Parameters.AddWithValue("@ApplicantId", obj.ApplicantId);
                myCommand.Parameters.AddWithValue("@FileName", obj.SaveName);

                myConnection.Open();
                myCommand.ExecuteNonQuery();
                myConnection.Close();
            }
        }

        public void DeleteDocument(int docId)
        {
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(dbOwner + ".sf_DeleteDocumentById", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("@DocumentId", docId);
                myConnection.Open();
                myCommand.ExecuteNonQuery();
                myConnection.Close();
            }
        }

        public Document GetDocumentById(int docId)
        {
            //Document doc = null;
            Document doc = new Document();
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(dbOwner + ".sf_SelectDocumentById", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("@DocumentId", docId);

                myConnection.Open();

                using (SqlDataReader reader = myCommand.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (reader.Read())
                    {
                        //doc = new Document();

                        doc.DocId = Convert.ToInt32(reader["DocumentId"]);
                        doc.ApplicantId = (Guid)reader["ApplicantId"];
                        doc.DocType = (DocumentEnum)Convert.ToInt32(reader["DocumentType"]);
                        doc.OriginalName = (string)reader["OriginalName"];
                        doc.SaveName = (string)reader["FileName"];

                        reader.Close();
                    }
                }

                myConnection.Close();
            }
            return doc;
        }
        #endregion

        #region Favorite
        // @FavoriteId int
        //,@OwnerId uniqueidentifier
        //,@ApplicantId uniqueidentifier
        public int InsertFavorite(Favorite obj)
        {
            int favoriteId;
            using (SqlConnection myconnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(dbOwner + ".sf_InsertFavorite", myconnection);
                myCommand.CommandType = CommandType.StoredProcedure;

                //myCommand.Parameters.AddWithValue("@FavoriteId", EnumHelper.EnumToInteger(obj.FavoriteId));
                myCommand.Parameters.AddWithValue("@OwnerId", obj.OwnerId);
                myCommand.Parameters.AddWithValue("@ApplicantId", obj.ApplicantId);

                myconnection.Open();
                favoriteId = Convert.ToInt32(myCommand.ExecuteScalar());
                myconnection.Close();

            }
            return favoriteId;
        }

        public void UpdateFavorite(Favorite obj)
        {
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(dbOwner + ".sf_UpdateFavorite", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;

                myCommand.Parameters.AddWithValue("@FavoriteId", EnumHelper.EnumToInteger(obj.FavoriteId));
                myCommand.Parameters.AddWithValue("@OwnerId", obj.OwnerId);
                myCommand.Parameters.AddWithValue("@ApplicantId", obj.ApplicantId);

                myConnection.Open();
                myCommand.ExecuteNonQuery();
                myConnection.Close();
            }
        }

        public void DeleteFavorite(int favoriteId)
        {
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(dbOwner + ".sf_DeleteFavoriteById", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;

                myCommand.Parameters.AddWithValue("@FavoriteId", favoriteId);

                myConnection.Open();
                myCommand.ExecuteNonQuery();
                myConnection.Close();
            }
        }

        public Favorite GetFavoriteById(int favoriteId)
        {
            Favorite fav = null;
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(dbOwner + ".sf_SelectFavoriteById", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("@FavoriteId", favoriteId);

                myConnection.Open();

                using (SqlDataReader reader = myCommand.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (reader.Read())
                    {
                        fav = new Favorite();

                        fav.FavoriteId = Convert.ToInt32(reader["FavoriteId"]);
                        fav.OwnerId = (Guid)reader["OwnerId"];
                        fav.ApplicantId = (Guid)reader["ApplicantId"];

                        reader.Close();
                    }
                }
                myConnection.Close();
            }
            return fav;
        }

        public DataSet GetFavoritesByUserId(Guid userId)
        {
            DataSet dsFavorites;
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(dbOwner + ".sf_GetFavoriteForOwner", myConnection);
                SqlDataAdapter da = new SqlDataAdapter(myCommand);
                myCommand.CommandType = CommandType.StoredProcedure;

                myCommand.Parameters.AddWithValue("@OwnerId", userId);

                myConnection.Open();
                dsFavorites = new DataSet("AllFavorites");
                da.Fill(dsFavorites, "Favorites");
                myConnection.Close();
            }
            return dsFavorites;
        }

        public bool IsFavoriteExist(Guid ownerId, Guid applicantId)
        {
            bool isExist = false;
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(dbOwner + ".sf_IsFavoriteExist", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;

                myCommand.Parameters.AddWithValue("@OwnerId", ownerId);
                myCommand.Parameters.AddWithValue("@ApplicantId", applicantId);

                myConnection.Open();
                if (Convert.ToInt32(myCommand.ExecuteScalar()) > 0)
                {
                    isExist = true;
                }
                myConnection.Close();
            }
            return isExist;
        }

        public bool DeleteFavorite(Guid ownerId, Guid applicantId)
        {
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(dbOwner + ".sf_DeleteFavorite", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;

                myCommand.Parameters.AddWithValue("@ownerId", ownerId);
                myCommand.Parameters.AddWithValue("@applicantId", applicantId);
                myConnection.Open();
                myCommand.ExecuteNonQuery();
                myConnection.Close();
            }
            return true;
        }
        #endregion

        #region Referral
        //@ReferralType int
        //@ApplicantId uniqueidentifier
        //@ReferrerId uniqueidentifier
        public int InsertReferral(Referral obj)
        {
            int refId;
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(dbOwner + ".sf_InsertReferral", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;

                myCommand.Parameters.AddWithValue("@ReferralType", EnumHelper.EnumToInteger(obj.Type));
                myCommand.Parameters.AddWithValue("@ApplicantId", obj.ApplicantId);
                myCommand.Parameters.AddWithValue("@ReferrerId", obj.ReferrerId);
                myCommand.Parameters.AddWithValue("@ReferredTime", obj.ReferredTime);

                myConnection.Open();
                refId = Convert.ToInt32(myCommand.ExecuteScalar());
                myConnection.Close();
            }
            return refId;
        }

        public void UpdateReferral(Referral obj)
        {
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(dbOwner + ".sf_UpdateReferral", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;

                myCommand.Parameters.AddWithValue("@ReferralId", obj.ReferralId);
                myCommand.Parameters.AddWithValue("@ReferralType", EnumHelper.EnumToInteger(obj.Type));
                myCommand.Parameters.AddWithValue("@ApplicantId", obj.ApplicantId);
                myCommand.Parameters.AddWithValue("@ReferrerId", obj.ReferrerId);
                myCommand.Parameters.AddWithValue("@ReferredTime", obj.ReferredTime);

                myConnection.Open();
                myCommand.ExecuteNonQuery();
                myConnection.Close();

            }
        }

        public void DeleteReferral(int referralId)
        {
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(dbOwner + ".sf_DeleteReferralById", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;

                myCommand.Parameters.AddWithValue("@ReferralId", referralId);

                myConnection.Open();
                myCommand.ExecuteNonQuery();
                myConnection.Close();

            }
        }

        public Referral GetReferralById(int referralId)
        {
            Referral referral = null;
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(dbOwner + ".sf_SelectReferralById", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;

                myCommand.Parameters.AddWithValue("@ReferralId", referralId);

                myConnection.Open();

                using (SqlDataReader reader = myCommand.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (reader.Read())
                    {
                        referral = new Referral();
                        referral.ReferralId = Convert.ToInt32(reader["ReferralId"]);
                        referral.Type = (ReferralType)Convert.ToInt32(reader["ReferralType"]);
                        referral.ApplicantId = (Guid)reader["ApplicantId"];
                        referral.ReferrerId = (Guid)reader["ReferrerId"];
                        referral.ReferredTime = Convert.ToDateTime(reader["ReferredTime"]);
                        referral.Relaters = RelatedReferrer.GetRelatedReferrerForReferral(referral.ReferralId);

                        reader.Close();
                    }
                }

                myConnection.Close();
            }
            return referral;
        }
        #endregion

        #region RelatedReferrer
        //@ReferralId int
        //@Email nvarchar(256)
        //@LastName nvarchar(256)
        //@FirstName nvarchar(256)
        //@Gender int
        public int InsertRelatedReferrer(RelatedReferrer obj)
        {
            int relrefId = 0;
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(dbOwner + ".sf_InsertRelatedReferrer", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;

                myCommand.Parameters.AddWithValue("@ReferralId", obj.ReferralId);
                myCommand.Parameters.AddWithValue("@Email", obj.Email);
                myCommand.Parameters.AddWithValue("@LastName", obj.LastName);
                myCommand.Parameters.AddWithValue("@FirstName", obj.FirstName);
                myCommand.Parameters.AddWithValue("@Gender", obj.Gender);

                myConnection.Open();
                relrefId = Convert.ToInt32(myCommand.ExecuteScalar());
                myConnection.Close();
            }

            return relrefId;

        }
        public void UpdateRelatedReferrer(RelatedReferrer obj)
        {
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(dbOwner + ".sf_UpdateRelatedReferrer", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;

                myCommand.Parameters.AddWithValue("@RelatedReferrerId", obj.RelaterId);
                myCommand.Parameters.AddWithValue("@ReferralId", obj.ReferralId);
                myCommand.Parameters.AddWithValue("@LastName", obj.LastName);
                myCommand.Parameters.AddWithValue("@FirstName", obj.FirstName);
                myCommand.Parameters.AddWithValue("@Gender", obj.Gender);

                myConnection.Open();
                myCommand.ExecuteNonQuery();
                myConnection.Close();

            }
        }

        public void DeleteRelatedReferrer(int relatedrefferId)
        {
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(dbOwner + ".sf_DeleteRelatedReferrerById", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("@RelatedReferrerId", relatedrefferId);
                myConnection.Open();
                myCommand.ExecuteNonQuery();
                myConnection.Close();
            }
        }

        public RelatedReferrer GetRelatedReferrer(int relatedrefferId)
        {
            RelatedReferrer relref = null;

            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(dbOwner + ".sf_SelectRelatedReferrerById", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("@RelatedReferrerId", relatedrefferId);
                myConnection.Open();

                using (SqlDataReader reader = myCommand.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (reader.Read())
                    {
                        relref = new RelatedReferrer();

                        relref.RelaterId = Convert.ToInt32(reader["RelatedReferrerId"]);
                        relref.ReferralId = Convert.ToInt32(reader["ReferralId"]);
                        relref.Email = (string)reader["Email"];
                        relref.LastName = (string)reader["LastName"];
                        relref.FirstName = (string)reader["FirstName"];
                        relref.Gender = (GenderEnum)Convert.ToInt32(reader["Gender"]);

                        reader.Close();
                    }
                }
                myConnection.Close();
            }
            return relref;
        }

        public List<RelatedReferrer> GetRelatedReferrerForReferral(int referralId)
        {
            List<RelatedReferrer> list = new List<RelatedReferrer>();

            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(dbOwner + ".sf_SelectRelatedReferrerByReferralId", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("@ReferralId", referralId);
                myConnection.Open();

                RelatedReferrer temp;

                using (SqlDataReader reader = myCommand.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (reader.Read())
                    {
                        temp = new RelatedReferrer();
                        temp.RelaterId = Convert.ToInt32(reader["RelatedReferrerId"]);
                        temp.ReferralId = Convert.ToInt32(reader["ReferralId"]);
                        temp.Email = (string)reader["Email"];
                        temp.LastName = (string)reader["LastName"];
                        temp.FirstName = (string)reader["FirstName"];
                        temp.Gender = (GenderEnum)Convert.ToInt32(reader["Gender"]);
                        list.Add(temp);
                    }

                    reader.Close();
                }
                myConnection.Close();
            }
            return list;
        }
        #endregion

        #region SiteUser
        public Guid GetUserIdByFullName(string fullname)
        {
            Guid userId = Guid.Empty;
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(dbOwner + ".sf_GetUserIdByUserName", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("@UserName", fullname);
                myConnection.Open();
                //myCommand.ExecuteNonQuery();
                object id = myCommand.ExecuteScalar();
                if (id != null)
                {
                    userId = new Guid(Convert.ToString(id));
                }
                myConnection.Close();
            }
            return userId;
        }

        public string GetFullNameByUserId(Guid userId)
        {
            string fullName = string.Empty;
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(dbOwner + ".sf_GetFullNameByUserId", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("@UserId", userId);
                myConnection.Open();
                //myCommand.ExecuteNonQuery();
                fullName = Convert.ToString(myCommand.ExecuteScalar());
                myConnection.Close();
            }
            return fullName;
        }

        public string GetUserIdByAlias(string Alias)
        {
            string UserId = string.Empty;
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(dbOwner + ".sf_GetUserIdByAlias", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("@Alias", Alias);
                myConnection.Open();
                UserId = Convert.ToString(myCommand.ExecuteScalar());
                myConnection.Close();
            }
            return UserId;
        }
        #endregion

        #region SiteGeneralInfo
        public SiteGeneralInfo GetSiteGeneralInfo()
        {
            SiteGeneralInfo myInfo;
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(dbOwner + ".sf_GetGeneralInfo", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("@UserAlias", SiteUser.Current.Alias.ToLower());
                myCommand.Parameters.AddWithValue("@UserId", SiteUser.Current.SiteUserId);
                myConnection.Open();

                using (SqlDataReader myReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    myInfo = new SiteGeneralInfo();
                    myReader.Read();
                    myInfo.ApplicationCount = Convert.ToInt32(myReader["ApplicationCount"]);

                    myReader.NextResult();
                    myReader.Read();
                    myInfo.UserProcssingCount = Convert.ToInt32(myReader["ProcssingCount"]);

                    myReader.NextResult();
                    myReader.Read();
                    myInfo.UserFeedbackCount = Convert.ToInt32(myReader["UserFeedbackCount"]);

                    myReader.NextResult();
                    myReader.Read();
                    myInfo.IRDecisionCount = Convert.ToInt32(myReader["DecisionCount"]);

                    myReader.NextResult();
                    myReader.Read();
                    myInfo.CompleteFeedbackCount = Convert.ToInt32(myReader["CompleteFeedbackCount"]);

                    myReader.NextResult();
                    myReader.Read();
                    myInfo.IncompleteFeedbackCount = Convert.ToInt32(myReader["IncompleteFeedbackCount"]);

                    myReader.NextResult();
                    myReader.Read();
                    myInfo.HiredInterviewCount = Convert.ToInt32(myReader["HiredInterviewCount"]);

                    myReader.NextResult();
                    myReader.Read();
                    myInfo.RejectedInterviewCount = Convert.ToInt32(myReader["RejectedInterviewCount"]);

                    myReader.NextResult();
                    myReader.Read();
                    myInfo.CompleteInterviewCount = Convert.ToInt32(myReader["CompleteInterviewCount"]);
                }

                myConnection.Close();
            }
            return myInfo;
        }
        #endregion

        #region Comment
        public int InsertComment(Comment obj)
        {
            int commentId;
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(dbOwner + ".sf_InsertComment", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;

                myCommand.Parameters.AddWithValue("@CommentTime", obj.CommentTime);
                myCommand.Parameters.AddWithValue("@CommentContent", obj.CommentContent);
                myCommand.Parameters.AddWithValue("@ApplicantId", obj.ApplicantId);
                myCommand.Parameters.AddWithValue("@Commenter", obj.Commenter);
                myCommand.Parameters.AddWithValue("@CommenterAlias", obj.CommenterFullName);

                myConnection.Open();
                //myCommand.ExecuteNonQuery();
                commentId = Convert.ToInt32(myCommand.ExecuteScalar());
                myConnection.Close();
            }
            return commentId;
        }

        public void UpdateComment(Comment obj)
        {
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(dbOwner + ".sf_UpdateComment", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;

                myCommand.Parameters.AddWithValue("@CommentId", obj.CommentId);
                myCommand.Parameters.AddWithValue("@CommentTime", obj.CommentTime);
                myCommand.Parameters.AddWithValue("@Commenter", obj.Commenter);
                myCommand.Parameters.AddWithValue("@ApplicantId", obj.ApplicantId);
                myCommand.Parameters.AddWithValue("@CommenterAlias", obj.CommenterFullName);
                myCommand.Parameters.AddWithValue("@CommentContent", obj.CommentContent);

                myConnection.Open();
                myCommand.ExecuteNonQuery();
                myConnection.Close();
            }
        }

        public void DeleteComment(int commentId)
        {
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(dbOwner + ".sf_DeleteCommentById", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("@CommentId", commentId);
                myConnection.Open();
                myCommand.ExecuteNonQuery();
                myConnection.Close();
            }
        }

        public Comment GetCommentById(int commentId)
        {
            Comment comment = null;
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(dbOwner + ".sf_SelectCommentById", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("@CommentId", commentId);

                myConnection.Open();

                using (SqlDataReader reader = myCommand.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (reader.Read())
                    {
                        comment = new Comment();

                        comment.CommentId = Convert.ToInt32(reader["CommentId"]);
                        comment.ApplicantId = (Guid)reader["ApplicantId"];
                        comment.CommentTime = Convert.ToDateTime(reader["CommentTime"]);
                        comment.Commenter = (Guid)reader["Commenter"];
                        comment.CommentContent = (string)reader["CommentContent"];
                        comment.CommenterFullName = (string)reader["CommentAlias"];

                        reader.Close();
                    }
                }

                myConnection.Close();
            }
            return comment;
        }

        public DataSet GetCommentForApplicant(Guid applicantId)
        {
            DataSet dsComment;
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(dbOwner + ".sf_GetCommentForApplicant", myConnection);
                SqlDataAdapter da = new SqlDataAdapter(myCommand);
                myCommand.CommandType = CommandType.StoredProcedure;

                myCommand.Parameters.AddWithValue("@ApplicantId", applicantId);

                myConnection.Open();
                dsComment = new DataSet("AllComment");
                da.Fill(dsComment, "Comments");
                myConnection.Close();
            }
            return dsComment;

        }
        #endregion

        #region CheckInForm
        public int InsertCheckInForm(CheckInForm obj)
        {
            int commentId;
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(dbOwner + ".sf_InsertCheckInForm", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;

                myCommand.Parameters.AddWithValue("@GroupId", obj.GroupId);
                myCommand.Parameters.AddWithValue("@ProjectId", obj.ProjectId);
                myCommand.Parameters.AddWithValue("@PositionId", obj.PositionId);
                myCommand.Parameters.AddWithValue("@InternTypeId", obj.InternTypeId);
                myCommand.Parameters.AddWithValue("@MentorAlias", obj.MentorAlias);
                myCommand.Parameters.AddWithValue("@PreferCheckInDate", obj.PreferCheckInDate);
                myCommand.Parameters.AddWithValue("@PreferLastWorkingday", obj.PreferLastWorkingDay);
                myCommand.Parameters.AddWithValue("@AdvisorApproved", obj.AdvisorApproved);
                //myCommand.Parameters.AddWithValue("@EnrollDate", obj.EnrollDate);
                //myCommand.Parameters.AddWithValue("@GraduateDate", obj.GraduateDate);
                myCommand.Parameters.AddWithValue("@Comments", obj.Comments);

                myConnection.Open();
                //myCommand.ExecuteNonQuery();
                commentId = Convert.ToInt32(myCommand.ExecuteScalar());
                myConnection.Close();
            }
            return commentId;
        }

        public CheckInForm GetCheckInFormById(int formId)
        {
            CheckInForm form = null;
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(dbOwner + ".sf_SelectCheckInFormById", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("@FormId", formId);

                myConnection.Open();

                using (SqlDataReader reader = myCommand.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (reader.Read())
                    {
                        form = new CheckInForm();
                        form.FormId = Convert.ToInt32(reader["FormId"]);
                        form.GroupId = Convert.ToInt32(reader["GroupId"]);
                        form.ProjectId = Convert.ToInt32(reader["ProjectId"]);
                        form.PositionId = Convert.ToInt32(reader["PositionId"]);
                        form.InternTypeId = Convert.ToInt32(reader["InternTypeId"]);
                        form.MentorAlias = (string)reader["MentorAlias"];
                        form.PreferCheckInDate = Convert.ToDateTime(reader["PreferCheckInDate"]);
                        form.PreferLastWorkingDay = Convert.ToDateTime(reader["PreferLastWorkingDay"]);
                        form.AdvisorApproved = Convert.ToBoolean(reader["AdvisorApproved"]);
                        //form.EnrollDate = Convert.ToDateTime(reader["EnrollDate"]);
                        //form.GraduateDate = Convert.ToDateTime(reader["GraduateDate"]);
                        form.Comments = (string)(reader["Comments"]);
                        reader.Close();
                    }
                }

                myConnection.Close();
            }
            return form;
        }

        public void UpdateCheckInForm(CheckInForm obj)
        {
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(dbOwner + ".sf_UpdateCheckInForm", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("@FormId", obj.FormId);
                myCommand.Parameters.AddWithValue("@GroupId", obj.GroupId);
                myCommand.Parameters.AddWithValue("@PositionId", obj.PositionId);
                myCommand.Parameters.AddWithValue("@ProjectId", obj.ProjectId);
                myCommand.Parameters.AddWithValue("@InternTypeId", obj.InternTypeId);
                myCommand.Parameters.AddWithValue("@MentorAlias", obj.MentorAlias);
                myCommand.Parameters.AddWithValue("@PreferCheckInDate", obj.PreferCheckInDate);
                myCommand.Parameters.AddWithValue("@PreferLastWorkingday", obj.PreferLastWorkingDay);
                myCommand.Parameters.AddWithValue("@AdvisorApproved", obj.AdvisorApproved);
                //myCommand.Parameters.AddWithValue("@EnrollDate", obj.EnrollDate);
                //myCommand.Parameters.AddWithValue("@GraduateDate", obj.GraduateDate);
                myCommand.Parameters.AddWithValue("@Comments", obj.Comments);

                myConnection.Open();
                myCommand.ExecuteNonQuery();
                myConnection.Close();
            }
        }

        public DataSet GetAllCheckinFormforHiringReport(DateTime StartDate, DateTime EndDate)
        {
            DataSet returnDS = new DataSet();
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(dbOwner + ".Sf_GetAllCheckinFormforHiringReport", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("@StartDate", StartDate);
                myCommand.Parameters.AddWithValue("@EndDate", EndDate);
                myConnection.Open();
                SqlDataAdapter da = new SqlDataAdapter(myCommand);
                da.Fill(returnDS);
                myConnection.Close();
            }
            return returnDS;
        }
        #endregion

        #region PerformanceAssessment
        public Guid InsertPerformanceAssessment(PerformanceAssessment obj)
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection cn = GetSqlConnection();
            SqlParameter prmPAId;
            try
            {
                cmd.Connection = cn;
                cmd.CommandText = "[dbo].[sf_InsertPerformanceAssessment]";
                cmd.CommandType = CommandType.StoredProcedure;

                #region Populate Parameters
                prmPAId = cmd.Parameters.Add("@id", SqlDbType.UniqueIdentifier);
                prmPAId.Direction = ParameterDirection.Output;

                SqlParameter prmApplicantId = cmd.Parameters.Add("@ApplicantId", SqlDbType.UniqueIdentifier);
                prmApplicantId.Value = obj.ApplicantId;

                SqlParameter prmInternName = cmd.Parameters.Add("@InternName", SqlDbType.NChar);
                prmInternName.Size = 20;
                prmInternName.Value = obj.InternName;

                SqlParameter prmInternPhone = cmd.Parameters.Add("@InternPhone", SqlDbType.NChar);
                prmInternPhone.Size = 50;
                prmInternPhone.Value = obj.InternPhone;

                SqlParameter prmInternEmail = cmd.Parameters.Add("@InternEmail", SqlDbType.NChar);
                prmInternEmail.Size = 100;
                prmInternEmail.Value = obj.InternEmail;

                SqlParameter prmInternPosition = cmd.Parameters.Add("@InternPosition", SqlDbType.Int);
                prmInternPosition.Value = obj.InternPosition;

                SqlParameter prmCheckInDate = cmd.Parameters.Add("@CheckInDate", SqlDbType.DateTime);
                prmCheckInDate.Value = obj.CheckInDate;

                SqlParameter prmCheckOutDate = cmd.Parameters.Add("@CheckOutDate", SqlDbType.DateTime);
                prmCheckOutDate.Value = obj.CheckOutDate;

                SqlParameter prmGroupId = cmd.Parameters.Add("@GroupId", SqlDbType.Int);
                prmGroupId.Value = obj.GroupId;

                SqlParameter prmDepartment = cmd.Parameters.Add("@Department", SqlDbType.Int);
                prmDepartment.Value = obj.Department;

                SqlParameter prmMentorName = cmd.Parameters.Add("@MentorName", SqlDbType.NChar);
                prmMentorName.Size = 50;
                prmMentorName.Value = obj.MentorName;

                SqlParameter prmMentorAlias = cmd.Parameters.Add("@MentorAlias", SqlDbType.NVarChar);
                prmMentorAlias.Value = obj.MentorAlias;

                SqlParameter prmGroupMgrId = cmd.Parameters.Add("@GroupMgrId", SqlDbType.UniqueIdentifier);
                prmGroupMgrId.Value = obj.GroupMgrId;

                SqlParameter prmProjectId = cmd.Parameters.Add("@ProjectId", SqlDbType.Int);
                prmProjectId.Value = obj.ProjectId;

                SqlParameter prmGraduationDate = cmd.Parameters.Add("@GraduationDate", SqlDbType.DateTime);
                prmGraduationDate.Value = obj.GraduationDate;

                SqlParameter prmDiscipline = cmd.Parameters.Add("@Discipline", SqlDbType.Int);
                prmDiscipline.Value = obj.Discipline;

                SqlParameter prmPipeline = cmd.Parameters.Add("@Pipeline", SqlDbType.NChar);
                prmPipeline.Size = 100;
                prmPipeline.Value = obj.Pipeline;

                SqlParameter prmObjective = cmd.Parameters.Add("@Objective", SqlDbType.Text);
                prmObjective.Value = obj.Objective;

                SqlParameter prmSelfEvaluation = cmd.Parameters.Add("@SelfEvaluation", SqlDbType.Text);
                prmSelfEvaluation.Value = obj.SelfEvaluation;

                SqlParameter prmStrengthsAndWeaknesses = cmd.Parameters.Add("@StrengthsAndWeaknesses", SqlDbType.Text);
                prmStrengthsAndWeaknesses.Value = obj.StrengthsAndWeaknesses;

                SqlParameter prmOverrallEvaluation = cmd.Parameters.Add("@OverrallEvaluation", SqlDbType.Int);
                prmOverrallEvaluation.Value = obj.OverrallEvaluation;

                SqlParameter prmCodingSkill = cmd.Parameters.Add("@CodingSkill", SqlDbType.Int);
                prmCodingSkill.Value = obj.CodingSkill;

                SqlParameter prmAnalyticalSkill = cmd.Parameters.Add("@AnalyticalSkill", SqlDbType.Int);
                prmAnalyticalSkill.Value = obj.AnalyticalSkill;

                SqlParameter prmProblemSolving = cmd.Parameters.Add("@ProblemSolving", SqlDbType.Int);
                prmProblemSolving.Value = obj.ProblemSolving;

                SqlParameter prmInnovation = cmd.Parameters.Add("@Innovation", SqlDbType.Int);
                prmInnovation.Value = obj.Innovation;

                SqlParameter prmDrivingForResults = cmd.Parameters.Add("@DrivingForResults", SqlDbType.Int);
                prmDrivingForResults.Value = obj.DrivingForResults;

                SqlParameter prmDealingWithAmbiguity = cmd.Parameters.Add("@DealingWithAmbiguity", SqlDbType.Int);
                prmDealingWithAmbiguity.Value = obj.DealingWithAmbiguity;

                SqlParameter prmQuickOnLearing = cmd.Parameters.Add("@QuickOnLearing", SqlDbType.Int);
                prmQuickOnLearing.Value = obj.QuickOnLearing;

                SqlParameter prmEnglish = cmd.Parameters.Add("@English", SqlDbType.Int);
                prmEnglish.Value = obj.English;

                SqlParameter prmCommunication = cmd.Parameters.Add("@Communication", SqlDbType.Int);
                prmCommunication.Value = obj.Communication;

                SqlParameter prmTeamWork = cmd.Parameters.Add("@TeamWork", SqlDbType.Int);
                prmTeamWork.Value = obj.TeamWork;

                SqlParameter prmAttitude = cmd.Parameters.Add("@Attitude", SqlDbType.Int);
                prmAttitude.Value = obj.Attitude;

                SqlParameter prmIntegrityHonesty = cmd.Parameters.Add("@IntegrityHonesty", SqlDbType.Int);
                prmIntegrityHonesty.Value = obj.IntegrityHonesty;

                SqlParameter prmOpenRespectful = cmd.Parameters.Add("@OpenRespectful", SqlDbType.Int);
                prmOpenRespectful.Value = obj.OpenRespectful;

                SqlParameter prmBigChallenges = cmd.Parameters.Add("@BigChallenges", SqlDbType.Int);
                prmBigChallenges.Value = obj.BigChallenges;

                SqlParameter prmPassion = cmd.Parameters.Add("@Passion", SqlDbType.Int);
                prmPassion.Value = obj.Passion;

                SqlParameter prmAccountable = cmd.Parameters.Add("@Accountable", SqlDbType.Int);
                prmAccountable.Value = obj.Accountable;

                SqlParameter prmSelfCritical = cmd.Parameters.Add("@SelfCritical", SqlDbType.Int);
                prmSelfCritical.Value = obj.SelfCritical;

                SqlParameter prmMentorComments = cmd.Parameters.Add("@MentorComments", SqlDbType.Text);
                prmMentorComments.Value = obj.MentorComments;

                SqlParameter prmMentorStrength = cmd.Parameters.Add("@MentorStrength", SqlDbType.Text);
                prmMentorStrength.Value = obj.MentorStrength;

                SqlParameter prmMentorWeakness = cmd.Parameters.Add("@MentorWeakness", SqlDbType.Text);
                prmMentorWeakness.Value = obj.MentorWeakness;

                SqlParameter prmHiredAsFTE = cmd.Parameters.Add("@HiredAsFTE", SqlDbType.Int);
                prmHiredAsFTE.Value = obj.HiredAsFTE;

                SqlParameter prmOnsiteInterviewNow = cmd.Parameters.Add("@OnsiteInterviewNow", SqlDbType.Int);
                prmOnsiteInterviewNow.Value = obj.OnsiteInterviewNow;

                SqlParameter prmExpectedOnsiteInterviewDate = cmd.Parameters.Add("@ExpectedOnsiteInterviewDate", SqlDbType.DateTime);
                prmExpectedOnsiteInterviewDate.Value = obj.ExpectedOnsiteInterviewDate;

                /*
                 * PA Approval
                 * Author: Yin.P
                 * Date: 2010-1-5
                 */
                cmd.Parameters.AddWithValue("@IsApproved", obj.IsApproved);

                SqlParameter prmExtendPeriod = cmd.Parameters.Add("@ExtendPeriod", SqlDbType.Decimal);
                prmExtendPeriod.Precision = 4;
                prmExtendPeriod.Scale = 2;
                prmExtendPeriod.Value = obj.ExtendPeriod;
                #endregion

                #region Execute Command
                cn.Open();
                cmd.ExecuteNonQuery();
                #endregion
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                {
                    cn.Close();
                }

                cn.Dispose();
                cmd.Dispose();
            }

            return (Guid)prmPAId.Value;
        }

        public bool UpdatePerformanceAssessment(PerformanceAssessment obj)
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection cn = GetSqlConnection();
            bool isSuccess = false;
            try
            {
                cmd.Connection = cn;
                cmd.CommandText = "[dbo].[sf_UpdatePerformanceAssessment]";
                cmd.CommandType = CommandType.StoredProcedure;

                #region Populate Parameters
                SqlParameter prmid = cmd.Parameters.Add("@id", SqlDbType.UniqueIdentifier);
                prmid.Value = obj.Id;

                SqlParameter prmApplicantId = cmd.Parameters.Add("@ApplicantId", SqlDbType.UniqueIdentifier);
                prmApplicantId.Value = obj.ApplicantId;

                SqlParameter prmInternName = cmd.Parameters.Add("@InternName", SqlDbType.NChar);
                prmInternName.Size = 20;
                prmInternName.Value = obj.InternName;

                SqlParameter prmInternPhone = cmd.Parameters.Add("@InternPhone", SqlDbType.NChar);
                prmInternPhone.Size = 50;
                prmInternPhone.Value = obj.InternPhone;

                SqlParameter prmInternEmail = cmd.Parameters.Add("@InternEmail", SqlDbType.NChar);
                prmInternEmail.Size = 100;
                prmInternEmail.Value = obj.InternEmail;

                SqlParameter prmInternPosition = cmd.Parameters.Add("@InternPosition", SqlDbType.Int);
                prmInternPosition.Value = obj.InternPosition;

                SqlParameter prmCheckInDate = cmd.Parameters.Add("@CheckInDate", SqlDbType.DateTime);
                prmCheckInDate.Value = obj.CheckInDate;

                SqlParameter prmCheckOutDate = cmd.Parameters.Add("@CheckOutDate", SqlDbType.DateTime);
                prmCheckOutDate.Value = obj.CheckOutDate;

                SqlParameter prmGroupId = cmd.Parameters.Add("@GroupId", SqlDbType.Int);
                prmGroupId.Value = obj.GroupId;

                SqlParameter prmDepartment = cmd.Parameters.Add("@Department", SqlDbType.Int);
                prmDepartment.Value = obj.Department;

                SqlParameter prmMentorName = cmd.Parameters.Add("@MentorName", SqlDbType.NChar);
                prmMentorName.Size = 50;
                prmMentorName.Value = obj.MentorName;

                SqlParameter prmMentorAlias = cmd.Parameters.Add("@MentorAlias", SqlDbType.NVarChar);
                prmMentorAlias.Value = obj.MentorAlias;

                SqlParameter prmGroupMgrId = cmd.Parameters.Add("@GroupMgrId", SqlDbType.UniqueIdentifier);
                prmGroupMgrId.Value = obj.GroupMgrId;

                SqlParameter prmProjectId = cmd.Parameters.Add("@ProjectId", SqlDbType.Int);
                prmProjectId.Value = obj.ProjectId;

                SqlParameter prmGraduationDate = cmd.Parameters.Add("@GraduationDate", SqlDbType.DateTime);

                prmGraduationDate.Value = obj.GraduationDate;

                SqlParameter prmDiscipline = cmd.Parameters.Add("@Discipline", SqlDbType.Int);
                prmDiscipline.Value = obj.Discipline;

                SqlParameter prmPipeline = cmd.Parameters.Add("@Pipeline", SqlDbType.NChar);
                prmPipeline.Size = 100;
                prmPipeline.Value = obj.Pipeline;

                SqlParameter prmObjective = cmd.Parameters.Add("@Objective", SqlDbType.Text);
                prmObjective.Value = obj.Objective;

                SqlParameter prmSelfEvaluation = cmd.Parameters.Add("@SelfEvaluation", SqlDbType.Text);
                prmSelfEvaluation.Value = obj.SelfEvaluation;

                SqlParameter prmStrengthsAndWeaknesses = cmd.Parameters.Add("@StrengthsAndWeaknesses", SqlDbType.Text);
                prmStrengthsAndWeaknesses.Value = obj.StrengthsAndWeaknesses;

                SqlParameter prmOverrallEvaluation = cmd.Parameters.Add("@OverrallEvaluation", SqlDbType.Int);
                prmOverrallEvaluation.Value = obj.OverrallEvaluation;

                SqlParameter prmCodingSkill = cmd.Parameters.Add("@CodingSkill", SqlDbType.Int);
                prmCodingSkill.Value = obj.CodingSkill;

                SqlParameter prmAnalyticalSkill = cmd.Parameters.Add("@AnalyticalSkill", SqlDbType.Int);
                prmAnalyticalSkill.Value = obj.AnalyticalSkill;

                SqlParameter prmProblemSolving = cmd.Parameters.Add("@ProblemSolving", SqlDbType.Int);
                prmProblemSolving.Value = obj.ProblemSolving;

                SqlParameter prmInnovation = cmd.Parameters.Add("@Innovation", SqlDbType.Int);
                prmInnovation.Value = obj.Innovation;

                SqlParameter prmDrivingForResults = cmd.Parameters.Add("@DrivingForResults", SqlDbType.Int);
                prmDrivingForResults.Value = obj.DrivingForResults;

                SqlParameter prmDealingWithAmbiguity = cmd.Parameters.Add("@DealingWithAmbiguity", SqlDbType.Int);
                prmDealingWithAmbiguity.Value = obj.DealingWithAmbiguity;

                SqlParameter prmQuickOnLearing = cmd.Parameters.Add("@QuickOnLearing", SqlDbType.Int);
                prmQuickOnLearing.Value = obj.QuickOnLearing;

                SqlParameter prmEnglish = cmd.Parameters.Add("@English", SqlDbType.Int);
                prmEnglish.Value = obj.English;

                SqlParameter prmCommunication = cmd.Parameters.Add("@Communication", SqlDbType.Int);
                prmCommunication.Value = obj.Communication;

                SqlParameter prmTeamWork = cmd.Parameters.Add("@TeamWork", SqlDbType.Int);
                prmTeamWork.Value = obj.TeamWork;

                SqlParameter prmAttitude = cmd.Parameters.Add("@Attitude", SqlDbType.Int);
                prmAttitude.Value = obj.Attitude;

                SqlParameter prmIntegrityHonesty = cmd.Parameters.Add("@IntegrityHonesty", SqlDbType.Int);
                prmIntegrityHonesty.Value = obj.IntegrityHonesty;

                SqlParameter prmOpenRespectful = cmd.Parameters.Add("@OpenRespectful", SqlDbType.Int);
                prmOpenRespectful.Value = obj.OpenRespectful;

                SqlParameter prmBigChallenges = cmd.Parameters.Add("@BigChallenges", SqlDbType.Int);
                prmBigChallenges.Value = obj.BigChallenges;

                SqlParameter prmPassion = cmd.Parameters.Add("@Passion", SqlDbType.Int);
                prmPassion.Value = obj.Passion;

                SqlParameter prmAccountable = cmd.Parameters.Add("@Accountable", SqlDbType.Int);
                prmAccountable.Value = obj.Accountable;

                SqlParameter prmSelfCritical = cmd.Parameters.Add("@SelfCritical", SqlDbType.Int);
                prmSelfCritical.Value = obj.SelfCritical;

                SqlParameter prmMentorComments = cmd.Parameters.Add("@MentorComments", SqlDbType.Text);
                prmMentorComments.Value = obj.MentorComments;

                SqlParameter prmMentorStrength = cmd.Parameters.Add("@MentorStrength", SqlDbType.Text);
                prmMentorStrength.Value = obj.MentorStrength;

                SqlParameter prmMentorWeakness = cmd.Parameters.Add("@MentorWeakness", SqlDbType.Text);
                prmMentorWeakness.Value = obj.MentorWeakness;

                SqlParameter prmHiredAsFTE = cmd.Parameters.Add("@HiredAsFTE", SqlDbType.Int);
                prmHiredAsFTE.Value = obj.HiredAsFTE;

                SqlParameter prmOnsiteInterviewNow = cmd.Parameters.Add("@OnsiteInterviewNow", SqlDbType.Int);
                prmOnsiteInterviewNow.Value = obj.OnsiteInterviewNow;

                SqlParameter prmExpectedOnsiteInterviewDate = cmd.Parameters.Add("@ExpectedOnsiteInterviewDate", SqlDbType.DateTime);
                if (obj.ExpectedOnsiteInterviewDate.ToString().Equals("1/1/0001 12:00:00 AM"))
                {
                    prmExpectedOnsiteInterviewDate.Value = DateTime.MaxValue;
                }
                else
                {
                    prmExpectedOnsiteInterviewDate.Value = obj.ExpectedOnsiteInterviewDate;
                }

                /* 
                 * PA Approval
                 * Author: Yin.P
                 * Date: 2010-1-5
                 */
                cmd.Parameters.AddWithValue("@IsApproved", obj.IsApproved);

                SqlParameter prmExtendPeriod = cmd.Parameters.Add("@ExtendPeriod", SqlDbType.Decimal);
                prmExtendPeriod.Precision = 4;
                prmExtendPeriod.Scale = 2;
                prmExtendPeriod.Value = obj.ExtendPeriod;
                #endregion

                #region Execute Command
                cn.Open();
                cmd.ExecuteNonQuery();
                isSuccess = true;
                #endregion
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                {
                    cn.Close();
                }

                cn.Dispose();
                cmd.Dispose();
            }

            return isSuccess;
        }

        public bool DeletePerformanceAssessmentById(Guid id)
        {
            bool isSuccess = false;
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(dbOwner + ".sf_DeletePerformanceAssessmentById", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter prmid = myCommand.Parameters.Add("@id", SqlDbType.UniqueIdentifier);
                prmid.Value = id;
                myConnection.Open();
                myCommand.ExecuteNonQuery();
                myConnection.Close();
                isSuccess = true;
            }
            return isSuccess;
        }

        public PerformanceAssessment GetPerformanceAssessmentById(Guid id)
        {
            PerformanceAssessment PA = null;

            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(dbOwner + ".sf_GetPerformanceAssessmentById", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("@id", id);
                myConnection.Open();

                using (SqlDataReader reader = myCommand.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (reader != null  && !reader.IsClosed)
                    {
                        PA = new PerformanceAssessment();
                        reader.Read();
                        PA.Id = new Guid(reader["id"].ToString());
                        PA.ApplicantId = (Guid)reader["ApplicantId"];
                        PA.InternName = reader["InternName"].ParseString();
                        PA.InternPhone = reader["InternPhone"].ParseString();
                        PA.InternEmail = reader["InternEmail"].ParseString();
                        PA.InternPosition = Convert.ToInt32(reader["InternPosition"]);
                        PA.CheckInDate = (DateTime)reader["CheckInDate"];
                        PA.CheckOutDate = (DateTime)reader["CheckOutDate"];
                        PA.GroupId = (int)reader["GroupId"];
                        PA.Department = reader["Department"] == DBNull.Value ? 1 : int.Parse(reader["Department"].ToString());
                        PA.MentorName = Convert.ToString(reader["MentorName"]);
                        PA.MentorAlias = Convert.ToString(reader["MentorAlias"]);
                        PA.GroupMgrId = reader["GroupMgrId"].ToString().Length==0?Guid.Empty:new Guid(reader["GroupMgrId"].ToString());
                        PA.ProjectId = reader["ProjectId"] == DBNull.Value ? 0 : int.Parse(reader["ProjectId"].ToString());
                        PA.GraduationDate = reader["GraduationDate"].ParseDateTime();
                        PA.Discipline = reader["Discipline"] == DBNull.Value ? 0 : int.Parse(reader["Discipline"].ToString());
                        PA.Pipeline = Convert.ToString(reader["Pipeline"]);
                        PA.Objective = Convert.ToString(reader["Objective"]);
                        PA.SelfEvaluation = Convert.ToString(reader["SelfEvaluation"]);
                        PA.StrengthsAndWeaknesses = Convert.ToString(reader["StrengthsAndWeaknesses"]);

                        PA.OverrallEvaluation = reader["OverrallEvaluation"].ParseInt();
                        PA.CodingSkill = reader["CodingSkill"].ParseInt();
                        PA.AnalyticalSkill = reader["AnalyticalSkill"].ParseInt();
                        PA.ProblemSolving = reader["ProblemSolving"].ParseInt();
                        PA.Innovation = reader["Innovation"].ParseInt();
                        PA.DrivingForResults = reader["DrivingForResults"].ParseInt();
                        PA.DealingWithAmbiguity = reader["DealingWithAmbiguity"].ParseInt();
                        PA.QuickOnLearing = reader["QuickOnLearing"].ParseInt();
                        PA.English = reader["English"].ParseInt();
                        PA.Communication = reader["Communication"].ParseInt();
                        PA.TeamWork = reader["TeamWork"].ParseInt();
                        PA.Attitude = reader["Attitude"].ParseInt();
                        PA.IntegrityHonesty = reader["IntegrityHonesty"].ParseInt();
                        PA.OpenRespectful = reader["OpenRespectful"].ParseInt();
                        PA.BigChallenges = reader["BigChallenges"].ParseInt();
                        PA.Passion = reader["Passion"].ParseInt();
                        PA.Accountable = reader["Accountable"].ParseInt();
                        PA.SelfCritical = reader["SelfCritical"].ParseInt();

                        PA.MentorComments = Convert.ToString(reader["MentorComments"]);
                        PA.MentorStrength = Convert.ToString(reader["MentorStrength"]);
                        PA.MentorWeakness = Convert.ToString(reader["MentorWeakness"]);

                        PA.HiredAsFTE = reader["HiredAsFTE"].ParseInt();
                        PA.OnsiteInterviewNow = reader["OnsiteInterviewNow"].ParseInt();
                        PA.ExpectedOnsiteInterviewDate = reader["ExpectedOnsiteInterviewDate"].ParseDateTime();
                        PA.ExtendPeriod = reader["ExtendPeriod"].ParseDecimal();
                        PA.InsertDate = reader["InsertDate"].ParseDateTime();
                        PA.ModifyDate = reader["ModifyDate"].ParseDateTime();
                        /*
                        * PA Approval
                        * Author: Yin.P
                        * Date: 2010-1-5
                        */
                        if (reader["IsApproved"] != DBNull.Value)
                        {
                            PA.IsApproved = Convert.ToInt32(reader["IsApproved"]);
                        }
                        else
                        {
                            PA.IsApproved = 0;
                        }
                    }

                }

                myConnection.Close();
            }

            return PA;
        }

        public DataSet GetPABasicInfoByApplicantId(Guid ApplicantId)
        {
            DataSet dsPABasicInfo;
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(dbOwner + ".sf_GetPABasicInfoByApplicantId", myConnection);
                SqlDataAdapter da = new SqlDataAdapter(myCommand);
                myCommand.CommandType = CommandType.StoredProcedure;

                myCommand.Parameters.AddWithValue("@ApplicantId", ApplicantId);

                myConnection.Open();
                dsPABasicInfo = new DataSet("PABasicInfo");
                da.Fill(dsPABasicInfo, "PABasicInfo");
                myConnection.Close();
            }
            return dsPABasicInfo;
        }

        public DataSet GetPerformanceAssessmentByApplicantId(Guid ApplicantId)
        {
            DataSet dsPA;
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(dbOwner + ".sf_GetPerformanceAssessmentByApplocantId", myConnection);
                SqlDataAdapter da = new SqlDataAdapter(myCommand);
                myCommand.CommandType = CommandType.StoredProcedure;

                myCommand.Parameters.AddWithValue("@ApplicantId", ApplicantId);

                myConnection.Open();
                dsPA = new DataSet("PA");
                da.Fill(dsPA, "PA");
                myConnection.Close();
            }
            return dsPA;
        }

        public DataSet GetPerformanceAssessment()
        {
            DataSet dsPerformanceAssessment;
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(dbOwner + ".sf_GetAllPerformanceAssessment", myConnection);
                SqlDataAdapter da = new SqlDataAdapter(myCommand);
                myCommand.CommandType = CommandType.StoredProcedure;

                //myCommand.Parameters.AddWithValue("@MentorAlias", Alias);

                myConnection.Open();
                dsPerformanceAssessment = new DataSet("PerformanceAssessments");
                da.Fill(dsPerformanceAssessment, "PerformanceAssessments");
                myConnection.Close();
            }
            return dsPerformanceAssessment;
        }

        public DataSet GetPAReportForHR()
        {
            DataSet dsPAReportForHR;
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(dbOwner + ".sf_GetPAReportForHR", myConnection);
                SqlDataAdapter da = new SqlDataAdapter(myCommand);
                myCommand.CommandType = CommandType.StoredProcedure;

                myConnection.Open();
                dsPAReportForHR = new DataSet("dsPAReportForHR");
                da.Fill(dsPAReportForHR, "dsPAReportForHR");
                myConnection.Close();
            }
            return dsPAReportForHR;
        }

        public DataSet GetPerformanceAssessmentByModifyTime(DateTime StartTime, DateTime EndTime)
        {
            DataSet dsPA;
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(dbOwner + ".sf_GetPerformanceAssessmentByModifyTime", myConnection);
                SqlDataAdapter da = new SqlDataAdapter(myCommand);
                myCommand.CommandType = CommandType.StoredProcedure;

                myCommand.Parameters.AddWithValue("@StartTime", StartTime);
                myCommand.Parameters.AddWithValue("@EndTime", EndTime);

                myConnection.Open();
                dsPA = new DataSet("PA");
                da.Fill(dsPA, "PA");
                myConnection.Close();
            }
            return dsPA;
        }

        public DataSet GetUncompletedPAbyApplicantId(Guid ApplicantId, DateTime BeginTime)
        {
            DataSet dsPA;
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(dbOwner + ".sf_GetUncompletedPAbyApplicantId", myConnection);
                SqlDataAdapter da = new SqlDataAdapter(myCommand);
                myCommand.CommandType = CommandType.StoredProcedure;

                myCommand.Parameters.AddWithValue("@ApplicantId", ApplicantId);
                myCommand.Parameters.AddWithValue("@BeginTime", BeginTime);

                myConnection.Open();
                dsPA = new DataSet("PA");
                da.Fill(dsPA, "PA");
                myConnection.Close();
            }
            return dsPA;
        }
        #endregion

        #region Checkout Survey
        /*
         * Add by Yuanqin, 2011.5.31 
         */ 
        public Guid InsertCheckoutSurvey(MSRA.Springfield.Components.BizObjects.CheckoutSurvey obj)
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection cn = GetSqlConnection();
            SqlParameter prmSurveyId;
            try
            {
                cmd.Connection = cn;
                cmd.CommandText = "[dbo].[sf_InsertCheckoutSurvey]";
                cmd.CommandType = CommandType.StoredProcedure;

                #region Populate Parameters
                prmSurveyId = cmd.Parameters.Add("@id", SqlDbType.UniqueIdentifier);
                prmSurveyId.Direction = ParameterDirection.Output;

                SqlParameter prmApplicantId = cmd.Parameters.Add("@ApplicantId", SqlDbType.UniqueIdentifier);
                prmApplicantId.Value = obj.ApplicantId;

                SqlParameter prmInterviewId = cmd.Parameters.Add("@InterviewId", SqlDbType.Int);
                prmInterviewId.Value = obj.InterviewId;

                SqlParameter prmInternName = cmd.Parameters.Add("@InternName", SqlDbType.NChar);
                prmInternName.Size = 20;
                prmInternName.Value = obj.InternName;

                SqlParameter prmGroupId = cmd.Parameters.Add("@GroupId", SqlDbType.Int);
                prmGroupId.Value = obj.GroupId;

                SqlParameter prmInternshipDuration = cmd.Parameters.Add("@InternshipDuration", SqlDbType.Int);
                prmInternshipDuration.Value = obj.InternshipDuration;

                SqlParameter prmCheckInDate = cmd.Parameters.Add("@CheckInDate", SqlDbType.DateTime);
                prmCheckInDate.Value = obj.CheckInDate;

                SqlParameter prmCheckOutDate = cmd.Parameters.Add("@CheckOutDate", SqlDbType.DateTime);
                prmCheckOutDate.Value = obj.CheckOutDate;

                SqlParameter prmOverallView = cmd.Parameters.Add("@OverallView", SqlDbType.Int);
                prmOverallView.Value = obj.OverallView;

                SqlParameter prmOverallComments = cmd.Parameters.Add("@OverallComments", SqlDbType.Text);
                prmOverallComments.Value = obj.OverallComments;

                SqlParameter prmLikeWork = cmd.Parameters.Add("@LikeWork", SqlDbType.Int);
                prmLikeWork.Value = obj.LikeWork;

                SqlParameter prmBackground = cmd.Parameters.Add("@Background", SqlDbType.Int);
                prmBackground.Value = obj.Background;

                SqlParameter prmWorkAmount = cmd.Parameters.Add("@WorkAmount", SqlDbType.Int);
                prmWorkAmount.Value = obj.WorkAmount;

                SqlParameter prmObjects = cmd.Parameters.Add("@Objects", SqlDbType.Int);
                prmObjects.Value = obj.Objects;

                SqlParameter prmDevelopmentSkill = cmd.Parameters.Add("@DevelopmentSkill", SqlDbType.Int);
                prmDevelopmentSkill.Value = obj.DevelopmentSkill;

                SqlParameter prmResearchSkill = cmd.Parameters.Add("@ResearchSkill", SqlDbType.Int);
                prmResearchSkill.Value = obj.ResearchSkill;

                SqlParameter prmSDESkill = cmd.Parameters.Add("@SDESkill", SqlDbType.Int);
                prmSDESkill.Value = obj.SDESkill;

                SqlParameter prmDesignSkill = cmd.Parameters.Add("@DesignSkill", SqlDbType.Int);
                prmDesignSkill.Value = obj.DesignSkill;

                SqlParameter prmProjectSkill = cmd.Parameters.Add("@ProjectSkill", SqlDbType.Int);
                prmProjectSkill.Value = obj.ProjectSkill;

                SqlParameter prmCommunicationSkill = cmd.Parameters.Add("@CommunicationSkill", SqlDbType.Int);
                prmCommunicationSkill.Value = obj.CommunicationSkill;

                SqlParameter prmTeamwork = cmd.Parameters.Add("@Teamwork", SqlDbType.Int);
                prmTeamwork.Value = obj.Teamwork;

                SqlParameter prmWorkComments = cmd.Parameters.Add("@WorkComments", SqlDbType.Text);
                prmWorkComments.Value = obj.WorkComments;

                SqlParameter prmMentorSetGoal = cmd.Parameters.Add("@MentorSetGoal", SqlDbType.Int);
                prmMentorSetGoal.Value = obj.MentorSetGoal;

                SqlParameter prmHelpFromMentor = cmd.Parameters.Add("@HelpFromMentor", SqlDbType.Int);
                prmHelpFromMentor.Value = obj.HelpFromMentor;

                SqlParameter prmMakeGoodUse = cmd.Parameters.Add("@MakeGoodUse", SqlDbType.Int);
                prmMakeGoodUse.Value = obj.MakeGoodUse;

                SqlParameter prmRespect = cmd.Parameters.Add("@Respect", SqlDbType.Int);
                prmRespect.Value = obj.Respect;

                SqlParameter prmMentorComments = cmd.Parameters.Add("@MentorComments", SqlDbType.Text);
                prmMentorComments.Value = obj.MentorComments;

                SqlParameter prmTrainingView = cmd.Parameters.Add("@TrainingView", SqlDbType.Int);
                prmTrainingView.Value = obj.TrainingView;

                SqlParameter prmTrainingSuitable = cmd.Parameters.Add("@TrainingSuitable", SqlDbType.Int);
                prmTrainingSuitable.Value = obj.TrainingSuitable;

                SqlParameter prmTrainingEssential = cmd.Parameters.Add("@TrainingEssential", SqlDbType.Int);
                prmTrainingEssential.Value = obj.TrainingEssential;

                SqlParameter prmActivityInterest = cmd.Parameters.Add("@ActivityInterest", SqlDbType.Int);
                prmActivityInterest.Value = obj.ActivityInterest;

                SqlParameter prmTrainingComments = cmd.Parameters.Add("@TrainingComments", SqlDbType.Text);
                prmTrainingComments.Value = obj.TrainingComments;

                SqlParameter prmBalance = cmd.Parameters.Add("@Balance", SqlDbType.Int);
                prmBalance.Value = obj.Balance;

                SqlParameter prmWorkEnvironment = cmd.Parameters.Add("@WorkEnvironment", SqlDbType.Int);
                prmWorkEnvironment.Value = obj.WorkEnvironment;

                SqlParameter prmCompensation = cmd.Parameters.Add("@Compensation", SqlDbType.Int);
                prmCompensation.Value = obj.Compensation;

                SqlParameter prmSatisfaction = cmd.Parameters.Add("@Satisfaction", SqlDbType.Int);
                prmSatisfaction.Value = obj.Satisfaction;

                SqlParameter prmOnBoard = cmd.Parameters.Add("@OnBoard", SqlDbType.Int);
                prmOnBoard.Value = obj.OnBoard;

                SqlParameter prmAccommodation = cmd.Parameters.Add("@Accommodation", SqlDbType.Int);
                prmAccommodation.Value = obj.Accommodation;

                SqlParameter prmSalaryAndMeal = cmd.Parameters.Add("@SalaryAndMeal", SqlDbType.Int);
                prmSalaryAndMeal.Value = obj.SalaryAndMeal;

                SqlParameter prmReimbursement = cmd.Parameters.Add("@Reimbursement", SqlDbType.Int);
                prmReimbursement.Value = obj.Reimbursement;

                SqlParameter prmITSupport = cmd.Parameters.Add("@ITSupport", SqlDbType.Int);
                prmITSupport.Value = obj.ITSupport;

                SqlParameter prmDailySupport = cmd.Parameters.Add("@DailySupport", SqlDbType.Int);
                prmDailySupport.Value = obj.DailySupport;

                SqlParameter prmLifeComments = cmd.Parameters.Add("@LifeComments", SqlDbType.Text);
                prmLifeComments.Value = obj.LifeComments;

                SqlParameter prmLeading = cmd.Parameters.Add("@Leading", SqlDbType.Int);
                prmLeading.Value = obj.Leading;

                SqlParameter prmMSCulture = cmd.Parameters.Add("@MSCulture", SqlDbType.Int);
                prmMSCulture.Value = obj.MSCulture;

                SqlParameter prmReturnAsIntern = cmd.Parameters.Add("@ReturnAsIntern", SqlDbType.Int);
                prmReturnAsIntern.Value = obj.ReturnAsIntern;

                SqlParameter prmJoinMS = cmd.Parameters.Add("@JoinMS", SqlDbType.Int);
                prmJoinMS.Value = obj.JoinMS;

                SqlParameter prmRecommend = cmd.Parameters.Add("@Recommend", SqlDbType.Int);
                prmRecommend.Value = obj.Recommend;

                SqlParameter prmMSRAComments = cmd.Parameters.Add("@MSRAComments", SqlDbType.Text);
                prmMSRAComments.Value = obj.MSRAComments;

                SqlParameter prmComments = cmd.Parameters.Add("@Comments", SqlDbType.Text);
                prmComments.Value = obj.Comments;

                SqlParameter prmInsertDate = cmd.Parameters.Add("@InsertDate", SqlDbType.DateTime);
                prmInsertDate.Value = obj.InsertDate;

                #endregion

                #region Execute Command
                cn.Open();
                cmd.ExecuteNonQuery();
                #endregion
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                {
                    cn.Close();
                }

                cn.Dispose();
                cmd.Dispose();
            }

            return (Guid)prmSurveyId.Value;
        }

        public bool UpdateCheckoutSurvey(MSRA.Springfield.Components.BizObjects.CheckoutSurvey obj)
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection cn = GetSqlConnection();
            bool isSuccess = false;
            try
            {
                cmd.Connection = cn;
                cmd.CommandText = "[dbo].[sf_UpdateCheckoutSurvey]";
                cmd.CommandType = CommandType.StoredProcedure;

                #region Populate Parameters
                SqlParameter prmSurveyId = cmd.Parameters.Add("@id", SqlDbType.UniqueIdentifier);
                prmSurveyId.Value = obj.id;

                SqlParameter prmApplicantId = cmd.Parameters.Add("@ApplicantId", SqlDbType.UniqueIdentifier);
                prmApplicantId.Value = obj.ApplicantId;

                SqlParameter prmInterviewId = cmd.Parameters.Add("@InterviewId", SqlDbType.Int);
                prmInterviewId.Value = obj.InterviewId;

                SqlParameter prmInternName = cmd.Parameters.Add("@InternName", SqlDbType.NChar);
                prmInternName.Size = 20;
                prmInternName.Value = obj.InternName;

                SqlParameter prmGroupId = cmd.Parameters.Add("@GroupId", SqlDbType.Int);
                prmGroupId.Value = obj.GroupId;

                SqlParameter prmInternshipDuration = cmd.Parameters.Add("@InternshipDuration", SqlDbType.Int);
                prmInternshipDuration.Value = obj.InternshipDuration;

                SqlParameter prmCheckInDate = cmd.Parameters.Add("@CheckInDate", SqlDbType.DateTime);
                prmCheckInDate.Value = obj.CheckInDate;

                SqlParameter prmCheckOutDate = cmd.Parameters.Add("@CheckOutDate", SqlDbType.DateTime);
                prmCheckOutDate.Value = obj.CheckOutDate;

                SqlParameter prmOverallView = cmd.Parameters.Add("@OverallView", SqlDbType.Int);
                prmOverallView.Value = obj.OverallView;

                SqlParameter prmOverallComments = cmd.Parameters.Add("@OverallComments", SqlDbType.Text);
                prmOverallComments.Value = obj.OverallComments;

                SqlParameter prmLikeWork = cmd.Parameters.Add("@LikeWork", SqlDbType.Int);
                prmLikeWork.Value = obj.LikeWork;

                SqlParameter prmBackground = cmd.Parameters.Add("@Background", SqlDbType.Int);
                prmBackground.Value = obj.Background;

                SqlParameter prmWorkAmount = cmd.Parameters.Add("@WorkAmount", SqlDbType.Int);
                prmWorkAmount.Value = obj.WorkAmount;

                SqlParameter prmObjects = cmd.Parameters.Add("@Objects", SqlDbType.Int);
                prmObjects.Value = obj.Objects;

                SqlParameter prmDevelopmentSkill = cmd.Parameters.Add("@DevelopmentSkill", SqlDbType.Int);
                prmDevelopmentSkill.Value = obj.DevelopmentSkill;

                SqlParameter prmResearchSkill = cmd.Parameters.Add("@ResearchSkill", SqlDbType.Int);
                prmResearchSkill.Value = obj.ResearchSkill;

                SqlParameter prmSDESkill = cmd.Parameters.Add("@SDESkill", SqlDbType.Int);
                prmSDESkill.Value = obj.SDESkill;

                SqlParameter prmDesignSkill = cmd.Parameters.Add("@DesignSkill", SqlDbType.Int);
                prmDesignSkill.Value = obj.DesignSkill;

                SqlParameter prmProjectSkill = cmd.Parameters.Add("@ProjectSkill", SqlDbType.Int);
                prmProjectSkill.Value = obj.ProjectSkill;

                SqlParameter prmCommunicationSkill = cmd.Parameters.Add("@CommunicationSkill", SqlDbType.Int);
                prmCommunicationSkill.Value = obj.CommunicationSkill;

                SqlParameter prmTeamwork = cmd.Parameters.Add("@Teamwork", SqlDbType.Int);
                prmTeamwork.Value = obj.Teamwork;

                SqlParameter prmWorkComments = cmd.Parameters.Add("@WorkComments", SqlDbType.Text);
                prmWorkComments.Value = obj.WorkComments;

                SqlParameter prmMentorSetGoal = cmd.Parameters.Add("@MentorSetGoal", SqlDbType.Int);
                prmMentorSetGoal.Value = obj.MentorSetGoal;

                SqlParameter prmHelpFromMentor = cmd.Parameters.Add("@HelpFromMentor", SqlDbType.Int);
                prmHelpFromMentor.Value = obj.HelpFromMentor;

                SqlParameter prmMakeGoodUse = cmd.Parameters.Add("@MakeGoodUse", SqlDbType.Int);
                prmMakeGoodUse.Value = obj.MakeGoodUse;

                SqlParameter prmRespect = cmd.Parameters.Add("@Respect", SqlDbType.Int);
                prmRespect.Value = obj.Respect;

                SqlParameter prmMentorComments = cmd.Parameters.Add("@MentorComments", SqlDbType.Text);
                prmMentorComments.Value = obj.MentorComments;

                SqlParameter prmTrainingView = cmd.Parameters.Add("@TrainingView", SqlDbType.Int);
                prmTrainingView.Value = obj.TrainingView;

                SqlParameter prmTrainingSuitable = cmd.Parameters.Add("@TrainingSuitable", SqlDbType.Int);
                prmTrainingSuitable.Value = obj.TrainingSuitable;

                SqlParameter prmTrainingEssential = cmd.Parameters.Add("@TrainingEssential", SqlDbType.Int);
                prmTrainingEssential.Value = obj.TrainingEssential;

                SqlParameter prmActivityInterest = cmd.Parameters.Add("@ActivityInterest", SqlDbType.Int);
                prmActivityInterest.Value = obj.ActivityInterest;

                SqlParameter prmTrainingComments = cmd.Parameters.Add("@TrainingComments", SqlDbType.Text);
                prmTrainingComments.Value = obj.TrainingComments;

                SqlParameter prmBalance = cmd.Parameters.Add("@Balance", SqlDbType.Int);
                prmBalance.Value = obj.Balance;

                SqlParameter prmWorkEnvironment = cmd.Parameters.Add("@WorkEnvironment", SqlDbType.Int);
                prmWorkEnvironment.Value = obj.WorkEnvironment;

                SqlParameter prmCompensation = cmd.Parameters.Add("@Compensation", SqlDbType.Int);
                prmCompensation.Value = obj.Compensation;

                SqlParameter prmSatisfaction = cmd.Parameters.Add("@Satisfaction", SqlDbType.Int);
                prmSatisfaction.Value = obj.Satisfaction;

                SqlParameter prmOnBoard = cmd.Parameters.Add("@OnBoard", SqlDbType.Int);
                prmOnBoard.Value = obj.OnBoard;

                SqlParameter prmAccommodation = cmd.Parameters.Add("@Accommodation", SqlDbType.Int);
                prmAccommodation.Value = obj.Accommodation;

                SqlParameter prmSalaryAndMeal = cmd.Parameters.Add("@SalaryAndMeal", SqlDbType.Int);
                prmSalaryAndMeal.Value = obj.SalaryAndMeal;

                SqlParameter prmReimbursement = cmd.Parameters.Add("@Reimbursement", SqlDbType.Int);
                prmReimbursement.Value = obj.Reimbursement;

                SqlParameter prmITSupport = cmd.Parameters.Add("@ITSupport", SqlDbType.Int);
                prmITSupport.Value = obj.ITSupport;

                SqlParameter prmDailySupport = cmd.Parameters.Add("@DailySupport", SqlDbType.Int);
                prmDailySupport.Value = obj.DailySupport;

                SqlParameter prmLifeComments = cmd.Parameters.Add("@LifeComments", SqlDbType.Text);
                prmLifeComments.Value = obj.LifeComments;

                SqlParameter prmLeading = cmd.Parameters.Add("@Leading", SqlDbType.Int);
                prmLeading.Value = obj.Leading;

                SqlParameter prmMSCulture = cmd.Parameters.Add("@MSCulture", SqlDbType.Int);
                prmMSCulture.Value = obj.MSCulture;

                SqlParameter prmReturnAsIntern = cmd.Parameters.Add("@ReturnAsIntern", SqlDbType.Int);
                prmReturnAsIntern.Value = obj.ReturnAsIntern;

                SqlParameter prmJoinMS = cmd.Parameters.Add("@JoinMS", SqlDbType.Int);
                prmJoinMS.Value = obj.JoinMS;

                SqlParameter prmRecommend = cmd.Parameters.Add("@Recommend", SqlDbType.Int);
                prmRecommend.Value = obj.Recommend;

                SqlParameter prmMSRAComments = cmd.Parameters.Add("@MSRAComments", SqlDbType.Text);
                prmMSRAComments.Value = obj.MSRAComments;

                SqlParameter prmComments = cmd.Parameters.Add("@Comments", SqlDbType.Text);
                prmComments.Value = obj.Comments;

                SqlParameter prmInsertDate = cmd.Parameters.Add("@InsertDate", SqlDbType.DateTime);
                prmInsertDate.Value = obj.InsertDate;

                #endregion

                #region Execute Command
                cn.Open();
                cmd.ExecuteNonQuery();
                isSuccess = true;
                #endregion
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                {
                    cn.Close();
                }

                cn.Dispose();
                cmd.Dispose();
            }

            return isSuccess;
        }

        public bool DeleteCheckoutSurveyById(Guid id)
        {
            bool isSuccess = false;
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(dbOwner + ".sf_DeleteCheckoutSurveyById", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter prmid = myCommand.Parameters.Add("@id", SqlDbType.UniqueIdentifier);
                prmid.Value = id;
                myConnection.Open();
                myCommand.ExecuteNonQuery();
                myConnection.Close();
                isSuccess = true;
            }
            return isSuccess;
        }

        public MSRA.Springfield.Components.BizObjects.CheckoutSurvey GetCheckoutSurveyById(Guid id)
        {
            MSRA.Springfield.Components.BizObjects.CheckoutSurvey Survey = null;

            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(dbOwner + ".sf_GetCheckoutSurveyById", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("@id", id);
                myConnection.Open();

                using (SqlDataReader reader = myCommand.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (reader != null && !reader.IsClosed)
                    {
                        Survey = new MSRA.Springfield.Components.BizObjects.CheckoutSurvey();
                        reader.Read();
                        Survey.id = new Guid(reader["id"].ToString());
                        Survey.ApplicantId = (Guid)reader["ApplicantId"];
                        Survey.InterviewId = reader["InterviewId"].ParseInt();
                        Survey.InternName = reader["InternName"].ParseString();
                        Survey.GroupId = reader["GroupId"].ParseInt();
                        Survey.InternshipDuration = reader["InternshipDuration"].ParseInt();
                        Survey.CheckInDate = (DateTime)reader["CheckInDate"];
                        Survey.CheckOutDate = (DateTime)reader["CheckOutDate"];
                        Survey.OverallView = reader["OverallView"].ParseInt();
                        Survey.OverallComments = reader["OverallComments"].ParseString();
                        Survey.LikeWork = reader["LikeWork"].ParseInt();
                        Survey.Background = reader["Background"].ParseInt();
                        Survey.WorkAmount = reader["WorkAmount"].ParseInt();
                        Survey.Objects = reader["Objects"].ParseInt();
                        Survey.DevelopmentSkill = reader["DevelopmentSkill"].ParseInt();
                        Survey.ResearchSkill = reader["ResearchSkill"].ParseInt();
                        Survey.SDESkill = reader["SDESkill"].ParseInt();
                        Survey.DesignSkill = reader["DesignSkill"].ParseInt();
                        Survey.ProjectSkill = reader["ProjectSkill"].ParseInt();
                        Survey.CommunicationSkill = reader["CommunicationSkill"].ParseInt();
                        Survey.Teamwork = reader["Teamwork"].ParseInt();
                        Survey.WorkComments = reader["WorkComments"].ParseString();
                        Survey.MentorSetGoal = reader["MentorSetGoal"].ParseInt();
                        Survey.HelpFromMentor = reader["HelpFromMentor"].ParseInt();
                        Survey.MakeGoodUse = reader["MakeGoodUse"].ParseInt();
                        Survey.Respect = reader["Respect"].ParseInt();
                        Survey.MentorComments = reader["MentorComments"].ParseString();
                        Survey.TrainingView = reader["TrainingView"].ParseInt();
                        Survey.TrainingSuitable = reader["TrainingSuitable"].ParseInt();
                        Survey.TrainingEssential = reader["TrainingEssential"].ParseInt();
                        Survey.ActivityInterest = reader["ActivityInterest"].ParseInt();
                        Survey.TrainingComments = reader["TrainingComments"].ParseString();
                        Survey.Balance = reader["Balance"].ParseInt();
                        Survey.WorkEnvironment = reader["WorkEnvironment"].ParseInt();
                        Survey.Compensation = reader["Compensation"].ParseInt();
                        Survey.Satisfaction = reader["Satisfaction"].ParseInt();
                        Survey.OnBoard = reader["OnBoard"].ParseInt();
                        Survey.Accommodation = reader["Accommodation"].ParseInt();
                        Survey.SalaryAndMeal = reader["SalaryAndMeal"].ParseInt();
                        Survey.Reimbursement = reader["Reimbursement"].ParseInt();
                        Survey.ITSupport = reader["ITSupport"].ParseInt();
                        Survey.DailySupport = reader["DailySupport"].ParseInt();
                        Survey.LifeComments = reader["LifeComments"].ParseString();
                        Survey.Leading = reader["Leading"].ParseInt();
                        Survey.MSCulture = reader["MSCulture"].ParseInt();
                        Survey.ReturnAsIntern = reader["ReturnAsIntern"].ParseInt();
                        Survey.JoinMS = reader["JoinMS"].ParseInt();
                        Survey.Recommend = reader["Recommend"].ParseInt();
                        Survey.MSRAComments = reader["MSRAComments"].ParseString();
                        Survey.Comments = reader["Comments"].ParseString();
                        Survey.InsertDate = (DateTime)reader["InsertDate"];
                    }

                }

                myConnection.Close();
            }

            return Survey;
        }

        public DataSet GetCheckoutSurveyByApplicantId(Guid ApplicantId)
        {
            DataSet dsSurvey;
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(dbOwner + ".sf_GetCheckoutSurveyByApplicantId", myConnection);
                SqlDataAdapter da = new SqlDataAdapter(myCommand);
                myCommand.CommandType = CommandType.StoredProcedure;

                myCommand.Parameters.AddWithValue("@ApplicantId", ApplicantId);

                myConnection.Open();
                dsSurvey = new DataSet("Survey");
                da.Fill(dsSurvey, "Survey");
                myConnection.Close();
            }
            return dsSurvey;
        }

        //public MSRA.Springfield.Components.BizObjects.CheckoutSurvey GetCheckoutSurveyByApplicantId(Guid ApplicantId)
        //{
        //    MSRA.Springfield.Components.BizObjects.CheckoutSurvey Survey = null;

        //    using (SqlConnection myConnection = GetSqlConnection())
        //    {
        //        //SqlCommand myCommand = new SqlCommand(dbOwner + ".sf_GetCheckoutSurveyById", myConnection);
        //        //myCommand.CommandType = CommandType.StoredProcedure;
        //        //myCommand.Parameters.AddWithValue("@id", id);
        //        //myConnection.Open();

        //        SqlCommand myCommand = new SqlCommand(dbOwner + ".sf_GetCheckoutSurveyByApplocantId", myConnection);
        //        myCommand.CommandType = CommandType.StoredProcedure;
        //        myCommand.Parameters.AddWithValue("@ApplicantId", ApplicantId);
        //        myConnection.Open();

        //        using (SqlDataReader reader = myCommand.ExecuteReader(CommandBehavior.CloseConnection))
        //        {
        //            if (reader != null && !reader.IsClosed)
        //            {
        //                Survey = new MSRA.Springfield.Components.BizObjects.CheckoutSurvey();
        //                reader.Read();
        //                Survey.id = new Guid(reader["id"].ToString());
        //                Survey.ApplicantId = (Guid)reader["ApplicantId"];
        //                Survey.InterviewId = reader["InterviewId"].ParseInt();
        //                Survey.InternName = reader["InternName"].ParseString();
        //                Survey.GroupId = reader["GroupId"].ParseInt();
        //                Survey.InternshipDuration = reader["InternshipDuration"].ParseInt();
        //                Survey.CheckInDate = (DateTime)reader["CheckInDate"];
        //                Survey.CheckOutDate = (DateTime)reader["CheckOutDate"];
        //                Survey.OverallView = reader["OverallView"].ParseInt();
        //                Survey.OverallComments = reader["OverallComments"].ParseString();
        //                Survey.LikeWork = reader["LikeWork"].ParseInt();
        //                Survey.Background = reader["Background"].ParseInt();
        //                Survey.WorkAmount = reader["WorkAmount"].ParseInt();
        //                Survey.Objects = reader["Objects"].ParseInt();
        //                Survey.DevelopmentSkill = reader["DevelopmentSkill"].ParseInt();
        //                Survey.ResearchSkill = reader["ResearchSkill"].ParseInt();
        //                Survey.SDESkill = reader["SDESkill"].ParseInt();
        //                Survey.DesignSkill = reader["DesignSkill"].ParseInt();
        //                Survey.ProjectSkill = reader["ProjectSkill"].ParseInt();
        //                Survey.CommunicationSkill = reader["CommunicationSkill"].ParseInt();
        //                Survey.Teamwork = reader["Teamwork"].ParseInt();
        //                Survey.WorkComments = reader["WorkComments"].ParseString();
        //                Survey.MentorSetGoal = reader["MentorSetGoal"].ParseInt();
        //                Survey.HelpFromMentor = reader["HelpFromMentor"].ParseInt();
        //                Survey.MakeGoodUse = reader["MakeGoodUse"].ParseInt();
        //                Survey.Respect = reader["Respect"].ParseInt();
        //                Survey.MentorComments = reader["MentorComments"].ParseString();
        //                Survey.TrainingView = reader["TrainingView"].ParseInt();
        //                Survey.TrainingSuitable = reader["TrainingSuitable"].ParseInt();
        //                Survey.TrainingEssential = reader["TrainingEssential"].ParseInt();
        //                Survey.ActivityInterest = reader["ActivityInterest"].ParseInt();
        //                Survey.TrainingComments = reader["TrainingComments"].ParseString();
        //                Survey.Balance = reader["Balance"].ParseInt();
        //                Survey.WorkEnvironment = reader["WorkEnvironment"].ParseInt();
        //                Survey.Compensation = reader["Compensation"].ParseInt();
        //                Survey.Satisfaction = reader["Satisfaction"].ParseInt();
        //                Survey.OnBoard = reader["OnBoard"].ParseInt();
        //                Survey.Accommodation = reader["Accommodation"].ParseInt();
        //                Survey.SalaryAndMeal = reader["SalaryAndMeal"].ParseInt();
        //                Survey.Reimbursement = reader["Reimbursement"].ParseInt();
        //                Survey.ITSupport = reader["ITSupport"].ParseInt();
        //                Survey.DailySupport = reader["DailySupport"].ParseInt();
        //                Survey.LifeComments = reader["LifeComments"].ParseString();
        //                Survey.Leading = reader["Leading"].ParseInt();
        //                Survey.MSCulture = reader["MSCulture"].ParseInt();
        //                Survey.ReturnAsIntern = reader["ReturnAsIntern"].ParseInt();
        //                Survey.JoinMS = reader["JoinMS"].ParseInt();
        //                Survey.Recommend = reader["Recommend"].ParseInt();
        //                Survey.MSRAComments = reader["MSRAComments"].ParseString();
        //                Survey.Comments = reader["Comments"].ParseString();
        //                Survey.InsertDate = (DateTime)reader["InsertDate"];
        //            }

        //        }

        //        myConnection.Close();
        //    }

        //    return Survey;
        //}

        public DataSet GetCheckoutSurvey()
        {
            DataSet dsCheckoutSurvey;
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(dbOwner + ".sf_GetAllCheckoutSurvey", myConnection);
                SqlDataAdapter da = new SqlDataAdapter(myCommand);
                myCommand.CommandType = CommandType.StoredProcedure;

                //myCommand.Parameters.AddWithValue("@MentorAlias", Alias);

                myConnection.Open();
                dsCheckoutSurvey = new DataSet("CheckoutSurvey");
                da.Fill(dsCheckoutSurvey, "CheckoutSurvey");
                myConnection.Close();
            }
            return dsCheckoutSurvey;
        }

        public DataSet GetSurveyReport()
        {
            DataSet dsSurveyReport;
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(dbOwner + ".sf_GetSurveyReport", myConnection);
                SqlDataAdapter da = new SqlDataAdapter(myCommand);
                myCommand.CommandType = CommandType.StoredProcedure;

                myConnection.Open();
                dsSurveyReport = new DataSet("dsSurveyReport");
                da.Fill(dsSurveyReport, "dsSurveyReport");
                myConnection.Close();
            }
            return dsSurveyReport;
        }
//#############add by bin###############################
        //public DataSet GetSurveyBasicInfoByApplicantId(Guid id)
        //{
        //    DataSet dsSurveyBasicInfo;
        //    using (SqlConnection myConnection = GetSqlConnection())
        //    {
        //        SqlCommand myCommend = new SqlCommand(dbOwner + ".sf_GetSurveyBasicInfoByApplicantId", myConnection);
        //        SqlDataAdapter da = new SqlDataAdapter(myCommend);
        //        myCommend.CommandType = CommandType.StoredProcedure;

        //        myConnection.Open();
        //        dsSurveyBasicInfo = new DataSet("dsSurveyBasicInfo");
        //        da.Fill(dsSurveyBasicInfo, "dsSurveyBasicInfo");
        //        myConnection.Close();
        //    }

        //    return dsSurveyBasicInfo;
        //}

        public DataSet GetSurveyBasicInfoByApplicantId(Guid ApplicantId)
        {
            DataSet dsSurveyBasicInfo;
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(dbOwner + ".sf_GetSurveyBasicInfoByApplicantId", myConnection);
                SqlDataAdapter da = new SqlDataAdapter(myCommand);
                myCommand.CommandType = CommandType.StoredProcedure;

                myCommand.Parameters.AddWithValue("@ApplicantId", ApplicantId);

                myConnection.Open();
                dsSurveyBasicInfo = new DataSet("dsSurveyBasicInfo");
                da.Fill(dsSurveyBasicInfo, "dsSurveyBasicInfo");
                myConnection.Close();
            }
            return dsSurveyBasicInfo;
        }

        #endregion


        #region InternPublication
        public Guid InsertInternPublication(InternPublication obj)
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection cn = GetSqlConnection();
            SqlParameter prmInternPublicationId;
            try
            {
                cmd.Connection = cn;
                cmd.CommandText = "[dbo].[sf_InsertInternPublication]";
                cmd.CommandType = CommandType.StoredProcedure;

                #region Populate Parameters
                prmInternPublicationId = cmd.Parameters.Add("@PublicationId", SqlDbType.UniqueIdentifier);
                prmInternPublicationId.Direction = ParameterDirection.Output;

                SqlParameter prmApplicantId = cmd.Parameters.Add("@ApplicantId", SqlDbType.UniqueIdentifier);
                prmApplicantId.Value = obj.ApplicantId;

                SqlParameter prmPAId = cmd.Parameters.Add("@PAId", SqlDbType.UniqueIdentifier);
                prmPAId.Value = obj.PAId;

                SqlParameter prmName = cmd.Parameters.Add("@Name", SqlDbType.Text);
                prmName.Value = obj.Name;

                SqlParameter prmCurrentStatus = cmd.Parameters.Add("@CurrentStatus", SqlDbType.Int);
                prmCurrentStatus.Value = obj.CurrentStatus;
                #endregion

                #region Execute Command
                cn.Open();
                cmd.ExecuteNonQuery();
                #endregion
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                {
                    cn.Close();
                }

                cn.Dispose();
                cmd.Dispose();
            }

            return (Guid)prmInternPublicationId.Value;
        }

        public bool UpdateInternPublication(InternPublication obj)
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection cn = GetSqlConnection();
            bool isSuccess = false;
            try
            {
                cmd.Connection = cn;
                cmd.CommandText = "[dbo].[sf_UpdateInternPublication]";
                cmd.CommandType = CommandType.StoredProcedure;

                #region Populate Parameters
                SqlParameter prmInternPublicationId = cmd.Parameters.Add("@PublicationId", SqlDbType.UniqueIdentifier);
                prmInternPublicationId.Value = obj.PublicationId;

                SqlParameter prmApplicantId = cmd.Parameters.Add("@ApplicantId", SqlDbType.UniqueIdentifier);
                prmApplicantId.Value = obj.ApplicantId;

                SqlParameter prmPAId = cmd.Parameters.Add("@PAId", SqlDbType.UniqueIdentifier);
                prmPAId.Value = obj.PAId;

                SqlParameter prmName = cmd.Parameters.Add("@Name", SqlDbType.Text);
                prmName.Value = obj.Name;

                SqlParameter prmCurrentStatus = cmd.Parameters.Add("@CurrentStatus", SqlDbType.Int);
                prmCurrentStatus.Value = obj.CurrentStatus;

                #endregion

                #region Execute Command
                cn.Open();
                cmd.ExecuteNonQuery();
                isSuccess = true;
                #endregion
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                {
                    cn.Close();
                }

                cn.Dispose();
                cmd.Dispose();
            }

            return isSuccess;
        }

        public bool DeleteInternPublicationById(Guid id)
        {
            bool isSuccess = false;
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(dbOwner + ".sf_DeleteInternPublication", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter prmid = myCommand.Parameters.Add("@PublicationId", SqlDbType.UniqueIdentifier);
                prmid.Value = id;
                myConnection.Open();
                myCommand.ExecuteNonQuery();
                myConnection.Close();
                isSuccess = true;
            }
            return isSuccess;
        }

        public DataSet GetInternPublicationByPAId(Guid PAId)//»ñµÃPAÄÚÈÝ
        {
            DataSet dsInternPublication;
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(dbOwner + ".sf_GetInternPublicationByPAId", myConnection);
                SqlDataAdapter da = new SqlDataAdapter(myCommand);
                myCommand.CommandType = CommandType.StoredProcedure;

                myCommand.Parameters.AddWithValue("@PAId", PAId);

                myConnection.Open();
                dsInternPublication = new DataSet("InternPublications");
                da.Fill(dsInternPublication, "InternPublications");
                myConnection.Close();
            }
            return dsInternPublication;
        }

        public InternPublication GetInternPublicationById(Guid PublicationId)
        {
            InternPublication ip = null;

            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(dbOwner + ".sf_GetInternPublicationById", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("@PublicationId", PublicationId);
                myConnection.Open();

                using (SqlDataReader reader = myCommand.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (reader != null && !reader.IsClosed)
                    {
                        ip = new InternPublication();
                        reader.Read();
                        ip.PublicationId = new Guid(reader["PublicationId"].ToString());
                        ip.ApplicantId = (Guid)reader["ApplicantId"];
                        ip.PAId = (Guid)reader["PAId"];
                        ip.Name = (string)reader["Name"];
                        ip.CurrentStatus = (int)reader["CurrentStatus"];
                        ip.SummitDate = (DateTime)reader["SummitDate"];
                        ip.ModifyDate = (DateTime)reader["ModifyDate"];
                    }

                }

                myConnection.Close();
            }

            return ip;
        }
        #endregion

        #region
        public bool AddCashedApplicant(Guid ApplicantId, int InterviewId)
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection cn = GetSqlConnection();
            bool isSuccess = false;
            try
            {
                cmd.Connection = cn;
                cmd.CommandText = "[dbo].[sf_InsertCashedApplicantInfo]";
                cmd.CommandType = CommandType.StoredProcedure;

                #region Populate Parameters
                SqlParameter prmApplicantId = cmd.Parameters.Add("@ApplicantId", SqlDbType.UniqueIdentifier);
                prmApplicantId.Value = ApplicantId;
                SqlParameter prmInterviewId = cmd.Parameters.Add("@InterviewId", SqlDbType.Int);
                prmInterviewId.Value = InterviewId;
                #endregion

                #region Execute Command
                cn.Open();
                cmd.ExecuteNonQuery();
                isSuccess = true;
                #endregion
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                {
                    cn.Close();
                }

                cn.Dispose();
                cmd.Dispose();
            }

            return isSuccess;
        }
        public DataSet GetAllApplcantsforSourcingReport(DateTime StartTime, DateTime EndTime)
        {
            DataSet dsApplcants;
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(dbOwner + ".sf_GetAllApplicantsforSorcingReport", myConnection);
                SqlDataAdapter da = new SqlDataAdapter(myCommand);
                myCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter prmStartTime = myCommand.Parameters.Add("@StartTime", SqlDbType.DateTime);
                prmStartTime.Value = StartTime;
                SqlParameter prmEndTime = myCommand.Parameters.Add("@EndTime", SqlDbType.DateTime);
                prmEndTime.Value = EndTime;

                myConnection.Open();
                dsApplcants = new DataSet("Applcants");
                da.Fill(dsApplcants, "Applcants");
                myConnection.Close();
            }
            return dsApplcants;
        }
        #endregion
    }
}
