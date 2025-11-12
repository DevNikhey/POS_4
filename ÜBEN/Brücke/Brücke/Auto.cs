using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brücke
{
    class Auto
    {
        public Direction origionDir;
        public string name { get; }

        public Auto(string name, Direction dir)
        {
            this.name = name;
            this.origionDir = dir;

            updateUI();
        }

        public void updateUI()
        {
            MainWindow.eastBox.Dispatcher.BeginInvoke(new Action(() =>
            {
                switch (origionDir)
                {
                    case Direction.EAST:
                        MainWindow.eastBox.Items.Add(name);
                        Bridge.carsWaitingWest++;
                        break;
                    case Direction.WEST:
                        MainWindow.westBox.Items.Add(name);
                        break;
                }
            }));
        }
    }
}
