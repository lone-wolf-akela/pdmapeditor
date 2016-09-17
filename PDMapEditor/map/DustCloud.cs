using OpenTK;
using System;
using System.Collections.Generic;
using System.Drawing;
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

        public float Resources;
        public float Spin;
        public bool Refill;

        private float radius;
        public float Radius { get { return radius; } set { radius = value; UpdateScale(); } }

        public bool AllowRotation { get; set; }

        public DustCloud() : base (Vector3.Zero)
        {
            Name = Program.main.boxDustCloudName.Text;

            Mesh = new MeshIcosphere(Vector3.Zero, Vector3.One, true);
            Mesh.Material.Translucent = true;

            Radius = (float)Program.main.numericDustCloudSize.Value;
            Mesh.Scale = new Vector3(Radius);
            Type = DustCloudType.GetTypeFromComboIndex(Program.main.comboDustCloudType.SelectedIndex);
            Resources = (float)Program.main.numericDustCloudResources.Value;

            Color buttonColor = Program.main.buttonDustCloudColor.BackColor;
            Color = new Vector4((float)buttonColor.R / 255, (float)buttonColor.G / 255, (float)buttonColor.B / 255, (float)Program.main.sliderDustCloudAlpha.Value / 100);
            DustClouds.Add(this);
            AllowRotation = false;
        }
        public DustCloud(string name, DustCloudType type, Vector3 position, Vector4 color, float spin, float radius) : base (position)
        {
            Name = name;
            Spin = spin;

            Mesh = new MeshIcosphere(position, Vector3.One, true);
            Mesh.Material.Translucent = true;
            Mesh.Scale = new Vector3(radius);

            DustClouds.Add(this);

            Type = type;
            Radius = radius;
            Color = color;

            AllowRotation = false;
        }
        public DustCloud(string name, DustCloudType type, Vector3 position, float resources, Vector4 color, float spin, float radius, bool refill) : base(position)
        {
            Name = name;
            Spin = spin;
            Resources = resources;
            Refill = refill;

            Mesh = new MeshIcosphere(position, Vector3.One, true);
            Mesh.Material.Translucent = true;
            Mesh.Scale = new Vector3(radius);

            DustClouds.Add(this);

            Type = type;
            Radius = radius;
            Color = color;

            AllowRotation = false;
        }

        private void UpdateScale()
        {
            Mesh.Scale = new Vector3(Radius);
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
            return new DustCloud(Name, Type, Position, Resources, Color, Spin, Radius, Refill);
        }
    }
}
