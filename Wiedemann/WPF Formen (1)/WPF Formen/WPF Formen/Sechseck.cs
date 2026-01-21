using System.Windows;
using System.Windows.Media;

namespace WPF_Formen
{
    internal class Sechseck : VieleckBasis
    {
        public double Radius { get; set; } = 40;

        protected override PathFigure CreatePathFigure()
        {
            Point[] p = BerechnePunkte(6, Radius);

            PathFigure fig = new PathFigure
            {
                StartPoint = p[0],
                IsClosed = true
            };

            for (int i = 1; i < p.Length; i++)
                fig.Segments.Add(new LineSegment(p[i], true));

            return fig;
        }
    }
}
