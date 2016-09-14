using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDMapEditor
{
    public class Pebble : Drawable, ISelectable
    {
        public static List<Pebble> Pebbles = new List<Pebble>();

        public PebbleType Type;

        public bool AllowRotation { get; set; }

        public Pebble() : base (Vector3.Zero)
        {
            Type = PebbleType.GetTypeFromComboIndex(Program.main.comboPebbleType.SelectedIndex);

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
