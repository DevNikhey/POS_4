using RatingControlNP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using WPF_Formen;

namespace PA2_Wiedemann_Raphael
{
    internal class Spirale : VieleckBasis
    {
        public static readonly DependencyProperty RadiusProperty =
            DependencyProperty.Register(
                "Radius",
                typeof(double),
                typeof(Spirale),
                new FrameworkPropertyMetadata(
                    20.0,
                    FrameworkPropertyMetadataOptions.AffectsRender |
                    FrameworkPropertyMetadataOptions.AffectsMeasure));

        public static readonly DependencyProperty SteigungProperty =
            DependencyProperty.Register(
                "Steigung",
                typeof(double),
                typeof(Spirale),
                new FrameworkPropertyMetadata(
                    5.0,
                    FrameworkPropertyMetadataOptions.AffectsRender |
                    FrameworkPropertyMetadataOptions.AffectsMeasure));

        public static readonly DependencyProperty UmdrehungenProperty =
            DependencyProperty.Register(
                "Umdrehungen",
                typeof(int),
                typeof(Spirale),
                new FrameworkPropertyMetadata(
                    5,
                    FrameworkPropertyMetadataOptions.AffectsRender |
                    FrameworkPropertyMetadataOptions.AffectsMeasure));

        public static readonly DependencyProperty EckenProperty =
            DependencyProperty.Register(
                "Ecken",
                typeof(int),
                typeof(Spirale),
                new FrameworkPropertyMetadata(
                    20,
                    FrameworkPropertyMetadataOptions.AffectsRender |
                    FrameworkPropertyMetadataOptions.AffectsMeasure));

        public double Radius
        {
            get => (double)GetValue(RadiusProperty);
            set => SetValue(RadiusProperty, value);
        }

        public double Steigung
        {
            get => (double)GetValue(SteigungProperty);
            set => SetValue(SteigungProperty, value);
        }

        public int Umdrehungen
        {
            get => (int)GetValue(UmdrehungenProperty);
            set => SetValue(UmdrehungenProperty, value);
        }

        public int Ecken
        {
            get => (int)GetValue(EckenProperty);
            set => SetValue(EckenProperty, value);
        }

        protected override PathFigure CreatePathFigure()
        {
            if (Ecken < 3 || Umdrehungen < 1)
                return new PathFigure();

            PathFigure figure = new PathFigure
            {
                IsClosed = false
            };

            int gesamtPunkte = Ecken * Umdrehungen;
            double winkelSchritt = 2 * Math.PI / Ecken;

            double winkel = 0;
            double radius = Radius;

            Point start = new Point(
                X1 + radius * Math.Cos(winkel),
                Y1 + radius * Math.Sin(winkel));

            figure.StartPoint = start;

            for (int i = 1; i < gesamtPunkte; i++)
            {
                winkel += winkelSchritt;
                radius += Steigung;

                Point p = new Point(
                    X1 + radius * Math.Cos(winkel),
                    Y1 + radius * Math.Sin(winkel));

                figure.Segments.Add(new LineSegment(p, true));
            }

            return figure;
        }
    }

}
