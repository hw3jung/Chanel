using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookSpade.Revamped.DAL;
using BookSpade.Revamped.Models;
using System.Data;
using BookSpade.Revamped.Utilities; 

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

                Dictionary<string, object> post = new Dictionary<string, object>();
                post.Add("UserId", newPost.UserId);
                post.Add("TextBookId", newPost.TextBookId);
                post.Add("ActionBy ", (int) newPost.ActionBy); // enum must be casted to int before we can store it in db
                post.Add("Price", newPost.Price);
                post.Add("BookCondition", (int) newPost.BookCondition); // enum must be casted to int before we can store it in db
                post.Add("IsTransacting", newPost.IsTransacting);
                post.Add("IsActive", 1);
                post.Add("IsDeleted", 0);
                post.Add("CreatedDate", DateTime.Now);
                post.Add("ModifiedDate", DateTime.Now);

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

        public static Post getPost(int postId)
        {
            Post post = null;

            try
            {
                DataAccess da = new DataAccess();
                DataTable dt = da.select(String.Format("PostId = '{0}' AND IsActive = 1 AND IsDeleted = 0", postId), "Posts", NumRows : 1);

                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    int profileId = Convert.ToInt32(row["UserId"]);
                    int textbookId = Convert.ToInt32(row["TextBookId"]);
                    ActionBy actionBy = (ActionBy)Convert.ToInt32(row["ActionBy"]);
                    decimal price = Convert.ToDecimal(row["Price"]);
                    BookCondition bookCondition = (BookCondition)Convert.ToInt32(row["BookCondition"]);
                    int isTransacting = Convert.ToInt32(row["IsTransacting"]);
                    int isActive = Convert.ToInt32(row["IsActive"]);
                    int isDeleted = Convert.ToInt32(row["IsDeleted"]);
                    DateTime createdDate = (DateTime)row["CreatedDate"];
                    DateTime modifiedDate = (DateTime)row["ModifiedDate"];

                    post = new Post(
                        postId,
                        profileId,
                        textbookId,
                        actionBy,
                        price,
                        bookCondition,
                        isTransacting,
                        isActive,
                        isDeleted,
                        createdDate,
                        modifiedDate
                    );
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message + "   " + ex.StackTrace); 
            }

            return post; 
        }
        #endregion

        #region updatePostState

        public static bool updatePostState(int postId, int isTransacting, int? isActive = null)
        {
            bool success = true;

            try
            {
                Dictionary<string, object> updateDict = new Dictionary<string, object>();
                updateDict.Add("IsTransacting", isTransacting);

                if (isActive != null)
                {
                    updateDict.Add("IsActive", isActive);
                }

                DataAccess da = new DataAccess();
                da.update("Posts", String.Format("PostId = {0}", postId), updateDict);
            }
            catch (Exception e)
            {
                Console.Write("ERROR: Could not update post state with the given post id --- " + e.Message);
                success = false;
            }

            return success;
        }

        #endregion
        
        #region findMatchingPosts

        public static Post findMatchingPost(Post post)
        {
            Post matchingPost = null;

            try
            {
                var counterparty = post.ActionBy == ActionBy.Buyer ? ActionBy.Seller : ActionBy.Buyer;

                string query = String.Format("UserId <> {0} AND TextBookId = {1} AND ActionBy = {2} " +
                    "AND IsTransacting = 0 AND IsActive = 1 AND IsDeleted = 0",
                    post.UserId,
                    post.TextBookId,
                    (int)counterparty
                );

                List<SortColumn> sortColumns = new List<SortColumn>();
                if (post.ActionBy == ActionBy.Buyer) //buyer
                {
                    query += String.Format("AND Price <= {0} AND BookCondition >= {1}",
                        post.Price,
                        (int)post.BookCondition
                    );

                    sortColumns.Add(new SortColumn("Price", "ASC"));
                }
                else //seller
                {
                    query += String.Format("AND Price >= {0} AND BookCondition <= {1}",
                        post.Price,
                        (int)post.BookCondition
                    );

                    sortColumns.Add(new SortColumn("Price", "DESC"));
                }
                
                DataAccess da = new DataAccess();
                DataTable dt = da.select(query, "Posts", NumRows : 1, SortColumns : sortColumns);

                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    int postId = Convert.ToInt32(row["PostId"]);
                    int profileId = Convert.ToInt32(row["UserId"]);
                    int textBookId = Convert.ToInt32(row["TextBookId"]);
                    ActionBy actionBy = (ActionBy)Convert.ToInt32(row["ActionBy"]);
                    decimal price = Convert.ToDecimal(row["Price"]);
                    BookCondition bookCondition = (BookCondition)Convert.ToInt32(row["BookCondition"]);
                    int isTransacting = Convert.ToInt32(row["IsTransacting"]);
                    int isActive = Convert.ToInt32(row["IsActive"]);
                    int isDeleted = Convert.ToInt32(row["IsDeleted"]);
                    DateTime createdDate = (DateTime)row["CreatedDate"];
                    DateTime modifiedDate = (DateTime)row["ModifiedDate"];

                    matchingPost = new Post(
                        postId,
                        profileId,
                        textBookId,
                        actionBy,
                        price,
                        bookCondition,
                        isTransacting,
                        isActive,
                        isDeleted,
                        createdDate,
                        modifiedDate
                    );
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message + "   " + ex.StackTrace); 
            }

            return matchingPost;
        }
        #endregion
    }
}