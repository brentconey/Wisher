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
    
    public partial class Friend
    {
        public int UserId { get; set; }
        public int FriendId { get; set; }
        public System.DateTime DateAdded { get; set; }
    
        public virtual UserProfile FriendProfile { get; set; }
    }
}
