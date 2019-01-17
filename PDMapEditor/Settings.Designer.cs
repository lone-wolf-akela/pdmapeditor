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
            this.sliderFadeBackground = new System.Windows.Forms.TrackBar();
            this.labelFadeBackground = new System.Windows.Forms.Label();
            this.groupGrid = new System.Windows.Forms.GroupBox();
            this.radioPolarGrid = new System.Windows.Forms.RadioButton();
            this.radioRectangularGrid = new System.Windows.Forms.RadioButton();
            this.checkVSync = new System.Windows.Forms.CheckBox();
            this.labelFSAASamples = new System.Windows.Forms.Label();
            this.comboFSAASamples = new System.Windows.Forms.ComboBox();
            this.checkCheckForUpdates = new System.Windows.Forms.CheckBox();
            this.groupUpdates = new System.Windows.Forms.GroupBox();
            this.groupDataPaths.SuspendLayout();
            this.groupView.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sliderFadeBackground)).BeginInit();
            this.groupGrid.SuspendLayout();
            this.groupUpdates.SuspendLayout();
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
            this.groupDataPaths.Location = new System.Drawing.Point(12, 213);
            this.groupDataPaths.Name = "groupDataPaths";
            this.groupDataPaths.Size = new System.Drawing.Size(350, 188);
            this.groupDataPaths.TabIndex = 20;
            this.groupDataPaths.TabStop = false;
            this.groupDataPaths.Text = "Data paths";
            // 
            // labelDataPathsInfo
            // 
            this.labelDataPathsInfo.Location = new System.Drawing.Point(6, 146);
            this.labelDataPathsInfo.Name = "labelDataPathsInfo";
            this.labelDataPathsInfo.Size = new System.Drawing.Size(338, 26);
            this.labelDataPathsInfo.TabIndex = 25;
            this.labelDataPathsInfo.Text = "The order of the paths matter. Files in lower paths will overwrite files in the p" +
    "aths above them.";
            // 
            // buttonRemoveDataPath
            // 
            this.buttonRemoveDataPath.Location = new System.Drawing.Point(179, 120);
            this.buttonRemoveDataPath.Name = "buttonRemoveDataPath";
            this.buttonRemoveDataPath.Size = new System.Drawing.Size(165, 23);
            this.buttonRemoveDataPath.TabIndex = 24;
            this.buttonRemoveDataPath.Text = "Remove";
            this.buttonRemoveDataPath.UseVisualStyleBackColor = true;
            this.buttonRemoveDataPath.Click += new System.EventHandler(this.buttonRemoveDataPath_Click);
            // 
            // buttonAddDataPath
            // 
            this.buttonAddDataPath.Location = new System.Drawing.Point(6, 120);
            this.buttonAddDataPath.Name = "buttonAddDataPath";
            this.buttonAddDataPath.Size = new System.Drawing.Size(167, 23);
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
            this.listDataPaths.Size = new System.Drawing.Size(338, 95);
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
            this.groupView.AutoSize = true;
            this.groupView.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupView.Controls.Add(this.labelFSAASamples);
            this.groupView.Controls.Add(this.comboFSAASamples);
            this.groupView.Controls.Add(this.checkVSync);
            this.groupView.Controls.Add(this.sliderFadeBackground);
            this.groupView.Controls.Add(this.labelFadeBackground);
            this.groupView.Location = new System.Drawing.Point(12, 12);
            this.groupView.Name = "groupView";
            this.groupView.Size = new System.Drawing.Size(350, 111);
            this.groupView.TabIndex = 21;
            this.groupView.TabStop = false;
            this.groupView.Text = "View";
            // 
            // sliderFadeBackground
            // 
            this.sliderFadeBackground.AutoSize = false;
            this.sliderFadeBackground.Location = new System.Drawing.Point(103, 69);
            this.sliderFadeBackground.Maximum = 100;
            this.sliderFadeBackground.Name = "sliderFadeBackground";
            this.sliderFadeBackground.Size = new System.Drawing.Size(241, 23);
            this.sliderFadeBackground.TabIndex = 1;
            this.sliderFadeBackground.TickStyle = System.Windows.Forms.TickStyle.None;
            this.sliderFadeBackground.Value = 50;
            this.sliderFadeBackground.Scroll += new System.EventHandler(this.sliderFadeBackground_Scroll);
            // 
            // labelFadeBackground
            // 
            this.labelFadeBackground.AutoSize = true;
            this.labelFadeBackground.Location = new System.Drawing.Point(6, 72);
            this.labelFadeBackground.Name = "labelFadeBackground";
            this.labelFadeBackground.Size = new System.Drawing.Size(91, 13);
            this.labelFadeBackground.TabIndex = 0;
            this.labelFadeBackground.Text = "Fade background";
            // 
            // groupGrid
            // 
            this.groupGrid.AutoSize = true;
            this.groupGrid.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupGrid.Controls.Add(this.radioPolarGrid);
            this.groupGrid.Controls.Add(this.radioRectangularGrid);
            this.groupGrid.Location = new System.Drawing.Point(12, 129);
            this.groupGrid.Name = "groupGrid";
            this.groupGrid.Size = new System.Drawing.Size(95, 78);
            this.groupGrid.TabIndex = 22;
            this.groupGrid.TabStop = false;
            this.groupGrid.Text = "Grid";
            // 
            // radioPolarGrid
            // 
            this.radioPolarGrid.AutoSize = true;
            this.radioPolarGrid.Location = new System.Drawing.Point(6, 42);
            this.radioPolarGrid.Name = "radioPolarGrid";
            this.radioPolarGrid.Size = new System.Drawing.Size(49, 17);
            this.radioPolarGrid.TabIndex = 1;
            this.radioPolarGrid.Text = "Polar";
            this.radioPolarGrid.UseVisualStyleBackColor = true;
            this.radioPolarGrid.CheckedChanged += new System.EventHandler(this.radioPolarGrid_CheckedChanged);
            // 
            // radioRectangularGrid
            // 
            this.radioRectangularGrid.AutoSize = true;
            this.radioRectangularGrid.Checked = true;
            this.radioRectangularGrid.Location = new System.Drawing.Point(6, 19);
            this.radioRectangularGrid.Name = "radioRectangularGrid";
            this.radioRectangularGrid.Size = new System.Drawing.Size(83, 17);
            this.radioRectangularGrid.TabIndex = 0;
            this.radioRectangularGrid.TabStop = true;
            this.radioRectangularGrid.Text = "Rectangular";
            this.radioRectangularGrid.UseVisualStyleBackColor = true;
            this.radioRectangularGrid.CheckedChanged += new System.EventHandler(this.radioRectangularGrid_CheckedChanged);
            // 
            // checkVSync
            // 
            this.checkVSync.AutoSize = true;
            this.checkVSync.Checked = true;
            this.checkVSync.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkVSync.Location = new System.Drawing.Point(9, 46);
            this.checkVSync.Name = "checkVSync";
            this.checkVSync.Size = new System.Drawing.Size(172, 17);
            this.checkVSync.TabIndex = 24;
            this.checkVSync.Text = "Enable vertical synchronization";
            this.checkVSync.UseVisualStyleBackColor = true;
            this.checkVSync.CheckedChanged += new System.EventHandler(this.checkVSync_CheckedChanged);
            // 
            // labelFSAASamples
            // 
            this.labelFSAASamples.AutoSize = true;
            this.labelFSAASamples.Location = new System.Drawing.Point(6, 22);
            this.labelFSAASamples.Name = "labelFSAASamples";
            this.labelFSAASamples.Size = new System.Drawing.Size(92, 13);
            this.labelFSAASamples.TabIndex = 26;
            this.labelFSAASamples.Text = "FSAA anti-aliasing";
            // 
            // comboFSAASamples
            // 
            this.comboFSAASamples.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboFSAASamples.FormattingEnabled = true;
            this.comboFSAASamples.Items.AddRange(new object[] {
            "0 samples",
            "2 samples",
            "4 samples"});
            this.comboFSAASamples.Location = new System.Drawing.Point(103, 19);
            this.comboFSAASamples.Name = "comboFSAASamples";
            this.comboFSAASamples.Size = new System.Drawing.Size(241, 21);
            this.comboFSAASamples.TabIndex = 25;
            this.comboFSAASamples.SelectedIndexChanged += new System.EventHandler(this.comboFSAASamples_SelectedIndexChanged);
            // 
            // checkCheckForUpdates
            // 
            this.checkCheckForUpdates.AutoSize = true;
            this.checkCheckForUpdates.Location = new System.Drawing.Point(6, 19);
            this.checkCheckForUpdates.Name = "checkCheckForUpdates";
            this.checkCheckForUpdates.Size = new System.Drawing.Size(151, 17);
            this.checkCheckForUpdates.TabIndex = 0;
            this.checkCheckForUpdates.Text = "Check for updates on start";
            this.checkCheckForUpdates.UseVisualStyleBackColor = true;
            this.checkCheckForUpdates.CheckedChanged += new System.EventHandler(this.checkCheckForUpdates_CheckedChanged);
            // 
            // groupUpdates
            // 
            this.groupUpdates.AutoSize = true;
            this.groupUpdates.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupUpdates.Controls.Add(this.checkCheckForUpdates);
            this.groupUpdates.Location = new System.Drawing.Point(12, 407);
            this.groupUpdates.Name = "groupUpdates";
            this.groupUpdates.Size = new System.Drawing.Size(163, 55);
            this.groupUpdates.TabIndex = 23;
            this.groupUpdates.TabStop = false;
            this.groupUpdates.Text = "Updates";
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(556, 608);
            this.Controls.Add(this.groupUpdates);
            this.Controls.Add(this.groupGrid);
            this.Controls.Add(this.groupView);
            this.Controls.Add(this.groupDataPaths);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Settings";
            this.Text = "Settings";
            this.TopMost = true;
            this.groupDataPaths.ResumeLayout(false);
            this.groupView.ResumeLayout(false);
            this.groupView.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sliderFadeBackground)).EndInit();
            this.groupGrid.ResumeLayout(false);
            this.groupGrid.PerformLayout();
            this.groupUpdates.ResumeLayout(false);
            this.groupUpdates.PerformLayout();
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
        private System.Windows.Forms.Label labelFadeBackground;
        private System.Windows.Forms.TrackBar sliderFadeBackground;
        private System.Windows.Forms.GroupBox groupGrid;
        private System.Windows.Forms.RadioButton radioPolarGrid;
        private System.Windows.Forms.RadioButton radioRectangularGrid;
        private System.Windows.Forms.Label labelFSAASamples;
        private System.Windows.Forms.ComboBox comboFSAASamples;
        private System.Windows.Forms.CheckBox checkVSync;
        private System.Windows.Forms.CheckBox checkCheckForUpdates;
        private System.Windows.Forms.GroupBox groupUpdates;
    }
}