using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using Ordinacia.Authentication;
using Ordinacia.Data_Access;
using Ordinacia.Models;
using Ordinacia.ViewModels;

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
                
                VM.Patients = patients;
                VM.Medicines = context.Medicines.ToList();
                VM.Pharmacies = context.Medicines.Select(x => x.PharmacyName).Distinct().ToList();
            }
            return View(VM);
        }

        public ActionResult Pharmacists()
        {
            return View();
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
    }
}