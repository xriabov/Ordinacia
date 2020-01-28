using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Ordinacia.ViewModels
{
    public class RegistrationVM
    {
        public RegistrationForm Form { get; set; }
        public bool State { get; set; }
        public string ErrorMessage { get; set; }
    }
}