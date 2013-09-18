using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookSpade.Revamped.Handlers; 

namespace BookSpade.Revamped.Models
{
    public class TransactionCommentModel
    {
        public TransactionDetailModel Details { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
        public int UserId { get; set; }
        public int OtherPartyId { get; set; } 

        public TransactionCommentModel(Transaction transaction, string UserName)
        {
            Details = new TransactionDetailModel(transaction);
            Comments = CommentHandler.getComments(transaction.TransactionId);

            Profile profile = ProfileHandler.GetProfile(UserName);
            UserId = profile.ProfileId;

            if (UserId == transaction.BuyerId)
                OtherPartyId = transaction.SellerId;
            else
                OtherPartyId = transaction.BuyerId; 
        }
    }
}