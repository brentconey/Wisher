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

        public class FriendSearchResult
        {
            public int UserId { get; set; }
            public string UserName { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public bool RequestHasBeenSent { get; set; }
            public bool AreFriends { get; set; }
            public string LargeProfilePic { get; set; }
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
                                                          RequestHasBeenSent = db.FriendRequests.Where(fr => fr.ToUserId == u.UserId && fr.FromUserId == loggedInUser.UserId).FirstOrDefault() == null ? false : true,
                                                          AreFriends = loggedInUser.Friends.Where(fid => fid.FriendProfile.UserId == u.UserId).FirstOrDefault() == null ? false : true,
                                                          LargeProfilePic = u.LargeProfilePic
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

        public class JSONFriendNotification
        {
            public int FriendId { get; set; }
            public string UserName { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }
        public ActionResult AjaxGetFriendNotifications()
        {
            IEnumerable<JSONFriendNotification> friendRequests = from fn in loggedInUser.FriendRequests
                                                                 select new JSONFriendNotification
                                                                 {
                                                                     FriendId = fn.FromUserProfile.UserId,
                                                                     //FirstName = fn.FromUserProfile.FirstName,
                                                                     //LastName = fn.FromUserProfile.LastName,
                                                                     UserName = fn.FromUserProfile.UserName
                                                                 };
            return Json(friendRequests, JsonRequestBehavior.AllowGet);
        }

        public bool AjaxAcceptFriendRequest(int fromUserId)
        {
            bool didAcceptRequest = false;
            //remove both just in case a request was sent both ways we want remove them both
            var requestsToRemove = db.FriendRequests.Where(fr => fr.FromUserId == fromUserId || fr.ToUserId == fromUserId).ToList();
            foreach (FriendRequest row in requestsToRemove)
            {
                db.FriendRequests.Remove(row);
            }
            DateTime friendshipStartDate = DateTime.Now;
            //We add the row both ways because the friendships are mutual.
            db.Friends.Add(new Friend{ UserId = loggedInUser.UserId, FriendId = fromUserId, DateAdded = friendshipStartDate});
            db.Friends.Add(new Friend { UserId = fromUserId, FriendId = loggedInUser.UserId, DateAdded = friendshipStartDate });
            try
            {
                db.SaveChanges();
                didAcceptRequest = true;
            }
            catch (InvalidOperationException)
            {
                didAcceptRequest = false;
            }

            return didAcceptRequest;
        }

        public bool AjaxDeclineFriendRequest(int fromUserId)
        {
            bool didDecline = false;
            FriendRequest requestToDelete = loggedInUser.FriendRequests.Where(fr => fr.FromUserId == fromUserId).FirstOrDefault();
            if (requestToDelete != null)
            {
                db.FriendRequests.Remove(requestToDelete);
                try
                {
                    db.SaveChanges();
                    didDecline = true;
                }
                catch (InvalidOperationException)
                {
                    didDecline = false;
                }
            }

            return didDecline;
            
        }

    }
}
