using OpenTK;

namespace PDMapEditor
{
    public class Dot : Drawable
    {
        public Dot(Vector3 position, Vector3 color, float size) : base(position)
        {
            Mesh = new MeshDot(position, color, size);
        }
    }
}
