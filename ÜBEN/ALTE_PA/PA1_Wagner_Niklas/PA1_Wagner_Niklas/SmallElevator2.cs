using System;
using System.Net.NetworkInformation;
using System.Threading;
using System.Windows;

namespace PA1_Wagner_Niklas
{
    internal class SmallElevator2 : Elevator
    {
        private readonly object lockObj = new object();
        private Stockwerk currentStockwerk = Stockwerk.UNKNOWN;

        private SemaphoreSlim semaphore = new SemaphoreSlim(1, 1);

        private int counterEG = 0;
        private int counterOG1 = 0;
        private int counterOG2 = 0;

        public SmallElevator2()
        {
        }

        public override void Use(Person p)
        {
            lock(lockObj)
            {
                while(currentStockwerk != Stockwerk.UNKNOWN && currentStockwerk != p._start)
                {
                    switch(p._start)
                    {
                        case Stockwerk.EG:
                            counterEG++;
                            break;
                        case Stockwerk.OG1:
                            counterOG1++;
                            break;
                        case Stockwerk.OG2:
                            counterOG2++;
                            break;
                    }
                    Monitor.Wait(lockObj);
                }

                if (currentStockwerk == Stockwerk.UNKNOWN)
                {
                    currentStockwerk = p._stop;
                }

                semaphore.Wait();
                base.Use(p);
                semaphore.Release();

                // check 
                bool toogle = false;
                switch(p._stop)
                {
                    case Stockwerk.EG:
                        counterEG--;
                        if(counterEG > 0)
                        {
                            toogle = true;
                        }
                        break;
                    case Stockwerk.OG1:
                        counterOG1--;
                        if (counterOG1 > 0)
                        {
                            toogle = true;
                        }
                        break;
                    case Stockwerk.OG2:
                        counterOG2--;
                        if (counterOG2 > 0)
                        {
                            toogle = true;
                        }
                        break;
                }
                
                currentStockwerk = toogle ? p._stop : getHighest();
                Monitor.PulseAll(lockObj);
            }
        }

        private Stockwerk getHighest()
        {
            if (counterEG >= counterOG1 && counterEG >= counterOG2)
                return Stockwerk.EG;

            if (counterOG1 >= counterEG && counterOG1 >= counterOG2)
                return Stockwerk.OG1;

            return Stockwerk.OG2;
        }
    }
}
