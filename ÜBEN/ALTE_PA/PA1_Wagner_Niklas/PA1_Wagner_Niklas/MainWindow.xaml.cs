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

namespace PA1_Wagner_Niklas
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public static ListBox OberG2;
        public static ListBox OberG1;
        public static ListBox ErdG;
        public static ListBox Elev;

        public static TextBox anzahlP;

        public static Label og2Counter;
        public static Label og1Counter;
        public static Label egCounter;

        public MainWindow()
        {
            InitializeComponent();

            OberG2 = OG2;
            OberG1 = OG1;
            ErdG = EG;
            Elev = AZ;

            og2Counter = og2C;
            og1Counter = og1C;
            egCounter = egC;

            anzahlP = azPersonen;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Elevator elevator = new Elevator();

            createAndStartElevator(elevator, int.Parse(anzahlP.Text));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            SmallElevator smallElevator = new SmallElevator();

            createAndStartElevator(smallElevator, int.Parse(anzahlP.Text));
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            BigElevator bigElevator = new BigElevator();

            createAndStartElevator(bigElevator, int.Parse(anzahlP.Text));
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            SmallElevator2 smallElevator2 = new SmallElevator2();
            createAndStartElevator(smallElevator2, int.Parse(anzahlP.Text));
        }

        private void createAndStartElevator(Elevator elevator, int personCount)
        {
            for (int i = 0; i < personCount; i++)
            {
                Person p = new Person(i);
                Thread personThread = new Thread(() => p.Move(elevator));
                personThread.Start();
            }
        }
    }
}