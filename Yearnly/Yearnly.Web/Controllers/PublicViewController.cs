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
            if (user.UserName != null)
            {
                ViewBag.UserName = user.UserName;
                return View();
            }
            return View("UserNotFound");
        }

        public ActionResult Lists(string username)
        {
            UserProfile user = UserProfile.LoadUserByUserName(username, db);
            if (user.UserName != null)
            {
                return View(user.UserLists.ToList());
            }

            return View("UserNotFound");
        }

        public ActionResult Items(string username)
        {
            UserProfile user = UserProfile.LoadUserByUserName(username, db);
            if (user.UserName != null)
            {
                return View(user.UserItems.ToList());
            }

            return View("UserNotFound");
        }

    }
}
