using Interact.GateInvitations.Core.Data;
using Interact.GateInvitations.Core.Repositories;
using Interact.GateInvitations.Core.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Interact.GateInvitations.Core.Services.ICustomerService;

namespace Interact.GateInvitations.DAL.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IRepository<Customer,Guid> _repository;
        public CustomerService(IRepository<Customer, Guid> repository)
        {
            _repository = repository;
        }

        public async Task<bool> IsActivated(Guid customerId)
        {
            return await _repository.AnyAsync(c => c.Id == customerId && c.UserStatus == Common.Enums.UserStatus.Active);
        }


        public async Task<(bool isValid, ICustomerService.RegisterCustomerError error)>  RegisterCustomer(Customer customer)
        {
            var entity = await _repository.Where(e =>
                     e.Email == customer.Email ||
                     e.Phone == customer.Phone ||
                     e.User.Username == customer.User.Username)
                .Include(e=>e.User)
                .FirstOrDefaultAsync();
            if (entity != null)
            {
                if (entity.User.Username == customer.User.Username)
                    return (false, new RegisterCustomerError(nameof(customer.User.Username), "User name is already exists"));
                if (entity.Email == customer.Email)
                    return (false, new RegisterCustomerError(nameof(customer.Email), "Email is already exists"));
                if (entity.Phone == customer.Phone)
                    return (false, new RegisterCustomerError(nameof(customer.Phone), "Phone is already exists"));
            }
            await _repository.InsertAsync(customer);
            return (true, null);
        }
    }
}
