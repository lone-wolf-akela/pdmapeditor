using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLua;
using OpenTK;
using System.Globalization;
using System.IO;

namespace PDMapEditor
{
    public static class LuaMap
    {
        static CultureInfo InvariantCulture = CultureInfo.InvariantCulture;

        public static void LoadMap(string path)
        {
            Lua lua = new Lua();
            Type type = typeof(LuaMap);

            lua.RegisterFunction("addAsteroid", null, type.GetMethod("AddAsteroid"));
            lua.RegisterFunction("addPoint", null, type.GetMethod("AddPoint"));
            lua.RegisterFunction("addPebble", null, type.GetMethod("AddPebble"));
            lua.RegisterFunction("addDustCloud", null, type.GetMethod("AddDustCloud"));
            lua.RegisterFunction("addDustCloudWithResources", null, type.GetMethod("AddDustCloudWithResources"));
            lua.RegisterFunction("setWorldBoundsInner", null, type.GetMethod("SetWorldBoundsInner"));
            lua.RegisterFunction("setWorldBoundsOuter", null, type.GetMethod("SetWorldBoundsOuter"));
            lua.RegisterFunction("addSquadron", null, type.GetMethod("AddSquadron"));
            lua.RegisterFunction("addCloud", null, type.GetMethod("AddCloud"));
            lua.RegisterFunction("addNebula", null, type.GetMethod("AddNebula"));
            lua.RegisterFunction("addNebulaWithResources", null, type.GetMethod("AddNebulaWithResources"));
            lua.RegisterFunction("addSphere", null, type.GetMethod("AddSphere"));
            lua.RegisterFunction("addCamera", null, type.GetMethod("AddCamera"));
            lua.RegisterFunction("addSalvage", null, type.GetMethod("AddSalvage"));
            lua.RegisterFunction("createSOBGroup", null, type.GetMethod("CreateSOBGroup"));
            lua.RegisterFunction("addToSOBGroup", null, type.GetMethod("AddToSOBGroup"));
            lua.RegisterFunction("addPath", null, type.GetMethod("AddPath"));
            lua.RegisterFunction("setObjectAttributes", null, type.GetMethod("SetObjectAttributes"));
            lua.RegisterFunction("addReactiveFleetSlot", null, type.GetMethod("AddReactiveFleetSlot"));
            lua.RegisterFunction("addReactiveFleetResourceSlot", null, type.GetMethod("AddReactiveFleetResourceSlot"));
            lua.RegisterFunction("addAxisAlignBox", null, type.GetMethod("AddAxisAlignBox"));
            lua.RegisterFunction("addReactiveFleetResourceSlotDustCloud", null, type.GetMethod("AddReactiveFleetResourceSlotDustCloud"));
            //lua.RegisterFunction("addTendrils", null, type.GetMethod("AddTendrils"));
            lua.RegisterFunction("addSensorsPlane", null, type.GetMethod("AddSensorsPlane"));

            lua.RegisterFunction("fogSetActive", null, type.GetMethod("FogSetActive"));
            lua.RegisterFunction("fogSetStart", null, type.GetMethod("FogSetStart"));
            lua.RegisterFunction("fogSetEnd", null, type.GetMethod("FogSetEnd"));
            lua.RegisterFunction("fogSetColour", null, type.GetMethod("FogSetColour"));
            lua.RegisterFunction("fogSetType", null, type.GetMethod("FogSetType"));
            lua.RegisterFunction("fogSetDensity", null, type.GetMethod("FogSetDensity"));
            lua.RegisterFunction("fogAddInterpolator", null, type.GetMethod("FogAddInterpolator"));

            lua.RegisterFunction("loadBackground", null, type.GetMethod("LoadBackground"));
            lua.RegisterFunction("setSensorsManagerCameraDistances", null, type.GetMethod("SetSensorsManagerCameraDistances"));
            lua.RegisterFunction("setDefaultMusic", null, type.GetMethod("SetDefaultMusic"));
            lua.RegisterFunction("setBattleMusic", null, type.GetMethod("SetBattleMusic"));
            lua.RegisterFunction("setGlareIntensity", null, type.GetMethod("SetGlareIntensity"));
            lua.RegisterFunction("setLevelShadowColour", null, type.GetMethod("SetLevelShadowColour"));
            lua.RegisterFunction("setDustCloudAmbient", null, type.GetMethod("SetDustCloudAmbient"));
            lua.RegisterFunction("setNebulaAmbient", null, type.GetMethod("SetNebulaAmbient"));

            lua.RegisterFunction("min", null, type.GetMethod("Min"));
            lua.RegisterFunction("max", null, type.GetMethod("Max"));

            try
            { lua.DoFile(path); }
            catch (NLua.Exceptions.LuaScriptException e) { new Problem(ProblemTypes.ERROR, e.Message); }

            try { lua.DoString("DetermChunk()"); }
            catch (NLua.Exceptions.LuaScriptException e) { new Problem(ProblemTypes.ERROR, e.Message); }

            try { lua.DoString("NonDetermChunk()"); }
            catch (NLua.Exceptions.LuaScriptException e) { new Problem(ProblemTypes.ERROR, e.Message); }

            string description = "";
            try
            { description = lua.GetString("levelDesc"); }
            catch { new Problem(ProblemTypes.WARNING, "Failed to parse level description."); }
            Map.Description = description;

            int maxPlayers = 2;
            try { maxPlayers = (int)lua.GetNumber("maxPlayers"); }
            catch { new Problem(ProblemTypes.ERROR, "Failed to parse maximum player count."); }

            if (maxPlayers > 8 || maxPlayers < 1)
            {
                new Problem(ProblemTypes.ERROR, "The maximum player count is not between 1 and 8. Resetting to 2.");
                maxPlayers = 2;
            }

            Map.MaxPlayers = maxPlayers;
        }

