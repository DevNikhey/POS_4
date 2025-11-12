using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace Supermarkt
{

    
    internal class Kassa
    {
        public AutoResetEvent kassaEvent = new AutoResetEvent(false);

        public Kassa()
        {
            MainWindow.kassaStatus.Dispatcher.BeginInvoke(new Action(() =>
            {
                MainWindow.kassaStatus.Content = "Status: Closed";
            }));
        }

        public virtual void processKunde(Kunde kunde)
        {
            kassaEvent.WaitOne();
            {
                this.updateUIZahlt(kunde.name);
                Thread.Sleep(5000);
                this.updateUIFertig(kunde.name);
            }
            kassaEvent.Set();
        }

        public virtual void start()
        {
            kassaEvent.Set();
            this.updateKasseStatus("Open");
        }

        public void updateUIZahlt(String costumerName)
        {
            MainWindow.warteListe.Dispatcher.BeginInvoke(new Action(() =>
            {
                MainWindow.warteListe.Items.Remove(costumerName);
            }));
            MainWindow.einkaufsListe.Dispatcher.BeginInvoke(new Action(() =>
            {
                MainWindow.einkaufsListe.Items.Add(costumerName);
            }));
        }

        public void updateUIFertig(String customerName)
        {
            MainWindow.einkaufsListe.Dispatcher.BeginInvoke(new Action(() =>
            {
                MainWindow.einkaufsListe.Items.Remove(customerName);
            }));
        }
        public void updateKasseStatus(String status)
        {
            MainWindow.kassaStatus.Dispatcher.BeginInvoke(new Action(() =>
            {
                MainWindow.kassaStatus.Content = "Status: " + status;
            }));
        }
    }
}
