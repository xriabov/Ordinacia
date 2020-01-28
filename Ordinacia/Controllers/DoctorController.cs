using System.Web.Mvc;
using Ordinacia.Authentication;

namespace Ordinacia.Controllers
{
    [Auth(Roles="Doctor")]
    public class DoctorController : Controller
    {
        // GET
        public ActionResult Index()
        {
            return View();
        }
    }
}