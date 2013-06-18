using System;
using GenArt.Classes;

namespace GenArt.AST
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

        public static DnaPoint GetRandom()
        {
            return new DnaPoint(Tools.GetRandomNumber(0, Tools.MaxWidth), Tools.GetRandomNumber(0, Tools.MaxHeight));
        }

        public DnaPoint Clone()
        {
            return new DnaPoint(X, Y);
        }
    }
}