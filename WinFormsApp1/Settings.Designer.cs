
namespace AgentApp
{
    partial class frmSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSettings));
            btnNewJob = new MetroSet_UI.Controls.MetroSetButton();
            panel1 = new MetroSet_UI.Controls.MetroSetPanel();
            panel3 = new System.Windows.Forms.Panel();
            metroSetLabel10 = new MetroSet_UI.Controls.MetroSetLabel();
            txtLoadBalancerUrl = new MetroSet_UI.Controls.MetroSetTextBox();
            metroSetLabel3 = new MetroSet_UI.Controls.MetroSetLabel();
            txtTenantUUID = new MetroSet_UI.Controls.MetroSetTextBox();
            metroSetLabel2 = new MetroSet_UI.Controls.MetroSetLabel();
            txtSecretKey = new MetroSet_UI.Controls.MetroSetTextBox();
            metroSetLabel1 = new MetroSet_UI.Controls.MetroSetLabel();
            txtAgentUUID = new MetroSet_UI.Controls.MetroSetTextBox();
            panel2 = new System.Windows.Forms.Panel();
            metroSetLabel7 = new MetroSet_UI.Controls.MetroSetLabel();
            txtPassword = new MetroSet_UI.Controls.MetroSetTextBox();
            metroSetLabel8 = new MetroSet_UI.Controls.MetroSetLabel();
            txtUsername = new MetroSet_UI.Controls.MetroSetTextBox();
            metroSetLabel9 = new MetroSet_UI.Controls.MetroSetLabel();
            txtDatabase = new MetroSet_UI.Controls.MetroSetTextBox();
            metroSetLabel4 = new MetroSet_UI.Controls.MetroSetLabel();
            txtPort = new MetroSet_UI.Controls.MetroSetTextBox();
            metroSetLabel5 = new MetroSet_UI.Controls.MetroSetLabel();
            txtHost = new MetroSet_UI.Controls.MetroSetTextBox();
            metroSetLabel6 = new MetroSet_UI.Controls.MetroSetLabel();
            txtDbType = new MetroSet_UI.Controls.MetroSetTextBox();
            chkQuartzJobs = new MetroSet_UI.Controls.MetroSetSwitch();
            folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            metroSetControlBox1 = new MetroSet_UI.Controls.MetroSetControlBox();
            panel1.SuspendLayout();
            panel3.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // btnNewJob
            // 
            btnNewJob.DisabledBackColor = System.Drawing.Color.FromArgb(120, 65, 177, 225);
            btnNewJob.DisabledBorderColor = System.Drawing.Color.FromArgb(120, 65, 177, 225);
            btnNewJob.DisabledForeColor = System.Drawing.Color.Gray;
            btnNewJob.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            btnNewJob.HoverBorderColor = System.Drawing.Color.FromArgb(95, 207, 255);
            btnNewJob.HoverColor = System.Drawing.Color.FromArgb(95, 207, 255);
            btnNewJob.HoverTextColor = System.Drawing.Color.White;
            btnNewJob.IsDerivedStyle = true;
            btnNewJob.Location = new System.Drawing.Point(186, 395);
            btnNewJob.Name = "btnNewJob";
            btnNewJob.NormalBorderColor = System.Drawing.Color.FromArgb(65, 177, 225);
            btnNewJob.NormalColor = System.Drawing.Color.FromArgb(65, 177, 225);
            btnNewJob.NormalTextColor = System.Drawing.Color.White;
            btnNewJob.PressBorderColor = System.Drawing.Color.FromArgb(35, 147, 195);
            btnNewJob.PressColor = System.Drawing.Color.FromArgb(35, 147, 195);
            btnNewJob.PressTextColor = System.Drawing.Color.White;
            btnNewJob.Size = new System.Drawing.Size(120, 52);
            btnNewJob.Style = MetroSet_UI.Enums.Style.Light;
            btnNewJob.StyleManager = null;
            btnNewJob.TabIndex = 10;
            btnNewJob.Text = "Save";
            btnNewJob.ThemeAuthor = "Narwin";
            btnNewJob.ThemeName = "MetroLite";
            btnNewJob.Click += btnNewJob_Click;
            // 
            // panel1
            // 
            panel1.BackgroundColor = System.Drawing.Color.White;
            panel1.BorderColor = System.Drawing.Color.FromArgb(150, 150, 150);
            panel1.BorderThickness = 1;
            panel1.Controls.Add(panel3);
            panel1.Controls.Add(panel2);
            panel1.Controls.Add(chkQuartzJobs);
            panel1.Controls.Add(btnNewJob);
            panel1.IsDerivedStyle = true;
            panel1.Location = new System.Drawing.Point(15, 73);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(469, 462);
            panel1.Style = MetroSet_UI.Enums.Style.Light;
            panel1.StyleManager = null;
            panel1.TabIndex = 16;
            panel1.ThemeAuthor = "Narwin";
            panel1.ThemeName = "MetroLite";
            // 
            // panel3
            // 
            panel3.Controls.Add(metroSetLabel10);
            panel3.Controls.Add(txtLoadBalancerUrl);
            panel3.Controls.Add(metroSetLabel3);
            panel3.Controls.Add(txtTenantUUID);
            panel3.Controls.Add(metroSetLabel2);
            panel3.Controls.Add(txtSecretKey);
            panel3.Controls.Add(metroSetLabel1);
            panel3.Controls.Add(txtAgentUUID);
            panel3.Location = new System.Drawing.Point(17, 11);
            panel3.Name = "panel3";
            panel3.Size = new System.Drawing.Size(440, 155);
            panel3.TabIndex = 64;
            // 
            // metroSetLabel10
            // 
            metroSetLabel10.AutoSize = true;
            metroSetLabel10.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            metroSetLabel10.IsDerivedStyle = true;
            metroSetLabel10.Location = new System.Drawing.Point(0, 115);
            metroSetLabel10.Name = "metroSetLabel10";
            metroSetLabel10.Size = new System.Drawing.Size(132, 17);
            metroSetLabel10.Style = MetroSet_UI.Enums.Style.Light;
            metroSetLabel10.StyleManager = null;
            metroSetLabel10.TabIndex = 46;
            metroSetLabel10.Text = "Load Balancer URL";
            metroSetLabel10.ThemeAuthor = "Narwin";
            metroSetLabel10.ThemeName = "MetroLite";
            // 
            // txtLoadBalancerUrl
            // 
            txtLoadBalancerUrl.AutoCompleteCustomSource = null;
            txtLoadBalancerUrl.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            txtLoadBalancerUrl.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            txtLoadBalancerUrl.BorderColor = System.Drawing.Color.FromArgb(155, 155, 155);
            txtLoadBalancerUrl.DisabledBackColor = System.Drawing.Color.FromArgb(204, 204, 204);
            txtLoadBalancerUrl.DisabledBorderColor = System.Drawing.Color.FromArgb(155, 155, 155);
            txtLoadBalancerUrl.DisabledForeColor = System.Drawing.Color.FromArgb(136, 136, 136);
            txtLoadBalancerUrl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            txtLoadBalancerUrl.HoverColor = System.Drawing.Color.FromArgb(102, 102, 102);
            txtLoadBalancerUrl.Image = null;
            txtLoadBalancerUrl.IsDerivedStyle = true;
            txtLoadBalancerUrl.Lines = null;
            txtLoadBalancerUrl.Location = new System.Drawing.Point(135, 118);
            txtLoadBalancerUrl.MaxLength = 100;
            txtLoadBalancerUrl.Multiline = false;
            txtLoadBalancerUrl.Name = "txtLoadBalancerUrl";
            txtLoadBalancerUrl.ReadOnly = false;
            txtLoadBalancerUrl.Size = new System.Drawing.Size(297, 27);
            txtLoadBalancerUrl.Style = MetroSet_UI.Enums.Style.Light;
            txtLoadBalancerUrl.StyleManager = null;
            txtLoadBalancerUrl.TabIndex = 45;
            txtLoadBalancerUrl.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            txtLoadBalancerUrl.ThemeAuthor = "Narwin";
            txtLoadBalancerUrl.ThemeName = "MetroLite";
            txtLoadBalancerUrl.UseSystemPasswordChar = false;
            txtLoadBalancerUrl.WatermarkText = "";
            // 
            // metroSetLabel3
            // 
            metroSetLabel3.AutoSize = true;
            metroSetLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            metroSetLabel3.IsDerivedStyle = true;
            metroSetLabel3.Location = new System.Drawing.Point(0, 82);
            metroSetLabel3.Name = "metroSetLabel3";
            metroSetLabel3.Size = new System.Drawing.Size(90, 17);
            metroSetLabel3.Style = MetroSet_UI.Enums.Style.Light;
            metroSetLabel3.StyleManager = null;
            metroSetLabel3.TabIndex = 44;
            metroSetLabel3.Text = "Tenant UUID";
            metroSetLabel3.ThemeAuthor = "Narwin";
            metroSetLabel3.ThemeName = "MetroLite";
            // 
            // txtTenantUUID
            // 
            txtTenantUUID.AutoCompleteCustomSource = null;
            txtTenantUUID.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            txtTenantUUID.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            txtTenantUUID.BorderColor = System.Drawing.Color.FromArgb(155, 155, 155);
            txtTenantUUID.DisabledBackColor = System.Drawing.Color.FromArgb(204, 204, 204);
            txtTenantUUID.DisabledBorderColor = System.Drawing.Color.FromArgb(155, 155, 155);
            txtTenantUUID.DisabledForeColor = System.Drawing.Color.FromArgb(136, 136, 136);
            txtTenantUUID.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            txtTenantUUID.HoverColor = System.Drawing.Color.FromArgb(102, 102, 102);
            txtTenantUUID.Image = null;
            txtTenantUUID.IsDerivedStyle = true;
            txtTenantUUID.Lines = null;
            txtTenantUUID.Location = new System.Drawing.Point(135, 85);
            txtTenantUUID.MaxLength = 100;
            txtTenantUUID.Multiline = false;
            txtTenantUUID.Name = "txtTenantUUID";
            txtTenantUUID.ReadOnly = false;
            txtTenantUUID.Size = new System.Drawing.Size(297, 27);
            txtTenantUUID.Style = MetroSet_UI.Enums.Style.Light;
            txtTenantUUID.StyleManager = null;
            txtTenantUUID.TabIndex = 3;
            txtTenantUUID.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            txtTenantUUID.ThemeAuthor = "Narwin";
            txtTenantUUID.ThemeName = "MetroLite";
            txtTenantUUID.UseSystemPasswordChar = false;
            txtTenantUUID.WatermarkText = "";
            // 
            // metroSetLabel2
            // 
            metroSetLabel2.AutoSize = true;
            metroSetLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            metroSetLabel2.IsDerivedStyle = true;
            metroSetLabel2.Location = new System.Drawing.Point(0, 49);
            metroSetLabel2.Name = "metroSetLabel2";
            metroSetLabel2.Size = new System.Drawing.Size(77, 17);
            metroSetLabel2.Style = MetroSet_UI.Enums.Style.Light;
            metroSetLabel2.StyleManager = null;
            metroSetLabel2.TabIndex = 42;
            metroSetLabel2.Text = "Secret Key";
            metroSetLabel2.ThemeAuthor = "Narwin";
            metroSetLabel2.ThemeName = "MetroLite";
            // 
            // txtSecretKey
            // 
            txtSecretKey.AutoCompleteCustomSource = null;
            txtSecretKey.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            txtSecretKey.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            txtSecretKey.BorderColor = System.Drawing.Color.FromArgb(155, 155, 155);
            txtSecretKey.DisabledBackColor = System.Drawing.Color.FromArgb(204, 204, 204);
            txtSecretKey.DisabledBorderColor = System.Drawing.Color.FromArgb(155, 155, 155);
            txtSecretKey.DisabledForeColor = System.Drawing.Color.FromArgb(136, 136, 136);
            txtSecretKey.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            txtSecretKey.HoverColor = System.Drawing.Color.FromArgb(102, 102, 102);
            txtSecretKey.Image = null;
            txtSecretKey.IsDerivedStyle = true;
            txtSecretKey.Lines = null;
            txtSecretKey.Location = new System.Drawing.Point(135, 52);
            txtSecretKey.MaxLength = 100;
            txtSecretKey.Multiline = false;
            txtSecretKey.Name = "txtSecretKey";
            txtSecretKey.ReadOnly = false;
            txtSecretKey.Size = new System.Drawing.Size(297, 27);
            txtSecretKey.Style = MetroSet_UI.Enums.Style.Light;
            txtSecretKey.StyleManager = null;
            txtSecretKey.TabIndex = 2;
            txtSecretKey.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            txtSecretKey.ThemeAuthor = "Narwin";
            txtSecretKey.ThemeName = "MetroLite";
            txtSecretKey.UseSystemPasswordChar = false;
            txtSecretKey.WatermarkText = "";
            // 
            // metroSetLabel1
            // 
            metroSetLabel1.AutoSize = true;
            metroSetLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            metroSetLabel1.IsDerivedStyle = true;
            metroSetLabel1.Location = new System.Drawing.Point(0, 16);
            metroSetLabel1.Name = "metroSetLabel1";
            metroSetLabel1.Size = new System.Drawing.Size(82, 17);
            metroSetLabel1.Style = MetroSet_UI.Enums.Style.Light;
            metroSetLabel1.StyleManager = null;
            metroSetLabel1.TabIndex = 40;
            metroSetLabel1.Text = "Agent UUID";
            metroSetLabel1.ThemeAuthor = "Narwin";
            metroSetLabel1.ThemeName = "MetroLite";
            // 
            // txtAgentUUID
            // 
            txtAgentUUID.AutoCompleteCustomSource = null;
            txtAgentUUID.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            txtAgentUUID.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            txtAgentUUID.BorderColor = System.Drawing.Color.FromArgb(155, 155, 155);
            txtAgentUUID.DisabledBackColor = System.Drawing.Color.FromArgb(204, 204, 204);
            txtAgentUUID.DisabledBorderColor = System.Drawing.Color.FromArgb(155, 155, 155);
            txtAgentUUID.DisabledForeColor = System.Drawing.Color.FromArgb(136, 136, 136);
            txtAgentUUID.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            txtAgentUUID.HoverColor = System.Drawing.Color.FromArgb(102, 102, 102);
            txtAgentUUID.Image = null;
            txtAgentUUID.IsDerivedStyle = true;
            txtAgentUUID.Lines = null;
            txtAgentUUID.Location = new System.Drawing.Point(135, 19);
            txtAgentUUID.MaxLength = 100;
            txtAgentUUID.Multiline = false;
            txtAgentUUID.Name = "txtAgentUUID";
            txtAgentUUID.ReadOnly = false;
            txtAgentUUID.Size = new System.Drawing.Size(297, 27);
            txtAgentUUID.Style = MetroSet_UI.Enums.Style.Light;
            txtAgentUUID.StyleManager = null;
            txtAgentUUID.TabIndex = 1;
            txtAgentUUID.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            txtAgentUUID.ThemeAuthor = "Narwin";
            txtAgentUUID.ThemeName = "MetroLite";
            txtAgentUUID.UseSystemPasswordChar = false;
            txtAgentUUID.WatermarkText = "";
            // 
            // panel2
            // 
            panel2.Controls.Add(metroSetLabel7);
            panel2.Controls.Add(txtPassword);
            panel2.Controls.Add(metroSetLabel8);
            panel2.Controls.Add(txtUsername);
            panel2.Controls.Add(metroSetLabel9);
            panel2.Controls.Add(txtDatabase);
            panel2.Controls.Add(metroSetLabel4);
            panel2.Controls.Add(txtPort);
            panel2.Controls.Add(metroSetLabel5);
            panel2.Controls.Add(txtHost);
            panel2.Controls.Add(metroSetLabel6);
            panel2.Controls.Add(txtDbType);
            panel2.Location = new System.Drawing.Point(17, 176);
            panel2.Name = "panel2";
            panel2.Size = new System.Drawing.Size(440, 213);
            panel2.TabIndex = 66;
            // 
            // metroSetLabel7
            // 
            metroSetLabel7.AutoSize = true;
            metroSetLabel7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            metroSetLabel7.IsDerivedStyle = true;
            metroSetLabel7.Location = new System.Drawing.Point(5, 176);
            metroSetLabel7.Name = "metroSetLabel7";
            metroSetLabel7.Size = new System.Drawing.Size(69, 17);
            metroSetLabel7.Style = MetroSet_UI.Enums.Style.Light;
            metroSetLabel7.StyleManager = null;
            metroSetLabel7.TabIndex = 68;
            metroSetLabel7.Text = "Password";
            metroSetLabel7.ThemeAuthor = "Narwin";
            metroSetLabel7.ThemeName = "MetroLite";
            // 
            // txtPassword
            // 
            txtPassword.AutoCompleteCustomSource = null;
            txtPassword.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            txtPassword.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            txtPassword.BorderColor = System.Drawing.Color.FromArgb(155, 155, 155);
            txtPassword.DisabledBackColor = System.Drawing.Color.FromArgb(204, 204, 204);
            txtPassword.DisabledBorderColor = System.Drawing.Color.FromArgb(155, 155, 155);
            txtPassword.DisabledForeColor = System.Drawing.Color.FromArgb(136, 136, 136);
            txtPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            txtPassword.HoverColor = System.Drawing.Color.FromArgb(102, 102, 102);
            txtPassword.Image = null;
            txtPassword.IsDerivedStyle = true;
            txtPassword.Lines = null;
            txtPassword.Location = new System.Drawing.Point(135, 176);
            txtPassword.MaxLength = 100;
            txtPassword.Multiline = false;
            txtPassword.Name = "txtPassword";
            txtPassword.ReadOnly = false;
            txtPassword.Size = new System.Drawing.Size(297, 27);
            txtPassword.Style = MetroSet_UI.Enums.Style.Light;
            txtPassword.StyleManager = null;
            txtPassword.TabIndex = 9;
            txtPassword.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            txtPassword.ThemeAuthor = "Narwin";
            txtPassword.ThemeName = "MetroLite";
            txtPassword.UseSystemPasswordChar = false;
            txtPassword.WatermarkText = "";
            // 
            // metroSetLabel8
            // 
            metroSetLabel8.AutoSize = true;
            metroSetLabel8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            metroSetLabel8.IsDerivedStyle = true;
            metroSetLabel8.Location = new System.Drawing.Point(5, 143);
            metroSetLabel8.Name = "metroSetLabel8";
            metroSetLabel8.Size = new System.Drawing.Size(79, 17);
            metroSetLabel8.Style = MetroSet_UI.Enums.Style.Light;
            metroSetLabel8.StyleManager = null;
            metroSetLabel8.TabIndex = 66;
            metroSetLabel8.Text = "User Name";
            metroSetLabel8.ThemeAuthor = "Narwin";
            metroSetLabel8.ThemeName = "MetroLite";
            // 
            // txtUsername
            // 
            txtUsername.AutoCompleteCustomSource = null;
            txtUsername.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            txtUsername.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            txtUsername.BorderColor = System.Drawing.Color.FromArgb(155, 155, 155);
            txtUsername.DisabledBackColor = System.Drawing.Color.FromArgb(204, 204, 204);
            txtUsername.DisabledBorderColor = System.Drawing.Color.FromArgb(155, 155, 155);
            txtUsername.DisabledForeColor = System.Drawing.Color.FromArgb(136, 136, 136);
            txtUsername.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            txtUsername.HoverColor = System.Drawing.Color.FromArgb(102, 102, 102);
            txtUsername.Image = null;
            txtUsername.IsDerivedStyle = true;
            txtUsername.Lines = null;
            txtUsername.Location = new System.Drawing.Point(135, 143);
            txtUsername.MaxLength = 100;
            txtUsername.Multiline = false;
            txtUsername.Name = "txtUsername";
            txtUsername.ReadOnly = false;
            txtUsername.Size = new System.Drawing.Size(297, 27);
            txtUsername.Style = MetroSet_UI.Enums.Style.Light;
            txtUsername.StyleManager = null;
            txtUsername.TabIndex = 8;
            txtUsername.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            txtUsername.ThemeAuthor = "Narwin";
            txtUsername.ThemeName = "MetroLite";
            txtUsername.UseSystemPasswordChar = false;
            txtUsername.WatermarkText = "";
            // 
            // metroSetLabel9
            // 
            metroSetLabel9.AutoSize = true;
            metroSetLabel9.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            metroSetLabel9.IsDerivedStyle = true;
            metroSetLabel9.Location = new System.Drawing.Point(5, 110);
            metroSetLabel9.Name = "metroSetLabel9";
            metroSetLabel9.Size = new System.Drawing.Size(110, 17);
            metroSetLabel9.Style = MetroSet_UI.Enums.Style.Light;
            metroSetLabel9.StyleManager = null;
            metroSetLabel9.TabIndex = 64;
            metroSetLabel9.Text = "Database Name";
            metroSetLabel9.ThemeAuthor = "Narwin";
            metroSetLabel9.ThemeName = "MetroLite";
            // 
            // txtDatabase
            // 
            txtDatabase.AutoCompleteCustomSource = null;
            txtDatabase.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            txtDatabase.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            txtDatabase.BorderColor = System.Drawing.Color.FromArgb(155, 155, 155);
            txtDatabase.DisabledBackColor = System.Drawing.Color.FromArgb(204, 204, 204);
            txtDatabase.DisabledBorderColor = System.Drawing.Color.FromArgb(155, 155, 155);
            txtDatabase.DisabledForeColor = System.Drawing.Color.FromArgb(136, 136, 136);
            txtDatabase.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            txtDatabase.HoverColor = System.Drawing.Color.FromArgb(102, 102, 102);
            txtDatabase.Image = null;
            txtDatabase.IsDerivedStyle = true;
            txtDatabase.Lines = null;
            txtDatabase.Location = new System.Drawing.Point(135, 110);
            txtDatabase.MaxLength = 100;
            txtDatabase.Multiline = false;
            txtDatabase.Name = "txtDatabase";
            txtDatabase.ReadOnly = false;
            txtDatabase.Size = new System.Drawing.Size(297, 27);
            txtDatabase.Style = MetroSet_UI.Enums.Style.Light;
            txtDatabase.StyleManager = null;
            txtDatabase.TabIndex = 7;
            txtDatabase.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            txtDatabase.ThemeAuthor = "Narwin";
            txtDatabase.ThemeName = "MetroLite";
            txtDatabase.UseSystemPasswordChar = false;
            txtDatabase.WatermarkText = "";
            // 
            // metroSetLabel4
            // 
            metroSetLabel4.AutoSize = true;
            metroSetLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            metroSetLabel4.IsDerivedStyle = true;
            metroSetLabel4.Location = new System.Drawing.Point(5, 75);
            metroSetLabel4.Name = "metroSetLabel4";
            metroSetLabel4.Size = new System.Drawing.Size(34, 17);
            metroSetLabel4.Style = MetroSet_UI.Enums.Style.Light;
            metroSetLabel4.StyleManager = null;
            metroSetLabel4.TabIndex = 62;
            metroSetLabel4.Text = "Port";
            metroSetLabel4.ThemeAuthor = "Narwin";
            metroSetLabel4.ThemeName = "MetroLite";
            // 
            // txtPort
            // 
            txtPort.AutoCompleteCustomSource = null;
            txtPort.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            txtPort.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            txtPort.BorderColor = System.Drawing.Color.FromArgb(155, 155, 155);
            txtPort.DisabledBackColor = System.Drawing.Color.FromArgb(204, 204, 204);
            txtPort.DisabledBorderColor = System.Drawing.Color.FromArgb(155, 155, 155);
            txtPort.DisabledForeColor = System.Drawing.Color.FromArgb(136, 136, 136);
            txtPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            txtPort.HoverColor = System.Drawing.Color.FromArgb(102, 102, 102);
            txtPort.Image = null;
            txtPort.IsDerivedStyle = true;
            txtPort.Lines = null;
            txtPort.Location = new System.Drawing.Point(135, 75);
            txtPort.MaxLength = 100;
            txtPort.Multiline = false;
            txtPort.Name = "txtPort";
            txtPort.ReadOnly = false;
            txtPort.Size = new System.Drawing.Size(297, 27);
            txtPort.Style = MetroSet_UI.Enums.Style.Light;
            txtPort.StyleManager = null;
            txtPort.TabIndex = 6;
            txtPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            txtPort.ThemeAuthor = "Narwin";
            txtPort.ThemeName = "MetroLite";
            txtPort.UseSystemPasswordChar = false;
            txtPort.WatermarkText = "";
            // 
            // metroSetLabel5
            // 
            metroSetLabel5.AutoSize = true;
            metroSetLabel5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            metroSetLabel5.IsDerivedStyle = true;
            metroSetLabel5.Location = new System.Drawing.Point(5, 42);
            metroSetLabel5.Name = "metroSetLabel5";
            metroSetLabel5.Size = new System.Drawing.Size(37, 17);
            metroSetLabel5.Style = MetroSet_UI.Enums.Style.Light;
            metroSetLabel5.StyleManager = null;
            metroSetLabel5.TabIndex = 60;
            metroSetLabel5.Text = "Host";
            metroSetLabel5.ThemeAuthor = "Narwin";
            metroSetLabel5.ThemeName = "MetroLite";
            // 
            // txtHost
            // 
            txtHost.AutoCompleteCustomSource = null;
            txtHost.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            txtHost.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            txtHost.BorderColor = System.Drawing.Color.FromArgb(155, 155, 155);
            txtHost.DisabledBackColor = System.Drawing.Color.FromArgb(204, 204, 204);
            txtHost.DisabledBorderColor = System.Drawing.Color.FromArgb(155, 155, 155);
            txtHost.DisabledForeColor = System.Drawing.Color.FromArgb(136, 136, 136);
            txtHost.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            txtHost.HoverColor = System.Drawing.Color.FromArgb(102, 102, 102);
            txtHost.Image = null;
            txtHost.IsDerivedStyle = true;
            txtHost.Lines = null;
            txtHost.Location = new System.Drawing.Point(135, 42);
            txtHost.MaxLength = 100;
            txtHost.Multiline = false;
            txtHost.Name = "txtHost";
            txtHost.ReadOnly = false;
            txtHost.Size = new System.Drawing.Size(297, 27);
            txtHost.Style = MetroSet_UI.Enums.Style.Light;
            txtHost.StyleManager = null;
            txtHost.TabIndex = 5;
            txtHost.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            txtHost.ThemeAuthor = "Narwin";
            txtHost.ThemeName = "MetroLite";
            txtHost.UseSystemPasswordChar = false;
            txtHost.WatermarkText = "";
            // 
            // metroSetLabel6
            // 
            metroSetLabel6.AutoSize = true;
            metroSetLabel6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            metroSetLabel6.IsDerivedStyle = true;
            metroSetLabel6.Location = new System.Drawing.Point(5, 9);
            metroSetLabel6.Name = "metroSetLabel6";
            metroSetLabel6.Size = new System.Drawing.Size(105, 17);
            metroSetLabel6.Style = MetroSet_UI.Enums.Style.Light;
            metroSetLabel6.StyleManager = null;
            metroSetLabel6.TabIndex = 58;
            metroSetLabel6.Text = "Database Type";
            metroSetLabel6.ThemeAuthor = "Narwin";
            metroSetLabel6.ThemeName = "MetroLite";
            // 
            // txtDbType
            // 
            txtDbType.AutoCompleteCustomSource = null;
            txtDbType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            txtDbType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            txtDbType.BorderColor = System.Drawing.Color.FromArgb(155, 155, 155);
            txtDbType.DisabledBackColor = System.Drawing.Color.FromArgb(204, 204, 204);
            txtDbType.DisabledBorderColor = System.Drawing.Color.FromArgb(155, 155, 155);
            txtDbType.DisabledForeColor = System.Drawing.Color.FromArgb(136, 136, 136);
            txtDbType.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            txtDbType.HoverColor = System.Drawing.Color.FromArgb(102, 102, 102);
            txtDbType.Image = null;
            txtDbType.IsDerivedStyle = true;
            txtDbType.Lines = null;
            txtDbType.Location = new System.Drawing.Point(135, 9);
            txtDbType.MaxLength = 100;
            txtDbType.Multiline = false;
            txtDbType.Name = "txtDbType";
            txtDbType.ReadOnly = false;
            txtDbType.Size = new System.Drawing.Size(297, 27);
            txtDbType.Style = MetroSet_UI.Enums.Style.Light;
            txtDbType.StyleManager = null;
            txtDbType.TabIndex = 4;
            txtDbType.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            txtDbType.ThemeAuthor = "Narwin";
            txtDbType.ThemeName = "MetroLite";
            txtDbType.UseSystemPasswordChar = false;
            txtDbType.WatermarkText = "";
            // 
            // chkQuartzJobs
            // 
            chkQuartzJobs.BackColor = System.Drawing.Color.Transparent;
            chkQuartzJobs.BackgroundColor = System.Drawing.Color.Empty;
            chkQuartzJobs.BorderColor = System.Drawing.Color.FromArgb(165, 159, 147);
            chkQuartzJobs.CheckColor = System.Drawing.Color.FromArgb(65, 177, 225);
            chkQuartzJobs.CheckState = MetroSet_UI.Enums.CheckState.Unchecked;
            chkQuartzJobs.Cursor = System.Windows.Forms.Cursors.Hand;
            chkQuartzJobs.DisabledBorderColor = System.Drawing.Color.FromArgb(205, 205, 205);
            chkQuartzJobs.DisabledCheckColor = System.Drawing.Color.FromArgb(100, 65, 177, 225);
            chkQuartzJobs.DisabledUnCheckColor = System.Drawing.Color.FromArgb(200, 205, 205, 205);
            chkQuartzJobs.IsDerivedStyle = true;
            chkQuartzJobs.Location = new System.Drawing.Point(0, 0);
            chkQuartzJobs.Name = "chkQuartzJobs";
            chkQuartzJobs.Size = new System.Drawing.Size(0, 0);
            chkQuartzJobs.Style = MetroSet_UI.Enums.Style.Light;
            chkQuartzJobs.StyleManager = null;
            chkQuartzJobs.Switched = false;
            chkQuartzJobs.SymbolColor = System.Drawing.Color.FromArgb(92, 92, 92);
            chkQuartzJobs.TabIndex = 3;
            chkQuartzJobs.ThemeAuthor = "Narwin";
            chkQuartzJobs.ThemeName = "MetroLite";
            chkQuartzJobs.UnCheckColor = System.Drawing.Color.FromArgb(155, 155, 155);
            // 
            // metroSetControlBox1
            // 
            metroSetControlBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            metroSetControlBox1.CloseHoverBackColor = System.Drawing.Color.FromArgb(183, 40, 40);
            metroSetControlBox1.CloseHoverForeColor = System.Drawing.Color.White;
            metroSetControlBox1.CloseNormalForeColor = System.Drawing.Color.Gray;
            metroSetControlBox1.DisabledForeColor = System.Drawing.Color.DimGray;
            metroSetControlBox1.IsDerivedStyle = false;
            metroSetControlBox1.Location = new System.Drawing.Point(384, 7);
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
            metroSetControlBox1.TabIndex = 17;
            metroSetControlBox1.Text = "metroSetControlBox1";
            metroSetControlBox1.ThemeAuthor = "Narwin";
            metroSetControlBox1.ThemeName = "MetroLite";
            // 
            // frmSettings
            // 
            AllowResize = false;
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            BackColor = System.Drawing.SystemColors.Control;
            BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            BorderThickness = 10F;
            ClientSize = new System.Drawing.Size(496, 550);
            ControlBox = false;
            Controls.Add(metroSetControlBox1);
            Controls.Add(panel1);
            Font = new System.Drawing.Font("Bookman Old Style", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            HeaderHeight = 10;
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Name = "frmSettings";
            ShowBorder = true;
            ShowIcon = false;
            ShowInTaskbar = false;
            SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Settings";
            TextColor = System.Drawing.Color.Black;
            TopMost = true;
            panel1.ResumeLayout(false);
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private MetroSet_UI.Controls.MetroSetButton btnNewJob;
        private MetroSet_UI.Controls.MetroSetPanel panel1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private MetroSet_UI.Controls.MetroSetSwitch chkQuartzJobs;
        private MetroSet_UI.Controls.MetroSetControlBox metroSetControlBox1;
        private System.Windows.Forms.Panel panel3;
        private MetroSet_UI.Controls.MetroSetLabel metroSetLabel3;
        private MetroSet_UI.Controls.MetroSetTextBox txtTenantUUID;
        private MetroSet_UI.Controls.MetroSetLabel metroSetLabel2;
        private MetroSet_UI.Controls.MetroSetTextBox txtSecretKey;
        private MetroSet_UI.Controls.MetroSetLabel metroSetLabel1;
        private MetroSet_UI.Controls.MetroSetTextBox txtAgentUUID;
        private System.Windows.Forms.Panel panel2;
        private MetroSet_UI.Controls.MetroSetLabel metroSetLabel7;
        private MetroSet_UI.Controls.MetroSetTextBox txtPassword;
        private MetroSet_UI.Controls.MetroSetLabel metroSetLabel8;
        private MetroSet_UI.Controls.MetroSetTextBox txtUsername;
        private MetroSet_UI.Controls.MetroSetLabel metroSetLabel9;
        private MetroSet_UI.Controls.MetroSetTextBox txtDatabase;
        private MetroSet_UI.Controls.MetroSetLabel metroSetLabel4;
        private MetroSet_UI.Controls.MetroSetTextBox txtPort;
        private MetroSet_UI.Controls.MetroSetLabel metroSetLabel5;
        private MetroSet_UI.Controls.MetroSetTextBox txtHost;
        private MetroSet_UI.Controls.MetroSetLabel metroSetLabel6;
        private MetroSet_UI.Controls.MetroSetTextBox txtDbType;
        private MetroSet_UI.Controls.MetroSetLabel metroSetLabel10;
        private MetroSet_UI.Controls.MetroSetTextBox txtLoadBalancerUrl;
    }
}