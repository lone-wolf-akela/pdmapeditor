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
        }

        public void SaveMap(string path)
        {
            string name = Path.GetFileName(path);

            string file = "";

            //Top comment
            file += "-- PayDay's Homeworld Remastered Map Editor generated file\n";
            file += "-- ============================================================\n";
            file += "-- Name: " + name + "\n";
            file += "-- Purpose: Automatically generated level file by the map editor.\n";
            file += "-- Created " + DateTime.Now.ToLongDateString() + " at " + DateTime.Now.ToLongTimeString() + ".\n";
            file += "-- ============================================================\n";
            file += "\n";

            //DetermChunk()
            file += "function DetermChunk()\n";
            file += "\t-- Points\n";
            foreach(Point point in Point.Points)
            {
                file += "\taddPoint(\"" + point.Name + "\", " + WriteFloatLuaTable(point.Position.X, point.Position.Y, point.Position.Z) + ", " + WriteFloatLuaTable(point.Rotation.X, point.Rotation.Y, point.Rotation.Z) + ")\n";
            }
            file += "\n";
            file += "\t-- Asteroids\n";
            foreach (Asteroid asteroid in Asteroid.Asteroids)
            {
                file += "\taddAsteroid(\"" + asteroid.Type.Name + "\", " + WriteFloatLuaTable(asteroid.Position.X, asteroid.Position.Y, asteroid.Position.Z) + ", " + asteroid.Multiplier.ToString(InvariantCulture) + ", " + asteroid.Rotation.X.ToString(InvariantCulture) + ", " + asteroid.Rotation.Y.ToString(InvariantCulture) + ", " + asteroid.Rotation.Z.ToString(InvariantCulture) + ", " + asteroid.RotSpeed.ToString(InvariantCulture) + ")\n";
            }
            file += "\n";
            file += "\t-- Dust clouds\n";
            foreach (DustCloud dustCloud in DustCloud.DustClouds)
            {
                file += "\taddDustCloud(\"" + dustCloud.Name + "\", \"" + dustCloud.Type.Name + "\", " + WriteFloatLuaTable(dustCloud.Position.X, dustCloud.Position.Y, dustCloud.Position.Z) + ", " + WriteFloatLuaTable(dustCloud.Color.X, dustCloud.Color.Y, dustCloud.Color.Z, dustCloud.Color.W) + ", " + dustCloud.Unknown1.ToString(InvariantCulture) + ", " + dustCloud.Size.ToString(InvariantCulture) + ")\n";
            }
            file += "\n";
            file += "\t-- Settings\n";
            file += "\tsetWorldBoundsInner(" + WriteFloatLuaTable(0, 0, 0) + ", " + WriteFloatLuaTable(Map.MapDimensions.X, Map.MapDimensions.Y, Map.MapDimensions.Z) + ")\n";
            file += "end\n";

            file += "\n";

            //NonDetermChunk()
            file += "function NonDetermChunk()\n";
            file += "\t-- Pebbles\n";
            foreach (Pebble pebble in Pebble.Pebbles)
            {
                file += "\taddPebble(\"" + pebble.Type.Name + "\", " + WriteFloatLuaTable(pebble.Position.X, pebble.Position.Y, pebble.Position.Z) + ", " + pebble.Rotation.X.ToString(InvariantCulture) + ", " + pebble.Rotation.Y.ToString(InvariantCulture) + ", " + pebble.Rotation.Z.ToString(InvariantCulture) + ")\n";
            }
            file += "\n";
            file += "\t-- Fog\n";
            string fogActive = "0";
            if (Map.FogActive)
                fogActive = "1";
            file += "\tfogSetActive(" + fogActive + ")\n";
            if(Map.FogActive)
            {
                file += "\tfogSetStart(" + Map.FogStart.ToString(InvariantCulture) + ")\n";
                file += "\tfogSetEnd(" + Map.FogEnd.ToString(InvariantCulture) + ")\n";
                file += "\tfogSetColour(" + Map.FogColor.X.ToString(InvariantCulture) + ", " + Map.FogColor.Y.ToString(InvariantCulture) + ", " + Map.FogColor.Z.ToString(InvariantCulture) + ", " + Map.FogColor.W.ToString(InvariantCulture) + ")\n";
                file += "\tfogSetType(\"" + Map.FogType + "\")\n";
                file += "\tfogSetDensity(" + Map.FogDensity.ToString(InvariantCulture) + ")\n";
            }
            file += "end\n";

            File.WriteAllText(path, file);
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

        public void LoadBackground(string background)
        {
            //STUB
        }

        public void SetSensorsManagerCameraDistances(float min, float max)
        {
            //STUB
        }

        public void SetDefaultMusic(string path)
        {
            //STUB
        }

        public void SetBattleMusic(string path)
        {
            //STUB
        }

        public void SetGlareIntensity(float intensity)
        {
            //STUB
        }

        public void SetLevelShadowColour(float red, float green, float blue, float alpha)
        {
            //STUB
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
