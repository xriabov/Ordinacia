using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using Newtonsoft.Json;
using Ordinacia.Authentication;
using Ordinacia.Models;

namespace Ordinacia
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        
        protected void Application_PostAuthenticateRequest(object sender, EventArgs e)
        {
            HttpCookie cookie = Request.Cookies["AuthCookieOrd"];
            if (cookie != null)
            {
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(cookie.Value);
                var serializeData = JsonConvert.DeserializeObject<SerializeModel>(authTicket.UserData);
                var principal = new OrdPrincipal(authTicket.Name)
                {
                    UserID = serializeData.UserID,
                    FirstName = serializeData.FirstName,
                    LastName = serializeData.LastName,
                    Roles = serializeData.RoleName.ToArray<string>()
                };
                HttpContext.Current.User = principal;
            }
        }
    }
}