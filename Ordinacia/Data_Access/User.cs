using System.Collections.Generic;

namespace Ordinacia.Data_Access
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual ICollection<Role> Roles { get; set; }
    }
}