﻿using BookSpade.Revamped.Utilities;
using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Caching;
using BookSpade.Revamped.Handlers;

namespace BookSpade.Revamped
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        private const string DummyCacheItemKey = "KevinSucks"; 

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();
            ViewEngines.Engines.Add(new RazorEngineCustomized());

            // Queue the Processor thread for BookQueue. Put this call 
            // before Parallel.Invoke to begin processing as soon as 
            // new posts are added to BookQueue.
            Task.Factory.StartNew(() => QueueWorker.ProcessPosts());
            //Register Cache Entry for 24 hour email reminder 
            RegisterCacheEntry();
            
        }

        private bool RegisterCacheEntry()
        {
            if (null != HttpContext.Current.Cache[DummyCacheItemKey]) return false;

            HttpContext.Current.Cache.Add(
                DummyCacheItemKey,
                "Test",
                null,
                DateTime.MaxValue,
                TimeSpan.FromDays(1),
                CacheItemPriority.Normal,
                new CacheItemRemovedCallback(CacheItemRemovedCallback));

            return true;
        }


        public void CacheItemRemovedCallback(
            string key,
            object value,
            CacheItemRemovedReason reason)
        {
            System.Diagnostics.Debug.WriteLine("Cache item callback: " + DateTime.Now.ToString());
            HitPage(); 
            DailyTask();
        }

        public void DailyTask()
        {
            try
            {
                CommentHandler.commentReminderMail();
                TransactionHandler.AutoConfirmTransaction(); 
            }
            catch (Exception ex)
            {
                Console.Write("error sending scheduled emails");
            }
        }

        private const string DummyPageUrl = "http://www.bookspade.com/Home/About";

        private void HitPage()
        {
            System.Net.WebClient client = new System.Net.WebClient();
            client.DownloadData(DummyPageUrl);
        }

        protected void Application_BeginRequest(Object sender, EventArgs e)
        {
            if (HttpContext.Current.Request.Url.ToString() == DummyPageUrl)
            {
                RegisterCacheEntry();
            }
        }
    }
}