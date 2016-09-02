using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDMapEditor
{
    public class Drawable : Object
    {
        public static List<Drawable> Drawables = new List<Drawable>();

        private Vector3 position;
        public override Vector3 Position { get { return position; } set { position = value; if(Mesh != null) Mesh.Position = value; } }

        private Vector3 rotation;
        public override Vector3 Rotation { get { return rotation; } set { rotation = value; if (Mesh != null) Mesh.Rotation = value; } }

        public Mesh Mesh;
        public bool Visible = true;

        public Drawable(Vector3 position) : base(position)
        {
            Position = position;

            Drawables.Add(this);
        }

        public Drawable(Vector3 position, Vector3 rotation) : base(position, rotation)
        {
            Position = position;
            Rotation = rotation;

            Drawables.Add(this);
        }
        public Drawable(Vector3 position, Assimp.Mesh assMesh) : base(position)
        {
            Position = position;
            Mesh = new Mesh(position, Vector3.Zero, assMesh);

            Drawables.Add(this);
        }
        public Drawable(Vector3 position, Vector3 rotation, Assimp.Mesh assMesh) : base(position, rotation)
        {
            Position = position;
            Rotation = rotation;
            Mesh = new Mesh(position, rotation, assMesh);

            Drawables.Add(this);
        }
    }
}
