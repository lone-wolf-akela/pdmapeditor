using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDMapEditor
{
    public class PebbleType
    {
        public static List<PebbleType> PebbleTypes = new List<PebbleType>();

        public string Name;
        public float PixelSize;
        public Vector4 PixelColor;

        public PebbleType(string name, float size, Vector4 color)
        {
            Name = name;
            PixelSize = size;
            PixelColor = color;

            PebbleTypes.Add(this);
        }

        public static PebbleType GetTypeFromName(string name)
        {
            foreach(PebbleType type in PebbleTypes)
            {
                if (type.Name == name)
                    return type;
            }

            return null;
        }
    }
}
