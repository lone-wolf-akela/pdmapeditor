using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDMapEditor
{
    public class Nebula : Drawable, ISelectable
    {
        public static List<Nebula> Nebulas = new List<Nebula>();

        public string Name;

        public NebulaType Type;

        private Vector4 color;
        public Vector4 Color { get { return color; } set { color = value; UpdateColor(); } }

        public float Unknown;

        private float size;
        public float Size { get { return size; } set { size = value; UpdateScale(); } }

        public bool AllowRotation { get; set; }

        public Nebula(string name, NebulaType type, Vector3 position, Vector4 color, float unknown, float size) : base (position)
        {
            Name = name;
            Unknown = unknown;

            Mesh = new MeshIcosphere(position, Vector3.One, false);
            Mesh.Material.Translucent = true;
            Mesh.Scale = new Vector3(size);

            Nebulas.Add(this);

            Type = type;
            Size = size;
            Color = color;

            AllowRotation = false;
        }

        private void UpdateScale()
        {
            Mesh.Scale = new Vector3(Size);
        }

        private void UpdateColor()
        {
            Mesh.Material.DiffuseColor = new Vector3(Color);
            Mesh.Material.Opacity = Color.W * 0.1f;
        }
    }
}
