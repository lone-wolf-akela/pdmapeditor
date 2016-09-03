using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDMapEditor
{
    public class Background
    {
        public static List<Background> Backgrounds = new List<Background>();

        public string Name;
        public int ComboBackgroundIndex;

        public Background(string name)
        {
            Name = name;

            Backgrounds.Add(this);

            Program.main.comboBackground.Items.Add(name);
            ComboBackgroundIndex = Program.main.comboBackground.Items.Count - 1;
        }

        public static Background GetBackgroundFromName(string name)
        {
            foreach (Background bg in Backgrounds)
            {
                if (bg.Name == name)
                    return bg;
            }

            return null;
        }
    }
}
