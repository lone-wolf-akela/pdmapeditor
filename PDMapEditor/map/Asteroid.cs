using OpenTK;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace PDMapEditor
{
    public class Asteroid : DrawableRotated, ISelectable, IElement
    {
        public static List<Asteroid> Asteroids = new List<Asteroid>();
        private static AsteroidType lastType;
        private static float lastMultiplier = 100;
        private static float lastRotSpeed = 0;

        private AsteroidType type;
        [CustomSortedCategory("Asteroid", 2, 2)]
        [Description("The type of the asteroid. From data/resource/asteroid/.")]
        public AsteroidType Type { get { return type; } set { type = value; UpdateScale(); UpdateColor(); lastType = value; Renderer.Invalidate(); Renderer.InvalidateView(); } }

        private float multiplier;
        [CustomSortedCategory("Asteroid", 2, 2)]
        [DisplayName("RU Multiplier")]
        [Description("Resource multiplier in percent.")]
        [TypeConverter(typeof(PercentConverter))]
        public float Multiplier { get { return multiplier; } set { multiplier = value; lastMultiplier = value; } }

        private float rotSpeed;
        [CustomSortedCategory("Asteroid", 2, 2)]
        [DisplayName("Rotation speed")]
        [Description("Rotation speed of the asteroid.")]
        public float RotSpeed { get { return rotSpeed; } set { rotSpeed = value; lastRotSpeed = value; } }

        [Browsable(false)]
        public string TypeName { get { return "Asteroid"; } }

        [Browsable(false)]
        public bool AllowRotation { get; set; }

        public Asteroid() : base(Vector3.Zero, Vector3.Zero)
        {
            if (lastType != null)
                type = lastType;
            else
                type = AsteroidType.AsteroidTypes[0];

            Mesh = new Mesh(Vector3.Zero, Vector3.Zero, Mesh.Asteroid);

            Type = type;
            Multiplier = lastMultiplier;
            RotSpeed = lastRotSpeed;

            Asteroids.Add(this);
            AllowRotation = true;
        }

        public Asteroid(AsteroidType type, Vector3 position, Vector3 rotation, float multiplier, float rotSpeed) : base (position, rotation)
        {
            Mesh = new Mesh(position, rotation, Mesh.Asteroid);
            
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

        private void UpdateColor()
        {
            Mesh.Material.DiffuseColor = new Vector3(Type.PixelColor);
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
