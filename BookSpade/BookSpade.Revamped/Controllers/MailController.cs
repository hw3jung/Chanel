using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using BookSpade.Revamped.Mailers;  

namespace BookSpade.Revamped.Controllers
{
    public class MailController : Controller
    {
        //
        // GET: /Mail/

        private IUserMailer _mailer = new UserMailer();
        public IUserMailer Mailer
        {
            get { return _mailer; }
            set { _mailer = value; } 
        }

        public ActionResult Index()
        {
            _mailer.Welcome().SendAsync();
            return View();
        }


    }
}
