using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
