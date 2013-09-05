using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
    
        public TransactionDetailModel(
            Transaction transaction, 
            Textbook book, 
            Post SellerPost,
            Course course)
        {
            BookTitle = book.BookTitle;
            ISBN = book.ISBN;
            Price = SellerPost.Price;
            FinalPrice = transaction.FinalPrice;
            Condition = SellerPost.BookCondition;
            CourseName = course.CourseName;
            StorePrice = book.StorePrice;

        }
    }
}