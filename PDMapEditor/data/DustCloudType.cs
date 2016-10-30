using OpenTK;
using System.Collections.Generic;
using System.ComponentModel;

namespace PDMapEditor
{
    [TypeConverter(typeof(DustCloudTypeConverter))]
    public class DustCloudType
    {
        public static List<DustCloudType> DustCloudTypes = new List<DustCloudType>();

        public string Name;
        public Vector4 PixelColor;

        public DustCloudType(string name, Vector4 pixelColor)
        {
            Name = name;
            PixelColor = pixelColor;

            DustCloudTypes.Add(this);
        }

        public static DustCloudType GetTypeFromName(string name)
        {
            foreach(DustCloudType type in DustCloudTypes)
            {
                if (type.Name == name)
                    return type;
            }

            return null;
        }
    }
}
