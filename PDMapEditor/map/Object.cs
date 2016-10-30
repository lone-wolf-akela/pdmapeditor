using OpenTK;

namespace PDMapEditor
{
    public abstract class Object
    {
        private Vector3 position;
        public virtual Vector3 Position { get { return position; } set { position = value; } }

        private Vector3 rotation;
        public virtual Vector3 Rotation { get { return rotation; } set { rotation = value; } }

        public Object(Vector3 position)
        {
            Position = position;
        }

        public Object(Vector3 position, Vector3 rotation)
        {
            Position = position;
            Rotation = rotation;
        }
    }
}
