using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PA1_Wagner_Niklas
{
    internal class Elevator
    {
        public Elevator()
        {

        }

        public virtual void Use(Person p)
        {
            p.updateUI("leaveStart");
            p.updateUI("enterAufzug");
            Thread.Sleep(StockwerkExtensions.calculateDistance(p._start, p._stop) * 1000);
            p.updateUI("moveFinal");
        }
    }
}
