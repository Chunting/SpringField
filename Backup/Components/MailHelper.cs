using System;
using MSRA.SpringField.Components.Configuration;
using System.Web.Security;
using System.Collections;
using System.Text.RegularExpressions;
using System.Text;
using System.Data;
using MSRA.SpringField.Components.Enumerations;
using MSRA.SpringField.Components.BizObjects;
using MSRA.Springfield.Components.BizObjects;

/// <summary>
/// Summary description for Class1
/// </summary>
namespace MSRA.SpringField.Components
{
    /* ¶¼±»×¢ÊÍµôÁË
    public class VariableManager
    {
        #region New Way
        private Hashtable htVars = new Hashtable();
        public Guid ApplicantId
        {
            set
            {
                AddApplicantVariables(value);
            }
        }

        public Int32 InterviewId
        {
            set
            {
                AddInterviewVariables(value);
            }
        }

        public Int32 FeedbackId
        {
            set
            {
                AddFeedbackVariables(value);
            }
        }

        public string ApplicantEmail
        {
            set
            {
                AddApplicantEmailVariables(value);
            }
        }

        public string ReferToAlias
        {
            set
            {
                AddReferToAliasVariables(value);
            }
        }

        public VariableManager()
        {
            AddConstantVariables();
        }

        public string ReplaceVars(string strText)
        {
            string strRet = strText;
            foreach (DictionaryEntry entry in htVars)
            {
                strRet = Regex.Replace(strRet, "//" + entry.Key + "//", entry.Value.ToString(), RegexOptions.IgnoreCase);
            }
            return strRet;
        }

        public void AddVars(Guid applicantId, Int32 interviewId, 
            Int32 feedbackId, string applicantEmail, string referToAlias)
        {
            AddConstantVariables();
            if (applicantId != null && applicantId != Guid.Empty)
            {
                AddApplicantVariables( applicantId);
            }
            if (interviewId != -1)
            {
                AddInterviewVariables(interviewId);
            }
            if (feedbackId != -1)
            {
                AddFeedbackVariables(feedbackId);
            }
            if (applicantEmail != null && applicantEmail != string.Empty)
            {
                AddApplicantEmailVariables(applicantEmail);
            }

            if (referToAlias != null && referToAlias != string.Empty)
            {
                AddReferToAliasVariables(referToAlias);
            }
        }
        protected void AddConstantVariables()
        {
            htVars.Add("//ReferMan//", SiteUser.Current.Alias);
            htVars.Add("//Intern Recruiter Email//", config.SiteAttributes["internRecruiterMail"]);
            htVars.Add("//RegisterLink//",
                "http://msra-training/applicationportal/default.aspx");
            htVars.Add("//MIATS//", config.SiteUrl);
            htVars.Add("//Date//", DateTime.Now.ToShortDateString());
            htVars.Add("//Boarder Mail To Alias//", config.SiteAttributes["boardMailToAlias"]);
        }

        protected void AddApplicantVariables(Guid applicantId)
        {
            AddConstantVariables();
            ApplicantBasicInfo abi = ApplicantBasicInfo.GetApplicantBasicInfoById(applicantId);
            htVars.Add("Applicant Name", abi.FirstName + " " + abi.LastName);
            htVars.Add("Applicant Email", abi.Email);
            htVars.Add("ApplicantName", abi.FirstName + " " + abi.LastName);
            htVars.Add("InternName", abi.FirstName + " " + abi.LastName);
            if (abi.ReferralId > 0)
            {
                Referral referral = Referral.GetReferralById(abi.ReferralId);
                string rfAlias = SiteUser.GetAliasByUserId(referral.ReferrerId);
                htVars.Add("Referrer Name", rfAlias);
                htVars.Add("Referrer Alias", rfAlias);
            }
            htVars.Add("//LinkGMApprovalForThisApplicant//",
                            config.SiteUrl + "ShowApplication.aspx?applicant=" + applicantId.ToString());
            htVars.Add("//InterviewDetailLink//",
                            config.SiteUrl
                            + "ShowApplication.aspx?applicant=" + applicantId.ToString() + "&tab=1");
            htVars.Add("//ApplicantInfoLink//",
                                    config.SiteUrl + "ShowApplication.aspx?applicant=" + applicantId.ToString());
            htVars.Add("//ApplicantRecordHyperlink//",
                                    config.SiteUrl + "ShowApplication.aspx?applicant=" + applicantId.ToString());
            htVars.Add("//ApprovalHyperlink//",
                                    config.SiteUrl + "ShowApplication.aspx?applicant=" + applicantId.ToString());
            htVars.Add("//AcceptInterviewLink//", config.SiteUrl + "ShowApplication.aspx?applicant=" + applicantId + "&tab=1");
        }
        protected void AddInterviewVariables(Int32 interviewId)
        {
            Interview interview = Interview.GetInterviewById(interviewId);
            AddApplicantVariables( interview.ApplicantId );
            htVars.Add("//Hiring Manager Name//", SiteUser.GetAliasByUserId(interview.HiringManagerId));
            htVars.Add("//Hiring Manager Alias//", SiteUser.GetAliasByUserId(interview.HiringManagerId));
            htVars.Add("MentorName", SiteUser.GetAliasByUserId(interview.HiringManagerId));
            if (interview.GroupManagerId != null && interview.GroupManagerId != Guid.Empty)
            {
                string gmAlias = SiteUser.GetAliasByUserId(interview.GroupManagerId);
                htVars.Add("//Group Manager Name//", gmAlias);
                htVars.Add("//Group Manager Alias//", gmAlias);

                MembershipUser curUser = Membership.GetUser(interview.GroupManagerId);
                string[] strArr = SiteUser.SplitComment(curUser.Comment);
                string groupName = strArr[1];
                htVars.Add("//GroupName//", groupName);
                htVars.Add("//Group Name//", groupName);
            }
        }

        protected void AddFeedbackVariables(Int32 feedbackId)
        {
            Feedback feedBack = Feedback.GetFeedbackById(feedbackId);
            AddInterviewVariables(feedBack.InterviewId);
            htVars.Add("//ChangeInterviewLink//", config.SiteUrl + "ChangeInterviewer.aspx?Feedback=" + feedBack.FeedBackId.ToString());
            htVars.Add("//2nd Interviewer Name//", feedBack.InterviewerAlias);
            htVars.Add("//Interviewer Name//", feedBack.InterviewerAlias);
            htVars.Add("//Interviewer Alias//", feedBack.InterviewerAlias);
        }

        protected void AddApplicantEmailVariables(string applicantEmail)
        {
            htVars.Add("//email//", applicantEmail);
            htVars.Add("//password//", applicantEmail);
        }

        protected void AddReferToAliasVariables(string referToAlias)
        {
            htVars.Add("//ReferedEmName//", referToAlias);
            htVars.Add("//Refer To Alias//", referToAlias);
        }

        #endregion
    }*/


