using Microsoft.Win32;
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

namespace Library
{
    public class RatingControl: Control
    {
        static RatingControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(RatingControl), new FrameworkPropertyMetadata(typeof(RatingControl)));
        }

        public int Rating
        {
            get { return (int)GetValue(RatingProperty); }
            set { SetValue(RatingProperty, value); }
        }

        //TODO: implement min, max
        public static readonly DependencyProperty RatingProperty =
            DependencyProperty.Register(nameof(Rating), typeof(int), typeof(RatingControl), new FrameworkPropertyMetadata(
                    0,
                    new PropertyChangedCallback(OnValueChanged)
                ));

        public bool ShowNumber
        {
            get { return (bool)GetValue(ShowNumberProperty); }
            set { SetValue(ShowNumberProperty, value); }
        }

        public static readonly DependencyProperty ShowNumberProperty =
            DependencyProperty.Register(nameof(ShowNumber), typeof(bool), typeof(RatingControl),  new FrameworkPropertyMetadata(
                    true,
                    new PropertyChangedCallback(onShowChanged)
                ));

        // Events
        public event EventHandler RatingChanged;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            var btnLogin = GetTemplateChild("PART_ADD") as Button;
            var diplayLogin = GetTemplateChild("PART_DISPLAY") as Button;

            if (btnLogin != null) btnLogin.Click += (s, e) => OnTestButtonClick();
            if (diplayLogin != null) diplayLogin.Click += (s, e) => OnDisplayButtonClick();

            //INFO: Wollte das in eine FOR-Schleife packen, ging aber leider nicht und hatte nicht mehr zeit. Deshalb copy-paste code sry :(
            //INFO 2: Es sind nur left-clicks erlaubt, rechts klicks werden ignoriert, deshalb MouseLeftButtonDown
            for(int i = 1; i <= 5; i++)
            {
                int temp = i;
                var full1 = GetTemplateChild("PART_STARF_" + temp) as Image;
                var empty1 = GetTemplateChild("PART_STARE_" + temp) as Image;
                full1.MouseLeftButtonDown += (s, e) => { changeTo(temp); };
                empty1.MouseLeftButtonDown += (s, e) => changeTo(temp);
            }
        }

        // Helper function to reduce copy pasting
        private void changeTo(int TRating)
        {
            Rating = TRating;
            RatingChanged?.Invoke(this, EventArgs.Empty);
        }

        // Function only used for testing
        private void OnTestButtonClick()
        {
            if(Rating == 5)
            {
                Rating = 0;
            } else
            {
                Rating++;
            }
        }

        // Function used to hide/show to textbox
        private void OnDisplayButtonClick()
        {
            ShowNumber = !ShowNumber;
        }

        private static void onShowChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            RatingControl control = (RatingControl)d;

            bool oldValue = (bool)e.OldValue;
            bool newValue = (bool)e.NewValue;
                
            if (newValue == oldValue)
            {
                return;
            }


            var text = control.GetTemplateChild("PART_SHOWRATING") as TextBlock;

            if (newValue) {
                text.Visibility = Visibility.Visible;
            } else
            {
                text.Visibility = Visibility.Hidden;
            }
        }
        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // cast the sender back to your control
            RatingControl control = (RatingControl)d;

            int oldValue = (int)e.OldValue;
            int newValue = (int)e.NewValue;

            if (newValue == oldValue)
            {
                return;
            }

            changeControl(control, 1, newValue >= 1);
            changeControl(control, 2, newValue >= 2);
            changeControl(control, 3, newValue >= 3);
            changeControl(control, 4, newValue >= 4);
            changeControl(control, 5, newValue >= 5);
        }

        // Helper function to be faster and reduce copy-pasted code
        private static void changeControl(RatingControl control, int starId, bool show)
        {
            var full = control.GetTemplateChild("PART_STARF_" + starId) as Image;
            var empty = control.GetTemplateChild("PART_STARE_" + starId) as Image;

            if (show)
            {
                full.Visibility = Visibility.Visible;
                empty.Visibility = Visibility.Hidden;
            } else
            {
                full.Visibility = Visibility.Hidden;
                empty.Visibility = Visibility.Visible;
            }
        }
    }
}