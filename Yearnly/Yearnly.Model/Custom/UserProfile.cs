using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yearnly.Model
{
    public partial class UserProfile
    {
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
    }
}
