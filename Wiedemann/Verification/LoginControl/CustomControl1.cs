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

namespace LoginControl
{
    /// <summary>
    /// Führen Sie die Schritte 1a oder 1b und anschließend Schritt 2 aus, um dieses benutzerdefinierte Steuerelement in einer XAML-Datei zu verwenden.
    ///
    /// Schritt 1a) Verwenden des benutzerdefinierten Steuerelements in einer XAML-Datei, die im aktuellen Projekt vorhanden ist.
    /// Fügen Sie dieses XmlNamespace-Attribut dem Stammelement der Markupdatei 
    /// an der Stelle hinzu, an der es verwendet werden soll:
    ///
    ///     xmlns:MyNamespace="clr-namespace:LoginControl"
    ///
    ///
    /// Schritt 1b) Verwenden des benutzerdefinierten Steuerelements in einer XAML-Datei, die in einem anderen Projekt vorhanden ist.
    /// Fügen Sie dieses XmlNamespace-Attribut dem Stammelement der Markupdatei 
    /// an der Stelle hinzu, an der es verwendet werden soll:
    ///
    ///     xmlns:MyNamespace="clr-namespace:LoginControl;assembly=LoginControl"
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
        private TextBox _userBox;
        private PasswordBox _passwordBox;
        private Button _loginButton;
        private Button _switchButton;
        private TextBlock _errorText;

        public string User {  get; private set; }
        public string Password { get; private set; }

        public event EventHandler Login;
        public event EventHandler SwitchToRegistration;
        static CustomControl1()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomControl1), new FrameworkPropertyMetadata(typeof(CustomControl1)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _userBox = GetTemplateChild("PART_USER") as TextBox;
            _passwordBox = GetTemplateChild("PART_PASSWORD") as PasswordBox;
            _loginButton = GetTemplateChild("PART_LOGIN") as Button;
            _switchButton = GetTemplateChild("PART_SWITCH_TO_REGISTRATION") as Button;
            _errorText = GetTemplateChild("PART_ERROR") as TextBlock;

            if (_loginButton != null)
            {
                _loginButton.Click += OnLoginClicked;
            }

            if(_switchButton != null)
            {
                _switchButton.Click += (s, e) => SwitchToRegistration?.Invoke(this, EventArgs.Empty);
            }
        }

        private void OnLoginClicked(object sender, RoutedEventArgs e)
        {
            User = _userBox.Text;
            Password = _passwordBox.Password;

            if(string.IsNullOrWhiteSpace(User)|| string.IsNullOrWhiteSpace(Password))
            {
                ShowError("Bitte Benutzername und Passwort eingeben.");
                return;
            }

            _errorText.Visibility = Visibility.Collapsed;
            Login?.Invoke(this, EventArgs.Empty);

        }
        private void ShowError(string message)
        {
            if (_errorText != null)
            {
                _errorText.Text = message;
                _errorText.Visibility = Visibility.Visible;
            }
        }
    }
}
