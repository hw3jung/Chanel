using BookSpade.Revamped.DAL;
using BookSpade.Revamped.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq; 

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

                Dictionary<string, string> Transaction = new Dictionary<string, string>();
                Transaction.Add("TextbookId", transaction.TextbookId.ToString());
                Transaction.Add("SellerPostId", transaction.SellerPostId.ToString());
                Transaction.Add("BuyerPostId", transaction.BuyerPostId.ToString());
                Transaction.Add("CommentId", transaction.CommentId.ToString());
                Transaction.Add("FinalPrice", transaction.FinalPrice.ToString());
                Transaction.Add("SellerPrice", transaction.SellerPrice.ToString());
                Transaction.Add("IsActive", transaction.IsActive.ToString());
                Transaction.Add("IsDeleted", transaction.IsDeleted.ToString());
                Transaction.Add("CreatedDate", transaction.CreatedDate.ToString());
                Transaction.Add("ModifiedDate", transaction.ModifiedDate.ToString());

                transactionId = da.insert(Transaction, "Transaction");
            }
            catch (Exception ex)
            {
                Console.Write("ERROR: An error occured in adding a new transaction --- " + ex.Message + "          " + ex.StackTrace); 
            }

            return transactionId; 
        }


        #endregion

        #region getTransactionDe

        public static Transaction getTransaction(int transactionId)
        {
            Transaction transaction = null;

            DataAccess da = new DataAccess();
            DataTable dt = da.select(String.Format("TransactionId = '{0}'", transactionId), "Transaction");

            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];

                int TransactionId = (int)row["TransactionId"];
                int TextbookId = (int)row["TextbookId"];
                int SellerPostId = (int)row["SellerPostId"];
                int BuyerPostId = (int)row["BuyerPostId"];
                int CommentId = (int)(row["CommentId"]);
                decimal FinalPrice = (decimal)(row["FinalPrice"]);
                decimal SellerPrice = (decimal)(row["SellerPrice"]);
                int IsActive = (int)(row["IsActive"]);
                int IsDeleted = (int)(row["IsDeleted"]);
                DateTime CreatedDate = (DateTime)(row["CreatedDate"]);
                DateTime ModifiedDate = (DateTime)(row["ModifiedDate"]); 
 
                transaction = new Transaction(
                    TransactionId,
                    TextbookId,
                    SellerPostId,
                    BuyerPostId,
                    CommentId,
                    FinalPrice,
                    SellerPrice,
                    IsActive,
                    IsDeleted,
                    CreatedDate,
                    ModifiedDate
                    ); 
            }

            return transaction; 
        }

        #endregion

        #region getUserTransactionHistory

        public static IEnumerable<Transaction> getUserTransactionHistory(int ProfileId)
        {
            DataAccess da = new DataAccess();
            List<int> transactionIds = da.select(String.Format("ProfileId = '{0}'", ProfileId), "TransactionHistory").AsEnumerable().Select(x => (int)x["TransactionId"]).ToList();
            string Ids = string.Join(",", transactionIds); 
            DataTable dt = da.select(String.Format("TransactionId IN '({0})'", Ids), "Transaction"); 
            IEnumerable<Transaction>  transactions = dt.AsEnumerable().Select(
                                                        x => new Transaction(
                                                            (int)x["TransactionId"],
                                                            (int)x["TextbookId"],
                                                            (int)x["SellerPostId"],
                                                            (int)x["BuyerPostId"],
                                                            (int)x["CommentId"],
                                                            (decimal)x["FinalPrice"],
                                                            (decimal)x["SellerPrice"],
                                                            (int)x["IsActive"],
                                                            (int)x["IsDeleted"],
                                                            (DateTime)x["CreatedDate"],
                                                            (DateTime)x["ModifiedDate"]));
 
            return transactions; 
        }

        #endregion
    }
}