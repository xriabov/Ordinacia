using System.Web.Mvc;
using Ordinacia.Authentication;

namespace Ordinacia.Controllers
{
    [Auth(Roles="Pharmacist")]
    public class PharmacistController : Controller
    {
        // GET
        public ActionResult Index()
        {
            return View();
        }
    }
}