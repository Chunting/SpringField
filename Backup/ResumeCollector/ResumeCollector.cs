using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ResumeCollector.Lib;
using ResumeCollector.ResumeHelper;
using MSRA.SpringField.Components;
using System.Threading;

namespace ResumeCollector
{
    public partial class MainForm : Form
    {
        private DateTime m_ExecuteTime;
        private TimeSpan m_ScheduleSpan;
        System.Globalization.CultureInfo oldCI = null;


        public MainForm()
        {
            InitializeComponent();            
            oldCI = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

            try
            {
                InitTaskTab();
                InitConfigTab();

                InitScheduleTask();
            }
            catch (System.Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void InitScheduleTask()
        {
            scheduleTimer.Enabled = true;
            scheduleTimer.Start();
            DateTime initTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, AppConfiguration.StartTime, 0, 0);

            TimeSpan scheduleSpan;
            switch (AppConfiguration.DurationSpan)
            {
                case DurationSpanEnum.Daily:
                    scheduleSpan = new TimeSpan(AppConfiguration.DurationNum, 0, 0, 0);
                    break;
                case DurationSpanEnum.Weekly:
                    scheduleSpan = new TimeSpan(7 * AppConfiguration.DurationNum, 0, 0, 0);
                    break;
                case DurationSpanEnum.Monthly:
                    scheduleSpan = new TimeSpan(30 * AppConfiguration.DurationNum, 0, 0, 0);
                    break;
                default:
                    scheduleSpan = new TimeSpan(3, 0, 0, 0);
                    break;
            }
            m_ScheduleSpan = scheduleSpan;

            m_ExecuteTime = initTime + scheduleSpan;
            lbNextTime.Text = string.Format("Next Execute Time: {0} {1}", m_ExecuteTime.ToLongDateString(), m_ExecuteTime.ToLongTimeString());
        }

        private void InitTaskTab()
        {
            tbArchiveLocation.Text = AppConfiguration.ArchiveLocation;
            tbFailedLocation.Text = AppConfiguration.FailedLocation;
            tbResumeLocation.Text = AppConfiguration.ResumeLocation;

            cbNewArchiveFolder.Checked = AppConfiguration.CreateArchiveFolder;
            cbNewFailedFolder.Checked = AppConfiguration.CreateFailedFolder;
        }

        private void InitConfigTab()
        {
            tbArchiveLocationSetting.Text = AppConfiguration.ArchiveLocation;
            tbFailedLocationSetting.Text = AppConfiguration.FailedLocation;
            tbResumeLocationSetting.Text = AppConfiguration.ResumeLocation;

            cbCreateArchiveFolder.Checked = AppConfiguration.CreateArchiveFolder;
            cbCreateFailedFolder.Checked = AppConfiguration.CreateFailedFolder;

            tbRecurrenceNum.Text = AppConfiguration.DurationNum.ToString();
            ddlDuration.SelectedIndex = EnumHelper.EnumToInteger(AppConfiguration.DurationSpan);
            ddlStartTime.SelectedIndex = Convert.ToInt32(AppConfiguration.StartTime);
        }

        private void taskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabsMain.SelectTab(0);
        }

