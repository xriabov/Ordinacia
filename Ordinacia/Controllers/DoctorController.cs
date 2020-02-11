using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using Microsoft.EntityFrameworkCore.Internal;
using Ordinacia.Authentication;
using Ordinacia.Data_Access;
using Ordinacia.Models;
using Ordinacia.ViewModels;
using Org.BouncyCastle.Ocsp;

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
                    p.Doctor.RefUser.UserId == ((OrdPrincipal) HttpContext.User).UserID).ToList();
                if (patients == null)
                    patients = new List<Patient>();
                
                VM.Patients = patients;
                VM.Medicines = context.Medicines.ToList();
                VM.Pharmacies = context.Pharms.Where(u => u.Doc.UserId == ((OrdPrincipal) User).UserID)
                    .Select(p => p.Pharmacy).Distinct().ToList();
                //VM.Pharmacies = context.Medicines.Select(x => x.PharmacyName).Distinct().ToList();
            }

            return View(VM);
        }

        public PartialViewResult RenderMedicines(string currentPharmacy, int currentPatient)
        {
            using (var db = new AuthenticationDB())
            {
                var VM = new DocMedicinesVM();
                VM.Medicines = db.Medicines.Where(m => m.PharmacyName == currentPharmacy).ToList();
                VM.CurrentPatientId = currentPatient;
                return PartialView(VM);
            }
        }

        public PartialViewResult GetPatientData(int id)
        {
            using (var db = new AuthenticationDB())
            {
                Patient patient = db.Patients
                    .FirstOrDefault(x =>
                        x.PatientID == id && x.Doctor.RefUser.UserId == ((OrdPrincipal) HttpContext.User).UserID);
                return PartialView(patient);
            }
        }

        public PartialViewResult GetPatientMedicines(int id)
        {
            using (var db = new AuthenticationDB())
            {
                PatientMedicinesVM VM = new PatientMedicinesVM();
                VM.Medicines = db.Patients
                    .FirstOrDefault(x =>
                        x.PatientID == id && x.Doctor.RefUser.UserId == ((OrdPrincipal) HttpContext.User).UserID)
                    ?.Medicines;
                VM.PatientId = id;

                if (VM.Medicines != null)
                    VM.Price = VM.Medicines.Sum(x => x.Price);
                return PartialView(VM);
            }
        }

        public void AddMedicine(int medicineId, int currentPatient)
        {
            using (var db = new AuthenticationDB())
            {
                db.Patients
                    .FirstOrDefault(p => (p.PatientID == currentPatient)
                                         & (p.Doctor.RefUser.UserId == ((OrdPrincipal) HttpContext.User).UserID))
                    ?.Medicines
                    .Add(db.Medicines.FirstOrDefault(m => m.MedicineID == medicineId));
                db.SaveChanges();
            }
        }

        public void DeleteMedicine(int patient, int medicine)
        {
            using (var db = new AuthenticationDB())
            {
                db.Patients
                    .FirstOrDefault(p => (p.PatientID == patient)
                                         && (p.Doctor.RefUser.UserId == ((OrdPrincipal) HttpContext.User).UserID))
                    ?.Medicines
                    .Remove(db.Medicines.FirstOrDefault(m => m.MedicineID == medicine));
                db.SaveChanges();
            }
        }

        public void DeletePatient(int patient)
        {
            using (var db = new AuthenticationDB())
            {
                // ReSharper disable once AssignNullToNotNullAttribute
                db.Patients.Remove(db.Patients.FirstOrDefault(p => (p.PatientID == patient)
                                                                   && (p.Doctor.RefUser.UserId ==
                                                                       ((OrdPrincipal) HttpContext.User).UserID)));
                db.SaveChanges();
            }
        }
        

        [HttpGet]
        public PartialViewResult AddPatient()
        {
            return PartialView();
        }
        
        [HttpPost]
        public ActionResult AddPatient(PatientForm patientForm)
        {
            using (var db = new AuthenticationDB())
            {
                db.Patients.Add(new Patient
                {
                    FirstName = patientForm.FirstName,
                    LastName = patientForm.LastName,
                    Doctor = db.Docs.FirstOrDefault(d => d.RefUser.UserId == ((OrdPrincipal) HttpContext.User).UserID),
                    Height = patientForm.Height,
                    Weight = patientForm.Weight,
                    InsuranceName = patientForm.InsuranceName,
                    Medicines = new List<Medicine>(),
                });
                db.SaveChanges();
            }

            return RedirectToAction("Patients");
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
        
        public ActionResult CoWorkers()
        {
            return View();
        }
    }
}