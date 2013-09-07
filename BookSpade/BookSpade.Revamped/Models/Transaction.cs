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
        public int SellerPostId { get; set; }
        public int BuyerPostId { get; set; }
        public int? CommentId { get; set; }
        public decimal? FinalPrice { get; set; }
        public decimal SellerPrice { get; set; }
        public int IsActive { get; set; }
        public int IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; } 

        public Transaction(
            int transactionId,
            int textbookId,
            int sellerPostId,
            int buyerPostId,
            int? commentId,
            decimal? finalPrice,
            decimal sellerPrice,
            int isActive,
            int isDeleted,
            DateTime createdDate,
            DateTime modifiedDate
            )
        {
            TransactionId = transactionId;
            TextbookId = textbookId; 
            SellerPostId = sellerPostId;
            BuyerPostId = buyerPostId;
            CommentId = commentId;
            FinalPrice = FinalPrice;
            SellerPrice = sellerPrice;
            IsActive = isActive;
            IsDeleted = isDeleted; 
            CreatedDate = createdDate;
            ModifiedDate = modifiedDate; 
        }
    }
}