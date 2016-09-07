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
        public ShipType Type;
        public int Player;
        public int SquadronSize;
        public bool InHyperspace;
        
        public bool AllowRotation { get; set; }

        public Squadron(string name, Vector3 position, Vector3 rotation, ShipType type, int player, int squadronSize, bool inHyperspace) : base (position, rotation)
        {
            Name = name;
            Type = type;
            Player = player;
            SquadronSize = squadronSize;
            InHyperspace = inHyperspace;

            Mesh = new Mesh(position, rotation, Mesh.Cube);
            Mesh.Material.DiffuseColor = new Vector3(1, 1, 1);
            Mesh.Scale = new Vector3(100);
            Squadrons.Add(this);

            AllowRotation = true;
        }
    }
}
