using System.Windows;
using System.Windows.Media;

namespace WPF_Formen
{
    internal class Raute : Basis
    {
        public double DiagonaleHorizontal { get; set; } = 80;
        public double DiagonaleVertikal { get; set; } = 50;

        protected override PathFigure CreatePathFigure()
        {
            Point p1 = new Point(X1, Y1 - DiagonaleVertikal / 2);
            Point p2 = new Point(X1 + DiagonaleHorizontal / 2, Y1);
            Point p3 = new Point(X1, Y1 + DiagonaleVertikal / 2);
            Point p4 = new Point(X1 - DiagonaleHorizontal / 2, Y1);

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
