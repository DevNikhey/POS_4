using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarkt
{
    internal class SelfCheckoutKassa : Kassa
    {
        public SemaphoreSlim semaphore = new SemaphoreSlim(3, 3);

        public SelfCheckoutKassa()
        {
            base.updateKasseStatus("Closed - Self-Checkout");
        }

        public override void processKunde(Kunde kunde)
        {
            semaphore.Wait();
            base.updateUIZahlt(kunde.name);
            Thread.Sleep(5000);
            base.updateUIFertig(kunde.name);
            semaphore.Release();
        }

        public override void start()
        {
            base.updateKasseStatus("Open - Self-Checkout");
        }
    }
}
