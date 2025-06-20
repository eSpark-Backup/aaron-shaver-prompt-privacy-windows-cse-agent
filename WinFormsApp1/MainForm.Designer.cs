
namespace eSpark_WindowsFIA
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
            components = new System.ComponentModel.Container();
            System.DirectoryServices.SortOption sortOption1 = new System.DirectoryServices.SortOption();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            lblFileSystemWatcher = new MetroSet_UI.Controls.MetroSetLabel();
            tootltip1label1 = new MetroSet_UI.Controls.MetroSetLabel();
            timer_callByTaskSched = new System.Windows.Forms.Timer(components);
            toolTip1 = new System.Windows.Forms.ToolTip(components);
            panel1 = new MetroSet_UI.Controls.MetroSetPanel();
            txtCurrentStatus = new MetroSet_UI.Controls.MetroSetTextBox();
            folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            directorySearcher1 = new System.DirectoryServices.DirectorySearcher();
            openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            timer1 = new System.Windows.Forms.Timer(components);
            progressbarTimer = new System.Windows.Forms.Timer(components);
            FSW_trayicon = new System.Windows.Forms.NotifyIcon(components);
            FSW_trayMenu = new System.Windows.Forms.ContextMenuStrip(components);
            FSWopen_toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            Quartz_toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            TaskScheduler_toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            About_toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            Exit_toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            notifyIcon1 = new System.Windows.Forms.NotifyIcon(components);
            toolStripProgressBar1 = new MetroSet_UI.Controls.MetroSetProgressBar();
            metroSetControlBox1 = new MetroSet_UI.Controls.MetroSetControlBox();
            metroSetLabel9 = new MetroSet_UI.Controls.MetroSetLabel();
            btnSettings = new MetroSet_UI.Controls.MetroSetButton();
            button1 = new MetroSet_UI.Controls.MetroSetButton();
            btnConnectCSE = new MetroSet_UI.Controls.MetroSetButton();
            panel1.SuspendLayout();
            FSW_trayMenu.SuspendLayout();
            SuspendLayout();
            // 
            // lblFileSystemWatcher
            // 
            lblFileSystemWatcher.AutoEllipsis = true;
            lblFileSystemWatcher.AutoSize = true;
            lblFileSystemWatcher.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lblFileSystemWatcher.IsDerivedStyle = true;
            lblFileSystemWatcher.Location = new System.Drawing.Point(3, 0);
            lblFileSystemWatcher.MaximumSize = new System.Drawing.Size(449, 35);
            lblFileSystemWatcher.MinimumSize = new System.Drawing.Size(449, 25);
            lblFileSystemWatcher.Name = "lblFileSystemWatcher";
            lblFileSystemWatcher.Size = new System.Drawing.Size(449, 25);
            lblFileSystemWatcher.Style = MetroSet_UI.Enums.Style.Light;
            lblFileSystemWatcher.StyleManager = null;
            lblFileSystemWatcher.TabIndex = 20;
            lblFileSystemWatcher.Text = "File System Watcher stopped!..................................................................................";
            lblFileSystemWatcher.ThemeAuthor = "Narwin";
            lblFileSystemWatcher.ThemeName = "MetroLite";
            // 
            // tootltip1label1
            // 
            tootltip1label1.AutoEllipsis = true;
            tootltip1label1.AutoSize = true;
            tootltip1label1.Dock = System.Windows.Forms.DockStyle.Right;
            tootltip1label1.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            tootltip1label1.IsDerivedStyle = true;
            tootltip1label1.Location = new System.Drawing.Point(459, 0);
            tootltip1label1.MaximumSize = new System.Drawing.Size(211, 25);
            tootltip1label1.MinimumSize = new System.Drawing.Size(101, 25);
            tootltip1label1.Name = "tootltip1label1";
            tootltip1label1.Size = new System.Drawing.Size(143, 25);
            tootltip1label1.Style = MetroSet_UI.Enums.Style.Light;
            tootltip1label1.StyleManager = null;
            tootltip1label1.TabIndex = 3;
            tootltip1label1.Text = "execution time: 0s............";
            tootltip1label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            tootltip1label1.ThemeAuthor = "Narwin";
            tootltip1label1.ThemeName = "MetroLite";
            tootltip1label1.UseCompatibleTextRendering = true;
            // 
            // timer_callByTaskSched
            // 
            timer_callByTaskSched.Interval = 5000;
            timer_callByTaskSched.Tick += timer1_Tick;
            // 
            // panel1
            // 
            panel1.BackgroundColor = System.Drawing.Color.White;
            panel1.BorderColor = System.Drawing.Color.FromArgb(150, 150, 150);
            panel1.BorderThickness = 1;
            panel1.Controls.Add(txtCurrentStatus);
            panel1.Controls.Add(lblFileSystemWatcher);
            panel1.Controls.Add(tootltip1label1);
            panel1.IsDerivedStyle = true;
            panel1.Location = new System.Drawing.Point(28, 100);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(602, 262);
            panel1.Style = MetroSet_UI.Enums.Style.Light;
            panel1.StyleManager = null;
            panel1.TabIndex = 4;
            panel1.ThemeAuthor = "Narwin";
            panel1.ThemeName = "MetroLite";
            // 
            // txtCurrentStatus
            // 
            txtCurrentStatus.AutoCompleteCustomSource = null;
            txtCurrentStatus.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            txtCurrentStatus.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            txtCurrentStatus.BorderColor = System.Drawing.Color.FromArgb(155, 155, 155);
            txtCurrentStatus.DisabledBackColor = System.Drawing.Color.FromArgb(204, 204, 204);
            txtCurrentStatus.DisabledBorderColor = System.Drawing.Color.FromArgb(155, 155, 155);
            txtCurrentStatus.DisabledForeColor = System.Drawing.Color.FromArgb(136, 136, 136);
            txtCurrentStatus.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            txtCurrentStatus.HoverColor = System.Drawing.Color.FromArgb(102, 102, 102);
            txtCurrentStatus.Image = null;
            txtCurrentStatus.IsDerivedStyle = true;
            txtCurrentStatus.Lines = null;
            txtCurrentStatus.Location = new System.Drawing.Point(3, 34);
            txtCurrentStatus.MaxLength = 32767;
            txtCurrentStatus.Multiline = true;
            txtCurrentStatus.Name = "txtCurrentStatus";
            txtCurrentStatus.ReadOnly = true;
            txtCurrentStatus.Size = new System.Drawing.Size(596, 225);
            txtCurrentStatus.Style = MetroSet_UI.Enums.Style.Light;
            txtCurrentStatus.StyleManager = null;
            txtCurrentStatus.TabIndex = 24;
            txtCurrentStatus.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            txtCurrentStatus.ThemeAuthor = "Narwin";
            txtCurrentStatus.ThemeName = "MetroLite";
            txtCurrentStatus.UseSystemPasswordChar = false;
            txtCurrentStatus.WatermarkText = "";
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
            timer1.Interval = 500;
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
            FSW_trayMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { FSWopen_toolStripMenuItem2, Quartz_toolStripMenuItem, TaskScheduler_toolStripMenuItem1, About_toolStripMenuItem1, Exit_toolStripMenuItem3 });
            FSW_trayMenu.Name = "contextMenuStrip1";
            FSW_trayMenu.Size = new System.Drawing.Size(201, 114);
            // 
            // FSWopen_toolStripMenuItem2
            // 
            FSWopen_toolStripMenuItem2.Name = "FSWopen_toolStripMenuItem2";
            FSWopen_toolStripMenuItem2.Size = new System.Drawing.Size(200, 22);
            FSWopen_toolStripMenuItem2.Text = "Open File System Agent";
            FSWopen_toolStripMenuItem2.Click += FSWopen_toolStripMenuItem2_Click;
            // 
            // Quartz_toolStripMenuItem
            // 
            Quartz_toolStripMenuItem.Name = "Quartz_toolStripMenuItem";
            Quartz_toolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            // 
            // TaskScheduler_toolStripMenuItem1
            // 
            TaskScheduler_toolStripMenuItem1.Name = "TaskScheduler_toolStripMenuItem1";
            TaskScheduler_toolStripMenuItem1.Size = new System.Drawing.Size(200, 22);
            // 
            // About_toolStripMenuItem1
            // 
            About_toolStripMenuItem1.Name = "About_toolStripMenuItem1";
            About_toolStripMenuItem1.Size = new System.Drawing.Size(200, 22);
            About_toolStripMenuItem1.Text = "About";
            About_toolStripMenuItem1.Click += toolStripMenuItem1_Click;
            // 
            // Exit_toolStripMenuItem3
            // 
            Exit_toolStripMenuItem3.Name = "Exit_toolStripMenuItem3";
            Exit_toolStripMenuItem3.Size = new System.Drawing.Size(200, 22);
            Exit_toolStripMenuItem3.Text = "Exit";
            Exit_toolStripMenuItem3.Click += Exit_toolStripMenuItem3_Click;
            // 
            // notifyIcon1
            // 
            notifyIcon1.Text = "notifyIcon1";
            notifyIcon1.Visible = true;
            notifyIcon1.Click += notifyIcon1_Click;
            // 
            // toolStripProgressBar1
            // 
            toolStripProgressBar1.BackgroundColor = System.Drawing.Color.FromArgb(238, 238, 238);
            toolStripProgressBar1.BorderColor = System.Drawing.Color.FromArgb(238, 238, 238);
            toolStripProgressBar1.DisabledBackColor = System.Drawing.Color.FromArgb(238, 238, 238);
            toolStripProgressBar1.DisabledBorderColor = System.Drawing.Color.FromArgb(238, 238, 238);
            toolStripProgressBar1.DisabledProgressColor = System.Drawing.Color.FromArgb(120, 65, 177, 225);
            toolStripProgressBar1.IsDerivedStyle = false;
            toolStripProgressBar1.Location = new System.Drawing.Point(0, 0);
            toolStripProgressBar1.Maximum = 100;
            toolStripProgressBar1.Minimum = 0;
            toolStripProgressBar1.Name = "toolStripProgressBar1";
            toolStripProgressBar1.Orientation = MetroSet_UI.Enums.ProgressOrientation.Horizontal;
            toolStripProgressBar1.ProgressColor = System.Drawing.Color.FromArgb(65, 177, 225);
            toolStripProgressBar1.Size = new System.Drawing.Size(525, 24);
            toolStripProgressBar1.Style = MetroSet_UI.Enums.Style.Light;
            toolStripProgressBar1.StyleManager = null;
            toolStripProgressBar1.TabIndex = 0;
            toolStripProgressBar1.ThemeAuthor = "Narwin";
            toolStripProgressBar1.ThemeName = "MetroLite";
            toolStripProgressBar1.Value = 100;
            // 
            // metroSetControlBox1
            // 
            metroSetControlBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            metroSetControlBox1.CloseHoverBackColor = System.Drawing.Color.FromArgb(183, 40, 40);
            metroSetControlBox1.CloseHoverForeColor = System.Drawing.Color.White;
            metroSetControlBox1.CloseNormalForeColor = System.Drawing.Color.Gray;
            metroSetControlBox1.DisabledForeColor = System.Drawing.Color.DimGray;
            metroSetControlBox1.IsDerivedStyle = true;
            metroSetControlBox1.Location = new System.Drawing.Point(530, 14);
            metroSetControlBox1.MaximizeBox = false;
            metroSetControlBox1.MaximizeHoverBackColor = System.Drawing.Color.FromArgb(238, 238, 238);
            metroSetControlBox1.MaximizeHoverForeColor = System.Drawing.Color.Gray;
            metroSetControlBox1.MaximizeNormalForeColor = System.Drawing.Color.Gray;
            metroSetControlBox1.MinimizeBox = false;
            metroSetControlBox1.MinimizeHoverBackColor = System.Drawing.Color.FromArgb(238, 238, 238);
            metroSetControlBox1.MinimizeHoverForeColor = System.Drawing.Color.Gray;
            metroSetControlBox1.MinimizeNormalForeColor = System.Drawing.Color.Gray;
            metroSetControlBox1.Name = "metroSetControlBox1";
            metroSetControlBox1.Size = new System.Drawing.Size(100, 25);
            metroSetControlBox1.Style = MetroSet_UI.Enums.Style.Light;
            metroSetControlBox1.StyleManager = null;
            metroSetControlBox1.TabIndex = 7;
            metroSetControlBox1.ThemeAuthor = "Narwin";
            metroSetControlBox1.ThemeName = "MetroLite";
            // 
            // metroSetLabel9
            // 
            metroSetLabel9.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            metroSetLabel9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            metroSetLabel9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            metroSetLabel9.IsDerivedStyle = true;
            metroSetLabel9.Location = new System.Drawing.Point(27, -7);
            metroSetLabel9.Name = "metroSetLabel9";
            metroSetLabel9.Size = new System.Drawing.Size(471, 27);
            metroSetLabel9.Style = MetroSet_UI.Enums.Style.Dark;
            metroSetLabel9.StyleManager = null;
            metroSetLabel9.TabIndex = 33;
            metroSetLabel9.Text = "Connects PrompPrivacy CSE server to Postgre dbIndexer";
            metroSetLabel9.ThemeAuthor = "Narwin";
            metroSetLabel9.ThemeName = "MetroDark";
            // 
            // btnSettings
            // 
            btnSettings.DisabledBackColor = System.Drawing.Color.FromArgb(120, 65, 177, 225);
            btnSettings.DisabledBorderColor = System.Drawing.Color.FromArgb(120, 65, 177, 225);
            btnSettings.DisabledForeColor = System.Drawing.Color.Gray;
            btnSettings.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            btnSettings.HoverBorderColor = System.Drawing.Color.FromArgb(95, 207, 255);
            btnSettings.HoverColor = System.Drawing.Color.FromArgb(95, 207, 255);
            btnSettings.HoverTextColor = System.Drawing.Color.White;
            btnSettings.IsDerivedStyle = true;
            btnSettings.Location = new System.Drawing.Point(393, 368);
            btnSettings.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnSettings.Name = "btnSettings";
            btnSettings.NormalBorderColor = System.Drawing.Color.FromArgb(65, 177, 225);
            btnSettings.NormalColor = System.Drawing.Color.FromArgb(65, 177, 225);
            btnSettings.NormalTextColor = System.Drawing.Color.White;
            btnSettings.PressBorderColor = System.Drawing.Color.FromArgb(35, 147, 195);
            btnSettings.PressColor = System.Drawing.Color.FromArgb(35, 147, 195);
            btnSettings.PressTextColor = System.Drawing.Color.White;
            btnSettings.Size = new System.Drawing.Size(140, 60);
            btnSettings.Style = MetroSet_UI.Enums.Style.Light;
            btnSettings.StyleManager = null;
            btnSettings.TabIndex = 36;
            btnSettings.Text = "Update cred";
            btnSettings.ThemeAuthor = "Narwin";
            btnSettings.ThemeName = "MetroLite";
            btnSettings.Click += btnSettings_Click;
            // 
            // button1
            // 
            button1.DisabledBackColor = System.Drawing.Color.FromArgb(120, 65, 177, 225);
            button1.DisabledBorderColor = System.Drawing.Color.FromArgb(120, 65, 177, 225);
            button1.DisabledForeColor = System.Drawing.Color.Gray;
            button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            button1.HoverBorderColor = System.Drawing.Color.FromArgb(95, 207, 255);
            button1.HoverColor = System.Drawing.Color.FromArgb(95, 207, 255);
            button1.HoverTextColor = System.Drawing.Color.White;
            button1.IsDerivedStyle = true;
            button1.Location = new System.Drawing.Point(245, 368);
            button1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            button1.Name = "button1";
            button1.NormalBorderColor = System.Drawing.Color.FromArgb(65, 177, 225);
            button1.NormalColor = System.Drawing.Color.FromArgb(65, 177, 225);
            button1.NormalTextColor = System.Drawing.Color.White;
            button1.PressBorderColor = System.Drawing.Color.FromArgb(35, 147, 195);
            button1.PressColor = System.Drawing.Color.FromArgb(35, 147, 195);
            button1.PressTextColor = System.Drawing.Color.White;
            button1.Size = new System.Drawing.Size(140, 60);
            button1.Style = MetroSet_UI.Enums.Style.Light;
            button1.StyleManager = null;
            button1.TabIndex = 35;
            button1.Text = "Disconnect CSE";
            button1.ThemeAuthor = "Narwin";
            button1.ThemeName = "MetroLite";
            button1.Click += button1_Click;
            // 
            // btnConnectCSE
            // 
            btnConnectCSE.DisabledBackColor = System.Drawing.Color.FromArgb(120, 65, 177, 225);
            btnConnectCSE.DisabledBorderColor = System.Drawing.Color.FromArgb(120, 65, 177, 225);
            btnConnectCSE.DisabledForeColor = System.Drawing.Color.Gray;
            btnConnectCSE.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            btnConnectCSE.HoverBorderColor = System.Drawing.Color.FromArgb(95, 207, 255);
            btnConnectCSE.HoverColor = System.Drawing.Color.FromArgb(95, 207, 255);
            btnConnectCSE.HoverTextColor = System.Drawing.Color.White;
            btnConnectCSE.IsDerivedStyle = true;
            btnConnectCSE.Location = new System.Drawing.Point(97, 368);
            btnConnectCSE.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnConnectCSE.Name = "btnConnectCSE";
            btnConnectCSE.NormalBorderColor = System.Drawing.Color.FromArgb(65, 177, 225);
            btnConnectCSE.NormalColor = System.Drawing.Color.FromArgb(65, 177, 225);
            btnConnectCSE.NormalTextColor = System.Drawing.Color.White;
            btnConnectCSE.PressBorderColor = System.Drawing.Color.FromArgb(35, 147, 195);
            btnConnectCSE.PressColor = System.Drawing.Color.FromArgb(35, 147, 195);
            btnConnectCSE.PressTextColor = System.Drawing.Color.White;
            btnConnectCSE.Size = new System.Drawing.Size(140, 60);
            btnConnectCSE.Style = MetroSet_UI.Enums.Style.Light;
            btnConnectCSE.StyleManager = null;
            btnConnectCSE.TabIndex = 34;
            btnConnectCSE.Text = "Connect CSE";
            btnConnectCSE.ThemeAuthor = "Narwin";
            btnConnectCSE.ThemeName = "MetroLite";
            btnConnectCSE.Click += btnConnectCSE_Click;
            // 
            // MainForm
            // 
            AllowResize = false;
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            BorderThickness = 10F;
            ClientSize = new System.Drawing.Size(644, 450);
            ControlBox = false;
            Controls.Add(btnSettings);
            Controls.Add(button1);
            Controls.Add(btnConnectCSE);
            Controls.Add(metroSetLabel9);
            Controls.Add(metroSetControlBox1);
            Controls.Add(panel1);
            Font = new System.Drawing.Font("Bookman Old Style", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            HeaderHeight = 10;
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Name = "MainForm";
            Padding = new System.Windows.Forms.Padding(12, 90, 12, 12);
            ShowBorder = true;
            SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "PromptPrivacy Database Indexer - postgre";
            TextColor = System.Drawing.Color.Black;
            HelpButtonClicked += Form1_HelpButtonClicked;
            FormClosing += Form1_FormClosing;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            FSW_trayMenu.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.ToolTip toolTip1;
        private MetroSet_UI.Controls.MetroSetPanel panel1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.DirectoryServices.DirectorySearcher directorySearcher1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.NotifyIcon FSW_trayicon;
        private System.Windows.Forms.ContextMenuStrip FSW_trayMenu;
        private System.Windows.Forms.ToolStripMenuItem FSWopen_toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem Quartz_toolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem TaskScheduler_toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem About_toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem Exit_toolStripMenuItem3;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private MetroSet_UI.Controls.MetroSetProgressBar toolStripProgressBar1;
        private MetroSet_UI.Controls.MetroSetLabel lblFileSystemWatcher;
        private MetroSet_UI.Controls.MetroSetLabel tootltip1label1;
        private MetroSet_UI.Controls.MetroSetControlBox metroSetControlBox1;
        private MetroSet_UI.Controls.MetroSetLabel metroSetLabel9;
        public System.Windows.Forms.Timer timer1;
        public System.Windows.Forms.Timer progressbarTimer;
        public System.Windows.Forms.Timer timer_callByTaskSched;
        public MetroSet_UI.Controls.MetroSetTextBox txtCurrentStatus;
        private MetroSet_UI.Controls.MetroSetButton btnSettings;
        private MetroSet_UI.Controls.MetroSetButton button1;
        private MetroSet_UI.Controls.MetroSetButton btnConnectCSE;
    }
}