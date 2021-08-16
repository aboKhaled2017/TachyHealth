using Interact.GateInvitations.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Interact.GateInvitations.WebAPI.ViewModels.Admin
{
    public record ShowCustomerForAdminViewModel:BaseViewModel
    {
        public Guid CustomerId { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string VillaNumber { get; set; }
        public string UserStatus { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }
        public string DestrictNumber { get; set; }
        public string Attachment { get; set; }
        public int InvitationsCount { get; set; }
    }
}
