using System;
using System.Collections.Generic;
using System.Linq;
using BookSpade.Revamped.Models;
using BookSpade.Revamped.DAL;
using System.Data;
using BookSpade.Revamped.Utilities; 

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

                Dictionary<string, object> TransactionComment = new Dictionary<string, object>();
                TransactionComment.Add("UserId", comment.UserId);
                TransactionComment.Add("ActionBy", (int)comment.ActionBy);
                TransactionComment.Add("TransactionId ", comment.TransactionId);
                TransactionComment.Add("Comment", comment.comment);
                TransactionComment.Add("ReminderSent", 0);
                TransactionComment.Add("IsActive", 1);
                TransactionComment.Add("IsDeleted", 0);
                TransactionComment.Add("CreatedDate", DateTime.Now);
                TransactionComment.Add("ModifiedDate", DateTime.Now);

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
            DataAccess da = new DataAccess();
            DataTable dt = da.select(String.Format("TransactionId = '{0}' AND IsActive = 1 AND IsDeleted = 0", TransactionId), "TransactionComments");
            IEnumerable<Comment> comments = dt.AsEnumerable().Select(
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

        #region reminderMail 
        
        public static void commentReminderMail()
        {
            EmailUtil mailUtil = new EmailUtil(); 
            DataAccess da = new DataAccess();
            DataTable dt = da.ExecuteStoredProc("getUsersToEmail");
            dt.AsEnumerable().ToList().ForEach(x =>
                    mailUtil.SendNetMailMessage(
                        Convert.ToString(x["UserName"]),
                        Convert.ToString(x["DisplayName"]),
                        String.Format("{0} has sent you a message! ", Convert.ToString(x["Commentor_DisplayName"])),
                        String.Format("{0} has sent you a message: <br/> '{1}'",
                            Convert.ToString(x["Commentor_DisplayName"]),
                            Convert.ToString(x["Comment"]))
                        )
                   );  
        }

        #endregion
    }
}