using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yearnly.Model
{
    public partial class UserProfile
    {
        //This method will either load a user or load an empty user.
        public static UserProfile LoadUserByUserName(String userName, YearnlyEntities db)
        {
            UserProfile returnedUser = new UserProfile();
            UserProfile user = db.UserProfiles.Where(up => up.UserName == userName).FirstOrDefault();
            if (user != null)
            {
                returnedUser = user;
            }
            return returnedUser;
        }

        public static UserProfile LoadUserByUserId(int userId, YearnlyEntities db)
        {
            UserProfile returnedUser = new UserProfile();
            returnedUser = db.UserProfiles.Where(up => up.UserId == userId).FirstOrDefault();
            return returnedUser;
        }


        //This is a quick checker to see if a user exists
        public static Boolean DoesUserExists(String userName, YearnlyEntities db)
        {
            Boolean returnValue = false;
            UserProfile user = db.UserProfiles.Where(up => up.UserName == userName).FirstOrDefault();
            if (user != null)
            {
                returnValue = true;
            }
            return returnValue;
        }

        public IEnumerable<UserProfile> GetFriends(YearnlyEntities db)
        {
            List<UserProfile> usersFriends = new List<UserProfile>();
            List<int> friendIds = db.Friends.Where(frnds => frnds.UserId == this.UserId).Select(fr => fr.FriendId).ToList();
            foreach (int friendId in friendIds)
            {
                usersFriends.Add(UserProfile.LoadUserByUserId(friendId, db));
            }
            return usersFriends;
        }
    }
}