    public class MailHelper
    {

        /*************************************************************************************
         * steps to add new email template
         *  1.add new type in MailType.cs
         *  1.complete Add***Variables() function
         *  2.complete Fetch***VariablesName() function
         *  3.modify FetchAvailableVariableName() to surport new type
         *  4.modify Page_Init() function of EmailTemplateEditor.aspx.cs to surport new type
         *  5.add email templete to database in SQL Server Management Studio
         *  6. add new Email to database:
                MailHelper mailHelper = new MailHelper();
                mailHelper.Add***Variables();
                mailHelper.SendMail(MailType.***);
         * Added by Yi Shao at 2009-6-21. the MailHelper class isn't wirted by me, I just
         * explain how to use it. hope this can save your time
         * **********************************************************************************/
        private Hashtable htVars = new Hashtable();
        private SiteConfiguration config = SiteConfiguration.GetConfig();
        public MailHelper()
        {
            
        }

        public string ReplaceVars(string strText)
        {
            string strRet = strText;
            MatchCollection matchCollection = Regex.Matches(strText, "//([^<>\"/]*?)//");
            foreach (Match match in matchCollection)
            {
                if (!htVars.ContainsKey(match.Groups[1].ToString()))
                {
                    throw new Exception("Cannot replace //" + match.Groups[1].ToString() + "//");
                }
                else if (htVars[match.Groups[1].ToString()] == null)
                {
                    throw new Exception("Value of //" + match.Groups[1].ToString() + "// is null!");
                }
                strRet = Regex.Replace(strRet, "//" + match.Groups[1].ToString() + "//", htVars[match.Groups[1].ToString()].ToString(), RegexOptions.IgnoreCase);
            }
            /*foreach (DictionaryEntry entry in htVars)
            {
                strRet = Regex.Replace(strRet, "//" + entry.Key + "//", entry.Value.ToString(), RegexOptions.IgnoreCase);
            }*/
            return strRet;
        }

