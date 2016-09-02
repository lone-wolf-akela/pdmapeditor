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

        public AsteroidType Type;
        public float Multiplier;
        public float RotSpeed;

        public Asteroid(AsteroidType type, Vector3 position, Vector3 rotation, float multiplier, float rotSpeed) : base (position, rotation)
        {
            Type = type;

            Mesh = new Mesh(position, rotation, Mesh.Asteroid);
            Mesh.Material.DiffuseColor = new Vector3(type.PixelColor);

            Multiplier = multiplier;
            RotSpeed = rotSpeed;

            float scale = type.ResourceValue / 140;
            scale = Math.Max(scale, 35);
            Mesh.Scale = new Vector3(scale);

            Asteroids.Add(this);
        }
    }
}
