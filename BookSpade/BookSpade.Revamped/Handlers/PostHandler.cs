using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookSpade.Revamped.DAL;
using BookSpade.Revamped.Models;
using System.Data; 

namespace BookSpade.Revamped.Handlers
{
    public class PostHandler
    {
        #region createPost 

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

        #endregion

        #region getPost

        public static Post getPost(int PostId)
        {
            Post post = null;

            try
            {
                DataAccess da = new DataAccess();
                DataTable dt = da.select(String.Format("PostId = '{0}'", PostId), "Posts");

                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    int ProfileId = Convert.ToInt32(row["UserId"]);
                    int TextBookId = Convert.ToInt32(row["TextBookId"]);
                    ActionBy ActionBy = (ActionBy)Convert.ToInt32(row["ActionBy"]);
                    decimal Price = Convert.ToDecimal(row["Price"]);
                    string BookCondition = Convert.ToString(row["BookCondition"]);
                    int IsActive = Convert.ToInt32(row["IsActive"]);
                    int IsDeleted = Convert.ToInt32(row["IsDeleted"]);
                    DateTime CreatedDate = (DateTime)row["CreatedDate"];
                    DateTime ModifiedDate = (DateTime)row["ModifiedDate"];

                    post = new Post(
                        PostId,
                        ProfileId,
                        TextBookId,
                        ActionBy,
                        Price,
                        BookCondition,
                        IsActive,
                        IsDeleted,
                        CreatedDate,
                        ModifiedDate); 

                }
                
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message + "   " + ex.StackTrace); 
            }

            return post; 
        }
        #endregion

    }
}