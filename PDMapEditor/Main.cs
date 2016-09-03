using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenTK;
using OpenTK.Graphics;

namespace PDMapEditor
{
    public partial class Main : Form
    {
        public bool Loaded = false;
        private bool problemsVisible;

        private bool ignoreMapDimensionChange;
        private bool ignoreFogAlphaChange;
        private bool ignoreFogDensityChange;

        public Main()
        {
            InitializeComponent();
        }

        public void glControl_Load(object sender, EventArgs e)
        {
            Log.WriteLine("Initializing...");

            Importer.Init();
            Renderer.Init();
            Selection.Init();

            HWData.ParseDataPaths();

            Application.Idle += glControl_Update;
            Program.DeltaCounter.Start();
            FPSCounter.LabelFPS = labelFPS;
            Loaded = true;

            gridProblems.RowTemplate.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            gridProblems.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            gridProblems.Columns[0].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            Map.Clear();
            Program.Camera.ResetCamera();

            Program.main.UpdateProblems();
            Renderer.UpdateMeshData();
            Renderer.UpdateView();
            Program.GLControl.Invalidate();
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Idle -= glControl_Update;
            GraphicsContext.CurrentContext.Dispose();
            Settings.SaveSettings();
            Hotkeys.SaveHotkeys();
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
        }

        public void glControl_Render(object sender, PaintEventArgs e)
        {
            if (!Loaded)
                return;

            FPSCounter.Update();
            Renderer.Render();
        }

        public void glControl_Resize(object sender, EventArgs e)
        {
            if (!Loaded)
                return;

            Renderer.Resize();
            Program.GLControl.Invalidate();
        }

        public void glControl_Enter(object sender, EventArgs e)
        {
            Program.GLControl.Focus();
        }

        public void glControl_Leave(object sender, EventArgs e)
        {
            this.Focus();
        }

        public void glControl_MouseMove(object sender, MouseEventArgs e)
        {
            Selection.UpdateDragging(e.X, Program.GLControl.ClientSize.Height - e.Y);
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

            //Selecting
            if (e.Button == MouseButtons.Left)
                Selection.LeftMouseUp(e.X, Program.GLControl.ClientSize.Height - e.Y);
        }

        public void glControl_KeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            ActionKey.KeyDown(e);
            Program.Camera.KeyDown();
            Selection.KeyDown();
        }

        public void glControl_KeyUp(object sender, KeyEventArgs e)
        {
            ActionKey.KeyUp(e);
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
            DialogResult result = openMapDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                Map.Clear();
                Program.Map.LoadMap(openMapDialog.FileName);
                this.Text = "PayDay's Homeworld Remastered Map Editor - " + openMapDialog.FileName;

                Program.Camera.ResetCamera();
                Program.main.UpdateProblems();
                Renderer.UpdateMeshData();
                Renderer.UpdateView();
                Program.GLControl.Invalidate();
            }
        }

        private void buttonSaveMap_Click(object sender, EventArgs e)
        {
            DialogResult result = saveMapDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                Program.Map.SaveMap(saveMapDialog.FileName);
            }
        }

        private void buttonSettings_Click(object sender, EventArgs e)
        {
            if (Program.Settings != null)
                return;

            Program.Settings = new Settings();
            Program.Settings.Visible = true;
            Program.Settings.Init();
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
            MessageBox.Show(this, "PayDay's Homeworld Remastered Map Editor b" + Program.BUILD + "\n\nDeveloped by Christoph (PayDay) Timmermann.\n\nUses\n - OpenTK\n - NLua\n - Assimp\n - Assimp.NET", "PayDay's Homeworld Remastered Map Editor", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            Program.GLControl.Invalidate();
        }

        private void checkFogDisplay_CheckedChanged(object sender, EventArgs e)
        {
            Renderer.DisplayFog = checkFogDisplay.Checked;
            Program.GLControl.Invalidate();
        }

        public void UpdateFogStart()
        {
            numericFogStart.Value = (decimal)Map.FogStart;
        }
        private void numericFogStart_ValueChanged(object sender, EventArgs e)
        {
            Map.FogStart = (float)numericFogStart.Value;
            Program.GLControl.Invalidate();
        }

        public void UpdateFogEnd()
        {
            numericFogEnd.Value = (decimal)Map.FogEnd;
        }
        private void numericFogEnd_ValueChanged(object sender, EventArgs e)
        {
            Map.FogEnd = (float)numericFogEnd.Value;
            Program.GLControl.Invalidate();
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
                Program.GLControl.Invalidate();
            }
        }
        private void sliderFogAlpha_Scroll(object sender, EventArgs e)
        {
            ignoreFogAlphaChange = true;
            Map.FogColor = new Vector4(Map.FogColor.X, Map.FogColor.Y, Map.FogColor.Z, (float)sliderFogAlpha.Value / 100);
            ignoreFogAlphaChange = false;
            Program.GLControl.Invalidate();
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
            Program.GLControl.Invalidate();
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
            Program.GLControl.Invalidate();
        }
        #endregion

        private void comboBackground_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach(Background background in Background.Backgrounds)
            {
                if(background.ComboBackgroundIndex == comboBackground.SelectedIndex)
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
    }
}
