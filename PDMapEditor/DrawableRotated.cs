using OpenTK;
using System.ComponentModel;

namespace PDMapEditor
{
    public class DrawableRotated : Drawable
    {
        [DisplayName("X")]
        [Description("The rotation around the X-axis.")]
        [CustomSortedCategory("Rotation", 2, 3)]
        public float RotX
        {
            get
            {
                return Rotation.X;
            }
            set
            {
                Rotation = new Vector3(value, Rotation.Y, Rotation.Z);
            }
        }
        [DisplayName("Y")]
        [Description("The rotation around the Y-axis.")]
        [CustomSortedCategory("Rotation", 2, 3)]
        public float RotY
        {
            get
            {
                return Rotation.Y;
            }
            set
            {
                Rotation = new Vector3(Rotation.X, value, Rotation.Z);
            }
        }
        [DisplayName("Z")]
        [Description("The rotation around the Z-axis.")]
        [CustomSortedCategory("Rotation", 2, 3)]
        public float RotZ
        {
            get
            {
                return Rotation.Z;
            }
            set
            {
                Rotation = new Vector3(Rotation.X, Rotation.Y, value);
            }
        }

        public DrawableRotated(Vector3 position) : base(position)
        {
        }
        public DrawableRotated(Vector3 position, Vector3 rotation) : base(position, rotation)
        {
        }
        public DrawableRotated(Vector3 position, Assimp.Mesh assMesh) : base(position)
        {
        }
        public DrawableRotated(Vector3 position, Vector3 rotation, Assimp.Mesh assMesh) : base(position, rotation)
        {
        }
    }
}
