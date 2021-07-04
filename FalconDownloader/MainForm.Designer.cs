namespace FalconDownloader
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
            this.scMain = new System.Windows.Forms.SplitContainer();
            this.scLeft = new System.Windows.Forms.SplitContainer();
            this.gbMain = new System.Windows.Forms.GroupBox();
            this.btnReconnect = new System.Windows.Forms.Button();
            this.lblReconnectTime = new System.Windows.Forms.Label();
            this.lblServerIsDown = new System.Windows.Forms.Label();
            this.gbCurrentTaskInfo = new System.Windows.Forms.GroupBox();
            this.lblCurrTaskStatusCaption = new System.Windows.Forms.Label();
            this.lblCurrTaskUnreadEmailsCaption = new System.Windows.Forms.Label();
            this.lblTimeToStartNew = new System.Windows.Forms.Label();
            this.lblCurrTaskTime = new System.Windows.Forms.Label();
            this.lblCurrTaskDownloadedFiles = new System.Windows.Forms.Label();
            this.lblCurrTaskUnreadEmails = new System.Windows.Forms.Label();
            this.lblCurrTaskStatus = new System.Windows.Forms.Label();
            this.lblCurrTaskReadEmailsCaption = new System.Windows.Forms.Label();
            this.lblCurrTaskReadEmails = new System.Windows.Forms.Label();
            this.lblCurrTaskDownloadedFilesCaption = new System.Windows.Forms.Label();
            this.lblTimeToStartNewCaption = new System.Windows.Forms.Label();
            this.lblCurrTaskTimeCaption = new System.Windows.Forms.Label();
            this.gbSummaryInfo = new System.Windows.Forms.GroupBox();
            this.lblStatusCaption = new System.Windows.Forms.Label();
            this.lblTime = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblDownloaded = new System.Windows.Forms.Label();
            this.lblReadEmailsCaption = new System.Windows.Forms.Label();
            this.lblDownloadedCaption = new System.Windows.Forms.Label();
            this.lblReadEmails = new System.Windows.Forms.Label();
            this.lblTimeCaption = new System.Windows.Forms.Label();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.gbFiles = new System.Windows.Forms.GroupBox();
            this.lvFiles = new System.Windows.Forms.ListView();
            this.ilListView = new System.Windows.Forms.ImageList(this.components);
            this.gbLog = new System.Windows.Forms.GroupBox();
            this.rtbLog = new System.Windows.Forms.RichTextBox();
            this.pMsgTypes = new System.Windows.Forms.Panel();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.rbAll = new System.Windows.Forms.RadioButton();
            this.ttMain = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.scMain)).BeginInit();
            this.scMain.Panel1.SuspendLayout();
            this.scMain.Panel2.SuspendLayout();
            this.scMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scLeft)).BeginInit();
            this.scLeft.Panel1.SuspendLayout();
            this.scLeft.Panel2.SuspendLayout();
            this.scLeft.SuspendLayout();
            this.gbMain.SuspendLayout();
            this.gbCurrentTaskInfo.SuspendLayout();
            this.gbSummaryInfo.SuspendLayout();
            this.gbFiles.SuspendLayout();
            this.gbLog.SuspendLayout();
            this.pMsgTypes.SuspendLayout();
            this.SuspendLayout();
            // 
            // scMain
            // 
            this.scMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.scMain.Location = new System.Drawing.Point(0, 0);
            this.scMain.Name = "scMain";
            // 
            // scMain.Panel1
            // 
            this.scMain.Panel1.Controls.Add(this.scLeft);
            // 
            // scMain.Panel2
            // 
            this.scMain.Panel2.Controls.Add(this.gbLog);
            this.scMain.Size = new System.Drawing.Size(884, 436);
            this.scMain.SplitterDistance = 500;
            this.scMain.TabIndex = 0;
            // 
            // scLeft
            // 
            this.scLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scLeft.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.scLeft.Location = new System.Drawing.Point(0, 0);
            this.scLeft.Name = "scLeft";
            this.scLeft.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scLeft.Panel1
            // 
            this.scLeft.Panel1.Controls.Add(this.gbMain);
            // 
            // scLeft.Panel2
            // 
            this.scLeft.Panel2.Controls.Add(this.gbFiles);
            this.scLeft.Size = new System.Drawing.Size(500, 436);
            this.scLeft.SplitterDistance = 250;
            this.scLeft.TabIndex = 0;
            // 
            // gbMain
            // 
            this.gbMain.Controls.Add(this.btnReconnect);
            this.gbMain.Controls.Add(this.lblReconnectTime);
            this.gbMain.Controls.Add(this.lblServerIsDown);
            this.gbMain.Controls.Add(this.gbCurrentTaskInfo);
            this.gbMain.Controls.Add(this.gbSummaryInfo);
            this.gbMain.Controls.Add(this.btnStop);
            this.gbMain.Controls.Add(this.btnStart);
            this.gbMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbMain.Location = new System.Drawing.Point(0, 0);
            this.gbMain.Name = "gbMain";
            this.gbMain.Size = new System.Drawing.Size(500, 250);
            this.gbMain.TabIndex = 0;
            this.gbMain.TabStop = false;
            this.gbMain.Text = "Main";
            // 
            // btnReconnect
            // 
            this.btnReconnect.Location = new System.Drawing.Point(368, 222);
            this.btnReconnect.Name = "btnReconnect";
            this.btnReconnect.Size = new System.Drawing.Size(105, 23);
            this.btnReconnect.TabIndex = 6;
            this.btnReconnect.Text = "Reconnect NOW";
            this.btnReconnect.UseVisualStyleBackColor = true;
            this.btnReconnect.Visible = false;
            this.btnReconnect.Click += new System.EventHandler(this.btnReconnect_Click);
            // 
            // lblReconnectTime
            // 
            this.lblReconnectTime.AutoSize = true;
            this.lblReconnectTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblReconnectTime.Location = new System.Drawing.Point(287, 227);
            this.lblReconnectTime.Name = "lblReconnectTime";
            this.lblReconnectTime.Size = new System.Drawing.Size(57, 13);
            this.lblReconnectTime.TabIndex = 5;
            this.lblReconnectTime.Text = "00:00:00";
            this.lblReconnectTime.Visible = false;
            // 
            // lblServerIsDown
            // 
            this.lblServerIsDown.AutoSize = true;
            this.lblServerIsDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblServerIsDown.ForeColor = System.Drawing.Color.Red;
            this.lblServerIsDown.Location = new System.Drawing.Point(265, 207);
            this.lblServerIsDown.Name = "lblServerIsDown";
            this.lblServerIsDown.Size = new System.Drawing.Size(195, 13);
            this.lblServerIsDown.TabIndex = 4;
            this.lblServerIsDown.Tag = "";
            this.lblServerIsDown.Text = "Server is down. Reconnect after:";
            this.lblServerIsDown.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.lblServerIsDown.Visible = false;
            // 
            // gbCurrentTaskInfo
            // 
            this.gbCurrentTaskInfo.Controls.Add(this.lblCurrTaskStatusCaption);
            this.gbCurrentTaskInfo.Controls.Add(this.lblCurrTaskUnreadEmailsCaption);
            this.gbCurrentTaskInfo.Controls.Add(this.lblTimeToStartNew);
            this.gbCurrentTaskInfo.Controls.Add(this.lblCurrTaskTime);
            this.gbCurrentTaskInfo.Controls.Add(this.lblCurrTaskDownloadedFiles);
            this.gbCurrentTaskInfo.Controls.Add(this.lblCurrTaskUnreadEmails);
            this.gbCurrentTaskInfo.Controls.Add(this.lblCurrTaskStatus);
            this.gbCurrentTaskInfo.Controls.Add(this.lblCurrTaskReadEmailsCaption);
            this.gbCurrentTaskInfo.Controls.Add(this.lblCurrTaskReadEmails);
            this.gbCurrentTaskInfo.Controls.Add(this.lblCurrTaskDownloadedFilesCaption);
            this.gbCurrentTaskInfo.Controls.Add(this.lblTimeToStartNewCaption);
            this.gbCurrentTaskInfo.Controls.Add(this.lblCurrTaskTimeCaption);
            this.gbCurrentTaskInfo.Location = new System.Drawing.Point(268, 19);
            this.gbCurrentTaskInfo.Name = "gbCurrentTaskInfo";
            this.gbCurrentTaskInfo.Size = new System.Drawing.Size(226, 180);
            this.gbCurrentTaskInfo.TabIndex = 0;
            this.gbCurrentTaskInfo.TabStop = false;
            this.gbCurrentTaskInfo.Text = "Current Task Information";
            // 
            // lblCurrTaskStatusCaption
            // 
            this.lblCurrTaskStatusCaption.AutoSize = true;
            this.lblCurrTaskStatusCaption.Location = new System.Drawing.Point(6, 25);
            this.lblCurrTaskStatusCaption.Name = "lblCurrTaskStatusCaption";
            this.lblCurrTaskStatusCaption.Size = new System.Drawing.Size(40, 13);
            this.lblCurrTaskStatusCaption.TabIndex = 1;
            this.lblCurrTaskStatusCaption.Text = "Status:";
            // 
            // lblCurrTaskUnreadEmailsCaption
            // 
            this.lblCurrTaskUnreadEmailsCaption.AutoSize = true;
            this.lblCurrTaskUnreadEmailsCaption.Location = new System.Drawing.Point(6, 50);
            this.lblCurrTaskUnreadEmailsCaption.Name = "lblCurrTaskUnreadEmailsCaption";
            this.lblCurrTaskUnreadEmailsCaption.Size = new System.Drawing.Size(77, 13);
            this.lblCurrTaskUnreadEmailsCaption.TabIndex = 1;
            this.lblCurrTaskUnreadEmailsCaption.Text = "Unread emails:";
            // 
            // lblTimeToStartNew
            // 
            this.lblTimeToStartNew.AutoSize = true;
            this.lblTimeToStartNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblTimeToStartNew.Location = new System.Drawing.Point(118, 150);
            this.lblTimeToStartNew.Name = "lblTimeToStartNew";
            this.lblTimeToStartNew.Size = new System.Drawing.Size(27, 13);
            this.lblTimeToStartNew.TabIndex = 2;
            this.lblTimeToStartNew.Text = "- - -";
            this.lblTimeToStartNew.Visible = false;
            // 
            // lblCurrTaskTime
            // 
            this.lblCurrTaskTime.AutoSize = true;
            this.lblCurrTaskTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblCurrTaskTime.Location = new System.Drawing.Point(118, 125);
            this.lblCurrTaskTime.Name = "lblCurrTaskTime";
            this.lblCurrTaskTime.Size = new System.Drawing.Size(27, 13);
            this.lblCurrTaskTime.TabIndex = 2;
            this.lblCurrTaskTime.Text = "- - -";
            // 
            // lblCurrTaskDownloadedFiles
            // 
            this.lblCurrTaskDownloadedFiles.AutoSize = true;
            this.lblCurrTaskDownloadedFiles.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblCurrTaskDownloadedFiles.Location = new System.Drawing.Point(118, 100);
            this.lblCurrTaskDownloadedFiles.Name = "lblCurrTaskDownloadedFiles";
            this.lblCurrTaskDownloadedFiles.Size = new System.Drawing.Size(27, 13);
            this.lblCurrTaskDownloadedFiles.TabIndex = 2;
            this.lblCurrTaskDownloadedFiles.Text = "- - -";
            // 
            // lblCurrTaskUnreadEmails
            // 
            this.lblCurrTaskUnreadEmails.AutoSize = true;
            this.lblCurrTaskUnreadEmails.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblCurrTaskUnreadEmails.Location = new System.Drawing.Point(118, 50);
            this.lblCurrTaskUnreadEmails.Name = "lblCurrTaskUnreadEmails";
            this.lblCurrTaskUnreadEmails.Size = new System.Drawing.Size(31, 13);
            this.lblCurrTaskUnreadEmails.TabIndex = 2;
            this.lblCurrTaskUnreadEmails.Text = "- - - ";
            // 
            // lblCurrTaskStatus
            // 
            this.lblCurrTaskStatus.AutoSize = true;
            this.lblCurrTaskStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblCurrTaskStatus.Location = new System.Drawing.Point(118, 25);
            this.lblCurrTaskStatus.Name = "lblCurrTaskStatus";
            this.lblCurrTaskStatus.Size = new System.Drawing.Size(27, 13);
            this.lblCurrTaskStatus.TabIndex = 2;
            this.lblCurrTaskStatus.Text = "- - -";
            // 
            // lblCurrTaskReadEmailsCaption
            // 
            this.lblCurrTaskReadEmailsCaption.AutoSize = true;
            this.lblCurrTaskReadEmailsCaption.Location = new System.Drawing.Point(6, 75);
            this.lblCurrTaskReadEmailsCaption.Name = "lblCurrTaskReadEmailsCaption";
            this.lblCurrTaskReadEmailsCaption.Size = new System.Drawing.Size(66, 13);
            this.lblCurrTaskReadEmailsCaption.TabIndex = 1;
            this.lblCurrTaskReadEmailsCaption.Text = "ReadEmails:";
            // 
            // lblCurrTaskReadEmails
            // 
            this.lblCurrTaskReadEmails.AutoSize = true;
            this.lblCurrTaskReadEmails.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblCurrTaskReadEmails.Location = new System.Drawing.Point(118, 75);
            this.lblCurrTaskReadEmails.Name = "lblCurrTaskReadEmails";
            this.lblCurrTaskReadEmails.Size = new System.Drawing.Size(27, 13);
            this.lblCurrTaskReadEmails.TabIndex = 2;
            this.lblCurrTaskReadEmails.Text = "- - -";
            // 
            // lblCurrTaskDownloadedFilesCaption
            // 
            this.lblCurrTaskDownloadedFilesCaption.AutoSize = true;
            this.lblCurrTaskDownloadedFilesCaption.Location = new System.Drawing.Point(6, 100);
            this.lblCurrTaskDownloadedFilesCaption.Name = "lblCurrTaskDownloadedFilesCaption";
            this.lblCurrTaskDownloadedFilesCaption.Size = new System.Drawing.Size(91, 13);
            this.lblCurrTaskDownloadedFilesCaption.TabIndex = 1;
            this.lblCurrTaskDownloadedFilesCaption.Text = "Downloaded files:";
            // 
            // lblTimeToStartNewCaption
            // 
            this.lblTimeToStartNewCaption.AutoSize = true;
            this.lblTimeToStartNewCaption.Location = new System.Drawing.Point(6, 150);
            this.lblTimeToStartNewCaption.Name = "lblTimeToStartNewCaption";
            this.lblTimeToStartNewCaption.Size = new System.Drawing.Size(70, 13);
            this.lblTimeToStartNewCaption.TabIndex = 1;
            this.lblTimeToStartNewCaption.Text = "Time to New:";
            this.lblTimeToStartNewCaption.Visible = false;
            // 
            // lblCurrTaskTimeCaption
            // 
            this.lblCurrTaskTimeCaption.AutoSize = true;
            this.lblCurrTaskTimeCaption.Location = new System.Drawing.Point(6, 125);
            this.lblCurrTaskTimeCaption.Name = "lblCurrTaskTimeCaption";
            this.lblCurrTaskTimeCaption.Size = new System.Drawing.Size(81, 13);
            this.lblCurrTaskTimeCaption.TabIndex = 1;
            this.lblCurrTaskTimeCaption.Text = "Time from Start:";
            // 
            // gbSummaryInfo
            // 
            this.gbSummaryInfo.Controls.Add(this.lblStatusCaption);
            this.gbSummaryInfo.Controls.Add(this.lblTime);
            this.gbSummaryInfo.Controls.Add(this.lblStatus);
            this.gbSummaryInfo.Controls.Add(this.lblDownloaded);
            this.gbSummaryInfo.Controls.Add(this.lblReadEmailsCaption);
            this.gbSummaryInfo.Controls.Add(this.lblDownloadedCaption);
            this.gbSummaryInfo.Controls.Add(this.lblReadEmails);
            this.gbSummaryInfo.Controls.Add(this.lblTimeCaption);
            this.gbSummaryInfo.Location = new System.Drawing.Point(12, 19);
            this.gbSummaryInfo.Name = "gbSummaryInfo";
            this.gbSummaryInfo.Size = new System.Drawing.Size(250, 180);
            this.gbSummaryInfo.TabIndex = 3;
            this.gbSummaryInfo.TabStop = false;
            this.gbSummaryInfo.Text = "Summary Information";
            // 
            // lblStatusCaption
            // 
            this.lblStatusCaption.AutoSize = true;
            this.lblStatusCaption.Location = new System.Drawing.Point(6, 25);
            this.lblStatusCaption.Name = "lblStatusCaption";
            this.lblStatusCaption.Size = new System.Drawing.Size(40, 13);
            this.lblStatusCaption.TabIndex = 1;
            this.lblStatusCaption.Text = "Status:";
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblTime.Location = new System.Drawing.Point(150, 100);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(57, 13);
            this.lblTime.TabIndex = 2;
            this.lblTime.Text = "00:00:00";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblStatus.Location = new System.Drawing.Point(150, 25);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(54, 13);
            this.lblStatus.TabIndex = 2;
            this.lblStatus.Text = "Stopped";
            // 
            // lblDownloaded
            // 
            this.lblDownloaded.AutoSize = true;
            this.lblDownloaded.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblDownloaded.Location = new System.Drawing.Point(150, 75);
            this.lblDownloaded.Name = "lblDownloaded";
            this.lblDownloaded.Size = new System.Drawing.Size(14, 13);
            this.lblDownloaded.TabIndex = 2;
            this.lblDownloaded.Text = "0";
            // 
            // lblReadEmailsCaption
            // 
            this.lblReadEmailsCaption.AutoSize = true;
            this.lblReadEmailsCaption.Location = new System.Drawing.Point(6, 50);
            this.lblReadEmailsCaption.Name = "lblReadEmailsCaption";
            this.lblReadEmailsCaption.Size = new System.Drawing.Size(68, 13);
            this.lblReadEmailsCaption.TabIndex = 1;
            this.lblReadEmailsCaption.Text = "Read emails:";
            // 
            // lblDownloadedCaption
            // 
            this.lblDownloadedCaption.AutoSize = true;
            this.lblDownloadedCaption.Location = new System.Drawing.Point(6, 75);
            this.lblDownloadedCaption.Name = "lblDownloadedCaption";
            this.lblDownloadedCaption.Size = new System.Drawing.Size(91, 13);
            this.lblDownloadedCaption.TabIndex = 1;
            this.lblDownloadedCaption.Text = "Downloaded files:";
            // 
            // lblReadEmails
            // 
            this.lblReadEmails.AutoSize = true;
            this.lblReadEmails.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblReadEmails.Location = new System.Drawing.Point(150, 50);
            this.lblReadEmails.Name = "lblReadEmails";
            this.lblReadEmails.Size = new System.Drawing.Size(14, 13);
            this.lblReadEmails.TabIndex = 2;
            this.lblReadEmails.Text = "0";
            // 
            // lblTimeCaption
            // 
            this.lblTimeCaption.AutoSize = true;
            this.lblTimeCaption.Location = new System.Drawing.Point(6, 100);
            this.lblTimeCaption.Name = "lblTimeCaption";
            this.lblTimeCaption.Size = new System.Drawing.Size(81, 13);
            this.lblTimeCaption.TabIndex = 1;
            this.lblTimeCaption.Text = "Time from Start:";
            // 
            // btnStop
            // 
            this.btnStop.Enabled = false;
            this.btnStop.Location = new System.Drawing.Point(115, 207);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(94, 37);
            this.btnStop.TabIndex = 2;
            this.btnStop.Text = "STOP";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(15, 207);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(94, 37);
            this.btnStart.TabIndex = 1;
            this.btnStart.Text = "START";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // gbFiles
            // 
            this.gbFiles.Controls.Add(this.lvFiles);
            this.gbFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbFiles.Location = new System.Drawing.Point(0, 0);
            this.gbFiles.Name = "gbFiles";
            this.gbFiles.Size = new System.Drawing.Size(500, 182);
            this.gbFiles.TabIndex = 0;
            this.gbFiles.TabStop = false;
            this.gbFiles.Text = "Downloaded files";
            // 
            // lvFiles
            // 
            this.lvFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvFiles.LargeImageList = this.ilListView;
            this.lvFiles.Location = new System.Drawing.Point(3, 16);
            this.lvFiles.Name = "lvFiles";
            this.lvFiles.ShowItemToolTips = true;
            this.lvFiles.Size = new System.Drawing.Size(494, 163);
            this.lvFiles.TabIndex = 0;
            this.lvFiles.UseCompatibleStateImageBehavior = false;
            this.lvFiles.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvFiles_MouseDoubleClick);
            // 
            // ilListView
            // 
            this.ilListView.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilListView.ImageStream")));
            this.ilListView.TransparentColor = System.Drawing.Color.Transparent;
            this.ilListView.Images.SetKeyName(0, "Word-icon.png");
            this.ilListView.Images.SetKeyName(1, "MS-Word-2-icon.png");
            this.ilListView.Images.SetKeyName(2, "Document-icon.png");
            // 
            // gbLog
            // 
            this.gbLog.Controls.Add(this.rtbLog);
            this.gbLog.Controls.Add(this.pMsgTypes);
            this.gbLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbLog.Location = new System.Drawing.Point(0, 0);
            this.gbLog.Name = "gbLog";
            this.gbLog.Size = new System.Drawing.Size(380, 436);
            this.gbLog.TabIndex = 0;
            this.gbLog.TabStop = false;
            this.gbLog.Text = "Log";
            // 
            // rtbLog
            // 
            this.rtbLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbLog.Location = new System.Drawing.Point(3, 42);
            this.rtbLog.Name = "rtbLog";
            this.rtbLog.ReadOnly = true;
            this.rtbLog.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.rtbLog.Size = new System.Drawing.Size(374, 391);
            this.rtbLog.TabIndex = 0;
            this.rtbLog.Text = "";
            // 
            // pMsgTypes
            // 
            this.pMsgTypes.Controls.Add(this.radioButton4);
            this.pMsgTypes.Controls.Add(this.radioButton3);
            this.pMsgTypes.Controls.Add(this.radioButton2);
            this.pMsgTypes.Controls.Add(this.radioButton1);
            this.pMsgTypes.Controls.Add(this.rbAll);
            this.pMsgTypes.Dock = System.Windows.Forms.DockStyle.Top;
            this.pMsgTypes.Location = new System.Drawing.Point(3, 16);
            this.pMsgTypes.Name = "pMsgTypes";
            this.pMsgTypes.Size = new System.Drawing.Size(374, 26);
            this.pMsgTypes.TabIndex = 1;
            // 
            // radioButton4
            // 
            this.radioButton4.AutoSize = true;
            this.radioButton4.Location = new System.Drawing.Point(254, 3);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(52, 17);
            this.radioButton4.TabIndex = 0;
            this.radioButton4.Tag = "3";
            this.radioButton4.Text = "Errors";
            this.radioButton4.UseVisualStyleBackColor = true;
            this.radioButton4.CheckedChanged += new System.EventHandler(this.rbLogFilter_CheckedChanged);
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(170, 3);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(70, 17);
            this.radioButton3.TabIndex = 0;
            this.radioButton3.Tag = "2";
            this.radioButton3.Text = "Warnings";
            this.radioButton3.UseVisualStyleBackColor = true;
            this.radioButton3.CheckedChanged += new System.EventHandler(this.rbLogFilter_CheckedChanged);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(113, 3);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(43, 17);
            this.radioButton2.TabIndex = 0;
            this.radioButton2.Tag = "1";
            this.radioButton2.Text = "Info";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.rbLogFilter_CheckedChanged);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(53, 3);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(46, 17);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.Tag = "0";
            this.radioButton1.Text = "Text";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.rbLogFilter_CheckedChanged);
            // 
            // rbAll
            // 
            this.rbAll.AutoSize = true;
            this.rbAll.Checked = true;
            this.rbAll.Location = new System.Drawing.Point(3, 3);
            this.rbAll.Name = "rbAll";
            this.rbAll.Size = new System.Drawing.Size(36, 17);
            this.rbAll.TabIndex = 0;
            this.rbAll.TabStop = true;
            this.rbAll.Text = "All";
            this.rbAll.UseVisualStyleBackColor = true;
            this.rbAll.CheckedChanged += new System.EventHandler(this.rbLogFilter_CheckedChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 436);
            this.Controls.Add(this.scMain);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(820, 475);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FalconDownloader";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.scMain.Panel1.ResumeLayout(false);
            this.scMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scMain)).EndInit();
            this.scMain.ResumeLayout(false);
            this.scLeft.Panel1.ResumeLayout(false);
            this.scLeft.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scLeft)).EndInit();
            this.scLeft.ResumeLayout(false);
            this.gbMain.ResumeLayout(false);
            this.gbMain.PerformLayout();
            this.gbCurrentTaskInfo.ResumeLayout(false);
            this.gbCurrentTaskInfo.PerformLayout();
            this.gbSummaryInfo.ResumeLayout(false);
            this.gbSummaryInfo.PerformLayout();
            this.gbFiles.ResumeLayout(false);
            this.gbLog.ResumeLayout(false);
            this.pMsgTypes.ResumeLayout(false);
            this.pMsgTypes.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer scMain;
        private System.Windows.Forms.SplitContainer scLeft;
        private System.Windows.Forms.GroupBox gbLog;
        private System.Windows.Forms.RichTextBox rtbLog;
        private System.Windows.Forms.GroupBox gbMain;
        private System.Windows.Forms.GroupBox gbFiles;
        private System.Windows.Forms.ListView lvFiles;
        private System.Windows.Forms.Label lblStatusCaption;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label lblReadEmails;
        private System.Windows.Forms.Label lblReadEmailsCaption;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblDownloaded;
        private System.Windows.Forms.Label lblDownloadedCaption;
        private System.Windows.Forms.GroupBox gbCurrentTaskInfo;
        private System.Windows.Forms.Label lblCurrTaskStatusCaption;
        private System.Windows.Forms.Label lblCurrTaskUnreadEmailsCaption;
        private System.Windows.Forms.Label lblTimeToStartNew;
        private System.Windows.Forms.Label lblCurrTaskTime;
        private System.Windows.Forms.Label lblCurrTaskDownloadedFiles;
        private System.Windows.Forms.Label lblCurrTaskUnreadEmails;
        private System.Windows.Forms.Label lblCurrTaskStatus;
        private System.Windows.Forms.Label lblCurrTaskReadEmailsCaption;
        private System.Windows.Forms.Label lblCurrTaskReadEmails;
        private System.Windows.Forms.Label lblCurrTaskDownloadedFilesCaption;
        private System.Windows.Forms.Label lblTimeToStartNewCaption;
        private System.Windows.Forms.Label lblCurrTaskTimeCaption;
        private System.Windows.Forms.GroupBox gbSummaryInfo;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Label lblTimeCaption;
        private System.Windows.Forms.Panel pMsgTypes;
        private System.Windows.Forms.RadioButton radioButton4;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton rbAll;
        private System.Windows.Forms.ImageList ilListView;
        private System.Windows.Forms.ToolTip ttMain;
        private System.Windows.Forms.Button btnReconnect;
        private System.Windows.Forms.Label lblReconnectTime;
        private System.Windows.Forms.Label lblServerIsDown;
    }
}

