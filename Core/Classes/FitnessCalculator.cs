using System.Drawing;
using System.Drawing.Imaging;
using GenArt.AST;

namespace GenArt.Classes
{
    public static class FitnessCalculator
    {
        public static double GetDrawingFitness(DnaDrawing newDrawing, Color[,] sourceColors)
        {
            double error = 0;

            using (var b = new Bitmap(Tools.MaxWidth, Tools.MaxHeight, PixelFormat.Format24bppRgb))
            using (Graphics g = Graphics.FromImage(b))
            {
                Renderer.Render(newDrawing, g, 1);

                BitmapData bmd1 = b.LockBits(new Rectangle(0, 0, Tools.MaxWidth, Tools.MaxHeight), ImageLockMode.ReadOnly,
                                             PixelFormat.Format24bppRgb);


                for (int y = 0; y < Tools.MaxHeight; y++)
                {
                    for (int x = 0; x < Tools.MaxWidth; x++)
                    {
                        Color c1 = GetPixel(bmd1, x, y);
                        Color c2 = sourceColors[x, y];

                        double pixelError = GetColorFitness(c1, c2);
                        error += pixelError;
                    }
                }

                b.UnlockBits(bmd1);
            }

            return error;
        }

        private static unsafe Color GetPixel(BitmapData bmd, int x, int y)
        {
            byte* p = (byte*) bmd.Scan0 + y*bmd.Stride + 3*x;
            return Color.FromArgb(p[2], p[1], p[0]);
        }

        private static double GetColorFitness(Color c1, Color c2)
        {
            double r = c1.R - c2.R;
            double g = c1.G - c2.G;
            double b = c1.B - c2.B;

            return r*r + g*g + b*b;
        }
    }
}