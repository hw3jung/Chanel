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

                transactionId = da.insert(Transaction, "Transactions");
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
            DataTable dt = da.select(String.Format("TransactionId = '{0}'", transactionId), "Transactions");

            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];

                int TransactionId = Convert.ToInt32(row["TransactionId"]);
                int TextbookId = Convert.ToInt32(row["TextbookId"]);
                int SellerPostId = Convert.ToInt32(row["SellerPostId"]);
                int BuyerPostId = Convert.ToInt32(row["BuyerPostId"]);
                int CommentId = Convert.ToInt32(row["CommentId"]);
                decimal FinalPrice = Convert.ToDecimal(row["FinalPrice"]);
                decimal SellerPrice = Convert.ToDecimal(row["SellerPrice"]);
                int IsActive = Convert.ToInt32(row["IsActive"]);
                int IsDeleted = Convert.ToInt32(row["IsDeleted"]);
                DateTime CreatedDate = Convert.ToDateTime(row["CreatedDate"]);
                DateTime ModifiedDate = Convert.ToDateTime(row["ModifiedDate"]); 
 
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
                                                            Convert.ToInt32(x["SellerPostId"]),
                                                            Convert.ToInt32(x["BuyerPostId"]),
                                                            x["CommentId"] is DBNull ? (int?)null : Convert.ToInt32(x["CommentId"]),
                                                            x["FinalPrice"] is DBNull ? (decimal?)null : Convert.ToDecimal(x["FinalPrice"]),
                                                            Convert.ToDecimal(x["SellerPrice"]),
                                                            Convert.ToInt32(x["IsActive"]),
                                                            Convert.ToInt32(x["IsDeleted"]),
                                                            Convert.ToDateTime(x["CreatedDate"]),
                                                            Convert.ToDateTime(x["ModifiedDate"]))).ToList();
 
            return transactions; 
        }

        #endregion


    }
}