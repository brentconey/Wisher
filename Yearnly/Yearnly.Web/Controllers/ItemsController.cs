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
        private UserProfile loggedInUser;

        public ItemsController()
        {
            db = new YearnlyEntities();
            loggedInUser = UserProfile.LoadUserByUserName(WebSecurity.CurrentUserName, db);
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
        public bool AjaxCreate(UserItem input)
        {
            bool didCreateItem = false;
            input.UserId = WebSecurity.CurrentUserId;
            input.DateCreated = DateTime.UtcNow;
            try
            {
                db.UserItems.Add(input);
                db.SaveChanges();
                didCreateItem = true;
            }
            catch
            {
                didCreateItem = false;
            }

            return didCreateItem;
        }
        //Used for the ajaxgetitem method.
        public class AjaxUserItem
        {
            public int Id { get; set; }
            public string Link { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public bool IsMyItem { get; set; }
            public List<AjaxItemComment> ItemComments { get; set; }
        }

        public class AjaxItemComment
        {
            public bool IsWhisper { get; set; }
            public string UserName { get; set; }
            public string SmallProfilePicLink { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Comment { get; set; }
            public int DaysAgo { get; set; }
        }

        public ActionResult AjaxGetItem(int itemId)
        {
            IEnumerable<ItemComment> dbComments = db.ItemComments.Where(ic => ic.ItemId == itemId).OrderBy(ic => ic.DateAdded).Take(3);
            var itemComments = dbComments.Select(ic => new AjaxItemComment
            {
                IsWhisper = ic.IsWhisper,
                UserName = ic.CommenterProfile.UserName,
                SmallProfilePicLink = ic.CommenterProfile.SmallProfilePic,
                FirstName = ic.CommenterProfile.FirstName,
                LastName = ic.CommenterProfile.LastName,
                Comment = ic.Comment,
                DaysAgo = (int)DateTime.Now.Subtract(ic.DateAdded).TotalDays
            }).ToList();

            UserItem dbItem = db.UserItems.Where(ui => ui.Id == itemId).FirstOrDefault();
            AjaxUserItem ajaxItem = null;
            if (dbItem != null)
            {
                ajaxItem = new AjaxUserItem
                {
                    Id = dbItem.Id,
                    Link = dbItem.Link,
                    Title = dbItem.Title,
                    Description = dbItem.Description,
                    IsMyItem = dbItem.UserId == loggedInUser.UserId ? true : false,
                    ItemComments = itemComments
                };
            }

            return Json(ajaxItem, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult AjaxPostItemComment(int itemId, string comment, bool isWhisper)
        {
            //This method returns the added comment so we can add it to the screen
            //In the jscript
            ItemComment addingComment = new ItemComment();
            addingComment.ItemId = itemId;
            addingComment.UserId = loggedInUser.UserId;
            addingComment.Comment = comment;
            addingComment.IsWhisper = isWhisper;
            addingComment.DateAdded = DateTime.UtcNow;
            AjaxItemComment addedComment = null;
            try
            {
                db.ItemComments.Add(addingComment);
                db.SaveChanges();
                addedComment = new AjaxItemComment();
                addedComment.UserName = loggedInUser.UserName;
                addedComment.SmallProfilePicLink = loggedInUser.SmallProfilePic;
                addedComment.FirstName = loggedInUser.FirstName;
                addedComment.LastName = loggedInUser.LastName;
                addedComment.Comment = addingComment.Comment;
            }
            catch
            {
                addedComment = null;
            }

            return Json(addedComment, JsonRequestBehavior.DenyGet);
        }

    }
}
