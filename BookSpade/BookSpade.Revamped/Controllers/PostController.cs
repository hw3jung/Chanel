using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookSpade.Revamped.Models;
using BookSpade.Revamped.Utilities;
using BookSpade.Revamped.Handlers;
using System.Threading.Tasks;

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

        //
        // GET: /Post/Create

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

            var viewModel = new CreatePostModel()
            {
                ActionBy = ActionBy.Buyer,
                BookCondition = BookCondition.Excellent,
                PostTypes = SelectListUtility.getPostTypes(),
                BookConditions = SelectListUtility.getBookConditions(),
                Textbooks = textBookCollection
            };

            return View("CreatePost", viewModel);
        }

        //
        // POST: /Post/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreatePostModel model)
        {
            if (ModelState.IsValid)
            {
                int textbookId = model.TextBookId;

                // if we have a new textbook info, create Textbook and store it
                if (!String.IsNullOrEmpty(model.BookTitle))
                {
                    var newTextbook = new Textbook(
                        -1, // id doesnt matter here
                        model.BookTitle,
                        model.ISBN,
                        model.Author,
                        model.CourseId,
                        model.NewCourseName,
                        model.BookImageUrl,
                        null,
                        1,
                        0,
                        DateTime.Now,
                        DateTime.Now
                    );

                    textbookId = TextbookHandler.createTextBook(newTextbook);
                }
                
                int profileId = ProfileHandler.GetProfileId(User.Identity.Name);
                decimal price = model.Price;
                ActionBy actionBy = model.ActionBy;

                if (model.IsNegotiable)
                {
                    if (actionBy == ActionBy.Buyer)
                    {
                        price = int.MaxValue;
                    }
                    else
                    {
                        price = 0;
                    }
                }

                var newPost = new Post(
                    -1, // id doesnt matter here
                    profileId,
                    textbookId,
                    actionBy,
                    price,
                    model.BookCondition,
                    0,
                    1,
                    0,
                    DateTime.Now,
                    DateTime.Now
                );

                Task.Run(() => QueueWorker.AddPost(newPost));

                // TODO: redirect to special "you've successfully created post" page
                // with links to create another buy/sell post
                return RedirectToAction("Index", "Home");
            }

            // If we got this far, something failed, redisplay form
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

            model.PostTypes = SelectListUtility.getPostTypes();
            model.BookConditions = SelectListUtility.getBookConditions();
            model.Textbooks = textBookCollection;

            return View("CreatePost", model);
        }
    }
}
