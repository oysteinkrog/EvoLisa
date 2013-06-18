using System;
using System.Collections.Generic;
using System.Linq;
using GenArt.Classes;

namespace GenArt.AST
{
    [Serializable]
    public class DnaPolygon
    {
        public List<DnaPoint> Points { get; private set; }
        public DnaBrush Brush { get; private set; }

        public DnaPolygon(IEnumerable<DnaPoint> points, DnaBrush brush)
        {
            Points = points.ToList();
            Brush = brush;
        }

        public static DnaPolygon GetRandom()
        {
            var points = new List<DnaPoint>();

            //int count = Tools.GetRandomNumber(3, 3);
            var origin = DnaPoint.GetRandom();

            for (int i = 0; i < Settings.ActivePointsPerPolygonMin; i++)
            {
                int clampedX = MathUtils.Clamp(origin.X + Tools.GetRandomNumber(-3, 3), 0, Tools.MaxWidth);
                int clampedY = MathUtils.Clamp(origin.Y + Tools.GetRandomNumber(-3, 3), 0, Tools.MaxHeight);
                var clampedPoint = new DnaPoint(clampedX, clampedY);

                points.Add(clampedPoint);
            }

            var brush = DnaBrush.GetRandom();

            return new DnaPolygon(points, brush);
        }

        public DnaPolygon Clone()
        {
            var clonedPoints = Points.Select(point => point.Clone());
            var clonedBrush = Brush.Clone();
            return new DnaPolygon(clonedPoints, clonedBrush);
        }
    }
}