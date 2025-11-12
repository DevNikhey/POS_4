using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aufzug
{
    class Person
    {

        public string name { get; }
        public Stock from { get; }
        public Stock to { get; }


        public Person(string name, Stock from, Stock to)
        {
            this.name = name;
            this.from = from;
            this.to = to;

            updateUI(true);
        }

        public void updateUI(bool add)
        {
            switch(from)
            {
                case Stock.ERDGESCHOSS:
                    if(add)
                    {
                        MainWindow.erdG.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            MainWindow.erdG.Items.Add(name);
                        }));
                    } else
                    {
                        MainWindow.erdG.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            MainWindow.erdG.Items.Remove(name);
                        }));
                    }
                        break;
                case Stock.ERSTES_OBERGESCHOSS:
                    if (add)
                    {
                        MainWindow.oberG1.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            MainWindow.oberG1.Items.Add(name);
                        }));
                    }
                    else
                    {
                        MainWindow.oberG1.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            MainWindow.oberG1.Items.Remove(name);
                        }));
                    }
                    break;
                case Stock.ZWEITES_OBERGESCHOSS:
                    if (add)
                    {
                        MainWindow.oberG2.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            MainWindow.oberG2.Items.Add(name);
                        }));
                    }
                    else
                    {
                        MainWindow.oberG2.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            MainWindow.oberG2.Items.Remove(name);
                        }));
                    }
                    break;
            }
        }
    }
}
