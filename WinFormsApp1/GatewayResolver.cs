// AgentApp/GatewayResolver.cs
using System.Configuration;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.VisualBasic.Logging;
using System.Security.Policy;
using NLog;

namespace AgentApp
{
    public static class GatewayResolver
    {
        private static readonly HttpClient client = new();
        public static string LoadBalancerUrl = GetSetting("LoadBalancerUrl");//"https://cse-indexer-loadbalancer.promptprivacy.com/check-gateway";

        public static async Task<string> GetGatewayUrlAsync(AgentConfig config)
        {
            var payload = new
            {
                agent_uuid = config.AgentUUID,
                tenant_uuid = config.TenantUUID,
                secret_key = config.SecretKey

            };

            var content = new StringContent(JsonSerializer.Serialize(payload), System.Text.Encoding.UTF8, "application/json");
            var response = await client.PostAsync(LoadBalancerUrl, content);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var doc = JsonDocument.Parse(json);

            Program.loger.Log(LogLevel.Info, " Connecting >> "+ LoadBalancerUrl);
            //AgentApp.Agent.OnLog?.Invoke($"Connected:");

            return doc.RootElement.GetProperty("gateway_ip_port").GetString();
            //return "34.9.75.135:443";
        }

        private static String GetSetting(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}

