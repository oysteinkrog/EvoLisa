using GenArt.Core.Classes;

namespace GenArt.Core.AST.Mutation
{
    public static class DnaDrawingMutateExtension
    {
        public static void Mutate(this DnaDrawing dnaDrawing)
        {
            if (Tools.WillMutate(Settings.ActiveAddPolygonMutationRate))
            {
                dnaDrawing.AddPolygon();
                dnaDrawing.SetDirty();
            }

            if (Tools.WillMutate(Settings.ActiveRemovePolygonMutationRate))
            {
                dnaDrawing.RemovePolygon();
                dnaDrawing.SetDirty();
            }

            if (Tools.WillMutate(Settings.ActiveMovePolygonMutationRate))
            {
                dnaDrawing.MovePolygon();
                dnaDrawing.SetDirty();
            }

            foreach (DnaPolygon polygon in dnaDrawing.Polygons)
            {
                polygon.Mutate(dnaDrawing);
            }
        }

        public static void MovePolygon(this DnaDrawing dnaDrawing)
        {
            if (dnaDrawing.Polygons.Count < 1)
                return;

            int index = Tools.GetRandomNumber(0, dnaDrawing.Polygons.Count);
            DnaPolygon poly = dnaDrawing.Polygons[index];
            dnaDrawing.Polygons.RemoveAt(index);
            index = Tools.GetRandomNumber(0, dnaDrawing.Polygons.Count);
            dnaDrawing.Polygons.Insert(index, poly);
        }

        public static void RemovePolygon(this DnaDrawing dnaDrawing)
        {
            if (dnaDrawing.Polygons.Count > Settings.ActivePolygonsMin)
            {
                int index = Tools.GetRandomNumber(0, dnaDrawing.Polygons.Count);
                dnaDrawing.Polygons.RemoveAt(index);
            }
        }

        public static void AddPolygon(this DnaDrawing dnaDrawing)
        {
            if (dnaDrawing.Polygons.Count < Settings.ActivePolygonsMax)
            {
                var newPolygon = DnaPolygon.GetRandom();
                int index = Tools.GetRandomNumber(0, dnaDrawing.Polygons.Count);

                dnaDrawing.Polygons.Insert(index, newPolygon);
            }
        }

    }
}