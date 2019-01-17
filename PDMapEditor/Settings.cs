using OpenTK;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml.Linq;

namespace PDMapEditor
{
    public partial class Settings : Form
    {
        public static System.Drawing.Point LastWindowLocation = new System.Drawing.Point(100, 100);
        public static Size LastWindowSize = new Size(1300, 900);
        public static FormWindowState LastWindowState = FormWindowState.Normal;

        private int oldComboFSAAIndex;
        private bool hideFSAAMessage;

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

            hideFSAAMessage = true;
            switch (Program.FSAASamples)
            {
                case 0:
                    comboFSAASamples.SelectedIndex = 0;
                    break;
                case 2:
                    comboFSAASamples.SelectedIndex = 1;
                    break;
                case 4:
                    comboFSAASamples.SelectedIndex = 2;
                    break;
            }
            hideFSAAMessage = false;

            radioRectangularGrid.Checked = !Map.PolarGrid;
            radioPolarGrid.Checked = Map.PolarGrid;

            checkVSync.Checked = Renderer.EnableVSync;
            checkCheckForUpdates.Checked = Updater.CheckForUpdatesOnStart;
        }

        //------------------------------------------ SETTINGS SAVING ----------------------------------------//
        public static void SaveSettings()
        {
            XElement settings = new XElement("settings");
            settings.Add(new XElement("polarGrid", Map.PolarGrid));

            foreach (string dataPath in HWData.DataPaths)
            {
                settings.Add(new XElement("dataPath", dataPath));
            }

            settings.Add(new XElement("fsaaSamples", Program.FSAASamples));
            settings.Add(new XElement("enableVSync", Renderer.EnableVSync));
            settings.Add(new XElement("checkForUpdatesOnStart", Updater.CheckForUpdatesOnStart));

            settings.Add(new XElement("lastOpenLocation", Program.main.LastOpenLocation));
            settings.Add(new XElement("lastSaveLocation", Program.main.LastSaveLocation));
            settings.Add(new XElement("lastWindowLocationX", Program.main.Location.X));
            settings.Add(new XElement("lastWindowLocationY", Program.main.Location.Y));
            settings.Add(new XElement("lastWindowSizeX", Program.main.Size.Width));
            settings.Add(new XElement("lastWindowSizeY", Program.main.Size.Height));
            settings.Add(new XElement("lastWindowState", Program.main.WindowState));

            File.WriteAllText(Path.Combine(Program.EXECUTABLE_PATH, "settings.xml"), settings.ToString());
        }

        public static void LoadSettings()
        {
            if (!File.Exists(Path.Combine(Program.EXECUTABLE_PATH, "settings.xml")))
            {
                Log.WriteLine("No settings.xml found, using default values.");
                return;
            }

            int lastWindowLocationX = 100;
            int lastWindowLocationY = 100;

            int lastWindowSizeX = 1300;
            int lastWindowSizeY = 900;

            try
            {
                string file = File.ReadAllText(Path.Combine(Program.EXECUTABLE_PATH, "settings.xml"));
                XElement settings = XElement.Parse(file);

                foreach (XElement element in settings.Elements())
                {
                    switch (element.Name.LocalName)
                    {
                        case "polarGrid":
                            bool value = false;
                            bool.TryParse(element.Value, out value);
                            Map.PolarGrid = value;
                            break;
                        case "dataPath":
                            HWData.DataPaths.Add(element.Value);
                            break;
                        case "fsaaSamples":
                            int fsaaSamples = 4;
                            int.TryParse(element.Value, out fsaaSamples);
                            Program.FSAASamples = fsaaSamples;
                            break;
                        case "enableVSync":
                            bool enableVSync = true;
                            bool.TryParse(element.Value, out enableVSync);
                            Renderer.EnableVSync = enableVSync;
                            break;
                        case "checkForUpdatesOnStart":
                            bool checkForUpdatesOnStart = true;
                            bool.TryParse(element.Value, out checkForUpdatesOnStart);
                            Updater.CheckForUpdatesOnStart = checkForUpdatesOnStart;
                            break;
                        case "lastOpenLocation":
                            Program.main.LastOpenLocation = element.Value;
                            break;
                        case "lastSaveLocation":
                            Program.main.LastSaveLocation = element.Value;
                            break;
                        case "lastWindowLocationX":
                            int intValue = 100;
                            int.TryParse(element.Value, out intValue);
                            lastWindowLocationX = intValue;
                            break;
                        case "lastWindowLocationY":
                            intValue = 100;
                            int.TryParse(element.Value, out intValue);
                            lastWindowLocationY = intValue;
                            break;
                        case "lastWindowSizeX":
                            intValue = 1300;
                            int.TryParse(element.Value, out intValue);
                            lastWindowSizeX = intValue;
                            break;
                        case "lastWindowSizeY":
                            intValue = 900;
                            int.TryParse(element.Value, out intValue);
                            lastWindowSizeY = intValue;
                            break;
                        case "lastWindowState":
                            FormWindowState state = FormWindowState.Normal;
                            Enum.TryParse(element.Value, out state);
                            LastWindowState = state;
                            break;
                    }
                }

                LastWindowLocation = new System.Drawing.Point(lastWindowLocationX, lastWindowLocationY);
                LastWindowSize = new Size(lastWindowSizeX, lastWindowSizeY);
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

        private void comboFSAASamples_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboFSAASamples.SelectedIndex)
            {
                case 0:
                    Program.FSAASamples = 0;
                    break;
                case 1:
                    Program.FSAASamples = 2;
                    break;
                case 2:
                    Program.FSAASamples = 4;
                    break;
            }

            if (comboFSAASamples.SelectedIndex != oldComboFSAAIndex && !hideFSAAMessage)
                MessageBox.Show("This action will come into effect after the program has been restarted.", "Restart needed", MessageBoxButtons.OK, MessageBoxIcon.Information);

            oldComboFSAAIndex = comboFSAASamples.SelectedIndex;
        }
        private void checkVSync_CheckedChanged(object sender, EventArgs e)
        {
            Renderer.EnableVSync = checkVSync.Checked;
        }

        //------------------------ GRID ------------------------//
        private void radioRectangularGrid_CheckedChanged(object sender, EventArgs e)
        {
            Map.PolarGrid = !radioRectangularGrid.Checked;
        }
        private void radioPolarGrid_CheckedChanged(object sender, EventArgs e)
        {
            Map.PolarGrid = radioPolarGrid.Checked;
        }

        //------------------------ UPDATER ---------------------//
        private void checkCheckForUpdates_CheckedChanged(object sender, EventArgs e)
        {
            Updater.CheckForUpdatesOnStart = checkCheckForUpdates.Checked;
        }
    }
}
