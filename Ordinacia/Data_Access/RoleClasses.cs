using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ordinacia.Data_Access
{
    public class Doc
    {
        [Key]
        public int DocID { get; set; }
        public virtual User RefUser { get; set; }
        public string Specialization { get; set; }
        public virtual ICollection<Patient> Patients { get; set; }
    }
    public class Pharm
    {
        [Key]
        public int PharmID { get; set; }
        public virtual User RefUser { get; set; }
        public string Pharmacy { get; set; }
    }
    public class InW
    {
        [Key]
        public int InWID { get; set; }
        public virtual User RefUser { get; set; }
        public string InsuranceName { get; set; }
    }
    
}