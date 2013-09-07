using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookSpade.Revamped.Handlers; 

namespace BookSpade.Revamped.Models
{
    public class TransactionDetailModel
    {
        public string BookTitle { get; set; }
        public string ISBN { get; set; }
        public decimal Price { get; set; }
        public decimal? FinalPrice { get; set; }
        public string Condition { get; set; }
        public string CourseName { get; set; }
        public decimal StorePrice { get; set; }

        public Post SellerPost { get; set; }
        public Post BuyerPost { get; set; }

        public TransactionDetailModel(Transaction transaction)
        {
            Textbook textbook = TextbookHandler.getTextbook(transaction.TextbookId);
            Course course = CourseHandler.getCourse(textbook.CourseId);
            SellerPost = PostHandler.getPost(transaction.SellerPostId);
            BuyerPost = PostHandler.getPost(transaction.BuyerPostId); 

            BookTitle = textbook.BookTitle;
            ISBN = textbook.ISBN;
            Price = SellerPost.Price;
            FinalPrice = transaction.FinalPrice;
            Condition = SellerPost.BookCondition;
            CourseName = course.CourseName;
            StorePrice = textbook.StorePrice;
        }
    }
}