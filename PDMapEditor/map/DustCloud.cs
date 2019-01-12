using OpenTK;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;

namespace PDMapEditor
{
    public class DustCloud : Drawable, ISelectable, IElement
    {
        public static List<DustCloud> DustClouds = new List<DustCloud>();
        private static string lastName = string.Empty;
        private static float lastRadius = 5000;
        private static DustCloudType lastType;
        private static Vector4 lastColor = Vector4.One;
        private static float lastResources = 0;
        private static float lastSpin = 0;
        private static bool lastRefill = false;

        private string name;
        [CustomSortedCategory("Dust cloud", 2, 2)]
        [Description("The name of the dust cloud. It can be used to refer to this dust cloud with scripts.")]
        public string Name { get { return name; } set { name = value; lastName = value; } }

        private DustCloudType type;
        [CustomSortedCategory("Dust cloud", 2, 2)]
        [Description("The type of the dust cloud. From data/resource/dustcloud/.")]
        public DustCloudType Type { get { return type; } set { type = value; UpdateColor(); lastType = value; } }

        private Vector4 color = Vector4.One;
        [Browsable(false)]
        public Vector4 Color { get { return color; } set { color = value; UpdateColor(); lastColor = value; } }
        [CustomSortedCategory("Dust cloud", 2, 2)]
        [DisplayName("Color")]
        [Description("The color tint of the dust cloud. This gets mixed with the color from the type.")]
        public Color ColorProperty
        {
            get
            {
                return System.Drawing.Color.FromArgb((int)Math.Round(Math.Min(Color.W * 128, 255)), (int)Math.Round(Math.Min(Color.X * 128, 255)), (int)Math.Round(Math.Min(Color.Y * 128, 255)), (int)Math.Round(Math.Min(Color.Z * 128, 255)));
            }
            set
            {
                Color = new Vector4((float)value.R / 128, (float)value.G / 128, (float)value.B / 128, (float)value.A / 128);
            }
        }

        private float radius;
        [CustomSortedCategory("Dust cloud", 2, 2)]
        [Description("The radius (size) of the dust cloud.")]
        public float Radius { get { return radius; } set { radius = value; UpdateScale(); lastRadius = value; } }

        private float resources;
        [CustomSortedCategory("Dust cloud", 2, 2)]
        [Description("The RUs in this dust cloud.")]
        [TypeConverter(typeof(ResourcesConverter))]
        public float Resources { get { return resources; } set { resources = value; lastResources = value; } }

        private float spin;
        [CustomSortedCategory("Dust cloud", 2, 2)]
        [Description("Rotation of the billboard (in degrees).")]
        [TypeConverter(typeof(DegreesConverter))]
        public float Spin { get { return spin; } set { spin = value; lastSpin = value; } }

        private bool refill;
        [CustomSortedCategory("Dust cloud", 2, 2)]
        [Description("Defines if the dust cloud gets refilled.")]
        public bool Refill { get { return refill; } set { refill = value; lastRefill = value; } }

        [Browsable(false)]
        public string TypeName { get { return "Dust cloud"; } }

        [Browsable(false)]
        public bool AllowRotation { get; set; }

        public DustCloud() : base (Vector3.Zero)
        {
            Name = "dustCloud" + DustClouds.Count;

            Mesh = new MeshIcosphere(Vector3.Zero, Vector3.One, true);
            Mesh.Material.Translucent = true;

            Radius = lastRadius;
            Mesh.Scale = new Vector3(Radius);

            if (lastType != null)
                type = lastType;
            else
                type = DustCloudType.DustCloudTypes[0];

            Resources = lastResources;
            Spin = lastSpin;
            Refill = lastRefill;
            Color = lastColor;

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
