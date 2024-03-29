﻿using System;
using System.Drawing;
using System.Windows.Forms;
using OpenTK;
using OpenTK.Graphics;
using System.Media;
using System.IO;
using System.Diagnostics;

namespace PDMapEditor
{
    public partial class Main : Form
    {
        public int BUILD = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Build;

        public bool Loaded = false;
        public bool FormActive = false;

        private bool problemsVisible;

        private bool ignoreMapDimensionChange;

        private bool ignoreFogAlphaChange;
        private bool ignoreFogDensityChange;

        private bool playingDefaultMusic;
        private bool playingBattleMusic;

        SoundPlayer player = new SoundPlayer();

        public string LastOpenLocation;
        public string LastSaveLocation;

        public Main()
        {
            InitializeComponent();
        }

        public void glControl_Load(object sender, EventArgs e)
        {
            Log.WriteLine("Initializing...");

            Texture.Init();
            Importer.Init();
            Renderer.Init();

            HWData.ParseDataPaths();

            Background.LoadBackgroundFades();

            Creation.Init();
            Selection.Init();

            LuaMap.SetupInterpreter();

            Application.Idle += glControl_Update;
            Program.DeltaCounter.Start();
            FPSCounter.LabelFPS = labelFPS;
            Loaded = true;

            gridProblems.RowTemplate.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            gridProblems.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            gridProblems.Columns[0].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            this.WindowState = Settings.LastWindowState;
            this.Location = Settings.LastWindowLocation;
            this.Size = Settings.LastWindowSize;

            Map.Clear();
            Program.Camera.ResetCamera();

            if (Updater.CheckForUpdatesOnStart)
                Updater.CheckForUpdates();

            Program.main.UpdateProblems();
            Renderer.InvalidateMeshData();
            Renderer.InvalidateView();
            Renderer.Invalidate();
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Idle -= glControl_Update;
            GraphicsContext.CurrentContext.Dispose();
            Texture.Close();
            Settings.SaveSettings();
            Hotkeys.SaveHotkeys();
            Background.SaveBackgroundFades();
            Log.Close();
        }

        public void glControl_Update(object sender, EventArgs e)
        {
            //For frame-independent stuff
            Program.DeltaCounter.Stop();
            Program.ElapsedSeconds = Program.DeltaCounter.Elapsed.TotalSeconds;
            Program.ElapsedMilliseconds = Program.DeltaCounter.Elapsed.TotalMilliseconds;
            Program.DeltaCounter.Reset();
            Program.DeltaCounter.Start();

            Program.Camera.Update();
            Program.Camera.UpdatePanning();

            Creation.UpdateObjectAtCursor();
            Selection.Update();
        }

        public void glControl_Render(object sender, PaintEventArgs e)
        {
            if (!Loaded)
                return;

            FPSCounter.Update();
            Renderer.Render();
            Renderer.Render2D();
        }

        public void glControl_Resize(object sender, EventArgs e)
        {
            if (!Loaded)
                return;

            Renderer.Resize();
            Renderer.Invalidate();
        }

        public void glControl_MouseEnter(object sender, EventArgs e)
        {
            if (FormActive)
            {
                if (Program.ExecuteCode != null)
                {
                    if (!Program.ExecuteCode.Visible)
                        Program.GLControl.Focus();
                }
                else
                    Program.GLControl.Focus();
            }
        }

        public void glControl_MouseLeave(object sender, EventArgs e)
        {
            //this.Focus();
            ActionKey.LostFocus();
        }

        public void glControl_LostFocus(object sender, EventArgs e)
        {
            ActionKey.LostFocus();
        }

        public void glControl_MouseMove(object sender, MouseEventArgs e)
        {
            int x = e.X;
            int y = Program.GLControl.ClientSize.Height - e.Y;

            Selection.UpdateDragging(x, y);
            Selection.UpdateRectangleSelection(x, y);

            if (FormActive)
            {
                if (Program.ExecuteCode != null)
                {
                    if (!Program.ExecuteCode.Visible)
                        Program.GLControl.Focus();
                }
                else
                    Program.GLControl.Focus();
            }
        }

        public void glControl_MouseDown(object sender, MouseEventArgs e)
        {
            Program.Camera.MouseDown(e);

            //Selecting
            if (e.Button == MouseButtons.Left)
                Selection.LeftMouseDown(e.X, Program.GLControl.ClientSize.Height - e.Y);
        }

