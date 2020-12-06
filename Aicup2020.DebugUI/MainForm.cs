using Aicup2020;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Aicup2020.Debug
{
    public partial class MainForm : Form
    {
        private Runner runner;
        private Thread thread;

        public MainForm()
        {
            InitializeComponent();
        }

        private void CreateRunner()
        {
            string host = "127.0.0.1";
            int port = 31001;
            string token = "0000000000000000";
            this.runner = new Runner(host, port, token);
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            this.thread = new Thread(() =>
            {
                try
                {
                    this.CreateRunner();
                    this.runner.Run();
                }
                catch
                {
                }
            });
            this.thread.Start();
        }

        private void buttonDisconnect_Click(object sender, EventArgs e)
        {
            this.runner.Stop();
            this.runner = null;
        }
    }
}
