using OpenTK;
using System.Collections.Generic;
using System.ComponentModel;

namespace PDMapEditor
{
    public class Point : DrawableRotated, ISelectable, IElement
    {
        public static List<Point> Points = new List<Point>();
        private static string lastName = string.Empty;

        private string name;
        [CustomSortedCategory("Point", 2, 2)]
        [Description("The name of the point. It can be used to refer to this point with scripts.")]
        public string Name { get { return name; } set { name = value; lastName = value; } }

        [Browsable(false)]
        public string TypeName { get { return "Point"; } }

        [Browsable(false)]
        public bool AllowRotation { get; set; }

        public Point() : base (Vector3.Zero, Vector3.Zero)
        {
            Name = "Point" + Points.Count;

            Mesh = new Mesh(Vector3.Zero, Vector3.Zero, Mesh.Point);
            Mesh.Material.DiffuseColor = new Vector3(1, 0, 0);
            Mesh.Scale = new Vector3(100);
            Points.Add(this);

            AllowRotation = true;
        }
        public Point(string name, Vector3 position, Vector3 rotation) : base (position, rotation)
        {
            Name = name;

            Mesh = new Mesh(position, rotation, Mesh.Point);
            Mesh.Material.DiffuseColor = new Vector3(1, 0, 0);
            Mesh.Scale = new Vector3(100);
            Points.Add(this);

            AllowRotation = true;
        }

        public override void Destroy()
        {
            base.Destroy();

            Points.Remove(this);
        }

        public ISelectable Copy()
        {
            return new Point(Name, Position, Rotation);
        }
    }
}
