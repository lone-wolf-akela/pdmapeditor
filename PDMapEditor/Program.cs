using OpenTK;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PDMapEditor
{
    static class Program
    {
        public static Main main;
        public static Hotkeys hotkeys;

        public static GLControl GLControl;
        public static Camera Camera = new Camera();
        public static Settings Settings;

        //Frame-independence
        public static Stopwatch DeltaCounter = new Stopwatch();
        public static double ElapsedSeconds;
        public static double ElapsedMilliseconds;

        public static string EXECUTABLE_PATH = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
        public static int BUILD = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Build;

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            main = new Main();

            Log.Init();
            ActionKey.Init();

            Settings.LoadSettings();
            Hotkeys.LoadHotkeys();

            CreateGLControl();

            Application.Run(main);
        }

        public static void CreateGLControl()
        {
            GLControl = new CustomGLControl(4);
            GLControl.BackColor = System.Drawing.Color.Black;
            GLControl.Dock = System.Windows.Forms.DockStyle.Fill;
            GLControl.Location = new System.Drawing.Point(0, 0);
            GLControl.Name = "glControl";
            GLControl.Size = new System.Drawing.Size(865, 758);
            GLControl.TabIndex = 0;
            GLControl.VSync = true;
            GLControl.Load += new System.EventHandler(main.glControl_Load);
            GLControl.Paint += new System.Windows.Forms.PaintEventHandler(main.glControl_Render);
            GLControl.MouseEnter += new System.EventHandler(main.glControl_MouseEnter);
            GLControl.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(main.glControl_KeyDown);
            GLControl.KeyUp += new System.Windows.Forms.KeyEventHandler(main.glControl_KeyUp);
            GLControl.MouseLeave += new System.EventHandler(main.glControl_MouseLeave);
            GLControl.MouseDown += new System.Windows.Forms.MouseEventHandler(main.glControl_MouseDown);
            GLControl.MouseMove += new System.Windows.Forms.MouseEventHandler(main.glControl_MouseMove);
            GLControl.MouseUp += new System.Windows.Forms.MouseEventHandler(main.glControl_MouseUp);
            GLControl.Resize += new System.EventHandler(main.glControl_Resize);
            GLControl.LostFocus += new EventHandler(main.glControl_LostFocus);

            main.splitViewportAndProblems.Panel1.Controls.Add(GLControl);
        }
    }
}
