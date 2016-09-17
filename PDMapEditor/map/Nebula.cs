using OpenTK;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDMapEditor
{
    public class Nebula : Drawable, ISelectable
    {
        public static List<Nebula> Nebulas = new List<Nebula>();

        public string Name;

        public NebulaType Type;

        private Vector4 color;
        public Vector4 Color { get { return color; } set { color = value; UpdateColor(); } }

        public float Resources;
        public float Spin;
        public bool Refill;

        private float radius;
        public float Radius { get { return radius; } set { radius = value; UpdateScale(); } }

        public bool AllowRotation { get; set; }

        public Nebula() : base(Vector3.Zero)
        {
            Name = Program.main.boxNebulaName.Text;

            Mesh = new MeshIcosphere(Vector3.Zero, Vector3.One);
            Mesh.Material.Translucent = true;

            Radius = (float)Program.main.numericNebulaSize.Value;
            Mesh.Scale = new Vector3(Radius);
            Nebulas.Add(this);

            Type = NebulaType.GetTypeFromComboIndex(Program.main.comboNebulaType.SelectedIndex);
            Resources = (float)Program.main.numericNebulaResources.Value;

            Color buttonColor = Program.main.buttonNebulaColor.BackColor;
            Color = new Vector4((float)buttonColor.R / 255, (float)buttonColor.G / 255, (float)buttonColor.B / 255, (float)Program.main.sliderNebulaAlpha.Value / 100);

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
