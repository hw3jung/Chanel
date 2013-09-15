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
        public Profile Profile { get; set; } 
        public IEnumerable<TransactionDetailModel> TransactionModels { get; set; }

        public TransactionHistory(int transactionHistoryId, int profileId, IEnumerable<TransactionDetailModel> transactionModels)
        {
            TransactionHistoryId = transactionHistoryId;
            ProfileId = profileId;
            TransactionModels = transactionModels; 
        }
    }
}