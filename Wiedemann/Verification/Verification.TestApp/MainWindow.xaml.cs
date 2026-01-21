using LoginControl;
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

namespace Verification.TestApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnSwitchToRegistration(object sender, EventArgs e)
        {
            LoginCtrl.Visibility = Visibility.Collapsed;
            RegistrationCtrl.Visibility = Visibility.Visible;
        }

        private void OnSwitchToLogin(object sender, EventArgs e)
        {
            RegistrationCtrl.Visibility = Visibility.Collapsed;
            LoginCtrl.Visibility = Visibility.Visible;
        }

        private void OnLogin(object sender, EventArgs e)
        {
            var login = (LoginControl.CustomControl1)sender;

            MessageBox.Show(
                $"User: {login.User}\nPasswort: {login.Password}",
                "Login");
        }

        private void OnRegister(object sender, EventArgs e)
        {
            var reg = (Verification.RegistrationControl)sender;

            MessageBox.Show(
                $"Name: {reg.FirstName} {reg.LastName}\nE-Mail: {reg.Email}",
                "Registrierung");
        }
    }
}
    