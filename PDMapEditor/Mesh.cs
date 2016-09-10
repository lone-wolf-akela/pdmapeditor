using Assimp;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;

namespace PDMapEditor
{
    public class Mesh
    {
        public static List<Mesh> Meshes = new List<Mesh>();

        #region AssMeshes
        public static Assimp.Mesh Icosphere;
        public static Assimp.Mesh IcosphereHigh;
        public static Assimp.Mesh Asteroid;
        public static Assimp.Mesh Point;
        public static Assimp.Mesh Cube;

        public static Assimp.Mesh Text000;
        public static Assimp.Mesh Text090;
        public static Assimp.Mesh Text180;
        public static Assimp.Mesh Text270;

        public static Assimp.Mesh GizmoXPos;
        public static Assimp.Mesh GizmoYPos;
        public static Assimp.Mesh GizmoZPos;

        public static Assimp.Mesh GizmoXRot;
        public static Assimp.Mesh GizmoYRot;
        public static Assimp.Mesh GizmoZRot;
        #endregion

        public Assimp.Mesh AssMesh;
        public Material Material = new Material();

        public Vector3 Position = Vector3.Zero;
        public Vector3 Rotation = Vector3.Zero;
        public Vector3 Scale = Vector3.One;

        public Vector3 LastScale = Vector3.One;

        public bool Wireframe = false;
        public bool VertexColored = true;
        public bool Persistent = false;
        public bool DrawInFront = false;

        public BeginMode BeginMode = BeginMode.Triangles;

        public float LineWidth = 1;
        public float DotSize = 1;

        public Matrix4 ModelMatrix = Matrix4.Identity;
        public Matrix4 ModelViewProjectionMatrix = Matrix4.Identity;

        public Vector3[] Vertices;
        public Vector3[] Normals;
        public int[] Indices;
        public Vector3[] Colors;
        public Vector2[] TextureCoords;

        public virtual int VertexCount { get { return AssMesh.VertexCount; } }
        public virtual int IndiceCount { get { return Indices.Length; } }

        public Mesh()
        {
            Meshes.Add(this);
        }

        public Mesh(Vector3 position)
        {
            this.Position = position;

            Meshes.Add(this);
        }

        public Mesh(Vector3 position, Vector3 rotation, Assimp.Mesh assMesh)
        {
            this.Position = position;
            this.Rotation = rotation;

            AssMesh = assMesh;

            Vertices = GetVertices();
            Normals = GetNormals();
            Indices = GetIndices();
            Colors = GetColorData();
            TextureCoords = GetTextureCoords();

            Meshes.Add(this);
        }

        public void Remove()
        {
            Meshes.Remove(this);
        }

        public virtual Vector3[] GetVertices()
        {
            List<Vector3> verticesList = new List<Vector3>();
            foreach (Vector3D vertex in AssMesh.Vertices)
            {
                verticesList.Add(new Vector3(vertex.X, vertex.Y, vertex.Z));
            }
            return verticesList.ToArray();
        }

        public virtual Vector3[] GetNormals()
        {
            List<Vector3> normalsList = new List<Vector3>();
            foreach (Vector3D normal in AssMesh.Normals)
            {
                normalsList.Add(new Vector3(normal.X, normal.Y, normal.Z));
            }
            return normalsList.ToArray();
        }

        public virtual int[] GetIndices(int offset = 0)
        {
            int[] indices = AssMesh.GetIndices();

            if (offset != 0)
            {
                for (int i = 0; i < indices.Length; i++)
                {
                    indices[i] += offset;
                }
            }

            return indices;
        }

        public virtual Vector3[] GetColorData()
        {
            if (AssMesh.VertexColorChannelCount > 0)
            {
                List<Vector3> colors = new List<Vector3>();

                foreach (Color4D color in AssMesh.VertexColorChannels[0])
                {
                    colors.Add(new Vector3(color.R, color.G, color.B));
                }

                return colors.ToArray();
            }
            else
            {
                List<Vector3> colors = new List<Vector3>();
                for(int i = 0; i < VertexCount; i ++)
                {
                    colors.Add(new Vector3(1));
                }

                return colors.ToArray();
            }
        }

        public virtual Vector2[] GetTextureCoords()
        {
            if (AssMesh.TextureCoordinateChannelCount > 0)
            {
                List<Vector2> coords = new List<Vector2>();

                foreach (Vector3D coord in AssMesh.TextureCoordinateChannels[0])
                {
                    coords.Add(new Vector2(coord.X, coord.Y));
                }

                return coords.ToArray();
            }
            else
            {
                return new Vector2[VertexCount];
            }
        }

        /// <summary>
        /// Calculates the model matrix from transforms
        /// </summary>
        public virtual void CalculateModelMatrix()
        {
            ModelMatrix = Matrix4.CreateScale(Scale) * Matrix4.CreateRotationX(MathHelper.DegreesToRadians(Rotation.X)) * Matrix4.CreateRotationY(MathHelper.DegreesToRadians(Rotation.Y)) * Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(Rotation.Z)) * Matrix4.CreateTranslation(Position);
        }
    }
}
