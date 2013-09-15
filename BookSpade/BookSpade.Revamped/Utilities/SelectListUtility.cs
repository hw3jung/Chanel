using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookSpade.Revamped.Utilities
{
    public sealed class SelectListUtility
    {
        private static readonly IEnumerable<SelectListItem> PostTypes = new List<SelectListItem>() {
            new SelectListItem()
            {
                Text = "Buy this book",
                Value = Convert.ToString(ActionBy.Buyer)
            },
            new SelectListItem()
            {
                Text = "Sell this book",
                Value = Convert.ToString(ActionBy.Seller)
            }
        };

        private static readonly IEnumerable<SelectListItem> BookConditions = new List<SelectListItem>() {
            new SelectListItem()
            {
                Text = "Excellent",
                Value = Convert.ToString(BookCondition.Excellent)
            },
            new SelectListItem()
            {
                Text = "Good",
                Value = Convert.ToString(BookCondition.Good)
            },
            new SelectListItem()
            {
                Text = "Decent",
                Value = Convert.ToString(BookCondition.Decent)
            },
            new SelectListItem()
            {
                Text = "Poor",
                Value = Convert.ToString(BookCondition.Poor)
            },
            new SelectListItem()
            {
                Text = "Salvageable",
                Value = Convert.ToString(BookCondition.Salvageable)
            }
        };

        public static IEnumerable<SelectListItem> getPostTypes()
        {
            return PostTypes;
        }

        public static IEnumerable<SelectListItem> getBookConditions()
        {
            return BookConditions;
        }
    }
}