using Interact.GateInvitations.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Interact.GateInvitations.WebAPI.ViewModels.Customer
{
    public record CustomerRegisterViewModel:BaseViewModel
    {
        [Required(ErrorMessage ="Username is required")]
        [StringLength(25,MinimumLength =5,ErrorMessage ="Username has at least 5 characters and 25 at max")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "Passord has at least 5 characters and 30 at max")]
        public string Password { get; set; }
        [RegularExpression("^((010)|(011)|(012)|(015)|(017))[0-9]{8}$", ErrorMessage = "Phone number is invalid")]
        [Required(ErrorMessage = "Phone Number is required")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Email Address is required")]
        [EmailAddress(ErrorMessage ="Email Address is invalid")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Villa number is required")]
        [StringLength(30,MinimumLength =2,ErrorMessage ="The number is not valid")]
        public string VillaNumber { get; set; }
    }
}
