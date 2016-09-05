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

        public int ComboIndex = -1;

        public PebbleType(string name, float size, Vector4 color)
        {
            Name = name;
            PixelSize = size;
            PixelColor = color;

            PebbleTypes.Add(this);

            Program.main.comboPebbleType.Items.Add(Name);
            ComboIndex = Program.main.comboPebbleType.Items.Count - 1;
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

        public static PebbleType GetTypeFromComboIndex(int index)
        {
            foreach (PebbleType type in PebbleTypes)
            {
                if (type.ComboIndex == index)
                    return type;
            }

            return null;
        }
    }
}
