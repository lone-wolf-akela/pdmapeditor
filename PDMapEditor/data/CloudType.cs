using OpenTK;
using System.Collections.Generic;
using System.ComponentModel;

namespace PDMapEditor
{
    [TypeConverter(typeof(CloudTypeConverter))]
    public class CloudType
    {
        public static List<CloudType> CloudTypes = new List<CloudType>();

        public string Name;
        public Vector4 PixelColor;

        public CloudType(string name, Vector4 pixelColor)
        {
            Name = name;
            PixelColor = pixelColor;

            CloudTypes.Add(this);
        }

        public static CloudType GetTypeFromName(string name)
        {
            foreach(CloudType type in CloudTypes)
            {
                if (type.Name == name)
                    return type;
            }

            return null;
        }
    }
}
