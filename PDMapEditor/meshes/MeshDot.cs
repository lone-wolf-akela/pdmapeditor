using Assimp;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace PDMapEditor
{
    public class MeshDot : Mesh
    {
        private Vector3 color;
        public Vector3 Color { get { return color; } set { color = value; Colors = GetColorData(); } }

        public override int VertexCount { get { return 1; } }
        public override int IndiceCount { get { return 1; } }

        public MeshDot(Vector3 position, Vector3 color, float size) : base(position)
        {
            this.Color = color;

            BeginMode = BeginMode.Points;
            DotSize = size;

            Vertices = GetVertices();
            Normals = GetNormals();
            Indices = GetIndices();
            Colors = GetColorData();
            TextureCoords = GetTextureCoords();
        }

        public override Vector3[] GetVertices()
        {
            return new Vector3[] { new Vector3(0) };
        }

        public override Vector3[] GetNormals()
        {
            return new Vector3[0];
        }

        public override int[] GetIndices(int offset = 0)
        {
            int[] indices = { 0 };

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
            Vector3[] colorData = { new Vector3(Color.X, Color.Y, Color.Z) };
            return colorData;
        }

        public override Vector2[] GetTextureCoords()
        {
            return new Vector2[0];
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
