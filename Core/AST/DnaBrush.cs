using GenArt.Classes;
using System;

namespace GenArt.AST
{
    [Serializable]
    public class DnaBrush
    {
        public int Red { get; private set; }
        public int Green { get; private set; }
        public int Blue { get; private set; }
        public int Alpha { get; private set; }

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

        public void Mutate(DnaDrawing drawing)
        {
            if (Tools.WillMutate(Settings.ActiveRedMutationRate))
            {
                Red = Tools.GetRandomNumber(Settings.ActiveRedRangeMin, Settings.ActiveRedRangeMax);
                drawing.SetDirty();
            }

            if (Tools.WillMutate(Settings.ActiveGreenMutationRate))
            {
                Green = Tools.GetRandomNumber(Settings.ActiveGreenRangeMin, Settings.ActiveGreenRangeMax);
                drawing.SetDirty();
            }

            if (Tools.WillMutate(Settings.ActiveBlueMutationRate))
            {
                Blue = Tools.GetRandomNumber(Settings.ActiveBlueRangeMin, Settings.ActiveBlueRangeMax);
                drawing.SetDirty();
            }

            if (Tools.WillMutate(Settings.ActiveAlphaMutationRate))
            {
                Alpha = Tools.GetRandomNumber(Settings.ActiveAlphaRangeMin, Settings.ActiveAlphaRangeMax);
                drawing.SetDirty();
            }
        }
    }
}