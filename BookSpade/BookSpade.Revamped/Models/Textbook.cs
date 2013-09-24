using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookSpade.Revamped.Models
{
    public class Textbook
    {
        public int TextBookId { get; set; }
        public string BookTitle { get; set; } 
        public string ISBN { get; set; }
        public string Author { get; set; }
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public string BookImageUrl { get; set; }
        public decimal? StorePrice { get; set; }
        public int IsActive { get; set; }
        public int IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; } 

        public Textbook(
                int textbookId,
                string bookTitle,
                string isbn,
                string author,
                int courseId,
                string courseName,
                string bookImageUrl,
                decimal? storePrice,
                int isActive,
                int isDeleted,
                DateTime createdDate,
                DateTime modifiedDate
            )
        {
            TextBookId = textbookId;
            BookTitle = bookTitle; 
            ISBN = isbn;
            Author = author;
            CourseId = courseId;
            CourseName = courseName;
            BookImageUrl = bookImageUrl;
            StorePrice = storePrice;
            IsActive = isActive;
            IsDeleted = isDeleted;
            CreatedDate = createdDate;
            ModifiedDate = modifiedDate; 
        }
    }
}