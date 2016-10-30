using OpenTK;
using System.Collections.Generic;
using System.IO;
using OpenTK.Graphics.OpenGL;
using System.Xml.Linq;
using System.Globalization;

namespace PDMapEditor
{
    public class Background
    {
        //---------------------------- STATIC -----------------------------//
        public static List<Background> Backgrounds = new List<Background>();

        public static Drawable[] Skybox = new Drawable[6];
        public static Texture[] SkyboxTextures = new Texture[6];

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
                face.Mesh.Scale = new Vector3(1000);
                face.Mesh.DrawInBack = true;
                face.Mesh.VertexColored = false;
                face.Mesh.Material.DiffuseColor = Renderer.BackgroundColor * 0.05f;
            }

            SetSkyboxFade(0.5f);
        }

        public static void SetSkyboxPosition(Vector3 pos)
        {
            foreach (Drawable face in Skybox)
                if(face != null)
                    face.Position = pos;
        }

        public static void SetSkyboxFade(float fade)
        {
            foreach (Drawable face in Skybox)
                if (face != null)
                    face.Mesh.Material.TextureFactor = fade;

            if(Program.GLControl != null)
                Renderer.Invalidate();
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
            SetSkyboxFade(0.5f);
        }

        public static void SetSkyboxTexture(Background bg)
        {
            RemoveSkyboxTexture();

            if (Map.Background.Fade <= 0)
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
                string file = bg.Path + "_hq_" + faces[i];

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
                        file = bg.Path + "_" + faces[i];

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
                Skybox[i].Mesh.Material.DiffuseColor = Renderer.BackgroundColor * 0.05f;
                SetSkyboxFade(bg.Fade);
            }
        }

        //SAVING/LOADING
        public static void LoadBackgroundFades()
        {
            if (!File.Exists(System.IO.Path.Combine(Program.EXECUTABLE_PATH, "backgrounds.xml")))
            {
                Log.WriteLine("No backgrounds.xml found, using default values.");
                return;
            }

            try
            {
                string file = File.ReadAllText(System.IO.Path.Combine(Program.EXECUTABLE_PATH, "backgrounds.xml"));
                XElement backgrounds = XElement.Parse(file);

                foreach (XElement element in backgrounds.Elements())
                {
                    Background bg = GetBackgroundFromName(element.Name.LocalName);
                    if (bg == null)
                        continue;

                    float value = 0.5f;
                    float.TryParse(element.Value, NumberStyles.Float, CultureInfo.InvariantCulture, out value);
                    bg.Fade = value;
                }
            }
            catch
            {
                Log.WriteLine("Failed to load \"" + System.IO.Path.Combine(Program.EXECUTABLE_PATH, "backgrounds.xml") + "\".");
            }
        }
        public static void SaveBackgroundFades()
        {
            XElement backgroundFades =
                new XElement("backgroundFades");

            foreach (Background bg in Backgrounds)
            {
                backgroundFades.Add(new XElement(bg.Name, bg.Fade));
            }

            File.WriteAllText(System.IO.Path.Combine(Program.EXECUTABLE_PATH, "backgrounds.xml"), backgroundFades.ToString());
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

        //---------------------------- INSTANCE -----------------------------//
        public string Name;
        public string Path;
        public int ComboIndex;
        private float fade = 0.5f;
        public float Fade
        {
            get { return fade; }
            set
            {
                fade = value;

                if(Map.Background == this)
                    SetSkyboxFade(value);
            }
        }

        public Background(string name, string path)
        {
            Name = name;
            Path = path;

            Backgrounds.Add(this);

            Program.main.comboBackground.Items.Add(name);
            ComboIndex = Program.main.comboBackground.Items.Count - 1;
        }
    }
}
