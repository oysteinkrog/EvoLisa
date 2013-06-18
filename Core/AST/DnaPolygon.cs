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

        public void Mutate(DnaDrawing drawing)
        {
            if (Tools.WillMutate(Settings.ActiveAddPointMutationRate))
                AddPoint(drawing);

            if (Tools.WillMutate(Settings.ActiveRemovePointMutationRate))
                RemovePoint(drawing);

            Brush.Mutate(drawing);
            Points.ForEach(p => p.Mutate(drawing));
        }

        private void RemovePoint(DnaDrawing drawing)
        {
            if (Points.Count > Settings.ActivePointsPerPolygonMin)
            {
                if (drawing.PointCount > Settings.ActivePointsMin)
                {
                    int index = Tools.GetRandomNumber(0, Points.Count);
                    Points.RemoveAt(index);

                    drawing.SetDirty();
                }
            }
        }

        private void AddPoint(DnaDrawing drawing)
        {
            if (Points.Count < Settings.ActivePointsPerPolygonMax)
            {
                if (drawing.PointCount < Settings.ActivePointsMax)
                {
                    int index = Tools.GetRandomNumber(1, Points.Count - 1);

                    DnaPoint prev = Points[index - 1];
                    DnaPoint next = Points[index];

                    var newPointX = (prev.X + next.X)/2;
                    var newPointY = (prev.Y + next.Y)/2;

                    var newPoint = new DnaPoint(newPointX, newPointY);

                    Points.Insert(index, newPoint);

                    drawing.SetDirty();
                }
            }
        }
    }
}