using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookSpade.Revamped.Models;
using BookSpade.Revamped.DAL;
using System.Data; 

namespace BookSpade.Revamped.Handlers
{
    public class TextbookHandler
    {
        #region CreateBook

        public static int createTextBook(Textbook book)
        {
            int BookId = -1;

            try
            {
                DataAccess da = new DataAccess();
                DataTable dt = da.select(String.Format("ISBN = '{0}'", book.ISBN), "TextBooks");

                if (dt == null || dt.Rows.Count == 0)
                {

                    int courseId = !string.IsNullOrEmpty(book.NewCourseName) ? CourseHandler.CreateCourse(book.NewCourseName) : book.CourseId.Value;

                    Dictionary<string, string> textbook = new Dictionary<string, string>();
                    textbook.Add("ISBN", book.ISBN);
                    textbook.Add("BookTitle", book.BookTitle);
                    textbook.Add("Author", book.Author);
                    textbook.Add("CourseId", courseId.ToString());
                    textbook.Add("BookImageURL", book.BookImageUrl);
                    textbook.Add("StorePrice", book.StorePrice.ToString());
                    textbook.Add("IsActive", "1");
                    textbook.Add("IsDeleted", "0");
                    textbook.Add("CreatedDate", DateTime.Now.ToString());
                    textbook.Add("ModifiedDate", DateTime.Now.ToString());

                }
            }
            catch (Exception ex) { Console.Write(ex.Message + "    " + ex.StackTrace); }

            return BookId; 
        }

        #endregion

        #region getTextbook

        public static Textbook getTextbook(int textbookId)
        {
            Textbook book = null;

            try
            {
                DataAccess da = new DataAccess();
                DataTable dt = da.select(String.Format("TextBookId = '{0}'", textbookId), "TextBooks");

                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    int BookId = (int)row["TextBookId"];
                    string BookTitle = (string)row["BookTitle"];
                    string ISBN = (string)row["ISBN"];
                    string Author = (string)row["Author"];
                    int CourseId = (int)row["CourseId"];
                    string BookImageUrl = Convert.ToString(row["BookImageURL"]);
                    decimal StorePrice = (decimal)row["StorePrice"];
                    int IsActive = (int)row["IsActive"];
                    int IsDeleted = (int)row["IsDeleted"];
                    DateTime CreatedDate = (DateTime)row["CreatedDate"];
                    DateTime ModifiedDate = (DateTime)row["ModifiedDate"];

                    book = new Textbook(BookId,
                        BookTitle,
                        ISBN,
                        Author,
                        CourseId,
                        null,
                        BookImageUrl,
                        StorePrice,
                        IsActive,
                        IsDeleted,
                        CreatedDate,
                        ModifiedDate); 
                }

            }
            catch (Exception ex) { }

            return book; 
        }

        #endregion
    }
}