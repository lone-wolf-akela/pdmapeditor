﻿using NLua;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

namespace PDMapEditor
{
    public static class HWData
    {
        public static List<string> DataPaths = new List<string>();
        static readonly CultureInfo InvariantCulture = CultureInfo.InvariantCulture;

        static Lua lua;
        static LuaTable pebbleConfig;
        static LuaTable asteroidConfig;
        static LuaTable dustCloudConfig;
        static LuaTable cloudConfig;
        static LuaTable salvageConfig;
        static LuaTable shipConfig;

        public static void ParseDataPaths()
        {
            //Check if there are any data paths
            if (DataPaths.Count <= 0)
            {
                MessageBox.Show("You did not specify any data paths yet!\nThis is needed for parsing of pebble types etc.\nDefine them in the settings window.", "No data paths specified", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                        ParsePebbleType(file);
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
                        ParseAsteroidType(file);
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
                        ParseDustCloudType(file);
                    }
                }

                //Parse cloud types
                string cloudTypesPath = Path.Combine(dataPath, "cloud/");

                //Check if types folder exists
                if (Directory.Exists(cloudTypesPath))
                {
                    string[] files = Directory.GetFiles(cloudTypesPath, "*.cloud", SearchOption.AllDirectories);
                    foreach (string file in files)
                    {
                        ParseCloudType(file);
                    }
                }

                //Parse nebula types
                string nebulaTypesPath = Path.Combine(dataPath, "resource/nebula");

                //Check if nebula types folder exists
                if (Directory.Exists(nebulaTypesPath))
                {
                    string[] files = Directory.GetFiles(nebulaTypesPath, "*.resource", SearchOption.AllDirectories);
                    foreach (string file in files)
                    {
                        ParseNebulaType(file);
                    }
                }

                //Parse salvage types
                string salvageTypesPath = Path.Combine(dataPath, "resource/salvage");

                //Check if salvage types folder exists
                if (Directory.Exists(salvageTypesPath))
                {
                    string[] files = Directory.GetFiles(salvageTypesPath, "*.resource", SearchOption.AllDirectories);
                    foreach (string file in files)
                    {
                        ParseSalvageType(file);
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
                        string path = Path.Combine(directory, name);

                        if (Background.GetBackgroundFromName(name) == null) //Don't create duplicate backgrounds (because of multiple data paths)
                            new Background(name, path);
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
                        ParseShipType(file);
                    }
                }
            }

            //Create default types if there are none in the data paths
            if(PebbleType.PebbleTypes.Count <= 0)
                new PebbleType("null", 2, Vector4.One);

            if (AsteroidType.AsteroidTypes.Count <= 0)
                new AsteroidType("null", Vector4.One, 0);

            if (DustCloudType.DustCloudTypes.Count <= 0)
                new DustCloudType("null", Vector4.One);

            if (NebulaType.NebulaTypes.Count <= 0)
                new NebulaType("null");

            if (SalvageType.SalvageTypes.Count <= 0)
                new SalvageType("null", 2, 0, Vector4.One);

            if (ShipType.ShipTypes.Count <= 0)
                new ShipType("null");
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

        private static void ParseCloudType(string path)
        {
            lua = new Lua();
            lua.RegisterFunction("StartCloudConfig", null, typeof(HWData).GetMethod("StartCloudConfig"));
            /*lua.RegisterFunction("setCollisionDamageToModifier", this, GetType().GetMethod("SetCollisionDamageToModifier"));
            lua.RegisterFunction("setCollisionDamageFromModifier", this, GetType().GetMethod("SetCollisionDamageFromModifier"));
            lua.RegisterFunction("SpawnAsteroidOnDeath", this, GetType().GetMethod("SpawnAsteroidOnDeath"));
            lua.RegisterFunction("resourceAttackMode", this, GetType().GetMethod("ResourceAttackMode"));*/
            lua.DoFile(path);

            string name = Path.GetFileNameWithoutExtension(path).ToLower();

            Vector4 pixelColor = Vector4.Zero;
            foreach (KeyValuePair<object, object> de in cloudConfig)
            {
                switch (de.Key.ToString())
                {
                    case "pixelColour":
                        pixelColor = LuaMap.LuaTableToVector4((LuaTable)de.Value);
                        break;
                }
            }

            CloudType existingType = null;
            foreach (CloudType type in CloudType.CloudTypes)
            {
                if (type.Name == name)
                {
                    existingType = type;
                    break;
                }
            }

            //Check if a type with that name already exists (because of multiple data paths)
            if (existingType == null)
                new CloudType(name, pixelColor);
            else
            {
                //Overwrite existing style
                existingType.Name = name;
                existingType.PixelColor = pixelColor;
            }
        }

        private static void ParseNebulaType(string path)
        {
            string name = Path.GetFileNameWithoutExtension(path).ToLower();

            NebulaType existingType = null;
            foreach (NebulaType type in NebulaType.NebulaTypes)
            {
                if (type.Name == name)
                {
                    existingType = type;
                    break;
                }
            }

            //Check if a type with that name already exists (because of multiple data paths)
            if (existingType == null)
                new NebulaType(name);
            else
            {
                //Overwrite existing style
                existingType.Name = name;
            }
        }

