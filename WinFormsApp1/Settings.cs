//using NLog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Globalization;
using System.IO;
using MetroSet_UI.Forms;
using NLog;

namespace AgentApp
{
    public partial class frmSettings : MetroSetForm
    {
        public frmSettings()
        {
            InitializeComponent();
            this.txtAgentUUID.Text = GetSetting("Agent_uuid");
            this.txtSecretKey.Text = GetSetting("Secret_key");
            this.txtTenantUUID.Text = GetSetting("Tenant_uuid");
            this.txtLoadBalancerUrl.Text = GetSetting("LoadBalancerUrl");
            

            this.txtDbType.Text = GetSetting("DbType");
            this.txtHost.Text = GetSetting("Host");
            this.txtPort.Text = GetSetting("Port");
            this.txtDatabase.Text = GetSetting("Database");
            this.txtUsername.Text = GetSetting("Username");
            this.txtPassword.Text = GetSetting("Password");
            this.Refresh();

        }

        private void btnNewJob_Click(object sender, EventArgs e)
        {
            try
            {
                SetSetting("Agent_uuid", this.txtAgentUUID.Text);
                SetSetting("Secret_key", this.txtSecretKey.Text);
                SetSetting("Tenant_uuid", this.txtTenantUUID.Text);
                SetSetting("LoadBalancerUrl", this.txtLoadBalancerUrl.Text);

                SetSetting("DbType", this.txtDbType.Text);
                SetSetting("Host", this.txtHost.Text);
                SetSetting("Port", this.txtPort.Text);
                SetSetting("Database", this.txtDatabase.Text);
                SetSetting("Username", this.txtUsername.Text);
                SetSetting("Password", this.txtPassword.Text);

                Program.loger.Log(LogLevel.Info, " Credentials Changed/Updated ....... " + DateTime.Now);
                MessageBox.Show("New Changes Saved Successfully!");
            }catch (Exception ex){ MessageBox.Show("Please give user permissions for saving application settings!","Error while saving settings!",MessageBoxButtons.OK,MessageBoxIcon.Warning); }
            this.Hide();

        }

        private String GetSetting(string key)
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
    }
       
}
