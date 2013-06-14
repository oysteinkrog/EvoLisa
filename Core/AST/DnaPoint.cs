using System;
using GenArt.Classes;

namespace GenArt.AST
{
    [Serializable]
    public class DnaPoint
    {
        public int X { get; set; }
        public int Y { get; set; }

        public void Init()
        {
            X = Tools.GetRandomNumber(0, Tools.MaxWidth);
            Y = Tools.GetRandomNumber(0, Tools.MaxHeight);
        }

        public DnaPoint Clone()
        {
            return new DnaPoint
                       {
                           X = X,
                           Y = Y,
                       };
        }

        public void Mutate(DnaDrawing drawing)
        {
            if (Tools.WillMutate(Settings.ActiveMovePointMaxMutationRate))
            {
                X = Tools.GetRandomNumber(0, Tools.MaxWidth);
                Y = Tools.GetRandomNumber(0, Tools.MaxHeight);
                drawing.SetDirty();
            }

            if (Tools.WillMutate(Settings.ActiveMovePointMidMutationRate))
            {
                X =
                    Math.Min(
                        Math.Max(0,
                                 X +
                                 Tools.GetRandomNumber(-Settings.ActiveMovePointRangeMid,
                                                       Settings.ActiveMovePointRangeMid)), Tools.MaxWidth);
                Y =
                    Math.Min(
                        Math.Max(0,
                                 Y +
                                 Tools.GetRandomNumber(-Settings.ActiveMovePointRangeMid,
                                                       Settings.ActiveMovePointRangeMid)), Tools.MaxHeight);
                drawing.SetDirty();
            }

            if (Tools.WillMutate(Settings.ActiveMovePointMinMutationRate))
            {
                X =
                    Math.Min(
                        Math.Max(0,
                                 X +
                                 Tools.GetRandomNumber(-Settings.ActiveMovePointRangeMin,
                                                       Settings.ActiveMovePointRangeMin)), Tools.MaxWidth);
                Y =
                    Math.Min(
                        Math.Max(0,
                                 Y +
                                 Tools.GetRandomNumber(-Settings.ActiveMovePointRangeMin,
                                                       Settings.ActiveMovePointRangeMin)), Tools.MaxHeight);
                drawing.SetDirty();
            }
        }
    }
}