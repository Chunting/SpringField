namespace ResumeCollector
{
    partial class TaskProcessor
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TaskProcessor));
            this.gpMonitor = new System.Windows.Forms.GroupBox();
            this.tbTaskLog = new System.Windows.Forms.TextBox();
            this.lbCurrentResume = new System.Windows.Forms.Label();
            this.lbResumePath = new System.Windows.Forms.Label();
            this.pbSingleResume = new System.Windows.Forms.ProgressBar();
            this.lbWholeProgress = new System.Windows.Forms.Label();
            this.pbWholeTask = new System.Windows.Forms.ProgressBar();
            this.lbProgressIndicator = new System.Windows.Forms.Label();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnStep = new System.Windows.Forms.Button();
            this.btnHide = new System.Windows.Forms.Button();
            this.taskRunner = new System.ComponentModel.BackgroundWorker();
            this.gpMonitor.SuspendLayout();
            this.SuspendLayout();
            // 
            // gpMonitor
            // 
            this.gpMonitor.Controls.Add(this.tbTaskLog);
            this.gpMonitor.Location = new System.Drawing.Point(12, 12);
            this.gpMonitor.Name = "gpMonitor";
            this.gpMonitor.Size = new System.Drawing.Size(470, 150);
            this.gpMonitor.TabIndex = 0;
            this.gpMonitor.TabStop = false;
            this.gpMonitor.Text = "Task porcessor monitor:";
            // 
            // tbTaskLog
            // 
            this.tbTaskLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbTaskLog.Location = new System.Drawing.Point(3, 18);
            this.tbTaskLog.Multiline = true;
            this.tbTaskLog.Name = "tbTaskLog";
            this.tbTaskLog.ReadOnly = true;
            this.tbTaskLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbTaskLog.Size = new System.Drawing.Size(464, 129);
            this.tbTaskLog.TabIndex = 0;
            // 
            // lbCurrentResume
            // 
            this.lbCurrentResume.AutoSize = true;
            this.lbCurrentResume.Location = new System.Drawing.Point(12, 177);
            this.lbCurrentResume.Name = "lbCurrentResume";
            this.lbCurrentResume.Size = new System.Drawing.Size(101, 16);
            this.lbCurrentResume.TabIndex = 1;
            this.lbCurrentResume.Text = "Current resume:";
            // 
            // lbResumePath
            // 
            this.lbResumePath.AutoSize = true;
            this.lbResumePath.Location = new System.Drawing.Point(119, 177);
            this.lbResumePath.Name = "lbResumePath";
            this.lbResumePath.Size = new System.Drawing.Size(0, 16);
            this.lbResumePath.TabIndex = 2;
            // 
            // pbSingleResume
            // 
            this.pbSingleResume.Location = new System.Drawing.Point(12, 196);
            this.pbSingleResume.Name = "pbSingleResume";
            this.pbSingleResume.Size = new System.Drawing.Size(470, 20);
            this.pbSingleResume.TabIndex = 3;
            // 
            // lbWholeProgress
            // 
            this.lbWholeProgress.AutoSize = true;
            this.lbWholeProgress.Location = new System.Drawing.Point(15, 224);
            this.lbWholeProgress.Name = "lbWholeProgress";
            this.lbWholeProgress.Size = new System.Drawing.Size(107, 16);
            this.lbWholeProgress.TabIndex = 4;
            this.lbWholeProgress.Text = "Whole progress:";
            // 
            // pbWholeTask
            // 
            this.pbWholeTask.Location = new System.Drawing.Point(12, 244);
            this.pbWholeTask.Name = "pbWholeTask";
            this.pbWholeTask.Size = new System.Drawing.Size(470, 20);
            this.pbWholeTask.TabIndex = 5;
            // 
            // lbProgressIndicator
            // 
            this.lbProgressIndicator.AutoSize = true;
            this.lbProgressIndicator.Location = new System.Drawing.Point(129, 224);
            this.lbProgressIndicator.Name = "lbProgressIndicator";
            this.lbProgressIndicator.Size = new System.Drawing.Size(0, 16);
            this.lbProgressIndicator.TabIndex = 6;
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(382, 293);
            this.btnStop.Margin = new System.Windows.Forms.Padding(4);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(100, 30);
            this.btnStop.TabIndex = 10;
            this.btnStop.Text = "Start";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnStep
            // 
            this.btnStep.Location = new System.Drawing.Point(262, 293);
            this.btnStep.Margin = new System.Windows.Forms.Padding(4);
            this.btnStep.Name = "btnStep";
            this.btnStep.Size = new System.Drawing.Size(100, 30);
            this.btnStep.TabIndex = 9;
            this.btnStep.Text = "Pause";
            this.btnStep.UseVisualStyleBackColor = true;
            this.btnStep.Click += new System.EventHandler(this.btnStep_Click);
            // 
            // btnHide
            // 
            this.btnHide.Location = new System.Drawing.Point(142, 293);
            this.btnHide.Margin = new System.Windows.Forms.Padding(4);
            this.btnHide.Name = "btnHide";
            this.btnHide.Size = new System.Drawing.Size(100, 30);
            this.btnHide.TabIndex = 9;
            this.btnHide.Text = "Hide";
            this.btnHide.UseVisualStyleBackColor = true;
            this.btnHide.Visible = false;
            this.btnHide.Click += new System.EventHandler(this.btnHide_Click);
            // 
            // taskRunner
            // 
            this.taskRunner.WorkerReportsProgress = true;
            this.taskRunner.WorkerSupportsCancellation = true;
            this.taskRunner.DoWork += new System.ComponentModel.DoWorkEventHandler(this.taskRunner_DoWork);
            this.taskRunner.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.taskRunner_RunWorkerCompleted);
            this.taskRunner.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.taskRunner_ProgressChanged);
            // 
            // TaskProcessor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(494, 336);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnHide);
            this.Controls.Add(this.btnStep);
            this.Controls.Add(this.lbProgressIndicator);
            this.Controls.Add(this.pbWholeTask);
            this.Controls.Add(this.lbWholeProgress);
            this.Controls.Add(this.pbSingleResume);
            this.Controls.Add(this.lbResumePath);
            this.Controls.Add(this.lbCurrentResume);
            this.Controls.Add(this.gpMonitor);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TaskProcessor";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "TaskProcessor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TaskProcessor_FormClosing);
            this.gpMonitor.ResumeLayout(false);
            this.gpMonitor.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gpMonitor;
        private System.Windows.Forms.TextBox tbTaskLog;
        private System.Windows.Forms.Label lbCurrentResume;
        private System.Windows.Forms.Label lbResumePath;
        private System.Windows.Forms.ProgressBar pbSingleResume;
        private System.Windows.Forms.Label lbWholeProgress;
        private System.Windows.Forms.ProgressBar pbWholeTask;
        private System.Windows.Forms.Label lbProgressIndicator;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnStep;
        private System.Windows.Forms.Button btnHide;
        private System.ComponentModel.BackgroundWorker taskRunner;
    }
}