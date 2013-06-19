using System;
using GenArt.Core.Classes;

namespace GenArt.Core.AST
{
    [Serializable]
    public class DnaPoint
    {
        public int X { get; internal set; }
        public int Y { get; internal set; }

        public DnaPoint(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static DnaPoint GetRandom(int maxX, int maxY)
        {
            return new DnaPoint(Tools.GetRandomNumber(0, maxX), Tools.GetRandomNumber(0, maxY));
        }

        public DnaPoint Clone()
        {
            return new DnaPoint(X, Y);
        }
    }
}