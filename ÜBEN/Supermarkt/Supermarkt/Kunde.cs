using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarkt
{
    internal class Kunde
    {
        public string name { get; }
        private Kassa kassa;

        public Kunde(String name, Kassa kassa)
        {
            this.name = name;
            this.kassa = kassa;
        }

        public void Shoppen()
        {
            MainWindow.warteListe.Dispatcher.BeginInvoke(new Action(() =>
            {
                MainWindow.warteListe.Items.Add(this.name);
            }));

            kassa.processKunde(this);
        }
    }
}
