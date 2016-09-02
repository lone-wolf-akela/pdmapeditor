namespace PDMapEditor
{
    partial class Updater
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
            this.labelDownloading = new System.Windows.Forms.Label();
            this.downloadProgress = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // labelDownloading
            // 
            this.labelDownloading.AutoSize = true;
            this.labelDownloading.Location = new System.Drawing.Point(13, 13);
            this.labelDownloading.Name = "labelDownloading";
            this.labelDownloading.Size = new System.Drawing.Size(69, 13);
            this.labelDownloading.TabIndex = 0;
            this.labelDownloading.Text = "Downloading";
            // 
            // downloadProgress
            // 
            this.downloadProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.downloadProgress.Location = new System.Drawing.Point(12, 61);
            this.downloadProgress.Name = "downloadProgress";
            this.downloadProgress.Size = new System.Drawing.Size(426, 23);
            this.downloadProgress.Step = 1;
            this.downloadProgress.TabIndex = 1;
            // 
            // Updater
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(450, 96);
            this.Controls.Add(this.downloadProgress);
            this.Controls.Add(this.labelDownloading);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Updater";
            this.Text = "Downloading update...";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelDownloading;
        private System.Windows.Forms.ProgressBar downloadProgress;
    }
}