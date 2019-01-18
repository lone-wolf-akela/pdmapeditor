using OpenTK;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;

namespace PDMapEditor
{
    public class Cloud : Drawable, ISelectable, IElement
    {
        public static List<Cloud> Clouds = new List<Cloud>();
        private static string lastName = string.Empty;
        private static float lastRadius = 5000;
        private static CloudType lastType;
        private static Vector4 lastColor = Vector4.One;
        private static float lastSpin = 0;

        private string name;
        [CustomSortedCategory("Cloud", 2, 2)]
        [Description("The name of the cloud. It can be used to refer to this cloud with scripts.")]
        public string Name { get { return name; } set { name = value; lastName = value; } }

        private CloudType type;
        [CustomSortedCategory("Cloud", 2, 2)]
        [Description("The type of the cloud. From data/cloud/.")]
        public CloudType Type { get { return type; } set { type = value; UpdateColor(); lastType = value; } }

        private Vector4 color = Vector4.One;
        [Browsable(false)]
        public Vector4 Color { get { return color; } set { color = value; UpdateColor(); lastColor = value; } }
        [CustomSortedCategory("Cloud", 2, 2)]
        [DisplayName("Color")]
        [Description("The color tint of the cloud. This gets mixed with the color from the type.")]
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
        [CustomSortedCategory("Cloud", 2, 2)]
        [Description("The radius (size) of the cloud.")]
        public float Radius { get { return radius; } set { radius = value; UpdateScale(); lastRadius = value; } }

        private float spin;
        [CustomSortedCategory("Cloud", 2, 2)]
        [Description("Rotation of the billboard (in degrees).")]
        [TypeConverter(typeof(DegreesConverter))]
        public float Spin { get { return spin; } set { spin = value; lastSpin = value; } }

        [Browsable(false)]
        public string TypeName { get { return "Cloud"; } }

        [Browsable(false)]
        public bool AllowRotation { get; set; }

        public Cloud() : base (Vector3.Zero)
        {
            Name = "cloud" + Clouds.Count;

            Mesh = new MeshIcosphere(Vector3.Zero, Vector3.One, true);
            Mesh.Material.Translucent = true;

            Radius = lastRadius;
            Mesh.Scale = new Vector3(Radius);

            if (lastType != null)
                type = lastType;
            else
                type = CloudType.CloudTypes[0];

            Spin = lastSpin;
            Color = lastColor;

            Clouds.Add(this);
            AllowRotation = false;
        }
        public Cloud(string name, CloudType type, Vector3 position, Vector4 color, float spin, float radius) : base (position)
        {
            Name = name;
            Spin = spin;

            Mesh = new MeshIcosphere(position, Vector3.One, true);
            Mesh.Material.Translucent = true;
            Mesh.Scale = new Vector3(radius);

            Clouds.Add(this);

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

            Clouds.Remove(this);
        }

        public ISelectable Copy()
        {
            return new Cloud(Name, Type, Position, Color, Spin, Radius);
        }
    }
}
