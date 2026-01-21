using System.Windows;
using System.Windows.Media;

namespace WPF_Formen
{
    internal class EllipseForm : Basis
    {
        public double RadiusX { get; set; } = 60;
        public double RadiusY { get; set; } = 40;

        protected override PathFigure CreatePathFigure()
        {
            Point start = new Point(X1 + RadiusX, Y1);

            ArcSegment arc = new ArcSegment
            {
                Point = start,
                Size = new Size(RadiusX, RadiusY),
                IsLargeArc = true,
                SweepDirection = SweepDirection.Clockwise
            };

            PathFigure fig = new PathFigure
            {
                StartPoint = start,
                IsClosed = true
            };

            fig.Segments.Add(arc);
            return fig;
        }
    }
}
