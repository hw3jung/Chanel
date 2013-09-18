using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookSpade.Revamped.Models;
using BookSpade.Revamped.Handlers;

namespace BookSpade.Revamped.Utilities
{
    /*
    * Driver of the match making system
    */
    public class Matchmaker
    {
        /*
         * Given a new post, match it with the most appropriate post (if possible).
         * 
         * Conditions
         * ----------
         * 
         * If the new post was made by a buyer, then the matching post must have:
         *      -price less than or equal to that specified by buyer
         *      -book condition better than or equal to that specified by buyer
         *      
         * If the new post was made by a seller, then the matching post must have:
         *      -price greater than or equal to that specified by seller
         *      -book condition worse than or equal to that specified by seller
         * 
         */
        public static void Match(Post newPost)
        {
            int postId = PostHandler.createPost(newPost);
            newPost.PostId = postId;

            Post matchingPost = PostHandler.findMatchingPost(newPost);
            if (matchingPost != null)
            {
                int buyerPostId = -1;
                int sellerPostId = -1;
                
                int buyerUserId = -1;
                int sellerUserId = -1;

             
                decimal initialPrice;

                if (newPost.ActionBy == ActionBy.Buyer)
                {
                    buyerPostId = newPost.PostId;
                    sellerPostId = matchingPost.PostId;

                    buyerUserId = newPost.UserId;
                    sellerUserId = matchingPost.UserId;

                    initialPrice = matchingPost.Price;
                }
                else 
                {
                    buyerPostId = matchingPost.PostId;
                    sellerPostId = newPost.PostId;

                    buyerUserId = matchingPost.UserId;
                    sellerUserId = newPost.UserId;

                    initialPrice = newPost.Price;
                }

                // Create transaction and transactionhistory for the matched buyer & seller
                Transaction newTransaction = new Transaction(
                    -1, // id doesnt matter here
                    newPost.TextBookId,
                    sellerUserId,
                    buyerUserId,
                    sellerPostId,
                    buyerPostId,
                    null,
                    initialPrice,
                    1,
                    0,
                    DateTime.Now,
                    DateTime.Now
                );

                int transactionId = TransactionHandler.CreateTransaction(newTransaction);
                newTransaction.TransactionId = transactionId;

                TransactionHandler.CreateTransactionHistory(transactionId, buyerUserId);
                TransactionHandler.CreateTransactionHistory(transactionId, sellerUserId);

                PostHandler.updatePostState(newPost.PostId, 1);
                PostHandler.updatePostState(matchingPost.PostId, 1);
            }
        }
    }
}