using System;
using GenArt.Classes;

namespace GenArt.AST
{
    [Serializable]
    public class DnaPoint
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        public DnaPoint(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static DnaPoint GetRandom()
        {
            return new DnaPoint(Tools.GetRandomNumber(0, Tools.MaxWidth), Tools.GetRandomNumber(0, Tools.MaxHeight));
        }

        public DnaPoint Clone()
        {
            return new DnaPoint(X, Y);
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
                X = MathUtils.Clamp(X + Tools.GetRandomNumber(-Settings.ActiveMovePointRangeMid, Settings.ActiveMovePointRangeMid), 0, Tools.MaxWidth);
                Y = MathUtils.Clamp(Y + Tools.GetRandomNumber(-Settings.ActiveMovePointRangeMid, Settings.ActiveMovePointRangeMid), 0, Tools.MaxHeight);
                drawing.SetDirty();
            }

            if (Tools.WillMutate(Settings.ActiveMovePointMinMutationRate))
            {
                X = MathUtils.Clamp(X + Tools.GetRandomNumber(-Settings.ActiveMovePointRangeMin, Settings.ActiveMovePointRangeMin), 0, Tools.MaxWidth);
                Y = MathUtils.Clamp(Y + Tools.GetRandomNumber(-Settings.ActiveMovePointRangeMin, Settings.ActiveMovePointRangeMin), 0, Tools.MaxHeight);

                drawing.SetDirty();
            }
        }
    }
}