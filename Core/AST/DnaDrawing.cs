using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using GenArt.Classes;
using System;

namespace GenArt.AST
{
    [Serializable]
    public class DnaDrawing
    {
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
            var polygons = new List<DnaPolygon>();

            for (int i = 0; i < Settings.ActivePolygonsMin; i++)
            {
                AddPolygon(polygons);
            }

            return new DnaDrawing(polygons, true);
        }

        public DnaDrawing Clone()
        {
            var clonedPolygons = Polygons.Select(polygon => polygon.Clone()).ToList();
            return new DnaDrawing(clonedPolygons, false);
        }

        public void Mutate()
        {
            if (Tools.WillMutate(Settings.ActiveAddPolygonMutationRate))
            {
                AddPolygon(Polygons);
                SetDirty();
            }

            if (Tools.WillMutate(Settings.ActiveRemovePolygonMutationRate))
            {
                RemovePolygon(Polygons);
                SetDirty();
            }

            if (Tools.WillMutate(Settings.ActiveMovePolygonMutationRate))
            {
                MovePolygon(Polygons);
                SetDirty();
            }

            foreach (DnaPolygon polygon in Polygons)
                polygon.Mutate(this);
        }

        public static void MovePolygon(List<DnaPolygon> polygons)
        {
            if (polygons.Count < 1)
                return;

            int index = Tools.GetRandomNumber(0, polygons.Count);
            DnaPolygon poly = polygons[index];
            polygons.RemoveAt(index);
            index = Tools.GetRandomNumber(0, polygons.Count);
            polygons.Insert(index, poly);
        }

        public static void RemovePolygon(List<DnaPolygon> polygons)
        {
            if (polygons.Count > Settings.ActivePolygonsMin)
            {
                int index = Tools.GetRandomNumber(0, polygons.Count);
                polygons.RemoveAt(index);
            }
        }

        public static void AddPolygon(List<DnaPolygon> polygons)
        {
            if (polygons.Count < Settings.ActivePolygonsMax)
            {
                var newPolygon = DnaPolygon.GetRandom();
                int index = Tools.GetRandomNumber(0, polygons.Count);

                polygons.Insert(index, newPolygon);
            }
        }

    }
}