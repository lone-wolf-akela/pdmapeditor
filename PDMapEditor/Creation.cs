using System;
using OpenTK.Graphics.OpenGL;
using System.Collections.Generic;
using OpenTK;

namespace PDMapEditor
{
    public static class Creation
    {
        static List<Type> objectTypes = new List<Type>();

        static Type typeToCreate;
        public static Drawable CreatedDrawable;

        public static void Init()
        {
            //Initialize object types
            objectTypes.Add(typeof(Asteroid));
            objectTypes.Add(typeof(Pebble));
            objectTypes.Add(typeof(Point));
            objectTypes.Add(typeof(DustCloud));
            objectTypes.Add(typeof(Squadron));
            objectTypes.Add(typeof(Nebula));

            //Add possible object types to creation type combo
            foreach (Type type in objectTypes)
            {
                Program.main.comboCreationType.Items.Add(type.Name);
            }
            Program.main.comboCreationType.SelectedIndex = 0;
            TypeToCreateChanged(null, EventArgs.Empty);

            //Register events
            Program.main.tabControlLeft.SelectedIndexChanged += new EventHandler(SelectedTabChanged);
            Program.main.comboCreationType.SelectedIndexChanged += new EventHandler(TypeToCreateChanged);
        }

        static void TypeToCreateChanged(object sender, EventArgs e)
        {
            typeToCreate = objectTypes[Program.main.comboCreationType.SelectedIndex];

            if (Program.main.tabControlLeft.SelectedTab == Program.main.tabCreate)
                CreateObjectAtCursor();
        }

        static void SelectedTabChanged(object sender, EventArgs e)
        {
            if (Program.main.tabControlLeft.SelectedTab == Program.main.tabCreate)
                CreateObjectAtCursor();
            else
                DestroyObjectAtCursor();
        }

        static void DestroyObjectAtCursor()
        {
            if (CreatedDrawable != null)
            {
                CreatedDrawable.Destroy();
                Renderer.UpdateMeshData();

                CreatedDrawable = null;
            }
        }

        static void CreateObjectAtCursor()
        {
            DestroyObjectAtCursor();

            CreatedDrawable = (Drawable)Activator.CreateInstance(typeToCreate);

            Renderer.UpdateMeshData();
        }

        public static void UpdateObjectAtCursor(int x, int y)
        {
            if (CreatedDrawable != null)
            {
                CreatedDrawable.Position = ScreenToWorldCoord(x, y);
            }
        }

        public static void LeftMouseUp(int x, int y)
        {
            if (CreatedDrawable != null)
            {
                CreatedDrawable = null;
                CreateObjectAtCursor();
            }
        }

        public static Vector3 ScreenToWorldCoord(int screenX, int screenY)
        {
            float x = (2.0f * screenX) / Program.GLControl.ClientSize.Width - 1.0f;
            float y = (2.0f * screenY) / Program.GLControl.ClientSize.Height - 1.0f;
            float z = 0.99f;

            Vector3 screen = new Vector3(x, y, z);

            Vector3 world = Vector3.TransformPerspective(screen, Renderer.ViewProjectionInverted);

            return world;
        }

    }
}
