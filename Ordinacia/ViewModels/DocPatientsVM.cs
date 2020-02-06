using System.Collections.Generic;
using Ordinacia.Data_Access;

namespace Ordinacia.Models
{
    public class DocPatientsVM
    {
        public ICollection<Patient> Patients { get; set; }
        public Dictionary<string, ICollection<Medicine>> Medicines { get; set; }
    }
}