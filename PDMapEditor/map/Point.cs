using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDMapEditor
{
    public class Point : Drawable, ISelectable
    {
        public static List<Point> Points = new List<Point>();

        public string Name;
        public bool AllowRotation { get; set; }

        public Point() : base (Vector3.Zero, Vector3.Zero)
        {
            Name = Program.main.boxPointName.Text;

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
