using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookSpade.Revamped.Models
{
    public class TransactionHistory
    {
        public int TransactionHistoryId { get; set; }
        public int ProfileId { get; set; }
        public IEnumerable<TransactionDetailModel> TransactionModels { get; set; }

        public TransactionHistory(int thId, int pId, IEnumerable<TransactionDetailModel> transactionModels)
        {
            TransactionHistoryId = thId;
            ProfileId = pId;
            TransactionModels = transactionModels; 
        }
    }
}