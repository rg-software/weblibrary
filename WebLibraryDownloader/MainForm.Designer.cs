namespace WebLibraryDownloader
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbUrl = new System.Windows.Forms.TextBox();
            this.tvLibTree = new System.Windows.Forms.TreeView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnChooseChromePath = new System.Windows.Forms.Button();
            this.btnChooseLibFolder = new System.Windows.Forms.Button();
            this.cbAutoSave = new System.Windows.Forms.CheckBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbUrl);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(524, 49);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Save URL";
            // 
            // tbUrl
            // 
            this.tbUrl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbUrl.Location = new System.Drawing.Point(3, 18);
            this.tbUrl.Name = "tbUrl";
            this.tbUrl.ReadOnly = true;
            this.tbUrl.Size = new System.Drawing.Size(518, 22);
            this.tbUrl.TabIndex = 0;
            this.tbUrl.TabStop = false;
            // 
            // tvLibTree
            // 
            this.tvLibTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvLibTree.Location = new System.Drawing.Point(0, 49);
            this.tvLibTree.Name = "tvLibTree";
            this.tvLibTree.Size = new System.Drawing.Size(524, 401);
            this.tvLibTree.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnChooseChromePath);
            this.panel1.Controls.Add(this.btnChooseLibFolder);
            this.panel1.Controls.Add(this.cbAutoSave);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Location = new System.Drawing.Point(12, 14);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(497, 47);
            this.panel1.TabIndex = 7;
            // 
            // btnChooseChromePath
            // 
            this.btnChooseChromePath.Location = new System.Drawing.Point(244, 4);
            this.btnChooseChromePath.Name = "btnChooseChromePath";
            this.btnChooseChromePath.Size = new System.Drawing.Size(156, 32);
            this.btnChooseChromePath.TabIndex = 4;
            this.btnChooseChromePath.Text = "Choose Chrome Path";
            this.btnChooseChromePath.UseVisualStyleBackColor = true;
            this.btnChooseChromePath.Click += new System.EventHandler(this.btnChooseChromePath_Click);
            // 
            // btnChooseLibFolder
            // 
            this.btnChooseLibFolder.Location = new System.Drawing.Point(89, 4);
            this.btnChooseLibFolder.Name = "btnChooseLibFolder";
            this.btnChooseLibFolder.Size = new System.Drawing.Size(149, 32);
            this.btnChooseLibFolder.TabIndex = 3;
            this.btnChooseLibFolder.Text = "Choose Lib Folder";
            this.btnChooseLibFolder.UseVisualStyleBackColor = true;
            this.btnChooseLibFolder.Click += new System.EventHandler(this.btnChooseLibFolder_Click);
            // 
            // cbAutoSave
            // 
            this.cbAutoSave.AutoSize = true;
            this.cbAutoSave.Location = new System.Drawing.Point(406, 10);
            this.cbAutoSave.Name = "cbAutoSave";
            this.cbAutoSave.Size = new System.Drawing.Size(89, 21);
            this.cbAutoSave.TabIndex = 5;
            this.cbAutoSave.Text = "Autosave";
            this.cbAutoSave.UseVisualStyleBackColor = true;
            this.cbAutoSave.CheckedChanged += new System.EventHandler(this.cbAutoSave_CheckedChanged);
            // 
            // btnSave
            // 
            this.btnSave.Enabled = false;
            this.btnSave.Image = global::WebLibraryDownloader.Properties.Resources.save;
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(3, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(80, 33);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Save";
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 377);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(524, 73);
            this.panel2.TabIndex = 8;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(524, 450);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.tvLibTree);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.Text = "WebLibrary Downloader";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tbUrl;
        private System.Windows.Forms.TreeView tvLibTree;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox cbAutoSave;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnChooseLibFolder;
        private System.Windows.Forms.Button btnChooseChromePath;
    }
}

