using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
        public decimal? FinalPrice { get; set; }
        public decimal InitialPrice { get; set; }
        public int IsActive { get; set; }
        public int IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; } 

        public Transaction(
            int transactionId,
            int textbookId,
            int sellerId,
            int buyerId,
            int sellerPostId,
            int buyerPostId,
            decimal? finalPrice,
            decimal initialPrice,
            int isActive,
            int isDeleted,
            DateTime createdDate,
            DateTime modifiedDate
            )
        {
            TransactionId = transactionId;
            TextbookId = textbookId;
            SellerId = sellerId;
            BuyerId = buyerId; 
            SellerPostId = sellerPostId;
            BuyerPostId = buyerPostId;
            FinalPrice = FinalPrice;
            InitialPrice = initialPrice;
            IsActive = isActive;
            IsDeleted = isDeleted; 
            CreatedDate = createdDate;
            ModifiedDate = modifiedDate; 
        }
    }
}