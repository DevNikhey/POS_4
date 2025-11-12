using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brücke
{
    class DoubleLineBridge : Bridge
    {
        protected SemaphoreSlim EastSlim = new SemaphoreSlim(1, 1);
        protected SemaphoreSlim WestSlim = new SemaphoreSlim(1, 1);

        public DoubleLineBridge()
        {
        }

        public override void CrossBridge(Auto auto)
        {
            if(auto.origionDir == Direction.WEST)
            {
                WestSlim.Wait();
            }
            else
            {
                EastSlim.Wait();
            }
               
            updateUI(auto, false);
            Thread.Sleep(new Random().Next(2000, 6000));
            updateUI(auto, true);

            if (auto.origionDir == Direction.WEST)
            {
                WestSlim.Release();
            }
            else
            {
                EastSlim.Release();
            }
        }

        override public void Start()
        {
        }
    }
}
