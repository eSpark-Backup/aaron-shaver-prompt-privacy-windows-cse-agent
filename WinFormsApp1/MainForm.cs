using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroSet_UI.Forms;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Buffers;
using System.Reflection;
using NLog;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using Microsoft.Win32;
using AgentApp;
using System.Collections.Concurrent;


namespace eSpark_WindowsFIA
{
    public partial class MainForm : MetroSetForm
    {
        public MainForm()
        {
            InitializeComponent();
            timer1.Start();
            stopwatch.Reset();

        }

        private ConcurrentDictionary<string, Agent> agents = new();
        //public static String collectedText = "";
        //public static StringBuilder sb_collectedText = new StringBuilder();
        public static int progressCounter = 0;
        public static int LogsLevel = 0;
        public static String logfilePath = "";
        //        public static NLog.Logger loger;
        public static double MaxSize = double.MaxValue;
        public static double MinSize = 0;
        public static String uuid = GetSetting("Agent_uuid");
        public static String TenantUUID = GetSetting("Tenant_UUID");
        public static String type = "database_indexer";
        public static String SecretKey = GetSetting("Secret_Key");
        public static String Load = GetSetting("Secret_Key");
        public Agent agent = new Agent(uuid, type, SecretKey, TenantUUID);
        public static Stopwatch stopwatch = new Stopwatch();



        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                FSW_trayicon.Visible = true;
                //e.Cancel = true;
                this.WindowState = FormWindowState.Minimized;
                this.Hide();

                if (this.notifyIcon1 == null || this.notifyIcon1.Container == null)
                {
                    this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
                    this.FSW_trayicon = new System.Windows.Forms.NotifyIcon(this.components);
                    //this.notifyIcon1 = new NotifyIcon(); 
                }

