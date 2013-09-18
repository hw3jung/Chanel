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

        public JsonResult newComment(string comment, int userId, int OtherUserId, int transactionId)
        {
            Comment newComment = new Comment(-1, comment, userId, ActionBy.Buyer, transactionId, 1, 0, DateTime.Now, DateTime.Now); 
            CommentHandler.createComment(newComment);
            Profile NotifyUser = ProfileHandler.GetProfile(OtherUserId);

            EmailUtil.Mail(NotifyUser.Email, newComment.CommentatorProfile.Name + " : Responded to your comment", "'" + comment + "'");  

            return Json(newComment.CommentatorProfile.Name);
        }

        #endregion

    }
}
