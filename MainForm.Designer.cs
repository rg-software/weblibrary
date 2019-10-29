﻿namespace WebLibrary
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.toolStripContainer = new System.Windows.Forms.ToolStripContainer();
            this.vertSplitter = new System.Windows.Forms.SplitContainer();
            this.tvLibTree = new System.Windows.Forms.TreeView();
            this.horizSplitter = new System.Windows.Forms.SplitContainer();
            this.lvArticles = new System.Windows.Forms.ListView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.backButton = new System.Windows.Forms.ToolStripButton();
            this.forwardButton = new System.Windows.Forms.ToolStripButton();
            this.urlTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.goButton = new System.Windows.Forms.ToolStripButton();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miChooseLibFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripContainer.ContentPanel.SuspendLayout();
            this.toolStripContainer.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.vertSplitter)).BeginInit();
            this.vertSplitter.Panel1.SuspendLayout();
            this.vertSplitter.Panel2.SuspendLayout();
            this.vertSplitter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.horizSplitter)).BeginInit();
            this.horizSplitter.Panel1.SuspendLayout();
            this.horizSplitter.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripContainer
            // 
            // 
            // toolStripContainer.ContentPanel
            // 
            this.toolStripContainer.ContentPanel.Controls.Add(this.vertSplitter);
            this.toolStripContainer.ContentPanel.Margin = new System.Windows.Forms.Padding(4);
            this.toolStripContainer.ContentPanel.Size = new System.Drawing.Size(973, 548);
            this.toolStripContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer.LeftToolStripPanelVisible = false;
            this.toolStripContainer.Location = new System.Drawing.Point(0, 28);
            this.toolStripContainer.Margin = new System.Windows.Forms.Padding(4);
            this.toolStripContainer.Name = "toolStripContainer";
            this.toolStripContainer.RightToolStripPanelVisible = false;
            this.toolStripContainer.Size = new System.Drawing.Size(973, 575);
            this.toolStripContainer.TabIndex = 0;
            this.toolStripContainer.Text = "toolStripContainer1";
            // 
            // toolStripContainer.TopToolStripPanel
            // 
            this.toolStripContainer.TopToolStripPanel.Controls.Add(this.toolStrip1);
            // 
            // vertSplitter
            // 
            this.vertSplitter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.vertSplitter.Location = new System.Drawing.Point(0, 0);
            this.vertSplitter.Name = "vertSplitter";
            // 
            // vertSplitter.Panel1
            // 
            this.vertSplitter.Panel1.Controls.Add(this.tvLibTree);
            // 
            // vertSplitter.Panel2
            // 
            this.vertSplitter.Panel2.Controls.Add(this.horizSplitter);
            this.vertSplitter.Size = new System.Drawing.Size(973, 548);
            this.vertSplitter.SplitterDistance = 324;
            this.vertSplitter.TabIndex = 0;
            // 
            // tvLibTree
            // 
            this.tvLibTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvLibTree.Location = new System.Drawing.Point(0, 0);
            this.tvLibTree.Name = "tvLibTree";
            this.tvLibTree.Size = new System.Drawing.Size(324, 548);
            this.tvLibTree.TabIndex = 0;
            this.tvLibTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvLibTree_AfterSelect);
            // 
            // horizSplitter
            // 
            this.horizSplitter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.horizSplitter.Location = new System.Drawing.Point(0, 0);
            this.horizSplitter.Name = "horizSplitter";
            this.horizSplitter.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // horizSplitter.Panel1
            // 
            this.horizSplitter.Panel1.Controls.Add(this.lvArticles);
            this.horizSplitter.Size = new System.Drawing.Size(645, 548);
            this.horizSplitter.SplitterDistance = 215;
            this.horizSplitter.TabIndex = 0;
            // 
            // lvArticles
            // 
            this.lvArticles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvArticles.HideSelection = false;
            this.lvArticles.LabelEdit = true;
            this.lvArticles.Location = new System.Drawing.Point(0, 0);
            this.lvArticles.Name = "lvArticles";
            this.lvArticles.Size = new System.Drawing.Size(645, 215);
            this.lvArticles.TabIndex = 0;
            this.lvArticles.UseCompatibleStateImageBehavior = false;
            this.lvArticles.View = System.Windows.Forms.View.Details;
            this.lvArticles.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvArticles_ColumnClick);
            this.lvArticles.SelectedIndexChanged += new System.EventHandler(this.lvArticles_SelectedIndexChanged);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.backButton,
            this.forwardButton,
            this.urlTextBox,
            this.goButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(0);
            this.toolStrip1.Size = new System.Drawing.Size(973, 27);
            this.toolStrip1.Stretch = true;
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Layout += new System.Windows.Forms.LayoutEventHandler(this.HandleToolStripLayout);
            // 
            // backButton
            // 
            this.backButton.Enabled = false;
            this.backButton.Image = global::WebLibrary.Properties.Resources.nav_left_green;
            this.backButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(64, 24);
            this.backButton.Text = "Back";
            this.backButton.Click += new System.EventHandler(this.BackButtonClick);
            // 
            // forwardButton
            // 
            this.forwardButton.Enabled = false;
            this.forwardButton.Image = global::WebLibrary.Properties.Resources.nav_right_green;
            this.forwardButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.forwardButton.Name = "forwardButton";
            this.forwardButton.Size = new System.Drawing.Size(87, 24);
            this.forwardButton.Text = "Forward";
            this.forwardButton.Click += new System.EventHandler(this.ForwardButtonClick);
            // 
            // urlTextBox
            // 
            this.urlTextBox.AutoSize = false;
            this.urlTextBox.Name = "urlTextBox";
            this.urlTextBox.Size = new System.Drawing.Size(500, 25);
            this.urlTextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.UrlTextBoxKeyUp);
            // 
            // goButton
            // 
            this.goButton.Image = global::WebLibrary.Properties.Resources.nav_plain_green;
            this.goButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.goButton.Name = "goButton";
            this.goButton.Size = new System.Drawing.Size(52, 24);
            this.goButton.Text = "Go";
            this.goButton.Click += new System.EventHandler(this.GoButtonClick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(973, 28);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miChooseLibFolder,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(44, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // miChooseLibFolder
            // 
            this.miChooseLibFolder.Name = "miChooseLibFolder";
            this.miChooseLibFolder.Size = new System.Drawing.Size(228, 26);
            this.miChooseLibFolder.Text = "Choose Library Folder";
            this.miChooseLibFolder.Click += new System.EventHandler(this.miChooseLibFolder_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(228, 26);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitMenuItemClick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(973, 603);
            this.Controls.Add(this.toolStripContainer);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainForm";
            this.Text = "WebLibrary";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.toolStripContainer.ContentPanel.ResumeLayout(false);
            this.toolStripContainer.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer.TopToolStripPanel.PerformLayout();
            this.toolStripContainer.ResumeLayout(false);
            this.toolStripContainer.PerformLayout();
            this.vertSplitter.Panel1.ResumeLayout(false);
            this.vertSplitter.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.vertSplitter)).EndInit();
            this.vertSplitter.ResumeLayout(false);
            this.horizSplitter.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.horizSplitter)).EndInit();
            this.horizSplitter.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripContainer toolStripContainer;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton backButton;
        private System.Windows.Forms.ToolStripButton forwardButton;
        private System.Windows.Forms.ToolStripTextBox urlTextBox;
        private System.Windows.Forms.ToolStripButton goButton;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem miChooseLibFolder;
        private System.Windows.Forms.SplitContainer vertSplitter;
        private System.Windows.Forms.SplitContainer horizSplitter;
        private System.Windows.Forms.TreeView tvLibTree;
        private System.Windows.Forms.ListView lvArticles;
    }
}