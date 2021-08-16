using Interact.GateInvitations.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Interact.GateInvitations.WebAPI.ViewModels.Admin
{
    public record ShowInvitationForAdminViewModel:BaseViewModel
    {
        public Guid InviterId { get; set; }
        public string InviterName { get; set; }
        public string InviteeName { get; set; }
        public string ImgUrl { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string QRCodeImgUrl { get; set; }
        public string InviterUsername { get; set; }
    }
}
