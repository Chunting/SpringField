using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using MSRA.SpringField.Components.BizObjects;
using MSRA.SpringField.Components.Enumerations;

namespace MSRA.SpringField.Components.Configuration
{
    public interface IDataProvider
    {
        #region ApplicantBasicInfo
        void InsertApplicantBasicInfo(ApplicantBasicInfo obj);
        void UpdateApplicantBasicInfo(ApplicantBasicInfo obj);
        void DeleteApplicantBasicInfoById(Guid id);
        ApplicantBasicInfo GetApplicantBasicInfoById(Guid id);
        void ChangeApplicantStatus(Guid id, ApplicationStatusEnum status);
        #endregion

        #region ApplicantEduBackground
        void InsertApplicantEduBackground(ApplicantEduBackground obj);
        void UpdateApplicantEduBackground(ApplicantEduBackground obj);
        void DeleteApplicantEduBackgroundById(Guid id);
        ApplicantEduBackground GetApplicantEduBackgroundById(Guid id);
        #endregion

        #region ApplicantRelatedInfo
        void InsertApplicantRelatedInfo(ApplicantRelatedInfo obj);
        void UpdateApplicantRelatedInfo(ApplicantRelatedInfo obj);
        void DeleteApplicantRelatedInfoById(Guid id);
        ApplicantRelatedInfo GetApplicantRelatedInfoById(Guid id);
        #endregion

        #region Applicant
        DataSet GetAllApplicants();
        DataSet GetAllApplicantsWithoutPermissionFilter();
        DataSet GetApplicantsByApplicationDate(DateTime dtStart, DateTime dtEnd);
        bool IsIdNumUsed(string idCard);
        DataSet GetDecidedApplicants(Int16 ApplicantStatus);
        DataSet GetAllInterviewApplicants();
        void DeleteApplcantById(Guid applicantId);
        DataSet GetApplicantsByResume(String condition);
        DataSet GetApplicantsByStatus(InterviewStatusEnum status);
        DataSet GetApplicantsByStatus();
        DataSet GetHiredApplicants();
        DataSet GetApplicantsByCondition(/*int IsFeedbackCompleted, string mentorAlias, int interviewStatus*/);
        /*
         * Get applicants by check-in date
         * Yuanqin, 2011.4.25
         */
        DataSet GetApplicantsByCheckInDate(DateTime dtStart, DateTime dtEnd);
        #endregion

        #region Document
        int InsertDocument(Document obj);
        void UpdateDocument(Document obj);
        void DeleteDocument(int docId);
        Document GetDocumentById(int docId);
        #endregion

        #region EmailTemplate
        EmailTemplate GetEmailTemplateByType(MailType mt);
        void UpdateEmailTemplate(EmailTemplate emailTemplate);
        #endregion

        #region Email
        int InsertEmail(Email obj);
        void UpdateEmail(Email obj);
        void DeleteEmail(int emailId);
        Email GetEmailById(int emailId);
        #endregion

        #region Favorites
        int InsertFavorite(Favorite obj);
        void UpdateFavorite(Favorite obj);
        void DeleteFavorite(int favoriteId);
        Favorite GetFavoriteById(int favoriteId);
        DataSet GetFavoritesByUserId(Guid userId);
        bool IsFavoriteExist(Guid ownerId, Guid applicantId);
        bool DeleteFavorite(Guid ownerId, Guid applicantId);
        #endregion

        #region Feedback
        int InsertFeedback(Feedback obj);
        void UpdateFeedback(Feedback obj);
        void DeleteFeedback(int id);
        Feedback GetFeedbackById(int id);
        DataSet GetFeedbackByInterview(int interviewId, bool isComplete);
        DataSet GetIncompleteFeedbackByAlias(string alias);
        Boolean CheckInterviewSameAsMentor(Int32 interviewId, String mentorAlias);
        #endregion

        #region Interview
        int InsertInterview(Interview obj);
        void UpdateInterview(Interview obj);
        void DeleteInterview(int interviewId);
        Interview GetInterviewById(int interviewId);
        Interview GetCurrentInterview(Guid applicantId, Guid hiringManagerId);
        DataSet GetInterviewForCurrentUser(Guid hiringManagerId, String Alias);
        string GetRecentInterviewIdByApplicant(Guid applicantId);
        string GetRecentInterviewStatus(Guid applicantId);
        string GetRecentInterviewDateTime(Guid applicantId);
        DataSet GetInterviewForSiteUser(Guid hiringManagerId);
        DataSet GetDurationInterviewedSiteUser(DateTime startDate, DateTime endDate);
        DataSet GetDurationInterview(DateTime startDate, DateTime endDate);
        void DeleteIncompleteFeedbackForInterview(int interviewId);
        DataSet GetAllProcessingInterview();
        string GetInterviewIdByApplicant(Guid applicantId);
        DataSet GetInterviewForApplicant(Guid applicantId);
        Boolean UpdateDecisionMailStatus(Int32 interviewId);
        #endregion

