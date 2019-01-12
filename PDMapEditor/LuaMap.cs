using System;
using System.Collections.Generic;
using System.Text;
using NLua;
using OpenTK;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;

namespace PDMapEditor
{
    public static class LuaMap
    {
        static readonly CultureInfo InvariantCulture = CultureInfo.InvariantCulture;

        private static Lua lua;
        private static List<string> output = new List<string>();

        public static void SetupInterpreter()
        {
            lua = new Lua();
            Type type = typeof(LuaMap);

            LoadMathLib();
            lua.RegisterFunction("print", null, type.GetMethod("Print"));

            lua.RegisterFunction("dofilepath", type.GetMethod("DoFilePath"));

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
            lua.RegisterFunction("setDustCloudColour", null, type.GetMethod("SetDustCloudColour"));
            lua.RegisterFunction("setNebulaAmbient", null, type.GetMethod("SetNebulaAmbient"));
        }

        public static void LoadMap(string path)
        {
            string code = File.ReadAllText(path);
            code = ConvertForLoopSyntax(code);

            try
            { lua.DoString(code); }
            catch (NLua.Exceptions.LuaScriptException e) { new Problem(ProblemTypes.ERROR, e.Message); }

            try { lua.DoString("NonDetermChunk()"); }
            catch (NLua.Exceptions.LuaScriptException e) { new Problem(ProblemTypes.ERROR, e.Message); }

            try { lua.DoString("DetermChunk()"); }
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

        private static string ConvertForLoopSyntax(string code)
        {
            string pattern = @"for(?<=for)(.*)(?=,),(?<=,)(.*)(?=in)in(?<=in)(.*)(?=do)do";
            int indexOffset = 0;
            MatchCollection matches = Regex.Matches(code, pattern);

            foreach(Match match in matches)
            {
                code = code.Insert(match.Groups[3].Index + match.Groups[3].Length + indexOffset, ") ");
                code = code.Insert(match.Groups[3].Index + indexOffset, " pairs(");

                indexOffset += 9;
            }

            return code;
        }

        private static void LoadMathLib()
        {
            Type math = typeof(Math);
            Type[] args = new Type[] { typeof(double) };
            lua.RegisterFunction("abs", math.GetMethod("Abs", args));
            lua.RegisterFunction("sin", math.GetMethod("Sin"));
            lua.RegisterFunction("cos", math.GetMethod("Cos"));
            lua.RegisterFunction("tan", math.GetMethod("Tan"));
            lua.RegisterFunction("asin", math.GetMethod("Asin"));
            lua.RegisterFunction("acos", math.GetMethod("Acos"));
            lua.RegisterFunction("acos", math.GetMethod("Acos"));
            lua.RegisterFunction("atan", math.GetMethod("Atan"));
            lua.RegisterFunction("atan2", math.GetMethod("Atan2"));
            lua.RegisterFunction("ceil", math.GetMethod("Ceiling", args));
            lua.RegisterFunction("floor", math.GetMethod("Floor", args));
            lua.RegisterFunction("mod", math.GetMethod("IEEERemainder"));
            lua.RegisterFunction("sqrt", math.GetMethod("Sqrt"));
            lua.RegisterFunction("pow", math.GetMethod("Pow"));
            lua.RegisterFunction("log", math.GetMethod("Log", args));
            lua.RegisterFunction("log10", math.GetMethod("Log10"));
            lua.RegisterFunction("exp", math.GetMethod("Exp"));
            lua.RegisterFunction("deg", typeof(LuaMap).GetMethod("ToDegrees"));
            lua.RegisterFunction("rad", typeof(LuaMap).GetMethod("ToRadians"));
            lua.RegisterFunction("min", math.GetMethod("Min", new Type[] { typeof(double), typeof(double) }));
            lua.RegisterFunction("max", math.GetMethod("Max", new Type[] { typeof(double), typeof(double) }));
            
            lua.RegisterFunction("PDME_random0", typeof(LuaMap).GetMethod("Random0"));
            lua.RegisterFunction("PDME_random1", typeof(LuaMap).GetMethod("Random1"));
            lua.RegisterFunction("PDME_random2", typeof(LuaMap).GetMethod("Random2"));
            lua.RegisterFunction("randomseed", typeof(LuaMap).GetMethod("RandomSeed"));

            lua.DoString(@"
function random(a, b)
    if (b) then
        return PDME_random2(a, b)
    else
        if (a) then
            return PDME_random1(a)
        else
            return PDME_random0()
        end
    end
end");
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
                    if (dustCloud.Resources <= 0)
                        sb.AppendLine("\taddDustCloud(\"" + dustCloud.Name + "\", \"" + dustCloud.Type.Name + "\", " + WriteFloatLuaTable(dustCloud.Position.X, dustCloud.Position.Y, dustCloud.Position.Z) + ", " + WriteFloatLuaTable(dustCloud.Color.X, dustCloud.Color.Y, dustCloud.Color.Z, dustCloud.Color.W) + ", " + dustCloud.Spin.ToString(InvariantCulture) + ", " + dustCloud.Radius.ToString(InvariantCulture) + ")");
                    else
                    {
                        string refill = "0";
                        if (dustCloud.Refill)
                            refill = "1";

                        sb.AppendLine("\taddDustCloudWithResources(\"" + dustCloud.Name + "\", \"" + dustCloud.Type.Name + "\", " + WriteFloatLuaTable(dustCloud.Position.X, dustCloud.Position.Y, dustCloud.Position.Z) + ", " + dustCloud.Resources.ToString(InvariantCulture) + ", " + WriteFloatLuaTable(dustCloud.Color.X, dustCloud.Color.Y, dustCloud.Color.Z, dustCloud.Color.W) + ", " + dustCloud.Spin.ToString(InvariantCulture) + ", " + dustCloud.Radius.ToString(InvariantCulture) + ", " + refill.ToString(InvariantCulture) + ")");
                    }
                }
                sb.AppendLine("");
            }

            if (Nebula.Nebulas.Count > 0)
            {
                sb.AppendLine("\t-- Nebulas");
                foreach (Nebula nebula in Nebula.Nebulas)
                {

                    if (nebula.Resources <= 0)
                        sb.AppendLine("\taddNebula(\"" + nebula.Name + "\", \"" + nebula.Type.Name + "\", " + WriteFloatLuaTable(nebula.Position.X, nebula.Position.Y, nebula.Position.Z) + ", " + WriteFloatLuaTable(nebula.Color.X, nebula.Color.Y, nebula.Color.Z, nebula.Color.W) + ", " + nebula.Spin.ToString(InvariantCulture) + ", " + nebula.Radius.ToString(InvariantCulture) + ")");
                    else
                    {
                        string refill = "0";
                        if (nebula.Refill)
                            refill = "1";

                        sb.AppendLine("\taddNebulaWithResources(\"" + nebula.Name + "\", \"" + nebula.Type.Name + "\", " + WriteFloatLuaTable(nebula.Position.X, nebula.Position.Y, nebula.Position.Z) + ", " + nebula.Resources.ToString(InvariantCulture) + ", " + WriteFloatLuaTable(nebula.Color.X, nebula.Color.Y, nebula.Color.Z, nebula.Color.W) + ", " + nebula.Spin.ToString(InvariantCulture) + ", " + nebula.Radius.ToString(InvariantCulture) + ", " + refill.ToString(InvariantCulture) + ")");
                    }
                }
                sb.AppendLine("");
            }

            if (Sphere.Spheres.Count > 0)
            {
                sb.AppendLine("\t-- Spheres");
                foreach (Sphere sphere in Sphere.Spheres)
                {
                     sb.AppendLine("\taddSphere(\"" + sphere.Name + "\", " + WriteFloatLuaTable(sphere.Position.X, sphere.Position.Y, sphere.Position.Z) + ", " + sphere.Radius.ToString(InvariantCulture) + ")");
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

        public static string[] ExecuteCode(string code)
        {
            output.Clear();

            try { lua.DoString(code); }
            catch (NLua.Exceptions.LuaScriptException e) { output.Add(e.Message); }
            return output.ToArray();
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
        public static object[] DoFilePath(string path)
        {
            string file = path.Substring(path.IndexOf(':') + 1);
            foreach (string dataPath in HWData.DataPaths)
            {
                string filepath = Path.Combine(dataPath, file);
                if (File.Exists(filepath))
                {
                    string code = File.ReadAllText(filepath);
                    code = ConvertForLoopSyntax(code);
                    object[] ret = lua.DoString(code);
                    return ret;
                }
            }
            return null;
        }

        public static void Print(string text)
        {
            output.Add(text);
        }

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

        public static void AddDustCloud(string name, string type, LuaTable position, LuaTable color, float spin, float radius)
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

            new DustCloud(name, dustCloudType, pos, col, spin, radius);
        }

        public static void AddDustCloudWithResources(string name, string type, LuaTable position, float resources, LuaTable color, float spin, float radius, int refill)
        {
            //Log.WriteLine("Adding dust cloud \"" + name + "\".");

            Vector3 pos = LuaTableToVector3(position);
            Vector4 col = LuaTableToVector4(color);

            string newType = type.ToLower();
            DustCloudType dustCloudType = DustCloudType.GetTypeFromName(newType);

            bool bRefill = false;
            if (refill != 0)
                bRefill = true;

            if (dustCloudType == null)
            {
                new Problem(ProblemTypes.WARNING, "Dust cloud type \"" + newType + "\" not found. Skipping dust cloud \"" + name + "\".");
                return;
            }

            new DustCloud(name, dustCloudType, pos, resources, col, spin, radius, bRefill);
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

        public static void AddNebula(string name, string type, LuaTable position, LuaTable color, float spin, float radius)
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

            new Nebula(name, nebulaType, pos, col, spin, radius);
        }

        public static void AddNebulaWithResources(string name, string type, LuaTable position, float resources, LuaTable color, float spin, float radius, int refill)
        {
            //Log.WriteLine("Adding nebula \"" + name + "\".");

            Vector3 pos = LuaTableToVector3(position);
            Vector4 col = LuaTableToVector4(color);

            string newType = type.ToLower();
            NebulaType nebulaType = NebulaType.GetTypeFromName(newType);

            bool bRefill = false;
            if (refill != 0)
                bRefill = true;

            if (nebulaType == null)
            {
                new Problem(ProblemTypes.WARNING, "Nebula type \"" + newType + "\" not found. Skipping nebula \"" + name + "\".");
                return;
            }

            new Nebula(name, nebulaType, pos, resources, col, spin, radius, bRefill);
        }

        public static void AddSphere(string name, LuaTable position, float radius)
        {
            Vector3 pos = LuaTableToVector3(position);

            new Sphere(name, pos, radius);
        }

        public static void AddCamera(string name, LuaTable target, LuaTable position)
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

        public static void SetDustCloudColour(LuaTable color)
        {
            //STUB
        }

        public static void SetNebulaAmbient(LuaTable color)
        {
            //STUB
        }

        //MathLib
        public static double ToRadians(double value)
        {
            return value * 180.0 / Math.PI;
        }

        public static double ToDegrees(double value)
        {
            return value * Math.PI / 180.0;
        }

        private static Random rng = new Random();
        
        public static double Random0()
        {
            return rng.NextDouble();
        }

        public static int Random1(double u)
        {
            return (int)(rng.NextDouble() * u + 1);
        }

        public static int Random2(double l, double u)
        {
            return (int)(rng.NextDouble() * (u - l + 1) + 1);
        }

        public static void RandomSeed(int seed)
        {
            rng = new Random(seed);
        }
        #endregion
    }
}
