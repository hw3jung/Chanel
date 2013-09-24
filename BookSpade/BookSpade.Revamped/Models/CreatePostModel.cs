using BookSpade.Revamped.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookSpade.Revamped.Models
{
    public class CreatePostModel
    {
        // Either we have the textbook info already
        public int TextBookId { get; set; }
        public int CourseId { get; set; }

        public bool IsNewBook { get; set; }

        // Or we are given new textbook info
        public string BookTitle { get; set; }
        public string ISBN { get; set; }
        public string Author { get; set; }
        public string CourseName { get; set; }
        public string BookImageUrl { get; set; }

        public ActionBy ActionBy { get; set; }
            
        [Required(ErrorMessage = "Please enter the desired minimum/maximum price for the book.")]
        public decimal Price { get; set; }

        public BookCondition BookCondition { get; set; }
        public bool IsNegotiable { get; set; }

        public IEnumerable<Textbook> Textbooks { get; set; }
        public IEnumerable<SelectListItem> PostTypes { get; set; }
        public IEnumerable<SelectListItem> BookConditions { get; set; }
    }
}