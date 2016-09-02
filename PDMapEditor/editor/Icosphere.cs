using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDMapEditor
{
    public class Icosphere : Drawable
    {
        public Icosphere(Vector3 position, Vector3 color) : base(position)
        {
            Mesh = new MeshIcosphere(position, color);
        }

        public Icosphere(Vector3 position, Vector3 color, bool detailed) : base(position)
        {
            Mesh = new MeshIcosphere(position, color, detailed);
        }
    }
}
