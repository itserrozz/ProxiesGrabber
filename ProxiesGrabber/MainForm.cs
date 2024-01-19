using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProxiesGrabber
{
    public partial class MainForm : Form
    {
        OptionsForm optionsForm;
        ProxiesGrabberForm proxiesGrabberForm;
        AboutForm aboutForm;
        public static bool isStarted = false, ExpandMenu = true;
        public static int sleep = 0, GrabbedProxiesCount = 0;
        public static TimeSpan remainTime;
        public static string[] URLS;
        public static string ProxiesPath = null;
        public static string SleepString = null;
        public static string Duration = null;
        private bool isMouseDown = false;
        private Point _frmlocation;
        private bool isProxiesFrmOpen, isOptionsFrmOpen, isAboutFrmOpen;
        public static string ThelastestVersion = "v1.0.0.0";
        public MainForm()
        {
            InitializeComponent();
            ctxMenu.Renderer = new ToolStripProfessionalRenderer(new MenuColorTable());
        }
        private void pictureBox1_Click(object sender, EventArgs e) => Close();
        

        private void mainbtn_Click(object sender, EventArgs e)
        {
            isOptionsFrmOpen = false;
            isAboutFrmOpen = false;
            isProxiesFrmOpen = true;
            if (proxiesGrabberForm == null)
            {
                mainbtn.ForeColor = Color.White;
                proxiesGrabberForm = new ProxiesGrabberForm();
                proxiesGrabberForm.FormClosed += proxiesGrabberForm_FormClosed;
                proxiesGrabberForm.MdiParent = this;
                proxiesGrabberForm.Dock = DockStyle.Fill;
                proxiesGrabberForm.Show();
            }
            else
            {

                optionsbtn.ForeColor = Color.Silver;
                aboutbtn.ForeColor = Color.Silver;
                proxiesGrabberForm.Activate();
            }
        }

        private void optionsForm_FormClosed(object sender, FormClosedEventArgs e) => optionsForm = null;

        private void proxiesGrabberForm_FormClosed(object sender, FormClosedEventArgs e) => proxiesGrabberForm = null;
        

        private void optionsbtn_Click(object sender, EventArgs e)
        {
            isProxiesFrmOpen = false;
            isAboutFrmOpen = false;
            isOptionsFrmOpen = true;
            if (optionsForm == null)
            {
                optionsForm = new OptionsForm();
                optionsForm.FormClosed += optionsForm_FormClosed;
                optionsForm.MdiParent = this;
                optionsForm.Dock = DockStyle.Fill;
                optionsForm.Show();
            }
            else
            {
                mainbtn.ForeColor = Color.Silver;
                aboutbtn.ForeColor = Color.Silver;
                optionsForm.Activate();
            }
        }

        private void pictureBox2_MouseClick(object sender, MouseEventArgs e) => ctxMenu.Show(sender as Control ?? throw new InvalidOperationException(), new Point(e.X, e.Y));

        private async void toolStripMenuItem1_ClickAsync(object sender, EventArgs e)
        {
            if (await IsLatestVersion())
            {
                MessageBox.Show("You are using the latest version! " + FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion.Substring(0, 3), "Proxies Grabber", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (MessageBox.Show("A new version is available, Would you like to download it?", "Proxies Grabber", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Process.Start($"https://github.com/itserrozz/proxiesgrabber/releases/download/{ThelastestVersion}/ProxiesGrabber.exe");
                }
            }

        }
        private async Task<bool> IsLatestVersion()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            var client = new HttpClient();
            var response = await client.GetAsync("https://github.com/itserrozz/proxiesgrabber/releases/latest");
            response.EnsureSuccessStatusCode();
            var responseUri = response.RequestMessage.RequestUri.ToString();
            response.Dispose();
            client.Dispose();
            var LastesVersion = responseUri.Substring(responseUri.LastIndexOf('/') + 1);
            var CurrentVersion =
                FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion;

            if (int.TryParse(Regex.Match(LastesVersion, @"\d+\.\d+\.\d+\.\d+").Value.Replace(".", string.Empty),
                    out var latestVersion) && int.TryParse(
                    Regex.Match(CurrentVersion, @"\d+\.\d+\.\d+\.\d+").Value.Replace(".", string.Empty),
                    out var currentVersion))
            {
                if (latestVersion > currentVersion)
                {
                    return false;
                }
                else
                {
                    ThelastestVersion = LastesVersion.ToString();
                    return true;
                }
            }
            return true;
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            isProxiesFrmOpen = false;
            isOptionsFrmOpen = false;
            isAboutFrmOpen = true;
            aboutbtn.ForeColor = Color.White;
            if (aboutForm == null)
            {
                aboutForm = new AboutForm();
                aboutForm.FormClosed += proxiesGrabberForm_FormClosed;
                aboutForm.MdiParent = this;
                aboutForm.Dock = DockStyle.Fill;
                aboutForm.Show();
            }
            else
            {
                mainbtn.ForeColor = Color.Silver;
                optionsbtn.ForeColor = Color.Silver;
                aboutForm.Activate();
            }
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e) => Close();


        private void timer1_Tick(object sender, EventArgs e)
        {
            if (ExpandMenu)
            {
                flowLayoutPanel1.Width -= 5;
                if (flowLayoutPanel1.Width <= 32)
                {
                    ExpandMenu = false;
                    SideBarTimer.Stop();
                }
            }
            else
            {
                flowLayoutPanel1.Width += 5;
                if (flowLayoutPanel1.Width >= 124)
                {
                    ExpandMenu = true;
                    SideBarTimer.Stop();
                }

            }
        }
        private void ShowAnimated()
        {
            var timer = new Timer
            {
                Interval = 10
            };
            timer.Tick += async delegate
            {
                if (Opacity < 1.0)
                    Opacity += 0.05;
                if (!(Opacity >= 1.0))
                    return;
                timer.Stop();
                Show();
                Opacity = 1.0;
                timer.Dispose();
                try
                {
                    if (await IsLatestVersion())
                        return;

                    if (MessageBox.Show("A new version is available, Would you like to download it?", "Proxies Grabber", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
                        Process.Start($"https://github.com/itserrozz/proxiesgrabber/releases/download/{ThelastestVersion}/ProxiesGrabber.exe");
                }
                catch { }

            };
            timer.Start();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            ShowAnimated();
            Controls.OfType<MdiClient>().FirstOrDefault().BackColor = Color.FromArgb(33, 34, 36);
            SleepString = "60";
            Duration = "Seconds";
            ThelastestVersion = "v" + FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion;
            label2.Text += " " + ThelastestVersion.Substring(0, 4);
            optionsbtn.PerformClick();
        }
        bool closing = false;

        private void _MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown)
            {
                this.Location = new Point(e.X + (Location.X - _frmlocation.X), e.Y + (Location.Y - _frmlocation.Y));
            }
            Update();
        }

        private void _MouseUp(object sender, MouseEventArgs e)
        {
            isMouseDown = false;
            Opacity = 1.0;
        }



        private void _MouseDown(object sender, MouseEventArgs e)
        {
            isMouseDown = true;
            _frmlocation = e.Location;
            Opacity = 0.85;
        }

        private void btn_MouseHover(object sender, EventArgs e) => ((Button)sender).ForeColor = Color.White;
        private void pictureBox3_Click(object sender, EventArgs e) => WindowState = FormWindowState.Minimized;


        private void pictureBox1_MouseHover(object sender, EventArgs e) => pictureBox1.Image = Properties.Resources.CloseHover;


        private void pictureBox1_MouseLeave(object sender, EventArgs e) => pictureBox1.Image = Properties.Resources.Close;


        private void pictureBox2_MouseHover(object sender, EventArgs e) => pictureBox2.Image = Properties.Resources.MenuHover;


        private void pictureBox2_MouseLeave(object sender, EventArgs e) => pictureBox2.Image = Properties.Resources.Menu;


        private void button1_Click(object sender, EventArgs e) => toolStripMenuItem2.PerformClick();

        private void btn_MouseLeave(object sender, EventArgs e)
        {
            Button targetbutton = sender as Button;
            if (targetbutton.Name == "mainbtn")
            {
                if (!isProxiesFrmOpen)
                {
                    ((Button)sender).ForeColor = Color.Silver;
                }
            }
            else if (targetbutton.Name == "optionsbtn")
            {
                if (!isOptionsFrmOpen)
                {
                    ((Button)sender).ForeColor = Color.Silver;
                }
            }
            else if (targetbutton.Name == "aboutfrm")
            {
                if (!isAboutFrmOpen)
                {
                    ((Button)sender).ForeColor = Color.Silver;
                }
            }
        }



        private void CloseAnimation()
        {
            var timer = new Timer
            {
                Interval = 10
            };
            timer.Tick += delegate
            {
                if (Opacity > 0.0)
                    Opacity += -0.1;
                if (!(Opacity <= 0.0))
                    return;
                Opacity = 0.0;
                timer.Stop();
                timer.Dispose();
                Environment.Exit(0);
            };
            timer.Start();
        }

        private new void Closing(object sender, FormClosingEventArgs e)
        {
            if (closing) return;
            closing = true;
            e.Cancel = true;
            CloseAnimation();
        }
    }
}