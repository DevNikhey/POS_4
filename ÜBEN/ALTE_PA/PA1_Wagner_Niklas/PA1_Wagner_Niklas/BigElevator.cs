using System;
using System.Threading;

namespace PA1_Wagner_Niklas
{
    internal class BigElevator : Elevator
    {
        private readonly object lockObj = new object();

        private Stockwerk currentStop = Stockwerk.UNKNOWN;
        private Stockwerk currentStart = Stockwerk.UNKNOWN;

        private int counter = 0;

        public BigElevator()
        {
        }

        public override void Use(Person p)
        {
            lock(lockObj)
            {
                while (currentStart != Stockwerk.UNKNOWN && (currentStart != p._start || currentStop != p._stop))
                {
                    Monitor.Wait(lockObj);
                }

                if (currentStart == Stockwerk.UNKNOWN)
                {
                    currentStop = p._stop;
                    currentStart = p._start;
                }

                counter++;
            }

            base.Use(p);
            lock (lockObj)
            {
                counter--;

                if (counter == 0)
                {
                    currentStart = Stockwerk.UNKNOWN;
                    currentStop = Stockwerk.UNKNOWN;
                    Monitor.PulseAll(lockObj);
                }
            }
        }
    }
}
