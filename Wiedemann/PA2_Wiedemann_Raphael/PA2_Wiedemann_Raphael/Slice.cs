using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace PA2_Wiedemann_Raphael
{
    internal class Slice : Basis
    {
        public static readonly DependencyProperty RadiusProperty = DependencyProperty.Register("Radius", typeof(double), typeof(Slice), new FrameworkPropertyMetadata( 20.0, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));
        public static readonly DependencyProperty AngleProperty = DependencyProperty.Register("Angle", typeof(double), typeof(Slice), new FrameworkPropertyMetadata(20.0, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));

        public double Radius
        {
            get => (double)GetValue(RadiusProperty);
            set => SetValue(RadiusProperty, value);
        }

        public double Angle
        {
            get => (double)GetValue(AngleProperty);
            set => SetValue(AngleProperty, value);
        }


        protected override PathFigure CreatePathFigure()
        {
            double angle = Math.Max(0, Math.Min(Angle, 90));
            double angleRad = angle * Math.PI / 180.0;

            Point center = new Point(X1, Y1);

            Point start = new Point(X1 + Radius, Y1);

            Point end = new Point(
                X1 + Radius * Math.Cos(angleRad),
                Y1 - Radius * Math.Sin(angleRad)
                );

            PathFigure figure = new PathFigure
            {
                StartPoint = start,
                IsClosed = true,
            };

            figure.Segments.Add(new LineSegment(start, true));

            // Erster Halbkreis
            figure.Segments.Add(new ArcSegment
            {
                Point = end,
                Size = new Size(Radius, Radius),
                IsLargeArc = false,
                SweepDirection = SweepDirection.Counterclockwise,
                IsStroked = true
            }); 

            // Zweiter Halbkreis

            figure.Segments.Add(new LineSegment(center, true));
            return figure;

        }
    }
}
