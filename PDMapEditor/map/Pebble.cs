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

        public Pebble(PebbleType type, Vector3 position, Vector3 rotation) : base (position)
        {
            Type = type;
            Rotation = rotation;

            Mesh = new MeshDot(position, new Vector3(type.PixelColor), type.PixelSize); //TODO: Use alpha value of pebble type
            Pebbles.Add(this);
        }
    }
}
