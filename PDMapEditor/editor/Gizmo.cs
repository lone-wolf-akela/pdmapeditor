using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDMapEditor
{
    public class Gizmo : Drawable, ISelectable
    {
        public bool AllowRotation { get; set; }

        public Gizmo(Vector3 position, Assimp.Mesh assMesh) : base(position, assMesh)
        {
            Mesh = new Mesh(position, Vector3.Zero, assMesh);
            Mesh.DrawInFront = true;

            AllowRotation = true;
        }
    }
}
