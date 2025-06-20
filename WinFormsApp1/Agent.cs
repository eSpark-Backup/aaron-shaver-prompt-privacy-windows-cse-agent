// AgentApp/Agent.cs
using System;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Windows.Forms;
using Npgsql;
using AgentApp.Config;
using System.Security.Policy;
using Newtonsoft.Json;
using System.Text.Json.Nodes;
using Newtonsoft.Json.Linq;
using NLog;

namespace AgentApp
{
    public class Agent
    {
        private readonly AgentConfig _config;
        private ClientWebSocket _socket;
        private CancellationTokenSource _cts;
        public event Action<string> OnLog;

        public Agent(string uuid, string type,string Secret_Key,string Tenant_UUID)
        {
            //02JUNE2025 -- file_indexer
            //"agent_uuid": "9c85ab8d-d784-47d3-bf08-7bdcc669ca96",
            //"secret_key": "a62f456668e2e5dec68913d18bc7866faf39ae72efb679168c64f2bb21e9e244", 
            //"tenant_uuid": "e115fb59-060d-489d-aa88-7a741fe1d664"

            //02JUNE2025 -- database_indexer
            //"agent_uuid":"8a4bc282-b12c-4687-a211-177d9dc99817", 
            //"database_type": "postgresql",
            //"secret_key": "f2fdf3f42035d5829287abbf9f55f983382bb5b5588225495eee4533da579f78", 
            //"tenant_uuid": "e115fb59-868d-489d-aa88-7a741fe1d664"

            //private static readonly string AGENT_UUID = "2d490c34-75d8-4ab9-aa80-729b0d311ae5";
            //private static readonly string TENANT_UUID = "e115fb59-060d-489d-aa08-7a741fe1d664";
            //private static readonly string SECRET_KEY = "64f1adab0921144819284825099bd35a25babf0870990499baa03bd3a530e19d";

            //working keys
            //private static readonly string AGENT_UUID = "2d490c34-75d8-4ab9-aa80-729b0d311ae5";
            //private static readonly string TENANT_UUID = "e115fb59-060d-489d-aa08-7a741fe1d664";
            //private static readonly string SECRET_KEY = "64f1adab0921144819284825099bd35a25babf0870990499baa03bd3a530e19d";

            //"agent_uuid": "6dcbd2d4-0f34-4bc9-a482-17a8c7b45f4f",
            //"database_type": "postgres",
            //"secret_key": "eba417b590de16d722c3d0ccbc5ac9ef952df26528a6f2903a2af74d6cce27b1",
            //"tenant_uuid": "e115fb59-060d-489d-aa08-7a741fe1d664"

            _config = new AgentConfig
            {

                AgentUUID = uuid,
                RequestType = type,
                SecretKey = Secret_Key,//"eba417b590de16d722c3d0ccbc5ac9ef952df26528a6f2903a2af74d6cce27b1",
                TenantUUID = Tenant_UUID///"e115fb59-060d-489d-aa08-7a741fe1d664"
        };
        }

        public async Task StartAsync()
        {
            _cts = new CancellationTokenSource();
            await ConnectLoopAsync(_cts.Token);
        }

        public async Task CloseStartAsync()
        {
            // Send connection request
            //if (_socket.State != WebSocketState.Closed)
            if (_socket != null || _socket.State!=WebSocketState.Closed)//The WebSocket is in an invalid state ('Closed') for this operation. Valid states are: 'Open, CloseReceived, CloseSent'
            {
                try
                {
                    await _socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", _cts.Token);
                }
                catch { }
                OnLog?.Invoke($"Connection Closed reason: User Cancelled");
            }
            
            Program.isConnected = false;
            Program.isQueryRunning = false;
            Program.loger.Log(LogLevel.Info, "==================Connection Closed reason: User Cancelled===============" + DateTime.Now);

            //_socket.Abort();

        }

        //_socket.CloseAsync

