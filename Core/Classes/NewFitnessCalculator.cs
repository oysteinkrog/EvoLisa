using System.Runtime.InteropServices;
using GenArt.Core.AST;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using GenArt.Core.AST;

namespace GenArt.Core.Classes
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Pixel
    {
        public byte B;
        public byte G;
        public byte R;
        public byte A;
    }

    /// <summary>
    /// Optimized fitness generator for EvoLisa copied from
    /// http://danbystrom.se/2008/12/14/improving-performance/
    /// </summary>
    public class NewFitnessCalculator : IDisposable
    {
        private Bitmap _bmp;
        private Graphics _g;
        private Pixel[] _sourceImagePixels;

        public NewFitnessCalculator(Bitmap sourceBitmap)
        {
            _bmp = new Bitmap(sourceBitmap.Width, sourceBitmap.Height);
            _g = Graphics.FromImage(_bmp);
            _sourceImagePixels = SetupSourceColorMatrix(sourceBitmap);
        }

        public void Dispose()
        {
            _g.Dispose();
            _bmp.Dispose();
        }

        private static Pixel[] SetupSourceColorMatrix(Bitmap sourceImage)
        {
            if (sourceImage == null)
                throw new NotSupportedException("A source image of Bitmap format must be provided");

            BitmapData bd = sourceImage.LockBits(new Rectangle(0, 0, sourceImage.Width, sourceImage.Height),
                ImageLockMode.ReadOnly,
                PixelFormat.Format32bppArgb);

            Pixel[] sourcePixels = new Pixel[sourceImage.Width * sourceImage.Height];
            unsafe
            {
                fixed (Pixel* psourcePixels = sourcePixels)
                {
                    Pixel* pSrc = (Pixel*)bd.Scan0.ToPointer();
                    Pixel* pDst = psourcePixels;
                    for (int i = sourcePixels.Length; i > 0; i--)
                        *(pDst++) = *(pSrc++);
                }
            }
            sourceImage.UnlockBits(bd);

            return sourcePixels;
        }

        public double GetDrawingFitness(DnaDrawing newDrawing)
        {
            double error = 0;

            Renderer.Render(newDrawing, _g, 1);

            BitmapData bd = _bmp.LockBits(
                new Rectangle(0, 0, newDrawing.Width, newDrawing.Height),
                ImageLockMode.ReadOnly,
                PixelFormat.Format32bppArgb);

            unchecked
            {
                unsafe
                {
                    fixed (Pixel* psourcePixels = _sourceImagePixels)
                    {
                        Pixel* p1 = (Pixel*) bd.Scan0.ToPointer();
                        Pixel* p2 = psourcePixels;
                        for (int i = _sourceImagePixels.Length; i > 0; i--, p1++, p2++)
                        {
                            int r = p1->R - p2->R;
                            int g = p1->G - p2->G;
                            int b = p1->B - p2->B;
                            error += r*r + g*g + b*b;
                        }
                    }
                }
            }
            _bmp.UnlockBits(bd);

            return error;
        }

    }
}