using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProxiesGrabber
{
    public partial class ProxiesGrabberForm : Form
    {
        public ProxiesGrabberForm()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            this.ControlBox = false;
        }
        private void WaitingSleepEvent()
        {
            bool isFirstRun = true;
            while (MainForm.isStarted)
            {
                int tempsleep = MainForm.sleep;
                while (tempsleep > 0 && !isFirstRun)
                {
                    tempsleep--;
                    MainForm.remainTime = TimeSpan.FromSeconds(tempsleep);
                    lblTime.Text = $"Timer : {MainForm.remainTime.Hours}:{MainForm.remainTime.Minutes}:{MainForm.remainTime.Seconds}";
                    if (!MainForm.isStarted)

                        break;

                    Thread.Sleep(1000);
                }
                isFirstRun = false;
                string Proxies = "";
                foreach (var URL in MainForm.URLS)
                {
                    if (!MainForm.isStarted)
                        break;
                    if (File.ReadAllLines(MainForm.ProxiesPath).Count() > 0)
                    {
                        var filecontent = File.ReadAllText(MainForm.ProxiesPath);
                        File.WriteAllText(MainForm.ProxiesPath, filecontent.Substring(0, filecontent.Length - 1));
                    }
                    if (!URL.ToLower().StartsWith("http") || URL == string.Empty)
                        continue;
                    if (URL.ToLower().StartsWith("https://api.proxyscrape"))
                    {
                        var Result = ExtractFromProxyscrapeFile(URL);
                        if (string.IsNullOrEmpty(Result))
                            continue;
                        Proxies += Result;
                    }
                    else
                    {
                        var Result = GetFromAPI(URL);
                        if (string.IsNullOrEmpty(Result))
                            continue;
                        Proxies += Result;
                    }
                }
                if (OptionsForm.isRemoveDupChecked)
                {
                    if (OptionsForm.isClearChecked)
                        File.WriteAllLines(MainForm.ProxiesPath, new List<string> { Proxies }.Distinct().ToList());
                    else
                    {
                        List<string> lines = new List<string>();
                        lines.AddRange(File.ReadAllLines(MainForm.ProxiesPath).Distinct().ToList());
                        lines.AddRange(Proxies.Split('\n').Distinct().ToList());
                        File.WriteAllLines(MainForm.ProxiesPath, lines);
                    }
                }
                else
                {
                    if (OptionsForm.isClearChecked)
                        File.WriteAllText(MainForm.ProxiesPath, Proxies);
                    else
                        File.AppendAllText(MainForm.ProxiesPath, Proxies);
                }
                MainForm.GrabbedProxiesCount = 0;
            
            }
            button2.Text = "Run";
        }
        private string GetFromAPI(string url)
        {
        again:
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                HttpWebRequest request = WebRequest.CreateHttp(url);
                request.Timeout = 5000;
                request.Method = "GET";
                var proxiesContent = new StreamReader(request.GetResponse().GetResponseStream()).ReadToEnd();
                foreach (var proxy in proxiesContent.Split('\n'))
                {
                    if (proxy == string.Empty)
                        continue;
                    ResultrichTextBox1.AppendText(proxy);
                    MainForm.GrabbedProxiesCount++;
                    lblProxiesCounter.Text = $"Grabbed : {MainForm.GrabbedProxiesCount.ToString("#,#")} Proxies";
                    Application.DoEvents();

                }
                return proxiesContent;
            }
            catch
            {
                goto again;
            }
        }
        private string ExtractFromProxyscrapeFile(string URL)
        {
            try
            {
                HttpClient httpclient = new HttpClient();
                Task<HttpResponseMessage> responseMessage = httpclient.GetAsync(URL);
                responseMessage.Wait(8000);
                if (responseMessage.Result.IsSuccessStatusCode)
                {
                    Task<string> ProxiesContent = responseMessage.Result.Content.ReadAsStringAsync();
                    foreach (string Proxy in ProxiesContent.Result.Split('\n'))
                    {
                        var _Proxy = Proxy + Environment.NewLine;
                        MainForm.GrabbedProxiesCount++;
                        lblProxiesCounter.Text = $"Grabbed : {MainForm.GrabbedProxiesCount.ToString("#,#")} Proxies";
                        ResultrichTextBox1.AppendText(Proxy);
                        Thread.Sleep(1);
                        Application.DoEvents();
                    }
                    return ProxiesContent.Result;
                  
                }
            }
            catch (AggregateException)
            {

            }
            catch (Exception)
            {
              
            }
            return null;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (button2.Text == "Stop")
            {
                MainForm.isStarted = false;
                lblTime.Text = "Timer :  00:00:00";
                return;
            }
            if (MainForm.URLS == null)
            {
                MessageBox.Show("Please add apis!", "Proxies Grabber", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            button2.Text = "Running...";
            if (string.IsNullOrEmpty(MainForm.ProxiesPath))
            {
                MessageBox.Show("Please choose the proxies file", "Failed to find proxy file", MessageBoxButtons.OK, MessageBoxIcon.Error);
                button2.Text = "Run";
                return;
            }
            MainForm.sleep = int.Parse(MainForm.SleepString);
            if (MainForm.SleepString == "60")
                MainForm.sleep -= 1;
            if (lblTime.Text == "Timer :  00:00:00")
            {
                switch (MainForm.Duration)
                {
                    case "Minutes":
                        MainForm.sleep = 0 + (MainForm.sleep) * 60 + 60;
                        break;
                    case "Hours":
                        MainForm.sleep = (MainForm.sleep) * 60 * 60 + (59 * 60) + 60;
                        break;
                }
            }
            MainForm.isStarted = true;
            new Thread(() => WaitingSleepEvent()).Start();
            button2.Text = "Stop";
        }
    }
}
