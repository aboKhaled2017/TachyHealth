using Interact.GateInvitations.Core.Data;
using Interact.GateInvitations.Core.Repositories;
using Interact.GateInvitations.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interact.GateInvitations.DAL.Services
{
    public class InvitationService : IInvitationService
    {
        private readonly IRepository<Invitation, Guid> _repository;
        private readonly IRepository<Customer, Guid> _customerRepository;
        public InvitationService(IRepository<Invitation, Guid> repository, IRepository<Customer, Guid> customerRepository)
        {
            _repository = repository;
            _customerRepository = customerRepository;
        }
        public async Task MakeInvite(Invitation entity)
        {
            if (!await _customerRepository.AnyAsync(e => e.Id == entity.CustomerId))
            {
                throw new Exception("Invalid customer id");
            }
            await _repository.InsertAsync(entity);
        }
    }
}