        public static void SaveMap(string path)
        {
            string name = Path.GetFileName(path);

            StringBuilder sb = new StringBuilder();

            //Top comment
            sb.AppendLine("-- PayDay's Homeworld Remastered Map Editor generated file");
            sb.AppendLine("-- ============================================================");
            sb.AppendLine("-- Name: " + name + "");
            sb.AppendLine("-- Purpose: Automatically generated level file by the map editor.");
            sb.AppendLine("-- Created " + DateTime.Now.ToLongDateString() + " at " + DateTime.Now.ToLongTimeString() + ".");
            sb.AppendLine("-- ============================================================");
            sb.AppendLine("");

            //DetermChunk()
            sb.AppendLine("function DetermChunk()");

            if (Point.Points.Count > 0)
            {
                sb.AppendLine("\t-- Points");
                foreach (Point point in Point.Points)
                {
                    sb.AppendLine("\taddPoint(\"" + point.Name + "\", " + WriteFloatLuaTable(point.Position.X, point.Position.Y, point.Position.Z) + ", " + WriteFloatLuaTable(point.Rotation.X, point.Rotation.Y, point.Rotation.Z) + ")");
                }
                sb.AppendLine("");
            }

            if (Squadron.Squadrons.Count > 0)
            {
                sb.AppendLine("\t-- Squadrons");
                foreach (Squadron squadron in Squadron.Squadrons)
                {
                    string inHyperspace = "0";
                    if (squadron.InHyperspace)
                        inHyperspace = "1";

                    sb.AppendLine("\taddSquadron(\"" + squadron.Name + "\", \"" + squadron.Type.Name + "\", " + WriteFloatLuaTable(squadron.Position.X, squadron.Position.Y, squadron.Position.Z) + ", " + squadron.Player.ToString() + ", " + WriteFloatLuaTable(squadron.Rotation.X, squadron.Rotation.Y, squadron.Rotation.Z) + ", " + squadron.SquadronSize.ToString() + ", " + inHyperspace.ToString() + ")");
                }
                sb.AppendLine("");
            }

            if (Asteroid.Asteroids.Count > 0)
            {
                sb.AppendLine("\t-- Asteroids");
                foreach (Asteroid asteroid in Asteroid.Asteroids)
                {
                    sb.AppendLine("\taddAsteroid(\"" + asteroid.Type.Name + "\", " + WriteFloatLuaTable(asteroid.Position.X, asteroid.Position.Y, asteroid.Position.Z) + ", " + asteroid.Multiplier.ToString(InvariantCulture) + ", " + asteroid.Rotation.X.ToString(InvariantCulture) + ", " + asteroid.Rotation.Y.ToString(InvariantCulture) + ", " + asteroid.Rotation.Z.ToString(InvariantCulture) + ", " + asteroid.RotSpeed.ToString(InvariantCulture) + ")");
                }
                sb.AppendLine("");
            }

            if (DustCloud.DustClouds.Count > 0)
            {
                sb.AppendLine("\t-- Dust clouds");
                foreach (DustCloud dustCloud in DustCloud.DustClouds)
                {
                    sb.AppendLine("\taddDustCloud(\"" + dustCloud.Name + "\", \"" + dustCloud.Type.Name + "\", " + WriteFloatLuaTable(dustCloud.Position.X, dustCloud.Position.Y, dustCloud.Position.Z) + ", " + WriteFloatLuaTable(dustCloud.Color.X, dustCloud.Color.Y, dustCloud.Color.Z, dustCloud.Color.W) + ", " + dustCloud.Unknown1.ToString(InvariantCulture) + ", " + dustCloud.Size.ToString(InvariantCulture) + ")");
                }
                sb.AppendLine("");
            }

            if (Nebula.Nebulas.Count > 0)
            {
                sb.AppendLine("\t-- Nebulas");
                foreach (Nebula nebula in Nebula.Nebulas)
                {
                    sb.AppendLine("\taddNebula(\"" + nebula.Name + "\", \"" + nebula.Type.Name + "\", " + WriteFloatLuaTable(nebula.Position.X, nebula.Position.Y, nebula.Position.Z) + ", " + WriteFloatLuaTable(nebula.Color.X, nebula.Color.Y, nebula.Color.Z, nebula.Color.W) + ", " + nebula.Unknown.ToString(InvariantCulture) + ", " + nebula.Size.ToString(InvariantCulture) + ")");
                }
                sb.AppendLine("");
            }

            sb.AppendLine("\t-- Settings");
            sb.AppendLine("\tsetWorldBoundsInner(" + WriteFloatLuaTable(0, 0, 0) + ", " + WriteFloatLuaTable(Map.MapDimensions.X, Map.MapDimensions.Y, Map.MapDimensions.Z) + ")");
            sb.AppendLine("end");

            sb.AppendLine("");

            //NonDetermChunk()
            sb.AppendLine("function NonDetermChunk()");
            if (Pebble.Pebbles.Count > 0)
            {
                sb.AppendLine("\t-- Pebbles");
                foreach (Pebble pebble in Pebble.Pebbles)
                {
                    sb.AppendLine("\taddPebble(\"" + pebble.Type.Name + "\", " + WriteFloatLuaTable(pebble.Position.X, pebble.Position.Y, pebble.Position.Z) + ", " + pebble.Rotation.X.ToString(InvariantCulture) + ", " + pebble.Rotation.Y.ToString(InvariantCulture) + ", " + pebble.Rotation.Z.ToString(InvariantCulture) + ")");
                }
                sb.AppendLine("");
            }
            sb.AppendLine("\t-- Fog");
            string fogActive = "0";
            if (Map.FogActive)
                fogActive = "1";
            sb.AppendLine("\tfogSetActive(" + fogActive + ")");
            if(Map.FogActive)
            {
                sb.AppendLine("\tfogSetStart(" + Map.FogStart.ToString(InvariantCulture) + ")");
                sb.AppendLine("\tfogSetEnd(" + Map.FogEnd.ToString(InvariantCulture) + ")");
                sb.AppendLine("\tfogSetColour(" + Map.FogColor.X.ToString(InvariantCulture) + ", " + Map.FogColor.Y.ToString(InvariantCulture) + ", " + Map.FogColor.Z.ToString(InvariantCulture) + ", " + Map.FogColor.W.ToString(InvariantCulture) + ")");
                sb.AppendLine("\tfogSetType(\"" + Map.FogType + "\")");
                sb.AppendLine("\tfogSetDensity(" + Map.FogDensity.ToString(InvariantCulture) + ")");
            }
            sb.AppendLine("");

            //Settings
            sb.AppendLine("\t-- Settings");
            if(Map.Background != null)
                sb.AppendLine("\tloadBackground(\"" + Map.Background.Name + "\")");
            else
                sb.AppendLine("\tloadBackground(\"\")");
            sb.AppendLine("\tsetGlareIntensity(" + Map.GlareIntensity.ToString(InvariantCulture) + ")");
            sb.AppendLine("\tsetLevelShadowColour(" + Map.ShadowColor.X.ToString(InvariantCulture) + ", " + Map.ShadowColor.Y.ToString(InvariantCulture) + ", " + Map.ShadowColor.Z.ToString(InvariantCulture) + ", " + Map.ShadowColor.W.ToString(InvariantCulture) + ")");
            sb.AppendLine("\tsetSensorsManagerCameraDistances(" + Map.SensorsManagerCameraMin + ", " + Map.SensorsManagerCameraMax + ")");

            sb.AppendLine("");

            //Music
            sb.AppendLine("\t-- Music");
            sb.AppendLine("\tsetDefaultMusic(\"data:" + Map.MusicDefault + "\")");
            sb.AppendLine("\tsetBattleMusic(\"data:" + Map.MusicBattle + "\")");

            sb.AppendLine("end");

            sb.AppendLine("");
            sb.AppendLine("levelDesc = \"" + Map.Description + "\"; -- Level description displayed in the lobby.");
            sb.AppendLine("maxPlayers = " + Map.MaxPlayers + "; -- Maximum number of players allowed on this level.");
            sb.AppendLine("");

            sb.AppendLine("player = {}; -- Info on each possible player on this level.");
            for(int i = 0; i < Map.MaxPlayers; i ++)
            {
                sb.AppendLine("player[" + i + "] =");
                sb.AppendLine("{");
                sb.AppendLine("\tid = " + i + ",");
                sb.AppendLine("\tname = \"\",");
                sb.AppendLine("\tresources = 0,");
                sb.AppendLine("\traceName = \"Hiigaran\",");
                sb.AppendLine("\tstartPos = 1");
                sb.AppendLine("}");
                sb.AppendLine("");
            }

            File.WriteAllText(path, sb.ToString());
        }

