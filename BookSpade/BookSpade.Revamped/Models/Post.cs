using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookSpade.Revamped.Models
{
    //ActionBy:
    // 0 => Seller
    // 1 => Buyer
    //Change to Enum if you'd like 

    public class Post
    { 
        public int PostId { get; set; }
        public int ProfileId { get; set; }
        public int TextBookId { get; set; }
        public ActionBy ActionBy { get; set; }
        public decimal Price { get; set; }
        public string BookCondition { get; set; }
        public int IsActive { get; set; }
        public int IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        public Post(
                int postId,
                int profileId,
                int textBookId,
                ActionBy actionBy,
                decimal price,
                string bookCondition,
                int isActive,
                int isDeleted,
                DateTime createdDate,
                DateTime modifiedDate)
        {
            PostId = postId;
            ProfileId = profileId;
            TextBookId = textBookId;
            ActionBy = actionBy;
            Price = price;
            BookCondition = bookCondition;
            IsActive = isActive;
            IsDeleted = isDeleted;
            CreatedDate = createdDate;
            ModifiedDate = modifiedDate;
        }

        public Profile PosterProfile() {
            return BookSpade.Revamped.Handlers.ProfileHandler.GetProfile(this.ProfileId); 
        }

    }
}