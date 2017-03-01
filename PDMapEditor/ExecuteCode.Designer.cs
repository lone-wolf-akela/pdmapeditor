namespace PDMapEditor
{
    partial class ExecuteCode
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExecuteCode));
            this.buttonExecute = new System.Windows.Forms.Button();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.boxCode = new ScintillaNET.Scintilla();
            this.boxErrors = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonExecute
            // 
            this.buttonExecute.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonExecute.Location = new System.Drawing.Point(12, 584);
            this.buttonExecute.Name = "buttonExecute";
            this.buttonExecute.Size = new System.Drawing.Size(758, 23);
            this.buttonExecute.TabIndex = 1;
            this.buttonExecute.Text = "Execute";
            this.buttonExecute.UseVisualStyleBackColor = true;
            this.buttonExecute.Click += new System.EventHandler(this.buttonExecute_Click);
            // 
            // splitContainer
            // 
            this.splitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer.Location = new System.Drawing.Point(0, -1);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.boxCode);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.boxErrors);
            this.splitContainer.Size = new System.Drawing.Size(782, 575);
            this.splitContainer.SplitterDistance = 483;
            this.splitContainer.TabIndex = 4;
            // 
            // boxCode
            // 
            this.boxCode.CaretLineBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.boxCode.CaretLineVisible = true;
            this.boxCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.boxCode.EdgeMode = ScintillaNET.EdgeMode.Line;
            this.boxCode.Lexer = ScintillaNET.Lexer.Lua;
            this.boxCode.Location = new System.Drawing.Point(0, 0);
            this.boxCode.Margins.Left = 6;
            this.boxCode.Name = "boxCode";
            this.boxCode.Size = new System.Drawing.Size(782, 483);
            this.boxCode.TabIndex = 4;
            this.boxCode.TabWidth = 6;
            this.boxCode.UseTabs = true;
            // 
            // boxErrors
            // 
            this.boxErrors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.boxErrors.Location = new System.Drawing.Point(0, 0);
            this.boxErrors.Multiline = true;
            this.boxErrors.Name = "boxErrors";
            this.boxErrors.ReadOnly = true;
            this.boxErrors.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.boxErrors.Size = new System.Drawing.Size(782, 88);
            this.boxErrors.TabIndex = 3;
            // 
            // ExecuteCode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(782, 619);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.buttonExecute);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ExecuteCode";
            this.Text = "Execute code";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ExecuteCode_FormClosing);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            this.splitContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button buttonExecute;
        private System.Windows.Forms.SplitContainer splitContainer;
        private ScintillaNET.Scintilla boxCode;
        private System.Windows.Forms.TextBox boxErrors;
    }
}