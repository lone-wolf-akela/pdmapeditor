using System;
using OpenTK.Graphics.OpenGL;
using System.Collections.Generic;
using OpenTK;
using System.Windows.Forms;

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
            objectTypes.Add(typeof(Sphere));

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

            //Set default object settings
            Program.main.comboAsteroidType.SelectedIndex = 0;
            Program.main.numericAsteroidResourceMultiplier.Value = 100;

            Program.main.comboPebbleType.SelectedIndex = 0;

            Program.main.comboDustCloudType.SelectedIndex = 0;
            Program.main.numericDustCloudSize.Value = 2000;
            Program.main.sliderDustCloudAlpha.Value = 100;
            Program.main.numericDustCloudResources.Value = 0;

            Program.main.comboSquadronType.SelectedIndex = 0;
            Program.main.comboSquadronPlayer.SelectedIndex = 0;

            Program.main.comboNebulaType.SelectedIndex = 0;
            Program.main.numericNebulaSize.Value = 5000;
            Program.main.sliderNebulaAlpha.Value = 100;
            Program.main.numericNebulaResources.Value = 0;

            Program.main.numericSphereRadius.Value = 5000;
        }

        static void TypeToCreateChanged(object sender, EventArgs e)
        {
            typeToCreate = objectTypes[Program.main.comboCreationType.SelectedIndex];

            if (Program.main.tabControlLeft.SelectedTab == Program.main.tabCreate)
            {
                CreateObjectAtCursor();
                UpdateGroupType();
            }
        }

        static void SelectedTabChanged(object sender, EventArgs e)
        {
            if (Program.main.tabControlLeft.SelectedTab == Program.main.tabCreate)
            {
                CreateObjectAtCursor();
                UpdateGroupType();
            }
            else if(Program.main.tabControlLeft.SelectedTab == Program.main.tabSelection)
            {
                DestroyObjectAtCursor();
                ResetGroupType();
                Selection.UpdateSelectionGUI();
            }
            else
            {
                DestroyObjectAtCursor();
                ResetGroupType();
            }
        }

        static void UpdateGroupType()
        {
            Program.main.groupAsteroid.Visible = false;
            Program.main.groupPebble.Visible = false;
            Program.main.groupPoint.Visible = false;
            Program.main.groupDustCloud.Visible = false;
            Program.main.groupSquadron.Visible = false;
            Program.main.groupNebula.Visible = false;
            Program.main.groupSphere.Visible = false;

            if (typeToCreate == typeof(Asteroid))
            {
                Program.main.tabCreate.Controls.Add(Program.main.groupAsteroid);
                Program.main.groupAsteroid.Location = new System.Drawing.Point(3, 33);
                Program.main.groupAsteroid.Visible = true;
            }
            else if (typeToCreate == typeof(Pebble))
            {
                Program.main.tabCreate.Controls.Add(Program.main.groupPebble);
                Program.main.groupPebble.Location = new System.Drawing.Point(3, 33);
                Program.main.groupPebble.Visible = true;
            }
            else if (typeToCreate == typeof(Point))
            {
                Program.main.boxPointName.Text = "Point" + (Point.Points.Count);

                Program.main.tabCreate.Controls.Add(Program.main.groupPoint);
                Program.main.groupPoint.Location = new System.Drawing.Point(3, 33);
                Program.main.groupPoint.Visible = true;
            }
            else if (typeToCreate == typeof(DustCloud))
            {
                Program.main.boxDustCloudName.Text = "dustCloud" + (DustCloud.DustClouds.Count + 1);

                Program.main.tabCreate.Controls.Add(Program.main.groupDustCloud);
                Program.main.groupDustCloud.Location = new System.Drawing.Point(3, 33);
                Program.main.groupDustCloud.Visible = true;
            }
            else if (typeToCreate == typeof(Squadron))
            {
                Program.main.boxSquadronName.Text = "Squadron" + Squadron.Squadrons.Count;

                Program.main.tabCreate.Controls.Add(Program.main.groupSquadron);
                Program.main.groupSquadron.Location = new System.Drawing.Point(3, 33);
                Program.main.groupSquadron.Visible = true;
            }
            else if (typeToCreate == typeof(Nebula))
            {
                Program.main.boxNebulaName.Text = "nebula" + (Nebula.Nebulas.Count + 1);

                Program.main.tabCreate.Controls.Add(Program.main.groupNebula);
                Program.main.groupNebula.Location = new System.Drawing.Point(3, 33);
                Program.main.groupNebula.Visible = true;
            }
            else if (typeToCreate == typeof(Sphere))
            {
                Program.main.boxSphereName.Text = "Sphere" + Sphere.Spheres.Count;

                Program.main.tabCreate.Controls.Add(Program.main.groupSphere);
                Program.main.groupSphere.Location = new System.Drawing.Point(3, 33);
                Program.main.groupSphere.Visible = true;
            }
        }
        
        static void ResetGroupType()
        {
            Program.main.groupAsteroid.Visible = false;
            Program.main.groupPebble.Visible = false;
            Program.main.groupPoint.Visible = false;
            Program.main.groupDustCloud.Visible = false;
            Program.main.groupSquadron.Visible = false;
            Program.main.groupNebula.Visible = false;
            Program.main.groupSphere.Visible = false;

            Program.main.tabSelection.Controls.Add(Program.main.groupAsteroid);
            Program.main.groupAsteroid.Location = new System.Drawing.Point(3, 215);

            Program.main.tabSelection.Controls.Add(Program.main.groupPebble);
            Program.main.groupPebble.Location = new System.Drawing.Point(3, 215);

            Program.main.tabSelection.Controls.Add(Program.main.groupPoint);
            Program.main.groupPoint.Location = new System.Drawing.Point(3, 215);

            Program.main.tabSelection.Controls.Add(Program.main.groupDustCloud);
            Program.main.groupDustCloud.Location = new System.Drawing.Point(3, 215);

            Program.main.tabSelection.Controls.Add(Program.main.groupSquadron);
            Program.main.groupSquadron.Location = new System.Drawing.Point(3, 215);

            Program.main.tabSelection.Controls.Add(Program.main.groupNebula);
            Program.main.groupNebula.Location = new System.Drawing.Point(3, 215);

            Program.main.tabSelection.Controls.Add(Program.main.groupSphere);
            Program.main.groupSphere.Location = new System.Drawing.Point(3, 215);
        }

        static void DestroyObjectAtCursor()
        {
            if (CreatedDrawable != null)
            {
                CreatedDrawable.Destroy();
                Renderer.UpdateMeshData();

                CreatedDrawable = null;

                Selection.gizmoLineX.Visible = false;
                Selection.gizmoLineY.Visible = false;
                Selection.gizmoLineZ.Visible = false;
            }
        }

        public static void CreateObjectAtCursor()
        {
            DestroyObjectAtCursor();

            CreatedDrawable = (Drawable)Activator.CreateInstance(typeToCreate);
            UpdateGroupType();

            Renderer.UpdateMeshData();

            Selection.gizmoLineX.Visible = true;
            Selection.gizmoLineY.Visible = true;
            Selection.gizmoLineZ.Visible = true;
        }

        public static void UpdateObjectAtCursor()
        {
            if (CreatedDrawable != null)
            {
                System.Drawing.Point pressPos = Program.GLControl.PointToClient(Cursor.Position);
                int x = pressPos.X;
                int y = Program.GLControl.ClientSize.Height - pressPos.Y;

                Vector3 position = ScreenToWorldCoord(x, y);
                CreatedDrawable.Position = position;

                Selection.gizmoLineX.Position = position;
                Selection.gizmoLineY.Position = position;
                Selection.gizmoLineZ.Position = position;
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
