using OpenTK;

namespace PDMapEditor
{
    public interface ISelectable
    {
        Vector3 Position { get; set; }
        Vector3 Rotation { get; set; }

        bool AllowRotation { get; }

        void Destroy();
        ISelectable Copy();
    }
}