        public static Vector3 LuaTableToVector3(LuaTable table)
        {
            Vector3 vector = Vector3.Zero;

            foreach (KeyValuePair<object, object> de in table)
            {
                string text = de.Value.ToString();
                text = text.Replace(",", ".");
                float value = float.Parse(text, InvariantCulture);

                switch (de.Key.ToString())
                {
                    case "1":
                        vector.X = value;
                        break;
                    case "2":
                        vector.Y = value;
                        break;
                    case "3":
                        vector.Z = value;
                        break;
                }
            }

            return vector;
        }

        public static Vector4 LuaTableToVector4(LuaTable table)
        {
            Vector4 vector = Vector4.Zero;

            foreach (KeyValuePair<object, object> de in table)
            {
                string text = de.Value.ToString();
                text = text.Replace(",", ".");
                float value = float.Parse(text, InvariantCulture);

                switch (de.Key.ToString())
                {
                    case "1":
                        vector.X = value;
                        break;
                    case "2":
                        vector.Y = value;
                        break;
                    case "3":
                        vector.Z = value;
                        break;
                    case "4":
                        vector.W = value;
                        break;
                }
            }

            return vector;
        }

        private static string WriteFloatLuaTable(params float[] parameters)
        {
            string code = "{";
            for(int i = 0; i < parameters.Length; i ++)
            {
                code += parameters[i].ToString(InvariantCulture);
                if (i < parameters.Length - 1)
                    code += ", ";
            }
            code += "}";
            return code;
        }

