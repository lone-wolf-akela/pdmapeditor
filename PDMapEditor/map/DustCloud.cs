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

        private DustCloudType type;
        public DustCloudType Type { get { return type; } set { type = value; UpdateColor(); } }

        private Vector4 color = Vector4.One;
        public Vector4 Color { get { return color; } set { color = value; UpdateColor(); } }

        public float Unknown1;

        private float size;
        public float Size { get { return size; } set { size = value; UpdateScale(); } }

        public bool AllowRotation { get; set; }

        public DustCloud() : base (Vector3.Zero)
        {
            Name = "dustCloud" + (DustClouds.Count + 1);

            Mesh = new MeshIcosphere(Vector3.Zero, Vector3.One, true);
            Mesh.Material.Translucent = true;

            Size = 2000;
            Mesh.Scale = new Vector3(Size);
            Type = DustCloudType.DustCloudTypes[0];
            Color = Vector4.One;
            DustClouds.Add(this);
            AllowRotation = false;
        }
        public DustCloud(string name, DustCloudType type, Vector3 position, Vector4 color, float unknown1, float size) : base (position)
        {
            Name = name;
            Unknown1 = unknown1;

            Mesh = new MeshIcosphere(position, Vector3.One, true);
            Mesh.Material.Translucent = true;
            Mesh.Scale = new Vector3(size);

            DustClouds.Add(this);

            Type = type;
            Size = size;
            Color = color;

            AllowRotation = false;
        }

        private void UpdateScale()
        {
            Mesh.Scale = new Vector3(Size);
        }

        private void UpdateColor()
        {
            Mesh.Material.DiffuseColor = Vector3.Multiply(new Vector3(Color), new Vector3(Type.PixelColor));
            Mesh.Material.Opacity = Color.W * Type.PixelColor.W * 0.1f;
        }

        public override void Destroy()
        {
            base.Destroy();

            DustClouds.Remove(this);
        }

        public ISelectable Copy()
        {
            return new DustCloud(Name, Type, Position, Color, Unknown1, Size);
        }
    }
}
