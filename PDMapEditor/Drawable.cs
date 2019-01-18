using OpenTK;
using System.Collections.Generic;
using System.ComponentModel;

namespace PDMapEditor
{
    public class Drawable : Object
    {
        public static List<Drawable> Drawables = new List<Drawable>();

        private Vector3 position;
        [Browsable(false)]
        public override Vector3 Position { get { return position; } set { position = value; if(Mesh != null) Mesh.Position = value; Target = Target; Selection.InvalidateAveragePosition(); Renderer.Invalidate(); Renderer.InvalidateView(); } }

        [DisplayName("X")]
        [Description("The X-coordinate of the position.")]
        [CustomSortedCategory("Position", 1, 3)]
        public float PosX
        {
            get
            {
                if (Selection.Selected.Count < 2)
                    return Position.X;
                else
                    return Selection.AveragePosition.X;
            }
            set
            {
                if (Selection.Selected.Count < 2)
                    Position = new Vector3(value, Position.Y, Position.Z);
                else
                {
                    Position = new Vector3(value - (Selection.AveragePosition.X - Position.X), Position.Y, Position.Z);
                }
            }
        }
        [DisplayName("Y")]
        [Description("The Y-coordinate of the position.")]
        [CustomSortedCategory("Position", 1, 3)]
        public float PosY
        {
            get
            {
                if (Selection.Selected.Count < 2)
                    return Position.Y;
                else
                    return Selection.AveragePosition.Y;
            }
            set
            {
                if (Selection.Selected.Count < 2)
                    Position = new Vector3(Position.X, value, Position.Z);
                else
                {
                    Position = new Vector3(Position.X, value - (Selection.AveragePosition.Y - Position.Y), Position.Z);
                }
            }
        }
        [DisplayName("Z")]
        [Description("The Z-coordinate of the position.")]
        [CustomSortedCategory("Position", 1, 3)]
        public float PosZ
        {
            get
            {
                if (Selection.Selected.Count < 2)
                    return Position.Z;
                else
                    return Selection.AveragePosition.Z;
            }
            set
            {
                if (Selection.Selected.Count < 2)
                    Position = new Vector3(Position.X, Position.Y, value);
                else
                {
                    Position = new Vector3(Position.X, Position.Y, value - (Selection.AveragePosition.Z - Position.Z));
                }
            }
        }

        private Vector3 rotation;
        [Browsable(false)]
        public override Vector3 Rotation { get { return rotation; } set { rotation = value; if (Mesh != null) { Mesh.Rotation = value; } Renderer.Invalidate(); Renderer.InvalidateView(); } }

        private bool rotationSetByTarget = false;
        [Browsable(false)]
        public bool RotationSetByTarget
        {
            get { return rotationSetByTarget; }
            set { rotationSetByTarget = value; if (Mesh != null) { Mesh.RotationSetByTarget = value; } }
        }

        private Vector3 target;
        [Browsable(false)]
        public virtual Vector3 Target
        {
            set { target = value; if (Mesh != null) { Mesh.Target = value; } Renderer.Invalidate(); Renderer.InvalidateView(); }
            get { return target; }
        }

        public Vector3 lastPosition;
        public Vector3 lastRotation;
        public Vector3 lastTarget;

        public Mesh Mesh;

        private bool visible = true;
        [Browsable(false)]
        public virtual bool Visible { get { return visible; } set { visible = value;  Renderer.Invalidate(); Renderer.InvalidateView(); } }

        public Drawable(Vector3 position) : base(position)
        {
            Position = position;

            Drawables.Add(this);
            Renderer.InvalidateAll();
        }

        public Drawable(Vector3 position, Vector3 rotation) : base(position, rotation)
        {
            Position = position;
            Rotation = rotation;

            Drawables.Add(this);
            Renderer.InvalidateAll();
        }
        public Drawable(Vector3 position, Assimp.Mesh assMesh) : base(position)
        {
            Position = position;
            Mesh = new Mesh(position, Vector3.Zero, assMesh);

            Drawables.Add(this);
            Renderer.InvalidateAll();
        }
        public Drawable(Vector3 position, Vector3 rotation, Assimp.Mesh assMesh) : base(position, rotation)
        {
            Position = position;
            Rotation = rotation;
            Mesh = new Mesh(position, rotation, assMesh);

            Drawables.Add(this);
            Renderer.InvalidateAll();
        }

        public virtual void Destroy()
        {
            Drawables.Remove(this);
            Mesh.Remove();
            Renderer.InvalidateAll();
        }
    }
}
