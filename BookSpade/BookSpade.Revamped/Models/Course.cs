using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookSpade.Revamped.Models
{
    public class Course
    {
        public int CourseId { get; set; } 
        public string CourseName { get; set; }
        public string CourseDescription { get; set; } 
        public int IsActive { get; set; }
        public int IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        public Course(int courseId,
                      string courseName,
                      string courseDescription,
                      int isActive,
                      int isDeleted,
                      DateTime createdDate,
                      DateTime modifiedDate)
        {
            CourseId = courseId;
            CourseName = courseName;
            CourseDescription = courseDescription;
            IsActive = isActive;
            IsDeleted = isDeleted;
            CreatedDate = createdDate;
            ModifiedDate = modifiedDate; 
        }

    }
}