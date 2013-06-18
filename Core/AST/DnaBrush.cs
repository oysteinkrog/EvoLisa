using System;
using GenArt.Core.Classes;

namespace GenArt.Core.AST
{
    [Serializable]
    public class DnaBrush
    {
        public int Red { get; internal set; }
        public int Green { get; internal set; }
        public int Blue { get; internal set; }
        public int Alpha { get; internal set; }

        public DnaBrush(int red, int green, int blue, int alpha)
        {
            Red = red;
            Green = green;
            Blue = blue;
            Alpha = alpha;
        }

        public static DnaBrush GetRandom()
        {
            return new DnaBrush(
                Tools.GetRandomNumber(0, 255),
                Tools.GetRandomNumber(0, 255),
                Tools.GetRandomNumber(0, 255), 
                Tools.GetRandomNumber(10, 60));
        }

        public DnaBrush Clone()
        {
            return new DnaBrush(Red, Green, Blue, Alpha);
        }
    }
}