using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ordinacia.Data_Access
{
    public class Patient
    {
        [Key]
        public int PatientID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
        public string InsuranceName { get; set; }
        public Doc Doctor { get; set; }
        public virtual ICollection<Medicine> Medicines { get; set; }
    }
}