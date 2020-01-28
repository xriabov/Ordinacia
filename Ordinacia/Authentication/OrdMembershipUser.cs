using System;
using System.Collections.Generic;
using System.Web.Security;
using Ordinacia.Data_Access;

namespace Ordinacia.Authentication
{
    public class OrdMembershipUser : MembershipUser
    {
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<Role> Roles { get; set; }
        
        public OrdMembershipUser(User user):base("CustomMembership", user.UserName, user.UserId, string.Empty, string.Empty, string.Empty, true, false, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now)
        {
            UserID = user.UserId;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Roles = user.Roles;
        }
    }
    
}