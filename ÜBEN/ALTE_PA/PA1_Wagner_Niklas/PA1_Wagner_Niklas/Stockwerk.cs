using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PA1_Wagner_Niklas
{
    public enum Stockwerk
    {
        EG,
        OG1,
        OG2,
        UNKNOWN
    }
    public static class StockwerkExtensions
    {
        public static int calculateDistance(Stockwerk start, Stockwerk stop)
        {
            return Math.Abs((int)stop - (int)start);
        }
    }
}
