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
    public class ItemsController : Controller
    {
        private YearnlyEntities db;

        public ItemsController()
        {
            this.db = new YearnlyEntities();
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
                var query = db.UserItems.Where(ui => ui.Id == id && ui.UserId == WebSecurity.CurrentUserId);
                returnView = View(query.FirstOrDefault());
            }
            else
            {
                returnView = RedirectToAction("Items", "Users");
            }
            return returnView;
        }

        [HttpPost]
        public ActionResult SaveEdit(UserItem input)
        {
            UserItem authenticatedItem = db.UserItems.Where(ui => ui.Id == input.Id && ui.UserId == WebSecurity.CurrentUserId).FirstOrDefault();
            if (authenticatedItem != null)
            {
                authenticatedItem.Title = input.Title;
                authenticatedItem.Link = input.Link;
                authenticatedItem.Description = input.Description;
                authenticatedItem.DateUpdated = DateTime.UtcNow;
                db.SaveChanges();
            }
            return RedirectToAction("Items", "Users");
        }

        [HttpPost]
        public ActionResult Create(UserItem input)
        {
            input.UserId = WebSecurity.CurrentUserId;
            input.DateCreated = DateTime.UtcNow;
            db.UserItems.Add(input);
            db.SaveChanges();

            return RedirectToAction("index", "items");
        }

        [HttpPost]
        [AllowAnonymous]
        public bool AjaxCreate(UserItem input)
        {
            bool didCreateItem = false;
            input.UserId = WebSecurity.CurrentUserId;
            input.DateCreated = DateTime.UtcNow;
            int numOfItemsAdded = 0;
            //We only want to save it if a user is logged in.
            if (input.UserId != -1)
            {
                using (YearnlyEntities context = new YearnlyEntities())
                {
                    context.UserItems.Add(input);
                    numOfItemsAdded = context.SaveChanges();
                }
                if (numOfItemsAdded > 0)
                {
                    didCreateItem = true;
                }
            }

            return didCreateItem;
        }

    }
}
