// Copyright © 2010-2015 The CefSharp Authors. All rights reserved.
//
// Use of this source code is governed by a BSD-style license that can be found in the LICENSE file.

using CefSharp.WinForms;
using System;
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

        public MainForm()
        {
            InitializeComponent();

            mBrowser = new ChromiumWebBrowser("about:blank") { Dock = DockStyle.Fill };
            horizSplitter.Panel2.Controls.Add(mBrowser);

            Properties.Settings settings = Properties.Settings.Default;
            Left = settings.MainForm_Left;
            Top = settings.MainForm_Top;
            Width = settings.MainForm_Width;
            Height = settings.MainForm_Height;
            horizSplitter.SplitterDistance = settings.HSplitterDistance;
            vertSplitter.SplitterDistance = settings.VSplitterDistance;
            WindowState = settings.IsMaximized ? FormWindowState.Maximized : FormWindowState.Normal;

            int[] colWidths = settings.ColWidths.Split(',').Select(s => Int32.Parse(s)).ToArray();
            mArticles = new ArticleList(settings.SortByColumn, settings.ReverseSort, lvArticles, fsWatcher);
            mArticles.InitializeColWidths(colWidths);

            if (!Directory.Exists(settings.LibHome))
                ChooseLibFolder();
            
            updateLibTree();
            fsWatcher.Path = settings.LibHome;
        }

        private void ExitMenuItemClick(object sender, EventArgs e)
        {
            Close();
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
            mArticles.FillArticles(Path.Combine(Properties.Settings.Default.LibHome, tvLibTree.SelectedNode.FullPath));
        }

        private void lvArticles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvArticles.SelectedIndices.Count > 0)
            {
                var idx = lvArticles.SelectedIndices[0];
                var path = mArticles.GetFullPath(idx);
                var url = "file:///" + path.Replace('\\', '/');
                mBrowser.Load(url);
            }
        }

        private void lvArticles_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            mArticles.HandleColumnClick(e.Column);
        }

        private void btnFavorite_Click(object sender, EventArgs e)
        {
            mArticles.ToggleFavorite(lvArticles.SelectedIndices[0]);
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            mArticles.ToggleRead(lvArticles.SelectedIndices[0]);
        }

        private void lvArticles_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Label))
                mArticles.RenameItem(e.Item, e.Label);
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            mBrowser.Dispose();
            Cef.Shutdown();
        }

        private void lvArticles_KeyDown(object sender, KeyEventArgs e)
        {
            KeyEvent k = new KeyEvent();
            if(e.Shift)
                k.Modifiers = CefEventFlags.ShiftDown; 
            
            k.WindowsKeyCode = e.KeyValue;
            k.FocusOnEditableField = true;
            k.IsSystemKey = false;
            k.Type = KeyEventType.Char;
            mBrowser.GetBrowser().GetHost().SendKeyEvent(k);
        }

        private void fsWatcher_Created(object sender, FileSystemEventArgs e)
        {
            updateLibTree();
        }

        private void fsWatcher_Deleted(object sender, FileSystemEventArgs e)
        {
            updateLibTree();
        }

        private void fsWatcher_Renamed(object sender, RenamedEventArgs e)
        {
            updateLibTree();
        }
    }
}
