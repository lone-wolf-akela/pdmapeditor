using NLua;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace PDMapEditor
{
    public static class HWData
    {
        public static List<string> DataPaths = new List<string>();

        static Lua lua = new Lua();
        static LuaTable pebbleConfig;
        static LuaTable asteroidConfig;
        static LuaTable dustCloudConfig;
        static LuaTable shipConfig;

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
                //Family list
                //string familyListPath = Path.Combine(dataPath, "scripts/familylist.lua");

                //if(File.Exists(familyListPath))
                //FamilyList.Parse(familyListPath);

                //Parse pebble types
                string pebbleTypesPath = Path.Combine(dataPath, "pebble");

                //Check if pebble types folder exists
                if (Directory.Exists(pebbleTypesPath))
                {
                    string[] files = Directory.GetFiles(pebbleTypesPath, "*.pebble", SearchOption.AllDirectories);
                    foreach (string file in files)
                    {
                        HWData.ParsePebbleType(file);
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
                        HWData.ParseAsteroidType(file);
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
                        HWData.ParseDustCloudType(file);
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

                //Parse ship types
                string shipTypesPath = Path.Combine(dataPath, "ship");

                //Check if ship types folder exists
                if (Directory.Exists(shipTypesPath))
                {
                    string[] files = Directory.GetFiles(shipTypesPath, "*.ship", SearchOption.AllDirectories);
                    foreach (string file in files)
                    {
                        HWData.ParseShipType(file);
                    }
                }
            }
        }

        private static void ParsePebbleType(string path)
        {
            lua = new Lua();
            lua.RegisterFunction("StartPebbleConfig", null, typeof(HWData).GetMethod("StartPebbleConfig"));
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

        private static void ParseAsteroidType(string path)
        {
            lua = new Lua();
            Type classType = typeof(HWData);

            lua.RegisterFunction("StartAsteroidConfig", null, classType.GetMethod("StartAsteroidConfig"));
            lua.RegisterFunction("setCollisionDamageToModifier", null, classType.GetMethod("SetCollisionDamageToModifier"));
            lua.RegisterFunction("setCollisionDamageFromModifier", null, classType.GetMethod("SetCollisionDamageFromModifier"));
            lua.RegisterFunction("SpawnAsteroidOnDeath", null, classType.GetMethod("SpawnAsteroidOnDeath"));
            lua.RegisterFunction("resourceAttackMode", null, classType.GetMethod("ResourceAttackMode"));
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

        private static void ParseDustCloudType(string path)
        {
            lua = new Lua();
            lua.RegisterFunction("StartDustCloudConfig", null, typeof(HWData).GetMethod("StartDustCloudConfig"));
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

        private static void ParseShipType(string path)
        {
            lua = new Lua();
            Type type = typeof(HWData);

            lua.RegisterFunction("StartShipConfig", null, type.GetMethod("StartShipConfig"));
            lua.RegisterFunction("getShipNum", null, type.GetMethod("GetShipNum"));
            lua.RegisterFunction("getShipStr", null, type.GetMethod("GetShipStr"));
            lua.RegisterFunction("setSupplyValue", null, type.GetMethod("SetSupplyValue"));
            lua.RegisterFunction("StartShipWeaponConfig", null, type.GetMethod("StartShipWeaponConfig"));
            lua.RegisterFunction("setEngineBurn", null, type.GetMethod("SetEngineBurn"));
            lua.RegisterFunction("setEngineGlow", null, type.GetMethod("SetEngineGlow"));
            lua.RegisterFunction("setEngineTrail", null, type.GetMethod("SetEngineTrail"));
            lua.RegisterFunction("setTargetBox", null, type.GetMethod("SetTargetBox"));
            lua.RegisterFunction("LoadModel", null, type.GetMethod("LoadModel"));
            lua.RegisterFunction("LoadSharedModel", null, type.GetMethod("LoadSharedModel"));
            lua.RegisterFunction("addShield", null, type.GetMethod("AddShield"));
            lua.RegisterFunction("AddShipAbility", null, type.GetMethod("AddShipAbility"));
            //lua.RegisterFunction("addAbility", null, type.GetMethod("AddAbility"));

            lua.DoString("luanet.load_assembly('mscorlib')");
            lua.DoString("luanet.load_assembly('PDMapEditor')");
            lua.DoString("HWData=luanet.import_type('PDMapEditor.HWData')");
            lua.DoString("data=HWData");

            lua.DoFile(path);

            string name = Path.GetFileNameWithoutExtension(path).ToLower();
            AvoidanceFamily avoidanceFamily = AvoidanceFamily.None;

            foreach (KeyValuePair<object, object> de in shipConfig)
            {
                switch (de.Key.ToString())
                {
                    case "AvoidanceFamily":
                        string family = de.Value.ToString();
                        avoidanceFamily = (AvoidanceFamily)Enum.Parse(typeof(AvoidanceFamily), family, true);
                        break;
                }
            }

            ShipType existingType = ShipType.GetTypeFromName(name);

            //Check if a type with that name already exists (because of multiple data paths)
            if (existingType == null)
                new ShipType(name, avoidanceFamily);
            else
            {
                //Overwrite existing style
                existingType.Name = name;
                existingType.AvoidanceFamily = avoidanceFamily;
            }
        }

        private static string DisableLuaFunctionInString(string text, string function)
        {
            string newText = text;

            int index = 0;
            bool inFunction = false;
            bool inString = false;

            while(index <= text.Length)
            {
                if (index < text.Length)
                {
                    if (text[index] == '\"') //String start/end found
                    {
                        inString = !inString;
                    }
                }

                if (!inFunction)
                {
                    if (text.Length >= index + function.Length)
                    {
                        if (newText.Substring(index, function.Length) == function) //Function occurence found
                        {
                            newText = newText.Insert(index, "--[[");
                            index += function.Length;
                            inFunction = true;
                        }
                    }
                }
                else
                {
                    if (!inString)
                    {
                        if (newText[index] == ')') //Function end found
                        {
                            newText = newText.Insert(index + 1, "]]");
                            inFunction = false;
                        }
                    }
                }

                index++;
            }

            return newText;
        }

        public static string GetFileInDataPaths(string relativePath)
        {
            foreach (string dataPath in DataPaths)
            {
                string combinedPath = Path.Combine(dataPath, relativePath);

                if (File.Exists(combinedPath))
                {
                    return combinedPath;
                }
            }

            return "";
        }

        #region Lua functions
        public static LuaTable StartPebbleConfig()
        {
            pebbleConfig = (LuaTable)lua.DoString("return {}")[0];
            return pebbleConfig;
        }
        public static LuaTable StartAsteroidConfig()
        {
            asteroidConfig = (LuaTable)lua.DoString("return {}")[0];
            return asteroidConfig;
        }
        public static LuaTable StartDustCloudConfig()
        {
            dustCloudConfig = (LuaTable)lua.DoString("return {}")[0];
            return dustCloudConfig;
        }

        public static LuaTable StartShipConfig()
        {
            shipConfig = (LuaTable)lua.DoString("return {}")[0];
            return shipConfig;
        }

        public static void SetCollisionDamageToModifier(LuaTable entity, string family, float modifier)
        {
            //Unused
        }

        public static void SetCollisionDamageFromModifier(LuaTable entity, string family, float modifier)
        {
            //Unused
        }

        public static void SpawnAsteroidOnDeath(LuaTable entity, string type, int count, float x, float y, float z, float randomX, float randomY, float randomZ, float velocityX, float velocityY, float velocityZ, float unknown1, float unknown2, float unknown3, float unknown4, float unknown5, float unknown6, float unknown7)
        {
            //SpawnAsteroidOnDeath(NewResourceTypeAsteroid_5_piece01, "Asteroid_4_piece01", 1, 0,  0,       0,            50,           -65,             0,             -30,               0,               0,              0,              0,              8,              0,              0,             -5,              2)
            //Unused
        }

        public static void ResourceAttackMode(LuaTable entity, LuaBase mode)
        {
            //Unused
        }

        public static void GetShipNum(LuaTable ship, string property, float parameter)
        {
            //Unused
        }

        public static void GetShipStr(LuaTable ship, string property, string parameter)
        {
            //Unused
        }

        public static void SetSupplyValue(LuaTable ship, string family, float parameter)
        {
            //Unused
        }

        public static void StartShipWeaponConfig(LuaTable ship, string weapon, string joint, string animation)
        {
            //Unused
        }

        public static void SetEngineBurn(LuaTable ship, int sparkCount, float opacityLow, float opacityHigh, float sparkSize, float speedSparkSize, float flareMin, float flarePos, float flareSize)
        {
            //Unused
        }

        public static void SetEngineGlow(LuaTable ship, float unknown2, float unknown3, float unknown4, float unknown5, float unknown6, float unknown7, float unknown8, LuaTable color)
        {
            //Unused
        }

        public static void SetEngineTrail(LuaTable entity, int index, float lingerTime, string textureName, float bulgeFrequency, float textureScrollFactor, float textureScaleFactor, float diameterFactor)
        {
            //Unused
        }

        public static void SetTargetBox(LuaTable ship, float posX, float posY, float posZ, float rotX, float rotY, float rotZ, float size)
        {
            //Unused
        }

        public static void LoadModel(LuaTable entity, int enabled)
        {
            //Unused
        }

        public static void LoadSharedModel(LuaTable entity, string objectToShareWith)
        {
            //Unused
        }

        public static void AddShield(LuaTable ship, string type, float max, float rechargeTime)
        {
            //Unused
        }

        public static void AddShipAbility(LuaTable ship, string ability, int active, string target, float radius)
        {
            //Unused
        }

        public static void addAbility(LuaTable entity, string ability, float unknown1, float unknown2)
        {
            //Unused
        }

        public static void addAbility(LuaTable entity, string ability)
        {
            //Unused
        }

        public static void addAbility(LuaTable entity, string ability, float unknown1, float unknown2, float unknown3, float unknown4, float unknown5, float unknown6)
        {
            //Unused
        }
        #endregion
    }
}
