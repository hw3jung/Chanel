using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookSpade.Revamped.Handlers; 
using BookSpade.Revamped.Utilities;

namespace BookSpade.Revamped.Models
{
    public class TransactionCommentModel
    {
        public TransactionDetailModel Details { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
        public int UserId { get; set; }
        public int CounterPartyId { get; set; }
        public Profile profile { get; set; }
        public ActionBy UserAction { get; set; }
        public string UserDisplayName { get; set; }
        public string CounterPartyDisplayName { get; set; }
        public string UserFacebookId { get; set; }
        public string CounterPartyFacebookId { get; set; }

        public TransactionCommentModel(Transaction transaction, string userName)
        {
            Details = new TransactionDetailModel(transaction);
            profile = ProfileHandler.GetProfile(userName);
            UserId = profile.ProfileId;
            UserDisplayName = profile.Name;
            UserFacebookId = profile.FacebookId;

            if (UserId == transaction.BuyerId)
            {
                CounterPartyId = transaction.SellerId;
                UserAction = ActionBy.Buyer;
            }
            else
            {
                CounterPartyId = transaction.BuyerId;
                UserAction = ActionBy.Seller; 
            }

            Profile counterPartyProfile = ProfileHandler.GetProfile(CounterPartyId);
            CounterPartyDisplayName = counterPartyProfile.Name;
            CounterPartyFacebookId = counterPartyProfile.FacebookId;

            // load comments
            Comments = CommentHandler.getComments(transaction.TransactionId);
            foreach (Comment comment in Comments)
            {
                if (comment.UserId == UserId)
                {
                    comment.CommentByCurrentUser = true;
                }
                else
                {
                    comment.CommentByCurrentUser = false;
                }
            }
        }

        public TransactionCommentModel()
        {

        }

    }
}