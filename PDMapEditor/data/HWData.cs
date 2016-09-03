using NLua;
using OpenTK;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace PDMapEditor
{
    public class HWData
    {
        public static List<string> DataPaths = new List<string>();

        Lua lua = new Lua();
        LuaTable pebbleConfig;
        LuaTable asteroidConfig;
        LuaTable dustCloudConfig;

        static CultureInfo InvariantCulture = CultureInfo.InvariantCulture;

        public static void ParseDataPaths()
        {
            //Check if there are any data paths
            if (DataPaths.Count <= 0)
            {
                MessageBox.Show("You did not specify any data paths yet!\nThis is needed for parsing of pebble types etc.\nDefine them in the settings window.", "No data paths specified", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //Check if the data paths are still valid
            List<string> pathsToRemove = new List<string>();
            foreach (string dataPath in DataPaths)
            {
                if (!File.Exists(Path.Combine(dataPath, "keeper.txt")))
                {
                    MessageBox.Show("Could not find keeper.txt in \"" + dataPath + "\".\nRemoving data path from list...", "Data path invalid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    pathsToRemove.Add(dataPath);
                }
            }

            //Remove invalid data paths
            foreach (string pathToRemove in pathsToRemove)
            {
                DataPaths.Remove(pathToRemove);
            }

            //Parse data
            foreach (string dataPath in DataPaths)
            {
                //Parse pebble types
                string pebbleTypesPath = Path.Combine(dataPath, "pebble");

                //Check if pebble types folder exists
                if (Directory.Exists(pebbleTypesPath))
                {
                    string[] files = Directory.GetFiles(pebbleTypesPath, "*.pebble", SearchOption.AllDirectories);
                    foreach (string file in files)
                    {
                        Program.HWData.ParsePebbleType(file);
                    }
                }

                //Parse asteroid types
                string asteroidTypesPath = Path.Combine(dataPath, "resource/asteroid");

                //Check if asteroid types folder exists
                if (Directory.Exists(asteroidTypesPath))
                {
                    string[] files = Directory.GetFiles(asteroidTypesPath, "*.resource", SearchOption.AllDirectories);
                    foreach (string file in files)
                    {
                        Program.HWData.ParseAsteroidType(file);
                    }
                }

                //Parse dustCloud types
                string dustCloudTypesPath = Path.Combine(dataPath, "resource/dustcloud");

                //Check if dustCloud types folder exists
                if (Directory.Exists(dustCloudTypesPath))
                {
                    string[] files = Directory.GetFiles(dustCloudTypesPath, "*.resource", SearchOption.AllDirectories);
                    foreach (string file in files)
                    {
                        Program.HWData.ParseDustCloudType(file);
                    }
                }

                //Parse backgrounds
                string backgroundsPath = Path.Combine(dataPath, "background");

                //Check if background folder exists
                if (Directory.Exists(backgroundsPath))
                {
                    string[] directories = Directory.GetDirectories(backgroundsPath, "*", SearchOption.TopDirectoryOnly);
                    foreach (string directory in directories)
                    {
                        string name = Path.GetDirectoryName(directory + "/meh.txt");
                        string[] split = name.Split(Path.DirectorySeparatorChar);
                        name = split[split.Length - 1];

                        if (Background.GetBackgroundFromName(name) == null) //Don't create duplicate backgrounds (because of multiple data paths)
                            new Background(name);
                    }
                }
            }
        }

        private void ParsePebbleType(string path)
        {
            lua = new Lua();
            lua.RegisterFunction("StartPebbleConfig", this, GetType().GetMethod("StartPebbleConfig"));
            lua.DoFile(path);

            string name = Path.GetFileNameWithoutExtension(path).ToLower();

            float pixelSize = 1;
            Vector4 pixelColor = Vector4.Zero;
            foreach (KeyValuePair<object, object> de in pebbleConfig)
            {
                switch (de.Key.ToString())
                {
                    case "pixelSize":
                        string text = de.Value.ToString();
                        text = text.Replace(",", ".");
                        pixelSize = float.Parse(text, InvariantCulture);
                        break;
                    case "pixelColour":
                        pixelColor = LuaMap.LuaTableToVector4((LuaTable)de.Value);
                        break;
                }
            }

            PebbleType existingType = null;
            foreach (PebbleType type in PebbleType.PebbleTypes)
            {
                if (type.Name == name)
                {
                    existingType = type;
                    break;
                }
            }

            //Check if a type with that name already exists (because of multiple data paths)
            if (existingType == null)
                new PebbleType(name, pixelSize, pixelColor);
            else
            {
                //Overwrite existing style
                existingType.Name = name;
                existingType.PixelSize = pixelSize;
                existingType.PixelColor = pixelColor;
            }
        }

        private void ParseAsteroidType(string path)
        {
            lua = new Lua();
            lua.RegisterFunction("StartAsteroidConfig", this, GetType().GetMethod("StartAsteroidConfig"));
            lua.RegisterFunction("setCollisionDamageToModifier", this, GetType().GetMethod("SetCollisionDamageToModifier"));
            lua.RegisterFunction("setCollisionDamageFromModifier", this, GetType().GetMethod("SetCollisionDamageFromModifier"));
            lua.RegisterFunction("SpawnAsteroidOnDeath", this, GetType().GetMethod("SpawnAsteroidOnDeath"));
            lua.RegisterFunction("resourceAttackMode", this, GetType().GetMethod("ResourceAttackMode"));
            lua.DoFile(path);

            string name = Path.GetFileNameWithoutExtension(path).ToLower();

            Vector4 pixelColor = Vector4.Zero;
            float resourceValue = 0;
            foreach (KeyValuePair<object, object> de in asteroidConfig)
            {
                switch (de.Key.ToString())
                {
                    case "pixelColour":
                        pixelColor = LuaMap.LuaTableToVector4((LuaTable)de.Value);
                        break;
                    case "resourceValue":
                        string text = de.Value.ToString();
                        text = text.Replace(",", ".");
                        resourceValue = float.Parse(text, InvariantCulture);
                        break;
                }
            }

            AsteroidType existingType = null;
            foreach (AsteroidType type in AsteroidType.AsteroidTypes)
            {
                if (type.Name == name)
                {
                    existingType = type;
                    break;
                }
            }

            //Check if a type with that name already exists (because of multiple data paths)
            if (existingType == null)
                new AsteroidType(name, pixelColor, resourceValue);
            else
            {
                //Overwrite existing style
                existingType.Name = name;
                existingType.PixelColor = pixelColor;
                existingType.ResourceValue = resourceValue;
            }
        }

        private void ParseDustCloudType(string path)
        {
            lua = new Lua();
            lua.RegisterFunction("StartDustCloudConfig", this, GetType().GetMethod("StartDustCloudConfig"));
            /*lua.RegisterFunction("setCollisionDamageToModifier", this, GetType().GetMethod("SetCollisionDamageToModifier"));
            lua.RegisterFunction("setCollisionDamageFromModifier", this, GetType().GetMethod("SetCollisionDamageFromModifier"));
            lua.RegisterFunction("SpawnAsteroidOnDeath", this, GetType().GetMethod("SpawnAsteroidOnDeath"));
            lua.RegisterFunction("resourceAttackMode", this, GetType().GetMethod("ResourceAttackMode"));*/
            lua.DoFile(path);

            string name = Path.GetFileNameWithoutExtension(path).ToLower();

            Vector4 pixelColor = Vector4.Zero;
            foreach (KeyValuePair<object, object> de in dustCloudConfig)
            {
                switch (de.Key.ToString())
                {
                    case "pixelColour":
                        pixelColor = LuaMap.LuaTableToVector4((LuaTable)de.Value);
                        break;
                }
            }

            DustCloudType existingType = null;
            foreach (DustCloudType type in DustCloudType.DustCloudTypes)
            {
                if (type.Name == name)
                {
                    existingType = type;
                    break;
                }
            }

            //Check if a type with that name already exists (because of multiple data paths)
            if (existingType == null)
                new DustCloudType(name, pixelColor);
            else
            {
                //Overwrite existing style
                existingType.Name = name;
                existingType.PixelColor = pixelColor;
            }
        }

        #region Lua functions
        public LuaTable StartPebbleConfig()
        {
            pebbleConfig = (LuaTable)lua.DoString("return {}")[0];
            return pebbleConfig;
        }
        public LuaTable StartAsteroidConfig()
        {
            asteroidConfig = (LuaTable)lua.DoString("return {}")[0];
            return asteroidConfig;
        }
        public LuaTable StartDustCloudConfig()
        {
            dustCloudConfig = (LuaTable)lua.DoString("return {}")[0];
            return dustCloudConfig;
        }

        public void SetCollisionDamageToModifier(LuaTable entity, string family, float modifier)
        {
            //Unused
        }

        public void SetCollisionDamageFromModifier(LuaTable entity, string family, float modifier)
        {
            //Unused
        }

        public void SpawnAsteroidOnDeath(LuaTable entity, string type, int count, float x, float y, float z, float randomX, float randomY, float randomZ, float velocityX, float velocityY, float velocityZ, float unknown1, float unknown2, float unknown3, float unknown4, float unknown5, float unknown6, float unknown7)
        {
            //SpawnAsteroidOnDeath(NewResourceTypeAsteroid_5_piece01, "Asteroid_4_piece01", 1, 0,  0,       0,            50,           -65,             0,             -30,               0,               0,              0,              0,              8,              0,              0,             -5,              2)
            //Unused
        }

        public void ResourceAttackMode(LuaTable entity, LuaBase mode)
        {
            //Unused
        }
        #endregion
    }
}
