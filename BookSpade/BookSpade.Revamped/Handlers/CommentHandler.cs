using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookSpade.Revamped.Models;
using BookSpade.Revamped.DAL;
using System.Data; 

namespace BookSpade.Revamped.Handlers
{
    public class CommentHandler
    {
        #region createComment

        public static int createComment(Comment comment)
        {
            int id = -1;

            try
            {
                DataAccess da = new DataAccess();

                Dictionary<string, string> TransactionComment = new Dictionary<string, string>();
                TransactionComment.Add("UserId", Convert.ToString(comment.UserId));
                TransactionComment.Add("ActionBy", Convert.ToString(comment.ActionBy));
                TransactionComment.Add("TransactionId ", Convert.ToString(comment.TransactionId));
                TransactionComment.Add("Comment", Convert.ToString(comment.comment));
                TransactionComment.Add("IsActive", "1");
                TransactionComment.Add("IsDeleted", "0");
                TransactionComment.Add("CreatedDate", Convert.ToString(DateTime.Now));
                TransactionComment.Add("ModifiedDate", Convert.ToString(DateTime.Now));

                id = da.insert(TransactionComment, "TransactionComments");
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message + "    " + ex.StackTrace);
            }

            return id;
        }

        #endregion

        #region getComments

        public static IEnumerable<Comment> getComments(int TransactionId)
        {
            IEnumerable<Comment> comments = new List<Comment>();

            DataAccess da = new DataAccess();
            DataTable dt = da.select(String.Format("TransactionId = '{0}'", TransactionId), "TransactionComments");
            IEnumerable<Comment> transactions = dt.AsEnumerable().Select(
                                                        x => new Comment(
                                                            Convert.ToInt32(x["CommentId"]),
                                                            Convert.ToString(x["Comment"]),
                                                            Convert.ToInt32(x["UserId"]),
                                                            (ActionBy)Convert.ToInt32(x["ActionBy"]),
                                                            Convert.ToInt32(x["TransactionId"]),
                                                            Convert.ToInt32(x["IsActive"]),
                                                            Convert.ToInt32(x["IsDeleted"]),
                                                            Convert.ToDateTime(x["CreatedDate"]),
                                                            Convert.ToDateTime(x["ModifiedDate"]))).OrderBy(x => x.CreatedDate).ToList();

            return comments; 
        }

        #endregion

    }
}