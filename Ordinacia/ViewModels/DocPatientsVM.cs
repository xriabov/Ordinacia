using System.Collections.Generic;
using Ordinacia.Data_Access;

namespace Ordinacia.Models
{
    public class DocPatientsVM
    {
        public ICollection<Patient> Patients { get; set; }
        public ICollection<Medicine> Medicines { get; set; }
        public ICollection<string> Pharmacies { get; set; }
        public string SelectedPharmacy { get; set; }
    }
}