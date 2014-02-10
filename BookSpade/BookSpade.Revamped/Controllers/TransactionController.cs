using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BookSpade.Revamped.Handlers;
using BookSpade.Revamped.Models;
using BookSpade.Revamped.Utilities;

namespace BookSpade.Revamped.Controllers
{
    public class TransactionController : Controller
    {
        //
        // GET: /Transaction/

        #region Index

        public ActionResult Index()
        {
            return View();
        }

        #endregion

        #region TransactionHistory

        public ActionResult TransactionHistory()
        {
            string username = User.Identity.Name;
            Profile profile = ProfileHandler.GetProfile(username);
            IEnumerable<Transaction> transactions = TransactionHandler.getUserTransactionHistory(profile.ProfileId);

            IEnumerable<TransactionDetailModel> detailedHistory = transactions.Select(
                x => new TransactionDetailModel(x));

            return View(detailedHistory); 
        }

        #endregion

        #region TransactionDetails

        public ActionResult TransactionDetails(int transactionId)
        {
            Transaction transaction = TransactionHandler.getTransaction(transactionId);

            Profile buyer = ProfileHandler.GetProfile(transaction.BuyerId);
            Profile seller = ProfileHandler.GetProfile(transaction.SellerId);
            if (User.Identity.Name == buyer.Email)
            {
                transaction.CurrentUser = ActionBy.Buyer;
                transaction.CounterPartyName = seller.Name;
            }
            else
            {
                transaction.CurrentUser = ActionBy.Seller;
                transaction.CounterPartyName = buyer.Name;
            }

            if ((transaction.CurrentUser == ActionBy.Buyer && transaction.Confirmed == Confirmed.ByBuyer)
                || (transaction.CurrentUser == ActionBy.Seller && transaction.Confirmed == Confirmed.BySeller))
            {
                transaction.ConfirmedByCurrentUser = true;
            }
            else
            {
                transaction.ConfirmedByCurrentUser = false;
            }

            TransactionCommentModel model = new TransactionCommentModel(transaction, User.Identity.Name);
            return View(model);
        }

        #endregion

        #region newComment

        public JsonResult newComment(string comment, ActionBy commentor, int userId, int OtherUserId, int transactionId)
        {
            Comment newComment = new Comment(
                -1,
                comment,
                userId,
                commentor,
                transactionId,
                1,
                0,
                DateTime.Now,
                DateTime.Now
            ); 
            CommentHandler.createComment(newComment);

            return Json("");
        }

        #endregion

        #region ConfirmTransaction

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmTransaction(Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                TransactionHandler.ConfirmTransaction(transaction);
            }

            return RedirectToAction("Index", "Home");
        }

        #endregion

        #region CancelTransaction

        [HttpPost]
        public ActionResult CancelTransaction(Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                TransactionHandler.CancelTransaction(transaction);
            }

            return RedirectToAction("Index", "Home");
        }

        #endregion
    }
}
