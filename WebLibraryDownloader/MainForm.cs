using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebLibraryDownloader
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            // $mm TODO: maybe move libtree functions to external code
            Properties.Settings settings = Properties.Settings.Default;
            Left = settings.MainForm_Left;
            Top = settings.MainForm_Top;
            Width = settings.MainForm_Width;
            Height = settings.MainForm_Height;
            cbAutoSave.Checked = settings.AutoSave;
            // $mm TODO: save current dir 

            if (!Directory.Exists(settings.LibHome))    // $mm TODO: exit if no folder is chosen
                ChooseLibFolder();

            if (!File.Exists(settings.ChromePath))
                ChooseChromePath();

            updateLibTree();
        }

        public void SetUrl(string url)
        {
            tbUrl.ReadOnly = false;
            tbUrl.Text = url;
            tbUrl.ReadOnly = true;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings settings = Properties.Settings.Default;

            settings.MainForm_Left = Left;
            settings.MainForm_Top = Top;
            settings.MainForm_Width = Width;
            settings.MainForm_Height = Height;
            settings.AutoSave = cbAutoSave.Checked;

            settings.Save();
        }

        private void btnChooseLibFolder_Click(object sender, EventArgs e)
        {
            ChooseLibFolder();
        }


        private void ChooseLibFolder()
        {
            using (var fbd = new FolderBrowserDialog())
            {
                if (fbd.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                    Properties.Settings.Default.LibHome = fbd.SelectedPath;
            }
        }

        private void ChooseChromePath()
        {
            using (var fbd = new OpenFileDialog())
            {
                if (fbd.ShowDialog() == DialogResult.OK)// $mm TODO: check options
                    Properties.Settings.Default.ChromePath = fbd.FileName;
            }
        }

        private void FillLibTree(string path, TreeNodeCollection nodes)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(path);
            var dirs = dirInfo.GetDirectories().OrderBy(d => d.Name);

            foreach (DirectoryInfo dir in dirs)
            {
                if (!dir.Name.StartsWith("."))   // don't show hidden directories -- $mm obsolete
                {
                    TreeNode e = new TreeNode(dir.Name);
                    nodes.Add(e);
                    FillLibTree(Path.Combine(path, dir.Name), e.Nodes);
                }
            }
        }

        void updateLibTree()
        {
            tvLibTree.BeginUpdate();
            tvLibTree.Nodes.Clear();
            FillLibTree(Properties.Settings.Default.LibHome, tvLibTree.Nodes);
            tvLibTree.EndUpdate();
            if (tvLibTree.Nodes.Count > 0)
                tvLibTree.SelectedNode = tvLibTree.Nodes[0];
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string saveFolder = Path.Combine(Properties.Settings.Default.LibHome, tvLibTree.SelectedNode.FullPath);
            saveUrl(tbUrl.Text, saveFolder);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void saveUrl(string url, string folder)
        {
            btnSave.Enabled = false;
            this.Cursor = Cursors.WaitCursor;

            string cliFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SingleFile-master", "cli", "single-file.bat");
            Directory.SetCurrentDirectory(folder);

            Properties.Settings settings = Properties.Settings.Default;
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.CreateNoWindow = true;
            startInfo.UseShellExecute = false;
            startInfo.FileName = cliFilePath;
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.Arguments = url + " --browser-executable-path \"" + settings.ChromePath + "\"";
            startInfo.Arguments += " --filename-template \"{page-title} ({date-iso}).html\"";

            try
            {
                // Start the process with the info we specified.
                // Call WaitForExit and then the using statement will close.
                using (Process exeProcess = Process.Start(startInfo))
                {
                    exeProcess.WaitForExit();
                }
            }
            catch
            {
                // Log error.
            }

            Close();
        }

        private void btnChooseChromePath_Click(object sender, EventArgs e)
        {
            ChooseChromePath();
        }
    }
}
