using Interact.GateInvitations.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Interact.GateInvitations.WebAPI.ViewModels.Admin
{
    public record ShowSecurityKeeperForAdminViewModel:BaseViewModel
    {
        public Guid SecurityKeeperId { get; set; }
        public string Username { get; set; }
        public int LoginTries { get; set; }
    }
}
