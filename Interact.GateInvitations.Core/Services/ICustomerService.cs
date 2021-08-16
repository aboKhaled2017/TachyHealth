using Interact.GateInvitations.Core.Data;
using Interact.GateInvitations.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interact.GateInvitations.Core.Services
{
    public interface ICustomerService
    {
        Task<(bool isValid, RegisterCustomerError error)> RegisterCustomer(Customer customer);
        Task<bool> IsActivated(Guid customerId);
        public class RegisterCustomerError
        {
            public RegisterCustomerError(string propName, string errorMessage)
            {
                this.PropName = propName;
                this.ErrorMessage = errorMessage;
            }
            public string PropName { get;private set; }
            public string ErrorMessage { get;private set; }
        }
    }
}
