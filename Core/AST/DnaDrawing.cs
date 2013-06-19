using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using GenArt.Core.AST.Mutation;
using GenArt.Core.Classes;

namespace GenArt.Core.AST
{
    [Serializable]
    public class DnaDrawing
    {
        public DnaDrawing(int width, int height)
        {
            Width = width;
            Height = height;
            Polygons = new List<DnaPolygon>();
            IsDirty = false;
        }

        public DnaDrawing(int width, int height, List<DnaPolygon> polygons, bool isDirty = true):
            this(width, height)
        {
            Polygons = polygons;
            IsDirty = isDirty;
        }

        public List<DnaPolygon> Polygons { get; private set; }

        [XmlIgnore]
        public bool IsDirty { get; private set; }

        [XmlIgnore]
        public int Width { get; set; }

        [XmlIgnore]
        public int Height { get; set; }

        public int PointCount
        {
            get
            {
                int pointCount = 0;
                foreach (DnaPolygon polygon in Polygons)
                    pointCount += polygon.Points.Count;

                return pointCount;
            }
        }

        public void SetDirty()
        {
            IsDirty = true;
        }

        public static DnaDrawing GetRandom(int width, int height)
        {
            var drawing = new DnaDrawing(width, height);
            for (int i = 0; i < Settings.ActivePolygonsMin; i++)
            {
                drawing.AddPolygon();
            }
            return drawing;
        }

        public DnaDrawing Clone()
        {
            var clonedPolygons = Polygons.Select(polygon => polygon.Clone()).ToList();
            return new DnaDrawing(Width, Height, clonedPolygons, false);
        }
    }
}