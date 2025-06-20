using System;
using System.Collections.Concurrent;
using System.Windows.Forms;

namespace AgentApp
{
    public partial class Form1OLD : Form
    {
        private ConcurrentDictionary<string, Agent> agents = new();

        public Form1OLD()
        {
            InitializeComponent();
        }

        //private async void btnStartAgent_Click(object sender, EventArgs e)
        //{
        //    var uuid = this.textBox1.Text;
        //    var type = "file_indexer";//cmbAgentType.Text.Trim();

        //    if (string.IsNullOrWhiteSpace(uuid) || (type != "database_indexer" && type != "file_indexer"))
        //        return;

        //    var agent = new Agent(uuid, type);
        //    agent.OnLog += LogToUI;
        //    agents[uuid] = agent;
        //    await agent.StartAsync();
        //}

        private void LogToUI(string msg)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => listBoxLogs.Items.Add(msg)));
            }
            else
            {
                listBoxLogs.Items.Add(msg);
            }
        }

        private async void btnFileIndexer_Click(object sender, EventArgs e)
        {
            var uuid = this.textBox1.Text;
            var type = "file_indexer";//cmbAgentType.Text.Trim();

            var agent = new Agent(uuid, type);
            agent.OnLog += LogToUI;
            agents[uuid] = agent;
            await agent.StartAsync();

        }

        private async void btnDbIndexer_Click(object sender, EventArgs e)
        {
            var uuid = this.textBox1.Text;
            var type = "database_indexer";//cmbAgentType.Text.Trim();

            var agent = new Agent(uuid, type);
            agent.OnLog += LogToUI;
            agents[uuid] = agent;
            await agent.StartAsync();
        }
    }
}
