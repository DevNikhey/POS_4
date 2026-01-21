using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace WPF_Formen
{
    class Quadrat : Basis
    {
        public static readonly DependencyProperty SeitenlaengenProperty = DependencyProperty.Register("Seitenlaenge", typeof(Double), typeof(Rechteck), new FrameworkPropertyMetadata(50.0, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));

        [TypeConverter(typeof(LengthConverter))]
        public double Seitenlaenge
        {
            get { return (double)GetValue(SeitenlaengenProperty); }
            set { SetValue(SeitenlaengenProperty, value); }
        }

        /// <summary>
        /// Zeichnet ein Rechteck
        /// </summary>
        protected override PathFigure CreatePathFigure()
        {
            PathFigure figure = new PathFigure();
            figure.StartPoint = new Point(X1, Y1);
            figure.Segments.Add(new LineSegment(new Point(X1 + Seitenlaenge, Y1), true));
            figure.Segments.Add(new LineSegment(new Point(X1 + Seitenlaenge, Y1 + Seitenlaenge), true));
            figure.Segments.Add(new LineSegment(new Point(X1, Y1 + Seitenlaenge), true));
            figure.IsClosed = true;
            return figure;
        }
    }
}
