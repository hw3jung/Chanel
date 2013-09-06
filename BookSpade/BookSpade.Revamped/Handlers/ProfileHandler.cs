using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookSpade.Revamped.Models;
using System.Data;
using BookSpade.Revamped.DAL; 

namespace BookSpade.Revamped.Handlers
{
    public class ProfileHandler
    {
        #region CreateProfile

        public static int CreateProfile(string Name, string Email)
        {
            int id = -1;

            try
            {
                DataAccess DAL = new DataAccess();
                DataTable dt = DAL.select(String.Format("UserName = '{0}'", Email), "UserProfile");

                if (dt == null || dt.Rows.Count == 0)
                {
                    Dictionary<string, string> profile = new Dictionary<string, string>();
                    profile.Add("Name", Name);
                    profile.Add("Email", Email);
                    profile.Add("IsActive", "1");
                    profile.Add("IsDeleted", "0");
                    profile.Add("CreatedDate", Convert.ToString(DateTime.Now));
                    profile.Add("ModifiedDate", Convert.ToString(DateTime.Now));

                    id = DAL.insert(profile, "UserProfile");
                }
            }
            catch (Exception ex)
            {
                Console.Write("ERROR: An error occured in adding a new course --- " + ex.Message);
            }

            return id;
        }

        #endregion

        #region GetProfile

        public static Profile GetProfile(int ProfileId){

            Profile profile = null;

            try
            {
                DataAccess DAL = new DataAccess();
                DataTable dt = DAL.select(String.Format("ProfileId = '{0}'", ProfileId), "Profile");

                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    string name = Convert.ToString(row["Name"]);
                    string email = Convert.ToString(row["Email"]);
                    
                    profile = new Profile(ProfileId, name, email);
                }
            }
            catch (Exception ex)
            {
                Console.Write("ERROR: An error occured in retrieving the user profile --- " + ex.Message);
            }

            return profile;

        }

        #endregion

        #region GetProfile

        public static Profile GetProfile(string Email) //in B.S. UserName == Email 
        {
            Profile profile = null;

            try
            {
                DataAccess DAL = new DataAccess();
                DataTable dt = DAL.select(String.Format("Email = '{0}'", Email), "Profile");

                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    int ProfileId = Convert.ToInt32(row["ProfileId"]); 
                    string name = Convert.ToString(row["Name"]);

                    profile = new Profile(ProfileId, name, Email);
                }
            }
            catch (Exception ex)
            {
                Console.Write("ERROR: An error occured in retrieving the user profile --- " + ex.Message);
            }

            return profile;
        }

        #endregion

    }
}