using System;
using System.Collections.Generic;
using System.Text;
using ResumeCollector.ResumeHelper;
using System.IO;
using ResumeCollector.CustomExceptions;
using System.Threading;
using System.ComponentModel;
using System.Xml;
using System.Security.AccessControl;
using System.Configuration;

namespace ResumeCollector.Lib
{
    public class CollectionTask
    {
        private int m_ResumeCount;
        private int m_CompleteCount;
        private string m_ResumeLocation;
        private string m_ArchiveLocation;
        private string m_FailedLocation;
        private bool m_IsPaused;
        private bool m_Working;
        private BackgroundWorker m_Worker;
        private TaskState m_State;
        private TaskReport m_TaskReport;
        private bool m_noError = true;
        private string m_ReportFilename;

        private DirectoryInfo m_ResumeDirectory;
        private DirectoryInfo m_ArchiveDirectory;
        private DirectoryInfo m_FailedDirectory;

        private static string m_CurDirPath;

        //private FileInfo[] m_ResumeFiles;
        //private DirectoryInfo[] m_ApplicantDirectories;
        private List<DirectoryInfo> m_ApplicantDirectories;

        public TaskReport CurrentReport
        {
            get { return m_TaskReport; }
        }

        public CollectionTask(BackgroundWorker worker, TaskState state, string resumeLocation, string archiveLocation, string failedLocation)
        {
            m_Worker = worker;
            m_State = state;

            m_ResumeLocation = resumeLocation;
            m_ArchiveLocation = archiveLocation;
            m_FailedLocation = failedLocation;

            InitTask();
        }

        //public event EventHandler StartTask;
        //public event EventHandler StopTask;
        //public event EventHandler PauseTask;
        //public event EventHandler ContinueTask;
        //public event EventHandler StepTask;
        //public event EventHandler NextResume;

        private void InitTask()
        { 
            //examine all folders and resources
            //init all pre action

            if (!Directory.Exists(m_ResumeLocation))
            {
                throw new ResumeLocationNotExistException(m_ResumeLocation);
            }
            else
            {
                m_ResumeDirectory = new DirectoryInfo(m_ResumeLocation);
            }

            if(!Directory.Exists(m_ArchiveLocation)) 
            { 
                m_ArchiveDirectory = Directory.CreateDirectory(m_ArchiveLocation);
            }

            if (!Directory.Exists(m_FailedLocation))
            {
                m_FailedDirectory = Directory.CreateDirectory(m_FailedLocation);
            }

            //m_ResumeFiles = m_ResumeDirectory.GetFiles("*.xls");
            //m_ApplicantDirectories = m_ResumeDirectory.GetDirectories();

            //recursive to get the whole directory list
            m_ApplicantDirectories = new List<DirectoryInfo>();
            DirectoryInfo[] topSubFolders = m_ResumeDirectory.GetDirectories();
            foreach (DirectoryInfo curFolder in topSubFolders)
            {
                InitApplicantDirList(curFolder);
            }

            m_ResumeCount = m_ApplicantDirectories.Count;
            m_CompleteCount = 0;
            m_IsPaused = false;
            m_Working = true;

            TaskLog.Clear();
            m_TaskReport = new TaskReport(m_ResumeDirectory.FullName);
        }

        private void InitApplicantDirList(DirectoryInfo root)
        { 
            DirectoryInfo[] subDirArr = root.GetDirectories();
            if (subDirArr.Length > 0)
            {
                foreach (DirectoryInfo subDir in subDirArr)
                {
                    InitApplicantDirList(subDir);
                }
            }
            else
            {
                FileInfo[] rgFiles = root.GetFiles("*.xls*");
                // This folder contain excel files
                if (rgFiles.Length > 0)
                {
                    m_ApplicantDirectories.Add(root);
                }
                else
                {
                    // Logger
                    // This is not a valid folder, since there is no applyforms
 
                }
            }
        }

        #region on event
        //protected void OnStartTask()
        //{
        //    if (StartTask != null)
        //    {
        //        StartTask(this, EventArgs.Empty);
        //    }
        //}

        //protected void OnStopTask()
        //{
        //    if (StopTask != null)
        //    {
        //        StopTask(this, EventArgs.Empty);
        //    }
        //}

