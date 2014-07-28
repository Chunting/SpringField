/*****************************************************************************

Microsoft Research Asia
Copyright (c) 2006 Microsoft Corporation

Module Name: 
		ApplicantEduBackground.cs

Author:
        Yuan Chen (f-yuchen@msrchina.research.microsoft.com)  
        MSRA/MS^2.3/Compass Team
 
Abstract:
        Class used to store the education background of Applicant

Remarks:
        This class should be used out of the Springfield.Components assembly, so it should be declared as an public class.
             
Environment:
		Class Library/.NET Framework 2.0 

Project:
        Springfield.Components for MSRA Intern Application Tracking System (MIATS), codename: Springfield;
        MSRA/MS^2.3/Compass Team

Revision History:			
		30/Apr/2006 Created by Yuan Chen

*****************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using MSRA.SpringField.Components.Enumerations;
using MSRA.SpringField.Components.Configuration;

namespace MSRA.SpringField.Components.BizObjects
{
    [Serializable]
    public class ApplicantEduBackground
    {
        #region Private Members
        private Guid applicantId;

        //private CollegeEnum highestEduIns;
        private string highestEduIns;

        private MajorCategoryEnum majorCategory;
        private string major;
        private int yearOfStudy;
        private int grade;
        private RankEnum rank;
        private Advisor internAdvisor;
        private Document resume;
        private List<Document> papers;
        private DateTime graduateDate;
        private DateTime enrollDate;
        private ResearchApproachEnum researchApproach;
        private DegreeEnum degree;
        private byte[] resumeImage;
        private string resumeExt;
        //private string degree;
        #endregion

        #region Constructors
        public ApplicantEduBackground()
        {
            highestEduIns = string.Empty;

            rank = RankEnum.Other;
            majorCategory = MajorCategoryEnum.ComputerEERelated;
            researchApproach = ResearchApproachEnum.Theory;
            degree = DegreeEnum.Other;
            //degree = string.Empty;
            applicantId = Guid.Empty ;
            major = string.Empty;
            grade = 0;
            internAdvisor = new Advisor();
            resume = new Document();
            resumeImage = new byte[0];
            resumeExt = string.Empty;
            papers = new List<Document>(2);
            for (int i = 0; i < papers.Capacity; i++)
            {
                papers.Add(new Document());
            }
            graduateDate = DateTime.MaxValue;
            enrollDate = DateTime.MaxValue;
            yearOfStudy = 0;
        }

        //public ApplicantEduBackground(string id)
        //{

        //}
        #endregion

        #region Methods
        public void Insert()
        {
            IDataProvider dp = DataProviderFactory.GetDataProvider();
            dp.InsertApplicantEduBackground(this);
        }

        public void Update()
        {
            IDataProvider dp = DataProviderFactory.GetDataProvider();
            dp.UpdateApplicantEduBackground(this);
        }

        //public void Delete()
        //{

        //}
        #endregion

        #region Static Helper Methods
        public static void DeleteApplicantEduBackgroundById(Guid id)
        {
            IDataProvider dp = DataProviderFactory.GetDataProvider();
            dp.DeleteApplicantEduBackgroundById(id);
        }

        public static ApplicantEduBackground GetApplicantEduBackgroundById(Guid id)
        {
            IDataProvider dp = DataProviderFactory.GetDataProvider();
            return dp.GetApplicantEduBackgroundById(id);
        }
        #endregion

        #region Preperties
        public Guid ApplicantId
        {
            get { return applicantId; }
            set { applicantId = value; }
        }

        //public CollegeEnum HighestEduInstitution
        //{
        //    get { return highestEduIns; }
        //    set { highestEduIns = value; }
        //}

        public string HighestEduInstitution
        {
            get { return highestEduIns; }
            set { highestEduIns = ((value == null) ? string.Empty : value); }
        }

        public MajorCategoryEnum MajorCategory
        {
            get { return majorCategory; }
            set { majorCategory = value; }
        }

        public string Major
        {
            get { return major; }
            set { major = ((value == null) ? string.Empty : value); }
        }

        public int YearOfStudy
        {
            get { return yearOfStudy; }
            set { yearOfStudy = value; }
        }

        public int Grade
        {
            get { return grade; }
            set { grade = value; }
        }

        public RankEnum Rank
        {
            get { return rank; }
            set { rank = value; }
        }

        public Advisor InternAdvisor
        {
            get { return internAdvisor; }
            set { internAdvisor = value; }
        }

        public Document Resume
        {
            get { return resume; }
            set { resume = value; }
        }

        public List<Document> Papers
        {
            get { return papers; }
            set { papers = value; }
        }

        public DateTime EnrollDate
        {
            get { return enrollDate; }
            set { enrollDate = value; }
        }

        public DateTime GraduateDate
        {
            get { return graduateDate; }
            set { graduateDate = value; }
        }

        public ResearchApproachEnum ResearchApproach
        {
            get { return researchApproach; }
            set { researchApproach = value; }
        }

        public DegreeEnum Degree
        {
            get { return degree; }
            set { degree = value; }
        }

        public byte[] ResumeImage
        {
            get { return resumeImage; }
            set { resumeImage = value; }
        }

        public string ResumeExt
        {
            get { return resumeExt; }
            set { resumeExt = value; }
        }
        //public string Degree
        //{
        //    get { return degree; }
        //    set { degree = value; }
        //}
        #endregion

        #region Add Resume to DB
        //static void Main(string[] args)
        //{
        //    string connectionString = @"Persist Security Info=False;Integrated Security=false;user id=compass;password=We're#1!;database=springfield;server=MSRA-SPFIELD;";
        //    string selectString = @"select sf_ApplicantEduBackground.applicantid, sf_Document.filename from sf_ApplicantEduBackground, sf_Document where sf_ApplicantEduBackground.resumeid=sf_Document.DocumentId";
        //    string updateString = @"sf_ZZImportResume";
        //    DataSet ds = SqlHelper.ExecuteDataset(connectionString, CommandType.Text, selectString);
        //    Console.WriteLine("Total: {0}", ds.Tables[0].Rows.Count);
        //    int i = 1;
        //    foreach (DataRow dr in ds.Tables[0].Rows)
        //    {
        //        Console.Write("Current: {0}", i);
        //        i++;
        //        object id = dr[0];
        //        string resumeFile = dr[1].ToString();
        //        string fullPath = Path.Combine(@"E:\Users\xcui\Springfield\docs", resumeFile);
        //        if (File.Exists(fullPath))
        //        {
        //            FileInfo fi = new FileInfo(fullPath);
        //            string ext = fi.Extension;
        //            byte[] stream = GetFileImage(fullPath);

        //            System.Data.SqlClient.SqlParameter[] rgParams = new System.Data.SqlClient.SqlParameter[] {
        //                new System.Data.SqlClient.SqlParameter("@ApplicantId", id),
        //            new System.Data.SqlClient.SqlParameter("@ResumeImage", stream),
        //            new System.Data.SqlClient.SqlParameter("@ResumeExt", ext)
        //            };
        //            try
        //            {
        //                SqlHelper.ExecuteNonQuery(connectionString, updateString, rgParams);
        //                Console.WriteLine("Success.");
        //            }
        //            catch (Exception)
        //            {
        //                Console.WriteLine("Insert DB failed.");
        //            }

        //        }
        //        else
        //        {
        //            Console.WriteLine("Resume not exist.");
        //        }
        //    }
        //}

        //public static byte[] GetFileImage(string ResumePath)
        //{
        //    // open the file
        //    FileStream Stream = new FileStream(ResumePath, FileMode.Open, FileAccess.Read);

        //    // create a buffer which can hold the whole file
        //    Byte[] FileData = new byte[Stream.Length];

        //    // read all the data into the buffer and close the stream
        //    Stream.Read(FileData, 0, Convert.ToInt32(Stream.Length));
        //    Stream.Close();

        //    return FileData;
        //}
        #endregion

    }
}
