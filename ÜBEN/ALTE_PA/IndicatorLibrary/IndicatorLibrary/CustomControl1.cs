using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IndicatorLibrary
{
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:IndicatorLibrary"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:IndicatorLibrary;assembly=IndicatorLibrary"
    ///
    /// You will also need to add a project reference from the project where the XAML file lives
    /// to this project and Rebuild to avoid compilation errors:
    ///
    ///     Right click on the target project in the Solution Explorer and
    ///     "Add Reference"->"Projects"->[Select this project]
    ///
    ///
    /// Step 2)
    /// Go ahead and use your control in the XAML file.
    ///
    ///     <MyNamespace:CustomControl1/>
    ///
    /// </summary>
    public class Indicator : Control
    {
        static Indicator()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Indicator),
                new FrameworkPropertyMetadata(typeof(Indicator)));
        }

        public double Minimum
        {
            get => (double)GetValue(MinimumProperty);
            set => SetValue(MinimumProperty, value);
        }
        public static readonly DependencyProperty MinimumProperty =
            DependencyProperty.Register(
                nameof(Minimum), typeof(double), typeof(Indicator),
                new PropertyMetadata(0.0, OnValuePropertyChanged));



        public double Maximum
        {
            get => (double)GetValue(MaximumProperty);
            set => SetValue(MaximumProperty, value);
        }
        public static readonly DependencyProperty MaximumProperty =
            DependencyProperty.Register(
                nameof(Maximum), typeof(double), typeof(Indicator),
                new PropertyMetadata(100.0, OnValuePropertyChanged));



        public double Value
        {
            get => (double)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(
                nameof(Value), typeof(double), typeof(Indicator),
                new PropertyMetadata(0.0, OnValuePropertyChanged));



        // --------- ANGLE ----------
        public double Angle
        {
            get => (double)GetValue(AngleProperty);
            private set => SetValue(AnglePropertyKey, value);
        }
        private static readonly DependencyPropertyKey AnglePropertyKey =
            DependencyProperty.RegisterReadOnly(
                nameof(Angle), typeof(double), typeof(Indicator),
                new PropertyMetadata(0.0));

        public static readonly DependencyProperty AngleProperty =
            AnglePropertyKey.DependencyProperty;


        // --------- CALLBACK ----------
        private static void OnValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (Indicator)d;
            control.UpdateAngle();
        }

        private void UpdateAngle()
        {
            if (Maximum <= Minimum)
                return;

            double ratio = (Value - Minimum) / (Maximum - Minimum);

            ratio = Math.Max(0, Math.Min(1, ratio)); // clamp

            // Winkelbereich 0° – 287°
            Angle = 287 * ratio;
        }
    }

}