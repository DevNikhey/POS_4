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

namespace Brücke
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public static ListBox eastBox;
        public static ListBox westBox;
        public static ListBox crossBox;

        public MainWindow()
        {
            InitializeComponent();

            eastBox = eastbox;
            westBox = westbox;
            crossBox = crossbox;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Bridge brücke = new MultiDoubleLineBridge();

            for(int i=0; i < new Random().Next(5, 30); i++)             {
                Direction tempDir;
                if (new Random().Next(0, 2) == 0)
                {
                    tempDir = Direction.EAST;
                }
                else
                {
                    tempDir = Direction.WEST;
                }

                Auto auto = new Auto("auto " + i, tempDir);
                Task.Run(() => 
                {
                    brücke.CrossBridge(auto);
                });
            }

            brücke.Start();
        }
    }
}