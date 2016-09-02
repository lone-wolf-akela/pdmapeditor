using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDMapEditor
{
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
