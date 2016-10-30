namespace PDMapEditor
{
    partial class Main
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.buttonNewMap = new System.Windows.Forms.ToolStripButton();
            this.buttonOpenMap = new System.Windows.Forms.ToolStripButton();
            this.buttonSaveMap = new System.Windows.Forms.ToolStripButton();
            this.buttonSettings = new System.Windows.Forms.ToolStripButton();
            this.buttonHotkeys = new System.Windows.Forms.ToolStripButton();
            this.buttonAbout = new System.Windows.Forms.ToolStripButton();
            this.openMapDialog = new System.Windows.Forms.OpenFileDialog();
            this.splitPropertiesAndViewportProblems = new System.Windows.Forms.SplitContainer();
            this.tabControlLeft = new System.Windows.Forms.TabControl();
            this.tabMap = new System.Windows.Forms.TabPage();
            this.groupMusic = new System.Windows.Forms.GroupBox();
            this.buttonPlayBattle = new System.Windows.Forms.Button();
            this.buttonPlayDefault = new System.Windows.Forms.Button();
            this.labelMusicBattle = new System.Windows.Forms.Label();
            this.boxMusicBattle = new System.Windows.Forms.TextBox();
            this.labelMusicDefault = new System.Windows.Forms.Label();
            this.boxMusicDefault = new System.Windows.Forms.TextBox();
            this.groupSensorsManager = new System.Windows.Forms.GroupBox();
            this.labelSensorsManagerCameraMax = new System.Windows.Forms.Label();
            this.numericSensorsManagerCameraMax = new System.Windows.Forms.NumericUpDown();
            this.labelSensorsManagerCameraMin = new System.Windows.Forms.Label();
            this.numericSensorsManagerCameraMin = new System.Windows.Forms.NumericUpDown();
            this.groupGeneral = new System.Windows.Forms.GroupBox();
            this.labelDescription = new System.Windows.Forms.Label();
            this.boxDescription = new System.Windows.Forms.TextBox();
            this.comboMaxPlayers = new System.Windows.Forms.ComboBox();
            this.labelShadowAlpha = new System.Windows.Forms.Label();
            this.labelMaxPlayers = new System.Windows.Forms.Label();
            this.sliderShadowAlpha = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonShadowColor = new System.Windows.Forms.Button();
            this.comboBackground = new System.Windows.Forms.ComboBox();
            this.labelBackground = new System.Windows.Forms.Label();
            this.sliderGlareIntensity = new System.Windows.Forms.TrackBar();
            this.labelGlareIntensity = new System.Windows.Forms.Label();
            this.groupFog = new System.Windows.Forms.GroupBox();
            this.checkFogDisplay = new System.Windows.Forms.CheckBox();
            this.sliderFogAlpha = new System.Windows.Forms.TrackBar();
            this.labelFogAlpha = new System.Windows.Forms.Label();
            this.labelFogDensity = new System.Windows.Forms.Label();
            this.sliderFogDensity = new System.Windows.Forms.TrackBar();
            this.labelFogType = new System.Windows.Forms.Label();
            this.comboFogType = new System.Windows.Forms.ComboBox();
            this.labelFogColor = new System.Windows.Forms.Label();
            this.buttonFogColor = new System.Windows.Forms.Button();
            this.labelFogEnd = new System.Windows.Forms.Label();
            this.numericFogEnd = new System.Windows.Forms.NumericUpDown();
            this.labelFogStart = new System.Windows.Forms.Label();
            this.numericFogStart = new System.Windows.Forms.NumericUpDown();
            this.checkFogActive = new System.Windows.Forms.CheckBox();
            this.groupMapDimensions = new System.Windows.Forms.GroupBox();
            this.labelMapDimensionsZ = new System.Windows.Forms.Label();
            this.numericMapDimensionsZ = new System.Windows.Forms.NumericUpDown();
            this.labelMapDimensionsY = new System.Windows.Forms.Label();
            this.numericMapDimensionsY = new System.Windows.Forms.NumericUpDown();
            this.labelMapDimensionsX = new System.Windows.Forms.Label();
            this.numericMapDimensionsX = new System.Windows.Forms.NumericUpDown();
            this.tabSelection = new System.Windows.Forms.TabPage();
            this.propertySelection = new System.Windows.Forms.PropertyGrid();
            this.tabCreate = new System.Windows.Forms.TabPage();
            this.propertyCreate = new System.Windows.Forms.PropertyGrid();
            this.labelCreationType = new System.Windows.Forms.Label();
            this.comboCreationType = new System.Windows.Forms.ComboBox();
            this.splitViewportAndProblems = new System.Windows.Forms.SplitContainer();
            this.gridProblems = new System.Windows.Forms.DataGridView();
            this.columnProblems = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buttonProblems = new System.Windows.Forms.Button();
            this.comboPerspectiveOrtho = new System.Windows.Forms.ComboBox();
            this.labelFPS = new System.Windows.Forms.Label();
            this.saveMapDialog = new System.Windows.Forms.SaveFileDialog();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.comboGizmoMode = new System.Windows.Forms.ComboBox();
            this.toolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitPropertiesAndViewportProblems)).BeginInit();
            this.splitPropertiesAndViewportProblems.Panel1.SuspendLayout();
            this.splitPropertiesAndViewportProblems.Panel2.SuspendLayout();
            this.splitPropertiesAndViewportProblems.SuspendLayout();
            this.tabControlLeft.SuspendLayout();
            this.tabMap.SuspendLayout();
            this.groupMusic.SuspendLayout();
            this.groupSensorsManager.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericSensorsManagerCameraMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericSensorsManagerCameraMin)).BeginInit();
            this.groupGeneral.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sliderShadowAlpha)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sliderGlareIntensity)).BeginInit();
            this.groupFog.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sliderFogAlpha)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sliderFogDensity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericFogEnd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericFogStart)).BeginInit();
            this.groupMapDimensions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericMapDimensionsZ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericMapDimensionsY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericMapDimensionsX)).BeginInit();
            this.tabSelection.SuspendLayout();
            this.tabCreate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitViewportAndProblems)).BeginInit();
            this.splitViewportAndProblems.Panel2.SuspendLayout();
            this.splitViewportAndProblems.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridProblems)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip
            // 
            this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buttonNewMap,
            this.buttonOpenMap,
            this.buttonSaveMap,
            this.buttonSettings,
            this.buttonHotkeys,
            this.buttonAbout});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(1102, 25);
            this.toolStrip.TabIndex = 0;
            this.toolStrip.Text = "toolStrip1";
            // 
            // buttonNewMap
            // 
            this.buttonNewMap.Image = ((System.Drawing.Image)(resources.GetObject("buttonNewMap.Image")));
            this.buttonNewMap.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonNewMap.Name = "buttonNewMap";
            this.buttonNewMap.Size = new System.Drawing.Size(51, 22);
            this.buttonNewMap.Text = "New";
            this.buttonNewMap.Click += new System.EventHandler(this.buttonNewMap_Click);
            // 
            // buttonOpenMap
            // 
            this.buttonOpenMap.Image = ((System.Drawing.Image)(resources.GetObject("buttonOpenMap.Image")));
            this.buttonOpenMap.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonOpenMap.Name = "buttonOpenMap";
            this.buttonOpenMap.Size = new System.Drawing.Size(56, 22);
            this.buttonOpenMap.Text = "Open";
            this.buttonOpenMap.Click += new System.EventHandler(this.buttonOpenMap_Click);
            // 
            // buttonSaveMap
            // 
            this.buttonSaveMap.Image = ((System.Drawing.Image)(resources.GetObject("buttonSaveMap.Image")));
            this.buttonSaveMap.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonSaveMap.Name = "buttonSaveMap";
            this.buttonSaveMap.Size = new System.Drawing.Size(51, 22);
            this.buttonSaveMap.Text = "Save";
            this.buttonSaveMap.Click += new System.EventHandler(this.buttonSaveMap_Click);
            // 
            // buttonSettings
            // 
            this.buttonSettings.Image = ((System.Drawing.Image)(resources.GetObject("buttonSettings.Image")));
            this.buttonSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonSettings.Name = "buttonSettings";
            this.buttonSettings.Size = new System.Drawing.Size(69, 22);
            this.buttonSettings.Text = "Settings";
            this.buttonSettings.Click += new System.EventHandler(this.buttonSettings_Click);
            // 
            // buttonHotkeys
            // 
            this.buttonHotkeys.Image = ((System.Drawing.Image)(resources.GetObject("buttonHotkeys.Image")));
            this.buttonHotkeys.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonHotkeys.Name = "buttonHotkeys";
            this.buttonHotkeys.Size = new System.Drawing.Size(70, 22);
            this.buttonHotkeys.Text = "Hotkeys";
            this.buttonHotkeys.Click += new System.EventHandler(this.buttonHotkeys_Click);
            // 
            // buttonAbout
            // 
            this.buttonAbout.Image = ((System.Drawing.Image)(resources.GetObject("buttonAbout.Image")));
            this.buttonAbout.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonAbout.Name = "buttonAbout";
            this.buttonAbout.Size = new System.Drawing.Size(60, 22);
            this.buttonAbout.Text = "About";
            this.buttonAbout.Click += new System.EventHandler(this.buttonAbout_Click);
            // 
            // openMapDialog
            // 
            this.openMapDialog.DefaultExt = "level";
            this.openMapDialog.Filter = "Level-files|*.level|All files|*.*";
            this.openMapDialog.Title = "Open lua level file...";
            // 
            // splitPropertiesAndViewportProblems
            // 
            this.splitPropertiesAndViewportProblems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitPropertiesAndViewportProblems.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitPropertiesAndViewportProblems.Location = new System.Drawing.Point(0, 25);
            this.splitPropertiesAndViewportProblems.Name = "splitPropertiesAndViewportProblems";
            // 
            // splitPropertiesAndViewportProblems.Panel1
            // 
            this.splitPropertiesAndViewportProblems.Panel1.Controls.Add(this.tabControlLeft);
            // 
            // splitPropertiesAndViewportProblems.Panel2
            // 
            this.splitPropertiesAndViewportProblems.Panel2.Controls.Add(this.splitViewportAndProblems);
            this.splitPropertiesAndViewportProblems.Size = new System.Drawing.Size(1102, 708);
            this.splitPropertiesAndViewportProblems.SplitterDistance = 262;
            this.splitPropertiesAndViewportProblems.TabIndex = 1;
            // 
            // tabControlLeft
            // 
            this.tabControlLeft.Controls.Add(this.tabMap);
            this.tabControlLeft.Controls.Add(this.tabSelection);
            this.tabControlLeft.Controls.Add(this.tabCreate);
            this.tabControlLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlLeft.HotTrack = true;
            this.tabControlLeft.Location = new System.Drawing.Point(0, 0);
            this.tabControlLeft.Name = "tabControlLeft";
            this.tabControlLeft.SelectedIndex = 0;
            this.tabControlLeft.Size = new System.Drawing.Size(262, 708);
            this.tabControlLeft.TabIndex = 0;
            // 
            // tabMap
            // 
            this.tabMap.Controls.Add(this.groupMusic);
            this.tabMap.Controls.Add(this.groupSensorsManager);
            this.tabMap.Controls.Add(this.groupGeneral);
            this.tabMap.Controls.Add(this.groupFog);
            this.tabMap.Controls.Add(this.groupMapDimensions);
            this.tabMap.Location = new System.Drawing.Point(4, 22);
            this.tabMap.Name = "tabMap";
            this.tabMap.Padding = new System.Windows.Forms.Padding(3);
            this.tabMap.Size = new System.Drawing.Size(254, 682);
            this.tabMap.TabIndex = 0;
            this.tabMap.Text = "Map";
            this.tabMap.ToolTipText = "You can set general map settings here.";
            this.tabMap.UseVisualStyleBackColor = true;
            // 
            // groupMusic
            // 
            this.groupMusic.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupMusic.Controls.Add(this.buttonPlayBattle);
            this.groupMusic.Controls.Add(this.buttonPlayDefault);
            this.groupMusic.Controls.Add(this.labelMusicBattle);
            this.groupMusic.Controls.Add(this.boxMusicBattle);
            this.groupMusic.Controls.Add(this.labelMusicDefault);
            this.groupMusic.Controls.Add(this.boxMusicDefault);
            this.groupMusic.Location = new System.Drawing.Point(3, 506);
            this.groupMusic.Name = "groupMusic";
            this.groupMusic.Size = new System.Drawing.Size(248, 101);
            this.groupMusic.TabIndex = 9;
            this.groupMusic.TabStop = false;
            this.groupMusic.Text = "Music";
            // 
            // buttonPlayBattle
            // 
            this.buttonPlayBattle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonPlayBattle.Location = new System.Drawing.Point(144, 71);
            this.buttonPlayBattle.Name = "buttonPlayBattle";
            this.buttonPlayBattle.Size = new System.Drawing.Size(101, 23);
            this.buttonPlayBattle.TabIndex = 5;
            this.buttonPlayBattle.Text = "Play battle";
            this.buttonPlayBattle.UseVisualStyleBackColor = true;
            this.buttonPlayBattle.Click += new System.EventHandler(this.buttonPlayBattle_Click);
            // 
            // buttonPlayDefault
            // 
            this.buttonPlayDefault.Location = new System.Drawing.Point(6, 71);
            this.buttonPlayDefault.Name = "buttonPlayDefault";
            this.buttonPlayDefault.Size = new System.Drawing.Size(98, 23);
            this.buttonPlayDefault.TabIndex = 4;
            this.buttonPlayDefault.Text = "Play default";
            this.buttonPlayDefault.UseVisualStyleBackColor = true;
            this.buttonPlayDefault.Click += new System.EventHandler(this.buttonPlayDefault_Click);
            // 
            // labelMusicBattle
            // 
            this.labelMusicBattle.AutoSize = true;
            this.labelMusicBattle.Location = new System.Drawing.Point(6, 48);
            this.labelMusicBattle.Name = "labelMusicBattle";
            this.labelMusicBattle.Size = new System.Drawing.Size(37, 13);
            this.labelMusicBattle.TabIndex = 3;
            this.labelMusicBattle.Text = "Battle:";
            // 
            // boxMusicBattle
            // 
            this.boxMusicBattle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.boxMusicBattle.Location = new System.Drawing.Point(51, 45);
            this.boxMusicBattle.Name = "boxMusicBattle";
            this.boxMusicBattle.Size = new System.Drawing.Size(191, 20);
            this.boxMusicBattle.TabIndex = 2;
            this.boxMusicBattle.TextChanged += new System.EventHandler(this.boxMusicBattle_TextChanged);
            // 
            // labelMusicDefault
            // 
            this.labelMusicDefault.AutoSize = true;
            this.labelMusicDefault.Location = new System.Drawing.Point(6, 22);
            this.labelMusicDefault.Name = "labelMusicDefault";
            this.labelMusicDefault.Size = new System.Drawing.Size(44, 13);
            this.labelMusicDefault.TabIndex = 1;
            this.labelMusicDefault.Text = "Default:";
            // 
            // boxMusicDefault
            // 
            this.boxMusicDefault.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.boxMusicDefault.Location = new System.Drawing.Point(51, 19);
            this.boxMusicDefault.Name = "boxMusicDefault";
            this.boxMusicDefault.Size = new System.Drawing.Size(191, 20);
            this.boxMusicDefault.TabIndex = 0;
            this.boxMusicDefault.TextChanged += new System.EventHandler(this.boxMusicDefault_TextChanged);
            // 
            // groupSensorsManager
            // 
            this.groupSensorsManager.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupSensorsManager.Controls.Add(this.labelSensorsManagerCameraMax);
            this.groupSensorsManager.Controls.Add(this.numericSensorsManagerCameraMax);
            this.groupSensorsManager.Controls.Add(this.labelSensorsManagerCameraMin);
            this.groupSensorsManager.Controls.Add(this.numericSensorsManagerCameraMin);
            this.groupSensorsManager.Location = new System.Drawing.Point(3, 613);
            this.groupSensorsManager.Name = "groupSensorsManager";
            this.groupSensorsManager.Size = new System.Drawing.Size(248, 74);
            this.groupSensorsManager.TabIndex = 8;
            this.groupSensorsManager.TabStop = false;
            this.groupSensorsManager.Text = "Sensors manager camera distances";
            // 
            // labelSensorsManagerCameraMax
            // 
            this.labelSensorsManagerCameraMax.AutoSize = true;
            this.labelSensorsManagerCameraMax.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSensorsManagerCameraMax.Location = new System.Drawing.Point(6, 47);
            this.labelSensorsManagerCameraMax.Name = "labelSensorsManagerCameraMax";
            this.labelSensorsManagerCameraMax.Size = new System.Drawing.Size(30, 13);
            this.labelSensorsManagerCameraMax.TabIndex = 8;
            this.labelSensorsManagerCameraMax.Text = "Max:";
            // 
            // numericSensorsManagerCameraMax
            // 
            this.numericSensorsManagerCameraMax.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numericSensorsManagerCameraMax.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericSensorsManagerCameraMax.Location = new System.Drawing.Point(51, 45);
            this.numericSensorsManagerCameraMax.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericSensorsManagerCameraMax.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericSensorsManagerCameraMax.Name = "numericSensorsManagerCameraMax";
            this.numericSensorsManagerCameraMax.Size = new System.Drawing.Size(191, 20);
            this.numericSensorsManagerCameraMax.TabIndex = 7;
            this.numericSensorsManagerCameraMax.ThousandsSeparator = true;
            this.numericSensorsManagerCameraMax.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericSensorsManagerCameraMax.ValueChanged += new System.EventHandler(this.numericSensorsManagerCameraMax_ValueChanged);
            // 
            // labelSensorsManagerCameraMin
            // 
            this.labelSensorsManagerCameraMin.AutoSize = true;
            this.labelSensorsManagerCameraMin.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSensorsManagerCameraMin.Location = new System.Drawing.Point(6, 21);
            this.labelSensorsManagerCameraMin.Name = "labelSensorsManagerCameraMin";
            this.labelSensorsManagerCameraMin.Size = new System.Drawing.Size(27, 13);
            this.labelSensorsManagerCameraMin.TabIndex = 6;
            this.labelSensorsManagerCameraMin.Text = "Min:";
            // 
            // numericSensorsManagerCameraMin
            // 
            this.numericSensorsManagerCameraMin.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numericSensorsManagerCameraMin.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericSensorsManagerCameraMin.Location = new System.Drawing.Point(51, 19);
            this.numericSensorsManagerCameraMin.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numericSensorsManagerCameraMin.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericSensorsManagerCameraMin.Name = "numericSensorsManagerCameraMin";
            this.numericSensorsManagerCameraMin.Size = new System.Drawing.Size(191, 20);
            this.numericSensorsManagerCameraMin.TabIndex = 5;
            this.numericSensorsManagerCameraMin.ThousandsSeparator = true;
            this.numericSensorsManagerCameraMin.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericSensorsManagerCameraMin.ValueChanged += new System.EventHandler(this.numericSensorsManagerCameraMin_ValueChanged);
            // 
            // groupGeneral
            // 
            this.groupGeneral.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupGeneral.Controls.Add(this.labelDescription);
            this.groupGeneral.Controls.Add(this.boxDescription);
            this.groupGeneral.Controls.Add(this.comboMaxPlayers);
            this.groupGeneral.Controls.Add(this.labelShadowAlpha);
            this.groupGeneral.Controls.Add(this.labelMaxPlayers);
            this.groupGeneral.Controls.Add(this.sliderShadowAlpha);
            this.groupGeneral.Controls.Add(this.label1);
            this.groupGeneral.Controls.Add(this.buttonShadowColor);
            this.groupGeneral.Controls.Add(this.comboBackground);
            this.groupGeneral.Controls.Add(this.labelBackground);
            this.groupGeneral.Controls.Add(this.sliderGlareIntensity);
            this.groupGeneral.Controls.Add(this.labelGlareIntensity);
            this.groupGeneral.Location = new System.Drawing.Point(3, 3);
            this.groupGeneral.Name = "groupGeneral";
            this.groupGeneral.Size = new System.Drawing.Size(248, 178);
            this.groupGeneral.TabIndex = 7;
            this.groupGeneral.TabStop = false;
            this.groupGeneral.Text = "General";
            // 
            // labelDescription
            // 
            this.labelDescription.AutoSize = true;
            this.labelDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDescription.Location = new System.Drawing.Point(6, 43);
            this.labelDescription.Name = "labelDescription";
            this.labelDescription.Size = new System.Drawing.Size(63, 13);
            this.labelDescription.TabIndex = 23;
            this.labelDescription.Text = "Description:";
            // 
            // boxDescription
            // 
            this.boxDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.boxDescription.Location = new System.Drawing.Point(90, 40);
            this.boxDescription.Name = "boxDescription";
            this.boxDescription.Size = new System.Drawing.Size(152, 20);
            this.boxDescription.TabIndex = 22;
            this.boxDescription.TextChanged += new System.EventHandler(this.boxDescription_TextChanged);
            // 
            // comboMaxPlayers
            // 
            this.comboMaxPlayers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboMaxPlayers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboMaxPlayers.FormattingEnabled = true;
            this.comboMaxPlayers.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8"});
            this.comboMaxPlayers.Location = new System.Drawing.Point(90, 66);
            this.comboMaxPlayers.Name = "comboMaxPlayers";
            this.comboMaxPlayers.Size = new System.Drawing.Size(152, 21);
            this.comboMaxPlayers.TabIndex = 21;
            this.comboMaxPlayers.SelectedIndexChanged += new System.EventHandler(this.comboMaxPlayers_SelectedIndexChanged);
            // 
            // labelShadowAlpha
            // 
            this.labelShadowAlpha.AutoSize = true;
            this.labelShadowAlpha.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelShadowAlpha.Location = new System.Drawing.Point(6, 153);
            this.labelShadowAlpha.Name = "labelShadowAlpha";
            this.labelShadowAlpha.Size = new System.Drawing.Size(78, 13);
            this.labelShadowAlpha.TabIndex = 19;
            this.labelShadowAlpha.Text = "Shadow alpha:";
            // 
            // labelMaxPlayers
            // 
            this.labelMaxPlayers.AutoSize = true;
            this.labelMaxPlayers.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMaxPlayers.Location = new System.Drawing.Point(6, 69);
            this.labelMaxPlayers.Name = "labelMaxPlayers";
            this.labelMaxPlayers.Size = new System.Drawing.Size(66, 13);
            this.labelMaxPlayers.TabIndex = 20;
            this.labelMaxPlayers.Text = "Max players:";
            // 
            // sliderShadowAlpha
            // 
            this.sliderShadowAlpha.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sliderShadowAlpha.AutoSize = false;
            this.sliderShadowAlpha.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.sliderShadowAlpha.Location = new System.Drawing.Point(83, 148);
            this.sliderShadowAlpha.Maximum = 100;
            this.sliderShadowAlpha.Name = "sliderShadowAlpha";
            this.sliderShadowAlpha.Size = new System.Drawing.Size(162, 23);
            this.sliderShadowAlpha.TabIndex = 18;
            this.sliderShadowAlpha.TickFrequency = 5;
            this.sliderShadowAlpha.TickStyle = System.Windows.Forms.TickStyle.None;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 126);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 17;
            this.label1.Text = "Shadow color:";
            // 
            // buttonShadowColor
            // 
            this.buttonShadowColor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonShadowColor.BackColor = System.Drawing.Color.Turquoise;
            this.buttonShadowColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonShadowColor.Location = new System.Drawing.Point(90, 122);
            this.buttonShadowColor.Name = "buttonShadowColor";
            this.buttonShadowColor.Size = new System.Drawing.Size(152, 20);
            this.buttonShadowColor.TabIndex = 16;
            this.buttonShadowColor.UseVisualStyleBackColor = false;
            this.buttonShadowColor.Click += new System.EventHandler(this.buttonShadowColor_Click);
            // 
            // comboBackground
            // 
            this.comboBackground.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBackground.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBackground.FormattingEnabled = true;
            this.comboBackground.Location = new System.Drawing.Point(90, 13);
            this.comboBackground.Name = "comboBackground";
            this.comboBackground.Size = new System.Drawing.Size(152, 21);
            this.comboBackground.TabIndex = 15;
            this.comboBackground.SelectedIndexChanged += new System.EventHandler(this.comboBackground_SelectedIndexChanged);
            // 
            // labelBackground
            // 
            this.labelBackground.AutoSize = true;
            this.labelBackground.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelBackground.Location = new System.Drawing.Point(6, 16);
            this.labelBackground.Name = "labelBackground";
            this.labelBackground.Size = new System.Drawing.Size(68, 13);
            this.labelBackground.TabIndex = 14;
            this.labelBackground.Text = "Background:";
            // 
            // sliderGlareIntensity
            // 
            this.sliderGlareIntensity.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sliderGlareIntensity.AutoSize = false;
            this.sliderGlareIntensity.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.sliderGlareIntensity.Location = new System.Drawing.Point(83, 93);
            this.sliderGlareIntensity.Maximum = 100;
            this.sliderGlareIntensity.Name = "sliderGlareIntensity";
            this.sliderGlareIntensity.Size = new System.Drawing.Size(162, 23);
            this.sliderGlareIntensity.TabIndex = 13;
            this.sliderGlareIntensity.TickFrequency = 5;
            this.sliderGlareIntensity.TickStyle = System.Windows.Forms.TickStyle.None;
            this.sliderGlareIntensity.Scroll += new System.EventHandler(this.sliderGlareIntensity_Scroll);
            // 
            // labelGlareIntensity
            // 
            this.labelGlareIntensity.AutoSize = true;
            this.labelGlareIntensity.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelGlareIntensity.Location = new System.Drawing.Point(6, 96);
            this.labelGlareIntensity.Name = "labelGlareIntensity";
            this.labelGlareIntensity.Size = new System.Drawing.Size(76, 13);
            this.labelGlareIntensity.TabIndex = 12;
            this.labelGlareIntensity.Text = "Glare intensity:";
            // 
            // groupFog
            // 
            this.groupFog.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupFog.Controls.Add(this.checkFogDisplay);
            this.groupFog.Controls.Add(this.sliderFogAlpha);
            this.groupFog.Controls.Add(this.labelFogAlpha);
            this.groupFog.Controls.Add(this.labelFogDensity);
            this.groupFog.Controls.Add(this.sliderFogDensity);
            this.groupFog.Controls.Add(this.labelFogType);
            this.groupFog.Controls.Add(this.comboFogType);
            this.groupFog.Controls.Add(this.labelFogColor);
            this.groupFog.Controls.Add(this.buttonFogColor);
            this.groupFog.Controls.Add(this.labelFogEnd);
            this.groupFog.Controls.Add(this.numericFogEnd);
            this.groupFog.Controls.Add(this.labelFogStart);
            this.groupFog.Controls.Add(this.numericFogStart);
            this.groupFog.Controls.Add(this.checkFogActive);
            this.groupFog.Location = new System.Drawing.Point(3, 293);
            this.groupFog.Name = "groupFog";
            this.groupFog.Size = new System.Drawing.Size(248, 207);
            this.groupFog.TabIndex = 6;
            this.groupFog.TabStop = false;
            this.groupFog.Text = "Fog";
            // 
            // checkFogDisplay
            // 
            this.checkFogDisplay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkFogDisplay.AutoSize = true;
            this.checkFogDisplay.Location = new System.Drawing.Point(182, 19);
            this.checkFogDisplay.Name = "checkFogDisplay";
            this.checkFogDisplay.Size = new System.Drawing.Size(60, 17);
            this.checkFogDisplay.TabIndex = 13;
            this.checkFogDisplay.Text = "Display";
            this.checkFogDisplay.UseVisualStyleBackColor = true;
            this.checkFogDisplay.CheckedChanged += new System.EventHandler(this.checkFogDisplay_CheckedChanged);
            // 
            // sliderFogAlpha
            // 
            this.sliderFogAlpha.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sliderFogAlpha.AutoSize = false;
            this.sliderFogAlpha.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.sliderFogAlpha.Enabled = false;
            this.sliderFogAlpha.Location = new System.Drawing.Point(51, 121);
            this.sliderFogAlpha.Maximum = 100;
            this.sliderFogAlpha.Name = "sliderFogAlpha";
            this.sliderFogAlpha.Size = new System.Drawing.Size(191, 23);
            this.sliderFogAlpha.TabIndex = 12;
            this.sliderFogAlpha.TickFrequency = 5;
            this.sliderFogAlpha.TickStyle = System.Windows.Forms.TickStyle.None;
            this.sliderFogAlpha.Scroll += new System.EventHandler(this.sliderFogAlpha_Scroll);
            // 
            // labelFogAlpha
            // 
            this.labelFogAlpha.AutoSize = true;
            this.labelFogAlpha.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelFogAlpha.Location = new System.Drawing.Point(6, 126);
            this.labelFogAlpha.Name = "labelFogAlpha";
            this.labelFogAlpha.Size = new System.Drawing.Size(37, 13);
            this.labelFogAlpha.TabIndex = 11;
            this.labelFogAlpha.Text = "Alpha:";
            // 
            // labelFogDensity
            // 
            this.labelFogDensity.AutoSize = true;
            this.labelFogDensity.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelFogDensity.Location = new System.Drawing.Point(6, 181);
            this.labelFogDensity.Name = "labelFogDensity";
            this.labelFogDensity.Size = new System.Drawing.Size(45, 13);
            this.labelFogDensity.TabIndex = 10;
            this.labelFogDensity.Text = "Density:";
            // 
            // sliderFogDensity
            // 
            this.sliderFogDensity.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sliderFogDensity.AutoSize = false;
            this.sliderFogDensity.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.sliderFogDensity.Enabled = false;
            this.sliderFogDensity.Location = new System.Drawing.Point(51, 177);
            this.sliderFogDensity.Maximum = 100;
            this.sliderFogDensity.Name = "sliderFogDensity";
            this.sliderFogDensity.Size = new System.Drawing.Size(191, 23);
            this.sliderFogDensity.TabIndex = 9;
            this.sliderFogDensity.TickFrequency = 5;
            this.sliderFogDensity.TickStyle = System.Windows.Forms.TickStyle.None;
            this.sliderFogDensity.Scroll += new System.EventHandler(this.sliderFogDensity_Scroll);
            // 
            // labelFogType
            // 
            this.labelFogType.AutoSize = true;
            this.labelFogType.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelFogType.Location = new System.Drawing.Point(6, 153);
            this.labelFogType.Name = "labelFogType";
            this.labelFogType.Size = new System.Drawing.Size(34, 13);
            this.labelFogType.TabIndex = 8;
            this.labelFogType.Text = "Type:";
            // 
            // comboFogType
            // 
            this.comboFogType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboFogType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboFogType.Enabled = false;
            this.comboFogType.FormattingEnabled = true;
            this.comboFogType.Items.AddRange(new object[] {
            "Linear",
            "Exponential",
            "Exponential 2"});
            this.comboFogType.Location = new System.Drawing.Point(51, 150);
            this.comboFogType.Name = "comboFogType";
            this.comboFogType.Size = new System.Drawing.Size(191, 21);
            this.comboFogType.TabIndex = 7;
            this.comboFogType.SelectedIndexChanged += new System.EventHandler(this.comboFogType_SelectedIndexChanged);
            // 
            // labelFogColor
            // 
            this.labelFogColor.AutoSize = true;
            this.labelFogColor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelFogColor.Location = new System.Drawing.Point(6, 99);
            this.labelFogColor.Name = "labelFogColor";
            this.labelFogColor.Size = new System.Drawing.Size(34, 13);
            this.labelFogColor.TabIndex = 6;
            this.labelFogColor.Text = "Color:";
            // 
            // buttonFogColor
            // 
            this.buttonFogColor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonFogColor.Enabled = false;
            this.buttonFogColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonFogColor.Location = new System.Drawing.Point(51, 95);
            this.buttonFogColor.Name = "buttonFogColor";
            this.buttonFogColor.Size = new System.Drawing.Size(191, 20);
            this.buttonFogColor.TabIndex = 5;
            this.buttonFogColor.UseVisualStyleBackColor = true;
            this.buttonFogColor.Click += new System.EventHandler(this.buttonFogColor_Click);
            // 
            // labelFogEnd
            // 
            this.labelFogEnd.AutoSize = true;
            this.labelFogEnd.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelFogEnd.Location = new System.Drawing.Point(6, 70);
            this.labelFogEnd.Name = "labelFogEnd";
            this.labelFogEnd.Size = new System.Drawing.Size(29, 13);
            this.labelFogEnd.TabIndex = 4;
            this.labelFogEnd.Text = "End:";
            // 
            // numericFogEnd
            // 
            this.numericFogEnd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numericFogEnd.Enabled = false;
            this.numericFogEnd.Increment = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.numericFogEnd.Location = new System.Drawing.Point(51, 68);
            this.numericFogEnd.Maximum = new decimal(new int[] {
            500000,
            0,
            0,
            0});
            this.numericFogEnd.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericFogEnd.Name = "numericFogEnd";
            this.numericFogEnd.Size = new System.Drawing.Size(191, 20);
            this.numericFogEnd.TabIndex = 3;
            this.numericFogEnd.ThousandsSeparator = true;
            this.numericFogEnd.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericFogEnd.ValueChanged += new System.EventHandler(this.numericFogEnd_ValueChanged);
            // 
            // labelFogStart
            // 
            this.labelFogStart.AutoSize = true;
            this.labelFogStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelFogStart.Location = new System.Drawing.Point(6, 44);
            this.labelFogStart.Name = "labelFogStart";
            this.labelFogStart.Size = new System.Drawing.Size(32, 13);
            this.labelFogStart.TabIndex = 2;
            this.labelFogStart.Text = "Start:";
            // 
            // numericFogStart
            // 
            this.numericFogStart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numericFogStart.Enabled = false;
            this.numericFogStart.Increment = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numericFogStart.Location = new System.Drawing.Point(51, 42);
            this.numericFogStart.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericFogStart.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericFogStart.Name = "numericFogStart";
            this.numericFogStart.Size = new System.Drawing.Size(191, 20);
            this.numericFogStart.TabIndex = 1;
            this.numericFogStart.ThousandsSeparator = true;
            this.numericFogStart.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericFogStart.ValueChanged += new System.EventHandler(this.numericFogStart_ValueChanged);
            // 
            // checkFogActive
            // 
            this.checkFogActive.AutoSize = true;
            this.checkFogActive.Location = new System.Drawing.Point(9, 19);
            this.checkFogActive.Name = "checkFogActive";
            this.checkFogActive.Size = new System.Drawing.Size(56, 17);
            this.checkFogActive.TabIndex = 0;
            this.checkFogActive.Text = "Active";
            this.checkFogActive.UseVisualStyleBackColor = true;
            this.checkFogActive.CheckedChanged += new System.EventHandler(this.checkFogActive_CheckedChanged);
            // 
            // groupMapDimensions
            // 
            this.groupMapDimensions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupMapDimensions.Controls.Add(this.labelMapDimensionsZ);
            this.groupMapDimensions.Controls.Add(this.numericMapDimensionsZ);
            this.groupMapDimensions.Controls.Add(this.labelMapDimensionsY);
            this.groupMapDimensions.Controls.Add(this.numericMapDimensionsY);
            this.groupMapDimensions.Controls.Add(this.labelMapDimensionsX);
            this.groupMapDimensions.Controls.Add(this.numericMapDimensionsX);
            this.groupMapDimensions.Location = new System.Drawing.Point(3, 187);
            this.groupMapDimensions.Name = "groupMapDimensions";
            this.groupMapDimensions.Size = new System.Drawing.Size(248, 100);
            this.groupMapDimensions.TabIndex = 0;
            this.groupMapDimensions.TabStop = false;
            this.groupMapDimensions.Text = "Dimensions";
            // 
            // labelMapDimensionsZ
            // 
            this.labelMapDimensionsZ.AutoSize = true;
            this.labelMapDimensionsZ.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMapDimensionsZ.Location = new System.Drawing.Point(6, 73);
            this.labelMapDimensionsZ.Name = "labelMapDimensionsZ";
            this.labelMapDimensionsZ.Size = new System.Drawing.Size(39, 13);
            this.labelMapDimensionsZ.TabIndex = 5;
            this.labelMapDimensionsZ.Text = "Z-Axis:";
            // 
            // numericMapDimensionsZ
            // 
            this.numericMapDimensionsZ.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numericMapDimensionsZ.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericMapDimensionsZ.Location = new System.Drawing.Point(51, 71);
            this.numericMapDimensionsZ.Maximum = new decimal(new int[] {
            500000,
            0,
            0,
            0});
            this.numericMapDimensionsZ.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericMapDimensionsZ.Name = "numericMapDimensionsZ";
            this.numericMapDimensionsZ.Size = new System.Drawing.Size(191, 20);
            this.numericMapDimensionsZ.TabIndex = 4;
            this.numericMapDimensionsZ.ThousandsSeparator = true;
            this.numericMapDimensionsZ.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericMapDimensionsZ.ValueChanged += new System.EventHandler(this.SetMapDimensions);
            // 
            // labelMapDimensionsY
            // 
            this.labelMapDimensionsY.AutoSize = true;
            this.labelMapDimensionsY.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMapDimensionsY.Location = new System.Drawing.Point(6, 47);
            this.labelMapDimensionsY.Name = "labelMapDimensionsY";
            this.labelMapDimensionsY.Size = new System.Drawing.Size(39, 13);
            this.labelMapDimensionsY.TabIndex = 3;
            this.labelMapDimensionsY.Text = "Y-Axis:";
            // 
            // numericMapDimensionsY
            // 
            this.numericMapDimensionsY.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numericMapDimensionsY.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericMapDimensionsY.Location = new System.Drawing.Point(51, 45);
            this.numericMapDimensionsY.Maximum = new decimal(new int[] {
            500000,
            0,
            0,
            0});
            this.numericMapDimensionsY.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericMapDimensionsY.Name = "numericMapDimensionsY";
            this.numericMapDimensionsY.Size = new System.Drawing.Size(191, 20);
            this.numericMapDimensionsY.TabIndex = 2;
            this.numericMapDimensionsY.ThousandsSeparator = true;
            this.numericMapDimensionsY.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericMapDimensionsY.ValueChanged += new System.EventHandler(this.SetMapDimensions);
            // 
            // labelMapDimensionsX
            // 
            this.labelMapDimensionsX.AutoSize = true;
            this.labelMapDimensionsX.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMapDimensionsX.Location = new System.Drawing.Point(6, 21);
            this.labelMapDimensionsX.Name = "labelMapDimensionsX";
            this.labelMapDimensionsX.Size = new System.Drawing.Size(39, 13);
            this.labelMapDimensionsX.TabIndex = 1;
            this.labelMapDimensionsX.Text = "X-Axis:";
            // 
            // numericMapDimensionsX
            // 
            this.numericMapDimensionsX.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numericMapDimensionsX.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericMapDimensionsX.Location = new System.Drawing.Point(51, 19);
            this.numericMapDimensionsX.Maximum = new decimal(new int[] {
            500000,
            0,
            0,
            0});
            this.numericMapDimensionsX.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericMapDimensionsX.Name = "numericMapDimensionsX";
            this.numericMapDimensionsX.Size = new System.Drawing.Size(191, 20);
            this.numericMapDimensionsX.TabIndex = 0;
            this.numericMapDimensionsX.ThousandsSeparator = true;
            this.numericMapDimensionsX.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericMapDimensionsX.ValueChanged += new System.EventHandler(this.SetMapDimensions);
            // 
            // tabSelection
            // 
            this.tabSelection.Controls.Add(this.propertySelection);
            this.tabSelection.Location = new System.Drawing.Point(4, 22);
            this.tabSelection.Name = "tabSelection";
            this.tabSelection.Size = new System.Drawing.Size(254, 682);
            this.tabSelection.TabIndex = 1;
            this.tabSelection.Text = "Selection";
            this.tabSelection.UseVisualStyleBackColor = true;
            // 
            // propertySelection
            // 
            this.propertySelection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertySelection.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.propertySelection.Location = new System.Drawing.Point(0, 0);
            this.propertySelection.Name = "propertySelection";
            this.propertySelection.PropertySort = System.Windows.Forms.PropertySort.Categorized;
            this.propertySelection.Size = new System.Drawing.Size(254, 682);
            this.propertySelection.TabIndex = 32;
            this.propertySelection.ToolbarVisible = false;
            this.propertySelection.UseCompatibleTextRendering = true;
            // 
            // tabCreate
            // 
            this.tabCreate.Controls.Add(this.propertyCreate);
            this.tabCreate.Controls.Add(this.labelCreationType);
            this.tabCreate.Controls.Add(this.comboCreationType);
            this.tabCreate.Location = new System.Drawing.Point(4, 22);
            this.tabCreate.Name = "tabCreate";
            this.tabCreate.Padding = new System.Windows.Forms.Padding(3);
            this.tabCreate.Size = new System.Drawing.Size(254, 682);
            this.tabCreate.TabIndex = 2;
            this.tabCreate.Text = "Create";
            this.tabCreate.UseVisualStyleBackColor = true;
            // 
            // propertyCreate
            // 
            this.propertyCreate.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.propertyCreate.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.propertyCreate.Location = new System.Drawing.Point(6, 33);
            this.propertyCreate.Name = "propertyCreate";
            this.propertyCreate.PropertySort = System.Windows.Forms.PropertySort.Categorized;
            this.propertyCreate.Size = new System.Drawing.Size(242, 643);
            this.propertyCreate.TabIndex = 20;
            this.propertyCreate.ToolbarVisible = false;
            this.propertyCreate.UseCompatibleTextRendering = true;
            // 
            // labelCreationType
            // 
            this.labelCreationType.AutoSize = true;
            this.labelCreationType.Location = new System.Drawing.Point(8, 9);
            this.labelCreationType.Name = "labelCreationType";
            this.labelCreationType.Size = new System.Drawing.Size(41, 13);
            this.labelCreationType.TabIndex = 19;
            this.labelCreationType.Text = "Object:";
            // 
            // comboCreationType
            // 
            this.comboCreationType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboCreationType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboCreationType.FormattingEnabled = true;
            this.comboCreationType.Location = new System.Drawing.Point(55, 6);
            this.comboCreationType.Name = "comboCreationType";
            this.comboCreationType.Size = new System.Drawing.Size(193, 21);
            this.comboCreationType.TabIndex = 18;
            // 
            // splitViewportAndProblems
            // 
            this.splitViewportAndProblems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitViewportAndProblems.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitViewportAndProblems.Location = new System.Drawing.Point(0, 0);
            this.splitViewportAndProblems.Name = "splitViewportAndProblems";
            // 
            // splitViewportAndProblems.Panel2
            // 
            this.splitViewportAndProblems.Panel2.Controls.Add(this.gridProblems);
            this.splitViewportAndProblems.Size = new System.Drawing.Size(836, 708);
            this.splitViewportAndProblems.SplitterDistance = 625;
            this.splitViewportAndProblems.TabIndex = 0;
            // 
            // gridProblems
            // 
            this.gridProblems.AllowUserToAddRows = false;
            this.gridProblems.AllowUserToDeleteRows = false;
            this.gridProblems.AllowUserToResizeColumns = false;
            this.gridProblems.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridProblems.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gridProblems.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridProblems.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.gridProblems.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            this.gridProblems.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.gridProblems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.gridProblems.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnProblems});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridProblems.DefaultCellStyle = dataGridViewCellStyle3;
            this.gridProblems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridProblems.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.gridProblems.Location = new System.Drawing.Point(0, 0);
            this.gridProblems.MultiSelect = false;
            this.gridProblems.Name = "gridProblems";
            this.gridProblems.ReadOnly = true;
            this.gridProblems.RowHeadersVisible = false;
            this.gridProblems.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridProblems.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.gridProblems.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.gridProblems.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridProblems.RowTemplate.Height = 500;
            this.gridProblems.RowTemplate.ReadOnly = true;
            this.gridProblems.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.gridProblems.Size = new System.Drawing.Size(207, 708);
            this.gridProblems.TabIndex = 1;
            this.gridProblems.SelectionChanged += new System.EventHandler(this.gridProblems_SelectionChanged);
            // 
            // columnProblems
            // 
            this.columnProblems.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.columnProblems.DefaultCellStyle = dataGridViewCellStyle2;
            this.columnProblems.HeaderText = "Problems";
            this.columnProblems.Name = "columnProblems";
            this.columnProblems.ReadOnly = true;
            this.columnProblems.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.columnProblems.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // buttonProblems
            // 
            this.buttonProblems.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonProblems.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.buttonProblems.FlatAppearance.BorderSize = 0;
            this.buttonProblems.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonProblems.Image = global::PDMapEditor.Properties.Resources.flagWhite;
            this.buttonProblems.Location = new System.Drawing.Point(1058, 0);
            this.buttonProblems.Name = "buttonProblems";
            this.buttonProblems.Size = new System.Drawing.Size(44, 25);
            this.buttonProblems.TabIndex = 9;
            this.buttonProblems.UseVisualStyleBackColor = false;
            this.buttonProblems.Click += new System.EventHandler(this.buttonProblems_Click);
            // 
            // comboPerspectiveOrtho
            // 
            this.comboPerspectiveOrtho.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboPerspectiveOrtho.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboPerspectiveOrtho.FormattingEnabled = true;
            this.comboPerspectiveOrtho.ItemHeight = 13;
            this.comboPerspectiveOrtho.Items.AddRange(new object[] {
            "Perspective",
            "Orthographic"});
            this.comboPerspectiveOrtho.Location = new System.Drawing.Point(947, 3);
            this.comboPerspectiveOrtho.MaxDropDownItems = 1;
            this.comboPerspectiveOrtho.Name = "comboPerspectiveOrtho";
            this.comboPerspectiveOrtho.Size = new System.Drawing.Size(105, 21);
            this.comboPerspectiveOrtho.TabIndex = 10;
            this.comboPerspectiveOrtho.SelectedIndexChanged += new System.EventHandler(this.comboPerspectiveOrtho_SelectedIndexChanged);
            // 
            // labelFPS
            // 
            this.labelFPS.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelFPS.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(248)))));
            this.labelFPS.Location = new System.Drawing.Point(817, 2);
            this.labelFPS.Name = "labelFPS";
            this.labelFPS.Size = new System.Drawing.Size(124, 23);
            this.labelFPS.TabIndex = 11;
            this.labelFPS.Text = "0 FPS";
            this.labelFPS.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // saveMapDialog
            // 
            this.saveMapDialog.DefaultExt = "level";
            this.saveMapDialog.FileName = "map";
            this.saveMapDialog.Title = "Save lua level file...";
            // 
            // colorDialog
            // 
            this.colorDialog.FullOpen = true;
            // 
            // comboGizmoMode
            // 
            this.comboGizmoMode.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.comboGizmoMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboGizmoMode.FormattingEnabled = true;
            this.comboGizmoMode.ItemHeight = 13;
            this.comboGizmoMode.Location = new System.Drawing.Point(569, 2);
            this.comboGizmoMode.MaxDropDownItems = 1;
            this.comboGizmoMode.Name = "comboGizmoMode";
            this.comboGizmoMode.Size = new System.Drawing.Size(105, 21);
            this.comboGizmoMode.TabIndex = 12;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1102, 733);
            this.Controls.Add(this.comboGizmoMode);
            this.Controls.Add(this.labelFPS);
            this.Controls.Add(this.comboPerspectiveOrtho);
            this.Controls.Add(this.buttonProblems);
            this.Controls.Add(this.splitPropertiesAndViewportProblems);
            this.Controls.Add(this.toolStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Main";
            this.Text = "PayDay\'s Homeworld Remastered Map Editor";
            this.Activated += new System.EventHandler(this.Main_Activated);
            this.Deactivate += new System.EventHandler(this.Main_Deactivate);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.splitPropertiesAndViewportProblems.Panel1.ResumeLayout(false);
            this.splitPropertiesAndViewportProblems.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitPropertiesAndViewportProblems)).EndInit();
            this.splitPropertiesAndViewportProblems.ResumeLayout(false);
            this.tabControlLeft.ResumeLayout(false);
            this.tabMap.ResumeLayout(false);
            this.groupMusic.ResumeLayout(false);
            this.groupMusic.PerformLayout();
            this.groupSensorsManager.ResumeLayout(false);
            this.groupSensorsManager.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericSensorsManagerCameraMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericSensorsManagerCameraMin)).EndInit();
            this.groupGeneral.ResumeLayout(false);
            this.groupGeneral.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sliderShadowAlpha)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sliderGlareIntensity)).EndInit();
            this.groupFog.ResumeLayout(false);
            this.groupFog.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sliderFogAlpha)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sliderFogDensity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericFogEnd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericFogStart)).EndInit();
            this.groupMapDimensions.ResumeLayout(false);
            this.groupMapDimensions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericMapDimensionsZ)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericMapDimensionsY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericMapDimensionsX)).EndInit();
            this.tabSelection.ResumeLayout(false);
            this.tabCreate.ResumeLayout(false);
            this.tabCreate.PerformLayout();
            this.splitViewportAndProblems.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitViewportAndProblems)).EndInit();
            this.splitViewportAndProblems.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridProblems)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton buttonOpenMap;
        private System.Windows.Forms.OpenFileDialog openMapDialog;
        private System.Windows.Forms.ToolStripButton buttonAbout;
        private System.Windows.Forms.ToolStripButton buttonSettings;
        private System.Windows.Forms.SplitContainer splitPropertiesAndViewportProblems;
        private System.Windows.Forms.DataGridView gridProblems;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnProblems;
        public System.Windows.Forms.SplitContainer splitViewportAndProblems;
        private System.Windows.Forms.Button buttonProblems;
        private System.Windows.Forms.ToolStripButton buttonHotkeys;
        private System.Windows.Forms.ComboBox comboPerspectiveOrtho;
        private System.Windows.Forms.Label labelFPS;
        private System.Windows.Forms.ToolStripButton buttonSaveMap;
        private System.Windows.Forms.SaveFileDialog saveMapDialog;
        private System.Windows.Forms.TabPage tabMap;
        private System.Windows.Forms.GroupBox groupMapDimensions;
        private System.Windows.Forms.Label labelMapDimensionsY;
        private System.Windows.Forms.NumericUpDown numericMapDimensionsY;
        private System.Windows.Forms.Label labelMapDimensionsX;
        private System.Windows.Forms.NumericUpDown numericMapDimensionsX;
        private System.Windows.Forms.Label labelMapDimensionsZ;
        private System.Windows.Forms.NumericUpDown numericMapDimensionsZ;
        private System.Windows.Forms.GroupBox groupFog;
        private System.Windows.Forms.CheckBox checkFogActive;
        private System.Windows.Forms.Label labelFogStart;
        private System.Windows.Forms.NumericUpDown numericFogStart;
        private System.Windows.Forms.Label labelFogType;
        private System.Windows.Forms.ComboBox comboFogType;
        private System.Windows.Forms.Label labelFogColor;
        private System.Windows.Forms.Button buttonFogColor;
        private System.Windows.Forms.Label labelFogEnd;
        private System.Windows.Forms.NumericUpDown numericFogEnd;
        private System.Windows.Forms.TrackBar sliderFogDensity;
        private System.Windows.Forms.Label labelFogDensity;
        private System.Windows.Forms.TrackBar sliderFogAlpha;
        private System.Windows.Forms.Label labelFogAlpha;
        private System.Windows.Forms.ToolStripButton buttonNewMap;
        public System.Windows.Forms.TabControl tabControlLeft;
        public System.Windows.Forms.TabPage tabSelection;
        private System.Windows.Forms.CheckBox checkFogDisplay;
        public System.Windows.Forms.ComboBox comboGizmoMode;
        private System.Windows.Forms.GroupBox groupGeneral;
        private System.Windows.Forms.Label labelBackground;
        private System.Windows.Forms.Label labelGlareIntensity;
        public System.Windows.Forms.ComboBox comboBackground;
        public System.Windows.Forms.TrackBar sliderGlareIntensity;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.Button buttonShadowColor;
        private System.Windows.Forms.GroupBox groupSensorsManager;
        private System.Windows.Forms.Label labelSensorsManagerCameraMax;
        public System.Windows.Forms.NumericUpDown numericSensorsManagerCameraMax;
        private System.Windows.Forms.Label labelSensorsManagerCameraMin;
        public System.Windows.Forms.NumericUpDown numericSensorsManagerCameraMin;
        private System.Windows.Forms.Label labelShadowAlpha;
        public System.Windows.Forms.TrackBar sliderShadowAlpha;
        private System.Windows.Forms.GroupBox groupMusic;
        private System.Windows.Forms.Label labelMusicBattle;
        private System.Windows.Forms.Label labelMusicDefault;
        public System.Windows.Forms.TextBox boxMusicBattle;
        public System.Windows.Forms.TextBox boxMusicDefault;
        private System.Windows.Forms.Button buttonPlayDefault;
        private System.Windows.Forms.Button buttonPlayBattle;
        public System.Windows.Forms.ComboBox comboMaxPlayers;
        private System.Windows.Forms.Label labelMaxPlayers;
        private System.Windows.Forms.Label labelDescription;
        public System.Windows.Forms.TextBox boxDescription;
        public System.Windows.Forms.ColorDialog colorDialog;
        private System.Windows.Forms.Label labelCreationType;
        public System.Windows.Forms.ComboBox comboCreationType;
        public System.Windows.Forms.TabPage tabCreate;
        public System.Windows.Forms.PropertyGrid propertyCreate;
        public System.Windows.Forms.PropertyGrid propertySelection;
    }
}

