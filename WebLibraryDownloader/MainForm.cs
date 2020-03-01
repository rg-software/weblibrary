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

            cbAutoSave.CheckedChanged -= cbAutoSave_CheckedChanged;
            cbAutoSave.Checked = settings.AutoSave;
            cbAutoSave.CheckedChanged += cbAutoSave_CheckedChanged;

            while (!Directory.Exists(settings.LibHome))
            {
                MessageBox.Show("Please select a valid library folder", "WebLibraryDownloader", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ChooseLibFolder();
            }

            while (!File.Exists(settings.ChromePath))
            {
                MessageBox.Show("Please select a valid path to a Chrome-compatible browser", "WebLibraryDownloader", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ChooseChromePath();
            }

            updateLibTree();
        }

        public void SetUrl(string url)
        {
            tbUrl.ReadOnly = false;
            tbUrl.Text = url;
            tbUrl.ReadOnly = true;
            btnSave.Enabled = true;
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
                fbd.Description = "Choose Library Folder";
                if (fbd.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                    Properties.Settings.Default.LibHome = fbd.SelectedPath;
            }
        }

        private void ChooseChromePath()
        {
            using (var fbd = new OpenFileDialog())
            {
                if (fbd.ShowDialog() == DialogResult.OK)
                    Properties.Settings.Default.ChromePath = fbd.FileName;
            }
        }

        private TreeNode FillLibTree(string path, TreeNodeCollection nodes)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(path);
            var dirs = dirInfo.GetDirectories().OrderBy(d => d.Name);
            TreeNode result = null;

            foreach (DirectoryInfo dir in dirs)
            {
                if (!dir.Name.StartsWith("."))   // don't show hidden directories -- $mm obsolete
                {
                    TreeNode e = new TreeNode(dir.Name);
                    nodes.Add(e);
                    TreeNode chResult = FillLibTree(Path.Combine(path, dir.Name), e.Nodes);

                    string lsp = Properties.Settings.Default.LastSavePath;
                    if (Path.Combine(Properties.Settings.Default.LibHome, e.FullPath) == lsp)
                        result = e;
                    if (chResult != null && Path.Combine(Properties.Settings.Default.LibHome, chResult.FullPath) == lsp)
                        result = chResult;
                }
            }

            return result;
        }

        void updateLibTree()
        {
            tvLibTree.BeginUpdate();
            tvLibTree.Nodes.Clear();
            TreeNode selectedNode = FillLibTree(Properties.Settings.Default.LibHome, tvLibTree.Nodes);
            tvLibTree.EndUpdate();
            
            if (tvLibTree.Nodes.Count > 0)
                tvLibTree.SelectedNode = selectedNode ?? tvLibTree.Nodes[0];
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string saveFolder = Path.Combine(Properties.Settings.Default.LibHome, tvLibTree.SelectedNode.FullPath);
            Properties.Settings.Default.LastSavePath = saveFolder;
            saveUrl(tbUrl.Text, saveFolder);
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
            catch(Exception)
            {
                // $mm TODO Log error.
            }

            Close();
        }

        private void btnChooseChromePath_Click(object sender, EventArgs e)
        {
            ChooseChromePath();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.BeginInvoke((MethodInvoker)delegate {
                if (cbAutoSave.Checked && tbUrl.Text.Length > 0 && tvLibTree.SelectedNode != null)
                    btnSave.PerformClick();
            });
        }

        private void cbAutoSave_CheckedChanged(object sender, EventArgs e)
        {
            if(cbAutoSave.Checked)
                MessageBox.Show("Note: to uncheck this box later, run WebLibraryDownloader without arguments", "WebLibraryDownloader", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
