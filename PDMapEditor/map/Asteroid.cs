using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDMapEditor
{
    public class Asteroid : Drawable, ISelectable
    {
        public static List<Asteroid> Asteroids = new List<Asteroid>();

        private AsteroidType type;
        public AsteroidType Type { get { return type; } set { type = value; UpdateScale(); } }

        public float Multiplier;
        public float RotSpeed;

        public bool AllowRotation { get; set; }

        public Asteroid() : base(Vector3.Zero, Vector3.Zero)
        {
            type = AsteroidType.GetTypeFromComboIndex(Program.main.comboAsteroidType.SelectedIndex);

            Mesh = new Mesh(Vector3.Zero, Vector3.Zero, Mesh.Asteroid);

            Mesh.Material.DiffuseColor = new Vector3(type.PixelColor);

            Type = type;
            Multiplier = (float)Program.main.numericAsteroidResourceMultiplier.Value;

            Asteroids.Add(this);
            AllowRotation = true;
        }

        public Asteroid(AsteroidType type, Vector3 position, Vector3 rotation, float multiplier, float rotSpeed) : base (position, rotation)
        {
            Mesh = new Mesh(position, rotation, Mesh.Asteroid);
            Mesh.Material.DiffuseColor = new Vector3(type.PixelColor);

            Multiplier = multiplier;
            RotSpeed = rotSpeed;

            Type = type;

            Asteroids.Add(this);

            AllowRotation = true;
        }

        private void UpdateScale()
        {
            float scale = Type.ResourceValue / 140;
            scale = Math.Max(scale, 35);
            Mesh.Scale = new Vector3(scale);
        }

        public override void Destroy()
        {
            base.Destroy();

            Asteroids.Remove(this);
        }

        public ISelectable Copy()
        {
            return new Asteroid(Type, Position, Rotation, Multiplier, RotSpeed);
        }
    }
}