        private static void FetchConstVariableName(ArrayList list)
        {
            list.Add("ReferMan");
            list.Add("RegisterLink");
            list.Add("MIATS");
            list.Add("Date");
            list.Add("SystemSender");
            list.Add("System Sender");
            list.Add("Intern Recruiter Email");
            list.Add("Board Mail To Alias");
            list.Add("Board Mail To Name");
            list.Add("xxx");
        }

        public void AddConstantVariables()
        {
            htVars.Add("ReferMan", SiteUser.Current.Alias);
            htVars.Add("RegisterLink",
                "http://msra-training/applicationportal/default.aspx");
            htVars.Add("MIATS", config.SiteUrl);
            htVars.Add("Date", DateTime.Now.ToShortDateString());
            htVars.Add("SystemSender", config.SiteAttributes["systemSender"]);
            htVars.Add("System Sender", config.SiteAttributes["systemSender"]);
            htVars.Add("Intern Recruiter Email", config.SiteAttributes["internRecruiterMail"]);
            htVars.Add("Board Mail To Alias", config.SiteAttributes["boardMailToAlias"]);
            htVars.Add("Board Mail To Name", config.SiteAttributes["boardMailToName"]);
            htVars.Add("xxx", config.SiteAttributes["boardMailToName"]);  //still usefull?
        }

        public static ArrayList FetchAvailableVariableName(MailType mailType)
        {
            ArrayList list = new ArrayList();
            switch(mailType)
            {
               case MailType.OnBoardReminder:
               case MailType.HireApplicant:
               case MailType.HireReferral:
               case MailType.RejectApplicant:
               case MailType.RejectReferral:
               case MailType.RejectMailToHM:
               case MailType.AskForApproval:
                   FetchInterviewVariableName(list);
                   break;
               case MailType.RequestRegister:
                   FetchApplicantEmailVariableName(list);
                   break;
               case MailType.FeedbackComplete:
               case MailType.InterviewerChange:
               case MailType.ArrangeInterview:
                   FetchFeedbackVariableName(list);
                   break;
               case MailType.ReferTo:
                   FetchApplicantVariableName(list);
                   FetchReferToAliasVariableName(list);
                   break;
               case MailType.Interviewreminder: //special case
                   FetchInterviewReminderVariableName(list);
                   break;
                case MailType.PAIncompleteRemind: //Add by Yuanqin, 2011.5.27
                case MailType.PAReminder:
                case MailType.PANotice:
                    FetchPerformanceAssessmentVariablesName(list);//################
                    break;
                case MailType.DailyPAReport:
                    FetchDailyPAReportVariablesName(list);
                    break;
                case MailType.PAApprovalRejected:
                    FetchPARejectVariableName(list);
                    break;
                case MailType.CheckoutSurvey://##################add by bin#############
                    FetchCheckoutSurveyVariablesName(list);
                    break;
               default:
                   FetchApplicantVariableName(list);
                   break;
            }
            return list;
        }

        private static void FetchPARejectVariableName(ArrayList list)
        {
            list.Add("System Sender");
            list.Add("Mentor Alias");
            list.Add("Intern Recruiter Email");
            list.Add("Mentor Name");
            list.Add("InternName");
            list.Add("xxx");
            list.Add("Date");
            list.Add("applicantid");
        }

        private static void FetchInterviewReminderVariableName(ArrayList list)
        {
            list.Add("Interviewer Name");
            list.Add("Applicant Name");
            list.Add("applicantid");
            list.Add("Date");
            //list.Add("SystemSender"); //not really variable in SpringfieldMailSender
            //list.Add("Applicant Email"); //not really variable in SpringfieldMailSender
        }

        private static void FetchApplicantVariableName(ArrayList list)
        {
            FetchConstVariableName(list);
            list.Add("Applicant Id");
            list.Add("applicantid");
            list.Add("Applicant Name");
            list.Add("ApplicantName");
            list.Add("Applicant Email");
            list.Add("InternName");
            list.Add("Referrer Name");
            list.Add("Referrer Alias");
            list.Add("LinkGMApprovalForThisApplicant");
            list.Add("InterviewDetailLink");
            list.Add("ApplicantInfoLink");
            list.Add("ApplicantRecordHyperlink");
            list.Add("ApprovalHyperlink");
            list.Add("AcceptInterviewLink");
        }

