using Interact.GateInvitations.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interact.GateInvitations.Core.Data
{
    public class InviteeLogin:BaseEntity<Guid>
    {
        public InviteeLoginStatus LoginStatus { get; set; }
        public string Comment { get; set; }
        public Guid HandlerSecurityKeeperId { get; set; }
        public Guid RelatedInvitationId { get; set; }
        [ForeignKey(nameof(HandlerSecurityKeeperId))]
        public virtual SecurityKeeper HandlerSecurityKeeper { get; set; }
        [ForeignKey(nameof(RelatedInvitationId))]
        public virtual Invitation RelatedInvitation { get; set; }

        protected override void GenerateId()
        {
            Id = Guid.NewGuid();
        }
    }
}
