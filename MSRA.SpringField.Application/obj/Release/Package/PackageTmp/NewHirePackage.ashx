<%@ WebHandler Language="C#" Class="NewHirePackage" %>

using System;
using System.Web;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;
using System.Text;
using System.Reflection;
using System.Data;
using MSRA.SpringField.Components.BizObjects;
using MSRA.SpringField.Components.Enumerations;
using MSRA.SpringField.Components;
using MSRA.SpringField.Components.Configuration;

public class NewHirePackage : IHttpHandler {

    public static string ConvertToString(byte[] data)
    {
        return Encoding.GetEncoding("utf-8").GetString(data, 0, data.Length);
    }

    public static byte[] ConvertToArray(string str)
    {
        return Encoding.GetEncoding("utf-8").GetBytes(str);
    }

    public void ProcessRequest (HttpContext context) {
        
        if (!(SiteUser.Current.IsInRole(RoleType.InternRecruiter)
            ||SiteUser.Current.IsInRole(RoleType.OnBoardManager)))
             
        {
            context.Response.Redirect("~/AccessDeny.htm");
        }

        string id = context.Request["applicantid"];
        if (id == null)
        {
            context.Response.Redirect("~/Error.aspx");
        }

        
        ZipOutputStream zipOs = new ZipOutputStream(context.Response.OutputStream);
        
        //resume
        //try
        //{
        //    string resumePath = GetResumePath(new Guid(id));
        //    string resumeShortName = GetShortName(resumePath);
        //    ZipEntry resumeEntry = new ZipEntry(resumeShortName);
        //    zipOs.PutNextEntry(resumeEntry);
        //    byte[] bytes = File.ReadAllBytes(resumePath);
        //    zipOs.Write(bytes, 0, bytes.Length);
        //}
        //catch (FileNotFoundException e)
        //{
            ApplicantEduBackground aeb = ApplicantEduBackground.GetApplicantEduBackgroundById(new Guid(id));
            byte[] bytes = aeb.ResumeImage;
            ZipEntry resumeEntry = new ZipEntry(id +"_cv" + aeb.ResumeExt);
            zipOs.PutNextEntry(resumeEntry);
            zipOs.Write(bytes, 0, bytes.Length);
        //}

        //Approval ( added by Yi Shao at 2009-6-24)
        DataSet dsInternview = Interview.GetInterviewForApplicant(new Guid(id));
        int InterviewNum = dsInternview.Tables[0].Rows.Count;
        if (InterviewNum > 0)
        {
            if (!String.IsNullOrEmpty(dsInternview.Tables[0].Rows[InterviewNum - 1]["GMApprovalDocId"].ToString()) && Convert.ToInt32(dsInternview.Tables[0].Rows[InterviewNum - 1]["GMApprovalDocId"].ToString()) > 0)
            {
                Document doc = Document.GetDocumentById(Convert.ToInt32(dsInternview.Tables[0].Rows[InterviewNum - 1]["GMApprovalDocId"].ToString()));
                string ApprovalPath = MSRA.SpringField.Components.Configuration.SiteConfiguration.GetConfig().SiteAttributes["docPath"] + doc.SaveName;
                if (File.Exists(ApprovalPath))
                {
                    ZipEntry ApprovalEntry = new ZipEntry(doc.SaveName);
                    zipOs.PutNextEntry(ApprovalEntry);
                    byte[] Approvalbytes = File.ReadAllBytes(ApprovalPath);
                    zipOs.Write(Approvalbytes, 0, Approvalbytes.Length);
                }
            }
            if (!String.IsNullOrEmpty(dsInternview.Tables[0].Rows[InterviewNum - 1]["MentorApprovalDocId"].ToString()) && Convert.ToInt32(dsInternview.Tables[0].Rows[InterviewNum - 1]["MentorApprovalDocId"].ToString()) > 0)
            {
                Document doc = Document.GetDocumentById(Convert.ToInt32(dsInternview.Tables[0].Rows[InterviewNum - 1]["MentorApprovalDocId"].ToString()));
                string ApprovalPath = SiteConfiguration.GetConfig().SiteAttributes["docPath"] + doc.SaveName;
                if (File.Exists(ApprovalPath))
                {
                    ZipEntry ApprovalEntry = new ZipEntry(doc.SaveName);
                    zipOs.PutNextEntry(ApprovalEntry);
                    byte[] Approvalbytes = File.ReadAllBytes(ApprovalPath);
                    zipOs.Write(Approvalbytes, 0, Approvalbytes.Length);
                }
            }
        }
        
        //interview info
        string interviewInfo = GetInterviewInfo(new Guid(id));
        zipOs.PutNextEntry(new ZipEntry("interview_information.txt"));
        byte[] bytesInterview = ConvertToArray( interviewInfo );
        zipOs.Write(bytesInterview, 0, bytesInterview.Length);
        
        zipOs.Finish();
        zipOs.Close();
        
        context.Response.ContentType = "application/x-zip-compressed";
        context.Response.AppendHeader("Content-Disposition",
            String.Format("attachment; filename={0}", id+".zip"));
        //context.Response.TransmitFile(context.Server.MapPath("~/temp/file.zip"));
        //xmlBackup.Save();
    }

