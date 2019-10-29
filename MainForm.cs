// Copyright © 2010-2015 The CefSharp Authors. All rights reserved.
//
// Use of this source code is governed by a BSD-style license that can be found in the LICENSE file.

using CefSharp.WinForms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using CefSharp;

namespace WebLibrary
{
    public partial class MainForm : Form
    {
        private readonly ChromiumWebBrowser mBrowser;
        private ArticleList mArticles;
        //private List<string> mArticles = new List<string>();
        //self.SortByColumn = 0
        //self.ReverseSort = False

        private void FillLibTree(string path, TreeNodeCollection nodes)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(path);
            var dirs = dirInfo.GetDirectories().OrderBy(d => d.Name);
            
            foreach (DirectoryInfo dir in dirs)
            {
                if (!dir.Name.StartsWith("."))   // don't show hidden directories
                {
                    TreeNode e = new TreeNode(dir.Name);
                    nodes.Add(e);
                    FillLibTree(Path.Combine(path, dir.Name), e.Nodes);
                }
            }
        }


        public MainForm()
        {
            InitializeComponent();

            mBrowser = new ChromiumWebBrowser("about:blank") { Dock = DockStyle.Fill };

            horizSplitter.Panel2.Controls.Add(mBrowser);
            mBrowser.IsBrowserInitializedChanged += OnIsBrowserInitializedChanged;
            mBrowser.LoadingStateChanged += OnLoadingStateChanged;
            mBrowser.AddressChanged += OnBrowserAddressChanged;

            Properties.Settings settings = Properties.Settings.Default;
            Left = settings.MainForm_Left;
            Top = settings.MainForm_Top;
            Width = settings.MainForm_Width;
            Height = settings.MainForm_Height;
            horizSplitter.SplitterDistance = settings.HSplitterDistance;
            vertSplitter.SplitterDistance = settings.VSplitterDistance;
            WindowState = settings.IsMaximized ? FormWindowState.Maximized : FormWindowState.Normal;

            int[] colWidths = settings.ColWidths.Split(',').Select(s => Int32.Parse(s)).ToArray();
            mArticles = new ArticleList(settings.SortByColumn, settings.ReverseSort, lvArticles);
            mArticles.InitializeColWidths(colWidths);

            if (!Directory.Exists(settings.LibHome))
                ChooseLibFolder();
            
            tvLibTree.BeginUpdate();
            FillLibTree(settings.LibHome, tvLibTree.Nodes);
            tvLibTree.EndUpdate();
            if (tvLibTree.Nodes.Count > 0)
                tvLibTree.SelectedNode = tvLibTree.Nodes[0];
        }

        private void OnIsBrowserInitializedChanged(object sender, EventArgs e)
        {
            var b = ((ChromiumWebBrowser)sender);
            this.InvokeOnUiThreadIfRequired(() => b.Focus());
        }

/*        private void OnBrowserConsoleMessage(object sender, ConsoleMessageEventArgs args)
        {
            DisplayOutput(string.Format("Line: {0}, Source: {1}, Message: {2}", args.Line, args.Source, args.Message));
        }

        private void OnBrowserStatusMessage(object sender, StatusMessageEventArgs args)
        {
            //$mm this.InvokeOnUiThreadIfRequired(() => statusLabel.Text = args.Value);
        }
        */

        private void OnLoadingStateChanged(object sender, LoadingStateChangedEventArgs args)
        {
            SetCanGoBack(args.CanGoBack);
            SetCanGoForward(args.CanGoForward);

            this.InvokeOnUiThreadIfRequired(() => SetIsLoading(!args.CanReload));
        }

        /*private void OnBrowserTitleChanged(object sender, TitleChangedEventArgs args)
        {
            this.InvokeOnUiThreadIfRequired(() => Text = args.Title);
        }*/

        private void OnBrowserAddressChanged(object sender, AddressChangedEventArgs args)
        {
            this.InvokeOnUiThreadIfRequired(() => urlTextBox.Text = args.Address);
        }

        private void SetCanGoBack(bool canGoBack)
        {
            this.InvokeOnUiThreadIfRequired(() => backButton.Enabled = canGoBack);
        }

        private void SetCanGoForward(bool canGoForward)
        {
            this.InvokeOnUiThreadIfRequired(() => forwardButton.Enabled = canGoForward);
        }

        private void SetIsLoading(bool isLoading)
        {
            goButton.Text = isLoading ?
                "Stop" :
                "Go";
            goButton.Image = isLoading ?
                Properties.Resources.nav_plain_red :
                Properties.Resources.nav_plain_green;

            HandleToolStripLayout();
        }

        //public void DisplayOutput(string output)
        //{
            // $mm this.InvokeOnUiThreadIfRequired(() => outputLabel.Text = output);
        //}

        private void HandleToolStripLayout(object sender, LayoutEventArgs e)
        {
            HandleToolStripLayout();
        }

        private void HandleToolStripLayout()
        {
            var width = toolStrip1.Width;
            foreach (ToolStripItem item in toolStrip1.Items)
            {
                if (item != urlTextBox)
                {
                    width -= item.Width - item.Margin.Horizontal;
                }
            }
            urlTextBox.Width = Math.Max(0, width - urlTextBox.Margin.Horizontal - 18);
        }

        private void ExitMenuItemClick(object sender, EventArgs e)
        {
            mBrowser.Dispose();
            Cef.Shutdown();
            Close();
        }

        private void GoButtonClick(object sender, EventArgs e)
        {
            LoadUrl(urlTextBox.Text);
        }

        private void BackButtonClick(object sender, EventArgs e)
        {
            mBrowser.Back();
        }

        private void ForwardButtonClick(object sender, EventArgs e)
        {
            mBrowser.Forward();
        }

        private void UrlTextBoxKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
            {
                return;
            }

            LoadUrl(urlTextBox.Text);
        }

        private void LoadUrl(string url)
        {
            if (Uri.IsWellFormedUriString(url, UriKind.RelativeOrAbsolute))
            {
                mBrowser.Load(url);
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings settings = Properties.Settings.Default;

            settings.MainForm_Left = Left;
            settings.MainForm_Top = Top;
            settings.MainForm_Width = Width;
            settings.MainForm_Height = Height;
            settings.HSplitterDistance = horizSplitter.SplitterDistance;
            settings.VSplitterDistance = vertSplitter.SplitterDistance;
            settings.IsMaximized = (WindowState == FormWindowState.Maximized);

            int[] colWidths = new int[lvArticles.Columns.Count];
            for (int i = 0; i < colWidths.Length; ++i)
                colWidths[i] = lvArticles.Columns[i].Width;

            settings.ColWidths = String.Join(",", colWidths.Select(i => i.ToString()).ToArray());
            settings.SortByColumn = mArticles.SortByColumn;
            settings.ReverseSort = mArticles.ReverseSort;
            settings.Save();
        }

        private void miChooseLibFolder_Click(object sender, EventArgs e)
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


        private void tvLibTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            mArticles.FillArticles(Path.Combine(Properties.Settings.Default.LibHome, tvLibTree.SelectedNode.Text));
        }

        private void lvArticles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvArticles.SelectedItems.Count > 0)
            {
                var idx = lvArticles.SelectedIndices[0];
                var path = Path.Combine(Properties.Settings.Default.LibHome, tvLibTree.SelectedNode.Text, mArticles.FileName(idx));
                var url = "file:///" + path.Replace('\\', '/');
                mBrowser.Load(url);
            }
        }

        private void lvArticles_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            mArticles.HandleColumnClick(e.Column);
        }
    }
}
