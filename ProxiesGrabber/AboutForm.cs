using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace ProxiesGrabber
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
            label2.Text = String.Format(label2.Text, MainForm.ThelastestVersion.Substring(0, 3));
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://i.instagram.com/404.erroz");
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://i.instagram.com/v404");

        }

        private void button1_Click(object sender, EventArgs e)
        {
            new WebClient().DownloadFile($"https://github.com/itserrozz/ProxiesGrabber/archive/refs/tags/{MainForm.ThelastestVersion}.zip", Assembly.GetExecutingAssembly().Location.Replace(".exe", "") + "-Src.zip");
            Process.Start("https://github.com/itserrozz/ProxiesGrabber/");
            Thread.Sleep(200);
            Process.Start(Directory.GetCurrentDirectory());

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!File.Exists("how_can_use_it.mp4"))
                new WebClient().DownloadFile("https://github.com/itserrozz/ProxiesGrabber/blob/main/How_can_use_it.mp4", "how_can_use_it.mp4");
            Process.Start("how_can_use_it.mp4");
        }
    }
}
