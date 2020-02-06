using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Ordinacia.Authentication;
using Ordinacia.Data_Access;

namespace Ordinacia.Models
{
    public class DoctorM
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Specialization { get; set; }
    }
}