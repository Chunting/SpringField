<%@ WebHandler Language="C#" Class="PAPackage" %>

using System;
using System.Web;
using Springfield.Components;
using ICSharpCode.SharpZipLib.Zip;
using System.Text;
using System.Reflection;
using System.Data;

public class PAPackage : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        //if (!(SiteUser.Current.IsInRole(RoleType.InternRecruiter)
        //    || SiteUser.Current.IsInRole(RoleType.OnBoardManager)))
        //{
        //    context.Response.Redirect("~/AccessDeny.htm");
        //}

        string PAId = context.Request["PAId"];
        if (PAId == null)
        {
            PAId = "3433bd82-ccc5-42df-ba53-f86c53286837";
            //context.Response.Redirect("~/Error.aspx");
        }


        ZipOutputStream zipOs = new ZipOutputStream(context.Response.OutputStream);
        string PAinfo = GetPAinfo(new Guid(PAId));
        zipOs.PutNextEntry(new ZipEntry("PerformanceAssessment.txt"));
        byte[] bytesPAinfo = ConvertToArray(PAinfo);
        zipOs.Write(bytesPAinfo, 0, bytesPAinfo.Length);

        zipOs.Finish();
        zipOs.Close();

        context.Response.ContentType = "application/x-zip-compressed";
        context.Response.AppendHeader("Content-Disposition",
            String.Format("attachment; filename={0}", PAId + "_PA.zip"));
        
    }

    private string GetPAinfo(Guid PAId)
    {
        ReportMaker rm = new ReportMaker();
        PerformanceAssessment curPA = PerformanceAssessment.GetPerformanceAssessmentById(PAId);
            
        ///////////////////////////////////////////////////////////////Intern Basic Info    
        rm.AddSection("Intern Basic Information");
        rm.AddField("Intern Name", curPA.InternName.Trim());
        rm.AddField("Intern Phone", curPA.InternPhone.Trim());
        rm.AddField("Intern Email", curPA.InternEmail.Trim());
        rm.AddField("Intern Position", PAResourceManager.IdToText("InternPosition", curPA.InternPosition));
        rm.AddField("Date", "From " + curPA.CheckInDate.ToString("yyyy-MM-dd") + " To " + curPA.CheckOutDate.ToString("yyyy-MM-dd"));
        
        if (curPA.GraduationDate != Convert.ToDateTime("9999-12-30 0:00:00"))
            rm.AddField("Graduation Date", curPA.GraduationDate.ToString("yyyy-MM-dd"));
        else
            rm.AddField("Graduation Date", "N/A");
        
        if (curPA.Department > 0 && PAResourceManager.IdToText("Department", curPA.Department) == "MSRA")
        {
            if (curPA.GroupId > 0)
                rm.AddField("Group", CheckInFormResourceManager.IdToText("Groups", curPA.GroupId));
            else
                rm.AddField("Group", "N/A");
        }
        
        rm.AddField("Mentor Name", curPA.MentorName.Trim());
        rm.AddField("Mentor Alias", curPA.MentorAlias.Trim());
        rm.AddField("Group Manager Alias", SiteUser.GetAliasByUserId(curPA.GroupMgrId).Trim());
        rm.AddField("Project", CheckInFormResourceManager.IdToText("Projects", curPA.ProjectId));  
              
        if (curPA.Department > 0 && PAResourceManager.IdToText("Department", curPA.Department) == "STC")
        {
            if (curPA.Discipline > 0)
                rm.AddField("Discipline", CheckInFormResourceManager.IdToText("Positions", curPA.Discipline));
            else
                rm.AddField("Discipline", "N/A");
            
            if(!String.IsNullOrEmpty(curPA.Pipeline.Trim()))
                rm.AddField("Project-based or FTE pipeline", PAResourceManager.IdToText("ProjectBasedorFTEPipeline",Convert.ToInt32(curPA.Pipeline)));
            else
                rm.AddField("Project-based or FTE pipeline", "N/A");
        }
        //////////////////////////////////////////////////////////////
        
        /////////////////////////////////////////////////////////////Intern PA
        rm.AddSection("INTERN¡¯S PERFORMANCE ASSESSMENT [FINISH BY INTERN]");
        //rm.AddParagraph("Summarize your performance against each objective considering WHAT you have achieved and HOW you have achieved it.");
        rm.AddSubSection("GOAL/OBJECTIVE");
        rm.AddParagraph(curPA.Objective);
        rm.AddSubSection("SELF EVALUATION");
        rm.AddParagraph(curPA.SelfEvaluation);
        //rm.AddParagraph("Please comment on your work assignment, your experience working with your mentor, our organization and the company Microsoft, or this review process.  Please comment on your performance STRENGTHS and WEAKNESSES demonstrated in your daily work here.");
        rm.AddSubSection("STRENGTHS AND WEAKNESSES");
        rm.AddParagraph(curPA.StrengthsAndWeaknesses);
        ///////////////////////////////////////////////////////////////
        
        ///////////////////////////////////////////////////////////////Intern Publications
        if (curPA.Department > 0 && PAResourceManager.IdToText("Department", curPA.Department) == "MSRA")
        {
            rm.AddSection("INFO OF THE PAPERS FINISHED AT MSRA");
            DataSet dsPublications = InternPublication.GetInternPublicationByPAId(curPA.Id);
            if (dsPublications.Tables[0].Rows.Count == 0)
                rm.AddParagraph("None");
            else
            {
                for (int i = 0; i < dsPublications.Tables[0].Rows.Count; i++)
                {
                    rm.AddField("Number", Convert.ToString(i + 1));
                    rm.AddField("Publication", dsPublications.Tables[0].Rows[i]["Name"].ToString());
                    rm.AddField("Status of the paper when they submit", PAResourceManager.IdToText("PaperStatus", Convert.ToInt32(dsPublications.Tables[0].Rows[i]["CurrentStatus"].ToString())));
                    rm.AddParagraph("");
                }
            }
        }
        ////////////////////////////////////////////////////////////////
        
        ////////////////////////////////////////////////////////////////Mentor PA
        rm.AddSection("GENERAL COMMENTS ON PERFORMANCE STRENGTHS AND WEAKNESSES [FINISH BY MENTOR]");
        if (curPA.OverrallEvaluation > 0 && curPA.OverrallEvaluation < 6)
            rm.AddField("Overall evaluation of the student¡¯s performance", PAResourceManager.IdToText("PerformanceLevel", curPA.OverrallEvaluation));
        else
            rm.AddField("Overall evaluation of the student¡¯s performance", "N/A");
        rm.AddSubSection("Detailed evaluation");
        if (curPA.CodingSkill > 0 && curPA.CodingSkill < 6)
            rm.AddField("Coding skill", PAResourceManager.IdToText("PerformanceLevel", curPA.CodingSkill));
        else
            rm.AddField("Coding skill", "N/A");
        if (curPA.AnalyticalSkill > 0 && curPA.AnalyticalSkill < 6)
            rm.AddField("Analytical skill", PAResourceManager.IdToText("PerformanceLevel", curPA.AnalyticalSkill));
        else
            rm.AddField("Analytical skill", "N/A");
        if (curPA.ProblemSolving > 0 && curPA.ProblemSolving < 6)
            rm.AddField("Problem solving", PAResourceManager.IdToText("PerformanceLevel", curPA.ProblemSolving));
        else
            rm.AddField("Problem solving", "N/A");
        if (curPA.Innovation > 0 && curPA.Innovation < 6)
            rm.AddField("Innovation", PAResourceManager.IdToText("PerformanceLevel", curPA.Innovation));
        else
            rm.AddField("Innovation", "N/A");
        if (curPA.DrivingForResults > 0 && curPA.DrivingForResults < 6)
            rm.AddField("Driving for results", PAResourceManager.IdToText("PerformanceLevel", curPA.DrivingForResults));
        else
            rm.AddField("Driving for results", "N/A");
        if (curPA.DealingWithAmbiguity > 0 && curPA.DealingWithAmbiguity < 6)
            rm.AddField("Dealing with Ambiguity", PAResourceManager.IdToText("PerformanceLevel", curPA.DealingWithAmbiguity));
        else
            rm.AddField("Dealing with Ambiguity", "N/A");
        if (curPA.QuickOnLearing > 0 && curPA.QuickOnLearing < 6)
            rm.AddField("Quick on Learning", PAResourceManager.IdToText("PerformanceLevel", curPA.QuickOnLearing));
        else
            rm.AddField("Quick on Learning", "N/A");
        if (curPA.English > 0 && curPA.English < 6)
            rm.AddField("English", PAResourceManager.IdToText("PerformanceLevel", curPA.English));
        else
            rm.AddField("English", "N/A");
        if (curPA.Communication > 0 && curPA.Communication < 6)
            rm.AddField("Communication skills", PAResourceManager.IdToText("PerformanceLevel", curPA.Communication));
        else
            rm.AddField("Communication skills", "N/A");
        if (curPA.TeamWork > 0 && curPA.TeamWork < 6)
            rm.AddField("Team work", PAResourceManager.IdToText("PerformanceLevel", curPA.TeamWork));
        else
            rm.AddField("Team work", "N/A");
        if (curPA.Attitude > 0 && curPA.Attitude < 6)
            rm.AddField("Attitude", PAResourceManager.IdToText("PerformanceLevel", curPA.Attitude));
        else
            rm.AddField("Attitude", "N/A");
        
        rm.AddSubSection("Microsoft Core Values");
        if (curPA.IntegrityHonesty > 0 && curPA.IntegrityHonesty < 6)
            rm.AddField("Integrity & Honesty", PAResourceManager.IdToText("PerformanceLevel", curPA.IntegrityHonesty));
        else
            rm.AddField("Integrity & Honesty", "N/A");
        if (curPA.OpenRespectful > 0 && curPA.OpenRespectful < 6)
            rm.AddField("Open & Respectful", PAResourceManager.IdToText("PerformanceLevel", curPA.OpenRespectful));
        else
            rm.AddField("Open & Respectful", "N/A");
        if (curPA.BigChallenges > 0 && curPA.BigChallenges < 6)
            rm.AddField("Big Challenges", PAResourceManager.IdToText("PerformanceLevel", curPA.BigChallenges));
        else
            rm.AddField("Big Challenges", "N/A");
        if (curPA.Passion > 0 && curPA.Passion < 6)
            rm.AddField("Passion", PAResourceManager.IdToText("PerformanceLevel", curPA.Passion));
        else
            rm.AddField("Passion", "N/A");
        if (curPA.Accountable > 0 && curPA.Accountable < 6)
            rm.AddField("Accountable", PAResourceManager.IdToText("PerformanceLevel", curPA.Accountable));
        else
            rm.AddField("Accountable", "N/A");
        if (curPA.SelfCritical > 0 && curPA.SelfCritical < 6)
            rm.AddField("Self-Critical", PAResourceManager.IdToText("PerformanceLevel", curPA.SelfCritical));
        else
            rm.AddField("Self-Critical", "N/A");
        rm.AddSubSection("Comments");
        rm.AddParagraph(curPA.MentorComments);
        rm.AddSubSection("Strength");
        rm.AddParagraph(curPA.MentorStrength);        
        rm.AddSubSection("Weakness");
        rm.AddParagraph(curPA.MentorWeakness);
        
        if (curPA.Department > 0)
        {
            if (PAResourceManager.IdToText("Department", curPA.Department) == "MSRA")
            {
                if (curPA.HiredAsFTE > 0)
                {
                    rm.AddSection("INFORMATION COLLECTION [FINISH BY MENTOR]");
                    rm.AddParagraph("1.	Do you want to hire this intern as FTE if he/she is qualified?");
                    rm.AddParagraph(PAResourceManager.IdToText("HiredAsFTE_MSRA", curPA.HiredAsFTE));
                }
                else
                {
                    rm.AddSection("INFORMATION COLLECTION [FINISH BY MENTOR]");
                    rm.AddParagraph("1.	Do you want to hire this intern as FTE if he/she is qualified?");
                    rm.AddParagraph("N/A");
                }
            }
            else if (PAResourceManager.IdToText("Department", curPA.Department) == "STC")
            {
                if (curPA.HiredAsFTE > 0)
                {
                    rm.AddSection("INFORMATION COLLECTION [FINISH BY MENTOR]");
                    rm.AddParagraph("1.	Do you want to hire this intern as FTE if he/she is qualified?");
                    rm.AddParagraph(PAResourceManager.IdToText("HiredAsFTE", curPA.HiredAsFTE));
                }
                else
                {
                    rm.AddSection("INFORMATION COLLECTION [FINISH BY MENTOR]");
                    rm.AddParagraph("1.	Do you want to hire this intern as FTE if he/she is qualified?");
                    rm.AddParagraph("N/A");
                }
                if (curPA.OnsiteInterviewNow > 0)
                {
                    rm.AddParagraph("2.	Is this intern ready for onsite-interview now?");
                    rm.AddParagraph(PAResourceManager.IdToText("OnsiteInterviewNow", curPA.OnsiteInterviewNow));
                }
                if (curPA.ExpectedOnsiteInterviewDate != Convert.ToDateTime("9999-12-30 0:00:00"))
                {
                    rm.AddParagraph("3.	If the intern is not ready for onsite-interview, when do you expect him/her  be ready? ");
                    rm.AddParagraph(curPA.ExpectedOnsiteInterviewDate.ToString("yyyy-MM-dd"));
                }

                rm.AddParagraph("4.	If the intern is not hirable, we suggest that you do not continue his/her internship.For project-based intern, do you want to extend his/her service period?");
                if (curPA.ExtendPeriod != 0)
                {
                    rm.AddParagraph("Yes. " + curPA.ExtendPeriod.ToString() + " Months ");
                }
                else
                {
                    rm.AddParagraph("No");
                }
            }
        }
        return rm.GetResult();
    }
    private static byte[] ConvertToArray(string str)
    {
        return Encoding.GetEncoding("utf-8").GetBytes(str);
    }
    public bool IsReusable {
        get {
            return false;
        }
    }

}

public class ReportMaker
{
    StringBuilder sb = new StringBuilder();
    public void AddSection(string sectionName)
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

    public void AddParagraph(string Paragraph)
    {
        sb.Append("\r\n");
        sb.Append(Paragraph);
        sb.Append("\r\n");
    }
    
    public void AddField(string fieldName, string value)
    {
        sb.Append(fieldName + ": " + value + "\r\n");
    }

    public string GetResult()
    {
        return sb.ToString();
    }
    public void AddObjectProperties(object obj)
    {
        Type type = obj.GetType();
        PropertyInfo[] pi = type.GetProperties();
        foreach (PropertyInfo prop in pi)
        {
            //FieldInfo fi = type.GetProperty(prop.Name, BindingFlags.GetProperty);
            if (prop.PropertyType == typeof(string))
            {
                AddField(prop.Name, (string)prop.GetValue(obj, null));
            }
            else if (prop.PropertyType == typeof(DateTime))
            {
                AddField(prop.Name, ((DateTime)prop.GetValue(obj, null)).ToString());
            }
            else if (prop.PropertyType.IsSubclassOf(typeof(Enum)))
            {
                AddField(prop.Name, EnumHelper.EnumToString(prop.GetValue(obj, null)));
            }
        }
    }
}