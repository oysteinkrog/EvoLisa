using GenArt.Core.Classes;

namespace GenArt.Core.AST.Mutation
{
    public static class DnaPolygonMutateExtension 
    {
        public static void Mutate(this DnaPolygon dnaPolygon, DnaDrawing drawing)
        {
            if (Tools.WillMutate(Settings.ActiveAddPointMutationRate))
                dnaPolygon.AddPoint(drawing);

            if (Tools.WillMutate(Settings.ActiveRemovePointMutationRate))
                dnaPolygon.RemovePoint(drawing);

            dnaPolygon.Brush.Mutate(drawing);
            dnaPolygon.Points.ForEach(p => p.Mutate(drawing));
        }

        private static void RemovePoint(this DnaPolygon dnaPolygon, DnaDrawing drawing)
        {
            if (dnaPolygon.Points.Count > Settings.ActivePointsPerPolygonMin)
            {
                if (drawing.PointCount > Settings.ActivePointsMin)
                {
                    int index = Tools.GetRandomNumber(0, dnaPolygon.Points.Count);
                    dnaPolygon.Points.RemoveAt(index);

                    drawing.SetDirty();
                }
            }
        }

        private static void AddPoint(this DnaPolygon dnaPolygon, DnaDrawing drawing)
        {
            if (dnaPolygon.Points.Count < Settings.ActivePointsPerPolygonMax)
            {
                if (drawing.PointCount < Settings.ActivePointsMax)
                {
                    int index = Tools.GetRandomNumber(1, dnaPolygon.Points.Count - 1);

                    DnaPoint prev = dnaPolygon.Points[index - 1];
                    DnaPoint next = dnaPolygon.Points[index];

                    var newPointX = (prev.X + next.X)/2;
                    var newPointY = (prev.Y + next.Y)/2;

                    var newPoint = new DnaPoint(newPointX, newPointY);

                    dnaPolygon.Points.Insert(index, newPoint);

                    drawing.SetDirty();
                }
            }
        }
    }
}