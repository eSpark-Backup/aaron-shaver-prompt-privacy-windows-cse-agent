using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;
using System.IO;
using System.Text.Json;
using Newtonsoft.Json;

namespace AgentApp
{
    public static class SqliteHelper
    {
        public static SQLiteConnection connectString;
        public static SQLiteCommand cmd;
        public static SQLiteDataAdapter adapter;
        public static DataSet ds = new DataSet();
        public static DataTable dt = new DataTable();

        public static async Task<object> GetMetadataAsync(String conn1)
        {
            // Example: return list of schemas
            //using var cmd = new NpgsqlCommand("SELECT schema_name FROM information_schema.schemata", conn);
            //using var reader = await cmd.ExecuteReaderAsync();
            //var result = new List<string>();
            //while (await reader.ReadAsync())
            //{
            //    result.Add(reader.GetString(0));
            //}
            //return result;

            InitializeFlexMoldDB();
            //using var cmd = new NpgsqlCommand("SELECT name, sql FROM sqlite_master WHERE type='table' ORDER BY name;", conn);
            //using var reader = await cmd.ExecuteReaderAsync();
            //var result = new List<string>();
            //while (await reader.ReadAsync())
            //{
            //    result.Add(reader.GetString(0));
            //}
            //return result;


            try
            {
                //if (connectString == null)
                //    InitializeFlexMoldDB();

                //if (connectString.State != ConnectionState.Open)
                //    connectString.Open();

                String connStr = "Data Source=" + Directory.GetCurrentDirectory() + "\\FlexMold.db;version=3";
                connectString = new SQLiteConnection(connStr);

                if (connectString.State != ConnectionState.Open)
                    connectString.Open();


                String sql = "SELECT name, sql FROM sqlite_master WHERE type='table' ORDER BY name;";
                
                adapter = new SQLiteDataAdapter(sql, connectString);
                ds.Reset();
                adapter.Fill(ds);
                //var result = new List<string>();
                //SQLiteConnection conn = new SQLiteConnection();
                //using var cmd = new SQLiteCommand(sql, conn);
                //using var reader = await cmd.ExecuteReaderAsync();


                ////dt = ds.Tables[0];
                ////dataGridView1.DataSource = dt;
                //connectString.Close();

                return JsonConvert.SerializeObject(ds.Tables[0]);                
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);

                return "Error in SQLITE query!!!";
            }

            
        }


        public static async Task<object> GetTablesDetailsAsync(String conn1)//(NpgsqlConnection conn)
        {
            // Return list of tables
            InitializeFlexMoldDB();

            //var cmd = new NpgsqlCommand("SELECT table_schema, table_name FROM information_schema.tables WHERE table_type='BASE TABLE'", conn);
            String connStr = "Data Source=" + Directory.GetCurrentDirectory() + "\\FlexMold.db;version=3";
            connectString = new SQLiteConnection(connStr);

            if (connectString.State != ConnectionState.Open)
                connectString.Open();


            String sql = "SELECT table_schema, table_name FROM information_schema.tables WHERE table_type='BASE TABLE'";


            using var reader = await cmd.ExecuteReaderAsync();
            var result = new List<Dictionary<string, string>>();
            while (await reader.ReadAsync())
            {
                result.Add(new Dictionary<string, string>
            {
                { "schema", reader.GetString(0) },
                { "table", reader.GetString(1) }
            });
            }
            return result;
        }