        public void glControl_MouseUp(object sender, MouseEventArgs e)
        {
            Program.Camera.MouseUp(e);

            int x = e.X;
            int y = Program.GLControl.ClientSize.Height - e.Y;

            if (e.Button == MouseButtons.Left)
            {
                Selection.LeftMouseUp(x, y);
                Creation.LeftMouseUp(x, y);
            }
        }

        public void glControl_KeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            ActionKey.KeyDown(e);
            Program.Camera.KeyDown();
            Selection.KeyDown();
            SavedAction.KeyDown();
        }

        public void glControl_KeyUp(object sender, KeyEventArgs e)
        {
            ActionKey.KeyUp(e);
        }

        public void Clear()
        {
            listSquadrons.Items.Clear();
            listSOBGroups.Items.Clear();
            comboSquadrons.Items.Clear();

            listSOBGroups_SelectedIndexChanged(this, EventArgs.Empty);
        }

        //---------------------- TOOL STRIP ------------------------//
        #region Tool strip
        private void buttonNewMap_Click(object sender, EventArgs e)
        {
            Map.Clear();
            Program.Camera.ResetCamera();
        }
        private void buttonOpenMap_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(LastOpenLocation))
                openMapDialog.InitialDirectory = LastOpenLocation;

            DialogResult result = openMapDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                LastOpenLocation = Path.GetDirectoryName(openMapDialog.FileName);

                Map.Clear();
                gridProblems.Rows.Clear();
                Problem.Problems.Clear();
                LuaMap.LoadMap(openMapDialog.FileName);

                Map.Path = openMapDialog.FileName;

                Program.Camera.ResetCamera();
                Program.main.UpdateProblems();
                Renderer.InvalidateMeshData();
                Renderer.InvalidateView();
                Renderer.Invalidate();
            }
        }

        private void buttonSaveMap_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(LastSaveLocation))
                saveMapDialog.InitialDirectory = LastSaveLocation;

            DialogResult result = saveMapDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                LastSaveLocation = Path.GetDirectoryName(saveMapDialog.FileName);

                LuaMap.SaveMap(saveMapDialog.FileName);
                Map.Path = saveMapDialog.FileName;
            }
        }

        private void buttonSettings_Click(object sender, EventArgs e)
        {
            if (Program.Settings == null || Program.Settings.IsDisposed)
            {
                Program.Settings = new Settings();
                Program.Settings.Open();
                Program.Settings.Visible = true;
            }
        }

        private void buttonHotkeys_Click(object sender, EventArgs e)
        {
            if (Program.hotkeys == null || Program.hotkeys.IsDisposed)
            {
                Program.hotkeys = new Hotkeys();
                Program.hotkeys.Init();
                Program.hotkeys.Visible = true;
            }
        }

        private void buttonAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this, "PayDay's Homeworld Remastered Map Editor b" + Program.BUILD + "\n\nDeveloped by Christoph (PayDay) Timmermann.\n\nUses\n - OpenTK\n - NLua\n - Assimp\n - Assimp.NET\n - DevIL\n - DevILSharp\n - FSharp\n - Scintilla", "PayDay's Homeworld Remastered Map Editor", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void buttonExecuteCode_Click(object sender, EventArgs e)
        {
            if (Program.ExecuteCode == null || Program.ExecuteCode.IsDisposed)
            {
                Program.ExecuteCode = new ExecuteCode();
                Program.ExecuteCode.Open();
                Program.ExecuteCode.Visible = true;
            }
        }
        #endregion

        //Perspective-Orthographic combobox
        private void comboPerspectiveOrtho_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboPerspectiveOrtho.SelectedIndex == 0)
                Program.Camera.Orthographic = false;
            else
                Program.Camera.Orthographic = true;
        }
        public void UpdatePerspectiveOrthoCombo()
        {
            if (!Program.Camera.Orthographic)
                comboPerspectiveOrtho.SelectedIndex = 0;
            else
                comboPerspectiveOrtho.SelectedIndex = 1;
        }

        //--------------------------------- PROBLEMS TAB ---------------------------------//
        #region Problems tab
        private void gridProblems_SelectionChanged(object sender, EventArgs e)
        {
            gridProblems.ClearSelection();
        }

        private void buttonProblems_Click(object sender, EventArgs e)
        {
            problemsVisible = !problemsVisible;
            splitViewportAndProblems.Panel2Collapsed = !problemsVisible;

            if (problemsVisible)
                buttonProblems.BackColor = Color.FromArgb(255, 178, 178, 178);
            else
                buttonProblems.BackColor = Color.FromArgb(255, 248, 248, 248);
        }

        public void AddProblem(Problem problem)
        {
            DataGridViewRow row = (DataGridViewRow)gridProblems.RowTemplate.Clone();
            row.CreateCells(gridProblems, problem.Description);
            gridProblems.Rows.Add(row);

            if (problem.Type == ProblemTypes.ERROR)
                row.Cells[0].Style.ForeColor = Color.Red;
            else if (problem.Type == ProblemTypes.WARNING)
                row.Cells[0].Style.ForeColor = Color.DarkOrange;
        }

        public void UpdateProblems()
        {
            bool errors = false;
            bool warnings = false;

            foreach (Problem problem in Problem.Problems)
            {
                if (problem.Type == ProblemTypes.ERROR)
                    errors = true;
                else if (problem.Type == ProblemTypes.WARNING)
                    warnings = true;
            }

            if (warnings)
            {
                buttonProblems.Image = this.buttonProblems.Image = global::PDMapEditor.Properties.Resources.flagYellow;
                problemsVisible = true;
            }

            if (errors)
            {
                buttonProblems.Image = this.buttonProblems.Image = global::PDMapEditor.Properties.Resources.flagRed;
                problemsVisible = true;
            }

            if (!warnings && !errors)
            {
                problemsVisible = false;
                buttonProblems.Image = this.buttonProblems.Image = global::PDMapEditor.Properties.Resources.flagWhite;
            }

            if (problemsVisible)
                buttonProblems.BackColor = Color.FromArgb(255, 178, 178, 178);
            else
                buttonProblems.BackColor = Color.FromArgb(255, 248, 248, 248);

            splitViewportAndProblems.Panel2Collapsed = !problemsVisible;
        }
        #endregion

        //--------------------------------- MAP DIMENSIONS ---------------------------------//
        public void UpdateMapDimensions()
        {
            ignoreMapDimensionChange = true;
            numericMapDimensionsX.Value = (decimal)Map.MapDimensions.X;
            numericMapDimensionsY.Value = (decimal)Map.MapDimensions.Y;
            numericMapDimensionsZ.Value = (decimal)Map.MapDimensions.Z;
            ignoreMapDimensionChange = false;
        }

        private void SetMapDimensions(object sender, EventArgs e)
        {
            if(!ignoreMapDimensionChange)
                Map.MapDimensions = new Vector3((float)numericMapDimensionsX.Value, (float)numericMapDimensionsY.Value, (float)numericMapDimensionsZ.Value);
        }

        //--------------------------------- FOG ---------------------------------//
        #region Fog
        public void UpdateFogActive()
        {
            checkFogActive.Checked = Map.FogActive;
        }
        private void checkFogActive_CheckedChanged(object sender, EventArgs e)
        {
            Map.FogActive = checkFogActive.Checked;
            numericFogStart.Enabled = checkFogActive.Checked;
            numericFogEnd.Enabled = checkFogActive.Checked;
            buttonFogColor.Enabled = checkFogActive.Checked;
            sliderFogAlpha.Enabled = checkFogActive.Checked;
            comboFogType.Enabled = checkFogActive.Checked;
            sliderFogDensity.Enabled = checkFogActive.Checked;
            Renderer.Invalidate();
        }

        private void checkFogDisplay_CheckedChanged(object sender, EventArgs e)
        {
            Renderer.DisplayFog = checkFogDisplay.Checked;
            Renderer.Invalidate();
        }

        public void UpdateFogStart()
        {
            numericFogStart.Value = (decimal)Map.FogStart;
        }
        private void numericFogStart_ValueChanged(object sender, EventArgs e)
        {
            Map.FogStart = (float)numericFogStart.Value;
            Renderer.Invalidate();
        }

        public void UpdateFogEnd()
        {
            numericFogEnd.Value = (decimal)Map.FogEnd;
        }
        private void numericFogEnd_ValueChanged(object sender, EventArgs e)
        {
            Map.FogEnd = (float)numericFogEnd.Value;
            Renderer.Invalidate();
        }

        public void UpdateFogColor()
        {
            buttonFogColor.BackColor = Color.FromArgb(255, (int)Math.Round(Map.FogColor.X * 255), (int)Math.Round(Map.FogColor.Y * 255), (int)Math.Round(Map.FogColor.Z * 255));

            if(!ignoreFogAlphaChange)
                sliderFogAlpha.Value = (int)Math.Round(Map.FogColor.W * 100);
        }
        private void buttonFogColor_Click(object sender, EventArgs e)
        {
            colorDialog.Color = buttonFogColor.BackColor;
            DialogResult result = colorDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                buttonFogColor.BackColor = colorDialog.Color;
                Map.FogColor = new Vector4((float)colorDialog.Color.R / 255, (float)colorDialog.Color.G / 255, (float)colorDialog.Color.B / 255, Map.FogColor.W);
                Renderer.Invalidate();
            }
        }
        private void sliderFogAlpha_Scroll(object sender, EventArgs e)
        {
            ignoreFogAlphaChange = true;
            Map.FogColor = new Vector4(Map.FogColor.X, Map.FogColor.Y, Map.FogColor.Z, (float)sliderFogAlpha.Value / 100);
            ignoreFogAlphaChange = false;
            Renderer.Invalidate();
        }

        public void UpdateFogType()
        {
            int index = 0;
            switch (Map.FogType)
            {
                case "linear":
                    index = 0;
                    break;
                case "exp":
                    index = 1;
                    break;
                case "exp2":
                    index = 2;
                    break;
            }
            comboFogType.SelectedIndex = index;
        }
        private void comboFogType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string type = "linear";
            switch (comboFogType.SelectedIndex)
            {
                case 0:
                    type = "linear";
                    break;
                case 1:
                    type = "exp";
                    break;
                case 2:
                    type = "exp2";
                    break;
            }
            Map.FogType = type;
            Renderer.Invalidate();
        }

        public void UpdateFogDensity()
        {
            if(!ignoreFogDensityChange)
                sliderFogDensity.Value = (int)Math.Round(Map.FogDensity * 100);
        }
        private void sliderFogDensity_Scroll(object sender, EventArgs e)
        {
            ignoreFogDensityChange = true;
            Map.FogDensity = (float)sliderFogDensity.Value / 100;
            ignoreFogDensityChange = false;
            Renderer.Invalidate();
        }
        #endregion

        private void comboBackground_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach(Background background in Background.Backgrounds)
            {
                if(background.ComboIndex == comboBackground.SelectedIndex)
                {
                    Map.Background = background;
                    break;
                }
            }
        }

        private void sliderGlareIntensity_Scroll(object sender, EventArgs e)
        {
            Map.GlareIntensity = (float)sliderGlareIntensity.Value / 100;
        }

        private void buttonShadowColor_Click(object sender, EventArgs e)
        {
            colorDialog.Color = buttonShadowColor.BackColor;
            DialogResult result = colorDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                buttonShadowColor.BackColor = colorDialog.Color;
                Map.ShadowColor = new Vector4((float)colorDialog.Color.R / 255, (float)colorDialog.Color.G / 255, (float)colorDialog.Color.B / 255, Map.FogColor.W);
            }
        }

        private void sliderShadowAlpha_Scroll(object sender, EventArgs e)
        {
            Map.ShadowColor = new Vector4(Map.ShadowColor.X, Map.ShadowColor.Y, Map.ShadowColor.Z, (float)sliderShadowAlpha.Value / 100);
        }

        private void numericSensorsManagerCameraMin_ValueChanged(object sender, EventArgs e)
        {
            Map.SensorsManagerCameraMin = (float)numericSensorsManagerCameraMin.Value;
        }

        private void numericSensorsManagerCameraMax_ValueChanged(object sender, EventArgs e)
        {
            Map.SensorsManagerCameraMax = (float)numericSensorsManagerCameraMax.Value;
        }

        private void boxMusicDefault_TextChanged(object sender, EventArgs e)
        {
            Map.MusicDefault = boxMusicDefault.Text;
        }

        private void boxMusicBattle_TextChanged(object sender, EventArgs e)
        {
            Map.MusicBattle = boxMusicBattle.Text;
        }

        public void PlaySound(string path)
        {
            player.Play();
        }

        private void buttonPlayDefault_Click(object sender, EventArgs e)
        {
            if (playingDefaultMusic)
            {
                player.Stop();
                buttonPlayDefault.Text = "Play default";
                playingDefaultMusic = false;
                return;
            }

            string path = HWData.GetFileInDataPaths(Map.MusicDefault + ".wav");

            playingDefaultMusic = false;
            playingBattleMusic = false;
            buttonPlayDefault.Text = "Play default";
            buttonPlayBattle.Text = "Play battle";

            player.Stop();
            if(!File.Exists(path))
            {
                MessageBox.Show("File \"" + Map.MusicDefault + ".wav\" does not exist in the specified data paths.", "Failed to play music", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            buttonPlayDefault.Text = "Stop";
            playingDefaultMusic = true;
            player.SoundLocation = path;
            player.Play();
        }

        private void buttonPlayBattle_Click(object sender, EventArgs e)
        {
            if (playingBattleMusic)
            {
                player.Stop();
                buttonPlayBattle.Text = "Play battle";
                playingBattleMusic = false;
                return;
            }

            string path = HWData.GetFileInDataPaths(Map.MusicBattle + ".wav");

            playingDefaultMusic = false;
            playingBattleMusic = false;
            buttonPlayDefault.Text = "Play default";
            buttonPlayBattle.Text = "Play battle";

            player.Stop();
            if (!File.Exists(path))
            {
                MessageBox.Show("File \"" + Map.MusicBattle + ".wav\" does not exist in the specified data paths.", "Failed to play music", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            buttonPlayBattle.Text = "Stop";
            playingBattleMusic = true;
            player.SoundLocation = path;
            player.Play();
        }

        private void comboMaxPlayers_SelectedIndexChanged(object sender, EventArgs e)
        {
            Map.MaxPlayers = comboMaxPlayers.SelectedIndex + 1;
        }

        private void boxDescription_TextChanged(object sender, EventArgs e)
        {
            Map.Description = boxDescription.Text;
        }

        #region Proper focus check
        private void Main_Activated(object sender, EventArgs e)
        {
            FormActive = true;
        }
        private void Main_Deactivate(object sender, EventArgs e)
        {
            FormActive = false;
        }
        #endregion

        //-------------------- SOB GROUPS -----------------------//
        public void AddSOBGroup(SOBGroup group)
        {
            group.ItemIndex = listSOBGroups.Items.Add(group.Name);
        }
        public void RemoveSOBGroup(SOBGroup group)
        {
            ignoreListSOBGroups_SelectedIndexChanged = true;
            listSOBGroups.Items.RemoveAt(group.ItemIndex);

            foreach(SOBGroup sobGroup in SOBGroup.SOBGroups)
            {
                if(sobGroup.ItemIndex > group.ItemIndex)
                    sobGroup.ItemIndex--;
            }

            listSOBGroups.ClearSelected();
            ignoreListSOBGroups_SelectedIndexChanged = false;
            SOBGroup.CurrentSelected = null;
            listSOBGroups_SelectedIndexChanged(this, EventArgs.Empty);
        }

        private bool ignoreListSOBGroups_SelectedIndexChanged = false;
        private void listSOBGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ignoreListSOBGroups_SelectedIndexChanged)
                return;

            if(listSOBGroups.SelectedItem == null)
            {
                buttonRemoveSOBGroup.Enabled = false;
                buttonAddSquadron.Enabled = false;
                buttonRemoveSquadron.Enabled = false;
                comboSquadrons.Enabled = false;
                comboSquadrons.SelectedItem = null;
                boxSOBGroupName.Text = string.Empty;
                boxSOBGroupName.Enabled = false;
                listSquadrons.Enabled = false;
                listSquadrons.Items.Clear();

                return;
            }

            SOBGroup.CurrentSelected = SOBGroup.GetByItemIndex(listSOBGroups.SelectedIndex);
            boxSOBGroupName.Text = SOBGroup.CurrentSelected.Name;

            listSquadrons.Items.Clear();
            foreach(Squadron squadron in SOBGroup.CurrentSelected.Squadrons)
            {
                squadron.ItemIndex = listSquadrons.Items.Add(squadron.Name);
            }

            buttonRemoveSOBGroup.Enabled = true;
            buttonAddSquadron.Enabled = true;
            buttonRemoveSquadron.Enabled = false;
            boxSOBGroupName.Enabled = true;
            comboSquadrons.Enabled = true;
            listSquadrons.Enabled = true;
        }

        private void boxSOBGroupName_TextChanged(object sender, EventArgs e)
        {
            if (SOBGroup.CurrentSelected == null)
                return;

            if (listSOBGroups.Items.Count == 0)
                return;

            SOBGroup.CurrentSelected.Name = boxSOBGroupName.Text;

            ignoreListSOBGroups_SelectedIndexChanged = true;
            listSOBGroups.Items[SOBGroup.CurrentSelected.ItemIndex] = SOBGroup.CurrentSelected.Name;
            listSOBGroups.SelectedIndex = SOBGroup.CurrentSelected.ItemIndex;
            ignoreListSOBGroups_SelectedIndexChanged = false;
        }

        private void buttonRemoveSOBGroup_Click(object sender, EventArgs e)
        {
            if (SOBGroup.CurrentSelected == null)
                return;

            SOBGroup.CurrentSelected.Destroy();
        }
        private void listSOBGroups_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Delete)
            {
                if (SOBGroup.CurrentSelected == null)
                    return;

                SOBGroup.CurrentSelected.Destroy();
            }
        }

        private void buttonAddSOBGroup_Click(object sender, EventArgs e)
        {
            SOBGroup newGroup = new SOBGroup("SobGroup_" + (SOBGroup.SOBGroups.Count + 1));
            listSOBGroups.SelectedIndex = newGroup.ItemIndex;
        }

        public void AddSquadron(Squadron squadron)
        {
            if (comboSquadrons.Items.Contains(squadron.Name)) //Ignore duplicate squadron names
                return;

            squadron.ItemIndex = comboSquadrons.Items.Add(squadron.Name);
        }
        public void RemoveSquadron(Squadron squadron)
        {
            comboSquadrons.Items.Remove(squadron.Name);
            foreach (Squadron squad in Squadron.Squadrons)
            {
                if (squad.ItemIndex > squadron.ItemIndex)
                    squad.ItemIndex--;
            }
            comboSquadrons.SelectedItem = null;
            listSquadrons.Items.Remove(squadron.Name);
            listSquadrons.ClearSelected();
            listSquadrons.Refresh();
        }

        private void buttonAddSquadron_Click(object sender, EventArgs e)
        {
            if (SOBGroup.CurrentSelected == null)
                return;

            if (comboSquadrons.SelectedItem == null)
                return;

            Squadron selectedSquadron = Squadron.GetByName(comboSquadrons.SelectedItem.ToString());

            if (selectedSquadron == null)
                return;

            if (SOBGroup.CurrentSelected.Squadrons.Contains(selectedSquadron))
                return;

            SOBGroup.CurrentSelected.AddSquadron(selectedSquadron);
            listSOBGroups_SelectedIndexChanged(this, EventArgs.Empty);
            listSquadrons.SelectedIndex = listSquadrons.Items.Count - 1;
        }

        private void buttonRemoveSquadron_Click(object sender, EventArgs e)
        {
            if (SOBGroup.CurrentSelected == null)
                return;

            Squadron selectedSquadron = Squadron.GetByName(listSquadrons.SelectedItem.ToString());

            if (selectedSquadron == null)
                return;

            SOBGroup.CurrentSelected.RemoveSquadron(selectedSquadron);
            listSquadrons.Items.Remove(selectedSquadron.Name);
            listSquadrons.ClearSelected();
            listSquadrons.Refresh();
        }

        private void listSquadrons_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SOBGroup.CurrentSelected == null)
                return;

            if(listSquadrons.SelectedItem == null)
            {
                buttonRemoveSquadron.Enabled = false;
            }
            else
            {
                buttonRemoveSquadron.Enabled = true;
            }
        }

        private void listSquadrons_KeyUp(object sender, KeyEventArgs e)
        {
            if (SOBGroup.CurrentSelected == null)
                return;

            Squadron selectedSquadron = Squadron.GetByName(listSquadrons.SelectedItem.ToString());

            if (selectedSquadron == null)
                return;

            SOBGroup.CurrentSelected.RemoveSquadron(selectedSquadron);
            listSquadrons.Items.Remove(selectedSquadron.Name);
            listSquadrons.ClearSelected();
            listSquadrons.Refresh();
        }

        //-------------------- UPDATER -----------------------//
        private void buttonCheckForUpdates_Click(object sender, EventArgs e)
        {
            Updater.CheckForUpdatesManually();
        }
    }
}
