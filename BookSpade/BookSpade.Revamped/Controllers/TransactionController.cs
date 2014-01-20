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

        #region setFinalPrice

        public JsonResult setFinalPrice(int transactionId, decimal finalprice)
        {
            TransactionHandler.UpdateTransaction(transactionId, finalprice, "FinalPrice"); 
            return Json("");
        }

        #endregion

        #region ConfirmTransaction

        [HttpPost]
        public ActionResult ConfirmTransaction(Transaction transaction)
        {
            // if final price is different than initial offer, update final price first
            if (transaction.FinalPrice != null &&
                transaction.FinalPrice != transaction.InitialPrice)
            {
                setFinalPrice(transaction.TransactionId, (decimal)transaction.FinalPrice);
            }

            TransactionHandler.ConfirmTransaction(transaction);
            return RedirectToAction("Index", "Home");
        }

        #endregion

        #region CancelTransaction

        [HttpPost]
        public ActionResult CancelTransaction(Transaction transaction)
        {
            TransactionHandler.CancelTransaction(transaction);
            return RedirectToAction("Index", "Home");
        }

        #endregion
    }
}
