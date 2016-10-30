using OpenTK;
using System.Collections.Generic;

namespace PDMapEditor
{
    public class Material
    {
        public static List<Material> Materials = new List<Material>();

        public string Name;

        public bool Translucent = false;

        private Vector3 diffuseColor = Vector3.One;
        public Vector3 DiffuseColor { get { return diffuseColor; } set { diffuseColor = value; Renderer.Invalidate(); } }

        public Vector3 SpecularColor = new Vector3(0.1f, 0.1f, 0.1f);
        public float SpecularExponent = 0.3f;
        public float Opacity = 1.0f;

        public float TextureFactor = 1;

        public string DiffuseMap = "";
        public string SpecularMap = "";
        public string OpacityMap = "";
        public string NormalMap = "";

        public Texture DiffuseTexture;

        public Material()
        {
            Materials.Add(this);
        }

        public Material(string name, Vector3 diffuse, Vector3 specular, float specexponent = 1.0f, float opacity = 1.0f) : base()
        {
            Name = name;
            DiffuseColor = diffuse;
            SpecularColor = specular;
            SpecularExponent = specexponent;
            Opacity = opacity;
        }
    }
}
