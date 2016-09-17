using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDMapEditor
{
    public class Sphere : Drawable, ISelectable
    {
        public static List<Sphere> Spheres = new List<Sphere>();

        public string Name;
        private float radius;
        public float Radius { get { return radius; } set { radius = value; UpdateScale(); } }

        public bool AllowRotation { get; set; }

        public Sphere() : base (Vector3.Zero)
        {
            Name = Program.main.boxSphereName.Text;

            Mesh = new MeshIcosphere(Vector3.Zero, Vector3.One);
            Mesh.Wireframe = true;
            Mesh.Material.Translucent = true;

            Radius = (float)Program.main.numericSphereRadius.Value;
            Mesh.Scale = new Vector3(Radius);

            Spheres.Add(this);
            AllowRotation = false;
        }
        public Sphere(string name, Vector3 position, float radius) : base (position)
        {
            Name = name;

            Mesh = new MeshIcosphere(position, Vector3.One);
            Mesh.Wireframe = true;
            Mesh.Material.Translucent = true;
            Mesh.Scale = new Vector3(radius);

            Spheres.Add(this);
            Radius = radius;

            AllowRotation = false;
        }

        private void UpdateScale()
        {
            Mesh.Scale = new Vector3(Radius);
        }

        public override void Destroy()
        {
            base.Destroy();

            Spheres.Remove(this);
        }

        public ISelectable Copy()
        {
            return new Sphere(Name, Position, Radius);
        }
    }
}