        public void AddApplicantVariables(Guid applicantId)
        {
            AddConstantVariables();
            ApplicantBasicInfo abi = ApplicantBasicInfo.GetApplicantBasicInfoById(applicantId);
            htVars.Add("Applicant Id", applicantId);
            htVars.Add("applicantid", applicantId);
            htVars.Add("Applicant Name", abi.FirstName + " " + abi.LastName);
            htVars.Add("ApplicantName", abi.FirstName + " " + abi.LastName);
            htVars.Add("InternName", abi.FirstName + " " + abi.LastName);
            htVars.Add("Applicant Email", abi.Email);
            if (abi.ReferralId > 0)
            {
                Referral referral = Referral.GetReferralById(abi.ReferralId);
                string rfAlias = SiteUser.GetAliasByUserId(referral.ReferrerId);
                htVars.Add("Referrer Name", rfAlias);
                htVars.Add("Referrer Alias", rfAlias);
            }
            htVars.Add("LinkGMApprovalForThisApplicant",
                            config.SiteUrl + "ShowApplication.aspx?applicant=" + applicantId.ToString());
            htVars.Add("InterviewDetailLink",
                            config.SiteUrl
                            + "ShowApplication.aspx?applicant=" + applicantId.ToString() + "&tab=1");
            htVars.Add("ApplicantInfoLink",
                                    config.SiteUrl + "ShowApplication.aspx?applicant=" + applicantId.ToString());
            htVars.Add("ApplicantRecordHyperlink",
                                    config.SiteUrl + "ShowApplication.aspx?applicant=" + applicantId.ToString());
            htVars.Add("ApprovalHyperlink",
                                    config.SiteUrl + "ShowApplication.aspx?applicant=" + applicantId.ToString());
            htVars.Add("AcceptInterviewLink", config.SiteUrl + "ShowApplication.aspx?applicant=" + applicantId + "&tab=1");
        }

        private static void FetchInterviewVariableName(ArrayList list)
        {
            FetchApplicantVariableName(list);
            list.Add("Hiring Manager Name");
            list.Add("Hiring Manager Alias");
            list.Add("Hiring Manager Id");
            list.Add("MentorName");
            list.Add("Group Manager Name");
            list.Add("Group Manager Alias");
            list.Add("GroupName");
        }


        public void AddInterviewVariables(Int32 interviewId)
        {
            Interview interview = Interview.GetInterviewById(interviewId);
            AddApplicantVariables(interview.ApplicantId);
            htVars.Add("Hiring Manager Name", SiteUser.GetAliasByUserId(interview.HiringManagerId));
            htVars.Add("Hiring Manager Alias", SiteUser.GetAliasByUserId(interview.HiringManagerId));
            htVars.Add("Hiring Manager Id", interview.HiringManagerId);
            htVars.Add("MentorName", SiteUser.GetAliasByUserId(interview.HiringManagerId));
            if (interview.GroupManagerId != null && interview.GroupManagerId != Guid.Empty)
            {
                string gmAlias = SiteUser.GetAliasByUserId(interview.GroupManagerId);
                htVars.Add("Group Manager Name", gmAlias);
                htVars.Add("Group Manager Alias", gmAlias);

                MembershipUser curUser = Membership.GetUser(interview.GroupManagerId);
                string[] strArr = SiteUser.SplitComment(curUser.Comment);
                string groupName = strArr[1];
                htVars.Add("GroupName", groupName);
            }
        }

        private static void FetchFeedbackVariableName(ArrayList list)
        {
            FetchInterviewVariableName(list);
            list.Add("ChangeInterviewLink");
            list.Add("2nd Interviewer Name");
            list.Add("Interviewer Name");
            list.Add("Interviewer Alias");
            list.Add("DueDate");
        }

        public void AddFeedbackVariables(Int32 feedbackId)
        {
            Feedback feedBack = Feedback.GetFeedbackById(feedbackId);
            AddInterviewVariables(feedBack.InterviewId);
            htVars.Add("ChangeInterviewLink", config.SiteUrl + "ChangeInterviewer.aspx?Feedback=" + feedBack.FeedBackId.ToString());
            htVars.Add("2nd Interviewer Name", feedBack.InterviewerAlias);
            htVars.Add("Interviewer Name", feedBack.InterviewerAlias);
            htVars.Add("Interviewer Alias", feedBack.InterviewerAlias);
            htVars.Add("DueDate", feedBack.DueDate.ToShortDateString());
        }