        private static void ParseSalvageType(string path)
        {
            lua = new Lua();
            lua.RegisterFunction("StartSalvageConfig", null, typeof(HWData).GetMethod("StartSalvageConfig"));
            lua.DoFile(path);

            string name = Path.GetFileNameWithoutExtension(path).ToLower();

            float pixelSize = 1;
            float resourceValue = 0;
            Vector4 pixelColor = Vector4.Zero;
            foreach (KeyValuePair<object, object> de in salvageConfig)
            {
                switch (de.Key.ToString())
                {
                    case "pixelSize":
                        string text = de.Value.ToString();
                        text = text.Replace(",", ".");
                        pixelSize = float.Parse(text, InvariantCulture);
                        break;
                    case "resourceValue":
                        text = de.Value.ToString();
                        text = text.Replace(",", ".");
                        resourceValue = float.Parse(text, InvariantCulture);
                        break;
                    case "pixelColour":
                        pixelColor = LuaMap.LuaTableToVector4((LuaTable)de.Value);
                        break;
                }
            }

            SalvageType existingType = null;
            foreach (SalvageType type in SalvageType.SalvageTypes)
            {
                if (type.Name == name)
                {
                    existingType = type;
                    break;
                }
            }

            //Check if a type with that name already exists (because of multiple data paths)
            if (existingType == null)
                new SalvageType(name, pixelSize, resourceValue, pixelColor);
            else
            {
                //Overwrite existing style
                existingType.Name = name;
                existingType.PixelSize = pixelSize;
                existingType.ResourceValue = resourceValue;
                existingType.PixelColor = pixelColor;
            }
        }

        private static void ParseShipType(string path)
        {
            string name = Path.GetFileNameWithoutExtension(path).ToLower();

            ShipType existingType = ShipType.GetTypeFromName(name);

            //Check if a type with that name already exists (because of multiple data paths)
            if (existingType == null)
                new ShipType(name);
            else
            {
                //Overwrite existing style
                existingType.Name = name;
            }
        }

        /*private static void ParseShipType(string path)
        {
            lua = new Lua();
            string file = File.ReadAllText(path);

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
            lua.RegisterFunction("setConcurrentBuildLimit", null, type.GetMethod("SetConcurrentBuildLimit"));
            lua.RegisterFunction("setCollisionDamageToModifier", null, type.GetMethod("SetCollisionDamageToModifier"));
            lua.RegisterFunction("setCollisionDamageFromModifier", null, type.GetMethod("SetCollisionDamageFromModifier"));
            lua.RegisterFunction("setSpecialDieTime", null, type.GetMethod("SetSpecialDieTime"));
            lua.RegisterFunction("addMagneticField", null, type.GetMethod("AddMagneticField"));

            file = DisableLuaFunctionInString(file, "addAbility");
            file = DisableLuaFunctionInString(file, "addCustomCode");
            file = DisableLuaFunctionInString(file, "SpawnSalvageOnDeath");
            file = DisableLuaFunctionInString(file, "SpawnDustCloudOnDeath");
            file = DisableLuaFunctionInString(file, "loadShipPatchList");
            file = DisableLuaFunctionInString(file, "loadLatchPointList");
            file = DisableLuaFunctionInString(file, "AddShipMultiplier");
            file = DisableLuaFunctionInString(file, "setTacticsMults");
            file = DisableLuaFunctionInString(file, "setSpeedvsAccuracyApplied");
            file = DisableLuaFunctionInString(file, "StartShipHardPointConfig");

            lua.DoString(file);

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
        }*/

        private static string DisableLuaFunctionInString(string text, string function)
        {
            string newText = text;

            int index = 0;
            bool inFunction = false;
            bool inString = false;

            while(index <= newText.Length)
            {
                if (index < newText.Length)
                {
                    if (newText[index] == '\"' || newText[index] == '\'') //String start/end found
                    {
                        inString = !inString;
                    }
                }

                if (!inFunction)
                {
                    if (newText.Length >= index + function.Length)
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
        public static LuaTable StartCloudConfig()
        {
            cloudConfig = (LuaTable)lua.DoString("return {}")[0];
            return cloudConfig;
        }
        public static LuaTable StartSalvageConfig()
        {
            salvageConfig = (LuaTable)lua.DoString("return {}")[0];
            return salvageConfig;
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

        public static float GetShipNum(LuaTable ship, string property, float parameter)
        {
            //Unused
            return 0;
        }

        public static string GetShipStr(LuaTable ship, string property, string parameter)
        {
            //Unused
            return "";
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

        public static void SetConcurrentBuildLimit(LuaTable ship, int min, int max)
        {
            //Unused
        }

        public static void SetSpecialDieTime(LuaTable ship, string killer, float time)
        {
            //Unused
        }

        public static void AddMagneticField(LuaTable entity, string type, float unknown1, float unknown2, string mesh, float unknown3, string mesh2, float unknown4, string hit, string effect)
        {
            //Unused
        }
        #endregion
    }
}
