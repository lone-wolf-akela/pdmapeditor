using OpenTK;
using System.Collections.Generic;
using System.ComponentModel;

namespace PDMapEditor
{
    public class Squadron : DrawableRotated, ISelectable, IElement
    {
        public static List<Squadron> Squadrons = new List<Squadron>();
        private static string lastName = string.Empty;
        private static ShipType lastType;
        private static int lastPlayer = -1;
        private static int lastSquadronSize = 1;
        private static bool lastInHyperspace = false;

        private string name;
        [CustomSortedCategory("Squadron", 2, 2)]
        [Description("The name of the squadron. It can be used to refer to this squadron with scripts.")]
        public string Name { get { return name; } set { name = value; lastName = value; } }

        private ShipType type;
        [CustomSortedCategory("Squadron", 2, 2)]
        [Description("The type of the squadron. From data/ship/.")]
        public ShipType Type { get { return type; } set { type = value; lastType = value; } }

        private int player;
        [CustomSortedCategory("Squadron", 2, 2)]
        [Description("This controls the player this squadron belongs to.")]
        public int Player { get { return player;} set { player = value; lastPlayer = value; } }

        private int squadronSize;
        [CustomSortedCategory("Squadron", 2, 2)]
        [DisplayName("Squadron size")]
        [Description("This controls the size of the squadron.")]
        public int SquadronSize { get { return squadronSize; } set { squadronSize = value; lastSquadronSize = value; } }

        private bool inHyperspace;
        [CustomSortedCategory("Squadron", 2, 2)]
        [DisplayName("In hyperspace")]
        [Description("If this is set to true, the squadron will spawn in hyperspace.")]
        public bool InHyperspace { get { return inHyperspace; } set { inHyperspace = value; lastInHyperspace = value; } }

        [Browsable(false)]
        public string TypeName { get { return "Squadron"; } }

        [Browsable(false)]
        public bool AllowRotation { get; set; }

        public Squadron() : base (Vector3.Zero, Vector3.Zero)
        {
            if (lastName.Length > 0)
                Name = lastName;
            else
                Name = "Squadron" + Squadrons.Count;

            if (lastType != null)
                Type = lastType;
            else
                Type = ShipType.ShipTypes[0];

            Player = lastPlayer;
            SquadronSize = lastSquadronSize;
            InHyperspace = lastInHyperspace;

            Mesh = new Mesh(Vector3.Zero, Vector3.Zero, Mesh.Cube);
            Mesh.Material.DiffuseColor = new Vector3(1, 1, 1);
            Mesh.Scale = new Vector3(100);
            Squadrons.Add(this);

            AllowRotation = true;
        }
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

        public override void Destroy()
        {
            base.Destroy();

            Squadrons.Remove(this);
        }

        public ISelectable Copy()
        {
            return new Squadron(Name, Position, Rotation, Type, Player, SquadronSize, InHyperspace);
        }
    }
}
