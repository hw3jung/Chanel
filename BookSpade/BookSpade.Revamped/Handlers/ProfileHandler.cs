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
                DataTable dt = DAL.select(String.Format("UserName = '{0}'", Email), "UserProfile", NumRows : 1);

                if (dt == null || dt.Rows.Count == 0)
                {
                    Dictionary<string, object> profile = new Dictionary<string, object>();
                    profile.Add("Name", Name);
                    profile.Add("Email", Email);
                    profile.Add("IsActive", 1);
                    profile.Add("IsDeleted", 0);
                    profile.Add("CreatedDate", DateTime.Now);
                    profile.Add("ModifiedDate", DateTime.Now);

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

        //by Id
        #region GetProfile

        public static Profile GetProfile(int userId){

            Profile profile = null;

            try
            {
                DataAccess DAL = new DataAccess();
                DataTable dt = DAL.select(String.Format("UserId = '{0}'", userId), "UserProfile", NumRows : 1);

                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    string name = Convert.ToString(row["DisplayName"]);
                    string email = Convert.ToString(row["UserName"]);
                    
                    profile = new Profile(userId, name, email);
                }
            }
            catch (Exception ex)
            {
                Console.Write("ERROR: An error occured in retrieving the user profile --- " + ex.Message);
            }

            return profile;

        }

        #endregion

        //by Email
        #region GetProfile

        public static Profile GetProfile(string Email) //in B.S. UserName == Email 
        {
            Profile profile = null;

            try
            {
                DataAccess DAL = new DataAccess();
                DataTable dt = DAL.select(String.Format("UserName = '{0}'", Email), "UserProfile", NumRows : 1);

                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    int ProfileId = Convert.ToInt32(row["UserId"]); 
                    string DisplayName = Convert.ToString(row["DisplayName"]);

                    profile = new Profile(ProfileId, DisplayName, Email);
                }
            }
            catch (Exception ex)
            {
                Console.Write("ERROR: An error occured in retrieving the user profile --- " + ex.Message);
            }

            return profile;
        }

        #endregion

        #region GetProfileId

        public static int GetProfileId(string Email) //in B.S. UserName == Email 
        {
            int profileId = -1;

            try
            {
                DataAccess DAL = new DataAccess();
                DataTable dt = DAL.select(String.Format("UserName = '{0}'", Email), "UserProfile", NumRows : 1);

                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    profileId = Convert.ToInt32(row["UserId"]);
                }
            }
            catch (Exception ex)
            {
                Console.Write("ERROR: An error occured in retrieving the user profile --- " + ex.Message);
            }

            return profileId;
        }

        #endregion
    }
}