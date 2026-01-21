using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Indicator
{
    /// <summary>
    /// Führen Sie die Schritte 1a oder 1b und anschließend Schritt 2 aus, um dieses benutzerdefinierte Steuerelement in einer XAML-Datei zu verwenden.
    ///
    /// Schritt 1a) Verwenden des benutzerdefinierten Steuerelements in einer XAML-Datei, die im aktuellen Projekt vorhanden ist.
    /// Fügen Sie dieses XmlNamespace-Attribut dem Stammelement der Markupdatei 
    /// an der Stelle hinzu, an der es verwendet werden soll:
    ///
    ///     xmlns:MyNamespace="clr-namespace:Indicator"
    ///
    ///
    /// Schritt 1b) Verwenden des benutzerdefinierten Steuerelements in einer XAML-Datei, die in einem anderen Projekt vorhanden ist.
    /// Fügen Sie dieses XmlNamespace-Attribut dem Stammelement der Markupdatei 
    /// an der Stelle hinzu, an der es verwendet werden soll:
    ///
    ///     xmlns:MyNamespace="clr-namespace:Indicator;assembly=Indicator"
    ///
    /// Darüber hinaus müssen Sie von dem Projekt, das die XAML-Datei enthält, einen Projektverweis
    /// zu diesem Projekt hinzufügen und das Projekt neu erstellen, um Kompilierungsfehler zu vermeiden:
    ///
    ///     Klicken Sie im Projektmappen-Explorer mit der rechten Maustaste auf das Zielprojekt und anschließend auf
    ///     "Verweis hinzufügen"->"Projekte"->[Dieses Projekt auswählen]
    ///
    ///
    /// Schritt 2)
    /// Fahren Sie fort, und verwenden Sie das Steuerelement in der XAML-Datei.
    ///
    ///     <MyNamespace:CustomControl1/>
    ///
    /// </summary>
    public class CustomControl1 : Control
    {
        public static readonly DependencyProperty DependencyMinimum = DependencyProperty.Register("Minimum", typeof(double), typeof(CustomControl1), new FrameworkPropertyMetadata(0.0, OnValueChanged));
        public static readonly DependencyProperty DependencyMaximum = DependencyProperty.Register("Maximum", typeof(double), typeof(CustomControl1), new FrameworkPropertyMetadata(0.0, OnValueChanged));
        public static readonly DependencyProperty DependencyValue = DependencyProperty.Register("Value", typeof(double), typeof(CustomControl1), new FrameworkPropertyMetadata(0.0, OnValueChanged));
        public static readonly DependencyProperty DependencyAngle = DependencyProperty.Register("Angle", typeof(double), typeof(CustomControl1), new FrameworkPropertyMetadata(0.0));

        static CustomControl1()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomControl1), new FrameworkPropertyMetadata(typeof(CustomControl1)));
        }

        public double Minimum
        {
            get { return (double)base.GetValue(DependencyMinimum); }
            set { base.SetValue(DependencyMinimum, value); }
        }

        public double Maximum
        {
            get { return (double)base.GetValue(DependencyMaximum); }
            set { base.SetValue(DependencyMaximum, value); }
        }

        public double Value
        {
            get { return (double)base.GetValue(DependencyValue); }
            set { base.SetValue(DependencyValue, value); }
        }

        public double Angle
        {
            get { return (double)base.GetValue(DependencyAngle); }
            set { base.SetValue(DependencyAngle, value); }
        }

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if(d is CustomControl1 control)
            {
                control.UpdateAngle();
            }
        }

        private void UpdateAngle()
        {
            if(Maximum <= Minimum)
            {
                Angle = 0;
                return;
            }

            double range = Maximum - Minimum;
            double normalizedValue = (Value- Minimum) / range;

            normalizedValue = Math.Max(0, Math.Min(1, normalizedValue));

            Angle = normalizedValue * 287.0;
        }

    }
}
