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

namespace Aufzug
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static ListBox erdG;
        public static ListBox oberG1;
        public static ListBox oberG2;
        public static Label elevatorStatus;

        public MainWindow()
        {
            InitializeComponent();
            erdG = eg;
            oberG1 = og;
            oberG2 = og2;
            elevatorStatus = elevatorstatus;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Elevator elevator = new Elevator();

            for(int i =0; i < 5; i++)
            {
                Stock from = (Stock)new Random().Next(0, 3);
                Stock to;
                do
                {
                    to = (Stock)new Random().Next(0, 3);
                } while (to == from);
                Person person = new Person("Person " + i, from, to);

                Task.Run(() =>
                {
                    elevator.Drive(person);
                });

            }

            elevator.Start();
        }
    }
}