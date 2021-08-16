using Interact.GateInvitations.Core.Data;
using Interact.GateInvitations.Core.Infrastructure;
using Interact.GateInvitations.Core.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Interact.GateInvitations.WebAPI.Attributes.ModelValidationAtttributes
{
    public class IsValidCustomerIdAttribute: ValidationAttribute
    {
        private readonly IRepository<Customer, Guid> _customerRepository;
        public IsValidCustomerIdAttribute()
        {
            _customerRepository = AppServiceProvider.GetService<IRepository<Customer, Guid>>();
        }
       
        protected override ValidationResult IsValid(
        object value, ValidationContext validationContext)
        {
            if (value == null) return null;
            var errorRes = new ValidationResult(GetErrorMessage());
            if (!Guid.TryParse(value.ToString(),out Guid _id))
            {
                return errorRes;
            }
            var res = _customerRepository.AnyAsync(e => e.Id == _id).ConfigureAwait(true);
            if (!res.GetAwaiter().GetResult())
            {
                return errorRes;
            }
          
            return ValidationResult.Success;
        }

        public string GetErrorMessage()
        {
            return $"This customer id is not valid";
        }
    }
}
