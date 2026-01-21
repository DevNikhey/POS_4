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

namespace Indicator
{
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:Indicator"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:Indicator;assembly=Indicator"
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
    public class StatusIndicatorControl : Control
    {

        public static readonly DependencyProperty StatusProperty = DependencyProperty.Register("Status", typeof(int), typeof(StatusIndicatorControl), new FrameworkPropertyMetadata(1, OnValueChanged));
        public static readonly DependencyProperty ShowTextProperty = DependencyProperty.Register("ShowText", typeof(Visibility), typeof(StatusIndicatorControl), new FrameworkPropertyMetadata(Visibility.Visible));
        public event EventHandler StatusChanged;


        static StatusIndicatorControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(StatusIndicatorControl), new FrameworkPropertyMetadata(typeof(StatusIndicatorControl)));
        }

        public int Status
        {
            get { return (int)base.GetValue(StatusProperty); }
            set { base.SetValue(StatusProperty, value); }
        }

        public Visibility ShowText
        {
            get { return (Visibility)base.GetValue(ShowTextProperty); }
            set { base.SetValue(ShowTextProperty, value); }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (GetTemplateChild("Button") is Button btn)
            {
                btn.Click += OnButtonClick;
            }

            UpdateStatus();
        }

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (StatusIndicatorControl)d;
            control.UpdateStatus();
            control.StatusChanged?.Invoke(control, EventArgs.Empty);
        }

        private void UpdateStatus()
        {
            if(GetTemplateChild("Kreis") is Ellipse kreis)
            {
                switch (Status)
                {
                    case 0:
                        kreis.Fill = Brushes.Green;
                        break;
                    case 1:
                        kreis.Fill = Brushes.Gold;
                        break;
                    case 2:
                        kreis.Fill = Brushes.Red;
                        break;
                }   
            }
        }

        private void OnButtonClick(object sender, RoutedEventArgs e)
        {
            Status = (Status + 1) % 3;
        }
    }
}