        //THIS ONE IS Agent
        private async Task ConnectLoopAsync(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                try
                {

                    Program.loger.Log(LogLevel.Info, " Getting gateway URL....... " + GatewayResolver.LoadBalancerUrl);
                    OnLog?.Invoke($"Connecting....... : "+ GatewayResolver.LoadBalancerUrl);

                    string gatewayUrl = await GatewayResolver.GetGatewayUrlAsync(_config);
                    _socket = new ClientWebSocket();
                    var uri = new Uri($"ws://{gatewayUrl}/ws/{_config.AgentUUID}");
                    //var uri = new Uri("ws://127.0.0.1:5000");


                    // Prepare payload
                    var authPayload = new
                    {
                        secret_key = _config.SecretKey
                    };

                    string json = JsonConvert.SerializeObject(authPayload);
                    byte[] buffer = Encoding.UTF8.GetBytes(json);
                    var segment = new ArraySegment<byte>(buffer);

                    // Send connection request
                    await _socket.ConnectAsync(uri, token);

                    // Send payload
                    await _socket.SendAsync(segment, WebSocketMessageType.Text, true, CancellationToken.None);
                    //Console.WriteLine("Authentication sent");
                    Program.loger.Log(LogLevel.Info, " Authentication sent ....... " + authPayload);
                    OnLog?.Invoke($"Connected: {_config.RequestType} -> {uri}");
                    Program.isConnected = true;
                    
                    //await AuthenticateAsync();
                    //
                    //
                    await RequestTypeDB();                    
                    _ = ListenLoopAsync(token);


                    break;
                }
                catch (Exception ex)
                {
                    Program.isConnected = false;
                    OnLog?.Invoke($"Connection failed: {ex.Message}, retrying...");
                    await Task.Delay(2000);
                }
            }
        }

        private async Task AuthenticateAsync()
        {
            var auth = System.Text.Json.JsonSerializer.Serialize(new { secret_key = _config.SecretKey });
            await SendAsync(auth);
        }
        private async Task RequestTypeDB()
        {
            var auth = System.Text.Json.JsonSerializer.Serialize(new { request_type = "database_indexer" });
            await SendAsync(auth);
        }

        private async Task ListenLoopAsync(CancellationToken token)
        {
            var buffer = new byte[4096];
            while (_socket.State == WebSocketState.Open && !token.IsCancellationRequested)
            {
                Program.isQueryRunning = true;
                var result = await _socket.ReceiveAsync(buffer, token);
                if (result.MessageType == WebSocketMessageType.Close)
                {
                    Program.isQueryRunning = false;
                    Program.isConnected =false;
                    await _socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", token);
                    OnLog?.Invoke($"Connection Closed reason: {result.CloseStatus}");
                    break;
                }
                 string message = Encoding.UTF8.GetString(buffer, 0, result.Count);
                OnLog?.Invoke($"Received: {message}");

                await HandleMessageAsync(message);
            }
        }

        private async Task HandleMessageAsync(string msg)
        {
            try
            {
                using var doc = JsonDocument.Parse(msg);
                var root = doc.RootElement;

                if (root.TryGetProperty("type", out var typeProp) && typeProp.GetString() == "ping")
                {
                    var pong = System.Text.Json.JsonSerializer.Serialize(new { type = "pong" });
                    await SendAsync(pong);
                    return;
                }

                if (_config.RequestType == "file_indexer")
                {
                    await SendFileUploadAsync();
                }
                else if (_config.RequestType == "database_indexer")
                {
                    await SendDatabaseResponseAsync(root);
                }
            }
            catch (Exception ex)
            {
                OnLog?.Invoke($"Error handling message: {ex.Message}");
            }
        }

        private async Task SendFileUploadAsync()
        {
            var payload = new
            {
                request_type = "file_indexer",
                agent_uuid = _config.AgentUUID,
                tenant_uuid = _config.TenantUUID,
                
                payload = new
                {
                    method = "upload-files",
                    files = new[]
                    {
                        new
                        {
                            file_size = "1234213",
                            creation_date = "2024-11-01T03:15:57Z",
                            modified_date = "2024-11-01T03:15:57Z",
                            file_hash = "abc123...",
                            fast_hash = "fast123...",
                            directory = "C:/User/Download",
                            file_name = "sample.png",
                            file = Convert.ToBase64String(Encoding.UTF8.GetBytes("dummydata"))
                        }
                    }
                }
            };

            var json = System.Text.Json.JsonSerializer.Serialize(payload);
            await SendAsync(json);
        }

