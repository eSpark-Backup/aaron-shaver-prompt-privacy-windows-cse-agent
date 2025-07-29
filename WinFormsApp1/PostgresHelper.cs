//using AgentApp.Config;
using Newtonsoft.Json;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
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

namespace AgentApp
{

    public class TableInfo
    {
        public int column_count { get; set; }
        public long row_count { get; set; }
        public string schema_name { get; set; }
        public Dictionary<string, List<object>> table_body { get; set; }
        public List<Dictionary<string, object>> table_schema { get; set; }
        public long table_size { get; set; }
    }

    //public class TableInfoLister
    //{
    //    public string table_name { get; set; }
    //    public TableInfo tableInfo { get; set; }
    //    //public String schema_name { get; set; }
    //    //public Dictionary<string ,List<object>> table_body { get; set; }
    //    //public List<Dictionary<string, object>> schema { get; set; }
    //    //public (long, long) info { get; set; }        
    //}

    public static class PostgresHelper
    {
        //private static readonly string _connectionString;

        //public PostgresHelper2(string connectionString)
        //{
        //    _connectionString = connectionString;
        //}

        private static String GetSetting(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        public static async Task<object> GetMetadataAsync()
        {
            // Example: return list of schemas
            //                SELECT pg_database_size(datname) AS database_size_in_bytes, current_database() AS database_name FROM pg_database WHERE datname = $1; ,self.credentials["database"],

            //var dbCredentials = DatabaseConfig.GetCredentials(databaseName);//ConfigurationManager.ConnectionStrings[GetSetting("DbTypeSQLexp")]
            //ConfigurationManager.ConnectionStrings["DBConnectionPostGreSQL"].ConnectionString
            await using var conn_postgresql = new NpgsqlConnection(ConfigurationManager.ConnectionStrings[GetSetting("DbTypePostGre")].ConnectionString);
            await conn_postgresql.OpenAsync();

            //using var cmd = new NpgsqlCommand("SELECT pg_database_size(datname) AS db_size, current_database() AS database_name,'postgresql' as database_type  FROM pg_database WHERE datname = current_database();", conn_postgresql);
            using var cmd = new NpgsqlCommand("SELECT pg_database_size(datname) AS db_size, current_database() AS database_name," +
                                            " (SELECT COUNT(*) FROM information_schema.tables WHERE table_schema NOT IN('information_schema', 'pg_catalog') ) AS table_count," +
                                            " (SELECT COUNT(*) FROM information_schema.columns WHERE table_schema NOT IN('information_schema', 'pg_catalog') ) AS column_count, "+
                                            " 'postgresql' as database_type FROM pg_database WHERE datname = current_database(); ", conn_postgresql);
            using var reader = await cmd.ExecuteReaderAsync();
            var result = new Dictionary<string, object>();// List<string>();
            //String result = "";
            //var dataTable = new DataTable();

            while (await reader.ReadAsync())
            //if (await reader.ReadAsync())
            //if (reader.HasRows)
            {
                //result.Add("database_size_in_bytes:"+reader.GetValue(0).ToString()+"L");//ALERT>>Here "L" added due to c# not understand bigint
                result.Add("database_name", reader.GetValue(1));
                result.Add("database_type", "postgresql");// dbCredentials.DbType);
                result.Add("db_size", reader.GetValue(0));
                result.Add("table_count", reader.GetValue(2));
                result.Add("column_count", reader.GetValue(3));

                //dataTable.Load(reader);

                //result = @"""db_size"":" + reader.GetValue(0).ToString() + 
                //         @" , ""database_name"":""" + reader.GetValue(1).ToString() + 
                //         @""" , ""database_type"":" + reader.GetValue(2).ToString() +@"""";


            }

            await conn_postgresql.CloseAsync();
            //JsonConvert.SerializeObject(dataTable, Formatting.Indented)
            //return System.Text.Json.JsonSerializer.Serialize(dataTable);
            //return JsonConvert.SerializeObject(result);
            return result;

        }

        //public static async Task<List<TableInfo>> GetTablesFullInfoAsync()
        public static async Task<object> GetTablesFullInfoAsync()
        {
            //var dbCredentials = DatabaseConfig.GetCredentials(databaseName);
            await using var conn_postgresql = new NpgsqlConnection(ConfigurationManager.ConnectionStrings[GetSetting("DbTypePostGre")].ConnectionString);// dbCredentials.ConnectionString);
            await conn_postgresql.OpenAsync();

            var table_nameList = new List<TableInfo>();

            var tables = await ListPostgresTablesAsync(conn_postgresql);
            var result = new Dictionary<string, object>();//result

            //List<String> resultJson = new List<String>();

            //    response_object = { }
            //    tables = await self.list_postgres_tables_and_creation()
            //print("tables", len(tables))
            //for table in tables:
            //    table_name = table["table_name"]
            //    schema_name = table["schema"]
            //    schema = await self.get_postgres_table_schema(table_name, schema_name)
            //    body = await self.fetch_postgres_sample_data(table_name, schema_name)
            //    info = await self.get_postgres_table_info(table_name, schema_name)
            //    response_object[table_name] = {
            //        "schema_name": schema_name,
            //        "table_schema": schema,
            //        "table_body": body,
            //        "table_size": info["size_in_bytes"],
            //        "row_count": info["row_count"],
            //        "column_count": info["column_count"],
            //    }
            //    return response_object

            //System.Collections.Generic.List<string, string, string, string, string, table_nameList> GetTablesFullInfoMetadata = new System.Collections.Generic.List<string, string, string, string, string, table_nameList>();
            //List<TableInfo> tblinfolst = new List<TableInfo>();
            Dictionary<string, TableInfo> tableInfoDict = new Dictionary<string, TableInfo>();
            //TableInfoLister TableInfoLister1= new TableInfoLister();
            //List<TableInfoLister> TableInfoListerCollection = new List<TableInfoLister>();

            foreach (var table in tables)
            {
                //TableInfoLister1 = new TableInfoLister();
                var table_name = table["table_name"].ToString();
                var schema_name = table["schema"].ToString();//		schema_name	null	string
                var schema = await GetPostgresTableSchemaAsync(table_name, schema_name, conn_postgresql);//System.Collections.Generic.List<System.Collections.Generic.Dictionary<string, object>>
                var body = await FetchPostgresSampleDataAsync(table_name, schema_name, conn_postgresql);//		body	null	System.Collections.Generic.Dictionary<string, System.Collections.Generic.List<object>>
                var info = await GetPostgresTableInfoAsync(table_name, schema_name, conn_postgresql);//-		info	(0, 0)	(long, long)

                //table_nameList.Add(new TableInfo
                //{
                //    //TableName = table_name,
                //    schema_name = schema_name,
                //    table_schema = schema,
                //    table_body = body,
                //    table_size = info.Item1,
                //    row_count = info.Item2,
                //    column_count = schema.Count
                //});

                //TableInfoLister1.table_name = table_name;//"table_name": "esindexer_settings",
                //TableInfoLister1.schema_name = schema_name;//"schema_name": "public",
                //TableInfoLister1.table_body = body;//"body": {        "fm_saltkey": [
                //TableInfoLister1.schema = schema;//"schema": [  { "schema": "public",
                //TableInfoLister1.info = info;//"info": {    "Item1": 16384, "Item2": 3
                //TableInfoLister1.tableInfo = (new TableInfo //      "tableInfo": {  "column_count": 3,  "row_count": 3, "schema_name": "public",
                //{                   
                //    column_count = schema.Count,
                //    //row_count = info.Item2,
                //    schema_name = schema_name,
                //    table_body = body,
                //    table_schema = schema,
                //    //table_size = info.Item1
                //});

                //TableInfoListerCollection.Add(TableInfoLister1);

                tableInfoDict.Add(table_name, new TableInfo
                {
                    column_count = schema.Count,
                    row_count = info.Item2,
                    schema_name = schema_name,
                    table_body = body,
                    table_schema = schema,
                    table_size = info.Item1
                });

                //tblinfolst.Add(new TableInfo {
                //    column_count = schema.Count,
                //    //row_count = info.Item2,
                //    schema_name = schema_name,
                //    table_body = body,
                //    table_schema = schema,
                //    //table_size = info.Item1
                //});

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
            //return JsonConvert.SerializeObject(table_nameList);
            //return JsonConvert.SerializeObject(resultJson);
            //return TableInfoListerCollection;
            return tableInfoDict;
        }

        private static async Task<List<Dictionary<string, object>>> ListPostgresTablesAsync(NpgsqlConnection conn_postgresql)
        {

            var tables = new List<Dictionary<string, object>>();

            //  SELECT table_schema, table_name, NULL AS creation_time
            //    FROM information_schema.tables
            //    WHERE table_type = 'BASE TABLE'
            //    AND table_schema NOT IN('pg_catalog', 'information_schema');

            //-- WHERE table_type = 'BASE TABLE' AND table_schema NOT IN ('pg_catalog');";

            var query = @"
            SELECT table_name, table_schema, NULL AS creation_time
            FROM information_schema.tables
            WHERE table_type = 'BASE TABLE' AND table_schema NOT IN('pg_catalog', 'information_schema');";


            await using var cmd = new NpgsqlCommand(query, conn_postgresql);
            await using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                tables.Add(new Dictionary<string, object>
            {
                { "table_name", reader["table_name"] },
                { "schema", reader["table_schema"] },
                { "creation_time", reader["creation_time"] }
            });
            }

            return tables;
        }

        private static async Task<List<Dictionary<string, object>>> GetPostgresTableSchemaAsync(string tableName, string schema, NpgsqlConnection conn_postgresql)
        {
            var schemaInfo = new List<Dictionary<string, object>>();

            //SELECT column_name, data_type FROM information_schema.columns WHERE table_name = $1 AND table_schema = $2;            

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
                //{ "column_name", reader["column_name"] },
                //{ "data_type", reader["data_type"] },
                //{ "is_nullable", reader["is_nullable"] }
                { "schema", schema },
                { "name",  reader["column_name"] },
                { "column_type", reader["data_type"] }
            });
            }

