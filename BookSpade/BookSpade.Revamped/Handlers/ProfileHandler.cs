﻿using System;
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

        public static Profile GetProfile(int userId)
        {
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
                    string facebookId = row["FacebookId"] is DBNull ? null : Convert.ToString(row["FacebookId"]);
                    string facebookLink = row["FacebookLink"] is DBNull ? null : Convert.ToString(row["FacebookLink"]);

                    profile = new Profile(userId, name, email, facebookId, facebookLink);
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

        public static Profile GetProfile(string email) //in B.S. UserName == Email 
        {
            Profile profile = null;

            try
            {
                DataAccess DAL = new DataAccess();
                DataTable dt = DAL.select(String.Format("UserName = '{0}'", email), "UserProfile", NumRows: 1);

                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    int profileId = Convert.ToInt32(row["UserId"]); 
                    string displayName = Convert.ToString(row["DisplayName"]);
                    string facebookId = row["FacebookId"] is DBNull ? null : Convert.ToString(row["FacebookId"]);
                    string facebookLink = row["FacebookLink"] is DBNull ? null : Convert.ToString(row["FacebookLink"]);
                    
                    profile = new Profile(profileId, displayName, email, facebookId, facebookLink);
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

        #region GetFacebookId

        public static string GetFacebookId(int userId) //in B.S. UserName == Email 
        {
            string facebookId = null;

            try
            {
                DataAccess DAL = new DataAccess();
                DataTable dt = DAL.select(String.Format("UserId = '{0}'", userId), "UserProfile", NumRows: 1, ColumnNames: new string[] { "FacebookId" });

                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    facebookId = row["FacebookId"] is DBNull ? null : Convert.ToString(row["FacebookId"]);
                }
            }
            catch (Exception ex)
            {
                Console.Write("ERROR: An error occured in retrieving the facebook id for user --- " + ex.Message);
            }

            return facebookId;
        }

        #endregion

        #region ValidateProfile

        //User clicked on link
        //We shall update the database to validate his/her account using the provided token
        public static Profile ValidateProfile(Guid Token){

            Profile profile = null;

            try
            {
                var DAL = new DataAccess();
                var dt = DAL.select(String.Format("ValidationToken = '{0}'", Token), "UserProfile", NumRows: 1);

                if (dt != null && dt.Rows.Count > 0)
                {
                    var updateDictionary = new Dictionary<string, object>();
                    
                    updateDictionary.Add("IsValid", true); 

                    DAL.update("UserProfile", "ValidationToken = '{0}'", updateDictionary); 

                    var row = dt.Rows[0];
                    var UserName = Convert.ToString(row["UserName"]); 
                    var profileId = Convert.ToInt32(row["UserId"]);
                    var displayName = Convert.ToString(row["DisplayName"]);
                    var facebookId = row["FacebookId"] is DBNull ? null : Convert.ToString(row["FacebookId"]);
                    var facebookLink = row["FacebookLink"] is DBNull ? null : Convert.ToString(row["FacebookLink"]);

                    profile = new Profile(profileId, displayName, UserName, facebookId, facebookLink);
                }
            }
            catch (Exception ex)
            {
                Console.Write("ERROR: An error occured in retrieving the user profile --- " + ex.Message);
            }

            return profile;
        }

        #endregion

        #region ConfirmValidation

        //Confirm Validation -> Checks to see if the user has confirmed using the token
        //If a user that hasn't confirmed tries to log in we will send them an incorrect username or password error

        public static bool ConfirmValidation(string Email)
        {
            try
            {
                var DAL = new DataAccess();
                var dt = DAL.select(String.Format("UserName = '{0}' AND IsValid = '1' ", Email), "UserProfile", NumRows: 1);

                return dt.Rows.Count > 0; 
              
            }
            catch (Exception ex)
            {
                Console.Write("ERROR: An error occured in retrieving the user profile --- " + ex.Message);
            }

            return false; //uh oh .. 
        }


        #endregion

    }
}