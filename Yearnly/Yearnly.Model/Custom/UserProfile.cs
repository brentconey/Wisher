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
        public static Boolean DoesUserExist(String userName, YearnlyEntities db)
        {
            Boolean returnValue = false;
            UserProfile user = db.UserProfiles.Where(up => up.UserName == userName).FirstOrDefault();
            if (user != null)
            {
                returnValue = true;
            }
            return returnValue;
        }

        public static IEnumerable<UserProfile> SearchUsers(string searchText, YearnlyEntities db)
        {
            return db.UserProfiles.Where(up => up.UserName.Contains(searchText) || up.FirstName.Contains(searchText) || up.LastName.Contains(searchText)).ToList();
        }
    }
}
