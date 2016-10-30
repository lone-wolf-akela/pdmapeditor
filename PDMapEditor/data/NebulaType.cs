using System.Collections.Generic;
using System.ComponentModel;

namespace PDMapEditor
{
    [TypeConverter(typeof(NebulaTypeConverter))]
    public class NebulaType
    {
        public static List<NebulaType> NebulaTypes = new List<NebulaType>();

        public string Name;

        public NebulaType(string name)
        {
            Name = name;

            NebulaTypes.Add(this);
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
    }
}
