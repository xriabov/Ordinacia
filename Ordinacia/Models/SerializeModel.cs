using System.Collections.Generic;

namespace Ordinacia.Models
{
    public class SerializeModel
    {
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<string> RoleName { get; set; }
    }
}