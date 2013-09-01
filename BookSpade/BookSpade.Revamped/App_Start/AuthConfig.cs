using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Web.WebPages.OAuth;
using BookSpade.Revamped.Models;

namespace BookSpade.Revamped
{
    public static class AuthConfig
    {
        public static void RegisterAuth()
        {
            // To let users of this site log in using their accounts from other sites such as Microsoft, Facebook, and Twitter,
            // you must update this site. For more information visit http://go.microsoft.com/fwlink/?LinkID=252166

            //OAuthWebSecurity.RegisterMicrosoftClient(
            //    clientId: "",
            //    clientSecret: "");

            //OAuthWebSecurity.RegisterTwitterClient(
            //    consumerKey: "",
            //    consumerSecret: "");

            OAuthWebSecurity.RegisterFacebookClient(
                //Production App
                //appId: "361893360608838",
                //appSecret: "574c926b35bc16581be0bb40963a8762"
                //Development App
                appId: "410278782401754",
                appSecret: "4d0fd841a025dd908191f50b86ec90f7"
             );
            //OAuthWebSecurity.RegisterGoogleClient();
        }
    }
}
