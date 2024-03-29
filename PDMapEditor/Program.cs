﻿using OpenTK;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace PDMapEditor
{
    static class Program
    {
        public static Main main;
        public static Hotkeys hotkeys;

        public static GLControl GLControl;
        public static EditorCamera Camera = new EditorCamera();
        public static Settings Settings;
        public static ExecuteCode ExecuteCode;

        //Frame-independence
        public static Stopwatch DeltaCounter = new Stopwatch();
        public static double ElapsedSeconds;
        public static double ElapsedMilliseconds;

        public static int FSAASamples = 4;

        public static string EXECUTABLE_PATH = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
        public static int BUILD = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Build;

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Log.Init();

            SetupTypeConverters();

            main = new Main();
            Settings.LoadSettings();
            ActionKey.Init();

            Hotkeys.LoadHotkeys();

            CreateGLControl();

            Application.Run(main);
        }

        public static void CreateGLControl()
        {
            GLControl = new CustomGLControl(FSAASamples)
            {
                BackColor = System.Drawing.Color.Black,
                Dock = System.Windows.Forms.DockStyle.Fill,
                Location = new System.Drawing.Point(0, 0),
                Name = "glControl",
                Size = new System.Drawing.Size(865, 758),
                TabIndex = 0,
                VSync = true
            };

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

        private static void SetupTypeConverters()
        {
        }
    }
}
