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

        public TransactionCommentModel(Transaction transaction)
        {
            Details = new TransactionDetailModel(transaction);
            Comments = CommentHandler.getComments(transaction.TransactionId); 
        }
    }
}