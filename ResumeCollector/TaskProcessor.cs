using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ResumeCollector.Lib;
using System.IO;

namespace ResumeCollector
{
    public partial class TaskProcessor : Form
    {
        private CollectionTask m_CurTask;
        private TaskState m_CurTaksState;

        private const string PAUSE = "Pause";
        private const string RESUME = "Resume";
        private const string START = "Start";
        private const string STOP = "Stop";

        private string m_ResumeLocation;
        private string m_ArchiveLocation;
        private string m_FailedLocation;

        private bool m_IsPaused;
        private bool m_IsWorking;

        public TaskProcessor(string resumeLocation, string archiveLocation, string failedLocation)
        {
            InitializeComponent();

            m_ResumeLocation = resumeLocation;
            m_ArchiveLocation = archiveLocation;
            m_FailedLocation = failedLocation;

            InitTask();
        }

        private void InitTask()
        {
            m_CurTaksState = new TaskState();
            //taskRunner = new BackgroundWorker();
            m_CurTask = new CollectionTask(taskRunner, m_CurTaksState, m_ResumeLocation, m_ArchiveLocation, m_FailedLocation);

            pbWholeTask.Maximum = m_CurTask.ResumeCount;
            pbWholeTask.Minimum = 0;
            pbWholeTask.Step = 1;

            pbSingleResume.Maximum = 3;
            pbSingleResume.Minimum = 0;
            pbSingleResume.Step = 1;

            //m_CurTask.NextResume += new EventHandler(m_CurTask_NextResume);
            //m_CurTask.StepTask += new EventHandler(m_CurTask_StepTask);

            m_IsPaused = false;
            m_IsWorking = false;

            btnHide.Enabled = false;
            btnStep.Enabled = false;
        }

        //void m_CurTask_StepTask(object sender, EventArgs e)
        //{
        //    pbSingleResume.PerformStep();
        //}

        //void m_CurTask_NextResume(object sender, EventArgs e)
        //{
        //    pbWholeTask.PerformStep();
        //}
        public void Start()
        {
            btnStop_Click(null, null);
        }
        private void btnStop_Click(object sender, EventArgs e)
        {
            if (m_IsWorking)
            {
                m_CurTask.Pause();
                if (AskToStop())
                {
                    CancelTask();
                    btnStop.Text = START;
                    this.FormClosing -= TaskProcessor_FormClosing;
                    this.Close();
                    this.Owner.Visible = true;
                }
                else
                {
                    m_CurTask.Continue();
                }
            }
            else
            {
                m_IsWorking = true;
                btnStop.Text = STOP;
                btnStep.Enabled = true;
                taskRunner.RunWorkerAsync();
            }
        }

        private void CancelTask()
        {
            if (m_IsWorking)
            {
                m_CurTask.Pause();
                taskRunner.CancelAsync();
                m_CurTask.Stop();
                m_IsWorking = false;

                //summary report
            }
        }

        private bool AskToStop()
        {
            if (MessageBox.Show("Stop current action?", "Stop", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void TaskProcessor_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (AskToStop())
            {
                CancelTask();
                this.FormClosing -= TaskProcessor_FormClosing;
                this.Owner.Visible = true;
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void btnHide_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        private void btnStep_Click(object sender, EventArgs e)
        {
            if (m_IsWorking)
            {
                if (!m_IsPaused)
                {
                    m_IsPaused = true;
                    btnStep.Text = RESUME;
                    m_CurTask.Pause();
                }
                else
                {
                    m_IsPaused = false;
                    btnStep.Text = PAUSE;
                    m_CurTask.Continue();
                }
            }
        }

        private void taskRunner_DoWork(object sender, DoWorkEventArgs e)
        {
            m_CurTask.Start();
        }

        private void taskRunner_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            tbTaskLog.Text = TaskLog.ToPureString();
            if (Convert.ToBoolean(e.UserState))
            {
                pbWholeTask.PerformStep();
                pbSingleResume.Value = 0;
            }
            else
            {
                pbSingleResume.PerformStep();
            }
        }

        private void taskRunner_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnStop.Enabled = false;
            btnStep.Enabled = false;
            m_CurTask.Stop();
            m_IsWorking = false;

            //TaskLog.SaveAs(m_ResumeLocation + "\\log.xml");
            //m_CurTask.CurrentReport.SaveAs(m_FailedLocation + "\\report.html");

            if (e.Error != null)
            {
                //stop unexpectly
                //report summary & return to main window
                MessageBox.Show("The collection task terminate unexpectedly!\nReason:" + e.Error.Message);
            }
            else
            {
                if (e.Cancelled || taskRunner.CancellationPending)
                {
                    MessageBox.Show("User cancelled");
                }
                else
                { 
                    //stop successfully
                    //report summary & return to main window
                    if (File.Exists(m_CurTask.ReportFilename))
                    {
                        if (m_CurTask.NoError)
                        {
                            string strMsg = "Task completed!";
                            MessageBox.Show(this, strMsg, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            string strMsg = "Open failure records report?";
                            if (MessageBox.Show(this, strMsg, "Failure", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                            {
                                //show log for user
                                //MessageBox.Show(TaskLog.ToXMLString());
                                //MessageBox.Show(m_CurTask.CurrentReport.ToString());
                                System.Diagnostics.Process.Start(m_CurTask.ReportFilename);
                            }
                        }
                    }
                }
            }

            this.FormClosing -= TaskProcessor_FormClosing;
            this.Close();
            this.Owner.Visible = true;
        }
    }
}