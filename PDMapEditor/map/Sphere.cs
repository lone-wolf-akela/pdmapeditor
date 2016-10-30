using OpenTK;
using System.Collections.Generic;
using System.ComponentModel;

namespace PDMapEditor
{
    public class Sphere : Drawable, ISelectable
    {
        public static List<Sphere> Spheres = new List<Sphere>();
        private static string lastName = string.Empty;
        private static float lastRadius;

        private string name;
        [CustomSortedCategory("Sphere", 2, 2)]
        [Description("The name of the sphere. It can be used to refer to this sphere with scripts.")]
        public string Name { get { return name; } set { name = value; lastName = value; } }

        private float radius;
        [CustomSortedCategory("Sphere", 2, 2)]
        [Description("The radius (size) of the sphere.")]
        public float Radius { get { return radius; } set { radius = value; UpdateScale(); lastRadius = value; } }

        [Browsable(false)]
        public bool AllowRotation { get; set; }

        public Sphere() : base (Vector3.Zero)
        {
            if (lastName.Length > 0)
                Name = lastName;
            else
                Name = "sphere" + Spheres.Count;

            Mesh = new MeshIcosphere(Vector3.Zero, Vector3.One);
            Mesh.Wireframe = true;
            Mesh.Material.Translucent = true;

            Radius = lastRadius;
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
