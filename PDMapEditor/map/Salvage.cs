using OpenTK;
using System.Collections.Generic;
using System.ComponentModel;

namespace PDMapEditor
{
    public class Salvage : Drawable, ISelectable, IElement
    {
        public static List<Salvage> Salvages = new List<Salvage>();

        private static SalvageType lastType;
        private static float lastMultiplier = 100;

        private SalvageType type;
        [CustomSortedCategory("Salvage", 2, 2)]
        [Description("The type of the salvage. From data/resource/salvage.")]
        public SalvageType Type { get { return type; } set { type = value; lastType = value; } }

        private float multiplier;
        [CustomSortedCategory("Salvage", 2, 2)]
        [DisplayName("RU Multiplier")]
        [Description("Resource multiplier in percent.")]
        [TypeConverter(typeof(PercentConverter))]
        public float Multiplier { get { return multiplier; } set { multiplier = value; lastMultiplier = value; } }

        [Browsable(false)]
        public string TypeName { get { return "Salvage"; } }

        [Browsable(false)]
        public bool AllowRotation { get; set; }

        public Salvage() : base (Vector3.Zero)
        {
            if (lastType != null)
                Type = lastType;
            else
                Type = SalvageType.SalvageTypes[0];

            Multiplier = lastMultiplier;

            CreateMesh();

            Salvages.Add(this);
            AllowRotation = true;
        }
        public Salvage(SalvageType type, Vector3 position, Vector3 rotation, float multiplier) : base (position, rotation)
        {
            Type = type;
            Rotation = rotation;
            Multiplier = multiplier;

            CreateMesh();

            Salvages.Add(this);
            AllowRotation = true;
        }

        private void CreateMesh()
        {
            Mesh = new MeshIcosphere(Position, Rotation, new Vector3(0, 0, 1), false);
            Mesh.Scale = new Vector3(type.PixelSize * 13);
        }

        public override void Destroy()
        {
            base.Destroy();

            Salvages.Remove(this);
        }

        public ISelectable Copy()
        {
            return new Salvage(Type, Position, Rotation, Multiplier);
        }
    }
}
