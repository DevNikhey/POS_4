using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace Brücke
{
    class Bridge
    {
        private AutoResetEvent eventSlim = new AutoResetEvent(false);
        public static int carsWaitingWest = 0;

        public Bridge()
        {

        }

        public virtual void CrossBridge(Auto auto)
        {
            eventSlim.WaitOne();
            updateUI(auto, false);
            Thread.Sleep(new Random().Next(2000, 6000));
            updateUI(auto, true);
            eventSlim.Set();
        }

        public virtual void Start()
        {
            eventSlim.Set();
        }

        public void updateUI(Auto auto, bool crossed)
        {
            if (!crossed)
            {
                MainWindow.crossBox.Dispatcher.BeginInvoke(new Action(() =>
                {
                    switch (auto.origionDir)
                    {
                        case Direction.WEST:
                            MainWindow.crossBox.Items.Add(auto.name + " -->");
                            break;
                        case Direction.EAST:
                            MainWindow.crossBox.Items.Add("<-- " + auto.name);
                            break;
                    }
                }));

                MainWindow.crossBox.Dispatcher.BeginInvoke(new Action(() =>
                {
                    switch (auto.origionDir)
                    {
                        case Direction.EAST:
                            MainWindow.eastBox.Items.Remove(auto.name);
                            break;
                        case Direction.WEST:
                            MainWindow.westBox.Items.Remove(auto.name);
                            break;
                    }
                }));
            }
            else
            {
                MainWindow.crossBox.Dispatcher.BeginInvoke(new Action(() =>
                {
                    switch (auto.origionDir)
                    {
                        case Direction.WEST:
                            MainWindow.crossBox.Items.Remove(auto.name + " -->");
                            break;
                        case Direction.EAST:
                            MainWindow.crossBox.Items.Remove("<-- " + auto.name);
                            break;
                    }
                }));

                MainWindow.crossBox.Dispatcher.BeginInvoke(new Action(() =>
                {
                    switch (auto.origionDir)
                    {
                        case Direction.EAST:
                            ListBoxItem item = new ListBoxItem();
                            item.Content = auto.name;
                            item.Background = new SolidColorBrush(Colors.LightGreen);
                            MainWindow.westBox.Items.Add(item);

                            break;
                        case Direction.WEST:
                            ListBoxItem item2 = new ListBoxItem();
                            item2.Content = auto.name;
                            item2.Background = new SolidColorBrush(Colors.LightGreen);
                            MainWindow.eastBox.Items.Add(item2);
                            break;
                    }
                }));
            }
        }
    }
}
