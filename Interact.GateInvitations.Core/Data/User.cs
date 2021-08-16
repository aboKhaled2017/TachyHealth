using Interact.GateInvitations.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interact.GateInvitations.Core.Data
{
    public class User:BaseEntity<Guid>
    {
        public string Username { get; set; }
        
        public string Password { get; set; }
        public UserType UserType { get; set; }
        protected override void GenerateId()
        {
            Id = Guid.NewGuid();
        }
    }
}
