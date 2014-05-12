using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BookSpade.Revamped
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // Create Post page
            routes.MapRoute(
                name: "CreatePost",
                url: "Post/Create/{postType}",
                defaults: new { controller = "Post", action = "Create", postType = "Buy" }
            );
            
            // Transaction Details page
            routes.MapRoute(
                name: "TransactionDetails",
                url: "Transaction/TransactionDetails/{transactionId}",
                defaults: new { controller = "Transaction", action = "TransactionDetails" }
            );

            // Validation
            routes.MapRoute(
                name: "AccountValidation",
                url: "Account/validate/{token}",
                defaults: new { controller = "Account", action = "validate", token = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}