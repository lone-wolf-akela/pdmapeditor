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
            this.radioRectangularGrid = new System.Windows.Forms.RadioButton();
            this.radioPolarGrid = new System.Windows.Forms.RadioButton();
            this.groupDataPaths.SuspendLayout();
            this.groupView.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sliderFadeBackground)).BeginInit();
            this.groupGrid.SuspendLayout();
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
            this.groupDataPaths.Location = new System.Drawing.Point(12, 132);
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
            this.groupView.Controls.Add(this.sliderFadeBackground);
            this.groupView.Controls.Add(this.labelFadeBackground);
            this.groupView.Location = new System.Drawing.Point(12, 12);
            this.groupView.Name = "groupView";
            this.groupView.Size = new System.Drawing.Size(329, 41);
            this.groupView.TabIndex = 21;
            this.groupView.TabStop = false;
            this.groupView.Text = "View";
            // 
            // sliderFadeBackground
            // 
            this.sliderFadeBackground.AutoSize = false;
            this.sliderFadeBackground.Location = new System.Drawing.Point(103, 13);
            this.sliderFadeBackground.Maximum = 100;
            this.sliderFadeBackground.Name = "sliderFadeBackground";
            this.sliderFadeBackground.Size = new System.Drawing.Size(220, 23);
            this.sliderFadeBackground.TabIndex = 1;
            this.sliderFadeBackground.TickStyle = System.Windows.Forms.TickStyle.None;
            this.sliderFadeBackground.Value = 50;
            this.sliderFadeBackground.Scroll += new System.EventHandler(this.sliderFadeBackground_Scroll);
            // 
            // labelFadeBackground
            // 
            this.labelFadeBackground.AutoSize = true;
            this.labelFadeBackground.Location = new System.Drawing.Point(6, 16);
            this.labelFadeBackground.Name = "labelFadeBackground";
            this.labelFadeBackground.Size = new System.Drawing.Size(91, 13);
            this.labelFadeBackground.TabIndex = 0;
            this.labelFadeBackground.Text = "Fade background";
            // 
            // groupGrid
            // 
            this.groupGrid.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupGrid.Controls.Add(this.radioPolarGrid);
            this.groupGrid.Controls.Add(this.radioRectangularGrid);
            this.groupGrid.Location = new System.Drawing.Point(12, 59);
            this.groupGrid.Name = "groupGrid";
            this.groupGrid.Size = new System.Drawing.Size(329, 67);
            this.groupGrid.TabIndex = 22;
            this.groupGrid.TabStop = false;
            this.groupGrid.Text = "Grid";
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
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(446, 447);
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
    }
}