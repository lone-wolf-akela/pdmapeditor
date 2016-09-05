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
    public class Mesh2D
    {
        public static List<Mesh2D> Meshes2D = new List<Mesh2D>();

        public Vector2 Position = Vector2.Zero;

        private Vector2 start;
        public Vector2 Start { get { return start; } set { start = value; UpdateVertices(); } }

        private Vector2 end;
        public Vector2 End { get { return end; } set { end = value; UpdateVertices(); } }

        public BeginMode BeginMode = BeginMode.Lines;

        public float LineWidth = 1f;
        public float DotSize = 1;

        public Vector2[] Vertices;
        public int[] Indices;

        public bool Visible = false;

        public virtual int VertexCount { get { return Vertices.Length; } }
        public virtual int IndiceCount { get { return Indices.Length; } }

        public Mesh2D()
        {
            Meshes2D.Add(this);

            BeginMode = BeginMode.Lines;

            Vertices = new Vector2[2];
            Indices = new int[] { 0, 1 };
        }

        public Mesh2D(Vector2 start, Vector2 end)
        {
            this.Position = Vector2.Zero;
            this.Start = start;
            this.End = end;

            Indices = new int[] { 0, 1 };

            BeginMode = BeginMode.Lines;

            Meshes2D.Add(this);
        }

        public void UpdateVertices()
        {
            Vertices = new Vector2[] { Start, End };
        }

        public int[] GetIndices(int offset = 0)
        {
            int[] indices = new int[] { 0, 1 };

            if (offset != 0)
            {
                for (int i = 0; i < indices.Length; i++)
                {
                    indices[i] += offset;
                }
            }

            return indices;
        }

        public void Remove()
        {
            Meshes2D.Remove(this);
        }
    }
}
