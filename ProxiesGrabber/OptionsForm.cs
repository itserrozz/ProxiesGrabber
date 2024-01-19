using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace ProxiesGrabber
{
    public partial class OptionsForm : Form
    {
       public static bool isRemoveDupChecked = false;
       public static bool isClearChecked = false;
        public OptionsForm()
        {
            InitializeComponent();
            ctxCopy.Renderer = new ToolStripProfessionalRenderer(new MenuColorTable());
            listView1.ContextMenuStrip = ctxCopy;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            this.ControlBox = false;
            comboBox1.SelectedIndex = 0;
            PathtextBox1.TabStop = false;
            listView1.MultiSelect = true;
            pictureBox1_Click(new object(), new EventArgs());
            pictureBox2_Click(new object(), new EventArgs());
        }
        private void menuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "Copy")
            {
                Clipboard.SetText(listView1.SelectedItems[0].Text);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Proxies (*.txt)| *.txt",
                Title = "Choose Proxies File",
                Multiselect = false,
                CheckFileExists = true,
                CheckPathExists = true,
                RestoreDirectory = true
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                PathtextBox1.Text = openFileDialog.FileName;
                MainForm.ProxiesPath = PathtextBox1.Text;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            MainForm.Duration = comboBox1.Text;
        }

        private void SleeptextBox_TextChanged(object sender, EventArgs e)
        {
            MainForm.SleepString = SleeptextBox.Text;
        }

        private void PathtextBox1_DragDrop(object sender, DragEventArgs e)
        {
            string[] file = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (file == null || file.Length==0)

                return;
            if (File.Exists(file[0]) && Path.GetExtension(file[0]).Equals(".txt"))
            {
                PathtextBox1.Text = file[0];
                MainForm.ProxiesPath = file[0];
            }
        }
        private void listview1_DragDrop(object sender, DragEventArgs e)
        {
            string[] file = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (file == null || file.Length == 0)

                return;
            if (File.Exists(file[0]) && Path.GetExtension(file[0]).Equals(".txt"))
            {
                string[] apis = File.ReadAllLines(file[0]);
                if (apis.Count() > 10)
                {
                    MessageBox.Show($"The maximum APIs is 10,You added {apis.Count()} APIs!", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return;
                }
                MainForm.URLS = apis;
                listView1.Items.Clear();
                foreach (string ap in apis)
                {
                    listView1.Items.Add(ap);
                    Thread.Sleep(20);
                    Application.DoEvents();
                }
            }
        }
        private void PathtextBox1_DragEnter(object sender, DragEventArgs e)
        => e.Effect = DragDropEffects.All;

        private void PathtextBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (PathtextBox1.Text.StartsWith("DROP"))
            {
                PathtextBox1.ForeColor = Color.White;
                PathtextBox1.Text = string.Empty;
            }
        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)

        => e.Handled = true;

        private void PathtextBox1_TextChanged(object sender, EventArgs e)
        {
            MainForm.ProxiesPath= PathtextBox1.Text; 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //ctxCopy.Show();
            //return;

            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "API (*.txt)| *.txt",
                Title = "Choose APIs File",
                Multiselect = false,
                CheckFileExists = true,
                CheckPathExists = true,
                RestoreDirectory = true
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string[] apis = File.ReadAllLines(openFileDialog.FileName);
                if (apis.Count() > 10)
                {
                    MessageBox.Show("The maximum APIs is 10 you insert {0} APIs!", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return;
                }
                MainForm.URLS = apis;
                foreach (string ap in apis)
                {
                    listView1.Items.Add(ap);
                    Thread.Sleep(20);
                    Application.DoEvents();
                }
            }
        }

        private void ctxCopy_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (listView1.Items.Count != 0)
                Clipboard.SetText(listView1.SelectedItems[0].Text);
            else
                MessageBox.Show("The text is empty!", "Copy error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (!isRemoveDupChecked)
            {
                isRemoveDupChecked = true;
                pictureBox1.Image = Properties.Resources.Toggle_On;
            }
            else
            {
                isRemoveDupChecked = false;
                pictureBox1.Image = Properties.Resources.Toggle_Off;
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (!isClearChecked)
            {
                isClearChecked = true;
                pictureBox2.Image = Properties.Resources.Toggle_On;
            }
            else
            {
                isClearChecked = false;
                pictureBox2.Image = Properties.Resources.Toggle_Off;
            }
        
        }
    }
    public class MenuColorTable : ProfessionalColorTable
    {
        public MenuColorTable() => UseSystemColors = false;

        public override Color MenuBorder => Color.Black;

        public override Color MenuItemBorder => Color.Black;

        public override Color MenuItemSelected => Color.FromArgb(18, 18, 18);
    }
}
