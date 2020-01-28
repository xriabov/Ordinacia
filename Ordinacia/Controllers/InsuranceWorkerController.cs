using System.Web.Mvc;
using Ordinacia.Authentication;

namespace Ordinacia.Controllers
{
    [Auth(Roles="Insurance worker")]
    public class InsuranceWorkerController : Controller
    {
        // GET
        public ActionResult Index()
        {
            return View();
        }
    }
}