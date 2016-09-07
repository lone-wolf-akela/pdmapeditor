using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDMapEditor
{
    public class NebulaType
    {
        public static List<NebulaType> NebulaTypes = new List<NebulaType>();

        public string Name;

        public int ComboIndex = -1;

        public NebulaType(string name)
        {
            Name = name;

            NebulaTypes.Add(this);

            Program.main.comboNebulaType.Items.Add(Name);
            ComboIndex = Program.main.comboNebulaType.Items.Count - 1;
        }

        public static NebulaType GetTypeFromName(string name)
        {
            foreach(NebulaType type in NebulaTypes)
            {
                if (type.Name == name)
                    return type;
            }

            return null;
        }

        public static NebulaType GetTypeFromComboIndex(int index)
        {
            foreach (NebulaType type in NebulaTypes)
            {
                if (type.ComboIndex == index)
                    return type;
            }

            return null;
        }
    }
}