        #region Lua functions
        public static void AddAsteroid(string type, LuaTable position, float multiplier, float rotX, float rotY, float rotZ, float rotSpeed)
        {
            //Log.WriteLine("Adding asteroid \"" + type + "\".");

            Vector3 pos = LuaTableToVector3(position);

            Vector3 rot = Vector3.Zero;
            rot.X = rotX;
            rot.Y = rotY;
            rot.Z = rotZ;

            string newType = type.ToLower();
            AsteroidType asteroidType = AsteroidType.GetTypeFromName(newType);

            if (asteroidType == null)
            {
                new Problem(ProblemTypes.WARNING, "Asteroid type \"" + newType + "\" not found. Skipping asteroid.");
                return;
            }

            new Asteroid(asteroidType, pos, rot, multiplier, rotSpeed);
        }

        public static void AddPoint(string name, LuaTable position, LuaTable rotation)
        {
            //Log.WriteLine("Adding starting point \"" + name + "\".");

            Vector3 pos = LuaTableToVector3(position);
            Vector3 rot = LuaTableToVector3(rotation);

            new Point(name, pos, rot);
        }

        public static void AddPebble(string type, LuaTable position, float rotX, float rotY, float rotZ)
        {
            //Log.WriteLine("Adding pebble of type \"" + type + "\".");

            Vector3 pos = LuaTableToVector3(position);
            Vector3 rot = new Vector3(rotX, rotY, rotZ);

            string newType = type.ToLower();
            PebbleType pebbleType = PebbleType.GetTypeFromName(newType);

            if (pebbleType == null)
            {
                new Problem(ProblemTypes.WARNING, "Pebble type \"" + newType + "\" not found. Skipping pebble.");
                return;
            }

            new Pebble(pebbleType, pos, rot);
        }

