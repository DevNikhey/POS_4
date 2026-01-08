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
    public class LoginControl : Control
    {
        static LoginControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LoginControl),
                new FrameworkPropertyMetadata(typeof(LoginControl)));
        }

        // Public Properties
        public string UsernameOrEmail
        {
            get { return (string)GetValue(UsernameOrEmailProperty); }
            set { SetValue(UsernameOrEmailProperty, value); }
        }

        public static readonly DependencyProperty UsernameOrEmailProperty =
            DependencyProperty.Register(nameof(UsernameOrEmail), typeof(string), typeof(LoginControl));

        public string Password
        {
            get { return (string)GetValue(PasswordProperty); }
            set { SetValue(PasswordProperty, value); }
        }

        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.Register(nameof(Password), typeof(string), typeof(LoginControl));

        public bool UseUsername
        {
            get { return (bool)GetValue(UseUsernameProperty); }
            set { SetValue(UseUsernameProperty, value); }
        }

        public static readonly DependencyProperty UseUsernameProperty =
            DependencyProperty.Register(nameof(UseUsername), typeof(bool), typeof(LoginControl), new PropertyMetadata(false));

        // Events
        public event EventHandler LoginClicked;
        public event EventHandler SwitchToRegistrationClicked;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            var btnLogin = GetTemplateChild("PART_LoginButton") as Button;
            var btnSwitch = GetTemplateChild("PART_SwitchButton") as Button;

            if (btnLogin != null) btnLogin.Click += (s, e) => OnLoginClicked();
            if (btnSwitch != null) btnSwitch.Click += (s, e) => SwitchToRegistrationClicked?.Invoke(this, EventArgs.Empty);
        }

        private void OnLoginClicked()
        {
            if (string.IsNullOrWhiteSpace(UsernameOrEmail) || string.IsNullOrWhiteSpace(Password))
            {
                MessageBox.Show("Bitte Benutzername/E-Mail und Passwort eingeben.", "Fehler", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            LoginClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}