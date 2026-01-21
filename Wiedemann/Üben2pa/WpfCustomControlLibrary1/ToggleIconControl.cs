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
using System.Xml.Linq;

namespace WpfCustomControlLibrary1
{
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:WpfCustomControlLibrary1"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:WpfCustomControlLibrary1;assembly=WpfCustomControlLibrary1"
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
    public class ToggleIconControl : Control
    {

        public static readonly DependencyProperty IsOnProperty = DependencyProperty.Register("IsOn", typeof(bool), typeof(ToggleIconControl), new FrameworkPropertyMetadata(false));
        public static readonly DependencyProperty TextOnProperty = DependencyProperty.Register("TextOn", typeof(string), typeof(ToggleIconControl), new FrameworkPropertyMetadata("IsOn"));
        public static readonly DependencyProperty TextOffProperty = DependencyProperty.Register("TextOff", typeof(string), typeof(ToggleIconControl), new FrameworkPropertyMetadata("IsOff"));

        public event EventHandler Toggled;

        public bool IsOn
        {
            get => (bool)GetValue(IsOnProperty);
            set => SetValue(IsOnProperty, value);
        }

        public string TextOn
        {
            get => (string)GetValue(TextOnProperty);
            set => SetValue(TextOnProperty, value);
        }

        public string TextOff
        {
            get => (string)GetValue(TextOffProperty);
            set => SetValue(TextOffProperty, value);
        }

        static ToggleIconControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ToggleIconControl), new FrameworkPropertyMetadata(typeof(ToggleIconControl)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            RegisterClick();
        }

        private void RegisterClick()
        {
            if (GetTemplateChild("Icon") is Ellipse icon)
            {
                icon.PreviewMouseLeftButtonDown += StartClick;
            }
        }

        private void StartClick(object sender, MouseButtonEventArgs e)
        {
            IsOn = !IsOn;

            // ✅ Event auslösen
            Toggled?.Invoke(this, EventArgs.Empty);

           
        }
    }
}