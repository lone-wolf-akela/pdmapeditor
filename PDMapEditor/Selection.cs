using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;
using OpenTK;

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

        private static DraggingState draggingState = DraggingState.NONE;

        private static float lastMouseX, lastMouseY;

        //GUI
        private static bool ignorePositionChange;
        private static bool ignoreRotationChange;

        private static ISelectable selected;
        public static ISelectable Selected { get { return selected; } set { selected = value; UpdateSelectionGUI(); UpdateSelectionGizmos(); } }

        public static void Init()
        {
            shader = new Shader("picking.vs", "picking.fs", true);

            //GUI
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
        }

        public static void LeftMouseUp(int x, int y)
        {
            if(draggingState == DraggingState.NONE)
            {
                ISelectable objectAtMouse = GetObjectAtPixel(x, y);
                Selected = objectAtMouse;
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
            if (Selected as Pebble == null && Selected as DustCloud == null)
            {
                UpdateRotationGUI();
                Program.main.groupSelectionRotation.Visible = true;
            }
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
            if(selectedDrawable == null)
            {
                gizmoPosX.Visible = false;
                gizmoPosY.Visible = false;
                gizmoPosZ.Visible = false;
                return;
            }

            //Gizmos
            gizmoPosX.Visible = true;
            gizmoPosX.Position = selectedDrawable.Position;

            gizmoPosY.Visible = true;
            gizmoPosY.Position = selectedDrawable.Position;

            gizmoPosZ.Visible = true;
            gizmoPosZ.Position = selectedDrawable.Position;

            UpdateSelectionGizmoScale();
        }

        public static void UpdateSelectionGizmoScale()
        {
            Drawable selectedDrawable = Selected as Drawable;
            if (selectedDrawable == null)
                return;

            float cameraDistanceApprox = (Program.Camera.Position - selectedDrawable.Position).LengthFast;
            Vector3 scale = new Vector3(cameraDistanceApprox / 700);

            gizmoPosX.Mesh.Scale = new Vector3(scale);
            gizmoPosY.Mesh.Scale = new Vector3(scale);
            gizmoPosZ.Mesh.Scale = new Vector3(scale);
        }

        public static void UpdateDragging(int mouseX, int mouseY)
        {
            Drawable selectedDrawable = Selected as Drawable;

            if (draggingState == DraggingState.MOVING_X || draggingState == DraggingState.MOVING_Y || draggingState == DraggingState.MOVING_Z)
            {
                float mouseXDelta = mouseX - lastMouseX;
                float mouseYDelta = mouseY - lastMouseY;
                float cameraDistanceApprox = (Program.Camera.Position - selectedDrawable.Position).LengthFast;

                float posX = selectedDrawable.Position.X;
                float posY = selectedDrawable.Position.Y;
                float posZ = selectedDrawable.Position.Z;

                switch (draggingState)
                {
                    case DraggingState.MOVING_X:
                        posX = selectedDrawable.Position.X + mouseXDelta * (cameraDistanceApprox / 800);
                        break;
                    case DraggingState.MOVING_Y:
                        posY = selectedDrawable.Position.Y + mouseYDelta * (cameraDistanceApprox / 800);
                        break;
                    case DraggingState.MOVING_Z:
                        posZ = selectedDrawable.Position.Z - mouseXDelta * (cameraDistanceApprox / 800);
                        break;
                }
                selectedDrawable.Position = new Vector3(posX, posY, posZ);

                UpdatePositionGUI();
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

                Mesh mesh = drawable.Mesh;

                GL.PointSize(drawable.Mesh.DotSize * 2); //Render larger dots for easier selection of pebbles
                GL.LineWidth(drawable.Mesh.LineWidth);

                if (drawable.Visible && selectable != null)
                {
                    // Convert "i", the integer mesh ID, into an RGB color
                    int r = (i & 0x000000FF) >> 0;
                    int g = (i & 0x0000FF00) >> 8;
                    int b = (i & 0x00FF0000) >> 16;
                    GL.Uniform3(shader.GetUniform("pickingColor"), (float)r / 255, (float)g / 255, (float)b / 255);

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

                    GL.DrawElements(mesh.BeginMode, mesh.IndiceCount, DrawElementsType.UnsignedInt, indiceat * sizeof(int));
                }
                indiceat += mesh.IndiceCount;
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
    }

    enum DraggingState
    {
        NONE,
        MOVING_X,
        MOVING_Y,
        MOVING_Z,
    }
}