        private static void FetchApplicantEmailVariableName(ArrayList list)
        {
            FetchConstVariableName(list);
            list.Add("email");
            list.Add("password");
            if (!list.Contains("Applicant Id"))
            {
                list.Add("Applicant Id");
            }
        }

        private void CheckAddConstantVariables()
        {
            ArrayList list = new ArrayList();
            FetchConstVariableName(list);
            bool bConstantAlreadyInList = false;
            foreach( string item in list )
            {
                if (htVars.ContainsKey(item))
                {
                    bConstantAlreadyInList = true;
                }
            }
            if (!bConstantAlreadyInList)
            {
                AddConstantVariables();
            }
        }
        public void AddApplicantEmailVariables(string applicantEmail)
        {
            CheckAddConstantVariables();
            htVars.Add("email", applicantEmail);
            htVars.Add("password", applicantEmail);
            htVars.Add("Applicant Id",SiteUser.GetIdByFullName(applicantEmail));
        }

        private static void FetchReferToAliasVariableName(ArrayList list)
        {
            list.Add("ReferedEmName");
            list.Add("Refer To Alias");
        }

        public void AddReferToAliasVariables(string referToAlias)
        {
            CheckAddConstantVariables();
            if (htVars.Contains("ReferedEmName"))
            {
                htVars["ReferedEmName"] = referToAlias;
            }
            else
            {
                htVars.Add("ReferedEmName", referToAlias);
            }

            if (htVars.Contains("Refer To Alias"))
            {
                htVars["Refer To Alias"] = referToAlias;
            }
            else
            {
                htVars.Add("Refer To Alias", referToAlias);
            }            
        }


        private static void FetchPerformanceAssessmentVariablesName(ArrayList list)
        {
            FetchApplicantVariableName(list);
            list.Add("Intern Name");
            list.Add("Mentor PA Link");
            list.Add("Checkin Time");
            list.Add("Checkout Time");
            list.Add("Mentor Name");
            list.Add("Mentor Alias");
            list.Remove("ReferMan");
            list.Add("Group");
            list.Add("Intern Type");
            list.Add("Degree");
            list.Add("Univeristy");
            list.Add("Major");
            list.Add("Graduation Date");
            list.Add("Overall Assessment");
            list.Add("Detail Link");
            list.Add("Group Manager Alias");
            list.Add("Mentor PA Deadline");
            list.Add("Group Manager PA Link");//##############bin
        }
        //
        private static void FetchCheckoutSurveyVariablesName(ArrayList list)
        {
            FetchApplicantVariableName(list);
            list.Add("Intern Name");
            list.Add("Mentor Survey Link");
            list.Add("Mentor Name");
            list.Add("Mentor Alias");          
            

        }

        /// <summary>
        /// Prepare for PA Approval reject mail
        /// </summary>
        /// <param name="paid"></param>
        public void AddPAApprovalRejectedVariables(Guid paid)
        {
            PerformanceAssessment pa = PerformanceAssessment.GetPerformanceAssessmentById(paid);
            AddConstantVariables();
            htVars.Add("applicantid", pa.ApplicantId.ToString());
            htVars.Add("Applicant Id", pa.ApplicantId.ToString());
            htVars.Add("Intern Name", pa.InternName.Trim());
            htVars.Add("Mentor Alias", pa.MentorAlias.Trim());
            htVars.Add("paid", pa.Id.ToString());            
        }

