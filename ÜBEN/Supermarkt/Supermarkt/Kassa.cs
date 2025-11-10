using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarkt
{

    
    internal class Kassa
    {
        public ManualResetEventSlim kassaEvent = new ManualResetEventSlim(true);

        public Kassa()
        {
        }

        public void Run()
        {
            while (true)
            {
                kassaEvent.Wait();
                Thread.Sleep(100); // Simulate processing time
                kassaEvent.Reset();
            }
        }

    }
}