                this.notifyIcon1.Icon = new Icon(AssemblyDirectory + "\\Resources\\logop256.ico");
                this.notifyIcon1.Text = "PromptPrivacy database indexer - Postgre !";
                this.notifyIcon1.Visible = true;
                this.notifyIcon1.BalloonTipTitle = "PromptPrivacy database indexer Postgre - is still running in background";
                this.notifyIcon1.BalloonTipText = "Click to open the window again!";
                //this.notifyIcon1.Click += new System.EventHandler(this.notifyIcon1_Click);
                this.notifyIcon1.BalloonTipClicked += new System.EventHandler(this.notifyIcon1_Click);
                this.notifyIcon1.ShowBalloonTip(100);
                this.FSW_trayicon.Visible = false;
            }
            else
            {
                Program.loger.Log(LogLevel.Info, "==================Closing Windows File Logger===============" + DateTime.Now);

                FSW_trayicon.Visible = false;

                NLog.LogManager.Shutdown(); // Flush and close down internal threads and timers
            }
        }


        private void Exit_toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Do you really want to close this application?", "Please confirm before closing!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            if (dialogResult == DialogResult.Yes)
            {
                FSW_trayicon.Visible = false;
                this.Close();
            }

        }

        public static string AssemblyDirectory
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This release version is 20.06.25 Dated December 2025", "This release version is 20.06.25 Dated December 2025", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
        }

        private static String GetSetting(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        private void SetSetting(string key, string value)
        {
            Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            if (configuration.AppSettings.Settings[key] != null)
                configuration.AppSettings.Settings[key].Value = value;
            else configuration.AppSettings.Settings.Add(key, value);

            configuration.Save(ConfigurationSaveMode.Modified, true);
            ConfigurationManager.RefreshSection("appSettings");
        }
        private void FSWopen_toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            FSW_trayicon.Visible = true;
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }

        #region Public Methods

        /// <summary>
        ///   Shows the dialog...
        /// </summary>
        /// <returns>OK, if the user selected a folder or Cancel, if no folder is selected.</returns>
        /// 
        public string Description { get; set; } = "Chose folder...";

        /// <summary>
        ///   The ROOT path!
        /// </summary>
        public string RootPath { get; set; } = "";

        /// <summary>
        ///   The SelectedPath. Here is no initialization possible.
        /// </summary>
        public string SelectedPath { get; private set; } = "";

        private DialogResult ShowDialog1()
        {
            var shellType = Type.GetTypeFromProgID("Shell.Application");
            var shell = Activator.CreateInstance(shellType);
            var folder = shellType.InvokeMember("BrowseForFolder", BindingFlags.InvokeMethod, null, shell, new object[] { 0, Description, 0, RootPath, });
            if (folder is null)
            {
                return DialogResult.Cancel;
            }
            else
            {
                var folderSelf = folder.GetType().InvokeMember("Self", BindingFlags.GetProperty, null, folder, null);
                SelectedPath = folderSelf.GetType().InvokeMember("Path", BindingFlags.GetProperty, null,folderSelf, null) as string;
                // maybe ensure that SelectedPath is set
                return DialogResult.OK;
            }
        }

        #endregion Public Methods
        public static String txtResult = "";


        private void notifyIcon1_Click(object sender, EventArgs e)
        {
            this.notifyIcon1.Visible = false;
            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.FSW_trayicon.Visible = true;
        }

        private void Form1_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            try { System.Diagnostics.Process.Start(AssemblyDirectory + "\\Help.chm"); } catch { }
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            frmSettings openset = new frmSettings();
            openset.ShowDialog();

        }
        private void updateUserMsg(String msg)
        {
            txtCurrentStatus.Text += msg + Environment.NewLine;
            Program.loger.Log(LogLevel.Info, msg + DateTime.Now);
        }

        private void LogToUI(string msg)
        {
            try
            {
                if (InvokeRequired)
                {
                    Invoke(new Action(() => updateUserMsg(">> " + msg)));
                }
                else
                {
                    txtCurrentStatus.Text += ">> " + msg + Environment.NewLine;
                    Program.loger.Log(LogLevel.Info, msg + DateTime.Now);
                }
            }catch (Exception ex){ MessageBox.Show("Please give user permissions for saving application Logs!", "Error while saving Logs!", MessageBoxButtons.OK,MessageBoxIcon.Warning); }
}

        private async void btnConnectCSE_Click(object sender, EventArgs e)
        {
            //this.txtDbType.Text = GetSetting("DbType");
            //this.txtHost.Text = GetSetting("Host");
            //this.txtPort.Text = GetSetting("Port");
            //this.txtDatabase.Text = GetSetting("Database");
            //this.txtUsername.Text = GetSetting("Username");
            //this.txtPassword.Text = GetSetting("Password");
            //this.txtAgentUUID.Text = GetSetting("Agent_uuid");
            //this.txtSecretKey.Text = GetSetting("Secret_key");
            //this.txtTenantUUID.Text = GetSetting("Tenant_uuid");


            var uuid = GetSetting("Agent_uuid");
            var type = "database_indexer";//cmbAgentType.Text.Trim();

            uuid = GetSetting("Agent_uuid");
            TenantUUID = GetSetting("Tenant_UUID");
            type = "database_indexer";
            SecretKey = GetSetting("Secret_Key");
            agent = new Agent(uuid, type, SecretKey, TenantUUID);

            agent.OnLog += LogToUI;
            agents[uuid] = agent;
            await agent.StartAsync();

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (agent == null)
            {
                uuid = GetSetting("Agent_uuid");
                TenantUUID = GetSetting("Tenant_UUID");
                type = "database_indexer";
                SecretKey = GetSetting("Secret_Key");
                agent = new Agent(uuid, type, SecretKey, TenantUUID);

                agent.OnLog += LogToUI;
                agents[uuid] = agent;
            }

            try { await agent.CloseStartAsync(); } catch { }

            Program.isConnected = false;
            Program.isQueryRunning = false;


        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            String con = Program.isConnected ? "Connected" : "Disconnected!";
            lblFileSystemWatcher.Text = "Current Status ... " + con + "!..................................................................................";

            if (Program.isConnected)
            {
                if (stopwatch.IsRunning)
                    tootltip1label1.Text = stopwatch.Elapsed + " elapsed";
                else
                {
                    //stopwatch.Reset();
                    stopwatch.Start();
                    tootltip1label1.Text = stopwatch.Elapsed + " elapsed";
                }

            }
        }
    }
}
