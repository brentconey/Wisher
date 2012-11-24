using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;
using Yearnly.Model;

namespace Yearnly.Web.Controllers
{
    public class PublicViewController : Controller
    {
        private YearnlyEntities db;
        public PublicViewController()
        {
            db = new YearnlyEntities();
        }

        public ActionResult Index(string username)
        {
            UserProfile user = UserProfile.LoadUserByUserName(username, db);
            if (!String.IsNullOrEmpty(user.UserName))
            {
                ViewBag.UserName = user.UserName;
                return View(user);
            }
            return View("UserNotFound");
        }

        public ActionResult UsersList(string listParseString, string username)
        {
            UserProfile user = UserProfile.LoadUserByUserName(username, db);
            string[] listParts = listParseString.Split('-');
            int listId;
            if (int.TryParse(listParts[0], out listId))
            {
                ActionResult successParseView;
                UserList loadedList = db.UserLists.Where(ul => ul.Id == listId && ul.UserId == user.UserId).FirstOrDefault();
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

        public ActionResult Lists(string username)
        {
            UserProfile user = UserProfile.LoadUserByUserName(username, db);
            if (!String.IsNullOrEmpty(user.UserName))
            {
                return View(user.UserLists.ToList());
            }

            return View("UserNotFound");
        }

        public ActionResult Items(string username)
        {
            UserProfile user = UserProfile.LoadUserByUserName(username, db);
            if (!String.IsNullOrEmpty(user.UserName))
            {
                return View(user.UserItems.ToList());
            }

            return View("UserNotFound");
        }

        public ActionResult Friends(string username)
        {
            UserProfile user = UserProfile.LoadUserByUserName(username, db);
            if (!String.IsNullOrEmpty(user.UserName))
            {
                ViewBag.UserName = user.UserName;
                return View(user.Friends.ToList());
            }

            return View("UserNotFound");
        }

    }
}
