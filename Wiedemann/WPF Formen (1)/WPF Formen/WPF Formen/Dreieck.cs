using System.Windows;
using System.Windows.Media;

namespace WPF_Formen
{
    internal class Dreieck : VieleckBasis
    {
        public double Radius { get; set; } = 40;

        protected override PathFigure CreatePathFigure()
        {
            Point[] p = BerechnePunkte(3, Radius);

            PathFigure fig = new PathFigure
            {
                StartPoint = p[0],
                IsClosed = true
            };

            fig.Segments.Add(new LineSegment(p[1], true));
            fig.Segments.Add(new LineSegment(p[2], true));

            return fig;
        }
    }
}
