using System.ComponentModel.DataAnnotations;
using Microsoft.Ajax.Utilities;

namespace Ordinacia.ViewModels
{
    public class RegistrationForm
    {
        [Required(ErrorMessage = "Username required")]
        public string Username { get; set; }
        
        [Required(ErrorMessage = "First name required")]
        public string FirstName { get; set; }
        
        [Required(ErrorMessage = "Last name required")]
        public string LastName { get; set; }
        
        [Required(ErrorMessage = "Password required")]
        [RegularExpression(".....")]
        public string Password { get; set; }
        
        [Required(ErrorMessage = "Repeat your password")]
        [Compare(nameof(Password), ErrorMessage = "Passwords must be equal")]
        public string PasswordRep { get; set; }
        
    }
}