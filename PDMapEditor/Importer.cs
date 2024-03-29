﻿using Assimp;
using Assimp.Configs;
using System;

namespace PDMapEditor
{
    public static class Importer
    {
        public static void Init()
        {
            AssimpContext importer = new AssimpContext();
            NormalSmoothingAngleConfig config = new NormalSmoothingAngleConfig(66.0f);
            importer.SetConfig(config);

            LogStream logStream = new LogStream(delegate (string msg, string userData)
            {
                Console.WriteLine(msg);
            });
            logStream.Attach();

            Scene scene = importer.ImportFile(@"resources/icosphere.obj", PostProcessPreset.TargetRealTimeQuality);
            Mesh.Icosphere = scene.Meshes[0];

            scene = importer.ImportFile(@"resources/icosphereHigh.obj", PostProcessPreset.TargetRealTimeQuality);
            Mesh.IcosphereHigh = scene.Meshes[0];

            scene = importer.ImportFile(@"resources/asteroid.obj", PostProcessPreset.TargetRealTimeQuality);
            Mesh.Asteroid = scene.Meshes[0];

            scene = importer.ImportFile(@"resources/point.obj", PostProcessPreset.TargetRealTimeQuality);
            Mesh.Point = scene.Meshes[0];

            scene = importer.ImportFile(@"resources/cube.obj", PostProcessPreset.TargetRealTimeQuality);
            Mesh.Cube = scene.Meshes[0];

            scene = importer.ImportFile(@"resources/camera.obj", PostProcessPreset.TargetRealTimeQuality);
            Mesh.Camera = scene.Meshes[0];

            scene = importer.ImportFile(@"resources/skybox.obj", PostProcessPreset.TargetRealTimeQuality);
            Mesh.SkyboxFront = scene.Meshes[0];
            Mesh.SkyboxBack = scene.Meshes[1];
            Mesh.SkyboxRight = scene.Meshes[2];
            Mesh.SkyboxLeft = scene.Meshes[3];
            Mesh.SkyboxTop = scene.Meshes[4];
            Mesh.SkyboxBottom = scene.Meshes[5];

            scene = importer.ImportFile(@"resources/gizmoPosX.ply", PostProcessPreset.TargetRealTimeQuality);
            Mesh.GizmoXPos = scene.Meshes[0];
            scene = importer.ImportFile(@"resources/gizmoPosY.ply", PostProcessPreset.TargetRealTimeQuality);
            Mesh.GizmoYPos = scene.Meshes[0];
            scene = importer.ImportFile(@"resources/gizmoPosZ.ply", PostProcessPreset.TargetRealTimeQuality);
            Mesh.GizmoZPos = scene.Meshes[0];

            scene = importer.ImportFile(@"resources/gizmoRotX.ply", PostProcessPreset.TargetRealTimeQuality);
            Mesh.GizmoXRot = scene.Meshes[0];
            scene = importer.ImportFile(@"resources/gizmoRotY.ply", PostProcessPreset.TargetRealTimeQuality);
            Mesh.GizmoYRot = scene.Meshes[0];
            scene = importer.ImportFile(@"resources/gizmoRotZ.ply", PostProcessPreset.TargetRealTimeQuality);
            Mesh.GizmoZRot = scene.Meshes[0];

            scene = importer.ImportFile(@"resources/text000.obj", PostProcessPreset.TargetRealTimeQuality);
            Mesh.Text000 = scene.Meshes[0];
            scene = importer.ImportFile(@"resources/text090.obj", PostProcessPreset.TargetRealTimeQuality);
            Mesh.Text090 = scene.Meshes[0];
            scene = importer.ImportFile(@"resources/text180.obj", PostProcessPreset.TargetRealTimeQuality);
            Mesh.Text180 = scene.Meshes[0];
            scene = importer.ImportFile(@"resources/text270.obj", PostProcessPreset.TargetRealTimeQuality);
            Mesh.Text270 = scene.Meshes[0];

            importer.Dispose();
            logStream.Detach();
        }
    }
}
