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
    public class UsersController : Controller
    {
        private YearnlyEntities db;

        public UsersController()
        {
            db = new YearnlyEntities();
        }
        public ActionResult Index(string username)
        {
            if (UserProfile.DoesUserExists(username, db))
            {
                ViewBag.username = username;
                return View();
            }
            else
            {
                return View("UserNotFound");
            }

        }

        public ActionResult Items(string username)
        {
            if (String.IsNullOrEmpty(username))
            {
                username = WebSecurity.CurrentUserName;
            }
            if (UserProfile.DoesUserExists(username, db))
            {
                var query = db.UserItems.Where(ul => ul.UserProfile.UserName == username);
                ViewBag.username = username;
                return View(query.ToList());
            }
            else
            {
                return View("UserNotFound");
            }
        }

        public ActionResult Lists(string username)
        {
            if (String.IsNullOrEmpty(username))
            {
                username = WebSecurity.CurrentUserName;
            }
            if (UserProfile.DoesUserExists(username, db))
            {
                var query = db.UserLists.Where(ul => ul.UserProfile.UserName == username);

                ViewBag.username = username;
                return View(query.ToList());
            }
            else
            {
                return View("UserNotFound");
            }
        }

    }
}
