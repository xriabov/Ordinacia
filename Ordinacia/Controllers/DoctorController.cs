using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using Ordinacia.Authentication;
using Ordinacia.Data_Access;
using Ordinacia.Models;

namespace Ordinacia.Controllers
{
    [Auth(Roles = "Doctor")]
    public class DoctorController : Controller
    {
        private DoctorM doc;

        public void DocInit()
        {
            doc = new DoctorM();
            doc.Id = ((OrdPrincipal) HttpContext.User).UserID;
            doc.FirstName = ((OrdPrincipal) HttpContext.User).FirstName;
            doc.LastName = ((OrdPrincipal) HttpContext.User).LastName;
            using (AuthenticationDB context = new AuthenticationDB())
            {
                doc.Specialization = context.Docs.FirstOrDefault(d => d.RefUser.UserId == doc.Id)?.Specialization;
            }
        }

        public ActionResult Index()
        {
            DocInit();
            return View(doc);
        }

        public ActionResult Patients()
        {
            var VM = new DocPatientsVM();
            using (AuthenticationDB context = new AuthenticationDB())
            {
                var patients = context.Patients.Where(p =>
                    p.Doctor.RefUser.UserId == ((OrdPrincipal)HttpContext.User).UserID).ToList();
                if(patients == null)
                    patients = new List<Patient>();
                var medicines = new Dictionary<string, ICollection<Medicine>>();
                foreach (var medicine in context.Medicines)
                {
                    if(!medicines.Keys.Contains(medicine.PharmacyName))
                        medicines[medicine.PharmacyName] = new List<Medicine>();
                    medicines[medicine.PharmacyName].Append(medicine);
                }
                VM.Patients = patients;
                VM.Medicines = medicines;
            }
            return View(VM);
        }

        public ActionResult Pharmacists()
        {
            return View();
        }

        public PartialViewResult RenderMedicines(string currentPharmacy)
        {
            
        }
    }
}