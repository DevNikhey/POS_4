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

namespace LoginRegisterLibrary
{
    public class SpeedoControl : Control
    {
        static SpeedoControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SpeedoControl),
                new FrameworkPropertyMetadata(typeof(SpeedoControl)));
        }

        // Public Properties
        public int Min
        {
            get { return (int)GetValue(MinProperty); }
            set { SetValue(MinProperty, value); }
        }

        public static readonly DependencyProperty MinProperty =
            DependencyProperty.Register(nameof(Min), typeof(int), typeof(SpeedoControl));

        public int Max
        {
            get { return (int)GetValue(MaxProperty); }
            set { SetValue(MaxProperty, value); }
        }

        public static readonly DependencyProperty MaxProperty =
            DependencyProperty.Register(nameof(Max), typeof(int), typeof(SpeedoControl));

        public int Value
        {
            get { return (int)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(
                nameof(Value),
                typeof(int),
                typeof(SpeedoControl),
                new FrameworkPropertyMetadata(
                    0,
                    new PropertyChangedCallback(OnValueChanged)
                )
            );

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
        }

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // cast the sender back to your control
            SpeedoControl control = (SpeedoControl)d;

            int oldValue = (int)e.OldValue;
            int newValue = (int)e.NewValue;
        }
    }
}