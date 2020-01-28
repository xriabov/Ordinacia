using System;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using Ordinacia.Authentication;
using Ordinacia.Data_Access;
using Ordinacia.Models;
using Ordinacia.ViewModels;

namespace Ordinacia.Controllers
{
    
    public class AccountController : Controller
    {
        // GET
        public ActionResult Index()
        {
            if (User.IsInRole("Doctor"))
                return RedirectToAction("Index", "Doctor");
            if (User.IsInRole("Insurance worker"))
                return RedirectToAction("Index", "InsuranceWorker");
            if (User.IsInRole("Pharmacist"))
                return RedirectToAction("Index", "Pharmacist");
            return RedirectToAction("Login");
        }

        [HttpGet]
        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
                Logout();
            return View();
        }

        public void Logout()
        {
            if (Request.Cookies["AuthCookieOrd"] != null)
                Response.Cookies["AuthCookieOrd"].Expires = DateTime.Now.AddHours(-1);
        }

        [HttpPost]
        public ActionResult Login(LoginVM loginVM)
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(loginVM.Username, loginVM.Password))
                {
                    var user = (OrdMembershipUser) Membership.GetUser(loginVM.Username, false);
                    if (user != null)
                    {
                        var userModel = new SerializeModel()
                        {
                            UserID = user.UserID,
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            RoleName = user.Roles.Select(r => r.RoleName).ToList(),
                        };

                        string userData = JsonConvert.SerializeObject(userModel);
                        FormsAuthenticationTicket ticket;
                        if (loginVM.Remember.HasValue && loginVM.Remember.Value)
                            ticket = new FormsAuthenticationTicket(
                                1, loginVM.Username, DateTime.Now,
                                DateTime.Now.AddDays(1), true, userData
                            );
                        else
                            ticket = new FormsAuthenticationTicket(
                                1, loginVM.Username, DateTime.Now,
                                DateTime.Now.AddMinutes(30), false, userData
                            );
                        string enTicket = FormsAuthentication.Encrypt(ticket);
                        HttpCookie auCookie = new HttpCookie("AuthCookieOrd", enTicket);
                        Response.Cookies.Add(auCookie);
                    }
                }
                
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Error: Invalid username or password.");
            return View(loginVM);
        }
    }
}