        public void AddPerformanceAssessmentVariables(Guid PAId)
        {
            PerformanceAssessment pa = PerformanceAssessment.GetPerformanceAssessmentById(PAId);
            AddApplicantVariables(pa.ApplicantId);
            htVars.Add("Intern Name", pa.InternName.Trim());

            htVars.Add("Group Manager PA Link", config.SiteAttributes["siteUrl"] + "ShowApplication.aspx?applicant=" + pa.ApplicantId.ToString() 
                + "&tab=2&paid=" + pa.Id.ToString());//#############bin

            htVars.Add("Mentor PA Link", config.SiteAttributes["siteUrl"] + "MentorPA.aspx?PAId=" + pa.Id.ToString());
            htVars.Add("Checkin Time", pa.CheckInDate.ToString("yyyy-MM-dd"));
            htVars.Add("Checkout Time", pa.CheckOutDate.ToString("yyyy-MM-dd"));            
            htVars.Remove("Applicant Email");
            htVars.Add("Applicant Email", pa.InternEmail.Trim());
            htVars.Remove("MentorName");
            htVars.Add("MentorName", pa.MentorName.Trim());
            htVars.Add("Mentor Name", pa.MentorName.Trim());
            htVars.Add("Mentor Alias", pa.MentorAlias.Trim());
            try
            {
                htVars.Add("Group Manager Alias", SiteUser.GetAliasByUserId(pa.GroupMgrId));
                //htVars.Add("Group Manager Name",SiteUser.
            }
            catch
            {
                throw new Exception("failed to get group manager's alias.");
            }
            if(pa.GroupId > 0)
                htVars.Add("Group", CheckInFormResourceManager.IdToText("Groups", pa.GroupId));
            else
                htVars.Add("Group", "N/A");
            htVars.Add("Intern Type", PAResourceManager.IdToText("InternPosition", pa.InternPosition));

            string Degree = "N/A";
            string Univeristy = "N/A";
            string Major = "N/A";
            try
            {                
                ApplicantEduBackground aeb = ApplicantEduBackground.GetApplicantEduBackgroundById(pa.ApplicantId);
                Degree = StaticData.DegreeList[(int)aeb.Degree] + aeb.YearOfStudy.ToString();
                Univeristy = aeb.HighestEduInstitution;
                Major = aeb.Major;
            }
            catch
            {
            }

            htVars.Add("Degree", Degree);
            htVars.Add("Univeristy", Univeristy);
            htVars.Add("Major", Major);
            if (pa.GraduationDate != Convert.ToDateTime("9999-12-30 0:00:00"))
                htVars.Add("Graduation Date", pa.GraduationDate.ToString("yyyy-MM-dd"));
            else
                htVars.Add("Graduation Date", "N/A");
            if(pa.OverrallEvaluation > 0)
                htVars.Add("Overall Assessment", PAResourceManager.IdToText("PerformanceLevel",pa.OverrallEvaluation));
            else
                htVars.Add("Overall Assessment", "N/A");
            htVars.Add("Detail Link", config.SiteAttributes["siteUrl"] + "ShowApplication.aspx?applicant=" + pa.ApplicantId.ToString() +"&tab=2");
            htVars.Add("Mentor PA Deadline", pa.InsertDate.AddMonths(1).ToString("yyyy-MM-dd"));
        }

        /*
         * CheckoutSurvey mail report
         * Add by Yuanqin, 2011.6.1
         */
        public void AddCheckoutSurveyVariables(string internName, Guid ApplicantId)
        {
            AddConstantVariables();
           // htVars.Add("Intern Name", internName);
            htVars.Add("InternName", internName);

            htVars.Add("Applicant Id", ApplicantId);
           // CheckoutSurvey CS = CheckoutSurvey.GetCheckoutSurveyById(SurveyId);


            htVars.Add("Mentor Survey Link", config.SiteAttributes["siteUrl"] + "ShowApplication.aspx?applicant=" + ApplicantId.ToString() + "&tab=3");

            DataSet dsSurvey = PerformanceAssessment.GetPABasicInfoByApplicantId(ApplicantId);
            DataTable tbSurvey = dsSurvey.Tables[0];
            htVars.Add("Mentor Alias", tbSurvey.Rows[0]["MentorAlias"].ToString());

        }
        public void AddNoticeApplicantVariables(Guid ApplicantId)
        {
            AddConstantVariables();

            ApplicantBasicInfo abi = ApplicantBasicInfo.GetApplicantBasicInfoById(ApplicantId);

            htVars.Add("Applicant Id", ApplicantId);
            htVars.Add("Applicant Email", abi.Email);

        }

        private static void FetchDailyPAReportVariablesName(ArrayList list)
        {
            FetchConstVariableName(list);
            list.Add("Start Time");
            list.Add("End Time");
            list.Add("PA Report Table");
            list.Add("Total Number");
        }

