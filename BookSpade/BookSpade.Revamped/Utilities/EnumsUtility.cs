using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookSpade.Revamped.Utilities
{
    public enum ActionBy
    {
        Seller  = 0,
        Buyer   = 1
    }

    public enum BookCondition
    {
        Salvageable = 1,
        Poor        = 2,
        Decent      = 3,
        Good        = 4,
        Excellent   = 5
    }

    public enum Confirmed
    {
        ByNone = 0,
        ByBuyer = 1,
        BySeller = 2,
        ByBoth = 3
    }

    public sealed class EnumsUtility
    {
    }
}