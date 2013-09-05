using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookSpade.Revamped.DAL;
using System.Data; 
using BookSpade.Revamped.Models;

namespace BookSpade.Revamped.Handlers
{
    public class CourseHandler
    {
        #region CreateCourse

        public static int CreateCourse(string CourseName)
        {
            int id = -1;

            try
            {
                DataAccess DAL = new DataAccess();
                DataTable dt = DAL.select(String.Format("CourseName = '{0}'", CourseName), "CourseInfo");

                if (dt == null || dt.Rows.Count == 0)
                {
                    Dictionary<string, string> courseInfo = new Dictionary<string, string>();
                    courseInfo.Add("CourseName", CourseName);
                    courseInfo.Add("Description", CourseName);
                    courseInfo.Add("IsActive", "1");
                    courseInfo.Add("IsDeleted", "0");
                    courseInfo.Add("CreatedDate", Convert.ToString(DateTime.Now));
                    courseInfo.Add("ModifiedDate", Convert.ToString(DateTime.Now));
                   
                    id = DAL.insert(courseInfo, "CourseInfo");
                }
            }
            catch (Exception ex)
            {
                Console.Write("ERROR: An error occured in adding a new course --- " + ex.Message);
            }

            return id;
        }

        #endregion

        #region GetCourse

        public static Course getCourse(int CourseId)
        {
            Course course = null;

            try
            {
                DataAccess da = new DataAccess();
                DataTable dt = da.select(String.Format("CourseId = '{0}'", CourseId), "CourseInfo");

                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    string CourseName = Convert.ToString(row["CourseName"]);
                    string Description = Convert.ToString(row["Description"]);
                    int IsActive = Convert.ToInt32(row["IsActive"]);
                    int IsDeleted = Convert.ToInt32(row["IsDeleted"]);
                    DateTime CreatedDate = Convert.ToDateTime(row["CreatedDate"]);
                    DateTime ModifiedDate = Convert.ToDateTime(row["ModifiedDate"]);

                    course = new Course(
                        CourseId,
                        CourseName,
                        Description,
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

            return course;
        }

        #endregion

    }
}