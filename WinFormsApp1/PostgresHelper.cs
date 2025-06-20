using AgentApp.Config;
using Newtonsoft.Json;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AgentApp
{

    public class TableInfo
    {
        public string TableName { get; set; }
        public string schema_name { get; set; }
        public List<Dictionary<string, object>> table_schema { get; set; }
        public List<Dictionary<string, object>> table_body { get; set; }
        public long table_size { get; set; }
        public long row_count { get; set; }
        public int column_count { get; set; }
    }

    public static class PostgresHelper
    {
        //private static readonly string _connectionString;

        //public PostgresHelper2(string connectionString)
        //{
        //    _connectionString = connectionString;
        //}

        public static async Task<object> GetMetadataAsync(string databaseName)
        {
            // Example: return list of schemas
            //                SELECT pg_database_size(datname) AS database_size_in_bytes, current_database() AS database_name FROM pg_database WHERE datname = $1; ,self.credentials["database"],

            var dbCredentials = DatabaseConfig.GetCredentials(databaseName);
            await using var conn_postgresql = new NpgsqlConnection(dbCredentials.ConnectionString);
            await conn_postgresql.OpenAsync();

            using var cmd = new NpgsqlCommand("SELECT pg_database_size(datname) AS db_size, current_database() AS database_name,'postgresql' as database_type  FROM pg_database WHERE datname = current_database();", conn_postgresql);
            using var reader = await cmd.ExecuteReaderAsync();
            var result = new Dictionary<string, object>();// List<string>();
            //String result = "";
            //var dataTable = new DataTable();

            while (await reader.ReadAsync())
            //if (await reader.ReadAsync())
            //if (reader.HasRows)
            {
                //result.Add("database_size_in_bytes:"+reader.GetValue(0).ToString()+"L");//ALERT>>Here "L" added due to c# not understand bigint
                result.Add("db_size:", reader.GetValue(0));
                result.Add("database_name:", reader.GetValue(1));
                result.Add("database_type:", reader.GetValue(2));
                //dataTable.Load(reader);

                //result = @"""db_size"":" + reader.GetValue(0).ToString() + 
                //         @" , ""database_name"":""" + reader.GetValue(1).ToString() + 
                //         @""" , ""database_type"":" + reader.GetValue(2).ToString() +@"""";


            }

            await conn_postgresql.CloseAsync();
            //JsonConvert.SerializeObject(dataTable, Formatting.Indented)
            //return System.Text.Json.JsonSerializer.Serialize(dataTable);
            return JsonConvert.SerializeObject(result);
            //return result;

        }

        //public static async Task<List<TableInfo>> GetTablesFullInfoAsync()
        public static async Task<object> GetTablesFullInfoAsync(string databaseName)
        {
            var dbCredentials = DatabaseConfig.GetCredentials(databaseName);
            await using var conn_postgresql = new NpgsqlConnection(dbCredentials.ConnectionString);
            await conn_postgresql.OpenAsync();

            var table_name = new List<TableInfo>();

            var tables = await ListPostgresTablesAsync(conn_postgresql);
            var result = new Dictionary<string, object>();//result

            //List<String> resultJson = new List<String>();

            foreach (var table in tables)
            {
                var tableName = table["table_name"].ToString();
                var schemaName = table["table_schema"].ToString();

                var schema = await GetPostgresTableSchemaAsync(tableName, schemaName, conn_postgresql);
                var body = await FetchPostgresSampleDataAsync(tableName, schemaName, conn_postgresql);
                var info = await GetPostgresTableInfoAsync(tableName, schemaName, conn_postgresql);
                table_name.Add(new TableInfo
                {
                    TableName = tableName,
                    schema_name = schemaName,
                    table_schema = schema,
                    table_body = body,
                    table_size = info.Item1,
                    row_count = info.Item2,
                    column_count = schema.Count
                });
                //    result = new Dictionary<string, object>();
                //    result.Add("TableName", tableName);
                //    result.Add("schema_name", schemaName);
                //    result.Add("table_schema", schema);
                //    result.Add("table_body", body);
                //    result.Add("table_size", info.Item1);
                //    result.Add("row_count", info.Item2);
                //    result.Add("column_count", schema.Count);

                //resultJson.Add(JsonConvert.SerializeObject(result));
            }

            await conn_postgresql.CloseAsync();

            //return JsonConvert.SerializeObject(result);
            return JsonConvert.SerializeObject(table_name);
            //return JsonConvert.SerializeObject(resultJson);
        }

        private static async Task<List<Dictionary<string, object>>> ListPostgresTablesAsync(NpgsqlConnection conn_postgresql)
        {

            var tables = new List<Dictionary<string, object>>();

            var query = @"
            SELECT table_name, table_schema
            FROM information_schema.tables
            WHERE table_type = 'BASE TABLE' AND table_schema NOT IN ('pg_catalog');";

            await using var cmd = new NpgsqlCommand(query, conn_postgresql);
            await using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                tables.Add(new Dictionary<string, object>
            {
                { "table_name", reader["table_name"] },
                { "table_schema", reader["table_schema"] }
            });
            }

            return tables;
        }

        private static async Task<List<Dictionary<string, object>>> GetPostgresTableSchemaAsync(string tableName, string schema, NpgsqlConnection conn_postgresql)
        {
            var schemaInfo = new List<Dictionary<string, object>>();

            var query = @"
            SELECT column_name, data_type, is_nullable
            FROM information_schema.columns
            WHERE table_name = @tableName AND table_schema = @schemaName
            ORDER BY ordinal_position;
        ";

            await using var cmd = new NpgsqlCommand(query, conn_postgresql);
            cmd.Parameters.AddWithValue("tableName", tableName);
            cmd.Parameters.AddWithValue("schemaName", schema);

            await using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                schemaInfo.Add(new Dictionary<string, object>
            {
                { "column_name", reader["column_name"] },
                { "data_type", reader["data_type"] },
                { "is_nullable", reader["is_nullable"] }
            });
            }

            return schemaInfo;
        }

        private static async Task<List<Dictionary<string, object>>> FetchPostgresSampleDataAsync(string tableName, string schema, NpgsqlConnection conn_postgresql)
        {
            var data = new List<Dictionary<string, object>>();

            var query = $"SELECT * FROM \"{schema}\".\"{tableName}\" LIMIT 5;";

            await using var cmd = new NpgsqlCommand(query, conn_postgresql);
            await using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                var row = new Dictionary<string, object>();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    row[reader.GetName(i)] = reader.GetValue(i);
                }
                data.Add(row);
            }

            return data;
        }

        private static async Task<(long, long)> GetPostgresTableInfoAsync(string tableName, string schema, NpgsqlConnection conn_postgresql)
        {
            // Get table size
            var sizeCmd = new NpgsqlCommand("SELECT pg_total_relation_size(@fullTableName);", conn_postgresql);
            sizeCmd.Parameters.AddWithValue("fullTableName", $"{schema}.{tableName}");
            var sizeResult = await sizeCmd.ExecuteScalarAsync();
            long sizeInBytes = sizeResult != DBNull.Value ? Convert.ToInt64(sizeResult) : 0;

            // Get row count
            var countCmd = new NpgsqlCommand($"SELECT COUNT(*) FROM \"{schema}\".\"{tableName}\";", conn_postgresql);
            var countResult = await countCmd.ExecuteScalarAsync();
            long rowCount = countResult != DBNull.Value ? Convert.ToInt64(countResult) : 0;

            return (sizeInBytes, rowCount);
        }

        public static async Task<object> GetTablesDetailsAsync(NpgsqlConnection conn)
        {
            // Return list of tables
            var cmd = new NpgsqlCommand("SELECT table_schema, table_name FROM information_schema.tables WHERE table_type='BASE TABLE'", conn);
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
            return JsonConvert.SerializeObject(result);
        }

        public static async Task<object> ExecuteQueryAsync(string databaseName, string query)//NpgsqlConnection conn, string query)
        {
            var dbCredentials = DatabaseConfig.GetCredentials(databaseName);
            await using var conn_postgresql = new NpgsqlConnection(dbCredentials.ConnectionString);
            await conn_postgresql.OpenAsync();

            using var cmd = new NpgsqlCommand(query, conn_postgresql);
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
            await conn_postgresql.CloseAsync();

            return JsonConvert.SerializeObject(result);
        }
    }
}
