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

namespace RatingControlNP
{
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:RatingControl"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:RatingControl;assembly=RatingControl"
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
    public class RatingControl : Control
    {

        public static readonly DependencyProperty RatingProperty = DependencyProperty.Register("Rating", typeof(int), typeof(RatingControl), new FrameworkPropertyMetadata(1, OnValueChanged));
        public static readonly DependencyProperty ShowNumberProperty = DependencyProperty.Register("ShowNumber", typeof(Visibility), typeof(RatingControl), new FrameworkPropertyMetadata(Visibility.Visible));
        public event EventHandler RatingChanged;

        static RatingControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(RatingControl), new FrameworkPropertyMetadata(typeof(RatingControl)));
        }

        public int Rating
        {
            get { return (int)base.GetValue(RatingProperty); }
            set { base.SetValue(RatingProperty, value); }
        }

        public Visibility ShowNumber
        {
            get { return (Visibility)base.GetValue(ShowNumberProperty); }
            set { base.SetValue(ShowNumberProperty, value); }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            RegisterStar("Star1");
            RegisterStar("Star2");
            RegisterStar("Star3");
            RegisterStar("Star4");
            RegisterStar("Star5");
            RegisterStar("UStar1");
            RegisterStar("UStar2");
            RegisterStar("UStar3");
            RegisterStar("UStar4");
            RegisterStar("UStar5");

            UpdateStars();
        }

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (RatingControl)d;
            control.UpdateStars();
        }


        private void UpdateStars()
        {
            SetStarVisibility("Star1", Rating >= 1);
            SetStarVisibility("Star2", Rating >= 2);
            SetStarVisibility("Star3", Rating >= 3);
            SetStarVisibility("Star4", Rating >= 4);
            SetStarVisibility("Star5", Rating >= 5);
        }

        private void SetStarVisibility(string name, bool visible)
        {
            if(GetTemplateChild(name) is UIElement star)
            {
                star.Visibility = visible ? Visibility.Visible : Visibility.Hidden;
            }

            if (GetTemplateChild("U" + name) is UIElement ustar)
            {
                ustar.Visibility = visible ? Visibility.Hidden : Visibility.Visible;
            }
        }

        public void changeTo(int newRating)
        {
            Rating = newRating;
            RatingChanged?.Invoke(this, new EventArgs());
        }

        private void RegisterStar(string name)
        {
            if(GetTemplateChild(name) is FrameworkElement star)
            {
                star.PreviewMouseLeftButtonDown += Star_Click;
            }
        }

        private void Star_Click(object sender, MouseButtonEventArgs e)
        {
            if(sender is FrameworkElement star)
            {
                switch(star.Name)
                {
                    case "Star1": changeTo(1); break;
                    case "UStar1": changeTo(1); break;
                    case "Star2": changeTo(2); break;
                    case "UStar2": changeTo(2); break;
                    case "Star3": changeTo(3); break;
                    case "UStar3": changeTo(3); break;
                    case "Star4": changeTo(4); break;
                    case "UStar4": changeTo(4); break;
                    case "Star5": changeTo(5); break;
                    case "UStar5": changeTo(5); break;
                }
            }
        }
    }
}