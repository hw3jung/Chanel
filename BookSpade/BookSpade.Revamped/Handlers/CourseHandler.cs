using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookSpade.Revamped.DAL;
using System.Data; 

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
    }
}