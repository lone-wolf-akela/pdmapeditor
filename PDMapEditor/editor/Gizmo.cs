using OpenTK;

namespace PDMapEditor
{
    public class Gizmo : Drawable, ISelectable
    {
        public bool AllowRotation { get; set; }

        public Gizmo(Vector3 position, Assimp.Mesh assMesh) : base(position, assMesh)
        {
            Mesh = new Mesh(position, Vector3.Zero, assMesh)
            {
                DrawInFront = true
            };

            AllowRotation = true;
        }

        public ISelectable Copy()
        {
            return null;
        }
    }
}
