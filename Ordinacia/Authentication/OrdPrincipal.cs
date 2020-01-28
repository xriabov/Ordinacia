using System.Linq;
using System.Security.Principal;

namespace Ordinacia.Authentication
{
    public class OrdPrincipal: IPrincipal
    {
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string[] Roles { get; set; }
        public IIdentity Identity { get; private set; }
        
        public bool IsInRole(string role)
        {
            if (Roles.Any(r => role.Contains(r)))
                return true;
            return false;
        }

        public OrdPrincipal(string username)
        {
            Identity = new GenericIdentity(username);
        }
        
        
    }
}