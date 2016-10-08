using OpenTK;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using OpenTK.Graphics.OpenGL;

namespace PDMapEditor
{
    public class Background
    {
        public static List<Background> Backgrounds = new List<Background>();

        public static Drawable[] Skybox = new Drawable[6];
        public static Texture[] SkyboxTextures = new Texture[6];

        private static bool displayCubemaps = true;
        public static bool DisplayCubemaps
        {
            get { return displayCubemaps; }
            set
            {
                displayCubemaps = value;
                if (value)
                {
                    if (Map.Background != null)
                        SetSkyboxTexture(Map.Background.Path);
                }
                else
                    RemoveSkyboxTexture();
            }
        }

        public string Name;
        public string Path;
        public int ComboIndex;

        public Background(string name, string path)
        {
            Name = name;
            Path = path;

            Backgrounds.Add(this);

            Program.main.comboBackground.Items.Add(name);
            ComboIndex = Program.main.comboBackground.Items.Count - 1;
        }

        public static void CreateSkybox()
        {
            Skybox = new Drawable[6];

            Skybox[0] = new Drawable(Vector3.Zero, Mesh.SkyboxFront);
            Skybox[1] = new Drawable(Vector3.Zero, Mesh.SkyboxBack);
            Skybox[2] = new Drawable(Vector3.Zero, Mesh.SkyboxRight);
            Skybox[3] = new Drawable(Vector3.Zero, Mesh.SkyboxLeft);
            Skybox[4] = new Drawable(Vector3.Zero, Mesh.SkyboxTop);
            Skybox[5] = new Drawable(Vector3.Zero, Mesh.SkyboxBottom);

            foreach (Drawable face in Skybox)
            {
                face.Mesh.Scale = new Vector3(100);
                face.Mesh.DrawInBack = true;
                face.Mesh.VertexColored = false;
            }

            SetSkyboxColor(Renderer.BackgroundColor * 0.05f);
        }

        public static void SetSkyboxPosition(Vector3 pos)
        {
            foreach (Drawable face in Skybox)
                if(face != null)
                    face.Position = pos;
        }

        public static void SetSkyboxColor(Vector3 Color)
        {
            foreach (Drawable face in Skybox)
                if (face != null)
                    face.Mesh.Material.DiffuseColor = Color;
        }

        private static void RemoveSkyboxTexture()
        {
            foreach (Texture texture in SkyboxTextures)
                if (texture != null)
                    GL.DeleteTexture(texture.ID);

            foreach (Drawable face in Skybox)
                if(face != null)
                    face.Mesh.Material.DiffuseTexture = null;

            SkyboxTextures = new Texture[6];
            SetSkyboxColor(Renderer.BackgroundColor * 0.05f);
        }

        public static void SetSkyboxTexture(string path)
        {
            RemoveSkyboxTexture();

            if (!DisplayCubemaps)
                return;

            string[] faces =
            {
                "posz",
                "negz",
                "negx",
                "posx",
                "negy",
                "posy",
            };

            for (int i = 0; i < faces.Length; i++)
            {
                string file = path + "_hq_" + faces[i];

                string newFile = file + ".dds";
                if (File.Exists(newFile))
                    SkyboxTextures[i] = Texture.LoadTexture(newFile);
                
                else
                {
                    newFile = file + ".tga";
                    if (File.Exists(newFile))
                        SkyboxTextures[i] = Texture.LoadTexture(newFile);
                }

                if (SkyboxTextures[i] != null)
                {
                    if (SkyboxTextures[i].ID == 0)
                    {
                        file = path + "_" + faces[i];

                        newFile = file + ".dds";
                        if (File.Exists(newFile))
                            SkyboxTextures[i] = Texture.LoadTexture(newFile);

                        else
                        {
                            newFile = file + ".tga";
                            if (File.Exists(newFile))
                                SkyboxTextures[i] = Texture.LoadTexture(newFile);
                        }
                    }
                }

                Skybox[i].Mesh.Material.DiffuseTexture = SkyboxTextures[i];

                if(SkyboxTextures[i] != null)
                    Skybox[i].Mesh.Material.DiffuseColor = Vector3.One;
                else
                    Skybox[i].Mesh.Material.DiffuseColor = Renderer.BackgroundColor * 0.05f;
            }
        }

        public static Background GetBackgroundFromName(string name)
        {
            foreach (Background bg in Backgrounds)
            {
                if (bg.Name == name)
                    return bg;
            }

            return null;
        }
    }
}
