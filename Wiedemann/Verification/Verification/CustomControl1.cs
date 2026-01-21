using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace Verification
{
    /// <summary>
    /// Führen Sie die Schritte 1a oder 1b und anschließend Schritt 2 aus, um dieses benutzerdefinierte Steuerelement in einer XAML-Datei zu verwenden.
    ///
    /// Schritt 1a) Verwenden des benutzerdefinierten Steuerelements in einer XAML-Datei, die im aktuellen Projekt vorhanden ist.
    /// Fügen Sie dieses XmlNamespace-Attribut dem Stammelement der Markupdatei 
    /// an der Stelle hinzu, an der es verwendet werden soll:
    ///
    ///     xmlns:MyNamespace="clr-namespace:Verification"
    ///
    ///
    /// Schritt 1b) Verwenden des benutzerdefinierten Steuerelements in einer XAML-Datei, die in einem anderen Projekt vorhanden ist.
    /// Fügen Sie dieses XmlNamespace-Attribut dem Stammelement der Markupdatei 
    /// an der Stelle hinzu, an der es verwendet werden soll:
    ///
    ///     xmlns:MyNamespace="clr-namespace:Verification;assembly=Verification"
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
    public class RegistrationControl : Control
    {

        private TextBox _firstNameBox;
        private TextBox _lastNameBox;
        private TextBox _emailBox;
        private TextBox _userNameBox;
        private PasswordBox _passwordBox;
        private PasswordBox _passwordConfirmBox;
        private TextBox _addressBox;
        private Button _registerButton;
        private Button _resetButton;
        private TextBlock _errorText;
        private Button _cancelButton;

        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string UserName { get; private set; }
        public string Password { get; private set; }
        public string Address { get; private set; }

        public event EventHandler Register;
        public event EventHandler SwitchtoLogin;

        static RegistrationControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(RegistrationControl), new FrameworkPropertyMetadata(typeof(RegistrationControl)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _firstNameBox = GetTemplateChild("PART_FIRSTNAME") as TextBox;
            _lastNameBox = GetTemplateChild("PART_LASTNAME") as TextBox;
            _emailBox = GetTemplateChild("PART_EMAIL") as TextBox;
            _userNameBox = GetTemplateChild("PART_USERNAME") as TextBox;
            _passwordBox = GetTemplateChild("PART_PASSWORD") as PasswordBox;
            _passwordConfirmBox = GetTemplateChild("PART_PASSWORD_CONFIRM") as PasswordBox;
            _addressBox = GetTemplateChild("PART_ADDRESS") as TextBox;
            _registerButton = GetTemplateChild("PART_REGISTER") as Button;
            _resetButton = GetTemplateChild("PART_RESET") as Button;
            _errorText = GetTemplateChild("PART_ERROR") as TextBlock;
            _cancelButton = GetTemplateChild("PART_CANCEL") as Button;

            if (_registerButton != null)
            {
                _registerButton.Click += OnRegisterClicked;
            }

            if(_resetButton != null)
            {
                _resetButton.Click += OnResetClicked;
            }

            if(_cancelButton != null)
            {
                _cancelButton.Click += (s, e) => SwitchtoLogin?.Invoke(this, EventArgs.Empty);
            }
        }

        private bool Validate()
        {
            if (string.IsNullOrWhiteSpace(FirstName) || string.IsNullOrWhiteSpace(LastName) || string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
            {
                ShowError("Bitte alle Pflichtfelder ausfüllen.", true);
                return false;
            }

            if (!Regex.IsMatch(_emailBox.Text, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
            {
                ShowError("Enter a valid email.", true);
                _emailBox.Select(0, _emailBox.Text.Length);
                _emailBox.Focus();
                return false;
            }

            if (Password != _passwordConfirmBox.Password) 
            {
                ShowError("Passwörter stimmen nicht überein.", true);
                return false;
            }
            return true;
        }

        private void ShowError(string message, bool boolean)
        {
            if (boolean)
            {
                if (_errorText != null)
                {
                    _errorText.Text = message;
                    _errorText.Visibility = Visibility.Visible;
                }
            }
            else
            {
                if (_errorText != null)
                {
                    _errorText.Text = string.Empty;
                    _errorText.Visibility = Visibility.Collapsed;
                }
            }
            
        }

        private void OnRegisterClicked(object sender, RoutedEventArgs e)
        {
            FirstName = _firstNameBox.Text;
            LastName = _lastNameBox.Text;
            Email = _emailBox.Text;
            UserName = _userNameBox.Text;
            Password = _passwordBox.Password;
            Address = _addressBox.Text;

            if(!Validate()) { return; }

            _errorText.Visibility = Visibility.Collapsed;
            Register?.Invoke(this, EventArgs.Empty);
        }

        private void OnResetClicked(object sender, RoutedEventArgs e) 
        { 
            _firstNameBox.Text = string.Empty;
            _lastNameBox.Text = string.Empty;
            _emailBox.Text = string.Empty;
            _userNameBox.Text = string.Empty;
            _passwordBox.Password = string.Empty;
            _passwordConfirmBox.Password = string.Empty;
            _addressBox.Text = string.Empty;

            ShowError("", false);
        }
    }
}
