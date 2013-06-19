using GenArt.Core.Classes;

namespace GenArt.Core.AST.Mutation
{
    public static class DnaPointMutateExtension
    {
        public static void Mutate(this DnaPoint dnaPoint, DnaDrawing dnaDrawing)
        {
            if (Tools.WillMutate(Settings.ActiveMovePointMaxMutationRate))
            {
                dnaPoint.X = Tools.GetRandomNumber(0, dnaDrawing.Width);
                dnaPoint.Y = Tools.GetRandomNumber(0, dnaDrawing.Height);
                dnaDrawing.SetDirty();
            }

            if (Tools.WillMutate(Settings.ActiveMovePointMidMutationRate))
            {
                dnaPoint.X =
                    MathUtils.Clamp(
                        dnaPoint.X +
                        Tools.GetRandomNumber(-Settings.ActiveMovePointRangeMid, Settings.ActiveMovePointRangeMid), 0,
                        dnaDrawing.Width);

                dnaPoint.Y =
                    MathUtils.Clamp(
                        dnaPoint.Y +
                        Tools.GetRandomNumber(-Settings.ActiveMovePointRangeMid, Settings.ActiveMovePointRangeMid), 0,
                        dnaDrawing.Height);

                dnaDrawing.SetDirty();
            }

            if (Tools.WillMutate(Settings.ActiveMovePointMinMutationRate))
            {
                dnaPoint.X =
                    MathUtils.Clamp(
                        dnaPoint.X +
                        Tools.GetRandomNumber(-Settings.ActiveMovePointRangeMin, Settings.ActiveMovePointRangeMin), 0,
                        dnaDrawing.Width);

                dnaPoint.Y =
                    MathUtils.Clamp(
                        dnaPoint.Y +
                        Tools.GetRandomNumber(-Settings.ActiveMovePointRangeMin, Settings.ActiveMovePointRangeMin), 0,
                        dnaDrawing.Height);

                dnaDrawing.SetDirty();
            }
        }
    }
}