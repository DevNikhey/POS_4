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
    public class RegisterControl : Control
    {
        static RegisterControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(RegisterControl),
                new FrameworkPropertyMetadata(typeof(RegisterControl)));
        }

        // Public Properties
        public string Vorname
        {
            get { return (string)GetValue(VornameProperty); }
            set { SetValue(VornameProperty, value); }
        }

        public static readonly DependencyProperty VornameProperty =
            DependencyProperty.Register(nameof(Vorname), typeof(string), typeof(RegisterControl));

        public string Password
        {
            get { return (string)GetValue(PasswordProperty); }
            set { SetValue(PasswordProperty, value); }
        }

        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.Register(nameof(Password), typeof(string), typeof(RegisterControl));

        // Events
        public event EventHandler Register;
        public event EventHandler Cancel;
        public event EventHandler SwitchToLoginClicked;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            var btnLogin = GetTemplateChild("PART_RegisterButton") as Button;
            var btnSwitch = GetTemplateChild("PART_SwitchButton") as Button;

            if (btnLogin != null) btnLogin.Click += (s, e) => OnRegisterClicked();
            if (btnSwitch != null) btnSwitch.Click += (s, e) => SwitchToLoginClicked?.Invoke(this, EventArgs.Empty);
        }

        private void OnRegisterClicked()
        {
            if (string.IsNullOrWhiteSpace(Vorname) || string.IsNullOrWhiteSpace(Password))
            {
                MessageBox.Show("Bitte Benutzername/E-Mail und Passwort eingeben.", "Fehler", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Register?.Invoke(this, EventArgs.Empty);
        }
    }
}