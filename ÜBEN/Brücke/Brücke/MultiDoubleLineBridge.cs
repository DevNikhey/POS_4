using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brücke
{
    class MultiDoubleLineBridge : DoubleLineBridge
    {
        public MultiDoubleLineBridge()
        {
            base.EastSlim = new SemaphoreSlim(2, 2);
            base.WestSlim = new SemaphoreSlim(2, 2);
        }
    }
}
