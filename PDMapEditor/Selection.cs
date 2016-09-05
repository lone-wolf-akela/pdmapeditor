using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;
using OpenTK;
using System.Media;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace PDMapEditor
{
    public static class Selection
    {
        private static Shader shader;

        private static Drawable gizmoPosX;
        private static Drawable gizmoPosY;
        private static Drawable gizmoPosZ;

        private static Line gizmoLineX;
        private static Line gizmoLineY;
        private static Line gizmoLineZ;

        private static Drawable gizmoRotX;
        private static Drawable gizmoRotY;
        private static Drawable gizmoRotZ;

        private static DraggingState draggingState = DraggingState.NONE;

        private static GizmoMode gizmoMode = GizmoMode.TRANSLATION;
        public static GizmoMode GizmoMode { get { return gizmoMode; } set { gizmoMode = value; UpdateSelectionGizmos(); } }

        private static float lastMouseX, lastMouseY;

        private static int mouseClickX, mouseClickY;

        private static Vector3 rotationAtStart = Vector3.Zero;

        private static Vector3 averagePosition = Vector3.Zero;
        public static bool rectangleSelecting;
        private static bool clickingLeft;

        //Selection lines
        static Mesh2D selectionLineA = new Mesh2D();
        static Mesh2D selectionLineB = new Mesh2D();
        static Mesh2D selectionLineC = new Mesh2D();
        static Mesh2D selectionLineD = new Mesh2D();

        //GUI
        private static bool ignorePositionChange;
        private static bool ignoreRotationChange;

        private static bool ignoreGizmoModeChange;
        private static object comboGizmoModeRotationItem;

        //private static ISelectable selected;
        //public static ISelectable Selected { get { return selected; } set { selected = value; UpdateSelectionGUI(); UpdateSelectionGizmos(); } }

        public static ObservableCollection<ISelectable> Selected = new ObservableCollection<ISelectable>();

        public static void Init()
        {
            shader = new Shader("picking.vs", "picking.fs", true);
            Selected.CollectionChanged += SelectedChanged;

            //GUI
            Program.main.comboGizmoMode.Items.Add("Translation");
            Program.main.comboGizmoMode.Items.Add("Rotation");
            comboGizmoModeRotationItem = Program.main.comboGizmoMode.Items[1];

            Program.main.comboGizmoMode.SelectedIndex = 0;
            Program.main.comboGizmoMode.SelectedIndexChanged += new EventHandler(comboGizmoMode_SelectedIndexChanged);

            Program.main.tabControlLeft.TabPages.Remove(Program.main.tabSelection);

            Program.main.numericSelectionPositionX.ValueChanged += new EventHandler(PositionChanged);
            Program.main.numericSelectionPositionY.ValueChanged += new EventHandler(PositionChanged);
            Program.main.numericSelectionPositionZ.ValueChanged += new EventHandler(PositionChanged);

            Program.main.numericSelectionRotationX.ValueChanged += new EventHandler(RotationChanged);
            Program.main.numericSelectionRotationY.ValueChanged += new EventHandler(RotationChanged);
            Program.main.numericSelectionRotationZ.ValueChanged += new EventHandler(RotationChanged);

            //Asteroid
            Program.main.comboAsteroidType.SelectedIndexChanged += new EventHandler(AsteroidTypeChanged);
            Program.main.numericAsteroidResourceMultiplier.ValueChanged += new EventHandler(AsteroidResourceMultiplierChanged);

            //Dust cloud
            Program.main.boxDustCloudName.TextChanged += new EventHandler(DustCloudNameChanged);
            Program.main.comboDustCloudType.SelectedIndexChanged += new EventHandler(DustCloudTypeChanged);
            Program.main.buttonDustCloudColor.Click+= new EventHandler(DustCloudColorClicked);
            Program.main.sliderDustCloudAlpha.Scroll += new EventHandler(DustCloudAlphaChanged);
            Program.main.numericDustCloudSize.ValueChanged += new EventHandler(DustCloudSizeChanged);

            //Point
            Program.main.boxPointName.TextChanged += new EventHandler(PointNameChanged);

            //Pebble
            Program.main.comboPebbleType.SelectedIndexChanged += new EventHandler(PebbleTypeChanged);
        }

        private static void SelectedChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            UpdateAveragePosition();
            UpdateSelectionGUI();
            UpdateSelectionGizmos();
        }

        private static void UpdateAveragePosition()
        {
            averagePosition = Vector3.Zero;
            foreach(ISelectable selectable in Selected)
            {
                averagePosition = Vector3.Add(averagePosition, selectable.Position);
            }
            averagePosition = Vector3.Divide(averagePosition, Selected.Count);
        }

        public static void CreateGizmos()
        {
            //Gizmos
            gizmoPosX = new Gizmo(Vector3.Zero, Mesh.GizmoXPos);
            gizmoPosX.Mesh.Scale = new Vector3(25);
            gizmoPosX.Visible = false;
            gizmoPosX.Mesh.Material.Translucent = true;
            gizmoLineX = new Line(Vector3.Zero + new Vector3(-100000, 0, 0), Vector3.Zero + new Vector3(100000, 0, 0), new Vector3(1, 0, 0));
            gizmoLineX.Visible = false;

            gizmoPosY = new Gizmo(Vector3.Zero, Mesh.GizmoYPos);
            gizmoPosY.Mesh.Scale = new Vector3(25);
            gizmoPosY.Visible = false;
            gizmoPosY.Mesh.Material.Translucent = true;
            gizmoLineY = new Line(Vector3.Zero + new Vector3(0, -100000, 0), Vector3.Zero + new Vector3(0, 100000, 0), new Vector3(0, 1, 0));
            gizmoLineY.Visible = false;

            gizmoPosZ = new Gizmo(Vector3.Zero, Mesh.GizmoZPos);
            gizmoPosZ.Mesh.Scale = new Vector3(25);
            gizmoPosZ.Visible = false;
            gizmoPosZ.Mesh.Material.Translucent = true;
            gizmoLineZ = new Line(Vector3.Zero + new Vector3(0, 0, -100000), Vector3.Zero + new Vector3(0, 0, 100000), new Vector3(0, 0, 1));
            gizmoLineZ.Visible = false;

            gizmoRotX = new Gizmo(Vector3.Zero, Mesh.GizmoXRot);
            gizmoRotX.Mesh.Scale = new Vector3(25);
            gizmoRotX.Visible = false;

            gizmoRotY = new Gizmo(Vector3.Zero, Mesh.GizmoYRot);
            gizmoRotY.Mesh.Scale = new Vector3(25);
            gizmoRotY.Visible = false;

            gizmoRotZ = new Gizmo(Vector3.Zero, Mesh.GizmoZRot);
            gizmoRotZ.Mesh.Scale = new Vector3(25);
            gizmoRotZ.Visible = false;
        }

        public static void LeftMouseDown(int x, int y)
        {
            mouseClickX = x;
            mouseClickY = y;

            ISelectable objectAtMouse = GetObjectAtPixel(x, y);

            if (objectAtMouse == gizmoPosX)
            {
                gizmoLineX.Visible = true;
                gizmoLineX.Position = gizmoPosX.Position;
                draggingState = DraggingState.MOVING_X;

                Renderer.UpdateView();
                Program.GLControl.Invalidate();
            }
            else if (objectAtMouse == gizmoPosY)
            {
                gizmoLineY.Visible = true;
                gizmoLineY.Position = gizmoPosY.Position;
                draggingState = DraggingState.MOVING_Y;

                Renderer.UpdateView();
                Program.GLControl.Invalidate();
            }
            else if (objectAtMouse == gizmoPosZ)
            {
                gizmoLineZ.Visible = true;
                gizmoLineZ.Position = gizmoPosZ.Position;
                draggingState = DraggingState.MOVING_Z;

                Renderer.UpdateView();
                Program.GLControl.Invalidate();
            }
            else if (objectAtMouse == gizmoRotX)
            {
                gizmoLineX.Visible = true;
                gizmoLineX.Position = gizmoRotX.Position;
                draggingState = DraggingState.ROTATING_X;

                rotationAtStart = Selected[0].Rotation;

                Renderer.UpdateView();
                Program.GLControl.Invalidate();
            }
            else if (objectAtMouse == gizmoRotY)
            {
                gizmoLineY.Visible = true;
                gizmoLineY.Position = gizmoRotY.Position;
                draggingState = DraggingState.ROTATING_Y;

                rotationAtStart = Selected[0].Rotation;

                Renderer.UpdateView();
                Program.GLControl.Invalidate();
            }
            else if (objectAtMouse == gizmoRotZ)
            {
                gizmoLineZ.Visible = true;
                gizmoLineZ.Position = gizmoRotZ.Position;
                draggingState = DraggingState.ROTATING_Z;

                rotationAtStart = Selected[0].Rotation;

                Renderer.UpdateView();
                Program.GLControl.Invalidate();
            }
            else
                clickingLeft = true;
        }
        
        public static void UpdateRectangleSelection(int mouseX, int mouseY)
        {
            if (clickingLeft)
            {
                float diffX = Math.Abs(mouseClickX - mouseX);
                float diffY = Math.Abs(mouseClickY - mouseY);
                if (diffX + diffY > 5 && !rectangleSelecting)
                {
                    rectangleSelecting = true;
                    selectionLineA.Visible = true;
                    selectionLineB.Visible = true;
                    selectionLineC.Visible = true;
                    selectionLineD.Visible = true;
                }

                if (rectangleSelecting)
                {
                    float clickXRel = ((float)mouseClickX / Program.GLControl.ClientSize.Width);
                    clickXRel = (clickXRel - 0.5f) * 2;
                    float clickYRel = ((float)mouseClickY / Program.GLControl.ClientSize.Height);
                    clickYRel = (clickYRel - 0.5f) * 2;

                    float mouseXRel = ((float)mouseX / Program.GLControl.ClientSize.Width);
                    mouseXRel = (mouseXRel - 0.5f) * 2;
                    float mouseYRel = ((float)mouseY / Program.GLControl.ClientSize.Height);
                    mouseYRel = (mouseYRel - 0.5f) * 2;

                    selectionLineA.Start = new Vector2(clickXRel, clickYRel);
                    selectionLineA.End = new Vector2(mouseXRel, clickYRel);

                    selectionLineB.Start = new Vector2(mouseXRel, clickYRel);
                    selectionLineB.End = new Vector2(mouseXRel, mouseYRel);

                    selectionLineC.Start = new Vector2(mouseXRel, mouseYRel);
                    selectionLineC.End = new Vector2(clickXRel, mouseYRel);

                    selectionLineD.Start = new Vector2(clickXRel, mouseYRel);
                    selectionLineD.End = new Vector2(clickXRel, clickYRel);

                    Renderer.Update2DMeshData();
                    Program.GLControl.Invalidate();
                }
            }
        }

        public static void LeftMouseUp(int x, int y)
        {
            if (draggingState == DraggingState.NONE)
            {
                if (rectangleSelecting) //Rectangle selection
                {
                    selectionLineA.Visible = false;
                    selectionLineB.Visible = false;
                    selectionLineC.Visible = false;
                    selectionLineD.Visible = false;

                    int startX = Math.Min(mouseClickX, x);
                    int startY = Math.Min(mouseClickY, y);

                    int width = Math.Abs(mouseClickX - x);
                    int height = Math.Abs(mouseClickY - y);

                    List<ISelectable> objects = GetObjectsInRectangle(startX, startY, width, height);

                    if (objects.Count > 0)
                    {
                        Selected.Clear();
                        foreach (ISelectable obj in objects)
                        {
                            Selected.Add(obj);
                        }
                    }
                }
                else //Single selection
                {
                    ISelectable objectAtMouse = GetObjectAtPixel(x, y);
                    Drawable drawable = objectAtMouse as Drawable;

                    if (drawable != null)
                    {
                        if (drawable != gizmoPosX && drawable != gizmoPosY && drawable != gizmoPosZ && drawable != gizmoRotX && drawable != gizmoRotY && drawable != gizmoRotZ)
                            if (ActionKey.IsDown(Action.SELECTION_ADD))
                                Selected.Add(objectAtMouse);
                            else
                            {
                                Selected.Clear();
                                Selected.Add(objectAtMouse);
                            }
                    }
                    else
                        if (!ActionKey.IsDown(Action.SELECTION_ADD))
                        Selected.Clear();
                }

                rectangleSelecting = false;
            }

            draggingState = DraggingState.NONE;
            gizmoLineX.Visible = false;
            gizmoLineY.Visible = false;
            gizmoLineZ.Visible = false;

            gizmoRotX.Rotation = Vector3.Zero;
            gizmoRotY.Rotation = Vector3.Zero;
            gizmoRotZ.Rotation = Vector3.Zero;

            clickingLeft = false;

            Renderer.UpdateView();
            Program.GLControl.Invalidate();
        }

        private static void UpdateSelectionGUI()
        {
            Program.main.groupSelectionRotation.Visible = false;

            Program.main.groupAsteroid.Visible = false;
            Program.main.groupDustCloud.Visible = false;
            Program.main.groupPoint.Visible = false;
            Program.main.groupPebble.Visible = false;

            if (Selected.Count == 0)
            {
                Program.main.tabControlLeft.SelectedIndex = 0;
                Program.main.tabControlLeft.TabPages.Remove(Program.main.tabSelection);
                return;
            }

            if(!Program.main.tabControlLeft.TabPages.Contains(Program.main.tabSelection))
                Program.main.tabControlLeft.TabPages.Add(Program.main.tabSelection);

            Program.main.tabControlLeft.SelectedIndex = 1;

            UpdatePositionGUI();

            bool allowRotation = true;
            foreach(ISelectable selectable in Selected)
            {
                if (!selectable.AllowRotation)
                {
                    allowRotation = false;
                    break;
                }
            }
            if (allowRotation) //Don't allow rotation changes if one of the objects cannot be rotated
            {
                UpdateRotationGUI();
                Program.main.groupSelectionRotation.Visible = true;

                if(!Program.main.comboGizmoMode.Items.Contains(comboGizmoModeRotationItem))
                    Program.main.comboGizmoMode.Items.Add(comboGizmoModeRotationItem);
            }
            else
            {
                GizmoMode = GizmoMode.TRANSLATION;
                Program.main.comboGizmoMode.Items.Remove(comboGizmoModeRotationItem);
            }

            if (Selected.Count == 1)
            {
                //Show individual groups for the properties
                Asteroid selectedAsteroid = Selected[0] as Asteroid;
                if (selectedAsteroid != null)
                {
                    Program.main.groupAsteroid.Visible = true;
                    Program.main.comboAsteroidType.SelectedIndex = selectedAsteroid.Type.ComboIndex;
                    Program.main.numericAsteroidResourceMultiplier.Value = (decimal)selectedAsteroid.Multiplier;
                }

                DustCloud selectedDustCloud = Selected[0] as DustCloud;
                if (selectedDustCloud != null)
                {
                    Program.main.groupDustCloud.Visible = true;
                    Program.main.boxDustCloudName.Text = selectedDustCloud.Name;
                    Program.main.comboDustCloudType.SelectedIndex = selectedDustCloud.Type.ComboIndex;

                    //This is BAD, dust clouds can have color values above 1
                    //TODO: Replace color dialog and slider for better controls
                    Program.main.buttonDustCloudColor.BackColor = Color.FromArgb(255, (int)Math.Round(Math.Min(selectedDustCloud.Color.X * 255, 255)), (int)Math.Round(Math.Min(selectedDustCloud.Color.Y * 255, 255)), (int)Math.Round(Math.Min(selectedDustCloud.Color.Z * 255, 255)));
                    Program.main.sliderDustCloudAlpha.Value = (int)Math.Round(Math.Min(selectedDustCloud.Color.W * 100, 100));

                    Program.main.numericDustCloudSize.Value = (decimal)selectedDustCloud.Size;
                }

                Point selectedPoint = Selected[0] as Point;
                if (selectedPoint != null)
                {
                    Program.main.groupPoint.Visible = true;
                    Program.main.boxPointName.Text = selectedPoint.Name;
                }

                Pebble selectedPebble = Selected[0] as Pebble;
                if (selectedPebble != null)
                {
                    Program.main.groupPebble.Visible = true;
                    Program.main.comboPebbleType.SelectedIndex = selectedPebble.Type.ComboIndex;
                }
            }

            Program.GLControl.Focus();
        }

        #region Asteroid
        private static void AsteroidResourceMultiplierChanged(object sender, EventArgs e)
        {
            Asteroid selectedAsteroid = Selected[0] as Asteroid;
            selectedAsteroid.Multiplier = (float)Program.main.numericAsteroidResourceMultiplier.Value;
        }
        private static void AsteroidTypeChanged(object sender, EventArgs e)
        {
            Asteroid selectedAsteroid = Selected[0] as Asteroid;
            selectedAsteroid.Type = AsteroidType.GetTypeFromComboIndex(Program.main.comboAsteroidType.SelectedIndex);

            Renderer.UpdateView();
            Program.GLControl.Invalidate();
        }
        #endregion

        #region Dust cloud
        private static void DustCloudNameChanged(object sender, EventArgs e)
        {
            DustCloud selectedDustCloud = Selected[0] as DustCloud;
            selectedDustCloud.Name = Program.main.boxDustCloudName.Text;
        }
        private static void DustCloudTypeChanged(object sender, EventArgs e)
        {
            DustCloud selectedDustCloud = Selected[0] as DustCloud;
            selectedDustCloud.Type = DustCloudType.GetTypeFromComboIndex(Program.main.comboDustCloudType.SelectedIndex);
            Program.GLControl.Invalidate();
        }
        private static void DustCloudColorClicked(object sender, EventArgs e)
        {
            Program.main.colorDialog.Color = Program.main.buttonDustCloudColor.BackColor;
            DialogResult result = Program.main.colorDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                Color color = Program.main.colorDialog.Color;
                Program.main.buttonDustCloudColor.BackColor = color;

                DustCloud selectedDustCloud = Selected[0] as DustCloud;
                selectedDustCloud.Color = new Vector4((float)color.R / 255, (float)color.G / 255, (float)color.B / 255, selectedDustCloud.Color.W);
                Program.GLControl.Invalidate();
            }
        }
        private static void DustCloudAlphaChanged(object sender, EventArgs e)
        {
            DustCloud selectedDustCloud = Selected[0] as DustCloud;
            selectedDustCloud.Color = new Vector4(selectedDustCloud.Color.X, selectedDustCloud.Color.Y, selectedDustCloud.Color.Z, (float)Program.main.sliderDustCloudAlpha.Value / 100);
            Program.GLControl.Invalidate();
        }

        private static void DustCloudSizeChanged(object sender, EventArgs e)
        {
            DustCloud selectedDustCloud = Selected[0] as DustCloud;
            selectedDustCloud.Size = (float)Program.main.numericDustCloudSize.Value;

            Renderer.UpdateView();
            Program.GLControl.Invalidate();
        }
        #endregion

        #region Point
        private static void PointNameChanged(object sender, EventArgs e)
        {
            Point selectedPoint = Selected[0] as Point;
            selectedPoint.Name = Program.main.boxPointName.Text;
        }
        #endregion

        #region Pebble
        private static void PebbleTypeChanged(object sender, EventArgs e)
        {
            Pebble selectedPebble = Selected[0] as Pebble;
            selectedPebble.Type = PebbleType.GetTypeFromComboIndex(selectedPebble.Type.ComboIndex);
            Program.GLControl.Invalidate();
        }
        #endregion

        private static void UpdatePositionGUI()
        {
            ignorePositionChange = true;
            Program.main.numericSelectionPositionX.Value = (decimal)averagePosition.X;
            Program.main.numericSelectionPositionY.Value = (decimal)averagePosition.Y;
            Program.main.numericSelectionPositionZ.Value = (decimal)averagePosition.Z;
            ignorePositionChange = false;
        }
        private static void UpdateRotationGUI()
        {
            if (Selected.Count == 1)
            {
                ignoreRotationChange = true;
                Program.main.numericSelectionRotationX.Value = (decimal)Selected[0].Rotation.X;
                Program.main.numericSelectionRotationY.Value = (decimal)Selected[0].Rotation.Y;
                Program.main.numericSelectionRotationZ.Value = (decimal)Selected[0].Rotation.Z;
                ignoreRotationChange = false;
            }
        }

        private static void UpdateSelectionGizmos()
        {
            gizmoPosX.Visible = false;
            gizmoPosY.Visible = false;
            gizmoPosZ.Visible = false;

            gizmoRotX.Visible = false;
            gizmoRotY.Visible = false;
            gizmoRotZ.Visible = false;

            ignoreGizmoModeChange = true;
            if (GizmoMode == GizmoMode.TRANSLATION)
                Program.main.comboGizmoMode.SelectedIndex = 0;
            else if (GizmoMode == GizmoMode.ROTATION)
                Program.main.comboGizmoMode.SelectedIndex = 1;
            ignoreGizmoModeChange = false;

            if (Selected.Count == 0)
                return;

            //Gizmos
            gizmoPosX.Position = averagePosition;
            gizmoPosY.Position = averagePosition;
            gizmoPosZ.Position = averagePosition;

            gizmoRotX.Position = averagePosition;
            gizmoRotY.Position = averagePosition;
            gizmoRotZ.Position = averagePosition;

            ignoreGizmoModeChange = true;
            if (GizmoMode == GizmoMode.TRANSLATION)
            {
                gizmoPosX.Visible = true;
                gizmoPosY.Visible = true;
                gizmoPosZ.Visible = true;
                Program.main.comboGizmoMode.SelectedIndex = 0;
            }
            else if(GizmoMode == GizmoMode.ROTATION)
            {
                gizmoRotX.Visible = true;
                gizmoRotY.Visible = true;
                gizmoRotZ.Visible = true;
                Program.main.comboGizmoMode.SelectedIndex = 1;
            }
            ignoreGizmoModeChange = false;

            UpdateSelectionGizmoScale();
        }

        private static void comboGizmoMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ignoreGizmoModeChange)
                return;

            if(Program.main.comboGizmoMode.SelectedIndex == 0) //Translation
            {
                GizmoMode = GizmoMode.TRANSLATION;
                Renderer.UpdateView();
                Program.GLControl.Invalidate();
            }
            else if (Program.main.comboGizmoMode.SelectedIndex == 1) //Rotation
            {
                GizmoMode = GizmoMode.ROTATION;
                Renderer.UpdateView();
                Program.GLControl.Invalidate();
            }
        }

        public static void UpdateSelectionGizmoScale()
        {
            if (Selected.Count == 0)
                return;

            Vector3 scale = Vector3.One;
            if (!Program.Camera.Orthographic)
            {
                float cameraDistanceApprox = (Program.Camera.Position - averagePosition).LengthFast;
                scale = new Vector3(cameraDistanceApprox / 700);
            }
            else
                scale = new Vector3(1 / Program.Camera.OrthographicSize * 2); //The orthographic size gets smaller the more you zoom out, so the scale should raise when the orthographic size gets smaller

            gizmoPosX.Mesh.Scale = new Vector3(scale);
            gizmoPosY.Mesh.Scale = new Vector3(scale);
            gizmoPosZ.Mesh.Scale = new Vector3(scale);

            gizmoRotX.Mesh.Scale = new Vector3(scale);
            gizmoRotY.Mesh.Scale = new Vector3(scale);
            gizmoRotZ.Mesh.Scale = new Vector3(scale);
        }

        public static void UpdateGizmoFading() //This fades gizmos that are in the way at certain camera angles
        {
            if (Selected.Count > 0)
            {
                Vector2 angles = Program.Camera.Angles;

                //Left
                float diffX = Math.Abs((float)Math.PI - angles.X);
                float diffY = Math.Abs((float)Math.PI * 1.5f - angles.Y);
                float average = (diffX + diffY) / 2;

                float opacity = average * (float)Math.PI;
                opacity /= 2;
                gizmoPosX.Mesh.Material.Opacity = opacity;

                if (opacity >= 1)
                {
                    //Right
                    diffX = Math.Abs((float)Math.PI - angles.X);
                    diffY = Math.Abs((float)Math.PI / 2 - angles.Y);
                    average = (diffX + diffY) / 2;

                    opacity = average * (float)Math.PI;
                    opacity /= 2;
                    gizmoPosX.Mesh.Material.Opacity = opacity;
                }

                //Top
                diffX = Math.Abs((float)Math.PI * 1.5f - angles.X);
                average = diffX;

                opacity = average * (float)Math.PI;
                opacity /= 2;
                gizmoPosY.Mesh.Material.Opacity = opacity;

                if (opacity >= 1)
                {
                    //Bottom
                    diffX = Math.Abs((float)Math.PI / 2 - angles.X);
                    average = diffX;

                    opacity = average * (float)Math.PI;
                    opacity /= 2;
                    gizmoPosY.Mesh.Material.Opacity = opacity;
                }

                //Front
                diffX = Math.Abs((float)Math.PI - angles.X);
                diffY = Math.Abs((float)Math.PI - angles.Y);
                average = (diffX + diffY) / 2;

                opacity = average * (float)Math.PI;
                opacity /= 2;
                gizmoPosZ.Mesh.Material.Opacity = opacity;

                if (opacity >= 1)
                {
                    //Back
                    diffX = Math.Abs((float)Math.PI - angles.X);
                    diffY = Math.Abs((float)0 - angles.Y);
                    average = (diffX + diffY) / 2;

                    opacity = average * (float)Math.PI;
                    opacity /= 2;
                    gizmoPosZ.Mesh.Material.Opacity = opacity;
                }
            }
        }

        public static void KeyDown()
        {
            if (ActionKey.IsDown(Action.MODE_TRANSLATION))
            {
                GizmoMode = GizmoMode.TRANSLATION;
                Renderer.UpdateView();
                Program.GLControl.Invalidate();
            }
            else if (ActionKey.IsDown(Action.MODE_ROTATION))
            {
                bool allowRotation = true;
                foreach (ISelectable selectable in Selected)
                {
                    if (!selectable.AllowRotation)
                    {
                        allowRotation = false;
                        break;
                    }
                }

                if (allowRotation) //Don't allow rotation changes if the object cannot be rotated
                {
                    GizmoMode = GizmoMode.ROTATION;
                    Renderer.UpdateView();
                    Program.GLControl.Invalidate();
                }
                else
                    SystemSounds.Beep.Play();
            }
            
            if(ActionKey.IsDown(Action.CAM_FOCUS_SELECTION))
            {
                if (Selected.Count > 0)
                {
                    Program.Camera.LookAt = averagePosition;
                    Renderer.UpdateView();
                    Program.GLControl.Invalidate();
                }
                else
                    SystemSounds.Beep.Play();
            }
        }

        public static void UpdateDragging(int mouseX, int mouseY)
        {
            if (draggingState != DraggingState.NONE)
            {
                float mouseXDelta = mouseX - lastMouseX;
                float mouseYDelta = mouseY - lastMouseY;
                float cameraDistanceApprox = (Program.Camera.Position - averagePosition).LengthFast;

                float posX = averagePosition.X;
                float posY = averagePosition.Y;
                float posZ = averagePosition.Z;

                //float rotX = selectedDrawable.Rotation.X;
                //float rotY = selectedDrawable.Rotation.Y;
                //float rotZ = selectedDrawable.Rotation.Z;
                float rotX = 0;
                float rotY = 0;
                float rotZ = 0;

                float speedMultiplier = 1;

                if (!Program.Camera.Orthographic)
                    speedMultiplier = cameraDistanceApprox / 800;
                else
                    speedMultiplier = 1 / Program.Camera.OrthographicSize; //The orthographic size gets smaller the more you zoom out, so the speed should raise the lower the size gets

                switch (draggingState)
                {
                    //Position
                    case DraggingState.MOVING_X:
                        if(Program.Camera.Position.Z > averagePosition.Z) //To fix the user experience
                            posX = averagePosition.X + mouseXDelta * speedMultiplier;
                        else
                            posX = averagePosition.X - mouseXDelta * speedMultiplier;
                        break;
                    case DraggingState.MOVING_Y:
                        posY = averagePosition.Y + mouseYDelta * speedMultiplier;
                        break;
                    case DraggingState.MOVING_Z:
                        if (Program.Camera.Position.X > averagePosition.X) //To fix the user experience
                            posZ = averagePosition.Z - mouseXDelta * speedMultiplier;
                        else
                            posZ = averagePosition.Z + mouseXDelta * speedMultiplier;
                        break;

                    //Rotation
                    case DraggingState.ROTATING_X:
                        if (Program.Camera.Position.X > averagePosition.X) //To fix the user experience
                            //rotX = selectedDrawable.Rotation.X - mouseXDelta;
                            rotX = -mouseXDelta;
                        else
                            //rotX = selectedDrawable.Rotation.X + mouseXDelta;
                            rotX = mouseXDelta;
                            break;
                    case DraggingState.ROTATING_Y:
                        //rotY = selectedDrawable.Rotation.Y + mouseXDelta;
                        rotY = mouseXDelta;
                        break;
                    case DraggingState.ROTATING_Z:
                        if (Program.Camera.Position.Z > averagePosition.Z) //To fix the user experience
                            //rotZ = selectedDrawable.Rotation.Z - mouseXDelta;
                            rotZ = -mouseXDelta;
                        else
                            //rotZ = selectedDrawable.Rotation.Z + mouseXDelta;
                            rotZ = -mouseXDelta;
                            break;
                }

                Vector3 deltaPos = new Vector3(posX, posY, posZ);
                Vector3 deltaRot = new Vector3(rotX, rotY, rotZ);

                foreach(ISelectable selectable in Selected)
                {
                    selectable.Position -= averagePosition - deltaPos;
                    selectable.Rotation += deltaRot;
                }

                averagePosition = deltaPos;

                gizmoRotX.Rotation += deltaRot;
                gizmoRotY.Rotation += deltaRot;
                gizmoRotZ.Rotation += deltaRot;

                UpdatePositionGUI();
                UpdateRotationGUI();
                UpdateSelectionGizmos();
                UpdateSelectionGizmoScale();
                Renderer.UpdateView();
                Program.GLControl.Invalidate();
            }

            lastMouseX = mouseX;
            lastMouseY = mouseY;
        }

        private static void PositionChanged(object sender, EventArgs e)
        {
            if (ignorePositionChange)
                return;

            Vector3 newPosition = new Vector3((float)Program.main.numericSelectionPositionX.Value, (float)Program.main.numericSelectionPositionY.Value, (float)Program.main.numericSelectionPositionZ.Value);

            foreach (ISelectable selectable in Selected)
            {
                selectable.Position -= averagePosition - newPosition;
            }

            averagePosition = newPosition;

            UpdateSelectionGizmos();
            Renderer.UpdateView();
            Program.GLControl.Invalidate();
        }

        private static void RotationChanged(object sender, EventArgs e)
        {
            if (Selected == null || ignoreRotationChange)
                return;

            foreach(ISelectable selectable in Selected)
            {
                selectable.Rotation = new Vector3((float)Program.main.numericSelectionRotationX.Value, (float)Program.main.numericSelectionRotationY.Value, (float)Program.main.numericSelectionRotationZ.Value);
            }
            //selectedDrawable.Rotation = new Vector3((float)Program.main.numericSelectionRotationX.Value, (float)Program.main.numericSelectionRotationY.Value, (float)Program.main.numericSelectionRotationZ.Value);

            Renderer.UpdateView();
            Program.GLControl.Invalidate();
        }

        public static void DrawSelectionScene()
        {
            GL.ClearColor(1, 1, 1, 1);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.UseProgram(shader.ProgramID);
            shader.LinkAttrib3(Renderer.pos_buffer, "inPos", false);

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, Renderer.ind_buffer);

            int indiceat = 0;
            for (int i = 0; i < Drawable.Drawables.Count; i++)
            {
                Drawable drawable = Drawable.Drawables[i];

                ISelectable selectable = drawable as ISelectable;

                if (drawable.Visible && selectable != null)
                {
                    if (!drawable.Mesh.DrawInFront)
                        DrawDrawable(drawable, indiceat, i);
                }
                indiceat += drawable.Mesh.IndiceCount;
            }

            indiceat = 0;
            GL.Clear(ClearBufferMask.DepthBufferBit);
            for (int i = 0; i < Drawable.Drawables.Count; i++)
            {
                Drawable drawable = Drawable.Drawables[i];

                ISelectable selectable = drawable as ISelectable;

                if (drawable.Visible && selectable != null)
                {
                    if (drawable.Mesh.DrawInFront)
                        DrawDrawable(drawable, indiceat, i);
                }
                indiceat += drawable.Mesh.IndiceCount;
            }

            // Wait until all the pending drawing commands are really done.
            // Ultra-mega-over slow ! 
            // There are usually a long time between glDrawElements() and
            // all the fragments completely rasterized.
            GL.Flush();
            GL.Finish();

            GL.PixelStore(PixelStoreParameter.UnpackAlignment, 1);
        }

        public static ISelectable GetObjectAtPixel(int x, int y)
        {
            DrawSelectionScene();

            byte[] pixel = new byte[3];
            GL.ReadPixels(x, y, 1, 1, PixelFormat.Rgb, PixelType.UnsignedByte, pixel);

            // Convert the color back to an integer ID
            int pickedID =
                pixel[0] +
                pixel[1] * 256 +
                pixel[2] * 256 * 256;

            ISelectable objectAtPixel = null;

            if (pickedID == 0x00FFFFFF) //Background
                objectAtPixel = null;
            else
                if(pickedID <= Drawable.Drawables.Count - 1)
                    objectAtPixel = Drawable.Drawables[pickedID] as ISelectable;

            GL.ClearColor(Renderer.BackgroundColor.X, Renderer.BackgroundColor.Y, Renderer.BackgroundColor.Z, 1);

            //Program.GLControl.SwapBuffers();
            return objectAtPixel;
        }

        public static List<ISelectable> GetObjectsInRectangle(int x, int y, int width, int height)
        {
            DrawSelectionScene();

            byte[,,] pixel = new byte[width, height, 4];
            GL.ReadPixels(x, y, width, height, PixelFormat.Rgba, PixelType.UnsignedByte, pixel);

            List<ISelectable> objects = new List<ISelectable>();

            for(int pixelX = 0; pixelX < width; pixelX++)
            {
                for (int pixelY = 0; pixelY < height; pixelY++)
                {
                    // Convert the color back to an integer ID
                    int pickedID =
                        pixel[pixelX, pixelY, 0] +
                        pixel[pixelX, pixelY, 1] * 256 +
                        pixel[pixelX, pixelY, 2] * 256 * 256;

                    ISelectable objectAtPixel = null;

                    if (pickedID == 0x00FFFFFF) //Background
                        objectAtPixel = null;
                    else
                    {
                        if (pickedID <= Drawable.Drawables.Count - 1)
                            objectAtPixel = Drawable.Drawables[pickedID] as ISelectable;

                        if (objectAtPixel == null)
                            continue;

                        if (objects.Contains(objectAtPixel))
                            continue;

                        objects.Add(objectAtPixel);
                    }
                }
            }

            GL.ClearColor(Renderer.BackgroundColor.X, Renderer.BackgroundColor.Y, Renderer.BackgroundColor.Z, 1);

            //Program.GLControl.SwapBuffers();
            return objects;
        }

        private static void DrawDrawable(Drawable drawable, int index, int id)
        {
            // Convert "i", the integer mesh ID, into an RGB color
            int r = (id & 0x000000FF) >> 0;
            int g = (id & 0x0000FF00) >> 8;
            int b = (id & 0x00FF0000) >> 16;
            GL.Uniform3(shader.GetUniform("pickingColor"), (float)r / 255, (float)g / 255, (float)b / 255);

            Mesh mesh = drawable.Mesh;

            GL.PointSize(mesh.DotSize * 2); //Render larger dots for easier selection of pebbles
            GL.LineWidth(mesh.LineWidth);

            Matrix4 model = mesh.ModelMatrix;
            Matrix4 camera = Program.Camera.GetViewMatrix();
            Matrix4 projection = Matrix4.Identity;
            if (!Program.Camera.Orthographic)
                projection = Matrix4.CreatePerspectiveFieldOfView(Program.Camera.FieldOfView, (float)Program.GLControl.Width / (float)Program.GLControl.Height, Program.Camera.NearClipDistance, Program.Camera.ClipDistance);
            else
                projection = Matrix4.CreateOrthographic((float)(Program.GLControl.Width / Program.Camera.OrthographicSize), (float)(Program.GLControl.Height / Program.Camera.OrthographicSize), Program.Camera.NearClipDistance, Program.Camera.ClipDistance);

            GL.UniformMatrix4(shader.GetUniform("inMatM"), false, ref model);
            GL.UniformMatrix4(shader.GetUniform("inMatV"), false, ref camera);
            GL.UniformMatrix4(shader.GetUniform("inMatP"), false, ref projection);

            GL.DrawElements(mesh.BeginMode, mesh.IndiceCount, DrawElementsType.UnsignedInt, index * sizeof(int));
        }
    }

    enum DraggingState
    {
        NONE,
        MOVING_X,
        MOVING_Y,
        MOVING_Z,
        ROTATING_X,
        ROTATING_Y,
        ROTATING_Z,
    }

    public enum GizmoMode
    {
        TRANSLATION,
        ROTATION,
    }
}
