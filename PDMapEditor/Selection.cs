using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;
using OpenTK;
using System.Media;

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

        //GUI
        private static bool ignorePositionChange;
        private static bool ignoreRotationChange;

        private static bool ignoreGizmoModeChange;
        private static object comboGizmoModeRotationItem;

        private static ISelectable selected;
        public static ISelectable Selected { get { return selected; } set { selected = value; UpdateSelectionGUI(); UpdateSelectionGizmos(); } }

        public static void Init()
        {
            shader = new Shader("picking.vs", "picking.fs", true);

            //GUI
            Program.main.comboGizmoMode.Items.Add("Translation");
            Program.main.comboGizmoMode.Items.Add("Rotation");
            comboGizmoModeRotationItem = Program.main.comboGizmoMode.Items[1];

            Program.main.comboGizmoMode.SelectedIndex = 0;
            Program.main.comboGizmoMode.SelectedIndexChanged += new EventHandler(comboGizmoMode_SelectedIndexChanged);

            Program.main.tabControlLeft.TabPages.Remove(Program.main.tabSelection);

            Program.main.numericSelectionPositionX.ValueChanged += new System.EventHandler(PositionChanged);
            Program.main.numericSelectionPositionY.ValueChanged += new System.EventHandler(PositionChanged);
            Program.main.numericSelectionPositionZ.ValueChanged += new System.EventHandler(PositionChanged);

            Program.main.numericSelectionRotationX.ValueChanged += new System.EventHandler(RotationChanged);
            Program.main.numericSelectionRotationY.ValueChanged += new System.EventHandler(RotationChanged);
            Program.main.numericSelectionRotationZ.ValueChanged += new System.EventHandler(RotationChanged);
        }

        public static void CreateGizmos()
        {
            //Gizmos
            gizmoPosX = new Gizmo(Vector3.Zero, Mesh.GizmoXPos);
            gizmoPosX.Mesh.Scale = new Vector3(25);
            gizmoPosX.Visible = false;
            gizmoLineX = new Line(Vector3.Zero + new Vector3(-100000, 0, 0), Vector3.Zero + new Vector3(100000, 0, 0), new Vector3(1, 0, 0));
            gizmoLineX.Visible = false;

            gizmoPosY = new Gizmo(Vector3.Zero, Mesh.GizmoYPos);
            gizmoPosY.Mesh.Scale = new Vector3(25);
            gizmoPosY.Visible = false;
            gizmoLineY = new Line(Vector3.Zero + new Vector3(0, -100000, 0), Vector3.Zero + new Vector3(0, 100000, 0), new Vector3(0, 1, 0));
            gizmoLineY.Visible = false;

            gizmoPosZ = new Gizmo(Vector3.Zero, Mesh.GizmoZPos);
            gizmoPosZ.Mesh.Scale = new Vector3(25);
            gizmoPosZ.Visible = false;
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

                Renderer.UpdateView();
                Program.GLControl.Invalidate();
            }
            else if (objectAtMouse == gizmoRotY)
            {
                gizmoLineY.Visible = true;
                gizmoLineY.Position = gizmoRotY.Position;
                draggingState = DraggingState.ROTATING_Y;

                Renderer.UpdateView();
                Program.GLControl.Invalidate();
            }
            else if (objectAtMouse == gizmoRotZ)
            {
                gizmoLineZ.Visible = true;
                gizmoLineZ.Position = gizmoRotZ.Position;
                draggingState = DraggingState.ROTATING_Z;

                Renderer.UpdateView();
                Program.GLControl.Invalidate();
            }
        }

        public static void LeftMouseUp(int x, int y)
        {
            if(draggingState == DraggingState.NONE)
            {
                ISelectable objectAtMouse = GetObjectAtPixel(x, y);
                Drawable drawable = objectAtMouse as Drawable;

                if (drawable != null)
                {
                    if (drawable != gizmoPosX && drawable != gizmoPosY && drawable != gizmoPosZ && drawable != gizmoRotX && drawable != gizmoRotY && drawable != gizmoRotZ)
                        Selected = objectAtMouse;
                }
                else
                    Selected = null;
            }

            draggingState = DraggingState.NONE;
            gizmoLineX.Visible = false;
            gizmoLineY.Visible = false;
            gizmoLineZ.Visible = false;

            Renderer.UpdateView();
            Program.GLControl.Invalidate();
        }

        private static void UpdateSelectionGUI()
        {
            Program.main.groupSelectionRotation.Visible = false;
            if (Selected == null)
            {
                Program.main.tabControlLeft.SelectedIndex = 0;
                Program.main.tabControlLeft.TabPages.Remove(Program.main.tabSelection);
                return;
            }

            if(!Program.main.tabControlLeft.TabPages.Contains(Program.main.tabSelection))
                Program.main.tabControlLeft.TabPages.Add(Program.main.tabSelection);

            Program.main.tabControlLeft.SelectedIndex = 1;
            Drawable selectedDrawable = Selected as Drawable;

            UpdatePositionGUI();
            if (Selected as Pebble == null && Selected as DustCloud == null) //Don't allow rotation changes if the object cannot be rotated
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

            Program.GLControl.Focus();
        }
        private static void UpdatePositionGUI()
        {
            Drawable selectedDrawable = Selected as Drawable;

            ignorePositionChange = true;
            Program.main.numericSelectionPositionX.Value = (decimal)selectedDrawable.Position.X;
            Program.main.numericSelectionPositionY.Value = (decimal)selectedDrawable.Position.Y;
            Program.main.numericSelectionPositionZ.Value = (decimal)selectedDrawable.Position.Z;
            ignoreRotationChange = false;
        }
        private static void UpdateRotationGUI()
        {
            Drawable selectedDrawable = Selected as Drawable;

            ignoreRotationChange = true;
            Program.main.numericSelectionRotationX.Value = (decimal)selectedDrawable.Rotation.X;
            Program.main.numericSelectionRotationY.Value = (decimal)selectedDrawable.Rotation.Y;
            Program.main.numericSelectionRotationZ.Value = (decimal)selectedDrawable.Rotation.Z;
            ignoreRotationChange = false;
        }

        private static void UpdateSelectionGizmos()
        {
            Drawable selectedDrawable = Selected as Drawable;

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

            if (selectedDrawable == null)
                return;

            //Gizmos
            gizmoPosX.Position = selectedDrawable.Position;
            gizmoPosY.Position = selectedDrawable.Position;
            gizmoPosZ.Position = selectedDrawable.Position;

            gizmoRotX.Position = selectedDrawable.Position;
            gizmoRotY.Position = selectedDrawable.Position;
            gizmoRotZ.Position = selectedDrawable.Position;

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
            Drawable selectedDrawable = Selected as Drawable;
            if (selectedDrawable == null)
                return;

            Vector3 scale = Vector3.One;
            if (!Program.Camera.Orthographic)
            {
                float cameraDistanceApprox = (Program.Camera.Position - selectedDrawable.Position).LengthFast;
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
                if (Selected as Pebble == null && Selected as DustCloud == null) //Don't allow rotation changes if the object cannot be rotated
                {
                    GizmoMode = GizmoMode.ROTATION;
                    Renderer.UpdateView();
                    Program.GLControl.Invalidate();
                }
                else
                    SystemSounds.Beep.Play();
            }
        }

        public static void UpdateDragging(int mouseX, int mouseY)
        {
            Drawable selectedDrawable = Selected as Drawable;

            if (draggingState != DraggingState.NONE)
            {
                float mouseXDelta = mouseX - lastMouseX;
                float mouseYDelta = mouseY - lastMouseY;
                float cameraDistanceApprox = (Program.Camera.Position - selectedDrawable.Position).LengthFast;

                float posX = selectedDrawable.Position.X;
                float posY = selectedDrawable.Position.Y;
                float posZ = selectedDrawable.Position.Z;

                float rotX = selectedDrawable.Rotation.X;
                float rotY = selectedDrawable.Rotation.Y;
                float rotZ = selectedDrawable.Rotation.Z;

                float speedMultiplier = 1;

                if (!Program.Camera.Orthographic)
                    speedMultiplier = cameraDistanceApprox / 800;
                else
                    speedMultiplier = 1 / Program.Camera.OrthographicSize; //The orthographic size gets smaller the more you zoom out, so the speed should raise the lower the size gets

                switch (draggingState)
                {
                    //Position
                    case DraggingState.MOVING_X:
                        if(Program.Camera.Position.Z > selectedDrawable.Position.Z) //To fix the user experience
                            posX = selectedDrawable.Position.X + mouseXDelta * speedMultiplier;
                        else
                            posX = selectedDrawable.Position.X - mouseXDelta * speedMultiplier;
                        break;
                    case DraggingState.MOVING_Y:
                        posY = selectedDrawable.Position.Y + mouseYDelta * speedMultiplier;
                        break;
                    case DraggingState.MOVING_Z:
                        if (Program.Camera.Position.X > selectedDrawable.Position.X) //To fix the user experience
                            posZ = selectedDrawable.Position.Z - mouseXDelta * speedMultiplier;
                        else
                            posZ = selectedDrawable.Position.Z + mouseXDelta * speedMultiplier;
                        break;

                    //Rotation
                    case DraggingState.ROTATING_X:
                        if (Program.Camera.Position.X > selectedDrawable.Position.X) //To fix the user experience
                            rotX = selectedDrawable.Rotation.X - mouseXDelta;
                        else
                            rotX = selectedDrawable.Rotation.X + mouseXDelta;
                        break;
                    case DraggingState.ROTATING_Y:
                        rotY = selectedDrawable.Rotation.Y + mouseXDelta;
                        break;
                    case DraggingState.ROTATING_Z:
                        if (Program.Camera.Position.Z > selectedDrawable.Position.Z) //To fix the user experience
                            rotZ = selectedDrawable.Rotation.Z - mouseXDelta;
                        else
                            rotZ = selectedDrawable.Rotation.Z + mouseXDelta;
                        break;
                }
                selectedDrawable.Position = new Vector3(posX, posY, posZ);
                selectedDrawable.Rotation = new Vector3(rotX, rotY, rotZ);

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
            if (Selected == null || ignorePositionChange)
                return;

            Drawable selectedDrawable = Selected as Drawable;
            selectedDrawable.Position = new Vector3((float)Program.main.numericSelectionPositionX.Value, (float)Program.main.numericSelectionPositionY.Value, (float)Program.main.numericSelectionPositionZ.Value);

            UpdateSelectionGizmos();
            Renderer.UpdateView();
            Program.GLControl.Invalidate();
        }

        private static void RotationChanged(object sender, EventArgs e)
        {
            if (Selected == null || ignoreRotationChange)
                return;

            Drawable selectedDrawable = Selected as Drawable;
            selectedDrawable.Rotation = new Vector3((float)Program.main.numericSelectionRotationX.Value, (float)Program.main.numericSelectionRotationY.Value, (float)Program.main.numericSelectionRotationZ.Value);

            Renderer.UpdateView();
            Program.GLControl.Invalidate();
        }

        public static ISelectable GetObjectAtPixel(int x, int y)
        {
            GL.ClearColor(1, 1, 1, 1);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.UseProgram(shader.ProgramID);
            shader.LinkAttrib3(Renderer.pos_buffer, "inPos", false);

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, Renderer.ind_buffer);

            int indiceat = 0;
            for (int i = 0; i < Drawable.Drawables.Count; i ++)
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
