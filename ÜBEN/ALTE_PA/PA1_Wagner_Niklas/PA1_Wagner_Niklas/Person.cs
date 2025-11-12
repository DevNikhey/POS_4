using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace PA1_Wagner_Niklas
{
    internal class Person
    {

        private int _id;
        public int id;

        public Stockwerk _start;
        public Stockwerk _stop;

        public Person(int id)
        {
            this._id = id;

            // Zufälliges Start- und Zielstockwerk generieren
            Random rand = new Random();
            _start = (Stockwerk)rand.Next(Enum.GetNames(typeof(Stockwerk)).Length - 1);
            do
            {
                _stop = (Stockwerk)rand.Next(Enum.GetNames(typeof(Stockwerk)).Length - 1);
            } while (_stop == _start);

            updateUI("addStart");
        }

        public void Move(Elevator elevator)
        {
            Thread.Sleep(new Random().Next(1000, 10000));
            elevator.Use(this);
        }

        public void updateUI(string action)
        {
            Application.Current.Dispatcher.BeginInvoke(() =>
            {
                switch (action)
                {
                    case "addStart":
                        switch (this._start)
                        {
                            case Stockwerk.OG2:
                                MainWindow.OberG2.Items.Add(this._id + ", " + this._start + " -> " + this._stop);
                                break;
                            case Stockwerk.OG1:
                                MainWindow.OberG1.Items.Add(this._id + ", " + this._start + " -> " + this._stop);
                                break;
                            case Stockwerk.EG:
                                MainWindow.ErdG.Items.Add(this._id + ", " + this._start + " -> " + this._stop);
                                break;
                        }
                        break;
                    case "leaveStart":
                        
                        switch(this._start)
                        {
                            case Stockwerk.OG2:
                                MainWindow.OberG2.Items.Remove(this._id + ", " + this._start + " -> " + this._stop);
                                break;
                            case Stockwerk.OG1:
                                MainWindow.OberG1.Items.Remove(this._id + ", " + this._start + " -> " + this._stop);
                                break;
                            case Stockwerk.EG:
                                MainWindow.ErdG.Items.Remove(this._id + ", " + this._start + " -> " + this._stop);
                                break;
                        }

                        break;
                    case "enterAufzug":
                        MainWindow.Elev.Items.Add(this._id + ", " + this._start + " -> " + this._stop);
                        break;
                    case "moveFinal":
                        MainWindow.Elev.Items.Remove(this._id + ", " + this._start + " -> " + this._stop);

                        ListBoxItem item = new ListBoxItem();
                        item.Background = new SolidColorBrush(Colors.Green);
                        item.Content = this._id + ", " + this._start + " -> " + this._stop;

                        switch (this._stop)
                        {
                            case Stockwerk.OG2:
                                MainWindow.OberG2.Items.Add(item);
                                break;
                            case Stockwerk.OG1:
                                MainWindow.OberG1.Items.Add(item);
                                break;
                            case Stockwerk.EG:
                                MainWindow.ErdG.Items.Add(item);
                                break;
                        }
                        break;
                }
            });
        }
    }
}
