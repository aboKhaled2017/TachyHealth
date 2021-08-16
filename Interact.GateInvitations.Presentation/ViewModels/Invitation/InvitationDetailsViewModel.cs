using Interact.GateInvitations.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Interact.GateInvitations.WebAPI.ViewModels.Invitation
{
    public record InvitationDetailsViewModel:BaseViewModel
    {
        public string InviteeName { get; set; }
        public string InviteeId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string InviterVillaNumber  { get; set; }
        public string InviterPhone { get; set; }
        public string InviterName { get; set; }
    }
}