        public void AddDailyPAReportVariables(DateTime StartTime, DateTime EndTime)
        {
            int MentorPATimeLimit = Convert.ToInt32(config.SiteAttributes["MentorPADays"]); //day
            string ShowApplicationPageURL = config.SiteUrl + "/ShowApplication.aspx";

            AddConstantVariables();
            htVars.Add("Start Time", StartTime.ToString());
            htVars.Add("End Time", EndTime.ToString());

            StringBuilder PATable = new StringBuilder();
            PATable.Append("<table  cellpadding=\"0\" cellspacing=\"0\">");
            PATable.Append("<tr>");
            PATable.Append("<td style=\"border:1px black solid\">#</td>");
            PATable.Append("<td style=\"border-bottom:1px black solid; border-right:1px black solid; border-top:1px black solid;\"><b>Status</b></td>");
            PATable.Append("<td style=\"border-bottom:1px black solid; border-right:1px black solid; border-top:1px black solid;\"><b>Intern</b></td>");
            PATable.Append("<td style=\"border-bottom:1px black solid; border-right:1px black solid; border-top:1px black solid;\"><b>Group</b></td>");
            PATable.Append("<td style=\"border-bottom:1px black solid; border-right:1px black solid; border-top:1px black solid;\"><b>Mentor Name</b></td>");
            PATable.Append("<td style=\"border-bottom:1px black solid; border-right:1px black solid; border-top:1px black solid;\"><b>Mentor Alias</b></td>");
            PATable.Append("<td style=\"border-bottom:1px black solid; border-right:1px black solid; border-top:1px black solid;\"><b>Intern¡¯s PA</b></td>");
            PATable.Append("<td style=\"border-bottom:1px black solid; border-right:1px black solid; border-top:1px black solid;\"><b>Mentor¡¯s PA</b></td>");
            PATable.Append("<td style=\"border-bottom:1px black solid; border-right:1px black solid; border-top:1px black solid;\"><b>Deadline</b></td>");
            PATable.Append("<td style=\"border-bottom:1px black solid; border-right:1px black solid; border-top:1px black solid;\"><b>Link to the PA</b></td>");
            PATable.Append("</tr>");
            //PATable.Append("");
            DataSet dsPA = PerformanceAssessment.GetPerformanceAssessmentByModifyTime(StartTime, EndTime);
            int PANumber = dsPA.Tables[0].Rows.Count;
            for (int i = 0; i < PANumber; i++)
            {
                PATable.Append("<tr>");

                PATable.Append("<td style=\"border-bottom:1px black solid; border-right:1px black solid; border-left:1px black solid;\">");
                PATable.Append(Convert.ToString(i + 1));
                PATable.Append("</td>");

                PATable.Append("<td style=\"border-bottom:1px black solid; border-right:1px black solid;\">");
                try
                {
                    Guid guidApplicantId = new Guid(dsPA.Tables[0].Rows[i]["ApplicantId"].ToString());
                    PATable.Append(ApplicantBasicInfo.GetApplicantBasicInfoById(guidApplicantId).Status.ToString());
                }
                catch
                {
                    PATable.Append("‡å");
                }
                PATable.Append("</td>");

                PATable.Append("<td style=\"border-bottom:1px black solid; border-right:1px black solid;\">");
                PATable.Append(dsPA.Tables[0].Rows[i]["InternName"].ToString());
                PATable.Append("</td>");

                PATable.Append("<td style=\"border-bottom:1px black solid; border-right:1px black solid;\">");
                try
                {
                    PATable.Append(CheckInFormResourceManager.IdToText("Groups", Convert.ToInt32(dsPA.Tables[0].Rows[i]["GroupId"].ToString())));
                }
                catch
                {
                    PATable.Append("Unknown");
                }
                PATable.Append("</td>");

                PATable.Append("<td style=\"border-bottom:1px black solid; border-right:1px black solid;\">");
                if (String.IsNullOrEmpty(dsPA.Tables[0].Rows[i]["MentorName"].ToString().Trim()))
                {
                    PATable.Append("Unknown");
                }
                else
                {
                    PATable.Append(dsPA.Tables[0].Rows[i]["MentorName"].ToString().Trim());
                }
                PATable.Append("</td>");

                PATable.Append("<td style=\"border-bottom:1px black solid; border-right:1px black solid;\">");
                if (String.IsNullOrEmpty(dsPA.Tables[0].Rows[i]["MentorAlias"].ToString().Trim()))
                {
                    PATable.Append("Unknown");
                }
                else
                {
                    PATable.Append(dsPA.Tables[0].Rows[i]["MentorAlias"].ToString().Trim());
                }
                PATable.Append("</td>");


                PATable.Append("<td style=\"border-bottom:1px black solid; border-right:1px black solid;\">");
                PATable.AppendFormat("{0:yyyy-MM-dd}",dsPA.Tables[0].Rows[i]["InsertDate"].ToString());
                PATable.Append("</td>");

                PATable.Append("<td style=\"border-bottom:1px black solid; border-right:1px black solid;\">");
                try
                {
                    PATable.Append(PAResourceManager.IdToText("PerformanceLevel", Convert.ToInt32(dsPA.Tables[0].Rows[i]["OverrallEvaluation"].ToString())));
                }
                catch
                {
                    PATable.Append("Uncompleted");
                }
                PATable.Append("</td>");

                PATable.Append("<td style=\"border-bottom:1px black solid; border-right:1px black solid;\">");
                try
                {
                    PATable.Append(Convert.ToDateTime(dsPA.Tables[0].Rows[i]["CheckOutDate"].ToString()).AddDays(MentorPATimeLimit).ToString("yyyy-MM-dd"));
                }
                catch
                {
                    PATable.Append("Unknown");
                }
                PATable.Append("</td>");

                PATable.Append("<td style=\"border-bottom:1px black solid; border-right:1px black solid;\">");
                PATable.Append("<a href='" + ShowApplicationPageURL + "?applicant=" + dsPA.Tables[0].Rows[i]["id"].ToString() + "&tab=2'>"+ShowApplicationPageURL + "?applicant=" + dsPA.Tables[0].Rows[i]["id"].ToString() + "&tab=2"+"</a>");
                PATable.Append("</td>");

                PATable.Append("</tr>");
            }

            PATable.Append("</table>");

            htVars.Add("PA Report Table", PATable.ToString());
            htVars.Add("Total Number", PANumber);
            htVars.Add("Applicant Id", Guid.Empty);
        }

