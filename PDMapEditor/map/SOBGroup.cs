using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PDMapEditor
{
    public class SOBGroup
    {
        public static List<SOBGroup> SOBGroups = new List<SOBGroup>();

        public static SOBGroup CurrentSelected;

        public static SOBGroup GetByItemIndex(int itemIndex)
        {
            SOBGroup SOBGroup = null;

            foreach(SOBGroup group in SOBGroups)
            {
                if(group.ItemIndex == itemIndex)
                {
                    SOBGroup = group;
                    break;
                }
            }

            return SOBGroup;
        }

        public static SOBGroup GetByName(string name)
        {
            SOBGroup SOBGroup = null;

            foreach (SOBGroup group in SOBGroups)
            {
                if (group.Name == name)
                {
                    SOBGroup = group;
                    break;
                }
            }

            return SOBGroup;
        }

        private string name;
        public string Name { get { return name; } set { name = value; } }

        public int ItemIndex;

        public List<Squadron> Squadrons = new List<Squadron>();

        public SOBGroup(string name)
        {
            this.name = name;

            SOBGroups.Add(this);
            Program.main.AddSOBGroup(this);
        }

        public void Destroy()
        {
            Program.main.RemoveSOBGroup(this);
            foreach(Squadron squadron in Squadrons)
            {
                RemoveSquadron(squadron);
            }
            SOBGroups.Remove(this);
        }

        public void AddSquadron(Squadron squadron)
        {
            squadron.SOBGroups.Add(this);
            Squadrons.Add(squadron);
        }

        public void RemoveSquadron(Squadron squadron)
        {
            squadron.SOBGroups.Remove(this);
            Squadrons.Remove(squadron);
        }
    }
}
