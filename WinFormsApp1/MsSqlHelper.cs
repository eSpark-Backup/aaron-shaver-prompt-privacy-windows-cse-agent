//using AgentApp.Config;
using Newtonsoft.Json;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Npgsql.Replication.PgOutput.Messages.RelationMessage;
using static System.ComponentModel.Design.ObjectSelectorEditor;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;
using System.Data.Odbc;
using System.Collections.Concurrent;
using System.Net;
using System.Security.Cryptography;
using System.Data.SqlClient;
using System.Drawing;
using System.Configuration;


namespace AgentApp
{
    public class SQLTableInfo
    {
        public string schema_name { get; set; }
        public object table_schema { get; set; }
        public object table_body { get; set; }
        public long table_size { get; set; }
        public long row_count { get; set; }
        public int column_count { get; set; }
        public DateTime creation_time { get; set; }
    }

    public static class SerializationUtils
    {
        public static object MakeJsonSerializable(object value)
        {
            if (value is null) return null;
            if (value is DateTime dt) return dt.ToString("o");
            if (value is DateOnly d) return d.ToString("o");
            if (value is TimeOnly t) return t.ToString("o");
            if (value is decimal dec) return (double)dec;
            if (value is byte[] bytes) return Convert.ToBase64String(bytes);
            if (value is Guid guid) return guid.ToString();

            if (value is IDictionary<string, object> dict)
                return dict.ToDictionary(kv => kv.Key, kv => MakeJsonSerializable(kv.Value));

            if (value is IEnumerable<object> list)
                return list.Select(MakeJsonSerializable).ToList();

            return value;
        }
    }

    /// <summary>
    /// ODBC connection for MS SQL
    /// </summary>
    public class AsyncODBCSQLServerDatabas
    {
        private readonly Dictionary<string, string> _credentials;
        private OdbcConnection _conn;
        public static ConcurrentQueue<string> QueryQueue = new();
        public static ConcurrentStack<string> QueryStack = new();
        //public DatabaseCredentials dbCredentials = DatabaseConfig.GetCredentials("sqlserver");

        //public AsyncSQLServerDatabase(Dictionary<string, string> credentials)
        //{
        //    _credentials = credentials;
        //}

        public async Task CreateConnectionAsync()
        {
            //var dsn = $"Driver={{{_credentials["driver"]}}};" +
            //          $"Server={_credentials["host"]},{_credentials["port"]};" +
            //          $"Database={_credentials["database"]};" +
            //          $"Uid={_credentials["user"]};Pwd={_credentials["password"]};";

            //var dbCredentials = DatabaseConfig.GetCredentials(databaseName);
            //await using var conn_postgresql = new NpgsqlConnection(dbCredentials.ConnectionString);

            await using var conn_postgresql = new OdbcConnection(ConfigurationManager.ConnectionStrings[GetSetting("DbTypeSQLodbc")].ConnectionString);//new OdbcConnection(dbCredentials.ConnectionString);

            _conn = conn_postgresql;
            await _conn.OpenAsync();
        }

