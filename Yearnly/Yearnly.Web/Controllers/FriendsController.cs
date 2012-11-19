using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;
using Yearnly.Model;

namespace Yearnly.Web.Controllers
{
    public class FriendsController : Controller
    {
        public class FriendSearchResult
        {
            public int UserId { get; set; }
            public string UserName { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public bool RequestHasBeenSent { get; set; }
        }
        
        private YearnlyEntities db;
        private UserProfile loggedInUser;
        public FriendsController()
        {
            db = new YearnlyEntities();
            loggedInUser = UserProfile.LoadUserByUserName(WebSecurity.CurrentUserName, db);
        }
       
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AjaxSearch(string searchText)
        {
            //This returns the search where it doesn't include the logged in user
            IEnumerable<FriendSearchResult> searchResults = from u in UserProfile.SearchUsers(searchText, db)
                                                      where u.UserId != loggedInUser.UserId
                                                      select new FriendSearchResult
                                                      {
                                                          UserId = u.UserId,
                                                          UserName = u.UserName,
                                                          FirstName = u.FirstName,
                                                          LastName = u.LastName,
                                                          RequestHasBeenSent = loggedInUser.FriendRequests.Where(toid => toid.ToUserId == u.UserId).FirstOrDefault() == null ? false : true
                                                      };
            return Json(searchResults);
        }

        public bool AjaxSendFriendRequest(int toUserId)
        {
            bool didSendFriendRequest = false;
            FriendRequest request = new FriendRequest();
            request.FromUserId = loggedInUser.UserId;
            request.ToUserId = toUserId;
            request.DateAdded = DateTime.Now;
            db.FriendRequests.Add(request);
            try
            {
                db.SaveChanges();
                didSendFriendRequest = true;
            }
            catch (InvalidOperationException)
            {
                didSendFriendRequest = false;
            }

            return didSendFriendRequest;
        }
        
        public ActionResult AjaxGetFriendNotifications()
        {
            
            return Json(loggedInUser.FriendRequests.ToList(), JsonRequestBehavior.AllowGet);
        }

    }
}
