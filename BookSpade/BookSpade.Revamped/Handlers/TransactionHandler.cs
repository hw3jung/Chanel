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

                Dictionary<string, object> Transaction = new Dictionary<string, object>();
                Transaction.Add("TextbookId", transaction.TextbookId);
                Transaction.Add("SellerId", transaction.SellerId);
                Transaction.Add("BuyerId", transaction.BuyerId);
                Transaction.Add("SellerPostId", transaction.SellerPostId);
                Transaction.Add("BuyerPostId", transaction.BuyerPostId);
                Transaction.Add("FinalPrice", transaction.FinalPrice);
                Transaction.Add("SellerPrice", transaction.InitialPrice);
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
            DataTable dt = da.select(String.Format("TransactionId = '{0}'", transactionId), "Transactions");

            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];

                int TransactionId = Convert.ToInt32(row["TransactionId"]);
                int TextbookId = Convert.ToInt32(row["TextbookId"]);
                int SellerId = Convert.ToInt32(row["SellerId"]);
                int BuyerId = Convert.ToInt32(row["BuyerId"]);
                int SellerPostId = Convert.ToInt32(row["SellerPostId"]);
                int BuyerPostId = Convert.ToInt32(row["BuyerPostId"]);
                decimal? FinalPrice = row["FinalPrice"] is DBNull ? (decimal?)null : Convert.ToDecimal(row["FinalPrice"]);
                decimal InitialPrice = Convert.ToDecimal(row["InitialPrice"]);
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
                    InitialPrice,
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
                                                            x["FinalPrice"] is DBNull ? (decimal?)null : Convert.ToDecimal(x["FinalPrice"]),
                                                            Convert.ToDecimal(x["InitialPrice"]),
                                                            Convert.ToInt32(x["IsActive"]),
                                                            Convert.ToInt32(x["IsDeleted"]),
                                                            Convert.ToDateTime(x["CreatedDate"]),
                                                            Convert.ToDateTime(x["ModifiedDate"]))).ToList();
 
            return transactions; 
        }

        #endregion


    }
}