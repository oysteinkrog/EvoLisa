using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using GenArt.Classes;
using System;
using GenArt.Core.AST.Mutation;

namespace GenArt.AST
{
    [Serializable]
    public class DnaDrawing
    {
        public DnaDrawing()
        {
            Polygons = new List<DnaPolygon>();
            IsDirty = false;
        }

        public DnaDrawing(List<DnaPolygon> polygons, bool isDirty = true)
        {
            Polygons = polygons;
            IsDirty = isDirty;
        }

        public List<DnaPolygon> Polygons { get; private set; }

        [XmlIgnore]
        public bool IsDirty { get; private set; }

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

        public static DnaDrawing GetRandom()
        {
            var drawing = new DnaDrawing();
            for (int i = 0; i < Settings.ActivePolygonsMin; i++)
            {
                drawing.AddPolygon();
            }
            return drawing;
        }

        public DnaDrawing Clone()
        {
            var clonedPolygons = Polygons.Select(polygon => polygon.Clone()).ToList();
            return new DnaDrawing(clonedPolygons, false);
        }
    }
}