        public static void AddDustCloud(string name, string type, LuaTable position, LuaTable color, float unknown1, float size)
        {
            //Log.WriteLine("Adding dust cloud \"" + name + "\".");

            Vector3 pos = LuaTableToVector3(position);
            Vector4 col = LuaTableToVector4(color);

            string newType = type.ToLower();
            DustCloudType dustCloudType = DustCloudType.GetTypeFromName(newType);

            if (dustCloudType == null)
            {
                new Problem(ProblemTypes.WARNING, "Dust cloud type \"" + newType + "\" not found. Skipping dust cloud \"" + name + "\".");
                return;
            }

            new DustCloud(name, dustCloudType, pos, col, unknown1, size);
        }

        public static void AddDustCloudWithResources(string name, string type, LuaTable position, float resources, LuaTable color, float unknown, float size, float unknown2)
        {
            //Console.WriteLine("Adding dust cloud \"" + name + "\".");
        }

        public static void SetWorldBoundsInner(LuaTable center, LuaTable dimensions)
        {
            Vector3 dim = LuaTableToVector3(dimensions);
            Map.MapDimensions = dim;
        }

        public static void SetWorldBoundsOuter(LuaTable center, LuaTable dimensions)
        {
            //STUB
        }

        public static void AddSquadron(string name, string type, LuaTable position, int player, LuaTable rotation, int shipCount, int inHyperspace)
        {
            //Log.WriteLine("Adding squadron \"" + name + "\".");

            Vector3 pos = LuaTableToVector3(position);
            Vector3 rot = LuaTableToVector3(rotation);

            string newType = type.ToLower();
            ShipType shipType = ShipType.GetTypeFromName(newType);

            bool inHS = false;
            if (inHyperspace != 0)
                inHS = true;

            if (shipType == null)
            {
                new Problem(ProblemTypes.WARNING, "Ship type \"" + newType + "\" not found. Skipping squadron \"" + name + "\".");
                return;
            }

            new Squadron(name, pos, rot, shipType, player, shipCount, inHS);
        }

        public static void AddCloud(string name, string type, LuaTable position, LuaTable color, float unknown, float size)
        {
            //STUB
        }

        public static void AddNebula(string name, string type, LuaTable position, LuaTable color, float unknown, float size)
        {
            //Log.WriteLine("Adding nebula \"" + name + "\".");

            Vector3 pos = LuaTableToVector3(position);
            Vector4 col = LuaTableToVector4(color);

            string newType = type.ToLower();
            NebulaType nebulaType = NebulaType.GetTypeFromName(newType);

            if (nebulaType == null)
            {
                new Problem(ProblemTypes.WARNING, "Nebula type \"" + newType + "\" not found. Skipping nebula \"" + name + "\".");
                return;
            }

            new Nebula(name, nebulaType, pos, col, unknown, size);
        }

