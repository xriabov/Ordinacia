using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using Ordinacia.Authentication;
using Ordinacia.Data_Access;
using Ordinacia.ViewModels;
using WebGrease.Css.Extensions;

namespace Ordinacia.Controllers
{
    [Auth(Roles = "Pharmacist")]
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
                    FirstName = ((OrdPrincipal) HttpContext.User).FirstName,
                    LastName = ((OrdPrincipal) HttpContext.User).LastName,
                    PharmacyName = db.Pharms.FirstOrDefault(p => p.RefUser.UserId == id).Pharmacy,
                };
                return View(VM);
            }
        }

        public ActionResult Orders()
        {
            using (var db = new AuthenticationDB())
            {
                string pharmacy = db.Pharms.FirstOrDefault(u =>
                    u.RefUser.UserId == ((OrdPrincipal) HttpContext.User).UserID).Pharmacy;

                var VM = db.Docs
                    .Select(d => new DocPrice
                    {
                        Id = d.DocID,
                        FirstName = d.RefUser.FirstName,
                        LastName = d.RefUser.LastName,
                        Price = d.Patients
                            .SelectMany(p => p.Medicines)
                            .Where(m => m.PharmacyName == pharmacy)
                            .Sum(x => x.Price)
                    }).ToList();
                return View(VM);
            }
        }

        public PartialViewResult RenderMedicines(int id)
        {
            using (var db = new AuthenticationDB())
            {
                string pharmacy = db.Pharms.FirstOrDefault(u =>
                    u.RefUser.UserId == ((OrdPrincipal) HttpContext.User).UserID).Pharmacy;
                var VM = db.Docs.FirstOrDefault(d => d.DocID == id).Patients.SelectMany(p => p.Medicines)
                    .Where(m => m.PharmacyName == pharmacy).ToList();
                ViewBag.Price = VM.Sum(x => x.Price); // I don't like ViewModels anymore
                ViewBag.Id = id;
                return PartialView(VM);
            }
        }

        public FileResult PrintMedicines(int id)
        {
            StreamWriter writer = new StreamWriter(Server.MapPath("~/tmp/Meds.txt"));
            using (var db = new AuthenticationDB())
            {
                string pharmacy = db.Pharms.FirstOrDefault(u =>
                    u.RefUser.UserId == ((OrdPrincipal) HttpContext.User).UserID).Pharmacy;
                var doc = db.Docs.FirstOrDefault(p => p.DocID == id);
                writer.WriteLine(doc.RefUser.FirstName + " " + doc.RefUser.LastName);
                var meds = db.Docs.FirstOrDefault(d => d.DocID == id).Patients.SelectMany(p => p.Medicines)
                    .Where(m => m.PharmacyName == pharmacy).ToList();
                foreach (var med in meds)
                {
                    writer.WriteLine(med.Name + "\t" + med.Price);
                }

                writer.WriteLine("Overall price:\t" + meds.Sum(x => x.Price));
                writer.Close();
            }
            
            byte[] fileBytes = System.IO.File.ReadAllBytes(Server.MapPath("~/tmp/Meds.txt"));
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Text.Plain, "Medicines.txt");
        }

        
        
        public ActionResult Medicines()
        {
            using (var db = new AuthenticationDB())
            {
                var pharmacy = db.Pharms.FirstOrDefault(u =>
                    u.RefUser.UserId == ((OrdPrincipal) HttpContext.User).UserID).Pharmacy;
                
                return View(db.Medicines.Where(m => m.PharmacyName == pharmacy).ToList());
            }
        }

        public ActionResult DeleteMedicine(int id)
        {
            using (var db = new AuthenticationDB())
            {
                db.Medicines.Remove(db.Medicines.FirstOrDefault(m => m.MedicineID == id));
                db.SaveChanges();
            }
            return RedirectToAction("Medicines");
        }

        public ActionResult EditMedicine(int id, double price)
        {
            using (var db = new AuthenticationDB())
            {
                db.Medicines.FirstOrDefault(m => m.MedicineID == id).Price = price;
                db.SaveChanges();
            }
            return RedirectToAction("Medicines");
        }
        
        public ActionResult AddMedicine(string name, double price)
        {
            using (var db = new AuthenticationDB())
            {
                string pharmacy = db.Pharms.FirstOrDefault(u =>
                    u.RefUser.UserId == ((OrdPrincipal) HttpContext.User).UserID).Pharmacy;
                db.Medicines.Add(new Medicine {Name = name, PharmacyName = pharmacy, Price = price});
                db.SaveChanges();
            }
            return RedirectToAction("Medicines");
        }
    }
}