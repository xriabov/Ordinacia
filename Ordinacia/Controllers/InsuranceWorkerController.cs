using System.IO;
using System.Linq;
using System.Web.Mvc;
using Ordinacia.Authentication;
using Ordinacia.Data_Access;
using Ordinacia.ViewModels;

namespace Ordinacia.Controllers
{
    [Auth(Roles = "Insurance worker")]
    public class InsuranceWorkerController : Controller
    {
        // GET
        public ActionResult Index()
        {
            using (var db = new AuthenticationDB())
            {
                int id = ((OrdPrincipal) HttpContext.User).UserID;
                ViewBag.FirstName = db.Usrs.FirstOrDefault(u => u.UserId == id).FirstName;
                ViewBag.LastName = db.Usrs.FirstOrDefault(u => u.UserId == id).LastName;
                ViewBag.InsuranceName = db.InWs.FirstOrDefault(u => u.RefUser.UserId == id).InsuranceName;
            }

            return View();
        }

        public ActionResult Patients()
        {
            using (var db = new AuthenticationDB())
            {
                string ins = db.InWs.FirstOrDefault(u => u.RefUser.UserId == ((OrdPrincipal) User).UserID)
                    .InsuranceName;
                var patients = db.Patients
                    .Where(p => p.InsuranceName == ins && p.Doctor.RefUser.UserId ==
                                db.InWs.FirstOrDefault(u => u.RefUser.UserId == ((OrdPrincipal) User).UserID).Doc.UserId).Select(
                        pat => new InsWVM
                        {
                            FirstName = pat.FirstName,
                            LastName = pat.LastName,
                            Id = pat.PatientID,
                            Price = pat.Medicines.Sum(x => x.Price),
                        }).ToList();
                return View(patients);
            }
        }

        public FileResult PrintMedicines(int patient)
        {
            StreamWriter writer = new StreamWriter(Server.MapPath("~/tmp/Meds.txt"));
            using (var db = new AuthenticationDB())
            {
                var pat = db.Patients.FirstOrDefault(p => p.PatientID == patient);
                writer.WriteLine(pat.FirstName + " " + pat.LastName);
                foreach (var med in pat.Medicines)
                {
                    writer.WriteLine(med.Name + "\t" + med.PharmacyName + "\t" + med.Price);
                }

                writer.WriteLine("Overall price:\t" + db.Medicines.Sum(x => x.Price));
                writer.Close();
            }

            byte[] fileBytes = System.IO.File.ReadAllBytes(Server.MapPath("~/tmp/Meds.txt"));
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Text.Plain, "Medicines.txt");
        }

        public PartialViewResult RenderMedicines(int id)
        {
            using (var db = new AuthenticationDB())
            {
                var VM = db.Patients.FirstOrDefault(p => p.PatientID == id).Medicines.ToList();
                ViewBag.Price = VM.Sum(x => x.Price);
                ViewBag.Id = id;
                return PartialView(VM);
            }
        }
    }
}