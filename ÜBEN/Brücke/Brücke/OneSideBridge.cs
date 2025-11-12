using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brücke
{
    class OneSideBridge : Bridge
    {
        private AutoResetEvent eLock = new AutoResetEvent(false);
        private AutoResetEvent wLock = new AutoResetEvent(false);

        public OneSideBridge()
        {

        }

        public override void CrossBridge(Auto auto)
        {
            if(auto.origionDir == Direction.WEST)
            {
                wLock.WaitOne();
            } else
            {
                eLock.WaitOne();
            }

            updateUI(auto, false);
            Thread.Sleep(new Random().Next(2000, 6000));
            updateUI(auto, true);

            if (auto.origionDir == Direction.WEST)
            {
                wLock.Set();
                carsWaitingWest--;
            }
            else
            {
                eLock.Set();
            }

            if (carsWaitingWest == 0)
            {
                wLock.Reset();
                eLock.Set();
            }
        }

        override public void Start()
        {
            // Only allow cars to move from west to east initially
            wLock.Set();
        }
    }
}
