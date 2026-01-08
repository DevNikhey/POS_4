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
using System.Windows.Shapes;

namespace CountdownWeckerLibrary
{
    /// <summary>
    /// Interaktionslogik für DateTimeDlg.xaml
    /// </summary>
    public partial class DateTimeDlg : Window
    {
        TextBox _mBox;
        TextBox _sBox;
        public int Minutes => int.TryParse(_mBox.Text, out int m) ? m : 0;
        public int Seconds => int.TryParse(_sBox.Text, out int s) ? s : 0;


        public DateTimeDlg()
        {
            InitializeComponent();
            _mBox = minutesBox;
            _sBox = secondsBox;
        }

        private void buttonOkay_Click(object sender, RoutedEventArgs e)
        {
            
            this.Close();
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
