using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Interact.GateInvitations.WebAPI.ViewModels
{
    public class JwtResponseModel
    {
        public string Token { get; set; }
        public long Expiry { get; set; }
    }
}
