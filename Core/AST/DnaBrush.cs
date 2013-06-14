using GenArt.Classes;
using System;

namespace GenArt.AST
{
    [Serializable]
    public class DnaBrush
    {
        public int Red { get; set; }
        public int Green { get; set; }
        public int Blue { get; set; }
        public int Alpha { get; set; }

        public void Init()
        {
            Red = Tools.GetRandomNumber(0, 255);
            Green = Tools.GetRandomNumber(0, 255);
            Blue = Tools.GetRandomNumber(0, 255);
            Alpha = Tools.GetRandomNumber(10, 60);
        }

        public DnaBrush Clone()
        {
            return new DnaBrush
                       {
                           Alpha = Alpha,
                           Blue = Blue,
                           Green = Green,
                           Red = Red,
                       };
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