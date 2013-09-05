using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookSpade.Revamped.Handlers;
using BookSpade.Revamped.Models; 

namespace BookSpade.Revamped.Controllers
{
    public class TransactionController : Controller
    {
        //
        // GET: /Transaction/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TransactionHistory()
        {
            string username = User.Identity.Name;
            Profile profile = ProfileHandler.GetProfile(username);
            IEnumerable<Transaction> transactions = TransactionHandler.getUserTransactionHistory(profile.ProfileId);

            IEnumerable<TransactionDetailModel> detailedHistory = transactions.Select(
                x => new TransactionDetailModel(x));

            return View(detailedHistory); 
        }

    }
}
