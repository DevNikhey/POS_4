using System.Windows;
using System.Windows.Media;

namespace WPF_Formen
{
    internal class Kreis : Basis
    {
        public double Radius { get; set; } = 40;

        protected override PathFigure CreatePathFigure()
        {
            // Startpunkt rechts vom Mittelpunkt
            Point start = new Point(X1 + Radius, Y1);

            // Erster Halbkreis
            ArcSegment arc1 = new ArcSegment
            {
                Point = new Point(X1 - Radius, Y1),
                Size = new Size(Radius, Radius),
                IsLargeArc = false,
                SweepDirection = SweepDirection.Clockwise,
                IsStroked = true
            };

            // Zweiter Halbkreis
            ArcSegment arc2 = new ArcSegment
            {
                Point = start,
                Size = new Size(Radius, Radius),
                IsLargeArc = false,
                SweepDirection = SweepDirection.Clockwise,
                IsStroked = true
            };

            PathFigure fig = new PathFigure
            {
                StartPoint = start,
                IsClosed = true
            };

            fig.Segments.Add(arc1);
            fig.Segments.Add(arc2);

            return fig;
        }
    }
}
