using OpenTK;
using System.Collections.Generic;
using System.ComponentModel;

namespace PDMapEditor
{
    [TypeConverter(typeof(AsteroidTypeConverter))]
    public class AsteroidType
    {
        public static List<AsteroidType> AsteroidTypes = new List<AsteroidType>();

        public string Name;
        public Vector4 PixelColor;
        public float ResourceValue;

        public AsteroidType(string name, Vector4 pixelColor, float resourceValue)
        {
            Name = name;
            PixelColor = pixelColor;
            ResourceValue = resourceValue;

            AsteroidTypes.Add(this);
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
    }
}
