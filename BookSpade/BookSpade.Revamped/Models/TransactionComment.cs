using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookSpade.Revamped.Models
{
   

    public class TransactionComment
    {
        public int CommentId { get; set; }
        public string Comment { get; set; }
        public int TransactionId { get; set; }
        public ActionBy ActionBy { get; set; }
        public TransactionDetailModel Details { get; set; } 

        public TransactionComment(int commentId, string comment, ActionBy actionBy, Transaction transaction)
        {
            CommentId = commentId;
            Comment = comment;
            TransactionId = transaction.TransactionId;
            Details = new TransactionDetailModel(transaction); 
        }
    }
}