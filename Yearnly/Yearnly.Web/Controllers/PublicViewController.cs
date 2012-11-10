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
                return View();
            }
            return View("UserNotFound");
        }

        public ActionResult Lists(string username, string listParseString)
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
