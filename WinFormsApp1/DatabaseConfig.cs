using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AgentApp.Config
{
    public class DatabaseCredentials
    {
        public string DbType { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string Database { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string LoadBalancerUrl { get; set; }

        public string ConnectionString
        {
            get
            {
                if (DbType == "postgresql" || DbType == "postgresql")
                {
                    return $"Host={Host};Port={Port};Username={Username};Password={Password};Database={Database};Pooling=true;";
                }
                //else if (DbType == "sqlite")
                //{
                //    return $"Host={Host};Port={Port};Username={Username};Password={Password};Database={Database};Pooling=true;";
                //}
                //else if (DbType == "mysql")
                //{
                //    return $"Host={Host};Port={Port};Username={Username};Password={Password};Database={Database};Pooling=true;";
                //}
                //else if (DbType == "mssql")
                //{
                //    return $"Host={Host};Port={Port};Username={Username};Password={Password};Database={Database};Pooling=true;";
                //}
                //else if (DbType == "oracle")
                //{
                //    return $"Host={Host};Port={Port};Username={Username};Password={Password};Database={Database};Pooling=true;";
                //}

                throw new NotSupportedException($"Unsupported DB type: {DbType}");
            }
        }
    }

    public static class DatabaseConfig
    {
        private static Dictionary<string, DatabaseCredentials> _credentials;

        static DatabaseConfig()
        {
            LoadConfig();
        }

        private static String GetSetting(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
        private static void LoadConfig()
        {
            try
            {
                //var json = File.ReadAllText("dbconfig.json");
                //_credentials = JsonSerializer.Deserialize<Dictionary<string, DatabaseCredentials>>(json);
                DatabaseCredentials dbcreds = new DatabaseCredentials();
                _credentials = new Dictionary<string, DatabaseCredentials>();
                dbcreds.DbType = GetSetting("DbType");
                dbcreds.Host = GetSetting("Host");
                dbcreds.Port = int.Parse(GetSetting("Port"));
                dbcreds.Database = GetSetting("Database");
                dbcreds.DbType = GetSetting("DbType");
                dbcreds.Username = GetSetting("Username");
                dbcreds.Password = GetSetting("Password");
                dbcreds.LoadBalancerUrl = GetSetting("LoadBalancerUrl");
                 

                _credentials.Add("DbType", dbcreds);
                _credentials.Add("Host", dbcreds);
                _credentials.Add("Port", dbcreds);
                _credentials.Add("Database", dbcreds);                
                _credentials.Add("Username", dbcreds);
                _credentials.Add("Password", dbcreds);


            }
            catch (Exception ex)
            {
                //Console.WriteLine($"Failed to load DB config: {ex.Message}");
                Program.loger.Log(LogLevel.Info, " Error while DB LoadConfig .... " + $"Failed to load DB config: {ex.Message}");
                _credentials = new Dictionary<string, DatabaseCredentials>();
            }
        }

        public static DatabaseCredentials GetCredentials(string databaseName)
        {
            if (_credentials == null || _credentials.Count<1)
                LoadConfig();

            if (_credentials.ContainsKey(databaseName))
            {
                return _credentials[databaseName];
            }

            if (_credentials.ContainsKey("Database"))
            {
                return _credentials["Database"];
            }


            return null;
        }
    }
}