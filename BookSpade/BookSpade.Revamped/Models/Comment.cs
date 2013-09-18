using BookSpade.Revamped.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookSpade.Revamped.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string comment { get; set; } 
        public int UserId { get; set; }
        public ActionBy ActionBy { get; set; }
        public int TransactionId { get; set; }
        public int IsActive { get; set; }
        public int IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        public Profile CommentatorProfile { get; set; }

        public Comment(
            int commentId,
            string comment,
            int userId, 
            ActionBy actionBy, 
            int transactionId,
            int isActive,
            int isDeleted,
            DateTime createdDate,
            DateTime modifiedDate
            )
        {
            CommentId = commentId;
            UserId = userId;
            ActionBy = actionBy;
            TransactionId = transactionId;
            this.comment = comment;
            IsActive = isActive;
            IsDeleted = isDeleted;
            CreatedDate = createdDate;
            ModifiedDate = modifiedDate;
            CommentatorProfile = BookSpade.Revamped.Handlers.ProfileHandler.GetProfile(UserId);
        }
    }
}