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
            if (loggedInUser.UserName != null)
            {
                ViewBag.username = loggedInUser.UserName;
                return View();
            }

            return View("UserNotFound");
        }

        public ActionResult Items()
        {
            if (loggedInUser.UserName != null)
            {
                return View(loggedInUser.UserItems.ToList());
            }

            return View("UserNotFound");
        }

        public ActionResult Lists()
        {
            if (loggedInUser.UserName != null)
            {
                return View(loggedInUser.UserLists.ToList());
            }

            return View("UserNotFound");
        }

        public ActionResult SpecificList(string listParseString)
        {
            if (loggedInUser.UserName != null)
            {
                string[] listParts = listParseString.Split('-');
                int listId;
                if (int.TryParse(listParts[0], out listId))
                {
                    ActionResult successParseView;
                    UserList loadedList = db.UserLists.Where(ul => ul.Id == listId).FirstOrDefault();
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

            return View("UserNotFound");
        }

        public ActionResult Friends()
        {
            return View(loggedInUser.Friends.ToList());
        }

    }
}
