using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookSpade.Revamped.DAL;
using BookSpade.Revamped.Models; 

namespace BookSpade.Revamped.Handlers
{
    public class PostHandler
    {
        public static int createPost(Post newPost)
        {
            int id = -1;

            try
            {
                DataAccess da = new DataAccess();

                Dictionary<string, string> post = new Dictionary<string, string>();
                post.Add("ProfileId", Convert.ToString(newPost.ProfileId));
                post.Add("TextBookId", Convert.ToString(newPost.TextBookId));
                post.Add("ActionBy ", Convert.ToString(newPost.ActionBy));
                post.Add("Price", Convert.ToString(newPost.Price));
                post.Add("BookCondition", newPost.BookCondition);
                post.Add("IsActive", "1");
                post.Add("IsDeleted", "0");
                post.Add("CreatedDate", Convert.ToString(DateTime.Now));
                post.Add("ModifiedDate", Convert.ToString(DateTime.Now));

                id = da.insert(post, "Posts"); 
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message + "    " + ex.StackTrace); 
            }

            return id; 
        }
    }
}