using BookSpade.Revamped.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookSpade.Revamped.Models
{
    public class Post
    { 
        public int PostId { get; set; }
        public int UserId { get; set; }
        public int TextBookId { get; set; }
        public ActionBy ActionBy { get; set; }
        public decimal Price { get; set; }
        public BookCondition BookCondition { get; set; }
        public int IsTransacting { get; set; }
        public int IsActive { get; set; }
        public int IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        public Post(
                int postId,
                int userId,
                int textBookId,
                ActionBy actionBy,
                decimal price,
                BookCondition bookCondition,
                int isTransacting,
                int isActive,
                int isDeleted,
                DateTime createdDate,
                DateTime modifiedDate)
        {
            PostId = postId;
            UserId = userId;
            TextBookId = textBookId;
            ActionBy = actionBy;
            Price = price;
            BookCondition = bookCondition;
            IsTransacting = isTransacting;
            IsActive = isActive;
            IsDeleted = isDeleted;
            CreatedDate = createdDate;
            ModifiedDate = modifiedDate;
        }

        public Profile PosterProfile() {
            return BookSpade.Revamped.Handlers.ProfileHandler.GetProfile(this.UserId); 
        }

    }
}