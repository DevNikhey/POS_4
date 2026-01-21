using System.Windows;
using System.Windows.Media;

namespace WPF_Formen
{
    internal class Trapez : Basis
    {
        public double BreiteOben { get; set; } = 40;
        public double BreiteUnten { get; set; } = 80;
        public double Höhe { get; set; } = 50;

        protected override PathFigure CreatePathFigure()
        {
            Point p1 = new Point(X1 - BreiteOben / 2, Y1 - Höhe / 2);
            Point p2 = new Point(X1 + BreiteOben / 2, Y1 - Höhe / 2);
            Point p3 = new Point(X1 + BreiteUnten / 2, Y1 + Höhe / 2);
            Point p4 = new Point(X1 - BreiteUnten / 2, Y1 + Höhe / 2);

            PathFigure fig = new PathFigure
            {
                StartPoint = p1,
                IsClosed = true
            };

            fig.Segments.Add(new LineSegment(p2, true));
            fig.Segments.Add(new LineSegment(p3, true));
            fig.Segments.Add(new LineSegment(p4, true));

            return fig;
        }
    }
}
