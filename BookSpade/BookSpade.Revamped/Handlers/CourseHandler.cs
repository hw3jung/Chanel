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

        public static int CreateCourse(string courseName)
        {
            int id = -1;

            try
            {
                DataAccess DAL = new DataAccess();
                DataTable dt = DAL.select(String.Format("CourseName = '{0}'", courseName), "CourseInfo", NumRows : 1);

                if (dt == null || dt.Rows.Count == 0)
                {
                    Dictionary<string, object> courseInfo = new Dictionary<string, object>();
                    courseInfo.Add("CourseName", courseName);
                    courseInfo.Add("Description", courseName);
                    courseInfo.Add("IsActive", 1);
                    courseInfo.Add("IsDeleted", 0);
                    courseInfo.Add("CreatedDate", DateTime.Now);
                    courseInfo.Add("ModifiedDate", DateTime.Now);
                   
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

        public static Course getCourse(int courseId)
        {
            Course course = null;

            try
            {
                DataAccess da = new DataAccess();
                DataTable dt = da.select(String.Format("CourseId = '{0}' AND IsActive = 1 AND IsDeleted = 0", courseId), "CourseInfo", NumRows : 1);

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
                        courseId,
                        CourseName,
                        Description,
                        IsActive,
                        IsDeleted,
                        CreatedDate,
                        ModifiedDate
                    );
                }

            }
            catch (Exception ex)
            {
                Console.Write(ex.Message + "   " + ex.StackTrace);
            }

            return course;
        }

        #endregion

        #region GetCourseByName

        public static Course getCourseByName(string courseName)
        {
            Course course = null;

            try
            {
                DataAccess da = new DataAccess();
                DataTable dt = da.select(String.Format("CourseName = '{0}' AND IsActive = 1 AND IsDeleted = 0", courseName), "CourseInfo", NumRows: 1);

                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    int CourseId = Convert.ToInt32(row["CourseId"]);
                    string Description = Convert.ToString(row["Description"]);
                    int IsActive = Convert.ToInt32(row["IsActive"]);
                    int IsDeleted = Convert.ToInt32(row["IsDeleted"]);
                    DateTime CreatedDate = Convert.ToDateTime(row["CreatedDate"]);
                    DateTime ModifiedDate = Convert.ToDateTime(row["ModifiedDate"]);

                    course = new Course(
                        CourseId,
                        courseName,
                        Description,
                        IsActive,
                        IsDeleted,
                        CreatedDate,
                        ModifiedDate
                    );
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message + "   " + ex.StackTrace);
            }

            return course;
        }

        #endregion

        #region GetCourseName

        public static string getCourseName(int courseId)
        {
            string courseName = null;

            try
            {
                DataAccess da = new DataAccess();
                DataTable dt = da.select(String.Format("CourseId = '{0}' AND IsActive = 1 AND IsDeleted = 0", courseId), "CourseInfo", NumRows: 1, ColumnNames: new string[] {"CourseName"});

                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    courseName = Convert.ToString(row["CourseName"]);
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message + "   " + ex.StackTrace);
            }

            return courseName;
        }

        #endregion

    }
}