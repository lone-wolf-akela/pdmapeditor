using OpenTK;
using System.Collections.Generic;
using System.ComponentModel;

namespace PDMapEditor
{
    [TypeConverter(typeof(SalvageTypeConverter))]
    public class SalvageType
    {
        public static List<SalvageType> SalvageTypes = new List<SalvageType>();

        public string Name;
        public float PixelSize;
        public float ResourceValue;
        public Vector4 PixelColor;

        public SalvageType(string name, float pixelSize, float resourceValue, Vector4 pixelColor)
        {
            Name = name;
            PixelSize = pixelSize;
            ResourceValue = resourceValue;
            PixelColor = pixelColor;

            SalvageTypes.Add(this);
        }

        public static SalvageType GetTypeFromName(string name)
        {
            foreach(SalvageType type in SalvageTypes)
            {
                if (type.Name == name)
                    return type;
            }

            return null;
        }
    }
}
