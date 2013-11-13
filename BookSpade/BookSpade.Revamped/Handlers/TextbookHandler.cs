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
                DataTable dt = da.select(String.Format("ISBN = '{0}'", book.ISBN), "TextBooks", NumRows : 1);

                if (dt == null || dt.Rows.Count == 0)
                {
                    int courseId = !string.IsNullOrEmpty(book.CourseName) ? CourseHandler.CreateCourse(book.CourseName) : book.CourseId;

                    Dictionary<string, object> textbook = new Dictionary<string, object>();
                    textbook.Add("ISBN", book.ISBN);
                    textbook.Add("BookTitle", book.BookTitle);
                    textbook.Add("Author", book.Author);
                    textbook.Add("CourseId", courseId);
                    textbook.Add("BookImageURL", book.BookImageUrl);
                    textbook.Add("StorePrice", book.StorePrice);
                    textbook.Add("IsActive", 1);
                    textbook.Add("IsDeleted", 0);
                    textbook.Add("CreatedDate", DateTime.Now);
                    textbook.Add("ModifiedDate", DateTime.Now);

                    BookId = da.insert(textbook, "TextBooks");
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
                DataTable dt = da.select(String.Format("TextBookId = '{0}' AND IsActive = 1 AND IsDeleted = 0", textbookId), "TextBooks", NumRows : 1);

                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    string bookTitle = Convert.ToString(row["BookTitle"]);
                    string isbn = Convert.ToString(row["ISBN"]);
                    string author = Convert.ToString(row["Author"]);
                    string bookImageUrl = row["BookImageURL"] is DBNull ? null : Convert.ToString(row["BookImageURL"]);
                    
                    int courseId = Convert.ToInt32(row["CourseId"]);
                    string courseName = CourseHandler.getCourseName(courseId);
                    
                    decimal? storePrice = row["StorePrice"] is DBNull ? (decimal?)null : Convert.ToDecimal(row["StorePrice"]);
                    int isActive = Convert.ToInt32(row["IsActive"]);
                    int isDeleted = Convert.ToInt32(row["IsDeleted"]);
                    DateTime createdDate = Convert.ToDateTime(row["CreatedDate"]);
                    DateTime modifiedDate = Convert.ToDateTime(row["ModifiedDate"]);

                    book = new Textbook(
                        textbookId,
                        bookTitle,
                        isbn,
                        author,
                        courseId,
                        courseName,
                        bookImageUrl,
                        storePrice,
                        isActive,
                        isDeleted,
                        createdDate,
                        modifiedDate
                    );
                }

            }
            catch (Exception ex) { Console.Write(ex.Message + " " + ex.StackTrace); }

            return book; 
        }

        #endregion

        # region getAllTextbooks

        public static IEnumerable<Textbook> getAllTextbooks()
        {
            List<Textbook> textbooks = new List<Textbook>();

            try
            {
                DataAccess DAL = new DataAccess();
                DataTable dt = DAL.select("", "TextBooks");

                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        int textbookId = Convert.ToInt32(row["TextBookId"]);
                        string bookTitle = Convert.ToString(row["BookTitle"]);
                        string isbn = Convert.ToString(row["ISBN"]);
                        string author = Convert.ToString(row["Author"]);
                        string bookImageUrl = row["BookImageURL"] is DBNull ? null : Convert.ToString(row["BookImageURL"]);

                        int courseId = Convert.ToInt32(row["CourseId"]);
                        string courseName = CourseHandler.getCourseName(courseId);

                        decimal? storePrice = row["StorePrice"] is DBNull ? (decimal?) null : Convert.ToDecimal(row["StorePrice"]);
                        int isActive = Convert.ToInt32(row["IsActive"]);
                        int isDeleted = Convert.ToInt32(row["IsDeleted"]);
                        DateTime createdDate = Convert.ToDateTime(row["CreatedDate"]);
                        DateTime modifiedDate = Convert.ToDateTime(row["ModifiedDate"]);

                        Textbook textbook = new Textbook(
                            textbookId,
                            bookTitle,
                            isbn,
                            author,
                            courseId,
                            courseName,
                            bookImageUrl,
                            storePrice,
                            isActive,
                            isDeleted,
                            createdDate,
                            modifiedDate
                        );
                        textbooks.Add(textbook);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write("ERROR: An error occured in retrieving all textbooks --- " + ex.Message);
            }

            return textbooks;
        }

        #endregion
    }
}