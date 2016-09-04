using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDMapEditor
{
    public class DustCloud : Drawable, ISelectable
    {
        public static List<DustCloud> DustClouds = new List<DustCloud>();

        public string Name;
        public DustCloudType Type;
        public Vector4 Color;
        public float Unknown1;
        public float Size;

        public DustCloud(string name, DustCloudType type, Vector3 position, Vector4 color, float unknown1, float size) : base (position)
        {
            Name = name;
            Type = type;
            Color = color;
            Unknown1 = unknown1;
            Size = size;

            Mesh = new MeshIcosphere(position, new Vector3(type.PixelColor), true);
            Mesh.Material.Translucent = true;
            Mesh.Material.Opacity = 0.1f;
            Mesh.Scale = new Vector3(size);

            DustClouds.Add(this);
        }
    }
}
