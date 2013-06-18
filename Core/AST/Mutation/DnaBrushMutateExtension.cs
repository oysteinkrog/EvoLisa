using GenArt.Core.Classes;

namespace GenArt.Core.AST.Mutation
{
    public static class DnaBrushMutateExtension 
    {
        public static void Mutate(this DnaBrush dnaBrush, DnaDrawing dnaDrawing)
        {
            if (Tools.WillMutate(Settings.ActiveRedMutationRate))
            {
                dnaBrush.Red = Tools.GetRandomNumber(Settings.ActiveRedRangeMin, Settings.ActiveRedRangeMax);
                dnaDrawing.SetDirty();
            }

            if (Tools.WillMutate(Settings.ActiveGreenMutationRate))
            {
                dnaBrush.Green = Tools.GetRandomNumber(Settings.ActiveGreenRangeMin, Settings.ActiveGreenRangeMax);
                dnaDrawing.SetDirty();
            }

            if (Tools.WillMutate(Settings.ActiveBlueMutationRate))
            {
                dnaBrush.Blue = Tools.GetRandomNumber(Settings.ActiveBlueRangeMin, Settings.ActiveBlueRangeMax);
                dnaDrawing.SetDirty();
            }

            if (Tools.WillMutate(Settings.ActiveAlphaMutationRate))
            {
                dnaBrush.Alpha = Tools.GetRandomNumber(Settings.ActiveAlphaRangeMin, Settings.ActiveAlphaRangeMax);
                dnaDrawing.SetDirty();
            }
        }
    }
}