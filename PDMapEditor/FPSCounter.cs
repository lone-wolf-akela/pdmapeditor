﻿using System;
using System.Windows.Forms;

namespace PDMapEditor
{
    static class FPSCounter
    {
        public static Label LabelFPS;

        static int frameCount = 0;
        static double accumulator = 0;
        static double fps = 0;
        static readonly double updateRate = 4.0;


        public static void Update()
        {
            frameCount++;
            accumulator += Program.ElapsedSeconds;
            if (accumulator > 1.0 / updateRate)
            {
                fps = frameCount / accumulator;
                LabelFPS.Text = Math.Round(fps) + " FPS";
                frameCount = 0;
                accumulator -= 1.0 / updateRate;
            }
        }
    }
}
