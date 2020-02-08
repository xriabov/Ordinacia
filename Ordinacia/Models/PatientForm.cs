using System.ComponentModel.DataAnnotations;
using Org.BouncyCastle.Utilities.IO.Pem;

namespace Ordinacia.Models
{
    public class PatientForm
    {
        [Required(ErrorMessage = "Enter first name")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Enter last name")]
        public string LastName { get; set; }
        [Required(ErrorMessage="Enter insurance name")]
        public string InsuranceName { get; set; }
        [Required(ErrorMessage = "Enter weight")]
        public double Weight { get; set; }
        [Required(ErrorMessage = "Enter height")]
        public double Height { get; set; }
    }
}