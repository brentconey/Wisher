//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Yearnly.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class UserProfile
    {
        public UserProfile()
        {
            this.UserItems = new HashSet<UserItem>();
            this.UserLists = new HashSet<UserList>();
            this.Friends = new HashSet<Friend>();
        }
    
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    
        public virtual ICollection<UserItem> UserItems { get; set; }
        public virtual ICollection<UserList> UserLists { get; set; }
        public virtual ICollection<Friend> Friends { get; set; }
    }
}
