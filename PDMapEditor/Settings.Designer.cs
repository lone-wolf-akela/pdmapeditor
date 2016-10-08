namespace PDMapEditor
{
    partial class Settings
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
            this.groupDataPaths = new System.Windows.Forms.GroupBox();
            this.labelDataPathsInfo = new System.Windows.Forms.Label();
            this.buttonRemoveDataPath = new System.Windows.Forms.Button();
            this.buttonAddDataPath = new System.Windows.Forms.Button();
            this.listDataPaths = new System.Windows.Forms.ListBox();
            this.addDataPathDialog = new System.Windows.Forms.OpenFileDialog();
            this.groupView = new System.Windows.Forms.GroupBox();
            this.checkDisplayCubemaps = new System.Windows.Forms.CheckBox();
            this.groupDataPaths.SuspendLayout();
            this.groupView.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupDataPaths
            // 
            this.groupDataPaths.AutoSize = true;
            this.groupDataPaths.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupDataPaths.Controls.Add(this.labelDataPathsInfo);
            this.groupDataPaths.Controls.Add(this.buttonRemoveDataPath);
            this.groupDataPaths.Controls.Add(this.buttonAddDataPath);
            this.groupDataPaths.Controls.Add(this.listDataPaths);
            this.groupDataPaths.Location = new System.Drawing.Point(12, 63);
            this.groupDataPaths.Name = "groupDataPaths";
            this.groupDataPaths.Size = new System.Drawing.Size(329, 188);
            this.groupDataPaths.TabIndex = 20;
            this.groupDataPaths.TabStop = false;
            this.groupDataPaths.Text = "Data paths";
            // 
            // labelDataPathsInfo
            // 
            this.labelDataPathsInfo.Location = new System.Drawing.Point(6, 146);
            this.labelDataPathsInfo.Name = "labelDataPathsInfo";
            this.labelDataPathsInfo.Size = new System.Drawing.Size(317, 26);
            this.labelDataPathsInfo.TabIndex = 25;
            this.labelDataPathsInfo.Text = "The order of the paths matter. Files in lower paths will overwrite files in the p" +
    "aths above them.";
            // 
            // buttonRemoveDataPath
            // 
            this.buttonRemoveDataPath.Location = new System.Drawing.Point(168, 120);
            this.buttonRemoveDataPath.Name = "buttonRemoveDataPath";
            this.buttonRemoveDataPath.Size = new System.Drawing.Size(155, 23);
            this.buttonRemoveDataPath.TabIndex = 24;
            this.buttonRemoveDataPath.Text = "Remove";
            this.buttonRemoveDataPath.UseVisualStyleBackColor = true;
            this.buttonRemoveDataPath.Click += new System.EventHandler(this.buttonRemoveDataPath_Click);
            // 
            // buttonAddDataPath
            // 
            this.buttonAddDataPath.Location = new System.Drawing.Point(6, 120);
            this.buttonAddDataPath.Name = "buttonAddDataPath";
            this.buttonAddDataPath.Size = new System.Drawing.Size(156, 23);
            this.buttonAddDataPath.TabIndex = 23;
            this.buttonAddDataPath.Text = "Add";
            this.buttonAddDataPath.UseVisualStyleBackColor = true;
            this.buttonAddDataPath.Click += new System.EventHandler(this.buttonAddDataPath_Click);
            // 
            // listDataPaths
            // 
            this.listDataPaths.FormattingEnabled = true;
            this.listDataPaths.HorizontalScrollbar = true;
            this.listDataPaths.Location = new System.Drawing.Point(6, 19);
            this.listDataPaths.Name = "listDataPaths";
            this.listDataPaths.Size = new System.Drawing.Size(317, 95);
            this.listDataPaths.TabIndex = 22;
            // 
            // addDataPathDialog
            // 
            this.addDataPathDialog.FileName = "keeper.txt";
            this.addDataPathDialog.Filter = "Data roots|keeper.txt";
            this.addDataPathDialog.Title = "Select keeper.txt in data root folder";
            // 
            // groupView
            // 
            this.groupView.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupView.Controls.Add(this.checkDisplayCubemaps);
            this.groupView.Location = new System.Drawing.Point(12, 12);
            this.groupView.Name = "groupView";
            this.groupView.Size = new System.Drawing.Size(329, 45);
            this.groupView.TabIndex = 21;
            this.groupView.TabStop = false;
            this.groupView.Text = "View";
            // 
            // checkDisplayCubemaps
            // 
            this.checkDisplayCubemaps.AutoSize = true;
            this.checkDisplayCubemaps.Location = new System.Drawing.Point(6, 19);
            this.checkDisplayCubemaps.Name = "checkDisplayCubemaps";
            this.checkDisplayCubemaps.Size = new System.Drawing.Size(112, 17);
            this.checkDisplayCubemaps.TabIndex = 0;
            this.checkDisplayCubemaps.Text = "Display cubemaps";
            this.checkDisplayCubemaps.UseVisualStyleBackColor = true;
            this.checkDisplayCubemaps.CheckedChanged += new System.EventHandler(this.checkDisplayCubemaps_CheckedChanged);
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(446, 447);
            this.Controls.Add(this.groupView);
            this.Controls.Add(this.groupDataPaths);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Settings";
            this.Text = "Settings";
            this.TopMost = true;
            this.groupDataPaths.ResumeLayout(false);
            this.groupView.ResumeLayout(false);
            this.groupView.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupDataPaths;
        private System.Windows.Forms.Label labelDataPathsInfo;
        private System.Windows.Forms.Button buttonRemoveDataPath;
        private System.Windows.Forms.Button buttonAddDataPath;
        private System.Windows.Forms.ListBox listDataPaths;
        private System.Windows.Forms.OpenFileDialog addDataPathDialog;
        private System.Windows.Forms.GroupBox groupView;
        private System.Windows.Forms.CheckBox checkDisplayCubemaps;
    }
}