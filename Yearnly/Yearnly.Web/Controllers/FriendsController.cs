using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Yearnly.Model;

namespace Yearnly.Web.Controllers
{
    public class FriendsController : Controller
    {
        private YearnlyEntities db;
        public FriendsController()
        {
            db = new YearnlyEntities();
        }
       
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AjaxSearch(string searchText)
        {
            return View(db.UserProfiles.Where(up => up.UserName.Contains(searchText)).ToList());
        }

    }
}
