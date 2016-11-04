using OpenTK;
using System.ComponentModel;

namespace PDMapEditor
{
    public interface IElement : ISelectable
    {
        string TypeName { get; }
    }
}