        public static async Task<object> ExecuteQueryAsync(NpgsqlConnection conn, string query)
        {
            using var cmd = new NpgsqlCommand(query, conn);
            using var reader = await cmd.ExecuteReaderAsync();
            var result = new List<Dictionary<string, object>>();
            while (await reader.ReadAsync())
            {
                var row = new Dictionary<string, object>();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    row[reader.GetName(i)] = reader.IsDBNull(i) ? null : reader.GetValue(i);
                }
                result.Add(row);
            }
            return result;
        }


        public static bool checkAdminPriv(string passkey)
        {
            //File.ReadAllLines(CSVHelper.GetCurrentDirectoryPath() + "\\FlexMoldUser.connect", Encoding.UTF8);

            //CREATE TABLE "FM_Settings"(
            //    "FM_SaltKey"    TEXT,
            //    "FM_UserName"   TEXT,
            //    "FM_Dept"   TEXT
            //)
            try
            {
                if (connectString.State != ConnectionState.Open)
                    connectString.Open();
                cmd = new SQLiteCommand();
                String sql = "";
                sql = "SELECT FM_SaltKey FROM FM_Settings";
                adapter = new SQLiteDataAdapter(sql, connectString);
                ds.Reset();
                adapter.Fill(ds);
                connectString.Close();

                string saltKey = "";
                if (ds != null)
                {
                    //foreach (DataTable dt in ds.Tables)
                    //{
                    //    foreach (DataRow dr in dt.Rows)
                    //    {
                    //        saltKey = dr["FM_SaltKey"].ToString();
                    //        if (saltKey != null && saltKey.Length > 1 && saltKey == passkey)
                    //        {
                    //            IsEditingEnabled = true;
                    //            return true;
                    //        }
                    //        else { IsEditingEnabled = false; }
                    //    }
                    //}
                }
                return false;
            }
            catch (Exception ee) { }

            //IsEditingEnabled = false;
            return false;
        }


        public static void InitializeFlexMoldDB()
        {
            //IsEditingEnabled = false;
            //var log = LogManager.GetLogger("Device Status");
            try
            {
                String connStr = "Data Source=" + Directory.GetCurrentDirectory() + "\\FlexMold.db;version=3";
                connectString = new SQLiteConnection(connStr);

                //checkAdminPriv("FM_Login");

                //log.Log(LogLevel.Info, "Database connected successfully!");
            }
            catch
            { //log.Log(LogLevel.Error, "Database connection error"); 
            }
        }

        public static DataSet ReadData(string TableName, string whereclause)
        {
            try
            {
                if (connectString == null)
                    InitializeFlexMoldDB();

                if (connectString.State != ConnectionState.Open)
                    connectString.Open();
                cmd = new SQLiteCommand();
                String sql = "";
                if (whereclause != null && whereclause.Length > 1)
                    sql = "SELECT * FROM " + TableName + " where " + whereclause;
                else
                    sql = "SELECT * FROM " + TableName;

                adapter = new SQLiteDataAdapter(sql, connectString);
                ds.Reset();
                adapter.Fill(ds);
                //dt = ds.Tables[0];
                //dataGridView1.DataSource = dt;
                connectString.Close();

                return ds;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
            return null;
        }

        public static bool UpdateData(string TableName, string whereclause, Dictionary<string, string> parms)
        {
            try
            {
                String parasStr = "";


                foreach (KeyValuePair<string, string> entry in parms)
                {
                    parasStr += entry.Key + "='" + entry.Value + "' ";
                    parasStr += " , ";
                }
                parasStr = parasStr.Remove(parasStr.Length - 2);

                if (connectString.State != ConnectionState.Open)
                    connectString.Open();

                cmd = new SQLiteCommand();
                String sql = "";
                if (whereclause != null && whereclause.Length > 1)
                    sql = "UPDATE " + TableName + " SET " + parasStr + " where " + whereclause;
                else
                    sql = "UPDATE " + TableName + " SET " + parasStr;

                adapter = new SQLiteDataAdapter(sql, connectString);
                ds.Reset();
                adapter.Fill(ds);
                //dt = ds.Tables[0];
                //dataGridView1.DataSource = dt;
                connectString.Close();

                return true;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
            return false;
        }

        public static bool InsertOrUpdateData(string TableName, string whereclause, Dictionary<string, string> parms)
        {
            try
            {
                if (connectString == null)
                    InitializeFlexMoldDB();

                if (connectString.State != ConnectionState.Open)
                    connectString.Open();
                cmd = new SQLiteCommand();
                String sqlQuery = "";
                if (whereclause != null && whereclause.Length > 1)
                    sqlQuery = "SELECT * FROM " + TableName + " where " + whereclause;
                else
                    sqlQuery = "SELECT * FROM " + TableName;

                adapter = new SQLiteDataAdapter(sqlQuery, connectString);
                ds.Reset();
                adapter.Fill(ds);
                //dt = ds.Tables[0];
                //dataGridView1.DataSource = dt;
                connectString.Close();

                //if record exist then update it else insert it
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    String parasStr = "";

                    foreach (KeyValuePair<string, string> entry in parms)
                    {
                        parasStr += entry.Key + "='" + entry.Value + "' ";
                        parasStr += " , ";
                    }
                    parasStr = parasStr.Remove(parasStr.Length - 2);

                    if (connectString.State != ConnectionState.Open)
                        connectString.Open();

                    cmd = new SQLiteCommand();
                    String sql = "";
                    if (whereclause != null && whereclause.Length > 1)
                        sql = "UPDATE " + TableName + " SET " + parasStr + " where " + whereclause;
                    else
                        sql = "UPDATE " + TableName + " SET " + parasStr;

                    adapter = new SQLiteDataAdapter(sql, connectString);
                    ds.Reset();
                    adapter.Fill(ds);
                    //dt = ds.Tables[0];
                    //dataGridView1.DataSource = dt;
                    connectString.Close();

                    return true;
                }
                else
                { //insert data
                    String parasStrKeys = "";
                    String parasStrVals = "";

                    foreach (KeyValuePair<string, string> entry in parms)
                    {
                        parasStrKeys += "" + entry.Key + "";
                        parasStrKeys += " , ";

                        parasStrVals += "'" + entry.Value + "'";
                        parasStrVals += " , ";
                    }

                    parasStrKeys = parasStrKeys.Remove(parasStrKeys.Length - 2);
                    parasStrVals = parasStrVals.Remove(parasStrVals.Length - 2);

                    if (connectString.State != ConnectionState.Open)
                        connectString.Open();

                    cmd = new SQLiteCommand();
                    String sql = "";
                    //if (whereclause != null && whereclause.Length > 1)
                    sql = "INSERT INTO " + TableName + " (" + parasStrKeys + ") values( " + parasStrVals + ") ";
                    //else
                    //    sql = "UPDATE " + TableName + " SET " + parasStr;

                    adapter = new SQLiteDataAdapter(sql, connectString);
                    ds.Reset();
                    adapter.Fill(ds);
                    //dt = ds.Tables[0];
                    //dataGridView1.DataSource = dt;
                    connectString.Close();

                    return true;
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
            return false;
        }
    }

}
