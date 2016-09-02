using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDMapEditor
{
    public class Dot : Drawable
    {
        public Dot(Vector3 position, Vector3 color, float size) : base(position)
        {
            Mesh = new MeshDot(position, color, size);
        }
    }
}