        #region Referral
        int InsertReferral(Referral obj);
        void UpdateReferral(Referral obj);
        void DeleteReferral(int referralId);
        Referral GetReferralById(int referralId);
        #endregion

        #region SiteUser
        Guid GetUserIdByFullName(string fullname);
        string GetFullNameByUserId(Guid userId);
        string GetUserIdByAlias(string Alias);//added by YiShao at 2009-6-10
        #endregion

        #region RelatedReferrer
        int InsertRelatedReferrer(RelatedReferrer obj);
        void UpdateRelatedReferrer(RelatedReferrer obj);
        void DeleteRelatedReferrer(int relatedrefferId);
        RelatedReferrer GetRelatedReferrer(int relatedrefferId);
        List<RelatedReferrer> GetRelatedReferrerForReferral(int referralId);
        #endregion

        #region SiteGeneralInfo
        SiteGeneralInfo GetSiteGeneralInfo();
        #endregion

        #region Comment
        int InsertComment(Comment obj);
        void UpdateComment(Comment obj);
        void DeleteComment(int commentId);
        Comment GetCommentById(int commentId);
        DataSet GetCommentForApplicant(Guid applicantId);
        #endregion

        #region University
        //Int32 InsertUniversity(String continent, String country, String university);
        //Int32 UpdateUniversity(Int32 universityID,String continent, String country, String university);
        //Int32 DeleteUniversity(Int32 universityID);
        
        #endregion

        #region CheckInForm
        int InsertCheckInForm(CheckInForm checkInForm);
        CheckInForm GetCheckInFormById(int id);
        void UpdateCheckInForm(CheckInForm checkInForm);
        DataSet GetAllCheckinFormforHiringReport(DateTime StartDate, DateTime EndDate);
        #endregion

        #region PerformanceAssessment
        Guid InsertPerformanceAssessment(PerformanceAssessment obj);
        bool UpdatePerformanceAssessment(PerformanceAssessment obj);
        bool DeletePerformanceAssessmentById(Guid id);
        PerformanceAssessment GetPerformanceAssessmentById(Guid id);
        DataSet GetPABasicInfoByApplicantId(Guid ApplicantId);
        DataSet GetPerformanceAssessmentByApplicantId(Guid ApplicantId);
        DataSet GetPerformanceAssessment();
        DataSet GetPAReportForHR();//
        DataSet GetPerformanceAssessmentByModifyTime(DateTime StartTime, DateTime EndTime);
        DataSet GetUncompletedPAbyApplicantId(Guid ApplicantId, DateTime BeginTime);
        #endregion

        /*
         * CheckoutSurvey
         * Add by Yuanqin, 2011.5.30
         */
        #region CheckoutSurvey
        Guid InsertCheckoutSurvey(MSRA.Springfield.Components.BizObjects.CheckoutSurvey obj);
        bool UpdateCheckoutSurvey(MSRA.Springfield.Components.BizObjects.CheckoutSurvey obj);
        bool DeleteCheckoutSurveyById(Guid id);
        MSRA.Springfield.Components.BizObjects.CheckoutSurvey GetCheckoutSurveyById(Guid id);
        DataSet GetCheckoutSurveyByApplicantId(Guid ApplicantId);
        DataSet GetCheckoutSurvey();
        DataSet GetSurveyReport();//
        DataSet GetSurveyBasicInfoByApplicantId(Guid id);//########add by bin###############
        #endregion

        #region Intern Publication
        Guid InsertInternPublication(InternPublication obj);
        bool UpdateInternPublication(InternPublication obj);
        bool DeleteInternPublicationById(Guid id);
        DataSet GetInternPublicationByPAId(Guid PAId);
        InternPublication GetInternPublicationById(Guid PublicationId);
        #endregion

        #region CashedApplicantInfo
        bool AddCashedApplicant(Guid ApplicantId, int InterviewId);
        DataSet GetAllApplcantsforSourcingReport(DateTime StartTime, DateTime EndTime);
        #endregion        
    }
}