        private String GetSetting(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        public async Task CloseAsync()
        {
            if (_conn?.State == ConnectionState.Open)
                await _conn.CloseAsync();
        }

        public async Task<Dictionary<string, object>> GetMetadataAsync()
        {
            try
            {
                if (_conn is null) await CreateConnectionAsync();

                var sql = @"SELECT DB_NAME() AS database_name, SUM(size) * 8 * 1024 AS database_size_in_bytes FROM sys.master_files WHERE database_id = DB_ID() GROUP BY database_id;";
                using var cmd = new OdbcCommand(sql, _conn);
                using var reader = await cmd.ExecuteReaderAsync();


                if (await reader.ReadAsync())
                {
                    return new Dictionary<string, object>
                {
                    { "database_name", reader.GetString(0) },
                    { "database_type", "sqlserver" }, //_credentials.GetValueOrDefault("db_type", "sqlserver") },//dbCredentials.DbType
                    { "db_size", reader.GetInt64(1) }
                };
                }

                await CloseAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching DB metadata: {ex.Message}");
            }
            return null;
        }

        public async Task<List<Dictionary<string, object>>> ListTablesAsync()
        {

            var sql = @" SELECT s.name AS table_schema, t.name AS table_name, t.create_date FROM sys.tables t JOIN sys.schemas s ON t.schema_id = s.schema_id WHERE t.is_ms_shipped = 0;";

            var result = new List<Dictionary<string, object>>();
            try
            {
                using var cmd = new OdbcCommand(sql, _conn);
                using var reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    result.Add(new Dictionary<string, object>
                {
                    { "schema", reader.GetString(0) },
                    { "table_name", reader.GetString(1) },
                    { "creation_time", reader.GetDateTime(2) }
                });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error listing tables: {ex.Message}");
            }
            return result;
        }

        public async Task<List<Dictionary<string, object>>> GetTableSchemaAsync(string tableName, string schemaName)
        {
            var sql = @"SELECT COLUMN_NAME, DATA_TYPE, IS_NULLABLE, CHARACTER_MAXIMUM_LENGTH FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA = '"+ tableName + "' AND TABLE_NAME = '"+ schemaName + "' ORDER BY ORDINAL_POSITION;";

            var result = new List<Dictionary<string, object>>();

            try
            {
                using var cmd = new OdbcCommand(sql, _conn);
                //cmd.Parameters.Add("schema", OdbcType.VarChar).Value = schemaName;
                //cmd.Parameters.Add("table", OdbcType.VarChar).Value = tableName;

                using var reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    result.Add(new Dictionary<string, object>
                {
                    { "column_name", reader.GetString(0) },
                    { "data_type", reader.GetString(1) },
                    { "is_nullable", reader.GetString(2) },
                    { "max_length", reader.IsDBNull(3) ? null : reader.GetValue(3) }
                });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching schema: {ex.Message}");
            }

            return result;
        }

        public async Task<List<Dictionary<string, object>>> FetchSampleDataAsync(string tableName, string schemaName)
        {
            var fullName = $"[{schemaName}].[{tableName}]";
            var countCmd = new OdbcCommand($"SELECT COUNT(*) FROM {fullName}", _conn);
            var totalRows = (int)(await countCmd.ExecuteScalarAsync());

            if (totalRows == 0) return new();

            var rowIndices = new HashSet<int>();
            if (totalRows <= 20)
            {
                for (int i = 1; i <= totalRows; i++) rowIndices.Add(i);
            }
            else
            {
                rowIndices.Add(1);
                rowIndices.Add(totalRows);
                int interval = Math.Max(1, (totalRows - 2) / 18);
                for (int i = 1 + interval; i < totalRows; i += interval)
                    rowIndices.Add(i);
            }

            var rowList = string.Join(",", rowIndices.OrderBy(x => x));
            var query = $@"
            SELECT * FROM (
                SELECT *, ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) AS row_num
                FROM {fullName}
            ) AS numbered
            WHERE row_num IN ({rowList})
            ORDER BY row_num";

            var result = new List<Dictionary<string, object>>();
            try
            {
                using var cmd = new OdbcCommand(query, _conn);
                using var reader = await cmd.ExecuteReaderAsync();
                var columnNames = Enumerable.Range(0, reader.FieldCount)
                                            .Select(reader.GetName).Where(n => n != "row_num").ToList();

                while (await reader.ReadAsync())
                {
                    var row = new Dictionary<string, object>();
                    foreach (var col in columnNames)
                    {
                        row[col] = SerializationUtils.MakeJsonSerializable(reader[col]);
                    }
                    result.Add(row);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching sample data: {ex.Message}");
            }

            return result;
        }

        public async Task<Dictionary<string, object>> GetTableInfoAsync(string tableName, string schemaName)
        {
            var info = new Dictionary<string, object>();
            var fullName = $"[{schemaName}].[{tableName}]";

            try
            {
                var statsCmd = new OdbcCommand(@" SELECT p.rows AS row_count,SUM(a.total_pages) * 8 * 1024 AS size_in_bytes
                FROM sys.tables t INNER JOIN sys.indexes i ON t.OBJECT_ID = i.object_id
                INNER JOIN sys.partitions p ON i.object_id = p.OBJECT_ID AND i.index_id = p.index_id
                INNER JOIN sys.allocation_units a ON p.partition_id = a.container_id
                LEFT OUTER JOIN sys.schemas s ON t.schema_id = s.schema_id WHERE t.is_ms_shipped = 0 AND t.NAME = '"+ schemaName + "' GROUP BY t.Name, s.Name, p.Rows ", _conn);
                //WHERE object_id = OBJECT_ID(?) AND index_id IN (0,1)", _conn);
                //statsCmd.Parameters.Add("obj", OdbcType.VarChar).Value = fullName;

                using var statsReader = await statsCmd.ExecuteReaderAsync();
                await statsReader.ReadAsync();
                info["row_count"] = statsReader.IsDBNull(0) ? 0 : statsReader.GetInt64(0);
                info["size_in_bytes"] = statsReader.IsDBNull(1) ? 0 : statsReader.GetInt64(1);

                var colCmd = new OdbcCommand(@"
                SELECT COUNT(*) FROM INFORMATION_SCHEMA.COLUMNS
                WHERE TABLE_SCHEMA = '"+ schemaName + "' AND TABLE_NAME = '"+ tableName + "' ", _conn);
                //colCmd.Parameters.Add("schema", OdbcType.VarChar).Value = schemaName;
                //colCmd.Parameters.Add("table", OdbcType.VarChar).Value = tableName;

                info["column_count"] = (int)(await colCmd.ExecuteScalarAsync());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting table info: {ex.Message}");
                info["row_count"] = null;
                info["size_in_bytes"] = null;
                info["column_count"] = null;
            }

            return info;
        }

        public async Task<Dictionary<string, object>> GetTableDetailsAsync()
        {
            if (_conn is null || _conn.State != ConnectionState.Open)
            {

                await using var conn_postgresql = new OdbcConnection(ConfigurationManager.ConnectionStrings[GetSetting("DbTypeSQLodbc")].ConnectionString);
                _conn = conn_postgresql;
                await _conn.OpenAsync();


                //await CreateConnectionAsync(); 

                var tables = await ListTablesAsync();
                var response = new Dictionary<string, object>();
                foreach (var tbl in tables)
                {
                    var tableName = tbl["table_name"].ToString();
                    var schemaName = tbl["schema"].ToString();

                    var schema = await GetTableSchemaAsync(tableName, schemaName);
                    var body = await FetchSampleDataAsync(tableName, schemaName);
                    var info = await GetTableInfoAsync(tableName, schemaName);

                    var raw = new SQLTableInfo
                    {
                        schema_name = schemaName,
                        table_schema = schema,
                        table_body = body,
                        table_size = Convert.ToInt64(info["size_in_bytes"]),
                        row_count = Convert.ToInt64(info["row_count"]),
                        column_count = Convert.ToInt32(info["column_count"]),
                        creation_time = Convert.ToDateTime(tbl["creation_time"])
                    };

                    response[tableName] = SerializationUtils.MakeJsonSerializable(raw);
                }
                await CloseAsync();

                return response;
            }
            return null;
        }

        public async Task<List<Dictionary<string, object>>> ExecuteQueryAsync(string query)
        {
            try
            {
                var rows = new List<Dictionary<string, object>>();

                if (_conn is null || _conn.State != ConnectionState.Open)
                {
                    await using var conn_postgresql = new OdbcConnection(ConfigurationManager.ConnectionStrings[GetSetting("DbTypeSQLodbc")].ConnectionString);
                    _conn = conn_postgresql;
                    await _conn.OpenAsync();

                    using var cmd = new OdbcCommand(query, _conn);
                    using var reader = await cmd.ExecuteReaderAsync();

                    var columns = Enumerable.Range(0, reader.FieldCount).Select(reader.GetName).ToArray();
                    

                    while (await reader.ReadAsync())
                    {
                        var row = new Dictionary<string, object>();
                        foreach (var col in columns)
                        {
                            row[col] = SerializationUtils.MakeJsonSerializable(reader[col]);
                        }
                        rows.Add(row);
                    }
                    await CloseAsync();
                }
                return rows;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error executing query: {ex.Message}");
                return null;
            }
        }
    }


    /// <summary>
    /// SqlConnection for MS SQL
    /// </summary>
    public class AsyncSqlConnectionSQLServerDatabase
    {
        private readonly Dictionary<string, string> _credentials;
        private SqlConnection _conn;
        public static ConcurrentQueue<string> QueryQueue = new();
        public static ConcurrentStack<string> QueryStack = new();
        //public DatabaseCredentials dbCredentials = DatabaseConfig.GetCredentials("sqlserver");

        //public AsyncSQLServerDatabase(Dictionary<string, string> credentials)
        //{
        //    _credentials = credentials;
        //}

        public async Task CreateConnectionAsync()
        {
            //var dsn = $"Driver={{{_credentials["driver"]}}};" +
            //          $"Server={_credentials["host"]},{_credentials["port"]};" +
            //          $"Database={_credentials["database"]};" +
            //          $"Uid={_credentials["user"]};Pwd={_credentials["password"]};";

            //var dbCredentials = DatabaseConfig.GetCredentials(databaseName);
            //await using var conn_postgresql = new NpgsqlConnection(dbCredentials.ConnectionString);

            //await using var conn_postgresql = new SqlConnection(dbCredentials.ConnectionString);
            await using var conn_postgresql = new SqlConnection(ConfigurationManager.ConnectionStrings[GetSetting("DbTypeSQLexp")].ConnectionString);

            _conn = conn_postgresql;
            await _conn.OpenAsync();
        }
        private String GetSetting(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        public async Task CloseAsync()
        {
            if (_conn?.State == ConnectionState.Open)
                await _conn.CloseAsync();
         }

        public async Task<Dictionary<string, object>> GetMetadataAsync()
        {
            try
            {
                if (_conn is null) await CreateConnectionAsync();

                if (_conn.State != ConnectionState.Open)
                {
                    //var dbCredentials = DatabaseConfig.GetCredentials(databaseName);
                    //await using var conn_postgresql = new NpgsqlConnection(dbCredentials.ConnectionString);

                    await using var conn_postgresql = new SqlConnection(ConfigurationManager.ConnectionStrings[GetSetting("DbTypeSQLexp")].ConnectionString);//SqlConnection(dbCredentials.ConnectionString);

                    _conn = conn_postgresql;
                    //await _conn.OpenAsync();
                    _conn.Open();

                    var sql = @"SELECT DB_NAME() AS database_name, SUM(size) * 8 * 1024 AS database_size_in_bytes, 
                                (SELECT COUNT(*) FROM sys.tables WHERE is_ms_shipped = 0 ) AS table_count,
                                (SELECT COUNT(*) FROM sys.columns c JOIN sys.tables t ON c.object_id = t.object_id WHERE t.is_ms_shipped = 0 ) AS column_count FROM sys.master_files WHERE database_id = DB_ID() GROUP BY database_id;";
                    using var cmd = new SqlCommand(sql, _conn);
                    using var reader = await cmd.ExecuteReaderAsync();

                    if (await reader.ReadAsync())
                    {
                        return new Dictionary<string, object>
                {
                    { "database_name", reader.GetString(0) },
                    { "database_type", "sqlserver" },//dbCredentials.DbType }, //_credentials.GetValueOrDefault("db_type", "sqlserver") },
                    { "db_size", reader.GetInt32(1) },
                            { "table_count", reader.GetInt32(2) },
                            { "column_count", reader.GetInt32(3) }
                };
                    }
                }
                await CloseAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching DB metadata: {ex.Message}");
            }
            return null;
        }

        public async Task<List<Dictionary<string, object>>> ListTablesAsync()
        {

            var sql = @" SELECT s.name AS table_schema, t.name AS table_name, t.create_date FROM sys.tables t JOIN sys.schemas s ON t.schema_id = s.schema_id WHERE t.is_ms_shipped = 0;";

            var result = new List<Dictionary<string, object>>();
            try
            {
                using var cmd = new SqlCommand(sql, _conn);
                using var reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    result.Add(new Dictionary<string, object>
                {
                    { "schema", reader.GetString(0) },
                    { "table_name", reader.GetString(1) },
                    { "creation_time", reader.GetDateTime(2) }
                });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error listing tables: {ex.Message}");
            }
            return result;
        }

        public async Task<List<Dictionary<string, object>>> GetTableSchemaAsync(string tableName, string schemaName)
        {
            //var sql = @"SELECT COLUMN_NAME, DATA_TYPE, IS_NULLABLE, CHARACTER_MAXIMUM_LENGTH FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA = '?' AND TABLE_NAME = '?' ORDER BY ORDINAL_POSITION;";
            var sql = @"SELECT COLUMN_NAME, DATA_TYPE, IS_NULLABLE, CHARACTER_MAXIMUM_LENGTH FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA = '"+ schemaName + "' AND TABLE_NAME = '"+ tableName + "' ORDER BY ORDINAL_POSITION;";

            var result = new List<Dictionary<string, object>>();

            try
            {
                using var cmd = new SqlCommand(sql, _conn);
                //cmd.Parameters.Add("schema", SqlDbType.VarChar).Value = schemaName;
                //cmd.Parameters.Add("table", SqlDbType.VarChar).Value = tableName;

                using var reader = await cmd.ExecuteReaderAsync();
                String colName = "";
                int ii = 0;
                while (await reader.ReadAsync())
                {
                    ii++;

                    try { colName= reader.GetString(0).ToLower(); } catch (Exception ee) { colName="COLUMN"+ ii; }
                    result.Add(new Dictionary<string, object>
                {
                    { "column_name", colName },
                    { "data_type", reader.GetString(1) },
                    { "is_nullable", reader.GetString(2) },
                    { "max_length", reader.IsDBNull(3) ? null : reader.GetValue(3) }
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching schema: {ex.Message}");
            }

            return result;
        }

        public async Task<List<Dictionary<string, object>>> FetchSampleDataAsync(string tableName, string schemaName)
        {
            var fullName = $"[{schemaName}].[{tableName}]";
            var countCmd = new SqlCommand($"SELECT COUNT(*) FROM {fullName}", _conn);
            var totalRows = (int)(await countCmd.ExecuteScalarAsync());

            if (totalRows == 0) return new();

            var rowIndices = new HashSet<int>();
            if (totalRows <= 20)
            {
                for (int i = 1; i <= totalRows; i++) rowIndices.Add(i);
            }
            else
            {
                rowIndices.Add(1);
                rowIndices.Add(totalRows);
                int interval = Math.Max(1, (totalRows - 2) / 18);
                for (int i = 1 + interval; i < totalRows; i += interval)
                    rowIndices.Add(i);
            }

            var rowList = string.Join(",", rowIndices.OrderBy(x => x));
            var query = $@"
            SELECT * FROM (
                SELECT *, ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) AS row_num
                FROM {fullName}
            ) AS numbered
            WHERE row_num IN ({rowList})
            ORDER BY row_num";

            var result = new List<Dictionary<string, object>>();
            try
            {
                using var cmd = new SqlCommand(query, _conn);
                using var reader = await cmd.ExecuteReaderAsync();
                var columnNames = Enumerable.Range(0, reader.FieldCount)
                                            .Select(reader.GetName).Where(n => n != "row_num").ToList();

                while (await reader.ReadAsync())
                {
                    var row = new Dictionary<string, object>();
                    foreach (var col in columnNames)
                    {
                        row[col] = SerializationUtils.MakeJsonSerializable(reader[col]);
                    }
                    result.Add(row);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching sample data: {ex.Message}");
            }

            return result;
        }

        public async Task<Dictionary<string, object>> GetTableInfoAsync(string tableName, string schemaName)
        {
            var info = new Dictionary<string, object>();
            var fullName = $"[{schemaName}].[{tableName}]";

            try
            {
                var statsCmd = new SqlCommand(@"
                 SELECT p.rows AS row_count,SUM(a.total_pages) * 8 * 1024 AS size_in_bytes
                FROM sys.tables t INNER JOIN sys.indexes i ON t.OBJECT_ID = i.object_id
                INNER JOIN sys.partitions p ON i.object_id = p.OBJECT_ID AND i.index_id = p.index_id
                INNER JOIN sys.allocation_units a ON p.partition_id = a.container_id
                LEFT OUTER JOIN sys.schemas s ON t.schema_id = s.schema_id WHERE t.is_ms_shipped = 0 AND t.NAME = '" + tableName + "' GROUP BY t.Name, s.Name, p.Rows ", _conn);
                //WHERE object_id = ? AND index_id IN (0,1)", _conn);
                //statsCmd.Parameters.Add("obj", SqlDbType.VarChar).Value = fullName;

                using var statsReader = await statsCmd.ExecuteReaderAsync();
                await statsReader.ReadAsync();
                info["row_count"] = statsReader.IsDBNull(0) ? 0 : statsReader.GetInt64(0);
                info["size_in_bytes"] = statsReader.IsDBNull(1) ? 0 : statsReader.GetInt64(1);

                await statsReader.CloseAsync();

                var colCmd = new SqlCommand(@"
                SELECT COUNT(*) FROM INFORMATION_SCHEMA.COLUMNS
                WHERE TABLE_SCHEMA = '"+ schemaName + "' AND TABLE_NAME = '"+ tableName + "' ", _conn);
                //colCmd.Parameters.Add("schema", SqlDbType.VarChar).Value = schemaName;
                //colCmd.Parameters.Add("table", SqlDbType.VarChar).Value = tableName;

                info["column_count"] = (int)(await colCmd.ExecuteScalarAsync());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting table info: {ex.Message}");
                info["row_count"] = null;
                info["size_in_bytes"] = null;
                info["column_count"] = null;
            }

            return info;
        }

        public async Task<Dictionary<string, SQLTableInfo>> GetTableDetailsAsync()
        {
            if (_conn is null) await CreateConnectionAsync();
            var response = new Dictionary<string, object>();
            Dictionary<string, SQLTableInfo> tableInfoDict = new Dictionary<string, SQLTableInfo>();

            if (_conn.State != ConnectionState.Open)
            {
                //var dbCredentials = DatabaseConfig.GetCredentials(databaseName);
                //await using var conn_postgresql = new NpgsqlConnection(dbCredentials.ConnectionString);

                await using var conn_postgresql = new SqlConnection(ConfigurationManager.ConnectionStrings[GetSetting("DbTypeSQLexp")].ConnectionString);//SqlConnection(dbCredentials.ConnectionString);

                _conn = conn_postgresql;
                //await _conn.OpenAsync();
                _conn.Open();


                var tables = await ListTablesAsync();

                foreach (var tbl in tables)
                {
                    var tableName = tbl["table_name"].ToString().ToLower();
                    var schemaName = tbl["schema"].ToString();

                    var schema = await GetTableSchemaAsync(tableName, schemaName);
                    var body = await FetchSampleDataAsync(tableName, schemaName);
                    var info = await GetTableInfoAsync(tableName, schemaName);

                    //var raw = new SQLTableInfo
                    tableInfoDict.Add(tableName, new SQLTableInfo
                    {
                        schema_name = schemaName,
                        table_schema = schema,
                        table_body = body,
                        table_size = Convert.ToInt64(info["size_in_bytes"]),
                        row_count = Convert.ToInt64(info["row_count"]),
                        column_count = Convert.ToInt32(info["column_count"]),
                        creation_time = Convert.ToDateTime(tbl["creation_time"])
                    });

                    //response[tableName] = SerializationUtils.MakeJsonSerializable(raw);
                }
                await CloseAsync();
            }
            return tableInfoDict;
        }

        public async Task<List<Dictionary<string, object>>> ExecuteQueryAsync(string query)
        {
            try
            {
                if (_conn is null) await CreateConnectionAsync();
                var rows = new List<Dictionary<string, object>>();

                if (_conn.State != ConnectionState.Open)
                {
                    //var dbCredentials = DatabaseConfig.GetCredentials(databaseName);
                    //await using var conn_postgresql = new NpgsqlConnection(dbCredentials.ConnectionString);

                    await using var conn_postgresql = new SqlConnection(ConfigurationManager.ConnectionStrings[GetSetting("DbTypeSQLexp")].ConnectionString);//new SqlConnection(dbCredentials.ConnectionString);

                    _conn = conn_postgresql;
                    //await _conn.OpenAsync();
                    _conn.Open();

                    using var cmd = new SqlCommand(query, _conn);
                    using var reader = await cmd.ExecuteReaderAsync();

                    var columns = Enumerable.Range(0, reader.FieldCount).Select(reader.GetName).ToArray();

                    while (await reader.ReadAsync())
                    {
                        var row = new Dictionary<string, object>();
                        foreach (var col in columns)
                        {
                            row[col] = SerializationUtils.MakeJsonSerializable(reader[col]);
                        }
                        rows.Add(row);
                    }
                    await CloseAsync();
                }
                return rows;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error executing query: {ex.Message}");
                //return null;
            }
            return null;
        }
    }




}
