using System;
using System.Drawing;
using System.Drawing.Imaging;
using GenArt.Core.AST;

namespace GenArt.Core.Classes
{
    public class FitnessCalculator
    {
        private readonly Color[,] _sourceColors;

        public FitnessCalculator(Bitmap sourceImage)
        {
            _sourceColors = GenerateSourceColorMatrix(sourceImage);
        }

        //covnerts the source image to a Color[,] for faster lookup
        private static Color[,] GenerateSourceColorMatrix(Bitmap sourceImage)
        {
            if (sourceImage == null)
                throw new NotSupportedException("A source image of Bitmap format must be provided");

            var sourceColors = new Color[sourceImage.Width, sourceImage.Height];

            for (int y = 0; y < sourceImage.Height; y++)
            {
                for (int x = 0; x < sourceImage.Width; x++)
                {
                    Color c = sourceImage.GetPixel(x, y);
                    sourceColors[x, y] = c;
                }
            }
            return sourceColors;
        }

        public double GetDrawingFitness(DnaDrawing newDrawing)
        {
            double error = 0;

            using (var b = new Bitmap(newDrawing.Width, newDrawing.Height, PixelFormat.Format24bppRgb))
            using (Graphics g = Graphics.FromImage(b))
            {
                Renderer.Render(newDrawing, g, 1);

                BitmapData bmd1 = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadOnly,
                                             PixelFormat.Format24bppRgb);


                for (int y = 0; y < b.Height; y++)
                {
                    for (int x = 0; x < b.Width; x++)
                    {
                        Color c1 = GetPixel(bmd1, x, y);
                        Color c2 = _sourceColors[x, y];

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