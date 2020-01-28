using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Ordinacia.Data_Access
{
    public class Medicine
    {
        [Key]
        public int MedicineID { get; set; }
        public string Name { get; set; }
        public string PharmacyName { get; set; }
        public double Price { get; set; }
        public virtual ICollection<Patient> Patients { get; set; }
    }
}