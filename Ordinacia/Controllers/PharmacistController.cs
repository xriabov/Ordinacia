using System.Linq;
using System.Web.Mvc;
using Ordinacia.Authentication;
using Ordinacia.Data_Access;
using Ordinacia.ViewModels;

namespace Ordinacia.Controllers
{
    [Auth(Roles="Pharmacist")]
    public class PharmacistController : Controller
    {
        // GET
        public ActionResult Index()
        {
            using (var db = new AuthenticationDB())
            {
                int id = ((OrdPrincipal) HttpContext.User).UserID;
                var VM = new PharmVM
                {
                    FirstName = ((OrdPrincipal)HttpContext.User).FirstName,
                    LastName=((OrdPrincipal)HttpContext.User).LastName,
                    PharmacyName = db.Pharms.FirstOrDefault(p => p.RefUser.UserId == id).Pharmacy,
                };
                return View(VM);
            }
        }

        public ActionResult Orders()
        {
            return View();
        }

        public ActionResult Medicines()
        {
            return View();
        }

    }
}