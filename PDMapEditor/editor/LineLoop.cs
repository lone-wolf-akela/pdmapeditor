using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDMapEditor
{
    public class LineLoop : Drawable
    {
        private Vector3[] polygons;
        public Vector3[] Polygons { get { return polygons; } set { polygons = value; MeshLineLoop.Polygons = value; MeshLineLoop.Vertices = MeshLineLoop.GetVertices(); } }

        private Vector3 position = Vector3.Zero;
        public override Vector3 Position { get { return position; } set { position = value; if(MeshLineLoop != null) MeshLineLoop.Position = value; } }

        private MeshLineLoop MeshLineLoop;

        public LineLoop(Vector3[] polygons, Vector3 color) : base(Vector3.Zero)
        {
            MeshLineLoop = new MeshLineLoop(polygons, color);
            Mesh = MeshLineLoop;

            Polygons = polygons;
        }
        public LineLoop(Vector3[] polygons, Vector3 color, float width) : base(Vector3.Zero)
        {
            MeshLineLoop = new MeshLineLoop(polygons, color, width);
            Mesh = MeshLineLoop;

            Polygons = polygons;
        }
    }
}
