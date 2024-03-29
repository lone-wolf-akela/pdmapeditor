﻿using OpenTK;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace PDMapEditor
{
    public static class Map
    {
        //Contants
        const int CIRCLE_SEGMENTS = 100;
        const int POLAR_LINES = 20;
        const int POLAR_CIRCLES = 11;
        const int POLAR_CIRCLE_SEGMENTS = 100;
        public static Vector3 COLOR_GRID = new Vector3(0.1f);
        public static Vector3 COLOR_CIRCLE = new Vector3(1, 0.65f, 0.16f);

        private static string path;
        public static string Path { get { return path; } set { path = value; Program.main.Text = "PayDay's Homeworld Remastered Map Editor"; if (value.Length > 0) Program.main.Text += " - " + value; } }

        private static Vector3 mapDimensions = new Vector3(40000);
        public static Vector3 MapDimensions { get { return mapDimensions; } set { mapDimensions = value; CreateGrid(PolarGrid); SetCameraZooming(); CreateCircle(); CreateDegreeTexts(); Renderer.InvalidateMeshData(); Renderer.InvalidateView(); Renderer.Invalidate(); Program.main.UpdateMapDimensions(); } }

        private static Background background;
        public static Background Background { get { return background; } set { background = value; Program.main.comboBackground.SelectedIndex = value.ComboIndex; Background.SetSkyboxTexture(value); /*if(Program.Settings != null) Program.Settings.Open();*/ Renderer.Invalidate(); } }

        private static float glareIntensity = 0;
        public static float GlareIntensity { get { return glareIntensity; } set { glareIntensity = value; Program.main.sliderGlareIntensity.Value = (int)Math.Round(value * 100); } }

        private static Vector4 shadowColor = new Vector4(0, 0, 0, 1);
        public static Vector4 ShadowColor { get { return shadowColor; } set { shadowColor = value; Program.main.buttonShadowColor.BackColor = Color.FromArgb((int)Math.Round(value.X * 255), (int)Math.Round(value.Y * 255), (int)Math.Round(value.Z * 255)); Program.main.sliderShadowAlpha.Value = (int)Math.Round(value.W * 100); } }

        private static float sensorsManagerCameraMin = 10000;
        public static float SensorsManagerCameraMin { get { return sensorsManagerCameraMin; } set { sensorsManagerCameraMin = value; Program.main.numericSensorsManagerCameraMin.Value = (decimal)value; } }

        private static float sensorsManagerCameraMax = 55000;
        public static float SensorsManagerCameraMax { get { return sensorsManagerCameraMax; } set { sensorsManagerCameraMax = value; Program.main.numericSensorsManagerCameraMax.Value = (decimal)value; } }

        private static int maxPlayers = 2;
        public static int MaxPlayers { get { return maxPlayers; } set { maxPlayers = value; Program.main.comboMaxPlayers.SelectedIndex = maxPlayers - 1; } }

        private static string musicDefault = "sound/music/ambient/amb_01";
        public static string MusicDefault { get { return musicDefault; } set { musicDefault = value; Program.main.boxMusicDefault.Text = value; } }

        private static string musicBattle = "sound/music/battle/battle_01";
        public static string MusicBattle { get { return musicBattle; } set { musicBattle = value; Program.main.boxMusicBattle.Text = value; } }

        private static string description = "";
        public static string Description { get { return description; } set { description = value; Program.main.boxDescription.Text = value; } }

        private static bool fogActive = true;
        public static bool FogActive
        {
            get { return fogActive; }
            set
            {
                fogActive = value;
                Program.main.UpdateFogActive();

                if(value && Renderer.DisplayFog)
                    Renderer.BackgroundColor = new Vector3(FogColor);
                else
                    Renderer.BackgroundColor = new Vector3(0.05f, 0.05f, 0.05f);
            }
        }

        private static float fogStart = 1000;
        public static float FogStart { get { return fogStart; } set { fogStart = value; Program.main.UpdateFogStart(); } }

        private static float fogEnd = 1000;
        public static float FogEnd { get { return fogEnd; } set { fogEnd = value; Program.main.UpdateFogEnd(); } }

        private static Vector4 fogColor = new Vector4(1, 1, 1, 1);
        public static Vector4 FogColor
        {
            get
            {
                return fogColor;
            }
            set
            {
                fogColor = value;
                Program.main.UpdateFogColor();
                float[] colorArray = { fogColor.X, fogColor.Y, fogColor.Z, fogColor.W };

                if(Renderer.DisplayFog)
                    Renderer.BackgroundColor = new Vector3(FogColor);
            }
        }

        private static string fogType = "linear";
        public static string FogType
        {
            get
            {
                return fogType;
            }
            set
            {
                fogType = value;
                Program.main.UpdateFogType();
            }
        }

        private static float fogDensity = 0.2f;
        public static float FogDensity
        {
            get
            {
                return fogDensity;
            }
            set
            {
                fogDensity = value;
                Program.main.UpdateFogDensity();
            }
        }

        private static List<Drawable> gridLines = new List<Drawable>();
        private static LineLoop circleLine;
        private static List<Drawable> degreeTexts = new List<Drawable>();

        private static bool polarGrid = false;
        public static bool PolarGrid { get { return polarGrid; } set { polarGrid = value; CreateGrid(value); if (Renderer.Initialized) { Renderer.InvalidateMeshData(); Renderer.InvalidateView(); Renderer.Invalidate(); } } }

        private static void CreateGrid(bool polar = false)
        {
            Drawable.Drawables = Drawable.Drawables.Except(gridLines).ToList();
            gridLines.Clear();

            if (!polar)
            {
                for (float x = -MapDimensions.X; x <= MapDimensions.X; x += MapDimensions.X / 20)
                {
                    Vector3 start = new Vector3(x, 0, -MapDimensions.Z);
                    Vector3 end = new Vector3(x, 0, MapDimensions.Z);
                    Line line = new Line(start, end, COLOR_GRID);
                    gridLines.Add(line);
                }

                for (float z = -MapDimensions.Z; z <= MapDimensions.Z; z += MapDimensions.Z / 20)
                {
                    Vector3 start = new Vector3(-MapDimensions.X, 0, z);
                    Vector3 end = new Vector3(MapDimensions.X, 0, z);
                    Line line = new Line(start, end, COLOR_GRID);
                    gridLines.Add(line);
                }
            }
            else
            {
                float radius = Math.Max(MapDimensions.X, MapDimensions.Z);

                //Lines
                double angleDelta = Math.PI / POLAR_LINES;
                double angle = 0;
                float width = 1;
                for (int i = 0; i < POLAR_LINES; i++)
                {
                    float startX = (float)Math.Cos(angle) * radius;
                    float startZ = (float)Math.Sin(angle) * radius;
                    Vector3 start = new Vector3(startX, 0, startZ);

                    float endX = (float)Math.Cos(angle + Math.PI) * radius;
                    float endZ = (float)Math.Sin(angle + Math.PI) * radius;
                    Vector3 end = new Vector3(endX, 0, endZ);

                    if (i == 0 || i == POLAR_LINES / 2)
                        width = 1.6f;
                    else
                        width = 1;

                    Line line = new Line(start, end, COLOR_GRID, width);
                    gridLines.Add(line);

                    angle += angleDelta;
                }

                //Circles
                float circleRadius = 0;
                float circleRadiusDelta = radius / POLAR_CIRCLES;
                for (int n = 0; n < POLAR_CIRCLES; n++)
                {
                    double theta = 0;
                    Vector3[] polygons = new Vector3[POLAR_CIRCLE_SEGMENTS];

                    for (int i = 0; i < POLAR_CIRCLE_SEGMENTS; i++)
                    {
                        theta = 2.0f * Math.PI * i / POLAR_CIRCLE_SEGMENTS;

                        float x = circleRadius * (float)Math.Cos(theta);
                        float z = circleRadius * (float)Math.Sin(theta);

                        polygons[i] = new Vector3(x, 0, z);
                    }

                    LineLoop line = new LineLoop(polygons, COLOR_GRID);
                    gridLines.Add(line);

                    circleRadius += circleRadiusDelta;
                }
            }
        }

        private static void CreateCircle()
        {
            Drawable.Drawables.Remove(circleLine);

            float radius = Math.Max(MapDimensions.X, MapDimensions.Z);
            double theta = 2.0f * Math.PI * 0 / CIRCLE_SEGMENTS;
            Vector3[] polygons = new Vector3[CIRCLE_SEGMENTS];

            for (int i = 0; i < CIRCLE_SEGMENTS; i++)
            {
                theta = 2.0f * Math.PI * i / CIRCLE_SEGMENTS;

                float x = radius * (float)Math.Cos(theta);
                float z = radius * (float)Math.Sin(theta); 

                polygons[i] = new Vector3(x, 0, z);
            }

            circleLine = new LineLoop(polygons, COLOR_CIRCLE, 2);
        }

        private static void CreateDegreeTexts()
        {
            Drawable.Drawables = Drawable.Drawables.Except(degreeTexts).ToList();
            degreeTexts.Clear();

            float radius = Math.Max(MapDimensions.X, MapDimensions.Z);
            float textOffset = radius / 12f;
            float textScale = radius / 8f;

            Drawable text = new Drawable(new Vector3(0, 0, -radius - textOffset), Vector3.Zero, Mesh.Text000);
            text.Mesh.Scale = new Vector3(textScale);
            text.Mesh.Material.DiffuseColor = COLOR_CIRCLE;
            degreeTexts.Add(text);

            text = new Drawable(new Vector3(radius + textOffset, 0, 0), new Vector3(0, -90, 0), Mesh.Text090);
            text.Mesh.Scale = new Vector3(textScale);
            text.Mesh.Material.DiffuseColor = COLOR_CIRCLE;
            degreeTexts.Add(text);

            text = new Drawable(new Vector3(0, 0, radius + textOffset), new Vector3(0, 180, 0), Mesh.Text180);
            text.Mesh.Scale = new Vector3(textScale);
            text.Mesh.Material.DiffuseColor = COLOR_CIRCLE;
            degreeTexts.Add(text);

            text = new Drawable(new Vector3(-radius - textOffset, 0, 0), new Vector3(0, 90, 0), Mesh.Text270);
            text.Mesh.Scale = new Vector3(textScale);
            text.Mesh.Material.DiffuseColor = COLOR_CIRCLE;
            degreeTexts.Add(text);
        }

        private static void SetCameraZooming()
        {
            float farthestMapDimension = Math.Max(MapDimensions.X, Math.Max(MapDimensions.Y, MapDimensions.Z));

            Program.Camera.MaxZoom = farthestMapDimension * 3;
            Program.Camera.ClipDistance = farthestMapDimension * 10;
            Program.Camera.ZoomSpeed = farthestMapDimension * 4;
            Program.Camera.CalculatedZoom = farthestMapDimension * 3;

            Program.Camera.Update(true);
        }

        public static void Clear()
        {
            Program.main.Clear();

            Drawable.Drawables.Clear();
            Mesh.Meshes.Clear();
            Asteroid.Asteroids.Clear();
            Pebble.Pebbles.Clear();
            Point.Points.Clear();
            DustCloud.DustClouds.Clear();
            Cloud.Clouds.Clear();
            Sphere.Spheres.Clear();
            Squadron.Squadrons.Clear();
            Salvage.Salvages.Clear();
            Camera.Cameras.Clear();

            SOBGroup.SOBGroups.Clear();

            Problem.Problems.Clear();
            Program.main.UpdateProblems();

            SavedAction.ClearHistory();


            Path = "";

            Map.MapDimensions = new Vector3(20000, 20000, 20000);
            Map.FogActive = true;
            Map.FogStart = 100;
            Map.FogEnd = 20000;
            Map.FogColor = new Vector4(0.38f, 0.21f, 0.06f, 1);
            Map.FogType = "linear";
            Map.FogDensity = 0.15f;

            Map.GlareIntensity = 0;
            Map.ShadowColor = new Vector4(0, 0, 0, 1);

            Map.SensorsManagerCameraMin = 10000;
            Map.SensorsManagerCameraMax = 55000;

            Map.MusicDefault = "sound/music/ambient/amb_01";
            Map.MusicBattle = "sound/music/battle/battle_01";

            Map.MaxPlayers = 2;
            Map.Description = "";

            Renderer.BackgroundColor = new Vector3(0.05f);

            Selection.CreateGizmos();
            Selection.Selected.Clear();

            Background.CreateSkybox();

            Renderer.InvalidateMeshData();
        }
    }
}