        //protected void OnPauseTask()
        //{
        //    if (PauseTask != null)
        //    {
        //        PauseTask(this, EventArgs.Empty);
        //    }
        //}

        //protected void OnStepTask()
        //{
        //    if (StepTask != null)
        //    {
        //        StepTask(this, EventArgs.Empty);
        //    }
        //}

        //protected void OnContinueTask()
        //{
        //    if (ContinueTask != null)
        //    {
        //        ContinueTask(this, EventArgs.Empty);
        //    }
        //}

        //protected void OnNextResume()
        //{
        //    //process one resume
        //    if (NextResume != null)
        //    {
        //        NextResume(this, EventArgs.Empty);
        //    }
        //}
        #endregion

        #region event methods
        public void Start()
        {
            if (m_Working)
            {
                m_Working = true;
                ProcessTask();
                Stop();

            }
            else
            {
                throw new TaskHasStartedException();
            }
        }

        //public void StopProcessTask(IAsyncResult ar)
        //{
        //    if (m_Working)
        //    {
        //        //OnStopTask();
        //        Pause();
        //        ClearResource();
        //        m_Working = false;
        //    }
        //}

        public void Stop()
        {
            if (m_Working)
            {
                //OnStopTask();
                Pause();
                ClearResource();
                m_Working = false;
            }
        }

        public void Pause()
        {
            //m_TaskThread.Suspend();
            m_IsPaused = true;
            //OnPauseTask();
        }

        private void Step()
        {
            m_Worker.ReportProgress(ProgressPercent, false);
            //OnStepTask();
        }

        public void Continue()
        {
            //m_TaskThread.Resume();
            m_IsPaused = false;
            //OnContinueTask();
        }

        private void ProcessNextResume()
        {
            m_CompleteCount++;
            m_Worker.ReportProgress(ProgressPercent, true);
            //OnNextResume();
        }
        #endregion

        private void ClearResource()
        {
            ExcelApplication.Quit();
        }

        private void ProcessTask()
        {
            TaskLog.AddGlobalEntry(TaskLog.ITEM, "begin the collection task at folder [" + m_ResumeLocation + "]");
            //foreach (FileInfo file in m_ResumeFiles)
            int index;
            ResumeWrapper resume = null;
            string filename = string.Empty;
            DirectoryInfo curDirectory = null;
            FileInfo[] allResumeFiles;
            FileInfo curResumeFile;
            string dest;

            try
            {
                NoError = true;
                for (index = m_CompleteCount; index < m_ResumeCount; index++)
                {
                    try
                    {
                        resume = null;
                        curDirectory = m_ApplicantDirectories[index];
                        m_CurDirPath = curDirectory.FullName;
                        allResumeFiles = curDirectory.GetFiles("*.xls");
                        //excel sheet not exist
                        if (allResumeFiles.Length == 0)
                        {
                            throw new ResumeFileNotExistException();
                        }
                        curResumeFile = allResumeFiles[0];
                        filename = curResumeFile.FullName;

                        resume = null;
                        resume = new ResumeWrapper(filename, AppConfiguration.ResumePassword);
                        TaskLog.AddGlobalEntry(TaskLog.ITEM, "begin process file[" + filename + "]");
                        resume.Step += new EventHandler(resume_Step);
                        resume.ProcessResume();
                        resume.Release();
                        //success: move to archive folder
                        dest = MoveToLocation(curDirectory, m_ResumeLocation, m_ArchiveLocation);
                        TaskLog.AddGlobalEntry(TaskLog.ITEM, "success! move current folder to the folder [" + dest + "]");
                    }
                    catch (System.Exception ex)
                    {
                        NoError = false;

                        if (resume != null)
                        {
                            resume.Release();
                        }
                        dest = MoveToLocation(curDirectory, m_ResumeLocation, m_FailedLocation);

                        if (resume != null)
                        {
                            m_TaskReport.RenderEntry(dest, resume.ApplicantEmail, resume.ApplicantName, ex.Message);
                        }
                        else
                        {
                            m_TaskReport.RenderEntry(dest, "N/A", "N/A", ex.Message);
                        }

                        TaskLog.AddGlobalEntry(TaskLog.EXCEPTION, ex.Message);
                        TaskLog.AddGlobalEntry(TaskLog.ITEM, "failed when processing the folder[" + curDirectory.FullName + "]");
                        TaskLog.AddGlobalEntry(TaskLog.ITEM, "move current folder to the folder[" + dest + "]");
                        //failed: move to failed folder
                    }
                    finally
                    {
                        if (resume != null)
                        {
                            resume.Step -= this.resume_Step;
                        }
                        TaskLog.AddGlobalEntry(TaskLog.ITEM, "end process file [" + filename + "]");
                        ProcessNextResume();
                        while (m_IsPaused) { Thread.Sleep(10); }
                    }
                }

                TaskLog.AddGlobalEntry(TaskLog.ITEM, "end the collection task at folder [" + m_ResumeLocation + "]");
                ClearAllDirectory();
            }
            finally
            {
                m_TaskReport.End();
                
                //save log & report
                string timeFilename = GenerateTimeFilename();
                string logFilename = Path.Combine(AppConfiguration.LogLocation, timeFilename + "_log.xml");
                string reportFilename = Path.Combine(AppConfiguration.LogLocation, timeFilename + "_report.html");
                TaskLog.SaveAs(logFilename);
                m_TaskReport.SaveAs(reportFilename);

                m_ReportFilename = reportFilename;

                //send notify email
                List<string> attachments = new List<string>();
                attachments.Add(logFilename);
                attachments.Add(reportFilename);
                try
                {
                    if (bool.Parse(ConfigurationManager.AppSettings["Mail4Log"].ToString()))
                    {
                        EmailSender.SendEmailByLocalOutlook(AppConfiguration.EmailReceiver, null,
                            "Collection Task Complete", "Collection work has completed. Attachments are the reports of the collection work. Please check it up.",
                            attachments.ToArray());
                    }
                }
                catch (System.Exception e)
                {
                    //do nothing
                    throw e;
                }
            }
        }

