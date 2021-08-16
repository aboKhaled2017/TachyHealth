using Interact.GateInvitations.Common.Enums;
using Interact.GateInvitations.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Interact.GateInvitations.WebAPI.ViewModels.Invitation
{
    public record InvitationLoginStatusViewModel:BaseViewModel
    {
        [Required(ErrorMessage ="Invitation id is requird")]
        public Guid InvitationId { get; set; }
        [Required(ErrorMessage = "Status is required")]
        public InviteeLoginStatus Status { get; set; } = InviteeLoginStatus.SuccessToEnter;
        public string Reason { get; set; } = null;
    }
}
