using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Principal;
using Ordinacia.Data_Access;

namespace Ordinacia.ViewModels
{
    public class CoWorker
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class CoWorkerPharmacist : CoWorker
    {
        public string Pharmacy { get; set; }
    }

    public class CoWorkerInsuranceWorker : CoWorker
    {
        public string InsuranceName { get; set; }
    }

    public class CoWorkersVM
    {
        public IEnumerable<CoWorkerPharmacist> Pharmacists { get; set; }
        public IEnumerable<CoWorkerInsuranceWorker> InsuranceWorkers { get; set; }
        public NewCoWorker NewCoWorker { get; set; }
    }

    public class NewCoWorker
    {
        [Required] 
        public string UserName { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Employer { get; set; }
        [Required]
        public string Type { get; set; }
    }
}