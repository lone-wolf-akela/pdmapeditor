﻿using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDMapEditor
{
    public class Point : Drawable, ISelectable
    {
        public static List<Point> Points = new List<Point>();

        public string Name;
        public bool AllowRotation { get; set; }

        public Point(string name, Vector3 position, Vector3 rotation) : base (position, rotation)
        {
            Name = name;

            Mesh = new Mesh(position, rotation, Mesh.Point);
            Mesh.Material.DiffuseColor = new Vector3(1, 0, 0);
            Mesh.Scale = new Vector3(100);
            Points.Add(this);

            AllowRotation = true;
        }
    }
}
