using OpenTK;
using System.Collections.Generic;
using System.ComponentModel;

namespace PDMapEditor
{
    public class Pebble : Drawable, ISelectable, IElement
    {
        public static List<Pebble> Pebbles = new List<Pebble>();
        private static PebbleType lastType;

        private PebbleType type;
        [CustomSortedCategory("Pebble", 2, 2)]
        [Description("The type of the pebble. From data/pebble/.")]
        public PebbleType Type { get { return type; } set { type = value; lastType = value; } }

        [Browsable(false)]
        public string TypeName { get { return "Pebble"; } }

        [Browsable(false)]
        public bool AllowRotation { get; set; }

        public Pebble() : base (Vector3.Zero)
        {
            if (lastType != null)
                Type = lastType;
            else
                Type = PebbleType.PebbleTypes[0];

            Mesh = new MeshDot(Vector3.Zero, new Vector3(Type.PixelColor), Type.PixelSize); //TODO: Use alpha value of pebble type
            Pebbles.Add(this);
            AllowRotation = false;
        }
        public Pebble(PebbleType type, Vector3 position, Vector3 rotation) : base (position)
        {
            Type = type;
            Rotation = rotation;

            Mesh = new MeshDot(position, new Vector3(type.PixelColor), type.PixelSize); //TODO: Use alpha value of pebble type
            Pebbles.Add(this);
            AllowRotation = false;
        }

        public override void Destroy()
        {
            base.Destroy();

            Pebbles.Remove(this);
        }

        public ISelectable Copy()
        {
            return new Pebble(Type, Position, Rotation);
        }
    }
}
