using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interact.GateInvitations.Core.Data
{
    public class Entity { }
    public class BaseEntity<TID>:Entity where TID : struct
    {
        public BaseEntity()
        {
            GenerateId();
        }
        protected virtual void GenerateId()
        {
            Id = default;
        }
        public TID Id { get; set; }
    }
}
