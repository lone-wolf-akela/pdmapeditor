using OpenTK;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;

namespace PDMapEditor
{
    public class Nebula : Drawable, ISelectable, IElement
    {
        public static List<Nebula> Nebulas = new List<Nebula>();
        private static string lastName = string.Empty;
        private static float lastRadius = 5000;
        private static NebulaType lastType;
        private static float lastResources = 0;
        private static Vector4 lastColor = Vector4.One;
        private static float lastSpin;
        private static bool lastRefill;

        private string name;
        [CustomSortedCategory("Nebula", 2, 2)]
        [Description("The name of the nebula. It can be used to refer to this nebula with scripts.")]
        public string Name { get { return name; } set { name = value; lastName = value; } }

        private NebulaType type;
        [CustomSortedCategory("Nebula", 2, 2)]
        [Description("The type of the nebula. From data/resource/nebula/.")]
        public NebulaType Type { get { return type; } set { type = value; lastType = value; } }

        private Vector4 color;
        [Browsable(false)]
        public Vector4 Color { get { return color; } set { color = value; UpdateColor(); lastColor = value; } }
        [CustomSortedCategory("Nebula", 2, 2)]
        [DisplayName("Color")]
        [Description("The color of the nebula.")]
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
        [CustomSortedCategory("Nebula", 2, 2)]
        [Description("The radius (size) of the nebula.")]
        public float Radius { get { return radius; } set { radius = value; UpdateScale(); lastRadius = value; } }

        private float resources;
        [CustomSortedCategory("Nebula", 2, 2)]
        [Description("The RUs in this nebula.")]
        [TypeConverter(typeof(ResourcesConverter))]
        public float Resources { get { return resources; } set { resources = value; lastResources = value; } }

        private float spin;
        [CustomSortedCategory("Nebula", 2, 2)]
        [Description("Rotation of the billboard (in degrees).")]
        [TypeConverter(typeof(DegreesConverter))]
        public float Spin { get { return spin; } set { spin = value; lastSpin = value; } }

        private bool refill;
        [CustomSortedCategory("Nebula", 2, 2)]
        [Description("Defines if the nebula gets refilled.")]
        public bool Refill { get { return refill; } set { refill = value; lastRefill = value; } }

        public string TypeName { get { return "Nebula"; } }

        [Browsable(false)]
        public bool AllowRotation { get; set; }

        public Nebula() : base(Vector3.Zero)
        {
            if (lastName.Length > 0)
                Name = lastName;
            else
                Name = "nebula" + Nebulas.Count;

            Mesh = new MeshIcosphere(Vector3.Zero, Vector3.One);
            Mesh.Material.Translucent = true;

            Radius = lastRadius;
            Mesh.Scale = new Vector3(Radius);
            Nebulas.Add(this);

            if (lastType != null)
                Type = lastType;
            else
                Type = NebulaType.NebulaTypes[0];

            Resources = lastResources;

            Color = lastColor;
            Spin = lastSpin;
            Refill = lastRefill;

            AllowRotation = false;
        }
        public Nebula(string name, NebulaType type, Vector3 position, Vector4 color, float spin, float radius) : base (position)
        {
            Name = name;
            Spin = spin;

            Mesh = new MeshIcosphere(position, Vector3.One);
            Mesh.Material.Translucent = true;
            Mesh.Scale = new Vector3(this.radius);

            Nebulas.Add(this);

            Type = type;
            Radius = radius;
            Color = color;

            AllowRotation = false;
        }
        public Nebula(string name, NebulaType type, Vector3 position, float resources, Vector4 color, float spin, float radius, bool refill) : base(position)
        {
            Name = name;
            Spin = spin;
            Resources = resources;
            Refill = refill;

            Mesh = new MeshIcosphere(position, Vector3.One);
            Mesh.Material.Translucent = true;
            Mesh.Scale = new Vector3(this.radius);

            Nebulas.Add(this);

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
            Mesh.Material.DiffuseColor = new Vector3(Color);
            Mesh.Material.Opacity = Color.W * 0.1f;
        }

        public override void Destroy()
        {
            base.Destroy();

            Nebulas.Remove(this);
        }

        public ISelectable Copy()
        {
            return new Nebula(Name, Type, Position, Resources, Color, Spin, Radius, Refill);
        }
    }
}
