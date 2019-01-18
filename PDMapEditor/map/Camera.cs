using OpenTK;
using System.Collections.Generic;
using System.ComponentModel;

namespace PDMapEditor
{
    public class Camera : Drawable, ISelectable, IElement
    {
        public static List<Camera> Cameras = new List<Camera>();
        private static string lastName = string.Empty;
        new private static Vector3 lastTarget = Vector3.Zero;

        private Line Line;

        private string name;
        [CustomSortedCategory("Camera", 2, 2)]
        [Description("The name of the camera. It can be used to refer to this camera with scripts.")]
        public string Name { get { return name; } set { name = value; lastName = value; } }

        public override Vector3 Position { get { return base.Position; } set { base.Position = value; if(Line != null) Line.Start = value; } }

        public override Vector3 Target { get { return base.Target; } set { base.Target = value; if (Line != null)  Line.End = value; } }

        [DisplayName("X")]
        [Description("The X-coordinate of the target.")]
        [CustomSortedCategory("Target", 1, 3)]
        public float TargetX
        {
            get
            {
                return Target.X;
            }
            set
            {
                Target = new Vector3(value, Target.Y, Target.Z);
                lastTarget = Target;
            }
        }
        [DisplayName("Y")]
        [Description("The Y-coordinate of the target.")]
        [CustomSortedCategory("Target", 1, 3)]
        public float TargetY
        {
            get
            {
                return Target.Y;
            }
            set
            {
                Target = new Vector3(Target.X, value, Target.Z);
                lastTarget = Target;
            }
        }
        [DisplayName("Z")]
        [Description("The Z-coordinate of the target.")]
        [CustomSortedCategory("Target", 1, 3)]
        public float TargetZ
        {
            get
            {
                return Target.Z;
            }
            set
            {
                Target = new Vector3(Target.X, Target.Y, value);
                lastTarget = Target;
            }
        }

        public override bool Visible { get { return base.Visible; } set { base.Visible = value; if (Line != null) Line.Visible = value; } }

        [Browsable(false)]
        public string TypeName { get { return "Camera"; } }

        [Browsable(false)]
        public bool AllowRotation { get; set; }

        public Camera() : base (Vector3.Zero, Vector3.Zero)
        {
            CreateLine();

            Name = "camera_" + Cameras.Count;

            Mesh = new Mesh(Vector3.Zero, Vector3.Zero, Mesh.Camera);
            Mesh.Material.DiffuseColor = new Vector3(1, 1, 0);
            Mesh.Scale = new Vector3(10);

            Target = lastTarget;

            Cameras.Add(this);

            AllowRotation = false;
            RotationSetByTarget = true;
        }
        public Camera(string name, Vector3 position, Vector3 target) : base (position)
        {
            CreateLine();

            Name = name;
            Mesh = new Mesh(position, Vector3.Zero, Mesh.Camera);
            Mesh.Material.DiffuseColor = new Vector3(1, 1, 0);
            Mesh.Scale = new Vector3(10);

            Target = target;

            Cameras.Add(this);

            AllowRotation = false;
            RotationSetByTarget = true;
        }

        public override void Destroy()
        {
            base.Destroy();

            Cameras.Remove(this);
            Line.Destroy();
        }

        public ISelectable Copy()
        {
            return new Camera(Name, Position, Target);
        }

        private void CreateLine()
        {
            Line = new Line(Position, Target, new Vector3(1, 1, 0));
        }
    }
}
