using System.Windows;
using System.Windows.Media;

namespace WPF_Formen
{
    internal class Parallelogramm : Basis
    {
        public double Breite { get; set; } = 80;
        public double Höhe { get; set; } = 50;
        public double Versatz { get; set; } = 20;

        protected override PathFigure CreatePathFigure()
        {
            Point p1 = new Point(X1 - Breite / 2 + Versatz, Y1 - Höhe / 2);
            Point p2 = new Point(X1 + Breite / 2 + Versatz, Y1 - Höhe / 2);
            Point p3 = new Point(X1 + Breite / 2, Y1 + Höhe / 2);
            Point p4 = new Point(X1 - Breite / 2, Y1 + Höhe / 2);

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
