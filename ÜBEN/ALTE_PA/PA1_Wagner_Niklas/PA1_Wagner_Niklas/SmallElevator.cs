using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PA1_Wagner_Niklas
{
    internal class SmallElevator : Elevator
    {
        private Object lockObj = new Object();
        private Stockwerk previousStockwerk;

        public SmallElevator()
        {
        }

        public override void Use(Person p)
        {
            lock(lockObj)
            {
                Thread.Sleep(calculateDistance(p._start) * 1000);
                base.Use(p);

                previousStockwerk = p._stop;
            }
        }

        private int calculateDistance(Stockwerk start)
        {
            return Math.Abs((int)start - (int)previousStockwerk);
        }   
    }
}
