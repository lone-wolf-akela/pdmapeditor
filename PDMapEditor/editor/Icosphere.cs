using OpenTK;

namespace PDMapEditor
{
    public class Icosphere : Drawable
    {
        public Icosphere(Vector3 position, Vector3 color) : base(position)
        {
            Mesh = new MeshIcosphere(position, color);
        }

        public Icosphere(Vector3 position, Vector3 color, bool detailed) : base(position)
        {
            Mesh = new MeshIcosphere(position, color, detailed);
        }
    }
}
