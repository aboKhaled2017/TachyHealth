using Interact.GateInvitations.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Interact.GateInvitations.WebAPI.ViewModels.Invitation
{
    public record InvitationDetailsViewModel:BaseViewModel
    {
        public string InviterName { get; set; }
        public string InviteeName { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