        private async Task SendDatabaseResponseAsync(JsonElement original)
        {
            //var response = new
            //{
            //    request_type = "database_indexer",
            //    request_id = original.GetProperty("request_id").GetString(),
            //    data = new { message = "Indexed successfully" }
            //};
            //var json = JsonSerializer.Serialize(response);
            //await SendAsync(json);


            //if method == "get-database-metadata":			                        //"get-database-metadata"
            //metadata = await db_instance.get_metadata()			                //{
            //return jsonify({			                                            //    "db_metadata":
            //    "success": True,			                                        //            {
            //    "message": "Database details retrieved successfully.",			//                "database_name": "pp-dev-1",
            //    "request_id": body["request"],			                        //                "database_type": "postgresql",
            //    "db_metadata": metadata			                                //                "db_size": 325026595
            //})			                                                        //            },
            //    "message": "Database details retrieved successfully.",
            //    "request_id": "2801d51b-f2b0-455a-8382-fbab6bb80be8",
            //    "request_type": "database_indexer",
            //    "success": true
            //}



            //if method == "get-tables-details":				                        //2 - get-tables-details
            //details = await db_instance.get_table_details()                           //{
            //return jsonify({
            //message": "Table schema and body retrieved successfully.",
            //success: True,				                                            //request_id: "bf94a746-252a-4bf4-9f45-3530bd418f11",
            //message: "Table schema and body retrieved successfully.",				    //request_type: "database_indexer",
            //request_id: body["request"],				                                //success: true,>
            //tables_detail: details                                                    //tables_detail: { }
            //})				                                                        //}


            //if method == "perform-query-execution":			                        //"get-database-metadata"
            //metadata = await db_instance.get_metadata()			                //{
            //return jsonify({			                                            //    "db_metadata":
            //    "success": True,			                                        //            {
            //    "message": "Database details retrieved successfully.",			//                "database_name": "pp-dev-1",
            //    "request_id": body["request"],			                        //                "database_type": "postgresql",
            //    "db_metadata": metadata			                                //                "db_size": 325026595
            //})			                                                        //            },
            //    "message": "Database details retrieved successfully.",
            //    "request_id": "2801d51b-f2b0-455a-8382-fbab6bb80be8",
            //    "request_type": "database_indexer",
            //    "success": true
            //}



            //   if method == "perform-query-execution":					            //if method == "perform-query-execution":	
            //query = payload.get("query")					                            //    "message": "Database details retrieved successfully.",	
            //   if not query:                                                         //    "request_id": "2801d51b-f2b0-455a-8382-fbab6bb80be8",	
            //       return jsonify({ "error": "query is required."}), 400				//    "request_type": "database_indexer",	
            //   result = await db_instance.execute_query(query)					    //"result": [{"col1":"val1","col2":"val2"."col3":"val3"}]	
            //   return jsonify({
            //       success: True,						
            //        message: "Query executed successfully.",						
            //        request_id: body["request"],						
            //        result: result
            //   })						



            //   else:						
            //return jsonify({ "error": f"Method {method} not supported."}), 488

            try
            {
                var message = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(original);
                Program.loger.Log(LogLevel.Info, "==================Original JSON msg ===============" + message);
                OnLog?.Invoke($"Original JSON: {message}");

                if (!message.TryGetProperty("request_id", out var requestIdProp))
                {
                    await SendErrorAsync("request is required.", "database_indexer");
                    Program.loger.Log(LogLevel.Info, "==================Missing Request ===============");
                    OnLog?.Invoke($"Missing Request_ID: {message}");
                    return;
                }

                if (!message.TryGetProperty("payload", out var payload))
                {
                    await SendErrorAsync("payload is required.", "database_indexer");
                    Program.loger.Log(LogLevel.Info, "================== Payload Missing ===============");
                    OnLog?.Invoke($"Missing Payload: {message}");
                    return;
                }



                //string databasetype = payload.GetProperty("database_type").GetString();




                string databaseName = payload.GetProperty("database_name").GetString();
                if (string.IsNullOrEmpty(databaseName))
                {
                    await SendErrorAsync("database_name is required.", "database_indexer");
                    Program.loger.Log(LogLevel.Info, "================== database_name ===============" + databaseName);
                    OnLog?.Invoke($"Missing database_name: {databaseName}");
                    return;
                }

                string method = payload.GetProperty("method").GetString();
                if (string.IsNullOrEmpty(method))
                {
                    await SendErrorAsync("method is required.", "database_indexer");
                    Program.loger.Log(LogLevel.Info, "================== Payload method ===============" + method);
                    OnLog?.Invoke($"Missing payload method: {method}");
                    return;
                }

                // Lookup database credentials (replace with your real config source)
                //var dbCredentials = DatabaseConfig.GetCredentials(databaseName);
                var dbCredentials = DatabaseConfig.GetCredentials("Database");
                if (dbCredentials == null)
                {
                    await SendErrorAsync($"db_credentials not found for {databaseName}", "database_indexer");
                    Program.loger.Log(LogLevel.Info, "================== Payload db_credentials not found for databaseName ===============" + databaseName);
                    OnLog?.Invoke($"Missing dbcredentials: {dbCredentials}");
                    return;
                }

                //if (dbCredentials.DbType != "sqlite")//postgresql
                //{
                //    await SendErrorAsync($"Database type {dbCredentials.DbType} not supported.", "database_indexer");
                //    return;
                //}

                //await conn.OpenAsync();
                //using var conn= new NpgsqlConnection();//tempconn
                object result = null;
                JsonObject Json_result = new JsonObject();

                if (method == "get-database-metadata")
                {
                    //if (databasetype == "postgresql")
                    //{              
                    //var conn_postgresql = new NpgsqlConnection(dbCredentials.ConnectionString);
                    result = await PostgresHelper.GetMetadataAsync(databaseName);
                    Program.loger.Log(LogLevel.Info, "================== Payload get-database-metadata ===============" + result);
                    OnLog?.Invoke($"Parsed Payload get-database-metadata : {result}");

                    //}
                    //else if (databasetype == "")
                    //    result = await SqliteHelper.GetMetadataAsync("Data Source=FlexMold.db;");

                    //result = await PostgresHelper.GetMetadataAsync(conn);
                }
                else if (method == "get-tables-details")
                {
                    //result = await PostgresHelper.GetTablesDetailsAsync(conn);
                    //if (databasetype == "postgresql")
                    //{
                        //var conn_postgresql = new NpgsqlConnection(dbCredentials.ConnectionString);
                        result = await PostgresHelper.GetTablesFullInfoAsync(databaseName);
                        Program.loger.Log(LogLevel.Info, "================== Payload get-tables-details ===============" + result);
                        OnLog?.Invoke($"Parsed Payload get-tables-details : {result}");

                    //}
                    //else if (databasetype == "")
                    //    result = await SqliteHelper.GetTablesDetailsAsync("Data Source=FlexMold.db;");

                }
                else if (method == "perform-query-execution")
                {
                    string query = payload.GetProperty("query").GetString();
                    if (string.IsNullOrEmpty(query))
                    {
                        await SendErrorAsync("query is required.", "database_indexer");
                        Program.loger.Log(LogLevel.Info, "================== Payload perform-query-execution Error > query missing ===============");
                        OnLog?.Invoke($"Parsed Payload perform-query-execution : {result}");

                        return;
                    }
                    //if (databasetype == "postgresql")
                    //{
                        result = await PostgresHelper.ExecuteQueryAsync(databaseName, query);
                        Program.loger.Log(LogLevel.Info, "================== Payload perform-query-execution ===============" + result);
                        OnLog?.Invoke($"Parsed Payload perform-query-execution  : {result}");

                    //}
                }
                else
                {
                    await SendErrorAsync($"Method {method} not supported.", "database_indexer");
                    Program.loger.Log(LogLevel.Info, "================== Method not supported ===============" + method);
                    OnLog?.Invoke($"NO METHOD MATCHED : {method}");
                    return;
                }

                //                {
                //                    "db_metadata": {
                //                        "database_name": "pp-dev-1",
                //        "database_type": "postgresql",
                //        "db_size": 325026595
                //                    },
                //    "message": "Database details retrieved successfully.",
                //    "request_id": "2801d51b-f2b0-455a-8382-fbab6bb80be8",
                //    "request_type": "database_indexer",
                //    "success": true
                //}

                var response = new object();
                object ob = "{}";
                JsonObject obb = new JsonObject();

                if (method == "get-database-metadata")
                    response = new
                    {
                        db_metadata = result,
                        message = "Database request completed successfully.",
                        request_id = requestIdProp.GetString(),
                        request_type = "database_indexer",
                        success = true
                    };
                else if (method == "get-tables-details")
                    response = new
                    {
                        tables_detail = result,
                        message = "Database request completed successfully.",
                        request_id = requestIdProp.GetString(),
                        request_type = "database_indexer",
                        success = true
                    };
                else if (method == "perform-query-execution")
                    response = new
                    {
                        tables_detail = result,//db_metadata = result,
                        message = "Database request completed successfully.",
                        request_id = requestIdProp.GetString(),
                        request_type = "database_indexer",
                        success = true
                    };



                //await _socket.SendAsync(JsonSerializer.Serialize(response));
                //Console.WriteLine(JsonConvert.SerializeObject(response));
                Program.loger.Log(LogLevel.Info, " Json Sent .... "+ JsonConvert.SerializeObject(response));
                OnLog?.Invoke(" Json Sent .... " + JsonConvert.SerializeObject(response));

                var bytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(response));
                await _socket.SendAsync(bytes, WebSocketMessageType.Text, true, _cts.Token);
            }
            catch (Exception ex)
            {
                await SendErrorAsync($"Exception occurred: {ex.Message}", "database_indexer");
                Program.loger.Log(LogLevel.Info, " Exception in Payload reply > "+ex.Message);
                OnLog?.Invoke(" Exception in Payload reply > " + ex.Message);
            }


        }

        private async Task SendErrorAsync(string error, string requestType)
        {
            var response = new
            {
                success = false,
                request_type = requestType,
                error
            };
            //await _socket.SendAsync(JsonSerializer.Serialize(response));
            var bytes = Encoding.UTF8.GetBytes(error);
            await _socket.SendAsync(bytes, WebSocketMessageType.Text, true, _cts.Token);

        }

        private async Task SendAsync(string msg)
        {
            var bytes = Encoding.UTF8.GetBytes(msg);
            await _socket.SendAsync(bytes, WebSocketMessageType.Text, true, _cts.Token);
        }
    }
}
