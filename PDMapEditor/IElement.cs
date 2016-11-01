using OpenTK;

namespace PDMapEditor
{
    public interface IElement : ISelectable
    {
        string TypeName { get; }
    }
}
