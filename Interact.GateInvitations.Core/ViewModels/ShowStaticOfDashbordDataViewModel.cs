using Interact.GateInvitations.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Interact.GateInvitations.Core.ViewModels
{
    public record ShowStaticOfDashbordDataViewModel
    {
        public int CustomersCount { get; set; }
        public int SecurityKeepersCount { get; set; }
        public int InvitationsCount { get; set; }
        public int LoginTriesCount { get; set; }
    }
}
