namespace ResumeCollector
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.statusMain = new System.Windows.Forms.StatusStrip();
            this.menuMain = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resumeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.taskToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.schemeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scheduleTimer = new System.Windows.Forms.Timer(this.components);
            this.fbdLocation = new System.Windows.Forms.FolderBrowserDialog();
            this.tabsMain = new System.Windows.Forms.TabControl();
            this.pageTask = new System.Windows.Forms.TabPage();
            this.pnlTaskAction = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnExcute = new System.Windows.Forms.Button();
            this.gbSettings = new System.Windows.Forms.GroupBox();
            this.lbNextTime = new System.Windows.Forms.Label();
            this.lbCounter = new System.Windows.Forms.Label();
            this.cbNewFailedFolder = new System.Windows.Forms.CheckBox();
            this.cbNewArchiveFolder = new System.Windows.Forms.CheckBox();
            this.tbFailedLocation = new System.Windows.Forms.TextBox();
            this.lbFailed = new System.Windows.Forms.Label();
            this.btnFailedLocation = new System.Windows.Forms.Button();
            this.gbLocations = new System.Windows.Forms.GroupBox();
            this.tbArchiveLocation = new System.Windows.Forms.TextBox();
            this.lbArchiveLocation = new System.Windows.Forms.Label();
            this.btnArchiveLocation = new System.Windows.Forms.Button();
            this.btnResumeLocation = new System.Windows.Forms.Button();
            this.tbResumeLocation = new System.Windows.Forms.TextBox();
            this.lbResumeLocation = new System.Windows.Forms.Label();
            this.pageResumeScheme = new System.Windows.Forms.TabPage();
            this.pnlAction = new System.Windows.Forms.Panel();
            this.btnCancelMapping = new System.Windows.Forms.Button();
            this.btnSaveMapping = new System.Windows.Forms.Button();
            this.pgScheme = new System.Windows.Forms.PropertyGrid();
            this.splitScheme = new System.Windows.Forms.Splitter();
            this.tvScheme = new System.Windows.Forms.TreeView();
            this.pageSettings = new System.Windows.Forms.TabPage();
            this.gbSchedule = new System.Windows.Forms.GroupBox();
            this.ddlStartTime = new System.Windows.Forms.ComboBox();
            this.lbStartTime = new System.Windows.Forms.Label();
            this.ddlDuration = new System.Windows.Forms.ComboBox();
            this.tbRecurrenceNum = new System.Windows.Forms.TextBox();
            this.lbSchedule = new System.Windows.Forms.Label();
            this.pnlSettingAction = new System.Windows.Forms.Panel();
            this.btnCancelSetting = new System.Windows.Forms.Button();
            this.btnSaveSetting = new System.Windows.Forms.Button();
            this.gpDefaultSettings = new System.Windows.Forms.GroupBox();
            this.cbCreateFailedFolder = new System.Windows.Forms.CheckBox();
            this.cbCreateArchiveFolder = new System.Windows.Forms.CheckBox();
            this.tbFailedLocationSetting = new System.Windows.Forms.TextBox();
            this.lbSetFaileFolder = new System.Windows.Forms.Label();
            this.btnSetFailedLocation = new System.Windows.Forms.Button();
            this.tbArchiveLocationSetting = new System.Windows.Forms.TextBox();
            this.lbSetArchiveFolder = new System.Windows.Forms.Label();
            this.btnSetArchiveLocation = new System.Windows.Forms.Button();
            this.btnSetResumeLocation = new System.Windows.Forms.Button();
            this.tbResumeLocationSetting = new System.Windows.Forms.TextBox();
            this.lbSetResumeFolder = new System.Windows.Forms.Label();
            this.nIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.ctxMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.activeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scheduleRunner = new System.ComponentModel.BackgroundWorker();
            this.menuMain.SuspendLayout();
            this.tabsMain.SuspendLayout();
            this.pageTask.SuspendLayout();
            this.pnlTaskAction.SuspendLayout();
            this.gbSettings.SuspendLayout();
            this.gbLocations.SuspendLayout();
            this.pageResumeScheme.SuspendLayout();
            this.pnlAction.SuspendLayout();
            this.pageSettings.SuspendLayout();
            this.gbSchedule.SuspendLayout();
            this.pnlSettingAction.SuspendLayout();
            this.gpDefaultSettings.SuspendLayout();
            this.ctxMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusMain
            // 
            this.statusMain.Location = new System.Drawing.Point(0, 505);
            this.statusMain.Name = "statusMain";
            this.statusMain.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusMain.Size = new System.Drawing.Size(694, 22);
            this.statusMain.SizingGrip = false;
            this.statusMain.TabIndex = 0;
            this.statusMain.Text = "Ready";
            // 
            // menuMain
            // 
            this.menuMain.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.resumeToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuMain.Location = new System.Drawing.Point(0, 0);
            this.menuMain.Name = "menuMain";
            this.menuMain.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.menuMain.Size = new System.Drawing.Size(694, 24);
            this.menuMain.TabIndex = 1;
            this.menuMain.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(42, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(97, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // resumeToolStripMenuItem
            // 
            this.resumeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.taskToolStripMenuItem,
            this.schemeToolStripMenuItem,
            this.settingsToolStripMenuItem});
            this.resumeToolStripMenuItem.Name = "resumeToolStripMenuItem";
            this.resumeToolStripMenuItem.Size = new System.Drawing.Size(55, 20);
            this.resumeToolStripMenuItem.Text = "&Tools";
            // 
            // taskToolStripMenuItem
            // 
            this.taskToolStripMenuItem.Name = "taskToolStripMenuItem";
            this.taskToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.taskToolStripMenuItem.Text = "Tas&k";
            this.taskToolStripMenuItem.Click += new System.EventHandler(this.taskToolStripMenuItem_Click);
            // 
            // schemeToolStripMenuItem
            // 
            this.schemeToolStripMenuItem.Name = "schemeToolStripMenuItem";
            this.schemeToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.schemeToolStripMenuItem.Text = "S&cheme";
            this.schemeToolStripMenuItem.Click += new System.EventHandler(this.schemeToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.settingsToolStripMenuItem.Text = "&Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(49, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
            this.aboutToolStripMenuItem.Text = "&About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem1_Click);
            // 
            // scheduleTimer
            // 
            this.scheduleTimer.Interval = 1000;
            this.scheduleTimer.Tick += new System.EventHandler(this.scheduleTimer_Tick);
            // 
            // tabsMain
            // 
            this.tabsMain.Controls.Add(this.pageTask);
            this.tabsMain.Controls.Add(this.pageResumeScheme);
            this.tabsMain.Controls.Add(this.pageSettings);
            this.tabsMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabsMain.Location = new System.Drawing.Point(0, 24);
            this.tabsMain.Margin = new System.Windows.Forms.Padding(0);
            this.tabsMain.Name = "tabsMain";
            this.tabsMain.SelectedIndex = 0;
            this.tabsMain.Size = new System.Drawing.Size(694, 481);
            this.tabsMain.TabIndex = 2;
            // 
            // pageTask
            // 
            this.pageTask.Controls.Add(this.pnlTaskAction);
            this.pageTask.Controls.Add(this.gbSettings);
            this.pageTask.Controls.Add(this.gbLocations);
            this.pageTask.Location = new System.Drawing.Point(4, 25);
            this.pageTask.Margin = new System.Windows.Forms.Padding(4);
            this.pageTask.Name = "pageTask";
            this.pageTask.Padding = new System.Windows.Forms.Padding(4);
            this.pageTask.Size = new System.Drawing.Size(686, 452);
            this.pageTask.TabIndex = 0;
            this.pageTask.Text = "Task";
            this.pageTask.UseVisualStyleBackColor = true;
            // 
            // pnlTaskAction
            // 
            this.pnlTaskAction.Controls.Add(this.btnCancel);
            this.pnlTaskAction.Controls.Add(this.btnExcute);
            this.pnlTaskAction.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlTaskAction.Location = new System.Drawing.Point(4, 366);
            this.pnlTaskAction.Name = "pnlTaskAction";
            this.pnlTaskAction.Size = new System.Drawing.Size(678, 82);
            this.pnlTaskAction.TabIndex = 3;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(549, 30);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 30);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Close";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnExcute
            // 
            this.btnExcute.Location = new System.Drawing.Point(413, 30);
            this.btnExcute.Margin = new System.Windows.Forms.Padding(4);
            this.btnExcute.Name = "btnExcute";
            this.btnExcute.Size = new System.Drawing.Size(100, 30);
            this.btnExcute.TabIndex = 7;
            this.btnExcute.Text = "Execute";
            this.btnExcute.UseVisualStyleBackColor = true;
            this.btnExcute.Click += new System.EventHandler(this.btnExcute_Click);
            // 
            // gbSettings
            // 
            this.gbSettings.Controls.Add(this.lbNextTime);
            this.gbSettings.Controls.Add(this.lbCounter);
            this.gbSettings.Controls.Add(this.cbNewFailedFolder);
            this.gbSettings.Controls.Add(this.cbNewArchiveFolder);
            this.gbSettings.Controls.Add(this.tbFailedLocation);
            this.gbSettings.Controls.Add(this.lbFailed);
            this.gbSettings.Controls.Add(this.btnFailedLocation);
            this.gbSettings.Location = new System.Drawing.Point(8, 163);
            this.gbSettings.Name = "gbSettings";
            this.gbSettings.Size = new System.Drawing.Size(670, 197);
            this.gbSettings.TabIndex = 2;
            this.gbSettings.TabStop = false;
            this.gbSettings.Text = "Task settings";
            // 
            // lbNextTime
            // 
            this.lbNextTime.AutoSize = true;
            this.lbNextTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbNextTime.Location = new System.Drawing.Point(7, 123);
            this.lbNextTime.Name = "lbNextTime";
            this.lbNextTime.Size = new System.Drawing.Size(145, 16);
            this.lbNextTime.TabIndex = 6;
            this.lbNextTime.Text = "Next Execute Time: ";
            // 
            // lbCounter
            // 
            this.lbCounter.AutoSize = true;
            this.lbCounter.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCounter.Location = new System.Drawing.Point(7, 96);
            this.lbCounter.Name = "lbCounter";
            this.lbCounter.Size = new System.Drawing.Size(411, 16);
            this.lbCounter.TabIndex = 5;
            this.lbCounter.Text = "Reduce Counter: 0 day(s) 0 hour(s) 0 minute(s) 0 second(s)";
            // 
            // cbNewFailedFolder
            // 
            this.cbNewFailedFolder.AutoSize = true;
            this.cbNewFailedFolder.Checked = true;
            this.cbNewFailedFolder.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbNewFailedFolder.Location = new System.Drawing.Point(10, 100);
            this.cbNewFailedFolder.Name = "cbNewFailedFolder";
            this.cbNewFailedFolder.Size = new System.Drawing.Size(374, 20);
            this.cbNewFailedFolder.TabIndex = 4;
            this.cbNewFailedFolder.Text = "Create new folder in failed location (yyyy-mm-dd hh:mm:ss)";
            this.cbNewFailedFolder.UseVisualStyleBackColor = true;
            this.cbNewFailedFolder.Visible = false;
            // 
            // cbNewArchiveFolder
            // 
            this.cbNewArchiveFolder.AutoSize = true;
            this.cbNewArchiveFolder.Checked = true;
            this.cbNewArchiveFolder.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbNewArchiveFolder.Location = new System.Drawing.Point(10, 73);
            this.cbNewArchiveFolder.Name = "cbNewArchiveFolder";
            this.cbNewArchiveFolder.Size = new System.Drawing.Size(385, 20);
            this.cbNewArchiveFolder.TabIndex = 3;
            this.cbNewArchiveFolder.Text = "Create new folder in archive location (yyyy-mm-dd hh:mm:ss)";
            this.cbNewArchiveFolder.UseVisualStyleBackColor = true;
            this.cbNewArchiveFolder.Visible = false;
            // 
            // tbFailedLocation
            // 
            this.tbFailedLocation.Location = new System.Drawing.Point(10, 44);
            this.tbFailedLocation.Name = "tbFailedLocation";
            this.tbFailedLocation.Size = new System.Drawing.Size(504, 22);
            this.tbFailedLocation.TabIndex = 1;
            // 
            // lbFailed
            // 
            this.lbFailed.AutoSize = true;
            this.lbFailed.Location = new System.Drawing.Point(7, 25);
            this.lbFailed.Name = "lbFailed";
            this.lbFailed.Size = new System.Drawing.Size(363, 16);
            this.lbFailed.TabIndex = 0;
            this.lbFailed.Text = "When failed, the resume will be copied to the location below:";
            // 
            // btnFailedLocation
            // 
            this.btnFailedLocation.Location = new System.Drawing.Point(520, 43);
            this.btnFailedLocation.Name = "btnFailedLocation";
            this.btnFailedLocation.Size = new System.Drawing.Size(34, 23);
            this.btnFailedLocation.TabIndex = 2;
            this.btnFailedLocation.Text = ". . .";
            this.btnFailedLocation.UseVisualStyleBackColor = true;
            this.btnFailedLocation.Click += new System.EventHandler(this.btnFailedLocation_Click);
            // 
            // gbLocations
            // 
            this.gbLocations.Controls.Add(this.tbArchiveLocation);
            this.gbLocations.Controls.Add(this.lbArchiveLocation);
            this.gbLocations.Controls.Add(this.btnArchiveLocation);
            this.gbLocations.Controls.Add(this.btnResumeLocation);
            this.gbLocations.Controls.Add(this.tbResumeLocation);
            this.gbLocations.Controls.Add(this.lbResumeLocation);
            this.gbLocations.Location = new System.Drawing.Point(8, 7);
            this.gbLocations.Name = "gbLocations";
            this.gbLocations.Size = new System.Drawing.Size(670, 150);
            this.gbLocations.TabIndex = 1;
            this.gbLocations.TabStop = false;
            this.gbLocations.Text = "Locations settings:";
            // 
            // tbArchiveLocation
            // 
            this.tbArchiveLocation.Location = new System.Drawing.Point(10, 105);
            this.tbArchiveLocation.Name = "tbArchiveLocation";
            this.tbArchiveLocation.Size = new System.Drawing.Size(504, 22);
            this.tbArchiveLocation.TabIndex = 4;
            // 
            // lbArchiveLocation
            // 
            this.lbArchiveLocation.AutoSize = true;
            this.lbArchiveLocation.Location = new System.Drawing.Point(7, 85);
            this.lbArchiveLocation.Name = "lbArchiveLocation";
            this.lbArchiveLocation.Size = new System.Drawing.Size(159, 16);
            this.lbArchiveLocation.TabIndex = 3;
            this.lbArchiveLocation.Text = "Resume archive location:";
            // 
            // btnArchiveLocation
            // 
            this.btnArchiveLocation.Location = new System.Drawing.Point(520, 104);
            this.btnArchiveLocation.Name = "btnArchiveLocation";
            this.btnArchiveLocation.Size = new System.Drawing.Size(34, 23);
            this.btnArchiveLocation.TabIndex = 2;
            this.btnArchiveLocation.Text = ". . .";
            this.btnArchiveLocation.UseVisualStyleBackColor = true;
            this.btnArchiveLocation.Click += new System.EventHandler(this.btnArchiveLocation_Click);
            // 
            // btnResumeLocation
            // 
            this.btnResumeLocation.Location = new System.Drawing.Point(521, 48);
            this.btnResumeLocation.Name = "btnResumeLocation";
            this.btnResumeLocation.Size = new System.Drawing.Size(34, 23);
            this.btnResumeLocation.TabIndex = 2;
            this.btnResumeLocation.Text = ". . .";
            this.btnResumeLocation.UseVisualStyleBackColor = true;
            this.btnResumeLocation.Click += new System.EventHandler(this.btnResumeLocation_Click);
            // 
            // tbResumeLocation
            // 
            this.tbResumeLocation.Location = new System.Drawing.Point(10, 50);
            this.tbResumeLocation.Name = "tbResumeLocation";
            this.tbResumeLocation.Size = new System.Drawing.Size(504, 22);
            this.tbResumeLocation.TabIndex = 1;
            // 
            // lbResumeLocation
            // 
            this.lbResumeLocation.AutoSize = true;
            this.lbResumeLocation.Location = new System.Drawing.Point(7, 30);
            this.lbResumeLocation.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbResumeLocation.Name = "lbResumeLocation";
            this.lbResumeLocation.Size = new System.Drawing.Size(132, 16);
            this.lbResumeLocation.TabIndex = 0;
            this.lbResumeLocation.Text = "Resume file location:";
            // 
            // pageResumeScheme
            // 
            this.pageResumeScheme.Controls.Add(this.pnlAction);
            this.pageResumeScheme.Controls.Add(this.pgScheme);
            this.pageResumeScheme.Controls.Add(this.splitScheme);
            this.pageResumeScheme.Controls.Add(this.tvScheme);
            this.pageResumeScheme.Location = new System.Drawing.Point(4, 25);
            this.pageResumeScheme.Margin = new System.Windows.Forms.Padding(4);
            this.pageResumeScheme.Name = "pageResumeScheme";
            this.pageResumeScheme.Padding = new System.Windows.Forms.Padding(4);
            this.pageResumeScheme.Size = new System.Drawing.Size(686, 452);
            this.pageResumeScheme.TabIndex = 1;
            this.pageResumeScheme.Text = "Scheme";
            this.pageResumeScheme.UseVisualStyleBackColor = true;
            // 
            // pnlAction
            // 
            this.pnlAction.Controls.Add(this.btnCancelMapping);
            this.pnlAction.Controls.Add(this.btnSaveMapping);
            this.pnlAction.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlAction.Location = new System.Drawing.Point(412, 381);
            this.pnlAction.Margin = new System.Windows.Forms.Padding(4);
            this.pnlAction.Name = "pnlAction";
            this.pnlAction.Size = new System.Drawing.Size(270, 67);
            this.pnlAction.TabIndex = 3;
            // 
            // btnCancelMapping
            // 
            this.btnCancelMapping.Location = new System.Drawing.Point(141, 15);
            this.btnCancelMapping.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancelMapping.Name = "btnCancelMapping";
            this.btnCancelMapping.Size = new System.Drawing.Size(100, 30);
            this.btnCancelMapping.TabIndex = 6;
            this.btnCancelMapping.Text = "Cancel";
            this.btnCancelMapping.UseVisualStyleBackColor = true;
            // 
            // btnSaveMapping
            // 
            this.btnSaveMapping.Location = new System.Drawing.Point(5, 15);
            this.btnSaveMapping.Margin = new System.Windows.Forms.Padding(4);
            this.btnSaveMapping.Name = "btnSaveMapping";
            this.btnSaveMapping.Size = new System.Drawing.Size(100, 30);
            this.btnSaveMapping.TabIndex = 5;
            this.btnSaveMapping.Text = "Save";
            this.btnSaveMapping.UseVisualStyleBackColor = true;
            this.btnSaveMapping.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // pgScheme
            // 
            this.pgScheme.Dock = System.Windows.Forms.DockStyle.Top;
            this.pgScheme.Location = new System.Drawing.Point(207, 4);
            this.pgScheme.Margin = new System.Windows.Forms.Padding(4);
            this.pgScheme.Name = "pgScheme";
            this.pgScheme.Size = new System.Drawing.Size(475, 377);
            this.pgScheme.TabIndex = 2;
            // 
            // splitScheme
            // 
            this.splitScheme.Location = new System.Drawing.Point(203, 4);
            this.splitScheme.Margin = new System.Windows.Forms.Padding(4);
            this.splitScheme.Name = "splitScheme";
            this.splitScheme.Size = new System.Drawing.Size(4, 444);
            this.splitScheme.TabIndex = 1;
            this.splitScheme.TabStop = false;
            // 
            // tvScheme
            // 
            this.tvScheme.Dock = System.Windows.Forms.DockStyle.Left;
            this.tvScheme.Location = new System.Drawing.Point(4, 4);
            this.tvScheme.Margin = new System.Windows.Forms.Padding(0);
            this.tvScheme.Name = "tvScheme";
            this.tvScheme.Size = new System.Drawing.Size(199, 444);
            this.tvScheme.TabIndex = 0;
            // 
            // pageSettings
            // 
            this.pageSettings.Controls.Add(this.gbSchedule);
            this.pageSettings.Controls.Add(this.pnlSettingAction);
            this.pageSettings.Controls.Add(this.gpDefaultSettings);
            this.pageSettings.Location = new System.Drawing.Point(4, 25);
            this.pageSettings.Margin = new System.Windows.Forms.Padding(4);
            this.pageSettings.Name = "pageSettings";
            this.pageSettings.Size = new System.Drawing.Size(686, 452);
            this.pageSettings.TabIndex = 2;
            this.pageSettings.Text = "Settings";
            this.pageSettings.UseVisualStyleBackColor = true;
            // 
            // gbSchedule
            // 
            this.gbSchedule.Controls.Add(this.ddlStartTime);
            this.gbSchedule.Controls.Add(this.lbStartTime);
            this.gbSchedule.Controls.Add(this.ddlDuration);
            this.gbSchedule.Controls.Add(this.tbRecurrenceNum);
            this.gbSchedule.Controls.Add(this.lbSchedule);
            this.gbSchedule.Location = new System.Drawing.Point(8, 202);
            this.gbSchedule.Name = "gbSchedule";
            this.gbSchedule.Size = new System.Drawing.Size(670, 163);
            this.gbSchedule.TabIndex = 4;
            this.gbSchedule.TabStop = false;
            this.gbSchedule.Text = "Schedule Task";
            // 
            // ddlStartTime
            // 
            this.ddlStartTime.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlStartTime.FormattingEnabled = true;
            this.ddlStartTime.Items.AddRange(new object[] {
            "12:00 AM",
            "1:00 AM",
            "2:00 AM",
            "3:00 AM",
            "4:00 AM",
            "5:00 AM",
            "6:00 AM",
            "7:00 AM",
            "8:00 AM",
            "9:00 AM",
            "10:00 AM",
            "11:00 AM",
            "12:00 PM",
            "1:00 PM",
            "2:00 PM",
            "3:00 PM",
            "4:00 PM",
            "5:00 PM",
            "6:00 PM",
            "7:00 PM",
            "8:00 PM",
            "9:00 PM",
            "10:00 PM",
            "11:00 PM"});
            this.ddlStartTime.Location = new System.Drawing.Point(89, 59);
            this.ddlStartTime.Name = "ddlStartTime";
            this.ddlStartTime.Size = new System.Drawing.Size(121, 24);
            this.ddlStartTime.TabIndex = 4;
            // 
            // lbStartTime
            // 
            this.lbStartTime.AutoSize = true;
            this.lbStartTime.Location = new System.Drawing.Point(7, 67);
            this.lbStartTime.Name = "lbStartTime";
            this.lbStartTime.Size = new System.Drawing.Size(72, 16);
            this.lbStartTime.TabIndex = 3;
            this.lbStartTime.Text = "Start Time:";
            // 
            // ddlDuration
            // 
            this.ddlDuration.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlDuration.FormattingEnabled = true;
            this.ddlDuration.Items.AddRange(new object[] {
            "Day(s)",
            "Week(s)",
            "Month(s)"});
            this.ddlDuration.Location = new System.Drawing.Point(181, 27);
            this.ddlDuration.Name = "ddlDuration";
            this.ddlDuration.Size = new System.Drawing.Size(121, 24);
            this.ddlDuration.TabIndex = 2;
            // 
            // tbRecurrenceNum
            // 
            this.tbRecurrenceNum.Location = new System.Drawing.Point(132, 29);
            this.tbRecurrenceNum.Name = "tbRecurrenceNum";
            this.tbRecurrenceNum.Size = new System.Drawing.Size(40, 22);
            this.tbRecurrenceNum.TabIndex = 1;
            // 
            // lbSchedule
            // 
            this.lbSchedule.AutoSize = true;
            this.lbSchedule.Location = new System.Drawing.Point(7, 35);
            this.lbSchedule.Name = "lbSchedule";
            this.lbSchedule.Size = new System.Drawing.Size(122, 16);
            this.lbSchedule.TabIndex = 0;
            this.lbSchedule.Text = "Recurrence:  Every";
            // 
            // pnlSettingAction
            // 
            this.pnlSettingAction.Controls.Add(this.btnCancelSetting);
            this.pnlSettingAction.Controls.Add(this.btnSaveSetting);
            this.pnlSettingAction.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlSettingAction.Location = new System.Drawing.Point(0, 371);
            this.pnlSettingAction.Name = "pnlSettingAction";
            this.pnlSettingAction.Size = new System.Drawing.Size(686, 81);
            this.pnlSettingAction.TabIndex = 3;
            // 
            // btnCancelSetting
            // 
            this.btnCancelSetting.Location = new System.Drawing.Point(553, 25);
            this.btnCancelSetting.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancelSetting.Name = "btnCancelSetting";
            this.btnCancelSetting.Size = new System.Drawing.Size(100, 30);
            this.btnCancelSetting.TabIndex = 8;
            this.btnCancelSetting.Text = "Cancel";
            this.btnCancelSetting.UseVisualStyleBackColor = true;
            this.btnCancelSetting.Click += new System.EventHandler(this.btnCancelSetting_Click);
            // 
            // btnSaveSetting
            // 
            this.btnSaveSetting.Location = new System.Drawing.Point(417, 25);
            this.btnSaveSetting.Margin = new System.Windows.Forms.Padding(4);
            this.btnSaveSetting.Name = "btnSaveSetting";
            this.btnSaveSetting.Size = new System.Drawing.Size(100, 30);
            this.btnSaveSetting.TabIndex = 7;
            this.btnSaveSetting.Text = "Save";
            this.btnSaveSetting.UseVisualStyleBackColor = true;
            this.btnSaveSetting.Click += new System.EventHandler(this.btnSaveSetting_Click);
            // 
            // gpDefaultSettings
            // 
            this.gpDefaultSettings.Controls.Add(this.cbCreateFailedFolder);
            this.gpDefaultSettings.Controls.Add(this.cbCreateArchiveFolder);
            this.gpDefaultSettings.Controls.Add(this.tbFailedLocationSetting);
            this.gpDefaultSettings.Controls.Add(this.lbSetFaileFolder);
            this.gpDefaultSettings.Controls.Add(this.btnSetFailedLocation);
            this.gpDefaultSettings.Controls.Add(this.tbArchiveLocationSetting);
            this.gpDefaultSettings.Controls.Add(this.lbSetArchiveFolder);
            this.gpDefaultSettings.Controls.Add(this.btnSetArchiveLocation);
            this.gpDefaultSettings.Controls.Add(this.btnSetResumeLocation);
            this.gpDefaultSettings.Controls.Add(this.tbResumeLocationSetting);
            this.gpDefaultSettings.Controls.Add(this.lbSetResumeFolder);
            this.gpDefaultSettings.Location = new System.Drawing.Point(8, 7);
            this.gpDefaultSettings.Name = "gpDefaultSettings";
            this.gpDefaultSettings.Size = new System.Drawing.Size(670, 189);
            this.gpDefaultSettings.TabIndex = 2;
            this.gpDefaultSettings.TabStop = false;
            this.gpDefaultSettings.Text = "Default location settings:";
            // 
            // cbCreateFailedFolder
            // 
            this.cbCreateFailedFolder.AutoSize = true;
            this.cbCreateFailedFolder.Checked = true;
            this.cbCreateFailedFolder.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbCreateFailedFolder.Location = new System.Drawing.Point(10, 195);
            this.cbCreateFailedFolder.Name = "cbCreateFailedFolder";
            this.cbCreateFailedFolder.Size = new System.Drawing.Size(374, 20);
            this.cbCreateFailedFolder.TabIndex = 9;
            this.cbCreateFailedFolder.Text = "Create new folder in failed location (yyyy-mm-dd hh:mm:ss)";
            this.cbCreateFailedFolder.UseVisualStyleBackColor = true;
            this.cbCreateFailedFolder.Visible = false;
            // 
            // cbCreateArchiveFolder
            // 
            this.cbCreateArchiveFolder.AutoSize = true;
            this.cbCreateArchiveFolder.Checked = true;
            this.cbCreateArchiveFolder.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbCreateArchiveFolder.Location = new System.Drawing.Point(10, 168);
            this.cbCreateArchiveFolder.Name = "cbCreateArchiveFolder";
            this.cbCreateArchiveFolder.Size = new System.Drawing.Size(385, 20);
            this.cbCreateArchiveFolder.TabIndex = 8;
            this.cbCreateArchiveFolder.Text = "Create new folder in archive location (yyyy-mm-dd hh:mm:ss)";
            this.cbCreateArchiveFolder.UseVisualStyleBackColor = true;
            this.cbCreateArchiveFolder.Visible = false;
            // 
            // tbFailedLocationSetting
            // 
            this.tbFailedLocationSetting.Location = new System.Drawing.Point(10, 139);
            this.tbFailedLocationSetting.Name = "tbFailedLocationSetting";
            this.tbFailedLocationSetting.Size = new System.Drawing.Size(504, 22);
            this.tbFailedLocationSetting.TabIndex = 6;
            // 
            // lbSetFaileFolder
            // 
            this.lbSetFaileFolder.AutoSize = true;
            this.lbSetFaileFolder.Location = new System.Drawing.Point(7, 119);
            this.lbSetFaileFolder.Name = "lbSetFaileFolder";
            this.lbSetFaileFolder.Size = new System.Drawing.Size(363, 16);
            this.lbSetFaileFolder.TabIndex = 5;
            this.lbSetFaileFolder.Text = "When failed, the resume will be copied to the location below:";
            // 
            // btnSetFailedLocation
            // 
            this.btnSetFailedLocation.Location = new System.Drawing.Point(520, 138);
            this.btnSetFailedLocation.Name = "btnSetFailedLocation";
            this.btnSetFailedLocation.Size = new System.Drawing.Size(34, 23);
            this.btnSetFailedLocation.TabIndex = 7;
            this.btnSetFailedLocation.Text = ". . .";
            this.btnSetFailedLocation.UseVisualStyleBackColor = true;
            this.btnSetFailedLocation.Click += new System.EventHandler(this.btnSetFailedLocation_Click);
            // 
            // tbArchiveLocationSetting
            // 
            this.tbArchiveLocationSetting.Location = new System.Drawing.Point(10, 88);
            this.tbArchiveLocationSetting.Name = "tbArchiveLocationSetting";
            this.tbArchiveLocationSetting.Size = new System.Drawing.Size(504, 22);
            this.tbArchiveLocationSetting.TabIndex = 4;
            // 
            // lbSetArchiveFolder
            // 
            this.lbSetArchiveFolder.AutoSize = true;
            this.lbSetArchiveFolder.Location = new System.Drawing.Point(7, 68);
            this.lbSetArchiveFolder.Name = "lbSetArchiveFolder";
            this.lbSetArchiveFolder.Size = new System.Drawing.Size(159, 16);
            this.lbSetArchiveFolder.TabIndex = 3;
            this.lbSetArchiveFolder.Text = "Resume archive location:";
            // 
            // btnSetArchiveLocation
            // 
            this.btnSetArchiveLocation.Location = new System.Drawing.Point(520, 85);
            this.btnSetArchiveLocation.Name = "btnSetArchiveLocation";
            this.btnSetArchiveLocation.Size = new System.Drawing.Size(34, 23);
            this.btnSetArchiveLocation.TabIndex = 2;
            this.btnSetArchiveLocation.Text = ". . .";
            this.btnSetArchiveLocation.UseVisualStyleBackColor = true;
            this.btnSetArchiveLocation.Click += new System.EventHandler(this.btnSetArchiveLocation_Click);
            // 
            // btnSetResumeLocation
            // 
            this.btnSetResumeLocation.Location = new System.Drawing.Point(521, 35);
            this.btnSetResumeLocation.Name = "btnSetResumeLocation";
            this.btnSetResumeLocation.Size = new System.Drawing.Size(34, 23);
            this.btnSetResumeLocation.TabIndex = 2;
            this.btnSetResumeLocation.Text = ". . .";
            this.btnSetResumeLocation.UseVisualStyleBackColor = true;
            this.btnSetResumeLocation.Click += new System.EventHandler(this.btnSetResumeLocation_Click);
            // 
            // tbResumeLocationSetting
            // 
            this.tbResumeLocationSetting.Location = new System.Drawing.Point(10, 37);
            this.tbResumeLocationSetting.Name = "tbResumeLocationSetting";
            this.tbResumeLocationSetting.Size = new System.Drawing.Size(504, 22);
            this.tbResumeLocationSetting.TabIndex = 1;
            // 
            // lbSetResumeFolder
            // 
            this.lbSetResumeFolder.AutoSize = true;
            this.lbSetResumeFolder.Location = new System.Drawing.Point(7, 17);
            this.lbSetResumeFolder.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbSetResumeFolder.Name = "lbSetResumeFolder";
            this.lbSetResumeFolder.Size = new System.Drawing.Size(132, 16);
            this.lbSetResumeFolder.TabIndex = 0;
            this.lbSetResumeFolder.Text = "Resume file location:";
            // 
            // nIcon
            // 
            this.nIcon.ContextMenuStrip = this.ctxMenu;
            this.nIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("nIcon.Icon")));
            this.nIcon.Text = "Springfield Resume Collector";
            this.nIcon.Visible = true;
            this.nIcon.DoubleClick += new System.EventHandler(this.nIcon_DoubleClick);
            // 
            // ctxMenu
            // 
            this.ctxMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.activeToolStripMenuItem,
            this.quitToolStripMenuItem});
            this.ctxMenu.Name = "ctxMenu";
            this.ctxMenu.Size = new System.Drawing.Size(111, 48);
            // 
            // activeToolStripMenuItem
            // 
            this.activeToolStripMenuItem.Enabled = false;
            this.activeToolStripMenuItem.Name = "activeToolStripMenuItem";
            this.activeToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.activeToolStripMenuItem.Text = "&Active";
            this.activeToolStripMenuItem.Click += new System.EventHandler(this.activeToolStripMenuItem_Click);
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.quitToolStripMenuItem.Text = "E&xit";
            this.quitToolStripMenuItem.Click += new System.EventHandler(this.quitToolStripMenuItem_Click);
            // 
            // scheduleRunner
            // 
            this.scheduleRunner.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.scheduleRunner_RunWorkerCompleted);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(694, 527);
            this.Controls.Add(this.tabsMain);
            this.Controls.Add(this.statusMain);
            this.Controls.Add(this.menuMain);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuMain;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Springfield Resume Collector";
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.menuMain.ResumeLayout(false);
            this.menuMain.PerformLayout();
            this.tabsMain.ResumeLayout(false);
            this.pageTask.ResumeLayout(false);
            this.pnlTaskAction.ResumeLayout(false);
            this.gbSettings.ResumeLayout(false);
            this.gbSettings.PerformLayout();
            this.gbLocations.ResumeLayout(false);
            this.gbLocations.PerformLayout();
            this.pageResumeScheme.ResumeLayout(false);
            this.pnlAction.ResumeLayout(false);
            this.pageSettings.ResumeLayout(false);
            this.gbSchedule.ResumeLayout(false);
            this.gbSchedule.PerformLayout();
            this.pnlSettingAction.ResumeLayout(false);
            this.gpDefaultSettings.ResumeLayout(false);
            this.gpDefaultSettings.PerformLayout();
            this.ctxMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusMain;
        private System.Windows.Forms.MenuStrip menuMain;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resumeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.FolderBrowserDialog fbdLocation;
        private System.Windows.Forms.TabControl tabsMain;
        private System.Windows.Forms.TabPage pageTask;
        private System.Windows.Forms.TabPage pageResumeScheme;
        private System.Windows.Forms.TabPage pageSettings;
        private System.Windows.Forms.NotifyIcon nIcon;
        private System.Windows.Forms.Splitter splitScheme;
        private System.Windows.Forms.TreeView tvScheme;
        private System.Windows.Forms.PropertyGrid pgScheme;
        private System.Windows.Forms.Panel pnlAction;
        private System.Windows.Forms.Button btnCancelMapping;
        private System.Windows.Forms.Button btnSaveMapping;
        private System.Windows.Forms.Label lbResumeLocation;
        private System.Windows.Forms.GroupBox gbLocations;
        private System.Windows.Forms.Button btnResumeLocation;
        private System.Windows.Forms.TextBox tbResumeLocation;
        private System.Windows.Forms.Label lbArchiveLocation;
        private System.Windows.Forms.Panel pnlTaskAction;
        private System.Windows.Forms.GroupBox gbSettings;
        private System.Windows.Forms.TextBox tbArchiveLocation;
        private System.Windows.Forms.Button btnArchiveLocation;
        private System.Windows.Forms.ToolStripMenuItem taskToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem schemeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Label lbFailed;
        private System.Windows.Forms.TextBox tbFailedLocation;
        private System.Windows.Forms.Button btnFailedLocation;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnExcute;
        private System.Windows.Forms.ContextMenuStrip ctxMenu;
        private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
        private System.Windows.Forms.CheckBox cbNewFailedFolder;
        private System.Windows.Forms.CheckBox cbNewArchiveFolder;
        private System.Windows.Forms.GroupBox gpDefaultSettings;
        private System.Windows.Forms.TextBox tbArchiveLocationSetting;
        private System.Windows.Forms.Label lbSetArchiveFolder;
        private System.Windows.Forms.Button btnSetArchiveLocation;
        private System.Windows.Forms.Button btnSetResumeLocation;
        private System.Windows.Forms.TextBox tbResumeLocationSetting;
        private System.Windows.Forms.Label lbSetResumeFolder;
        private System.Windows.Forms.CheckBox cbCreateFailedFolder;
        private System.Windows.Forms.CheckBox cbCreateArchiveFolder;
        private System.Windows.Forms.TextBox tbFailedLocationSetting;
        private System.Windows.Forms.Label lbSetFaileFolder;
        private System.Windows.Forms.Button btnSetFailedLocation;
        private System.Windows.Forms.ToolStripMenuItem activeToolStripMenuItem;
        private System.Windows.Forms.Panel pnlSettingAction;
        private System.Windows.Forms.Button btnCancelSetting;
        private System.Windows.Forms.Button btnSaveSetting;
        private System.Windows.Forms.GroupBox gbSchedule;
        private System.Windows.Forms.Label lbSchedule;
        private System.Windows.Forms.ComboBox ddlDuration;
        private System.Windows.Forms.TextBox tbRecurrenceNum;
        private System.Windows.Forms.Label lbStartTime;
        private System.Windows.Forms.ComboBox ddlStartTime;
        private System.Windows.Forms.Label lbCounter;
        private System.ComponentModel.BackgroundWorker scheduleRunner;
        private System.Windows.Forms.Timer scheduleTimer;
        private System.Windows.Forms.Label lbNextTime;
    }
}

