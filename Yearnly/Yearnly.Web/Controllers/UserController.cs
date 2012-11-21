using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;
using Yearnly.Model;

namespace Yearnly.Web.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private YearnlyEntities db;
        private UserProfile loggedInUser;
        public UserController()
        {
            db = new YearnlyEntities();
            loggedInUser = UserProfile.LoadUserByUserName(WebSecurity.CurrentUserName, db);
        }
        public ActionResult Index()
        {
            ViewBag.username = loggedInUser.UserName;
            return View();
        }

        public ActionResult Items()
        {
            return View(loggedInUser.UserItems.ToList());
        }

        public ActionResult Lists()
        {
            return View(loggedInUser.UserLists.ToList());
        }

        public ActionResult Dibs()
        {
            return View();
        }

        public ActionResult SpecificList(string listParseString)
        {

            string[] listParts = listParseString.Split('-');
            int listId;
            if (int.TryParse(listParts[0], out listId))
            {
                ActionResult successParseView;
                UserList loadedList = db.UserLists.Where(ul => ul.Id == listId && ul.UserId == loggedInUser.UserId).FirstOrDefault();
                if (loadedList != null)
                {
                    //If loadedList isn't null we have a real list
                    successParseView = View(loadedList);
                }
                else
                {
                    //The query returned nothing
                    successParseView = View("ListNotFound");
                }

                return successParseView;
            }
            //If it can't parse it; it's not found
            return View("ListNotFound");

        }

        public ActionResult Friends()
        {
            return View(loggedInUser.Friends.ToList());
        }

    }
}
