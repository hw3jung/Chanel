using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using BookSpade.Revamped.Utilities;

namespace BookSpade.Revamped.Models
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public int TextbookId { get; set; }
        public int SellerId { get; set; }
        public int BuyerId { get; set; } 
        public int SellerPostId { get; set; }
        public int BuyerPostId { get; set; }
        public int? FinalPrice { get; set; }
        public int? ConfirmPrice { get; set; }
        public int InitialPrice { get; set; }
        public Confirmed Confirmed { get; set; }
        public int IsActive { get; set; }
        public int IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        public ActionBy CurrentUser { get; set; }
        public bool ConfirmedByCurrentUser { get; set; }
        public string CounterPartyName { get; set; }

        // how many days are left before this transaction is auto confirmed
        // default transaction period is 14 days
        public int DaysRemaining { get; set; }

        public Transaction(
            int transactionId,
            int textbookId,
            int sellerId,
            int buyerId,
            int sellerPostId,
            int buyerPostId,
            int? finalPrice,
            int? confirmPrice,
            int initialPrice,
            Confirmed confirmed,
            int isActive,
            int isDeleted,
            DateTime createdDate,
            DateTime modifiedDate
            )
        {
            this.TransactionId = transactionId;
            this.TextbookId = textbookId;
            this.SellerId = sellerId;
            this.BuyerId = buyerId;
            this.SellerPostId = sellerPostId;
            this.BuyerPostId = buyerPostId;
            this.FinalPrice = finalPrice;
            this.ConfirmPrice = confirmPrice;
            this.InitialPrice = initialPrice;
            this.Confirmed = confirmed; 
            this.IsActive = isActive;
            this.IsDeleted = isDeleted;
            this.CreatedDate = createdDate;
            this.ModifiedDate = modifiedDate;

            // calculate how many days are left before this transaction is auto confirmed
            // default transaction period is 14 days
            DateTime deadline = this.CreatedDate.AddDays(Globals.TRANSACTION_PERIOD);
            this.DaysRemaining = (deadline - DateTime.Now).Days + 1;
        }

        public Transaction() { }
    }
}