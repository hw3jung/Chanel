﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookSpade.Revamped.Handlers;
using BookSpade.Revamped.Utilities;

namespace BookSpade.Revamped.Models
{
    public class TransactionDetailModel
    {
        public string BookTitle { get; set; }
        public string ISBN { get; set; }
        public decimal Price { get; set; }
        public decimal? FinalPrice { get; set; }
        public BookCondition Condition { get; set; }
        public string CourseName { get; set; }
        public decimal? StorePrice { get; set; }

        public Post SellerPost { get; set; }
        public Post BuyerPost { get; set; }
        public Transaction transaction { get; set; }

        public TransactionDetailModel(Transaction transaction)
        {
            Textbook textbook = TextbookHandler.getTextbook(transaction.TextbookId);
            SellerPost = PostHandler.getPost(transaction.SellerPostId);
            BuyerPost = PostHandler.getPost(transaction.BuyerPostId); 

            this.BookTitle = textbook.BookTitle;
            this.ISBN = textbook.ISBN;
            this.Price = SellerPost.Price;
            this.FinalPrice = transaction.FinalPrice;
            this.Condition = SellerPost.BookCondition;
            this.CourseName = CourseHandler.getCourseName(textbook.CourseId);
            this.StorePrice = textbook.StorePrice;
            this.transaction = transaction;
        }

        public TransactionDetailModel()
        {
        }

    }
}