    public string GetShortName(string resumePath)
    {
        int lastIdx = resumePath.LastIndexOf("\\");
        if (lastIdx != -1)
        {
            return resumePath.Substring( lastIdx+1);
        }
        else
        {
            return resumePath;
        }
    }
    public string GetResumePath(Guid applicantId)
    {
        string docPath = SiteConfiguration.GetConfig().SiteAttributes["docPath"];
        ApplicantEduBackground aeb = ApplicantEduBackground.GetApplicantEduBackgroundById( applicantId);
        return docPath + aeb.Resume.SaveName;
    }

    public string GetInterviewInfo(Guid applicantId)
    {
        ApplicantBasicInfo abi = ApplicantBasicInfo.GetApplicantBasicInfoById(applicantId);
        ApplicantEduBackground aeb = ApplicantEduBackground.GetApplicantEduBackgroundById( applicantId);
        ApplicantRelatedInfo ari = ApplicantRelatedInfo.GetApplicantRelatedInfoById( applicantId );
        ReportMaker rm = new ReportMaker();
        
        //Applicant Information
        if( abi != null )
        {
            rm.AddSection("Applicant Personal Information");
            rm.AddField("Name in Chinese", abi.NameInChinese);
            rm.AddField("Gender", EnumHelper.EnumToString(abi.Gender));
            rm.AddField("Nationality", abi.Nationality);
            rm.AddField("Email", abi.Email);
            rm.AddField("Contact Phone Num", abi.PhoneNumber);
            rm.AddField("Current City", abi.CurrentCity);
            rm.AddField("Current Province", abi.CurrentProvince);
            rm.AddField("Current Country", abi.CurrentCountry);
            //rm.AddObjectProperties(abi);     
            
        }
        if (aeb != null)
        {
            rm.AddSection("Applicant Education Background");
            rm.AddField("Highest Educational Institution (College)", aeb.HighestEduInstitution);
            rm.AddField("Major Category", EnumHelper.EnumToString(aeb.MajorCategory));
            rm.AddField("Major", aeb.Major);
            rm.AddField("Grade", aeb.YearOfStudy.ToString());
            rm.AddField("Enroll Date", aeb.EnrollDate.ToShortDateString());
            rm.AddField("Graduate Date", aeb.GraduateDate.ToShortDateString());
            rm.AddField("Rank", aeb.Rank.ToString());

            //rm.AddObjectProperties(aeb);
            Advisor adv = aeb.InternAdvisor;
            if (adv != null)
            {
                rm.AddField("Advisor First Name", adv.FirstName );
                rm.AddField("Advisor Last Name",adv.LastName);
                rm.AddField("Advisor Email",adv.Email );
                rm.AddField("Advisor Organization",adv.Organization);
                //rm.AddObjectProperties(adv);
            }
            rm.AddField("Research Approach", EnumHelper.EnumToString(aeb.ResearchApproach)); 
        }
        if (ari != null)
        {
            rm.AddSection("Applicant Related Information");
            //rm.AddObjectProperties(ari);
            rm.AddField("Preferred Internship Start Date", ari.PreferredAvailStartDate.ToShortDateString());
            rm.AddField("Preferred Internship End Date", ari.SecondaryAvailStartDate.ToShortDateString());
            rm.AddField("Interested Research Group", ari.InterestedGroup);
            rm.AddField("Apply For", EnumHelper.EnumToString(ari.InternshipType));
            rm.AddField("Get Information From", string.Format("Source:[{0}] Channel:[{1}] Detail:[{2}]", ari.JobInfoSource, ari.JobInfoChannel, ari.JobInfoDetail ));
        }
        
        //Interview Information
        Interview interview = Interview.GetInterviewById( Convert.ToInt32(Interview.GetRecentInterviewIdByApplicant(applicantId)));

        if (interview != null)
        {
            rm.AddSection("Complete Interview History");
            DataSet ds = Feedback.GetFeedbackByInterview(interview.InterviewId);
            if( ds!=null )
            {
                DataTable tbl = ds.Tables[0];
                foreach (DataRow dr in tbl.Rows)
                {
                    rm.AddSubSection("Feedback");
                    rm.AddField("Time",dr["InterviewDate"].ToString() );
                    rm.AddField("Interviewer",dr["InterviewerAlias"].ToString() );
                    rm.AddField("Suggestion", Convert.ToBoolean(dr["SuggestionResult"]) ? "Hire" : "Reject");
                    rm.AddField("Comment", "\r\n"+dr["FeedbackContent"].ToString());
                }
            }

            rm.AddSubSection("Mentor's Result");
            rm.AddField("Time", interview.MentorDecisionTime.ToShortDateString());
            rm.AddField("Alias", SiteUser.GetAliasByUserId(interview.HiringManagerId));
            rm.AddField("Suggestion", interview.HiringManagerResult ? "Hire" : "Reject");
            rm.AddField("Comment", "\r\n"+interview.HiringManagerComment);
        }

        if (interview.CheckInFormId != 0)
        {
            CheckInForm form = CheckInForm.GetCheckInFormById( interview.CheckInFormId);
            rm.AddSection("MSRA New Intern On-board Request");
            rm.AddField("1. His/her group", CheckInFormResourceManager.IdToText("Groups", form.GroupId) );
            rm.AddField("2. If your intern belongs to the following project, please choose the according one from the following list",CheckInFormResourceManager.IdToText("Projects", form.ProjectId) );
            rm.AddField("3. Position", CheckInFormResourceManager.IdToText("Positions", form.PositionId) );
            rm.AddField("4. His/Her Intern Type",(form.InternTypeId==1?"Full-time":"Part-time"));
            rm.AddField("5. His/her Mentor (alias)",form.MentorAlias);
            rm.AddField("6. I prefer him/her check-in on",form.PreferCheckInDate.ToShortDateString());
            rm.AddField("7. His/her preferred last working day is (at least three months)",form.PreferLastWorkingDay.ToShortDateString());
            rm.AddField("8. Has he/she got the approval letter from his/her advisor/university?",form.AdvisorApproved?
                "Yes - Please send a copy to MSRA intern support (Alias:msrainte)":
                "No - PERSON will check with new students");
            
            //removed
            //rm.AddField("9. Enroll date in university",form.EnrollDate.ToShortDateString());
            //rm.AddField("   Graduation date in university",form.GraduateDate.ToShortDateString());
            
            rm.AddField("9. Comments",form.Comments);
        }

        rm.AddSection("Group Manager's Result");
        rm.AddField("Time", interview.ManagerDecisionTime.ToShortDateString());
        rm.AddField("Alias", SiteUser.GetAliasByUserId(interview.GroupManagerId));
        rm.AddField("Suggestion", interview.GroupManagerResult ? "Hire" : "Reject");
        rm.AddField("Comment", "\r\n"+interview.HiringManagerComment);
        
        return rm.GetResult();
    }
    
