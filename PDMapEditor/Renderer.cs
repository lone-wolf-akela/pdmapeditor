using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace PDMapEditor
{
    static class Renderer
    {
        public static Matrix4 View = Matrix4.Identity;
        public static Matrix4 Projection = Matrix4.Identity;
        public static Matrix4 ViewProjection = Matrix4.Identity;

        private static Shader shader;

        private static Vector3 backgroundColor = new Vector3(0.05f, 0.05f, 0.05f);
        public static Vector3 BackgroundColor { get { return backgroundColor; } set { backgroundColor = value; GL.ClearColor(value.X, value.Y, value.Z, 1); } }

        private static bool displayFog;
        public static bool DisplayFog { get { return displayFog; } set { displayFog = value; if (value && Map.FogActive) BackgroundColor = new Vector3(Map.FogColor.X, Map.FogColor.Y, Map.FogColor.Z); else BackgroundColor = new Vector3(0.05f); } }

        public static int pos_buffer = 0;
        static int col_buffer = 0;
        static int uv0_buffer = 0;
        public static int ind_buffer = 0;

        public static void Init()
        {
            BackgroundColor = new Vector3(0.05f, 0.05f, 0.05f);

            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.Blend);
            GL.Enable(EnableCap.AlphaTest);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

            GL.LineWidth(1);

            // Gen buffers
            GL.GenBuffers(1, out pos_buffer);
            GL.GenBuffers(1, out uv0_buffer);
            GL.GenBuffers(1, out col_buffer);
            GL.GenBuffers(1, out ind_buffer);

            shader = new Shader("editor.vs", "editor.fs", true);

            Log.WriteLine("Renderer initialized.");
        }

        public static void UpdateMeshData()
        {
            List<Vector3> editor_verts = new List<Vector3>();
            List<Vector3> editor_colors = new List<Vector3>();
            List<Vector2> editor_uv0 = new List<Vector2>();
            List<int> editor_inds = new List<int>();
            int editor_vertcount = 0;

            List<Drawable> newList = new List<Drawable>();
            foreach (Drawable drawable in Drawable.Drawables)
                if(!drawable.Mesh.DrawInFront)
                    if (!drawable.Mesh.Material.Translucent)
                        newList.Add(drawable);

            foreach (Drawable drawable in Drawable.Drawables)
                if (!drawable.Mesh.DrawInFront)
                    if (drawable.Mesh.Material.Translucent)
                        newList.Add(drawable);

            foreach (Drawable drawable in Drawable.Drawables)
                if (drawable.Mesh.DrawInFront)
                    if (!drawable.Mesh.Material.Translucent)
                        newList.Add(drawable);

            foreach (Drawable drawable in Drawable.Drawables)
                if (drawable.Mesh.DrawInFront)
                    if (drawable.Mesh.Material.Translucent)
                        newList.Add(drawable);

            Drawable.Drawables = newList;

            foreach (Drawable drawable in Drawable.Drawables)
            {
                editor_verts.AddRange(drawable.Mesh.Vertices);
                editor_uv0.AddRange(drawable.Mesh.TextureCoords);
                editor_colors.AddRange(drawable.Mesh.Colors);
                editor_inds.AddRange(drawable.Mesh.GetIndices(editor_vertcount).ToList());
                editor_vertcount += drawable.Mesh.VertexCount;
            }
            Vector3[] vertdata = editor_verts.ToArray();
            Vector2[] uv0data = editor_uv0.ToArray();
            Vector3[] coldata = editor_colors.ToArray();
            int[] indicedata = editor_inds.ToArray();

            BindBufferData(pos_buffer, vertdata, false);
            BindBufferData(uv0_buffer, uv0data, false);
            BindBufferData(col_buffer, coldata, false);

            // Buffer index data
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ind_buffer);
            GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)(indicedata.Length * sizeof(int)), indicedata, BufferUsageHint.StaticDraw);
        }

        private static void BindBufferData(int buffer, Vector2[] data, bool normalized)
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, buffer);
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(data.Length * Vector2.SizeInBytes), data, BufferUsageHint.StaticDraw);
        }

        private static void BindBufferData(int buffer, Vector3[] data, bool normalized)
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, buffer);
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(data.Length * Vector3.SizeInBytes), data, BufferUsageHint.StaticDraw);
        }

        private static void BindBufferData(int buffer, Vector4[] data, bool normalized)
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, buffer);
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(data.Length * Vector4.SizeInBytes), data, BufferUsageHint.StaticDraw);
        }

        public static void Render()
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.UseProgram(shader.ProgramID);
            shader.LinkAttrib3(pos_buffer, "inPos", false);
            shader.LinkAttrib3(col_buffer, "inColor", false);
            shader.LinkAttrib2(uv0_buffer, "inUV0", false);

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ind_buffer);

            //Fog uniforms
            if (Map.FogActive && DisplayFog)
            {
                GL.Uniform1(shader.GetUniform("fogParams.active"), 1);
                GL.Uniform4(shader.GetUniform("fogParams.color"), Map.FogColor);
                GL.Uniform1(shader.GetUniform("fogParams.start"), Map.FogStart);
                GL.Uniform1(shader.GetUniform("fogParams.end"), Map.FogEnd);
                GL.Uniform1(shader.GetUniform("fogParams.density"), Map.FogDensity);
                int equation = 0;
                switch (Map.FogType)
                {
                    case "linear":
                        equation = 0;
                        break;
                    case "exp":
                        equation = 1;
                        break;
                    case "exp2":
                        equation = 2;
                        break;
                }
                GL.Uniform1(shader.GetUniform("fogParams.equation"), equation);
            }
            else
                GL.Uniform1(shader.GetUniform("fogParams.active"), 0);

            int indiceat = 0;
            foreach (Drawable drawable in Drawable.Drawables)
            {
                if(!drawable.Mesh.DrawInFront)
                    if(!drawable.Mesh.Material.Translucent)
                        indiceat += DrawDrawable(drawable, indiceat);
            }

            GL.DepthMask(false);
            foreach (Drawable drawable in Drawable.Drawables)
            {
                if (!drawable.Mesh.DrawInFront)
                    if (drawable.Mesh.Material.Translucent)
                        indiceat += DrawDrawable(drawable, indiceat);
            }
            GL.DepthMask(true);

            //Draw in front
            GL.Clear(ClearBufferMask.DepthBufferBit);
            foreach (Drawable drawable in Drawable.Drawables)
            {
                if (drawable.Mesh.DrawInFront)
                    if (!drawable.Mesh.Material.Translucent)
                        indiceat += DrawDrawable(drawable, indiceat);
            }

            GL.DepthMask(false);
            foreach (Drawable drawable in Drawable.Drawables)
            {
                if (drawable.Mesh.DrawInFront)
                    if (drawable.Mesh.Material.Translucent)
                        indiceat += DrawDrawable(drawable, indiceat);
            }
            GL.DepthMask(true);




            Program.GLControl.SwapBuffers();
        }

        private static int DrawDrawable(Drawable drawable, int index)
        {
            if (drawable.Visible)
            {
                if(drawable.Mesh.Wireframe)
                    GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);
                else
                    GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);

                GL.PointSize(drawable.Mesh.DotSize);
                GL.LineWidth(drawable.Mesh.LineWidth);

                Matrix4 model = drawable.Mesh.ModelMatrix;
                Matrix4 camera = Program.Camera.GetViewMatrix();

                Matrix4 ModelViewMatrix = model * camera;
                GL.UniformMatrix4(shader.GetUniform("inMatM"), false, ref model);
                GL.UniformMatrix4(shader.GetUniform("inMatV"), false, ref camera);
                GL.UniformMatrix4(shader.GetUniform("inMatP"), false, ref Projection);
                GL.UniformMatrix4(shader.GetUniform("inMatMV"), false, ref ModelViewMatrix);

                Texture texture = null;
                if (drawable.Mesh.Material != null)
                {
                    texture = drawable.Mesh.Material.DiffuseTexture;
                    Vector4 diffuse = new Vector4(drawable.Mesh.Material.DiffuseColor, drawable.Mesh.Material.Opacity);

                    GL.Uniform4(shader.GetUniform("matDiffuse"), ref diffuse);
                }
                else
                {
                    GL.Uniform4(shader.GetUniform("matDiffuse"), 1f, 1f, 1f, 1f);
                }

                if (texture != null)
                {
                    GL.ActiveTexture(TextureUnit.Texture0);
                    GL.BindTexture(TextureTarget.Texture2D, texture.ID);
                    GL.Uniform1(shader.GetUniform("inTexMat"), 0);
                    GL.Uniform1(shader.GetUniform("isTextured"), 1); //Tell shader to use texture colors
                }
                else
                {
                    GL.Uniform1(shader.GetUniform("isTextured"), 0); //Tell shader to use vertex colors
                }

                GL.DrawElements(drawable.Mesh.BeginMode, drawable.Mesh.IndiceCount, DrawElementsType.UnsignedInt, index * sizeof(int));
            }
            return drawable.Mesh.IndiceCount;
        }

        public static void UpdateView()
        {
            View = Program.Camera.GetViewMatrix();

            float aspectRatio = (float)Program.GLControl.Width / (float)Program.GLControl.Height;
            float aspectRatioWidthOrtho = (float)(Program.GLControl.Width / Program.Camera.OrthographicSize);
            float aspectRatioHeightOrtho = (float)(Program.GLControl.Height / Program.Camera.OrthographicSize);

            if (!Program.Camera.Orthographic)
                Projection = Matrix4.CreatePerspectiveFieldOfView(Program.Camera.FieldOfView, aspectRatio, Program.Camera.NearClipDistance, Program.Camera.ClipDistance);
            else
                Projection = Matrix4.CreateOrthographic(aspectRatioWidthOrtho, aspectRatioHeightOrtho, Program.Camera.NearClipDistance, Program.Camera.ClipDistance);

            ViewProjection = View * Projection;

            // Update model view matrices
            foreach (Drawable drawable in Drawable.Drawables)
            {
                if (drawable.Visible)
                {
                    drawable.Mesh.CalculateModelMatrix();
                    drawable.Mesh.ModelViewProjectionMatrix = drawable.Mesh.ModelMatrix * ViewProjection;
                }
            }
        }

        public static void Resize()
        {
            GL.Viewport(Program.GLControl.ClientRectangle.X, Program.GLControl.ClientRectangle.Y, Program.GLControl.ClientRectangle.Width, Program.GLControl.ClientRectangle.Height);
            UpdateView();
        }
    }
}