using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDMapEditor
{
    public class Squadron : Drawable, ISelectable
    {
        public static List<Squadron> Squadrons = new List<Squadron>();

        public string Name;
        public bool AllowRotation { get; set; }

        public Squadron(string name, Vector3 position, Vector3 rotation) : base (position, rotation)
        {
            Name = name;

            Mesh = new Mesh(position, rotation, Mesh.Point);
            Mesh.Material.DiffuseColor = new Vector3(1, 0, 0);
            Mesh.Scale = new Vector3(100);
            Squadrons.Add(this);

            AllowRotation = true;
        }
    }
}
