using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace Aufzug
{
    class Elevator
    {
        private AutoResetEvent aEvent = new AutoResetEvent(false);
        public Elevator()
        {
        }

        public virtual void Drive(Person person)
        {
            aEvent.WaitOne();
            {
                this.updateUI(person, true);
                person.updateUI(false); // Person leaves the from floor
                Random rand = new Random();
                Thread.Sleep(rand.Next(2000, 5000));
                this.updateUI(person, false); // move the person to the new floor
            }
            aEvent.Set();
        }

        public virtual void Start()
        {
            aEvent.Set();
        }

        private void updateUI(Person person, bool driving)
        {
            if (driving)
            {
                MainWindow.elevatorStatus.Dispatcher.BeginInvoke(new Action(() =>
                {
                    MainWindow.elevatorStatus.Content = "Elevator: Driving " + person.name + " from " + person.from.ToString() + " to " + person.to.ToString();
                }));
            }
            else
            {
                MainWindow.elevatorStatus.Dispatcher.BeginInvoke(new Action(() =>
                {
                    MainWindow.elevatorStatus.Content = "Elevator: " + person.name + " Arrived at " + person.to.ToString();
                }));
            }


            if(!driving)
            {
                switch (person.to)
                {                    
                    case Stock.ERDGESCHOSS:
                        MainWindow.erdG.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            ListBoxItem item = new ListBoxItem();
                            item.Background = new SolidColorBrush(Colors.LightGreen);
                            item.Content = person.name;
                            MainWindow.erdG.Items.Add(item);
                        }));
                        break;
                    case Stock.ERSTES_OBERGESCHOSS:
                        MainWindow.oberG1.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            ListBoxItem item = new ListBoxItem();
                            item.Background = new SolidColorBrush(Colors.LightGreen);
                            item.Content = person.name;
                            MainWindow.oberG1.Items.Add(item);
                        }));
                        break;
                    case Stock.ZWEITES_OBERGESCHOSS:
                        MainWindow.oberG2.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            ListBoxItem item = new ListBoxItem();
                            item.Background = new SolidColorBrush(Colors.LightGreen);
                            item.Content = person.name;
                            MainWindow.oberG2.Items.Add(item);
                        }));
                        break;
                }
            }
        }
    }
}
