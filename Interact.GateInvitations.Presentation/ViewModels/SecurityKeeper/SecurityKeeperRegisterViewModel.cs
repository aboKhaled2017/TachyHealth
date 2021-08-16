using Interact.GateInvitations.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Interact.GateInvitations.WebAPI.ViewModels.Customer
{
    public record SecurityKeeperRegisterViewModel:BaseViewModel
    {
        [Required(ErrorMessage ="Username is required")]
        [StringLength(25,MinimumLength =5,ErrorMessage ="Username has at least 5 characters and 25 at max")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "Passord has at least 5 characters and 30 at max")]
        public string Password { get; set; }
    }
}
