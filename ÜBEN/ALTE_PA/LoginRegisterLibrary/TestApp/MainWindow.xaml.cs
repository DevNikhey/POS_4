using LoginRegisterLibrary;
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

namespace TestApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            loginControl.LoginClicked += LoginControl_LoginClicked;
            loginControl.SwitchToRegistrationClicked += SwitchToRegister;

            registerControl.SwitchToLoginClicked += SwitchToLogin;
        }

        private void LoginControl_LoginClicked(object sender, EventArgs e)
        {
            string usernameOrEmail = loginControl.UsernameOrEmail;
            string password = loginControl.Password;

            MessageBox.Show($"Logging in with {usernameOrEmail} and Password {password}");
            // Call your login logic here
        }

        private void SwitchToRegister(object sender, EventArgs e)
        {
            loginControl.Visibility = Visibility.Hidden;
            registerControl.Visibility = Visibility.Visible;
        }

        private void SwitchToLogin(object sender, EventArgs e)
        {
            loginControl.Visibility = Visibility.Visible;
            registerControl.Visibility = Visibility.Hidden;
        }
    }
}