            return schemaInfo;
        }

        //private static async Task<List<Dictionary<string, object>>> FetchPostgresSampleDataAsync(string tableName, string schema, NpgsqlConnection conn_postgresql)
        //{
        //    var data = new List<Dictionary<string, object>>();

        //    //var query = $"SELECT * FROM \"{schema}\".\"{tableName}\" LIMIT 5;";
        //    String qualified_table  = "\"{schema}\".\"{tableName}";

        //    var query = $"SELECT COUNT(*) FROM {qualified_table}";

        //    await using var cmd = new NpgsqlCommand(query, conn_postgresql);
        //    await using var reader = await cmd.ExecuteReaderAsync();

        //    int total_rows = 0;
        //    while (await reader.ReadAsync())
        //    {
        //        //var row =  new Dictionary<string, object>();
        //        //for (int i = 0; i < reader.FieldCount; i++)
        //        //{
        //        //    row[reader.GetName(i)] = reader.GetValue(i);
        //        //}
        //        //data.Add(row);

        //        total_rows = int.Parse(reader.GetValue(0).ToString());

        //    }

        //    String col_names_query = "";

        //    if (total_rows == 0)
        //    { col_names_query = "SELECT * FROM {qualified_table} LIMIT 0;";
        //        await using var cmd_col_names_query = new NpgsqlCommand(col_names_query, conn_postgresql);
        //        await using var reader_col_names_query = await cmd.ExecuteReaderAsync();

        //        while (await reader.ReadAsync())
        //        {

        //        //    stmt = await self.connection.prepare(col_names_query)
        //        //columns = [a.name for a in stmt.get_attributes()]
        //        //return { column: [] for column in columns}

        //            var row = new Dictionary<string, object>();
        //            for (int i = 0; i < reader.FieldCount; i++)
        //            {
        //                row[reader.GetName(i)] = null;
        //                data.Add(row);                        
        //            }
        //            return data;
        //        }
        //    }

        //    if (total_rows <= 20)
        //        row_indices = list(range(total_rows))
        //    else:
        //        row_indices = [0, total_rows - 1]
        //        skip = max(1, int((total_rows - 2) / 18))
        //        for i in range(skip, total_rows - 1, skip):
        //            row_indices.append(i)
        //        row_indices = sorted(row_indices[:20])

        //    query = (
        //        f"SELECT * FROM (SELECT *, ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) AS row_num FROM {qualified_table}"
        //        f") AS subquery WHERE row_num IN ({', '.join(str(i + 1) for i in row_indices)});"
        //    )

        //        rows = await self.connection.fetch(query)
        //    if not rows:
        //        return None

        //    columns = list(rows[0].keys())
        //    result = { col: [] for col in columns}
        //    for row in rows:
        //        for col in columns:
        //            result[col].append(await make_json_serializable(row[col]))

        //    if "row_num" in result:
        //    del result["row_num"]

        //    return result





        //    //while (await reader.ReadAsync())
        //    //{
        //    //    var row = new Dictionary<string, object>();
        //    //    for (int i = 0; i < reader.FieldCount; i++)
        //    //    {
        //    //        row[reader.GetName(i)] = reader.GetValue(i);
        //    //    }
        //    //    data.Add(row);
        //    //}

        //    return data;
        //}


        //public async Task<Dictionary<string, List<object>>> FetchSampleDataAsync(string schemaName, string tableName)
        //private static async Task<List<Dictionary<string, List<object>>>> FetchPostgresSampleDataAsync(string tableName, string schemaName)
        private static async Task<Dictionary<string, List<object>>> FetchPostgresSampleDataAsync(string tableName, string schemaName, NpgsqlConnection conn_postgresql)
        {
            try
            {

                string qualified_table = $"\"{schemaName}\".\"{tableName}\"";


                var query = $"SELECT COUNT(*) FROM {qualified_table}";
                await using var cmd = new NpgsqlCommand(query, conn_postgresql);
                var totalRows = Convert.ToInt32(await cmd.ExecuteScalarAsync());


                if (totalRows == 0)
                {
                    var emptyCmd = new NpgsqlCommand($"SELECT * FROM {qualified_table} LIMIT 0;", conn_postgresql);
                    using var reader = await emptyCmd.ExecuteReaderAsync();

                    var schemaTable = reader.GetSchemaTable();
                    var result = new Dictionary<string, List<object>>();
                    foreach (DataRow col in schemaTable.Rows)
                    {
                        result[col["ColumnName"].ToString()] = new List<object>();
                    }
                    return result;
                }

                List<int> rowIndices;
                if (totalRows <= 20)
                {
                    rowIndices = Enumerable.Range(0, totalRows).ToList();
                }
                else
                {
                    rowIndices = new List<int> { 0, totalRows - 1 };
                    int skip = Math.Max(1, (totalRows - 2) / 18);
                    for (int i = skip; i < totalRows - 1; i += skip)
                    {
                        rowIndices.Add(i);
                    }
                    rowIndices = rowIndices.OrderBy(i => i).Take(20).ToList();
                }

                string rowNumbers = string.Join(", ", rowIndices.Select(i => i + 1));
                query = $" SELECT * FROM ( SELECT *, ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) AS row_num FROM {qualified_table}) AS subquery WHERE row_num IN ({rowNumbers}) ";

                using var dataCmd = new NpgsqlCommand(query, conn_postgresql);
                using var dataReader = await dataCmd.ExecuteReaderAsync();

                var results = new Dictionary<string, List<object>>();
                for (int i = 0; i < dataReader.FieldCount; i++)
                {
                    results[dataReader.GetName(i)] = new List<object>();
                }

                while (await dataReader.ReadAsync())
                {
                    for (int i = 0; i < dataReader.FieldCount; i++)
                    {
                        var val = dataReader.IsDBNull(i) ? null : dataReader.GetValue(i);
                        results[dataReader.GetName(i)].Add(MakeJsonSerializable(val));
                    }
                }

                if (results.ContainsKey("row_num"))
                {
                    results.Remove("row_num");
                }

                return results;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching records from PostgreSQL table '{schemaName}.{tableName}': {  ex.Message } ");
                return null;
            }
        }

        public static object MakeJsonSerializable(object value)
        {
            if (value == null || value is string || value is ValueType)
                return value;
            return value.ToString();
        }


        private static async Task<(long, long)> GetPostgresTableInfoAsync(string tableName, string schema, NpgsqlConnection conn_postgresql)
        {
            string qualified_table = $"\"{schema}\".\"{tableName}\"";
            
            //var result = new Dictionary<string, List<object>>();
            //result.Add("size_in_bytes", "");
            //result.Add("row_count", "");
            //result.Add("column_count", "");

            // Get table size f"SELECT pg_total_relation_size('{qualified_table}');"
            var sizeCmd = new NpgsqlCommand("SELECT pg_total_relation_size(@fullTableName);", conn_postgresql);
            sizeCmd.Parameters.AddWithValue("fullTableName", $"{schema}.{tableName}");
            var sizeResult = await sizeCmd.ExecuteScalarAsync();
            long sizeInBytes = sizeResult != DBNull.Value ? Convert.ToInt64(sizeResult) : 0;

            // Get row count
            var countCmd = new NpgsqlCommand($"SELECT COUNT(*) FROM {qualified_table};", conn_postgresql);
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

        public static async Task<object> ExecuteQueryAsync(string query)//NpgsqlConnection conn, string query)
        {
            //var dbCredentials = DatabaseConfig.GetCredentials(databaseName);
            await using var conn_postgresql = new NpgsqlConnection(ConfigurationManager.ConnectionStrings[GetSetting("DbTypePostGre")].ConnectionString);// dbCredentials.ConnectionString);
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

            //return JsonConvert.SerializeObject(result);
            return result;
        }
    }
}
