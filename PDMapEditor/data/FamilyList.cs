using NLua;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDMapEditor
{
    public static class FamilyList
    {
        public static List<string> AvoidanceFamilies = new List<string>();

        static Lua lua;

        public static void Parse(string path)
        {
            //Clear from other data paths
            AvoidanceFamilies.Clear();

            lua = new Lua();
            lua.DoFile(path);

            LuaTable avoidanceFamilies = lua.GetTable("avoidanceFamily");
            foreach (LuaTable table in avoidanceFamilies)
            {
                string name = "";
                int numParam = -1;

                foreach (KeyValuePair<object, object> de in table.Values)
                {
                    switch (de.Key.ToString())
                    {
                        case "name":
                            name = de.Value.ToString();
                            break;
                        case "numParam":
                            string text = de.Value.ToString();
                            numParam = int.Parse(text);
                            break;
                    }
                }

                if (name.Length > 0)
                    AvoidanceFamilies.Add(name);
            }
        }
    }
}
