

namespace AgentApp
{
    partial class Form1
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
                this.FSW_trayicon.Dispose();
                this.notifyIcon1.Dispose();
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
            components = new System.ComponentModel.Container();
            System.DirectoryServices.SortOption sortOption1 = new System.DirectoryServices.SortOption();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            toolTip1 = new System.Windows.Forms.ToolTip(components);
            folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            directorySearcher1 = new System.DirectoryServices.DirectorySearcher();
            openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            timer1 = new System.Windows.Forms.Timer(components);
            progressbarTimer = new System.Windows.Forms.Timer(components);
            FSW_trayicon = new System.Windows.Forms.NotifyIcon(components);
            FSW_trayMenu = new System.Windows.Forms.ContextMenuStrip(components);
            FSWopen_toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            About_toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            Exit_toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            notifyIcon1 = new System.Windows.Forms.NotifyIcon(components);
            btnConnectCSE = new System.Windows.Forms.Button();
            tootltip1label1 = new System.Windows.Forms.Label();
            panel1 = new System.Windows.Forms.Panel();
            txtCurrentStatus = new System.Windows.Forms.TextBox();
            lblFileSystemWatcher = new System.Windows.Forms.Label();
            btnSettings = new System.Windows.Forms.Button();
            button1 = new System.Windows.Forms.Button();
            FileSystemWatcherTimer = new System.Windows.Forms.Timer(components);
            FSW_trayMenu.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // directorySearcher1
            // 
            directorySearcher1.ClientTimeout = System.TimeSpan.Parse("-00:00:01");
            directorySearcher1.ServerPageTimeLimit = System.TimeSpan.Parse("-00:00:01");
            directorySearcher1.ServerTimeLimit = System.TimeSpan.Parse("-00:00:01");
            directorySearcher1.Sort = sortOption1;
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // timer1
            // 
            timer1.Tick += timer1_Tick;
            // 
            // progressbarTimer
            // 
            progressbarTimer.Interval = 50;
            // 
            // FSW_trayicon
            // 
            FSW_trayicon.BalloonTipText = "This is eSPark FSW";
            FSW_trayicon.BalloonTipTitle = "This is eSPark FSW";
            FSW_trayicon.ContextMenuStrip = FSW_trayMenu;
            FSW_trayicon.Icon = (System.Drawing.Icon)resources.GetObject("FSW_trayicon.Icon");
            FSW_trayicon.Text = "eSpark FSW";
            FSW_trayicon.Visible = true;
            // 
            // FSW_trayMenu
            // 
            FSW_trayMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { FSWopen_toolStripMenuItem2, About_toolStripMenuItem1, Exit_toolStripMenuItem3 });
            FSW_trayMenu.Name = "contextMenuStrip1";
            FSW_trayMenu.Size = new System.Drawing.Size(207, 70);
            // 
            // FSWopen_toolStripMenuItem2
            // 
            FSWopen_toolStripMenuItem2.Name = "FSWopen_toolStripMenuItem2";
            FSWopen_toolStripMenuItem2.Size = new System.Drawing.Size(206, 22);
            FSWopen_toolStripMenuItem2.Text = "Open postgre DB Indexer";
            FSWopen_toolStripMenuItem2.Click += FSWopen_toolStripMenuItem2_Click;
            // 
            // About_toolStripMenuItem1
            // 
            About_toolStripMenuItem1.Name = "About_toolStripMenuItem1";
            About_toolStripMenuItem1.Size = new System.Drawing.Size(206, 22);
            About_toolStripMenuItem1.Text = "About";
            About_toolStripMenuItem1.Click += toolStripMenuItem1_Click;
            // 
            // Exit_toolStripMenuItem3
            // 
            Exit_toolStripMenuItem3.Name = "Exit_toolStripMenuItem3";
            Exit_toolStripMenuItem3.Size = new System.Drawing.Size(206, 22);
            Exit_toolStripMenuItem3.Text = "Exit";
            Exit_toolStripMenuItem3.Click += Exit_toolStripMenuItem3_Click;
            // 
            // notifyIcon1
            // 
            notifyIcon1.Text = "notifyIcon1";
            notifyIcon1.Visible = true;
            notifyIcon1.Click += notifyIcon1_Click;
            // 
            // btnConnectCSE
            // 
            btnConnectCSE.Location = new System.Drawing.Point(27, 299);
            btnConnectCSE.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnConnectCSE.Name = "btnConnectCSE";
            btnConnectCSE.Size = new System.Drawing.Size(140, 60);
            btnConnectCSE.TabIndex = 13;
            btnConnectCSE.Text = "Connect CSE";
            btnConnectCSE.UseVisualStyleBackColor = true;
            btnConnectCSE.Click += btnConnectCSE_Click;
            // 
            // tootltip1label1
            // 
            tootltip1label1.AutoSize = true;
            tootltip1label1.Dock = System.Windows.Forms.DockStyle.Right;
            tootltip1label1.Location = new System.Drawing.Point(509, 0);
            tootltip1label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            tootltip1label1.MaximumSize = new System.Drawing.Size(107, 29);
            tootltip1label1.MinimumSize = new System.Drawing.Size(107, 29);
            tootltip1label1.Name = "tootltip1label1";
            tootltip1label1.Size = new System.Drawing.Size(107, 29);
            tootltip1label1.TabIndex = 3;
            tootltip1label1.Text = "execution time: 0s.....................";
            // 
            // panel1
            // 
            panel1.Controls.Add(txtCurrentStatus);
            panel1.Controls.Add(lblFileSystemWatcher);
            panel1.Controls.Add(tootltip1label1);
            panel1.Location = new System.Drawing.Point(14, 14);
            panel1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(616, 279);
            panel1.TabIndex = 0;
            // 
            // txtCurrentStatus
            // 
            txtCurrentStatus.BackColor = System.Drawing.SystemColors.ControlLight;
            txtCurrentStatus.BorderStyle = System.Windows.Forms.BorderStyle.None;
            txtCurrentStatus.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            txtCurrentStatus.Location = new System.Drawing.Point(4, 27);
            txtCurrentStatus.Multiline = true;
            txtCurrentStatus.Name = "txtCurrentStatus";
            txtCurrentStatus.ReadOnly = true;
            txtCurrentStatus.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            txtCurrentStatus.Size = new System.Drawing.Size(609, 243);
            txtCurrentStatus.TabIndex = 23;
            // 
            // lblFileSystemWatcher
            // 
            lblFileSystemWatcher.AutoSize = true;
            lblFileSystemWatcher.Location = new System.Drawing.Point(4, 0);
            lblFileSystemWatcher.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblFileSystemWatcher.MaximumSize = new System.Drawing.Size(476, 40);
            lblFileSystemWatcher.MinimumSize = new System.Drawing.Size(476, 29);
            lblFileSystemWatcher.Name = "lblFileSystemWatcher";
            lblFileSystemWatcher.Size = new System.Drawing.Size(476, 40);
            lblFileSystemWatcher.TabIndex = 20;
            lblFileSystemWatcher.Text = "Current Status ... DisConnected!........................................................................";
            // 
            // btnSettings
            // 
            btnSettings.Location = new System.Drawing.Point(323, 299);
            btnSettings.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnSettings.Name = "btnSettings";
            btnSettings.Size = new System.Drawing.Size(140, 60);
            btnSettings.TabIndex = 22;
            btnSettings.Text = "Update cred";
            btnSettings.UseVisualStyleBackColor = true;
            btnSettings.Click += btnSettings_Click;
            // 
            // button1
            // 
            button1.Location = new System.Drawing.Point(175, 299);
            button1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(140, 60);
            button1.TabIndex = 21;
            button1.Text = "Disconnect CSE";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // FileSystemWatcherTimer
            // 
            FileSystemWatcherTimer.Enabled = true;
            FileSystemWatcherTimer.Interval = 1000;
            // 
            // Form1
            // 
            AcceptButton = btnConnectCSE;
            AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(643, 372);
            Controls.Add(btnSettings);
            Controls.Add(panel1);
            Controls.Add(button1);
            Controls.Add(btnConnectCSE);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            HelpButton = true;
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "Form1";
            Text = "PromptPrivacy Database Indexer - postgre";
            HelpButtonClicked += Form1_HelpButtonClicked;
            FormClosing += Form1_FormClosing;
            Load += Form1_Load;
            FSW_trayMenu.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.DirectoryServices.DirectorySearcher directorySearcher1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.NotifyIcon FSW_trayicon;
        private System.Windows.Forms.ContextMenuStrip FSW_trayMenu;
        private System.Windows.Forms.ToolStripMenuItem TaskScheduler_toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem FSWopen_toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem Exit_toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem About_toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem Quartz_toolStripMenuItem;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.Button btnConnectCSE;
        private System.Windows.Forms.Label tootltip1label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Timer FileSystemWatcherTimer;
        private System.Windows.Forms.Label lblFileSystemWatcher;
        public System.Windows.Forms.Timer timer1;
        public System.Windows.Forms.Timer progressbarTimer;
        private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.Button button1;
        public System.Windows.Forms.TextBox txtCurrentStatus;
        //public static System.Windows.Forms.Timer timer1;
        //public static System.Windows.Forms.Timer progressbarTimer;
    }

}