    public bool IsReusable {
        get {
            return false;
        }
    }

    public class ReportMaker
    {
        StringBuilder sb =new StringBuilder();
        public void AddSection( string sectionName )
        {
            sb.Append("\r\n\r\n=============");
            sb.Append(sectionName);
            sb.Append("=============\r\n");
        }
        
        public void AddSubSection(string subSectionName)
        {
            sb.Append("\r\n----");
            sb.Append(subSectionName);
            sb.Append("----\r\n");
        }
        
        public void AddField( string fieldName, string value )
        {
            sb.Append( fieldName + ": " + value + "\r\n");
        }
        
        public string GetResult()
        {
            return sb.ToString();
        }
        public void AddObjectProperties( object obj)
        {
            Type type = obj.GetType();
            PropertyInfo[] pi = type.GetProperties();
            foreach(PropertyInfo prop in pi)
            {
                //FieldInfo fi = type.GetProperty(prop.Name, BindingFlags.GetProperty);
                if ( prop.PropertyType == typeof(string) )
                {
                    AddField(prop.Name, (string)prop.GetValue(obj,null));
                }
                else if (prop.PropertyType == typeof(DateTime))
                {
                    AddField(prop.Name, ((DateTime)prop.GetValue(obj,null)).ToString());
                }
                else if (prop.PropertyType.IsSubclassOf(typeof(Enum)))
                {
                    AddField(prop.Name, EnumHelper.EnumToString(prop.GetValue(obj,null)));
                }
            }
        }
    }
}