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
    public class Customer:BaseEntity<Guid>
    {
        public Customer()
        {
            Invitations = new HashSet<Invitation>();
        }
 
        public string Phone { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string DestrictNumber { get; set; }
        public string AttachmentUrl { get; set; }
        public string VillaNumber { get; set; }
        public UserStatus UserStatus { get; set; } = UserStatus.InActive;
        [ForeignKey(nameof(Id))]
        public virtual User User { get; set; }
        public virtual ICollection<Invitation> Invitations { get; set; }
    }
}
