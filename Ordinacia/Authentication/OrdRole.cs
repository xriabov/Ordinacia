using System;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Security;
using Ordinacia.Data_Access;

namespace Ordinacia.Authentication
{
    public class OrdRole : RoleProvider
    {
        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new System.NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new System.NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new System.NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new System.NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new System.NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
                return null;

            using (var context = new AuthenticationDB())
            {
                var user = context.Usrs
                    .FirstOrDefault(us => string.Compare(username, us.UserName, StringComparison.OrdinalIgnoreCase) == 0);
                return user?.Roles.Select(r => r.RoleName).ToArray();
            }
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new System.NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            return GetRolesForUser(username).Contains(roleName);
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new System.NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new System.NotImplementedException();
        }

        public override string ApplicationName { get; set; }
    }
}