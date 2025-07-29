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
//using AgentApp.Config;

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

            //this.txtDbType.Text = GetSetting("DbType");
            //try { this.chkAlternateDbConnection1.Checked = bool.Parse(GetSetting("AlternateConn")); } catch { this.chkAlternateDbConnection1.Checked = false; }
            GetConnectionStrings();
            this.radioButton1.Checked = GetSetting("DbTypeSQLSelected")== "DbTypeSQLexp" ? true:false;
            this.radioButton2.Checked = GetSetting("DbTypeSQLSelected")== "DbTypeSQLodbc" ? true : false;
            this.cmbDbTypesSQLexp.SelectedItem = GetSetting("DbTypeSQLexp");
            this.cmbDbTypesSQLODBC.SelectedItem = GetSetting("DbTypeSQLodbc");
            this.cmbDbTypesPostgre.SelectedItem = GetSetting("DbTypePostGre");


            //this.txtHost1.Text = GetSetting("Host");
            //this.txtPort1.Text = GetSetting("Port");
            //this.txtDatabase1.Text = GetSetting("Database");
            //this.txtUsername1.Text = GetSetting("Username");
            //this.txtPassword1.Text = GetSetting("Password");
            this.Refresh();

        }

        public void GetConnectionStrings()
        {
            ConnectionStringSettingsCollection settings = ConfigurationManager.ConnectionStrings;
            //cmbDbTypes.Items.AddRange(new object[] { "postgresql", "sqlserver" });
            foreach (ConnectionStringSettings cs in settings)
            {
                cmbDbTypesSQLexp.Items.Add(cs.Name);
                cmbDbTypesSQLODBC.Items.Add(cs.Name);
                cmbDbTypesPostgre.Items.Add(cs.Name);

                //Console.WriteLine(cs.Name);
                //Console.WriteLine(cs.ProviderName);
                //Console.WriteLine(cs.ConnectionString);
            }
        }

        private void btnNewJob_Click(object sender, EventArgs e)
        {
            try
            {
                SetSetting("Agent_uuid", this.txtAgentUUID.Text);
                SetSetting("Secret_key", this.txtSecretKey.Text);
                SetSetting("Tenant_uuid", this.txtTenantUUID.Text);
                SetSetting("LoadBalancerUrl", this.txtLoadBalancerUrl.Text);

                //SetSetting("DbType", this.txtDbType.Text);
                //SetSetting("AlternateConn", this.chkAlternateDbConnection1.Checked.ToString());
                SetSetting("DbTypeSQLSelected", this.radioButton1.Checked? "DbTypeSQLexp": "DbTypeSQLodbc");
                SetSetting("DbTypeSQLexp", this.cmbDbTypesSQLexp.SelectedItem.ToString());
                SetSetting("DbTypeSQLodbc", this.cmbDbTypesSQLODBC.SelectedItem.ToString());
                SetSetting("DbTypePostgre", this.cmbDbTypesPostgre.SelectedItem.ToString());

                //SetSetting("Host", this.txtHost1.Text);
                //SetSetting("Port", this.txtPort1.Text);
                //SetSetting("Database", this.txtDatabase1.Text);
                //SetSetting("Username", this.txtUsername1.Text);
                //SetSetting("Password", this.txtPassword1.Text);
                //DatabaseConfig._credentials["DbTypeSQLexp"].DbType = this.cmbDbTypesSQLexp.SelectedItem.ToString();
                //DatabaseConfig._credentials["DbTypeSQLodbc"].DbType = this.cmbDbTypesSQLODBC.SelectedItem.ToString();
                //DatabaseConfig._credentials["DbTypePostgre"].DbType = this.cmbDbTypesPostgre.SelectedItem.ToString();

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
