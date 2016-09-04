using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDMapEditor
{
    public class AsteroidType
    {
        public static List<AsteroidType> AsteroidTypes = new List<AsteroidType>();

        public string Name;
        public Vector4 PixelColor;
        public float ResourceValue;

        public int ComboIndex = -1;

        public AsteroidType(string name, Vector4 pixelColor, float resourceValue)
        {
            Name = name;
            PixelColor = pixelColor;
            ResourceValue = resourceValue;

            AsteroidTypes.Add(this);

            Program.main.comboAsteroidType.Items.Add(Name);
            ComboIndex = Program.main.comboAsteroidType.Items.Count - 1;
        }

        public static AsteroidType GetTypeFromName(string name)
        {
            foreach(AsteroidType type in AsteroidTypes)
            {
                if (type.Name == name)
                    return type;
            }

            return null;
        }

        public static AsteroidType GetTypeFromComboIndex(int index)
        {
            foreach(AsteroidType type in AsteroidTypes)
            {
                if (type.ComboIndex == index)
                    return type;
            }

            return null;
        }
    }
}
