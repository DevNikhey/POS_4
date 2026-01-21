using System;
using System.Windows;
using System.Windows.Media;

namespace WPF_Formen
{
    internal class Stern : Basis
    {
        public int Zacken { get; set; } = 5;
        public double RadiusAussen { get; set; } = 50;
        public double RadiusInnen { get; set; } = 25;

        protected override PathFigure CreatePathFigure()
        {
            int punkte = Zacken * 2;
            Point[] p = new Point[punkte];

            double winkel = -Math.PI / 2;
            double schritt = Math.PI / Zacken;

            for (int i = 0; i < punkte; i++)
            {
                double r = (i % 2 == 0) ? RadiusAussen : RadiusInnen;
                p[i] = new Point(
                    X1 + r * Math.Cos(winkel),
                    Y1 + r * Math.Sin(winkel));
                winkel += schritt;
            }

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
