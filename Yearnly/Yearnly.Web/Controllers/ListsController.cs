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
    public class ListsController : Controller
    {
        private YearnlyEntities db;
        public ListsController()
        {
            db = new YearnlyEntities();
        }

        public ActionResult Add()
        {
            return View();
        }

        public ActionResult Edit(int? id)
        {
            ActionResult returnView = null;
            if (id.HasValue)
            {
                var query = db.UserLists.Where(ul => ul.Id == id && ul.UserId == WebSecurity.CurrentUserId);
                returnView = View(query.FirstOrDefault());
            }
            else
            {
                returnView = RedirectToAction("Lists", "Users");
            }

            return returnView;
        }

        [HttpPost]
        public ActionResult SaveEdit(UserList list)
        {
            //Pull the list from the db with the currently logged in user 
            //This makes sure the person trying to save the list is the owner.
            UserList authenticatedList = db.UserLists.Where(ul => ul.Id == list.Id && ul.UserId == WebSecurity.CurrentUserId).FirstOrDefault();
            if (authenticatedList != null)
            {
                authenticatedList.Name = list.Name;
                authenticatedList.DateUpdated = DateTime.UtcNow;
                db.SaveChanges();
            }
            return RedirectToAction("Lists", "Users");
        }

        [HttpPost]
        public ActionResult Create(String listname)
        {
            UserList newList = new UserList();
            newList.Name = listname;
            newList.UserId = WebSecurity.CurrentUserId;
            newList.DateCreated = DateTime.UtcNow;
            using (YearnlyEntities context = new YearnlyEntities())
            {
                context.UserLists.Add(newList);
                context.SaveChanges();
            }
            return RedirectToAction("Lists", "Users");
        }
    }
}
