using Assimp;
using OpenTK;
using System.Collections.Generic;

namespace PDMapEditor
{
    public class MeshIcosphere : Mesh
    {
        private Vector3 color;
        public Vector3 Color { get { return color; } set { color = value; Colors = GetColorData(); } }

        public override int VertexCount { get { return AssMesh.VertexCount; } }
        public override int IndiceCount { get { return Indices.Length; } }

        public MeshIcosphere(Vector3 position, Vector3 color) : this(position, color, false)
        {
        }

        public MeshIcosphere(Vector3 position, Vector3 color, bool detailed) : this(position, Vector3.Zero, color, detailed)
        {
        }

        public MeshIcosphere(Vector3 position, Vector3 rotation, Vector3 color, bool detailed) : base(position, Vector3.Zero, Mesh.IcosphereHigh)
        {
            this.Color = color;
        }

        public override Vector3[] GetVertices()
        {
            List<Vector3> verticesList = new List<Vector3>();
            foreach (Vector3D vertex in AssMesh.Vertices)
            {
                verticesList.Add(new Vector3(vertex.X, vertex.Y, vertex.Z));
            }
            return verticesList.ToArray();
        }

        public override Vector3[] GetNormals()
        {
            List<Vector3> normalsList = new List<Vector3>();
            foreach (Vector3D normal in AssMesh.Normals)
            {
                normalsList.Add(new Vector3(normal.X, normal.Y, normal.Z));
            }
            return normalsList.ToArray();
        }

        public override int[] GetIndices(int offset = 0)
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

        public override Vector3[] GetColorData()
        {
            Vector3[] colorData = new Vector3[VertexCount];
            for (int i = 0; i < VertexCount; i++)
            {
                colorData[i] = Color;
            }

            return colorData;
        }

        public override Vector2[] GetTextureCoords()
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
        public override void CalculateModelMatrix()
        {
            ModelMatrix = Matrix4.CreateScale(Scale) * Matrix4.CreateTranslation(Position);
        }
    }
}
