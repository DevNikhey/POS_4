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

namespace Supermarkt
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static ListView einkaufsListe;
        public static ListView warteListe;
        public static Label kassaStatus;

        public MainWindow()
        {
            InitializeComponent();

            einkaufsListe = einkaufsliste;
            warteListe = warteliste;
            kassaStatus = kassastatus;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SelfCheckoutKassa kassa = new SelfCheckoutKassa();

            for (int i = 0; i < Int32.Parse(kundenAnzahl.Text); i++)
            {
                Kunde k = new Kunde("Kunde " + i, kassa);
                new Thread(k.Shoppen).Start();
            }

            kassa.start();
        }
    }
}