        public string FormatMailAddresses(string mailList)
        {
            string[] toAddresses = mailList.Split(';');
            string emailExt = config.SiteAttributes["emailExt"];
            string strRet="";
            bool bFirst = true;
            string toAddress = "";
            foreach (string iter in toAddresses)
            {
                toAddress = iter.Trim();
                if (toAddress == "") continue;
                if( !bFirst){
                    strRet +=";";
                }
                if (GlobalHelper.ValidateEmail(toAddress))
                {
                    strRet += toAddress;
                }
                else
                {
                    strRet += toAddress + emailExt;
                }
                bFirst = false;
            }
            return strRet;
        }

        public void SendMail(MailType mailType)
        {
            if (mailType == MailType.Interviewreminder)
            {
                throw new Exception("Please do not send interviewer reminder mail by MailHelper!");
            }


            Email mail = new Email();
            mail.RelatedUserId = new Guid(htVars["Applicant Id"].ToString());//####

            if (mailType == MailType.ArrangeInterview)
            {
                mail.RelatedUserId = (Guid)htVars["Hiring Manager Id"];
            }

            EmailTemplate mailTemplate = EmailTemplate.GetEmailTemplateByType(mailType);
            mail.From = FormatMailAddresses(ReplaceVars(mailTemplate.From));
            mail.To = FormatMailAddresses(ReplaceVars(mailTemplate.To));
            mail.CC = FormatMailAddresses(ReplaceVars(mailTemplate.CC));
            mail.BCC = FormatMailAddresses(ReplaceVars(mailTemplate.BCC));
            mail.Subject = ReplaceVars(mailTemplate.Subject);
            mail.Body = ReplaceVars(mailTemplate.Body);
            mail.Insert();//insert into dababase then wait to be sent
        }

        public static ArrayList FetchLinkVariableName()
        {
            ArrayList listLinks = new ArrayList();
            listLinks.Add("LinkGMApprovalForThisApplicant");
            listLinks.Add("AcceptInterviewLink");
            listLinks.Add("ChangeInterviewLink");
            listLinks.Add("InterviewDetailLink");
            listLinks.Add("ApplicantInfoLink");
            listLinks.Add("ApplicantRecordHyperlink");
            listLinks.Add("ApprovalHyperlink");
            return listLinks;
        }
    }
}