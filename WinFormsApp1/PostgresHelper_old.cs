using Newtonsoft.Json;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AgentApp
{
    public static class PostgresHelper_old
    {
        public static async Task<object> GetMetadataAsync(NpgsqlConnection conn)
        {
            // Example: return list of schemas
            //                SELECT pg_database_size(datname) AS database_size_in_bytes, current_database() AS database_name FROM pg_database WHERE datname = $1; ,self.credentials["database"],

            using var cmd = new NpgsqlCommand("SELECT pg_database_size(datname) AS database_size_in_bytes, current_database() AS database_name,'postgresql' as database_type  FROM pg_database WHERE datname = current_database();", conn);
            using var reader = await cmd.ExecuteReaderAsync();
            var result = new List<string>();
            while (await reader.ReadAsync())
            {
                result.Add(reader.GetString(0));
            }
            return JsonConvert.SerializeObject(result);
            //return result;

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
            return JsonConvert.SerializeObject(result);
        }
    }

}
