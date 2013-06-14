using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

using GenArt.AST;

namespace GenArt.Classes
{
    public static class Renderer
    {
        //Render a Drawing
        public static void Render(DnaDrawing drawing,Graphics g,int scale)
        {
            g.Clear(Color.Black);

            foreach (DnaPolygon polygon in drawing.Polygons)
                Render(polygon, g, scale);
        }

        //Render a polygon
        private static void Render(DnaPolygon polygon, Graphics g, int scale)
        {
            using (Brush brush = GetGdiBrush(polygon.Brush))
            {
                Point[] points = GetGdiPoints(polygon.Points, scale);
                g.FillPolygon(brush,points);
            }
        }

        //Convert a list of DnaPoint to a list of System.Drawing.Point's
        private static Point[] GetGdiPoints(IList<DnaPoint> points,int scale)
        {
            Point[] pts = new Point[points.Count];
            int i = 0;
            foreach (DnaPoint pt in points)
            {
                pts[i++] = new Point(pt.X * scale, pt.Y * scale);
            }
            return pts;
        }

        //Convert a DnaBrush to a System.Drawing.Brush
        private static Brush GetGdiBrush(DnaBrush b)
        {
            return new SolidBrush(Color.FromArgb(b.Alpha, b.Red, b.Green, b.Blue));
        }

        
    }
}
