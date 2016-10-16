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
        public static bool Initialized = false;

        public static Matrix4 View = Matrix4.Identity;
        public static Matrix4 Projection = Matrix4.Identity;
        public static Matrix4 ViewProjection = Matrix4.Identity;
        public static Matrix4 ViewProjectionInverted = Matrix4.Identity;

        private static Shader shader;
        private static Shader shader2D;

        private static Vector3 backgroundColor = new Vector3(0.05f, 0.05f, 0.05f);
        public static Vector3 BackgroundColor { get { return backgroundColor; } set { backgroundColor = value; GL.ClearColor(value.X, value.Y, value.Z, 1); } }

        private static bool displayFog;
        public static bool DisplayFog { get { return displayFog; } set { displayFog = value; if (value && Map.FogActive) BackgroundColor = new Vector3(Map.FogColor.X, Map.FogColor.Y, Map.FogColor.Z); else BackgroundColor = new Vector3(0.05f); } }

        public static int pos_buffer = 0;
        static int col_buffer = 0;
        static int uv0_buffer = 0;
        public static int ind_buffer = 0;

        static int pos_buffer_2D = 0;
        static int ind_buffer_2D = 0;

        public static void Init()
        {
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.Blend);
            GL.Disable(EnableCap.AlphaTest);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

            // Gen buffers
            GL.GenBuffers(1, out pos_buffer);
            GL.GenBuffers(1, out uv0_buffer);
            GL.GenBuffers(1, out col_buffer);
            GL.GenBuffers(1, out ind_buffer);

            //2D Buffers
            GL.GenBuffers(1, out pos_buffer_2D);
            GL.GenBuffers(1, out ind_buffer_2D);

            shader = new Shader("editor.vs", "editor.fs", true);
            shader2D = new Shader("2d.vs", "2d.fs", true);

            BackgroundColor = new Vector3(0.05f, 0.05f, 0.05f);

            Log.WriteLine("Renderer initialized.");
            Initialized = true;
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
                if (drawable.Mesh.DrawInBack)
                    newList.Add(drawable);

            foreach (Drawable drawable in Drawable.Drawables)
                if(!drawable.Mesh.DrawInFront)
                    if (!drawable.Mesh.Material.Translucent)
                        if (!drawable.Mesh.DrawInBack)
                            newList.Add(drawable);

            foreach (Drawable drawable in Drawable.Drawables)
                if (!drawable.Mesh.DrawInFront)
                    if (drawable.Mesh.Material.Translucent)
                        if (!drawable.Mesh.DrawInBack)
                            newList.Add(drawable);

            foreach (Drawable drawable in Drawable.Drawables)
                if (drawable.Mesh.DrawInFront)
                    if (!drawable.Mesh.Material.Translucent)
                        if (!drawable.Mesh.DrawInBack)
                            newList.Add(drawable);

            foreach (Drawable drawable in Drawable.Drawables)
                if (drawable.Mesh.DrawInFront)
                    if (drawable.Mesh.Material.Translucent)
                        if (!drawable.Mesh.DrawInBack)
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

        public static void Update2DMeshData()
        {
            List<Vector2> vertices = new List<Vector2>();
            List<int> indices = new List<int>();
            int vertexCount = 0;

            foreach (Mesh2D mesh in Mesh2D.Meshes2D)
            {
                vertices.AddRange(mesh.Vertices);
                indices.AddRange(mesh.GetIndices(vertexCount).ToList());
                vertexCount += mesh.VertexCount;
            }
            Vector2[] vertData = vertices.ToArray();
            int[] indexData = indices.ToArray();

            BindBufferData(pos_buffer_2D, vertData, false);

            // Buffer index data
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ind_buffer_2D);
            GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)(indexData.Length * sizeof(int)), indexData, BufferUsageHint.StaticDraw);
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
            //GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.Clear(ClearBufferMask.DepthBufferBit); //No need to clear the color buffer, because now we have a skybox

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
            //Draw in back
            GL.DepthMask(false);
            foreach (Drawable drawable in Drawable.Drawables)
            {
                if (drawable.Mesh.DrawInBack)
                    indiceat += DrawDrawable(drawable, indiceat);
            }
            GL.DepthMask(true);

            foreach (Drawable drawable in Drawable.Drawables)
            {
                if(!drawable.Mesh.DrawInFront)
                    if(!drawable.Mesh.Material.Translucent)
                        if (!drawable.Mesh.DrawInBack)
                            indiceat += DrawDrawable(drawable, indiceat);
            }

            GL.DepthMask(false);
            foreach (Drawable drawable in Drawable.Drawables)
            {
                if (!drawable.Mesh.DrawInFront)
                    if (drawable.Mesh.Material.Translucent)
                        if (!drawable.Mesh.DrawInBack)
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
        }

        public static void Render2D()
        {
            //2D Rendering
            GL.UseProgram(shader2D.ProgramID);
            shader2D.LinkAttrib2(pos_buffer_2D, "inPos", false);

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ind_buffer_2D);

            int indiceat = 0;
            foreach (Mesh2D mesh in Mesh2D.Meshes2D)
            {
                if (mesh.Visible)
                {
                    GL.PointSize(mesh.DotSize);
                    GL.LineWidth(mesh.LineWidth);

                    GL.DrawElements(mesh.BeginMode, mesh.IndiceCount, DrawElementsType.UnsignedInt, indiceat * sizeof(int));
                }
                indiceat += mesh.IndiceCount;
            }

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
                    GL.Uniform1(shader.GetUniform("textureFactor"), drawable.Mesh.Material.TextureFactor);

                    if (drawable.Mesh.Material.Opacity < 1)
                        GL.Enable(EnableCap.Blend);
                }
                else
                {
                    GL.Uniform4(shader.GetUniform("matDiffuse"), 1f, 1f, 1f, 1f);
                    GL.Disable(EnableCap.Blend);
                }

                if(drawable.Mesh.VertexColored)
                    GL.Uniform1(shader.GetUniform("vertexColored"), 1);
                else
                    GL.Uniform1(shader.GetUniform("vertexColored"), 0);

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
            ViewProjectionInverted = ViewProjection.Inverted();

            // Update model view matrices
            foreach (Drawable drawable in Drawable.Drawables)
            {
                if (drawable.Visible && (drawable.lastPosition != drawable.Position || drawable.lastRotation != drawable.Rotation || drawable.Mesh.Scale != drawable.Mesh.LastScale))
                {
                    drawable.Mesh.CalculateModelMatrix();

                    drawable.lastPosition = drawable.Position;
                    drawable.lastRotation = drawable.Rotation;
                    drawable.Mesh.LastScale = drawable.Mesh.Scale;
                }

                drawable.Mesh.ModelViewProjectionMatrix = drawable.Mesh.ModelMatrix * ViewProjection;
            }
        }

        public static void Resize()
        {
            GL.Viewport(Program.GLControl.ClientRectangle.X, Program.GLControl.ClientRectangle.Y, Program.GLControl.ClientRectangle.Width, Program.GLControl.ClientRectangle.Height);
            UpdateView();
        }
    }
}