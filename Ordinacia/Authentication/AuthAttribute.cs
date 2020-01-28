using System.Web.Mvc;

namespace Ordinacia.Authentication
{
    public class AuthAttribute: AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectResult("/Account/Index");
        }
    }
}