        public static void AddNebulaWithResources(string name, string type, LuaTable position, float resources, LuaTable color, float unknown, float size, float unknown2)
        {
            //STUB
        }

        public static void AddSphere(string name, LuaTable position, float size)
        {
            //STUB
        }

        public static void AddCamera(string name, LuaTable position, LuaTable focusPosition)
        {
            //STUB
        }

        public static void AddSalvage(string type, LuaTable position, float resources, float rotX, float rotY, float rotZ, float unknown)
        {
            //STUB
        }

        public static void CreateSOBGroup(string name)
        {
            //STUB
        }

        public static void AddToSOBGroup(string type, string sobGroup)
        {
            //STUB
        }

        public static void AddPath(string name, params string[] points)
        {
            //STUB
        }

        public static void SetObjectAttributes(float unknown, float unknown2)
        {
            //STUB
        }

        public static void AddReactiveFleetSlot(string sobGroup, int player, float unknown, LuaTable position, float rotX, float rotY, float rotZ, string shipType)
        {
            //STUB
        }

        public static void AddReactiveFleetResourceSlotDustCloud(string name, string type, LuaTable position, float red, float green, float blue)
        {
            //STUB
        }

        public static void AddReactiveFleetResourceSlot(string type, LuaTable position, float rotX, float rotY, float rotZ)
        {
            //STUB
        }

        public static void AddAxisAlignBox(string name, LuaTable position, float size)
        {
            //STUB
        }

        public static void AddTendrils()
        {
            //STUB
        }

        public static void AddSensorsPlane(float distance, float factor, LuaTable color)
        {
            //STUB
        }

        public static void FogSetActive(int active)
        {
            if (active == 1)
                Map.FogActive = true;
            else
                Map.FogActive = false;
        }

        public static void FogSetStart(float start)
        {
            Map.FogStart = start;
        }

        public static void FogSetEnd(float end)
        {
            Map.FogEnd = end;
        }

        public static void FogSetColour(float red, float green, float blue, float alpha)
        {
            Map.FogColor = new Vector4(red, green, blue, alpha);
        }
        public static void FogSetType(string type)
        {
            Map.FogType = type;
        }
        public static void FogSetDensity(float density)
        {
            Map.FogDensity = density;
        }

        public static void FogAddInterpolator(string buffer, float duration, float target)
        {
            //STUB
        }

        public static void LoadBackground(string name)
        {
            Background background = Background.GetBackgroundFromName(name);
            if (background == null)
            {
                new Problem(ProblemTypes.WARNING, "Background \"" + name + "\" not found.");
                return;
            }

            Map.Background = background;
        }

        public static void SetSensorsManagerCameraDistances(float min, float max)
        {
            Map.SensorsManagerCameraMin = min;
            Map.SensorsManagerCameraMax = max;
        }

        public static void SetDefaultMusic(string path)
        {
            string music = path.ToLower();
            music = music.Replace("data:", "");
            music = music.Replace("data_", "");
            Map.MusicDefault = music;
        }

        public static void SetBattleMusic(string path)
        {
            string music = path.ToLower();
            music = music.Replace("data:", "");
            music = music.Replace("data_", "");
            Map.MusicBattle = music;
        }

        public static void SetGlareIntensity(float intensity)
        {
            Map.GlareIntensity = intensity;
        }

        public static void SetLevelShadowColour(float red, float green, float blue, float alpha)
        {
            Map.ShadowColor = new Vector4(red, green, blue, alpha);
        }

        public static void SetDustCloudAmbient(LuaTable color)
        {
            //STUB
        }

        public static void SetNebulaAmbient(LuaTable color)
        {
            //STUB
        }

        //Miscellanous
        public static float Min(float valueA, float valueB)
        {
            return Math.Min(valueA, valueB);
        }

        public static float Max(float valueA, float valueB)
        {
            return Math.Max(valueA, valueB);
        }
        #endregion
    }
}
