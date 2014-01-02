using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookSpade.Revamped.Utilities
{
    public static class Globals
    {
        // number of days before transaction is automatically confirmed
        public static int TRANSACTION_PERIOD
        {
            get
            {
                return 14;
            }
        }
    }
}