        private string GenerateTimeFilename()
        {
            string year = DateTime.Now.Year.ToString();
            string month = DateTime.Now.Month.ToString();
            string day = DateTime.Now.Day.ToString();
            string hour = DateTime.Now.Hour.ToString();
            string minute = DateTime.Now.Minute.ToString();
            string second = DateTime.Now.Second.ToString();

            StringBuilder sbFilename = new StringBuilder();
            sbFilename.Append(year);
            sbFilename.Append("-");
            sbFilename.Append(month);
            sbFilename.Append("-");
            sbFilename.Append(day);
            sbFilename.Append(" ");
            sbFilename.Append(hour);
            sbFilename.Append(".");
            sbFilename.Append(minute);
            sbFilename.Append(".");
            sbFilename.Append(second);

            return sbFilename.ToString();
        }

        private void ClearAllDirectory()
        {
            DirectoryInfo[] subDirs = m_ResumeDirectory.GetDirectories();
            foreach (DirectoryInfo curSub in subDirs)
            {
                if (curSub.GetFiles().Length == 0)
                {
                    curSub.Delete(true);
                }
            }
        }

        private void resume_Step(object sender, EventArgs e)
        {
            Step();
        }

        private string MoveToLocation(DirectoryInfo curDir, string replacement, string destDir)
        {
            string destLocation = curDir.FullName.Replace(replacement, destDir+"\\");

            if (Directory.Exists(destLocation))
            {
                Directory.Delete(destLocation, true);                
            }
            
            curDir.MoveTo(destLocation);
            return destLocation;
        }

        #region Properties

        public bool NoError
        {
            get { return m_noError; }
            set { m_noError = value; }
        }

        public static string CurrentDirectory
        {
            get { return m_CurDirPath; }
        }

        public int ResumeCount
        {
            get { return m_ResumeCount; }
        }

        public int RestCount
        {
            get { return (m_ResumeCount - m_CompleteCount); }
        }

        public int CompleteCount
        {
            get { return m_CompleteCount; }
        }

        public int ProgressPercent
        {
            get { return Convert.ToInt32((Convert.ToDouble(m_CompleteCount) / Convert.ToDouble(m_ResumeCount)) * 100); }
        }

        public bool IsWorking
        {
            get { return m_Working; }
        }

        public bool IsPaused
        {
            get { return m_IsPaused; }
        }

        public string ReportFilename
        {
            get { return m_ReportFilename; }
        }
        #endregion
    }
}
