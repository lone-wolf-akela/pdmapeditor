using OpenTK;
using OpenTK.Input;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace PDMapEditor
{
    public class Camera
    {
        public Vector3 Position = new Vector3(0, 0, 15);
        public Vector3 Orientation = new Vector3((float)Math.PI, 0f, 0f);

        public float NearClipDistance = 100f;
        public float ClipDistance = 500000;
        public float FieldOfView = 0.9599f;

        public float MinZoom = 1f;
        public float MaxZoom = 100000f;

        private bool orthographic;
        public bool Orthographic { get { return orthographic; } set { orthographic = value; Update(true); } }

        private float orthographicSize = 16;
        public float OrthographicSize { get { return orthographicSize; } set { orthographicSize = value; Update(); } }
        private float perspectiveZoom = 1;

        private float lastOrthographicSize;

        public float CalculatedZoom = 1;
        public float ZoomSpeed = 500000;
        public float PanSpeed = 0.9f;

        const float PAN_MIN_SPEED = 10000;

        public Vector3 LookAt = Vector3.Zero;
        public Vector3 Direction
        {
            get
            {
                return Program.Camera.LookAt - Program.Camera.Position;
            }
        }

        private float zoom = 1;
        public float Zoom { get { return zoom; } set { zoom = value; Update(); } }


        private float lastZoom;
        private Vector2 angles = new Vector2((float)Math.PI, (float)Math.PI);

        private float lastWheelPrecise;

        private System.Drawing.Point lastPos;
        private System.Drawing.Point pressPos;

        public void MouseDown(System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                MouseState mouse = Mouse.GetState();
                Cursor.Hide();
                pressPos = Cursor.Position;
            }
        }

        public void MouseUp(System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Cursor.Position = pressPos;
                Cursor.Show();
                pressPos = System.Drawing.Point.Empty;
            }
        }

        public void KeyDown()
        {
            if (ActionKey.IsDown(Action.TOGGLE_ORTHOGRAPHIC)) //Toggle orthographic view
            {
                this.Orthographic = !this.Orthographic;
                if (this.Orthographic)
                {
                    this.perspectiveZoom = this.Zoom;
                    this.Zoom = CalculatedZoom;
                }
                else
                    this.Zoom = perspectiveZoom;

                Program.main.UpdatePerspectiveOrthoCombo();
                Renderer.UpdateView();
                Program.GLControl.Invalidate();
            }
            else if (ActionKey.IsDown(Action.VIEW_FRONT))
            {
                angles.X = (float)Math.PI;
                angles.Y = (float)Math.PI;

                UpdatePosition();
                Renderer.UpdateView();
                Program.GLControl.Invalidate();
            }
            else if (ActionKey.IsDown(Action.VIEW_BACK))
            {
                angles.X = (float)Math.PI;
                angles.Y = 0;

                UpdatePosition();
                Renderer.UpdateView();
                Program.GLControl.Invalidate();
            }
            else if (ActionKey.IsDown(Action.VIEW_LEFT))
            {
                angles.X = (float)Math.PI;
                angles.Y = (float)-Math.PI / 2;

                UpdatePosition();
                Renderer.UpdateView();
                Program.GLControl.Invalidate();
            }
            else if (ActionKey.IsDown(Action.VIEW_RIGHT))
            {
                angles.X = (float)Math.PI;
                angles.Y = (float)Math.PI / 2;

                UpdatePosition();
                Renderer.UpdateView();
                Program.GLControl.Invalidate();
            }
            else if (ActionKey.IsDown(Action.VIEW_TOP))
            {
                angles.X = (float)Math.PI * 1.5f;
                angles.Y = (float)Math.PI;

                angles.X = (float)Utilities.Clamp(angles.X, Math.PI - Math.PI / 2, (Math.PI + Math.PI / 2) - 0.000001f);

                UpdatePosition();
                Renderer.UpdateView();
                Program.GLControl.Invalidate();
            }
            else if (ActionKey.IsDown(Action.VIEW_BOTTOM))
            {
                angles.X = 0;
                angles.Y = (float)Math.PI;

                angles.X = (float)Utilities.Clamp(angles.X, Math.PI - Math.PI / 2, (Math.PI + Math.PI / 2) - 0.000001f);

                UpdatePosition();
                Renderer.UpdateView();
                Program.GLControl.Invalidate();
            }
            else if (ActionKey.IsDown(Action.CAM_RESET))
            {
                ResetCamera();
            }
        }

        public void ResetCamera()
        {
            angles.X = (float)Math.PI / 2 + (float)Math.PI * 0.75f;
            angles.Y = (float)Math.PI;

            LookAt = Vector3.Zero;

            System.Drawing.Point position = Cursor.Position;
            lastPos = position;

            this.Orthographic = false;

            this.Zoom = CalculatedZoom;
            this.perspectiveZoom = this.Zoom;
            this.orthographicSize = 0.009f;

            UpdatePosition();
            Renderer.UpdateView();
            Program.GLControl.Invalidate();
            Program.main.UpdatePerspectiveOrthoCombo();
        }

        public void UpdatePanning()
        {
            Vector3 delta = Vector3.Zero;

            if (ActionKey.IsDown(Action.PAN_RIGHT))
                delta += new Vector3(Renderer.View.M11, Renderer.View.M21, Renderer.View.M31);
            else if (ActionKey.IsDown(Action.PAN_LEFT))
                delta += -new Vector3(Renderer.View.M11, Renderer.View.M21, Renderer.View.M31);
            if (ActionKey.IsDown(Action.PAN_FORWARDS))
                delta += new Vector3(Renderer.View.M12, 0, Renderer.View.M32); //Ignore x and z rotation of the matrix
            else if (ActionKey.IsDown(Action.PAN_BACKWARDS))
                delta += -new Vector3(Renderer.View.M12, 0, Renderer.View.M32); //Ignore x and z rotation of the matrix

            if (ActionKey.IsDown(Action.PAN_UP))
                delta += Vector3.UnitY;
            else if (ActionKey.IsDown(Action.PAN_DOWN))
                delta += -Vector3.UnitY;

            if (delta != Vector3.Zero)
            {
                float finalPanSpeed = 1;

                if(!Orthographic)
                    finalPanSpeed = (float)Math.Max(PanSpeed * zoom, PAN_MIN_SPEED);
                else
                    finalPanSpeed = (float)Math.Max(1 / OrthographicSize * 700, PAN_MIN_SPEED); //The orthographic size gets smaller the more you zoom out, so the speed should raise the lower the size gets

                LookAt += delta * finalPanSpeed * (float)Program.ElapsedSeconds;

                UpdatePosition();
                Renderer.UpdateView();
                Program.GLControl.Invalidate();
            }
        }

        public void Update(bool forceUpdate = false)
        {
            if (Program.GLControl == null)
                return;

            MouseState mouse = Mouse.GetState();
            System.Drawing.Point position = Cursor.Position;

            if (Program.GLControl.Focused || forceUpdate)
            {
                float zoomDelta = mouse.WheelPrecise - lastWheelPrecise;

                if (mouse.RightButton == OpenTK.Input.ButtonState.Pressed || forceUpdate)
                {
                    float deltaX = position.X - lastPos.X;
                    float deltaY = position.Y - lastPos.Y;

                    angles.X += deltaY * 0.01f;
                    angles.Y -= deltaX * 0.01f;

                    angles.X = (float)Utilities.Clamp(angles.X, Math.PI - Math.PI / 2, (Math.PI + Math.PI / 2) - 0.000001f);

                    Renderer.UpdateView();
                    Program.GLControl.Invalidate();
                }

                if (!this.Orthographic)
                {
                    zoom -= zoomDelta * ZoomSpeed * 0.01f;
                    zoom = Utilities.Clamp(zoom, MinZoom, MaxZoom);
                }
                else
                {
                    orthographicSize += zoomDelta * (orthographicSize / 25);
                    orthographicSize = Utilities.Clamp(orthographicSize, 0.0001f, 1);
                }
            }

            UpdatePosition();

            if (lastZoom != zoom)
            {
                Renderer.UpdateView();
                Program.GLControl.Invalidate();
            }

            if (lastOrthographicSize != orthographicSize)
            {
                Renderer.UpdateView();
                Program.GLControl.Invalidate();
            }

            lastPos = position;
            lastZoom = zoom;
            lastOrthographicSize = orthographicSize;
            lastWheelPrecise = mouse.WheelPrecise;
        }

        private void UpdatePosition()
        {
            Position = LookAt + Vector3.Transform(new Vector3(0, 0, Zoom), Matrix4.CreateRotationX(angles.X) * Matrix4.CreateRotationY(angles.Y));

            //Limit camera orbit position to map dimensions
            LookAt.X = Math.Min(LookAt.X, Map.MapDimensions.X);
            LookAt.X = Math.Max(LookAt.X, -Map.MapDimensions.X);

            LookAt.Y = Math.Min(LookAt.Y, Map.MapDimensions.Y);
            LookAt.Y = Math.Max(LookAt.Y, -Map.MapDimensions.Y);

            LookAt.Z = Math.Min(LookAt.Z, Map.MapDimensions.Z);
            LookAt.Z = Math.Max(LookAt.Z, -Map.MapDimensions.Z);

            Selection.UpdateSelectionGizmoScale();
        }

        public Matrix4 GetViewMatrix()
        {
            return Matrix4.LookAt(Position, LookAt, new Vector3(0, 1, 0));
        }
    }
}