        private void schemeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabsMain.SelectTab(1);
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabsMain.SelectTab(2);
        }

        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AboutBox aboutBox = new AboutBox();
            aboutBox.ShowDialog(this);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnExcute_Click(object sender, EventArgs e)
        {
            if (m_ExecuteTime - DateTime.Now <= new TimeSpan(1, 0, 0, 0))
            {
                m_ExecuteTime += m_ScheduleSpan;
                lbNextTime.Text = string.Format("Next Execute Time: {0} {1}", m_ExecuteTime.ToLongDateString(), m_ExecuteTime.ToLongTimeString());
            }
            TaskProcessor tp = new TaskProcessor(tbResumeLocation.Text, tbArchiveLocation.Text, tbFailedLocation.Text);
            this.Visible = false;
            tp.Start();
            tp.ShowDialog(this);

            System.Threading.Thread.CurrentThread.CurrentCulture = oldCI;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool AskForClose()
        {
            if (MessageBox.Show("Exit current program?", "Exit", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void EndApplication()
        {
            //release resource here
            //TaskLog.Clear();
            //ExcelApplication.Quit();
            scheduleTimer.Stop();
            scheduleTimer.Enabled = false;
            nIcon.Visible = false;
        }

        private void btnResumeLocation_Click(object sender, EventArgs e)
        {
            if (fbdLocation.ShowDialog(this) == DialogResult.OK)
            {
                tbResumeLocation.Text = fbdLocation.SelectedPath;
            }
        }

        private void btnArchiveLocation_Click(object sender, EventArgs e)
        {
            if (fbdLocation.ShowDialog(this) == DialogResult.OK)
            {
                tbArchiveLocation.Text = fbdLocation.SelectedPath;
            }
        }

        private void btnFailedLocation_Click(object sender, EventArgs e)
        {
            if (fbdLocation.ShowDialog(this) == DialogResult.OK)
            {
                tbFailedLocation.Text = fbdLocation.SelectedPath;
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (AskForClose())
            {
                this.FormClosing -= this.MainForm_FormClosing;
                //this.Close();
                //this.Visible = false;
                EndApplication();
                Application.Exit();
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }

        private void btnSetResumeLocation_Click(object sender, EventArgs e)
        {
            if (fbdLocation.ShowDialog(this) == DialogResult.OK)
            {
                tbResumeLocationSetting.Text = fbdLocation.SelectedPath;
            }
        }

        private void btnSetArchiveLocation_Click(object sender, EventArgs e)
        {
            if (fbdLocation.ShowDialog(this) == DialogResult.OK)
            {
                tbArchiveLocationSetting.Text = fbdLocation.SelectedPath;
            }
        }

        private void btnSetFailedLocation_Click(object sender, EventArgs e)
        {
            if (fbdLocation.ShowDialog(this) == DialogResult.OK)
            {
                tbFailedLocationSetting.Text = fbdLocation.SelectedPath;
            }
        }

        private void btnCancelSetting_Click(object sender, EventArgs e)
        {
            InitConfigTab();
        }

        private void btnSaveSetting_Click(object sender, EventArgs e)
        {
            //validate duration num
            int recurrence = 1;
            try
            {
                recurrence = Convert.ToInt32(tbRecurrenceNum.Text);
                if(recurrence <= 0)
                {
                    throw new System.Exception();
                }
            }
            catch
            {
                MessageBox.Show(this, "The recurrence number should be >=0!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            AppConfiguration.CreateFailedFolder = cbCreateFailedFolder.Checked;
            AppConfiguration.CreateArchiveFolder = cbCreateArchiveFolder.Checked;
            AppConfiguration.ResumeLocation = tbResumeLocationSetting.Text;
            AppConfiguration.ArchiveLocation = tbArchiveLocationSetting.Text;
            AppConfiguration.FailedLocation = tbFailedLocationSetting.Text;

            AppConfiguration.StartTime = ddlStartTime.SelectedIndex;
            AppConfiguration.DurationSpan = (DurationSpanEnum)EnumHelper.IntegerToEnum(typeof(DurationSpanEnum), ddlDuration.SelectedIndex);
            AppConfiguration.DurationNum = recurrence;

            MessageBox.Show(this,"You should restart the application before the new configuration take effect","Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        private void scheduleTimer_Tick(object sender, EventArgs e)
        {
            TimeSpan timeSpan = m_ExecuteTime - DateTime.Now;
            lbCounter.Text = string.Format("Reduce Counter: {0} day(s) {1} hour(s) {2} minute(s) {3} second(s)", timeSpan.Days, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
            if (DateTime.Now  >= m_ExecuteTime)
            {
                this.Visible = false;
                activeToolStripMenuItem.Enabled = false;

                nIcon.BalloonTipTitle = "Busy";
                nIcon.BalloonTipText = "Collection task is running now!";
                nIcon.ShowBalloonTip(5000);

                scheduleTimer.Stop();
                //execute the task
                TaskState taskState = new TaskState();
                //scheduleRunner = new BackgroundWorker();
                CollectionTask cTask = new CollectionTask(scheduleRunner, taskState, AppConfiguration.ResumeLocation, AppConfiguration.ArchiveLocation, AppConfiguration.FailedLocation);
                cTask.Start();

                DateTime initTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, AppConfiguration.StartTime, 0, 0);
                m_ExecuteTime = initTime + m_ScheduleSpan;

                lbNextTime.Text = string.Format("Next Execute Time: {0} {1}", m_ExecuteTime.ToLongDateString(), m_ExecuteTime.ToLongTimeString());
                scheduleTimer.Start();
            }
        }

        private void scheduleRunner_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Visible = true;
            this.WindowState = FormWindowState.Normal;

            nIcon.BalloonTipTitle = "Complete";
            nIcon.BalloonTipText = "Collection task complete!";
            nIcon.ShowBalloonTip(5000);
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Visible = false;
                activeToolStripMenuItem.Enabled = true;
            }
        }

        private void activeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Visible = true;
            this.WindowState = FormWindowState.Normal;
            activeToolStripMenuItem.Enabled = false;
        }

        private void nIcon_DoubleClick(object sender, EventArgs e)
        {
            this.Visible = true;
            this.WindowState = FormWindowState.Normal;
            activeToolStripMenuItem.Enabled = false;
        }
    }
}