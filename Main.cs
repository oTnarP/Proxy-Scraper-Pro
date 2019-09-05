using System;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proxy_Scraper_Pro
{
    public partial class Main : Form
    {
        int count;
        public Main()
        {
            InitializeComponent();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.Filter = "Text Files|*.txt|All files|*.*";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                txtSource.Text = ofd.FileName;
            }
        }

        private async void btnRun_Click(object sender, EventArgs e)
        {
            count = 0;
            btnRun.Enabled = false;
            txtResult.Clear();
            using (StreamReader sr = new StreamReader(txtSource.Text))
            {
                string line;

                while ((line = sr.ReadLine()) != null)
                {
                    await Task.Run(() => GetProxy(line));
                }
            }

            btnRun.Enabled = true;
        }

        public void GetProxy(string url)
        {
            Label.CheckForIllegalCrossThreadCalls = false;
            Button.CheckForIllegalCrossThreadCalls = false;
            TextBox.CheckForIllegalCrossThreadCalls = false;
            ListBox.CheckForIllegalCrossThreadCalls = false;
            ListView.CheckForIllegalCrossThreadCalls = false;
            DataGridView.CheckForIllegalCrossThreadCalls = false;

            WebClient wb = new WebClient();
            string data = wb.DownloadString(url);

            string pattern = @"\d{1,3}(\.\d{1,3}){3}:\d{1,5}";

            MatchCollection matches = Regex.Matches(data, pattern);

            if (matches.Count > 0)
            {
                foreach (Match match in matches)
                {
                    txtResult.AppendText(match.Groups[0].Value + Environment.NewLine);
                    count++;

                    groupBox2.Text = "Proxies Found: " + count.ToString();
                }
            }

            wb.Dispose();
        }
    }
}
