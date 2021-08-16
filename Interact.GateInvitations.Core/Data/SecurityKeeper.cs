using Interact.GateInvitations.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interact.GateInvitations.Core.Data
{
    public class SecurityKeeper:BaseEntity<Guid>
    {
        public SecurityKeeper()
        {
            InvitationsLogins = new HashSet<InviteeLogin>();
        }
     
        [ForeignKey(nameof(Id))]
        public User User { get; set; }
        public virtual ICollection<InviteeLogin> InvitationsLogins { get; set; }
    }
}
