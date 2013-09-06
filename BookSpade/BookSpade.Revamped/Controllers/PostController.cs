using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookSpade.Revamped.Models;

namespace BookSpade.Revamped.Controllers
{
    public class PostController : Controller
    {
        //
        // GET: /Post/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            List<Textbook> textBookCollection = new List<Textbook>();

            // test data
            for(int i = 0; i < 100; i++) {
                Textbook book = new Textbook(
                    i,
                    "AFM 10" + i + " Financial Accounting",
                    "100000000000" + i,
                    "Author " + i,
                    100 + i,
                    "",
                    "",
                    10 + i,
                    1,
                    0,
                    DateTime.Now,
                    DateTime.Now
                );
                textBookCollection.Add(book);
            }
            //

            return View("CreatePost", textBookCollection);
        }
        


    }
}
