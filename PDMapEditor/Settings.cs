using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace PDMapEditor
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
        }

        public void Open()
        {
            listDataPaths.Items.Clear();
            listDataPaths.Items.AddRange(HWData.DataPaths.ToArray());

            if(Map.Background != null)
                sliderFadeBackground.Value = (int)Math.Round(Map.Background.Fade * 100);
            else
                sliderFadeBackground.Value = 50;
        }

        //------------------------------------------ SETTINGS SAVING ----------------------------------------//
        public static void SaveSettings()
        {
            XElement settings =
                new XElement("settings");

            foreach (string dataPath in HWData.DataPaths)
            {
                settings.Add(new XElement("dataPath", dataPath));
            }

            File.WriteAllText(Path.Combine(Program.EXECUTABLE_PATH, "settings.xml"), settings.ToString());
        }

        public static void LoadSettings()
        {
            if (!File.Exists(Path.Combine(Program.EXECUTABLE_PATH, "settings.xml")))
            {
                Log.WriteLine("No settings.xml found, using default values.");
                return;
            }

            try
            {
                string file = File.ReadAllText(Path.Combine(Program.EXECUTABLE_PATH, "settings.xml"));
                XElement settings = XElement.Parse(file);

                foreach (XElement element in settings.Elements())
                {
                    switch (element.Name.LocalName)
                    {
                        case "dataPath":
                            HWData.DataPaths.Add(element.Value);
                            break;
                    }
                }
            }
            catch
            {
                Log.WriteLine("Failed to load \"" + Path.Combine(Program.EXECUTABLE_PATH, "settings.xml") + "\".");
            }
        }

        //------------------------------------------ DATA PATHS ----------------------------------------//
        private void buttonAddDataPath_Click(object sender, EventArgs e)
        {
            DialogResult result = addDataPathDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                string path = Path.GetDirectoryName(addDataPathDialog.FileName);

                if (HWData.DataPaths.Contains(path))
                {
                    MessageBox.Show("This data path has already been added to the list.", "Data path already added", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                HWData.DataPaths.Add(path);
                listDataPaths.Items.Add(path);
                MessageBox.Show("This action will come into effect after the program has been restarted.", "Restart needed", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void buttonRemoveDataPath_Click(object sender, EventArgs e)
        {
            if (listDataPaths.SelectedItem != null)
            {
                string path = (string)listDataPaths.SelectedItem;
                HWData.DataPaths.Remove(path);
                listDataPaths.Items.Remove(path);
            }
        }


        //------------------------ VIEW ------------------------//
        private void sliderFadeBackground_Scroll(object sender, EventArgs e)
        {
            if(Map.Background != null)
                Map.Background.Fade = (float)sliderFadeBackground.Value / 100;
        }
    }
}
