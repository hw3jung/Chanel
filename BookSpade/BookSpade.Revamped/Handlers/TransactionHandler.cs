﻿using BookSpade.Revamped.DAL;
using BookSpade.Revamped.Models;
using BookSpade.Revamped.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace BookSpade.Revamped.Handlers
{
    public class TransactionHandler
    {
        #region CreateTransaction 

        public static int CreateTransaction(Transaction transaction)
        {
            int transactionId = -1;

            try
            {
                DataAccess da = new DataAccess();

                Dictionary<string, object> Transaction = new Dictionary<string, object>();
                Transaction.Add("TextbookId", transaction.TextbookId);
                Transaction.Add("SellerId", transaction.SellerId);
                Transaction.Add("BuyerId", transaction.BuyerId);
                Transaction.Add("SellerPostId", transaction.SellerPostId);
                Transaction.Add("BuyerPostId", transaction.BuyerPostId);
                Transaction.Add("FinalPrice", transaction.FinalPrice == null ? (object)DBNull.Value : transaction.FinalPrice);
                Transaction.Add("InitialPrice", transaction.InitialPrice);
                Transaction.Add("Confirmed", transaction.Confirmed);
                Transaction.Add("IsActive", transaction.IsActive);
                Transaction.Add("IsDeleted", transaction.IsDeleted);
                Transaction.Add("CreatedDate", transaction.CreatedDate);
                Transaction.Add("ModifiedDate", transaction.ModifiedDate);

                transactionId = da.insert(Transaction, "Transactions");

                Dictionary<string, object> TransactionSeller = new Dictionary<string, object>();
                TransactionSeller.Add("TransactionId", transactionId);
                TransactionSeller.Add("UserId", transaction.SellerId);

                Dictionary<string, object> TransactionBuyer = new Dictionary<string, object>();
                TransactionBuyer.Add("TransactionId", transactionId);
                TransactionBuyer.Add("UserId", transaction.BuyerId);

                da.insert(TransactionBuyer, "TransactionHistory");
                da.insert(TransactionSeller, "TransactionHistory");

            }
            catch (Exception ex)
            {
                Console.Write("ERROR: An error occured in adding a new transaction --- " + ex.Message + "          " + ex.StackTrace); 
            }

            return transactionId; 
        }

        #endregion

        #region getTransaction

        public static Transaction getTransaction(int transactionId)
        {
            Transaction transaction = null;

            DataAccess da = new DataAccess();
            DataTable dt = da.select(String.Format("TransactionId = '{0}'", transactionId), "Transactions", NumRows : 1);

            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];

                int TransactionId = Convert.ToInt32(row["TransactionId"]);
                int TextbookId = Convert.ToInt32(row["TextbookId"]);
                int SellerId = Convert.ToInt32(row["SellerId"]);
                int BuyerId = Convert.ToInt32(row["BuyerId"]);
                int SellerPostId = Convert.ToInt32(row["SellerPostId"]);
                int BuyerPostId = Convert.ToInt32(row["BuyerPostId"]);
                int? FinalPrice = row["FinalPrice"] is DBNull ? (int?)null : Convert.ToInt32(row["FinalPrice"]);
                int? ConfirmPrice = row["ConfirmPrice"] is DBNull ? (int?)null : Convert.ToInt32(row["ConfirmPrice"]);
                int InitialPrice = Convert.ToInt32(row["InitialPrice"]);
                Confirmed Confirmed = (Confirmed) Convert.ToInt32(row["Confirmed"]);
                int IsActive = Convert.ToInt32(row["IsActive"]);
                int IsDeleted = Convert.ToInt32(row["IsDeleted"]);
                DateTime CreatedDate = Convert.ToDateTime(row["CreatedDate"]);
                DateTime ModifiedDate = Convert.ToDateTime(row["ModifiedDate"]); 
 
                transaction = new Transaction(
                    TransactionId,
                    TextbookId,
                    SellerId,
                    BuyerId,
                    SellerPostId,
                    BuyerPostId,
                    FinalPrice,
                    ConfirmPrice,
                    InitialPrice,
                    Confirmed,
                    IsActive,
                    IsDeleted,
                    CreatedDate,
                    ModifiedDate
                );
            }

            return transaction; 
        }

        #endregion

        #region CreateTransactionHistory

        public static int CreateTransactionHistory(int transactionId, int userId)
        {
            int transactionHistoryId = -1;

            try
            {
                DataAccess da = new DataAccess();

                Dictionary<string, object> transactionHistory = new Dictionary<string, object>();
                transactionHistory.Add("TransactionId", transactionId);
                transactionHistory.Add("UserId", userId);

                transactionHistoryId = da.insert(transactionHistory, "TransactionHistory");
            }
            catch (Exception ex)
            {
                Console.Write("ERROR: An error occured in adding a new transaction --- " + ex.Message + "          " + ex.StackTrace);
            }

            return transactionHistoryId;
        }

        #endregion

        #region getUserTransactionHistory

        public static IEnumerable<Transaction> getUserTransactionHistory(int UserId)
        {
            DataAccess da = new DataAccess();
            List<int> transactionIds = da.select(String.Format("UserId = '{0}'", UserId), "TransactionHistory").AsEnumerable().Select(x => (int)x["TransactionId"]).ToList();
            string Ids = string.Join(",", transactionIds);
            DataTable dt = da.select(String.Format("TransactionId IN ({0})", Ids), "Transactions"); 
            IEnumerable<Transaction>  transactions = dt.AsEnumerable().Select(
                                                        x => new Transaction(
                                                            Convert.ToInt32(x["TransactionId"]),
                                                            Convert.ToInt32(x["TextbookId"]),
                                                            Convert.ToInt32(x["SellerId"]),
                                                            Convert.ToInt32(x["BuyerId"]),
                                                            Convert.ToInt32(x["SellerPostId"]),
                                                            Convert.ToInt32(x["BuyerPostId"]),
                                                            x["FinalPrice"] is DBNull ? (int?) null : Convert.ToInt32(x["FinalPrice"]),
                                                            x["ConfirmPrice"] is DBNull ? (int?)null : Convert.ToInt32(x["ConfirmPrice"]),
                                                            Convert.ToInt32(x["InitialPrice"]),
                                                            (Confirmed) Convert.ToInt32(x["Confirmed"]),
                                                            Convert.ToInt32(x["IsActive"]),
                                                            Convert.ToInt32(x["IsDeleted"]),
                                                            Convert.ToDateTime(x["CreatedDate"]),
                                                            Convert.ToDateTime(x["ModifiedDate"]))).ToList();
 
            return transactions; 
        }

        #endregion

        #region UpdateTransaction

        public static void UpdateTransaction(int transactionId, Dictionary<string, object> updateDictionary)
        {
            DataAccess da = new DataAccess();
            da.update("Transactions", String.Format("TransactionId = {0}", transactionId), updateDictionary);
        }

        #endregion

        #region ConfirmTransaction

        public static bool ConfirmTransaction(Transaction transaction)
        {
            Dictionary<string, object> updateDictionary = new Dictionary<string, object>();

            if (transaction.FinalPrice != null && transaction.Confirmed == Confirmed.ByNone)
            {
                updateDictionary.Add("FinalPrice", (int)transaction.FinalPrice);
                
                Profile buyer = ProfileHandler.GetProfile(transaction.BuyerId);
                Profile seller = ProfileHandler.GetProfile(transaction.SellerId);

                Textbook book = TextbookHandler.getTextbook(transaction.TextbookId);
                if (transaction.CurrentUser == ActionBy.Buyer)
                {
                    updateDictionary.Add("Confirmed", (int)Confirmed.ByBuyer);
                    EmailUtility.SendEmail(
                        Convert.ToString(seller.Email),
                        Convert.ToString(seller.Name),
                        "Please confirm your transaction",
                        String.Format("Item: {0}<br/>{1} has confirmed a transaction with you!<br/>" +
                            "Please confirm it yourself to finalize the transaction.",
                            book.BookTitle,
                            buyer.Name)
                    );
                }
                else
                {
                    updateDictionary.Add("Confirmed", (int)Confirmed.BySeller);
                    EmailUtility.SendEmail(
                        Convert.ToString(buyer.Email),
                        Convert.ToString(buyer.Name),
                        "Please confirm your transaction",
                        String.Format("Item: {0}<br/>{1} has confirmed a transaction with you!<br/>" +
                            "Please confirm it yourself to finalize the transaction.",
                            book.BookTitle,
                            seller.Name)
                    );
                }
            }
            else if (transaction.FinalPrice != null)
            {
                updateDictionary.Add("ConfirmPrice", (int)transaction.FinalPrice);
                updateDictionary.Add("Confirmed", (int)Confirmed.ByBoth);
                updateDictionary.Add("IsActive", 0);

                PostHandler.updatePostState(transaction.SellerPostId, 0, 0);
                PostHandler.updatePostState(transaction.BuyerPostId, 0, 0);

                Profile buyer = ProfileHandler.GetProfile(transaction.BuyerId);
                Profile seller = ProfileHandler.GetProfile(transaction.SellerId);

                Textbook book = TextbookHandler.getTextbook(transaction.TextbookId);
                EmailUtility.SendEmail(
                    Convert.ToString(buyer.Email),
                    Convert.ToString(buyer.Name),
                    "Your transaction is now complete",
                    String.Format("Item: {0}<br/>Your transaction with {1} has finalized.<br/>" +
                        "Thank you for using BookSpade!",
                        book.BookTitle,
                        seller.Name)
                );

                EmailUtility.SendEmail(
                    Convert.ToString(seller.Email),
                    Convert.ToString(seller.Name),
                    "Your transaction is now complete",
                    String.Format("Item: {0}<br/>Your transaction with {1} has finalized.<br/>" +
                        "Thank you for using BookSpade!",
                        book.BookTitle,
                        buyer.Name)
                );
            }
            
            UpdateTransaction(transaction.TransactionId, updateDictionary);
            return true;
        }

        #endregion

        #region CancelTransaction

        public static bool CancelTransaction(Transaction transaction)
        {
            bool success = CreateForbiddenMatch(transaction.BuyerPostId, transaction.SellerPostId) > 0
                && CreateForbiddenMatch(transaction.SellerPostId, transaction.BuyerPostId) > 0;

            if (success)
            {
                Dictionary<string, object> updateDictionary = new Dictionary<string, object>();
                updateDictionary.Add("IsDeleted", 1);
                UpdateTransaction(transaction.TransactionId, updateDictionary);

                PostHandler.updatePostState(transaction.SellerPostId, 0);
                PostHandler.updatePostState(transaction.BuyerPostId, 0);

                Profile buyer = ProfileHandler.GetProfile(transaction.BuyerId);
                Profile seller = ProfileHandler.GetProfile(transaction.SellerId);

                Textbook book = TextbookHandler.getTextbook(transaction.TextbookId);
                EmailUtility.SendEmail(
                    Convert.ToString(buyer.Email),
                    Convert.ToString(buyer.Name),
                    "Your transaction has been cancelled",
                    String.Format("Item: {0}</br>{1} has cancelled the transaction with you!<br/>" +
                        "We'll try to match you with someone else for this item.",
                        book.BookTitle,
                        seller.Name)
                );

                EmailUtility.SendEmail(
                    Convert.ToString(seller.Email),
                    Convert.ToString(seller.Name),
                    "Your transaction has been cancelled",
                    String.Format("Item: {0}<br/>{1} has cancelled the transaction with you!<br/>" +
                        "We'll try to match you with someone else for this item.",
                        book.BookTitle,
                        buyer.Name)
                );

                Post sellerPost = PostHandler.getPost(transaction.SellerPostId);
                Post buyerPost = PostHandler.getPost(transaction.BuyerPostId);
                Task.Run(() => QueueWorker.AddPost(sellerPost));
                Task.Run(() => QueueWorker.AddPost(buyerPost));
            }

            return success;
        }

        #endregion

        #region CreateForbiddenMatch

        private static int CreateForbiddenMatch(int PostId, int MatchedPostId)
        {
            DataAccess da = new DataAccess();

            Dictionary<string, object> CancelledTransaction = new Dictionary<string, object>();
            CancelledTransaction.Add("PostId", PostId);
            CancelledTransaction.Add("MatchedPostId", MatchedPostId);

            return da.insert(CancelledTransaction, "CancelledTransactions"); 

        }

        #endregion

        #region AutoConfirmEmails

        public static void AutoConfirmEmails(string BuyerEmail, string Buyer, string SellerEmail, string Seller)
        {
            EmailUtility.SendEmail(
                    BuyerEmail,
                    Buyer,
                    String.Format("Transaction Confirmed!"),
                    String.Format("Your transaction with {0} has been confirmed in our system.<br /> We assume that your transaction has occured and you just forgot to confirm with us. ",
                        Seller)
                    );

            EmailUtility.SendEmail(
                   SellerEmail,
                   Seller,
                   String.Format("Transaction Confirmed!"),
                   String.Format("Your transaction with {0} has been confirmed in our system.<br /> We assume that your transaction has occured and you just forgot to confirm with us. ",
                       Buyer)
                   );
        }

        #endregion

        #region AutoConfirmTransaction 

        /// ** VALUES SET ** 
        /// After 14 day since a transaction has occured set the following:
        /// PostTable: IsActive = 0, IsTransacting = 0 [for each participant]
        /// Transaction Table: IsActive = 0, confirmed = 3
        /// 

        public static void AutoConfirmTransaction()
        {
            DataAccess da = new DataAccess();
            DataTable dt = da.ExecuteStoredProc("AutoConfirmTransaction");

            dt.AsEnumerable().ToList().ForEach(x =>
                AutoConfirmEmails(
                    Convert.ToString(x["BuyerEmail"]),
                    Convert.ToString(x["Buyer"]), 
                    Convert.ToString(x["SellerEmail"]),
                    Convert.ToString(x["Seller"])
                    )
                ); 
        }

        #endregion

    }
}