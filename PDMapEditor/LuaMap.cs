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
    public class LuaMap
    {
        static CultureInfo InvariantCulture = CultureInfo.InvariantCulture;

        public void LoadMap(string path)
        {
            Lua lua = new Lua();
            lua.RegisterFunction("addAsteroid", this, GetType().GetMethod("AddAsteroid"));
            lua.RegisterFunction("addPoint", this, GetType().GetMethod("AddPoint"));
            lua.RegisterFunction("addPebble", this, GetType().GetMethod("AddPebble"));
            lua.RegisterFunction("addDustCloud", this, GetType().GetMethod("AddDustCloud"));
            lua.RegisterFunction("addDustCloudWithResources", this, GetType().GetMethod("AddDustCloudWithResources"));
            lua.RegisterFunction("setWorldBoundsInner", this, GetType().GetMethod("SetWorldBoundsInner"));
            lua.RegisterFunction("setWorldBoundsOuter", this, GetType().GetMethod("SetWorldBoundsOuter"));
            lua.RegisterFunction("addSquadron", this, GetType().GetMethod("AddSquadron"));
            lua.RegisterFunction("addCloud", this, GetType().GetMethod("AddCloud"));
            lua.RegisterFunction("addNebula", this, GetType().GetMethod("AddNebula"));
            lua.RegisterFunction("addNebulaWithResources", this, GetType().GetMethod("AddNebulaWithResources"));
            lua.RegisterFunction("addSphere", this, GetType().GetMethod("AddSphere"));
            lua.RegisterFunction("addCamera", this, GetType().GetMethod("AddCamera"));
            lua.RegisterFunction("addSalvage", this, GetType().GetMethod("AddSalvage"));
            lua.RegisterFunction("createSOBGroup", this, GetType().GetMethod("CreateSOBGroup"));
            lua.RegisterFunction("addToSOBGroup", this, GetType().GetMethod("AddToSOBGroup"));
            lua.RegisterFunction("addPath", this, GetType().GetMethod("AddPath"));
            lua.RegisterFunction("setObjectAttributes", this, GetType().GetMethod("SetObjectAttributes"));
            lua.RegisterFunction("addReactiveFleetSlot", this, GetType().GetMethod("AddReactiveFleetSlot"));
            lua.RegisterFunction("addReactiveFleetResourceSlot", this, GetType().GetMethod("AddReactiveFleetResourceSlot"));
            lua.RegisterFunction("addAxisAlignBox", this, GetType().GetMethod("AddAxisAlignBox"));
            lua.RegisterFunction("addReactiveFleetResourceSlotDustCloud", this, GetType().GetMethod("AddReactiveFleetResourceSlotDustCloud"));
            lua.RegisterFunction("addTendrils", this, GetType().GetMethod("AddTendrils"));
            lua.RegisterFunction("addSensorsPlane", this, GetType().GetMethod("AddSensorsPlane"));

            lua.RegisterFunction("fogSetActive", this, GetType().GetMethod("FogSetActive"));
            lua.RegisterFunction("fogSetStart", this, GetType().GetMethod("FogSetStart"));
            lua.RegisterFunction("fogSetEnd", this, GetType().GetMethod("FogSetEnd"));
            lua.RegisterFunction("fogSetColour", this, GetType().GetMethod("FogSetColour"));
            lua.RegisterFunction("fogSetType", this, GetType().GetMethod("FogSetType"));
            lua.RegisterFunction("fogSetDensity", this, GetType().GetMethod("FogSetDensity"));
            lua.RegisterFunction("fogAddInterpolator", this, GetType().GetMethod("FogAddInterpolator"));

            lua.RegisterFunction("loadBackground", this, GetType().GetMethod("LoadBackground"));
            lua.RegisterFunction("setSensorsManagerCameraDistances", this, GetType().GetMethod("SetSensorsManagerCameraDistances"));
            lua.RegisterFunction("setDefaultMusic", this, GetType().GetMethod("SetDefaultMusic"));
            lua.RegisterFunction("setBattleMusic", this, GetType().GetMethod("SetBattleMusic"));
            lua.RegisterFunction("setGlareIntensity", this, GetType().GetMethod("SetGlareIntensity"));
            lua.RegisterFunction("setLevelShadowColour", this, GetType().GetMethod("SetLevelShadowColour"));
            lua.RegisterFunction("setDustCloudAmbient", this, GetType().GetMethod("SetDustCloudAmbient"));
            lua.RegisterFunction("setNebulaAmbient", this, GetType().GetMethod("SetNebulaAmbient"));

            lua.RegisterFunction("min", this, GetType().GetMethod("Min"));
            lua.RegisterFunction("max", this, GetType().GetMethod("Max"));

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
            catch { new Problem(ProblemTypes.ERROR, "Failed to parse level description."); }
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

        public void SaveMap(string path)
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
        public void AddAsteroid(string type, LuaTable position, float multiplier, float rotX, float rotY, float rotZ, float rotSpeed)
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

        public void AddPoint(string name, LuaTable position, LuaTable rotation)
        {
            //Log.WriteLine("Adding starting point \"" + name + "\".");

            Vector3 pos = LuaTableToVector3(position);
            Vector3 rot = LuaTableToVector3(rotation);

            new Point(name, pos, rot);
        }

        public void AddPebble(string type, LuaTable position, float rotX, float rotY, float rotZ)
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

        public void AddDustCloud(string name, string type, LuaTable position, LuaTable color, float unknown1, float size)
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

        public void AddDustCloudWithResources(string name, string type, LuaTable position, float resources, LuaTable color, float unknown, float size, float unknown2)
        {
            //Console.WriteLine("Adding dust cloud \"" + name + "\".");
        }

        public void SetWorldBoundsInner(LuaTable center, LuaTable dimensions)
        {
            Vector3 dim = LuaTableToVector3(dimensions);
            Map.MapDimensions = dim;
        }

        public void SetWorldBoundsOuter(LuaTable center, LuaTable dimensions)
        {
            //STUB
        }

        public void AddSquadron(string name, string type, LuaTable position, float unknown, LuaTable rotation, float unknown2, float unknown3)
        {
            //STUB
        }

        public void AddCloud(string name, string type, LuaTable position, LuaTable color, float unknown, float size)
        {
            //STUB
        }

        public void AddNebula(string name, string type, LuaTable position, LuaTable color, float size, float size2)
        {
            //STUB
        }

        public void AddNebulaWithResources(string name, string type, LuaTable position, float resources, LuaTable color, float unknown, float size, float unknown2)
        {
            //STUB
        }

        public void AddSphere(string name, LuaTable position, float size)
        {
            //STUB
        }

        public void AddCamera(string name, LuaTable position, LuaTable focusPosition)
        {
            //STUB
        }

        public void AddSalvage(string type, LuaTable position, float resources, float rotX, float rotY, float rotZ, float unknown)
        {
            //STUB
        }

        public void CreateSOBGroup(string name)
        {
            //STUB
        }

        public void AddToSOBGroup(string type, string sobGroup)
        {
            //STUB
        }

        public void AddPath(string name, params string[] points)
        {
            //STUB
        }

        public void SetObjectAttributes(float unknown, float unknown2)
        {
            //STUB
        }

        public void AddReactiveFleetSlot(string sobGroup, int player, float unknown, LuaTable position, float rotX, float rotY, float rotZ, string shipType)
        {
            //STUB
        }

        public void AddReactiveFleetResourceSlotDustCloud(string name, string type, LuaTable position, float red, float green, float blue)
        {
            //STUB
        }

        public void AddReactiveFleetResourceSlot(string type, LuaTable position, float rotX, float rotY, float rotZ)
        {
            //STUB
        }

        public void AddAxisAlignBox(string name, LuaTable position, float size)
        {
            //STUB
        }

        public void AddTendrils()
        {
            //STUB
        }

        public void AddSensorsPlane(float distance, float factor, LuaTable color)
        {
            //STUB
        }

        public void FogSetActive(int active)
        {
            if (active == 1)
                Map.FogActive = true;
            else
                Map.FogActive = false;
        }

        public void FogSetStart(float start)
        {
            Map.FogStart = start;
        }

        public void FogSetEnd(float end)
        {
            Map.FogEnd = end;
        }

        public void FogSetColour(float red, float green, float blue, float alpha)
        {
            Map.FogColor = new Vector4(red, green, blue, alpha);
        }
        public void FogSetType(string type)
        {
            Map.FogType = type;
        }
        public void FogSetDensity(float density)
        {
            Map.FogDensity = density;
        }

        public void FogAddInterpolator(string buffer, float duration, float target)
        {
            //STUB
        }

        public void LoadBackground(string name)
        {
            Background background = Background.GetBackgroundFromName(name);
            if (background == null)
            {
                new Problem(ProblemTypes.WARNING, "Background \"" + name + "\" not found.");
                return;
            }

            Map.Background = background;
        }

        public void SetSensorsManagerCameraDistances(float min, float max)
        {
            Map.SensorsManagerCameraMin = min;
            Map.SensorsManagerCameraMax = max;
        }

        public void SetDefaultMusic(string path)
        {
            string music = path.ToLower();
            music = music.Replace("data:", "");
            music = music.Replace("data_", "");
            Map.MusicDefault = music;
        }

        public void SetBattleMusic(string path)
        {
            string music = path.ToLower();
            music = music.Replace("data:", "");
            music = music.Replace("data_", "");
            Map.MusicBattle = music;
        }

        public void SetGlareIntensity(float intensity)
        {
            Map.GlareIntensity = intensity;
        }

        public void SetLevelShadowColour(float red, float green, float blue, float alpha)
        {
            Map.ShadowColor = new Vector4(red, green, blue, alpha);
        }

        public void SetDustCloudAmbient(LuaTable color)
        {
            //STUB
        }

        public void SetNebulaAmbient(LuaTable color)
        {
            //STUB
        }

        //Misceallenous
        public float Min(float valueA, float valueB)
        {
            return Math.Min(valueA, valueB);
        }

        public float Max(float valueA, float valueB)
        {
            return Math.Max(valueA, valueB);
        }
        #endregion
    }
}
