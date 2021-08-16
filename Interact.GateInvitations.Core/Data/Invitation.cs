using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interact.GateInvitations.Core.Data
{
    public class Invitation:BaseEntity<Guid>
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid CustomerId { get; set; }
        public string InvitedName { get; set; }
        public string InvitedId { get; set; }
        public string ImgUrl { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string QRCode { get; set; }
        public string QRCodeImgUrl { get; set; }
        [ForeignKey(nameof(CustomerId))]
        public Customer Customer { get; set; }

        protected override void GenerateId()
        {
            Id = Guid.NewGuid();
        }
    }
}
