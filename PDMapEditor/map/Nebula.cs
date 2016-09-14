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

        public float Unknown;

        private float size;
        public float Size { get { return size; } set { size = value; UpdateScale(); } }

        public bool AllowRotation { get; set; }

        public Nebula() : base(Vector3.Zero)
        {
            Name = Program.main.boxNebulaName.Text;

            Mesh = new MeshIcosphere(Vector3.Zero, Vector3.One);
            Mesh.Material.Translucent = true;

            Size = (float)Program.main.numericNebulaSize.Value;
            Mesh.Scale = new Vector3(Size);
            Nebulas.Add(this);

            Type = NebulaType.GetTypeFromComboIndex(Program.main.comboNebulaType.SelectedIndex);

            Color buttonColor = Program.main.buttonNebulaColor.BackColor;
            Color = new Vector4((float)buttonColor.R / 255, (float)buttonColor.G / 255, (float)buttonColor.B / 255, (float)Program.main.sliderNebulaAlpha.Value / 100);

            AllowRotation = false;
        }
        public Nebula(string name, NebulaType type, Vector3 position, Vector4 color, float unknown, float size) : base (position)
        {
            Name = name;
            Unknown = unknown;

            Mesh = new MeshIcosphere(position, Vector3.One);
            Mesh.Material.Translucent = true;
            Mesh.Scale = new Vector3(size);

            Nebulas.Add(this);

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
            return new Nebula(Name, Type, Position, Color, Unknown, Size);
        }
    }
}
