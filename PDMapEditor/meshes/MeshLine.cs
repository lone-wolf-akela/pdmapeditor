using Assimp;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace PDMapEditor
{
    public class MeshLine : Mesh
    {
        public Vector3 Start;
        public Vector3 End;

        private Vector3 color;
        public Vector3 Color { get { return color; } set { color = value; Colors = GetColorData(); } }

        public override int VertexCount { get { return 2; } }
        public override int IndiceCount { get { return 2; } }

        public MeshLine(Vector3 start, Vector3 end, Vector3 color) : base()
        {
            Color = color;

            BeginMode = BeginMode.Lines;
            Start = start;
            End = end;

            Vertices = GetVertices();
            Normals = GetNormals();
            Indices = GetIndices();
            Colors = GetColorData();
            TextureCoords = GetTextureCoords();
        }

        public MeshLine(Vector3 start, Vector3 end, Vector3 color, float width) : base()
        {
            Color = color;
            LineWidth = width;

            BeginMode = BeginMode.Lines;
            Start = start;
            End = end;

            Vertices = GetVertices();
            Normals = GetNormals();
            Indices = GetIndices();
            Colors = GetColorData();
            TextureCoords = GetTextureCoords();
        }

        public override Vector3[] GetVertices()
        {
            return new Vector3[] { Start, End };
        }

        public override Vector3[] GetNormals()
        {
            return new Vector3[0];
        }

        public override int[] GetIndices(int offset = 0)
        {
            int[] indices = { 0, 1 };

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
            Vector3[] colorData = { new Vector3(Color.X, Color.Y, Color.Z), new Vector3(Color.X, Color.Y, Color.Z) };
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
            ;
        }
    }
}
