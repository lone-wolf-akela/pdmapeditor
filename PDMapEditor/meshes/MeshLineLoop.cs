using Assimp;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace PDMapEditor
{
    public class MeshLineLoop : Mesh
    {
        public Vector3[] Polygons = new Vector3[0];

        private Vector3 color;
        public Vector3 Color { get { return color; } set { color = value; Colors = GetColorData(); } }

        public override int VertexCount { get { return Polygons.Length; } }
        public override int IndiceCount { get { return Polygons.Length; } }

        public MeshLineLoop(Vector3[] polygons, Vector3 color) : base()
        {
            Color = color;

            BeginMode = BeginMode.LineLoop;
            Polygons = polygons;

            Vertices = GetVertices();
            Normals = GetNormals();
            Indices = GetIndices();
            Colors = GetColorData();
            TextureCoords = GetTextureCoords();
        }

        public MeshLineLoop(Vector3[] polygons, Vector3 color, float width) : base()
        {
            Color = color;
            LineWidth = width;

            BeginMode = BeginMode.LineLoop;
            Polygons = polygons;

            Vertices = GetVertices();
            Normals = GetNormals();
            Indices = GetIndices();
            Colors = GetColorData();
            TextureCoords = GetTextureCoords();
        }

        public override Vector3[] GetVertices()
        {
            return Polygons;
        }

        public override Vector3[] GetNormals()
        {
            return new Vector3[0];
        }

        public override int[] GetIndices(int offset = 0)
        {
            int[] indices = new int[IndiceCount];

            for (int i = 0; i < indices.Length; i++)
            {
                indices[i] = i + offset;
            }

            return indices;
        }

        public override Vector3[] GetColorData()
        {
            Vector3[] colorData = new Vector3[Polygons.Length];
            for (int i = 0; i < Polygons.Length; i++)
            {
                colorData[i] = Color;
            }
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
