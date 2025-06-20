using eSpark_WindowsFIA;
using NLog;
using System;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace AgentApp
{
    internal static class Program
    {

        public static NLog.Config.LoggingConfiguration config = new NLog.Config.LoggingConfiguration();
        public static NLog.Logger loger = NLog.LogManager.GetCurrentClassLogger();
        public static String database_type= ConfigurationManager.AppSettings["database_type"];
        public static String database_name = ConfigurationManager.AppSettings["database_name"];
        public static int LogsLevel = 0;
        public static bool isConnected = false;
        public static bool isQueryRunning = false;


        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Targets where to log to: File and Console
            var logfile = new NLog.Targets.FileTarget("logfile")
            {
                FileName = (AssemblyDirectory + "\\logfile.txt").Replace("\\\\", "\\"),
                ArchiveFileName = "logfile.{#}.txt",
                ArchiveNumbering = NLog.Targets.ArchiveNumberingMode.Date,
                ArchiveEvery = NLog.Targets.FileArchivePeriod.Day,
                ArchiveDateFormat = "yyyyMMdd"
            };


            if (LogsLevel == 0) config.AddRule(LogLevel.Info, LogLevel.Info, logfile);
            
            config.Reload();
            NLog.LogManager.Configuration = config;
            loger = NLog.LogManager.GetCurrentClassLogger();

            loger.Log(LogLevel.Info, " Starting dbIndexer for postgre ....... " + DateTime.Now);
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            //ApplicationConfiguration.Initialize();
            Application.Run(new MainForm());
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
    }
}