using System.Security.Principal;
using GenArt.Core.Classes;

namespace GenArt.Core.AST.Mutation
{
    public static class DnaDrawingMutateExtension
    {
        public static void Mutate(this DnaDrawing dnaDrawing)
        {
            if (Tools.WillMutate(Settings.ActiveAddPolygonMutationRate))
            {
                if (dnaDrawing.AddPolygon())
                {
                    dnaDrawing.SetDirty();
                }
            }

            if (Tools.WillMutate(Settings.ActiveRemovePolygonMutationRate))
            {
                if (dnaDrawing.RemovePolygon())
                {
                    dnaDrawing.SetDirty();
                }
            }

            if (Tools.WillMutate(Settings.ActiveMovePolygonMutationRate))
            {
                if (dnaDrawing.MovePolygon())
                {
                    dnaDrawing.SetDirty();
                }
            }

            foreach (DnaPolygon polygon in dnaDrawing.Polygons)
            {
                polygon.Mutate(dnaDrawing);
            }
        }

        public static bool MovePolygon(this DnaDrawing dnaDrawing)
        {
            if (dnaDrawing.Polygons.Count < 1)
                return false;

            int index = Tools.GetRandomNumber(0, dnaDrawing.Polygons.Count);
            DnaPolygon poly = dnaDrawing.Polygons[index];
            dnaDrawing.Polygons.RemoveAt(index);
            index = Tools.GetRandomNumber(0, dnaDrawing.Polygons.Count);
            dnaDrawing.Polygons.Insert(index, poly);
            return true;
        }

        public static bool RemovePolygon(this DnaDrawing dnaDrawing)
        {
            if (dnaDrawing.Polygons.Count > Settings.ActivePolygonsMin)
            {
                int index = Tools.GetRandomNumber(0, dnaDrawing.Polygons.Count);
                dnaDrawing.Polygons.RemoveAt(index);
                return true;
            }
            return false;
        }

        public static bool AddPolygon(this DnaDrawing dnaDrawing)
        {
            if (dnaDrawing.Polygons.Count < Settings.ActivePolygonsMax)
            {
                var newPolygon = DnaPolygon.GetRandom(dnaDrawing.Width, dnaDrawing.Height);
                int index = Tools.GetRandomNumber(0, dnaDrawing.Polygons.Count);

                dnaDrawing.Polygons.Insert(index, newPolygon);
                return true;
            }
            return false;
        }

    }
}