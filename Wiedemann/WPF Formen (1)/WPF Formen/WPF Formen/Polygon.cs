using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;

namespace WPF_Formen
{
    internal class Polygon : VieleckBasis
    {
        public static readonly DependencyProperty AnzahlProperty =
            DependencyProperty.Register(
                "Anzahl",
                typeof(int),
                typeof(Polygon),
                new FrameworkPropertyMetadata(
                    3,
                    FrameworkPropertyMetadataOptions.AffectsRender |
                    FrameworkPropertyMetadataOptions.AffectsMeasure));

        public static readonly DependencyProperty RadiusProperty =
            DependencyProperty.Register(
                "Radius",
                typeof(double),
                typeof(Polygon),
                new FrameworkPropertyMetadata(
                    50.0,
                    FrameworkPropertyMetadataOptions.AffectsRender |
                    FrameworkPropertyMetadataOptions.AffectsMeasure));

        public int Anzahl
        {
            get => (int)GetValue(AnzahlProperty);
            set => SetValue(AnzahlProperty, value);
        }

        [TypeConverter(typeof(LengthConverter))]
        public double Radius
        {
            get => (double)GetValue(RadiusProperty);
            set => SetValue(RadiusProperty, value);
        }

        protected override PathFigure CreatePathFigure()
        {
            if (Anzahl < 3)
                return new PathFigure(); // kein gültiges Polygon

            Point[] punkte = BerechnePunkte(Anzahl, Radius);

            PathFigure figure = new PathFigure
            {
                StartPoint = punkte[0],
                IsClosed = true
            };

            for (int i = 1; i < punkte.Length; i++)
            {
                figure.Segments.Add(
                    new LineSegment(punkte[i], true));
            }

            return figure;
        }
    }
}
