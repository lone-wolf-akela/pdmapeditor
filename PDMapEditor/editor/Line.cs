using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDMapEditor
{
    public class Line : Drawable
    {
        private Vector3 start = Vector3.Zero;
        public Vector3 Start { get { return start; } set { start = value; MeshLine.Start = value; MeshLine.Vertices = MeshLine.GetVertices(); } }

        private Vector3 end = Vector3.Zero;
        public Vector3 End { get { return end; } set { end = value; MeshLine.End = value; MeshLine.Vertices = MeshLine.GetVertices(); } }

        private Vector3 position = Vector3.Zero;
        public override Vector3 Position { get { return position; } set { position = value; if(MeshLine != null) MeshLine.Position = value; } }

        private MeshLine MeshLine;

        public Line(Vector3 start, Vector3 end, Vector3 color) : base(Vector3.Zero)
        {
            MeshLine = new MeshLine(start, end, color);
            Mesh = MeshLine;

            Start = start;
            End = end;
        }
        public Line(Vector3 start, Vector3 end, Vector3 color, float width) : base(Vector3.Zero)
        {
            MeshLine = new MeshLine(start, end, color, width);
            Mesh = MeshLine;

            Start = start;
            End = end;
        }
    }
}
