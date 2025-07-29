using Microsoft.VisualBasic.ApplicationServices;
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
using System.Xml.Linq;

namespace AgentApp.Config
{
    public class DatabaseCredentials
    {
        //public string SQLDriver { get; set; }
        public string DbType { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string Database { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string LoadBalancerUrl { get; set; }
        //public static bool AlternateConn { get; set; }

        private static String GetSetting(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        public string ConnectionString
        {//https://www.connectionstrings.com/sql-server/
            get
            {
                if (DbType == "postgresql" || DbType == "postgresql")
                {
                    return $"Host={Host};Port={Port};Username={Username};Password={Password};Database={Database};Pooling=true;";
                    //return @"Driver={ODBC Driver 17 for SQL Server};Server=MASB\SQLEXPRESS;Database=PSVAT5;Trusted_Connection=Yes;";
                    //return @"Driver={ODBC Driver 17 for SQL Server};Server=MASB\SQLEXPRESS;Database=PSVAT5;Uid=postgres;Pwd=1122;";
                    //return @"Driver={ODBC Driver 17 for SQL Server};Server={Host}\SQLEXPRESS;Database={Database};Trusted_Connection=Yes; providerName=System.Data.Odbc";
                }
                //else if (DbType == "sqlite")
                //{
                //    return $"Host={Host};Port={Port};Username={Username};Password={Password};Database={Database};Pooling=true;";
                //}
                //else if (DbType == "mysql")
                //{
                //    return $"Host={Host};Port={Port};Username={Username};Password={Password};Database={Database};Pooling=true;";
                //}
                else if (DbType == "sqlserver")
                {
                    //return $"Host={Host};Port={Port};Username={Username};Password={Password};Database={Database};Pooling=true;";

                    //if (AlternateConn)
                    if (DatabaseConfig._credentials["DbType"].DbType.Contains("DBConnectionSQLodbc")) //DatabaseCredentials.AlternateConn)
                        //conn.ConnectionString = "Data Source=ServerName;Initial Catalog=DataBaseName;User id=UserName;Password=Secret;";
                        //$"Data Source=={Host};Initial Catalog={Database};Uid={Username};Pwd={Password};";
                        //Driver={ODBC Driver 17 for SQL Server};Server=.\SQLExpress;AttachDBFileName=c:\dir\\mydb.mdf;Database=dbName;Trusted_Connection=yes;
                        //Server=.\SQLExpress;Database=PSVAT5;User Id=postgres;Password=1122;Trusted_Connection=Yes;

                        //return "Server={Host};Database ={Database};User Id={Username};Password={Password};";
                        return ConfigurationManager.ConnectionStrings[GetSetting("DbTypeSQLodbc")].ConnectionString;

                    else//Driver={ODBC Driver 17 for SQL Server};Server=myServerAddress;Database=myDataBase;UID=myUsername;PWD=myPassword;
                        //return @"Server =.\SQLExpress;Integrated Security=SSPI;Database =PSVAT5; User Id=postgres; Password = 1122; Trusted_Connection = Yes;Initial Catalog=PSVAT5;";
                        //return @"Data Source=(LocalDb)\MSSQLLocalDB;Initial Catalog=PSVAT5;Integrated Security=SSPI;UID=postgres;PWD=1122;TrustServerCertificate=true;";
                        //return $"Driver={SQLDriver};Server={Host},{Port};Database={Database};Uid={Username};Pwd={Password};";
                        //return @"Data Source =.\\SQLExpress; Integrated Security = true;User Instance = true; Initial Catalog = PSVAT5;UID=postgres;PWD=1122;TrustServerCertificate=true;";
                        //return @"Server=MASB\SQLEXPRESS;Database=PSVAT5;Trusted_Connection=True;";
                        //return @"Driver={ODBC Driver 17 for SQL Server};Server={Host}\SQLEXPRESS;Database={Database};Trusted_Connection=Yes;";
                                return ConfigurationManager.ConnectionStrings[GetSetting("DbTypeSQLexp")].ConnectionString;
                    

                }
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
        public static Dictionary<string, DatabaseCredentials> _credentials;

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
                //try { DatabaseCredentials.AlternateConn = bool.Parse(GetSetting("AlternateConn")); } catch { DatabaseCredentials.AlternateConn = false; }
                dbcreds.DbType = GetSetting("DbType");
                //dbcreds.Host = GetSetting("Host");
                //dbcreds.Port = int.Parse(GetSetting("Port"));
                //dbcreds.Database = GetSetting("Database");
                //dbcreds.DbType = GetSetting("DbType");
                //dbcreds.Username = GetSetting("Username");
                //dbcreds.Password = GetSetting("Password");
                dbcreds.LoadBalancerUrl = GetSetting("LoadBalancerUrl");


                //_credentials.Add("AlternateConn", dbcreds); 
                _credentials.Add("DbType", dbcreds);
                //_credentials.Add("Host", dbcreds);
                //_credentials.Add("Port", dbcreds);
                //_credentials.Add("Database", dbcreds);                
                //_credentials.Add("Username", dbcreds);
                //_credentials.Add("Password", dbcreds);


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