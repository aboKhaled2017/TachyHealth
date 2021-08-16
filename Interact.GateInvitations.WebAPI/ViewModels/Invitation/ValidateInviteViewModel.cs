using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Interact.GateInvitations.WebAPI.ViewModels.Invitation
{
    public record  ValidateInviteViewModel
    {
        [Required]
        public string QrCode { get; set; }

    }
}
