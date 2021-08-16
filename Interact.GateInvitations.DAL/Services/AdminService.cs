using AutoMapper.QueryableExtensions;
using Interact.GateInvitations.Common.Enums;
using Interact.GateInvitations.Core.Data;
using Interact.GateInvitations.Core.Infrastructure;
using Interact.GateInvitations.Core.Repositories;
using Interact.GateInvitations.Core.Services;
using Interact.GateInvitations.Core.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interact.GateInvitations.DAL.Services
{
    public class AdminService : IAdminService
    {
        #region Constructor and Properties
        private readonly IRepository<User, Guid> _usersRepository;
        private readonly IRepository<Invitation, Guid> _invitationsRepository;
        private readonly IRepository<InviteeLogin, Guid> _InviteeLoginRepository;
        private readonly IRepository<Admin, Guid> _repository;
        private readonly IRepository<Customer, Guid> _customerRepository;
        private readonly IRepository<SecurityKeeper, Guid> _securityKeeperRepository;
        public AdminService(IRepository<User, Guid> usersRepository, IRepository<Admin, Guid> repository = null, IRepository<Invitation, Guid> invitationsRepository = null, IRepository<Customer, Guid> customerRepository = null, IRepository<SecurityKeeper, Guid> securityKeeperRepository = null, IRepository<InviteeLogin, Guid> inviteeLoginRepository = null)
        {
            _usersRepository = usersRepository;
            _repository = repository;
            _invitationsRepository = invitationsRepository;
            _customerRepository = customerRepository;
            _securityKeeperRepository = securityKeeperRepository;
            _InviteeLoginRepository = inviteeLoginRepository;
        }
        #endregion


        public async Task<Admin> GetAdminByUsernameAsync(string Username)
        {
            var user =await _usersRepository.Where(u => u.Username == Username).SingleOrDefaultAsync();
            if (user is null) throw new Exception($"User with username {Username} is not found");
            var admin = await _repository.GetAsync(user.Id);
            if (admin is null) throw new Exception($"Admin with username {Username} is not found");
            return admin;
        }

        public async Task<Admin> GetAdminByIdAsync(Guid Id)
        {
            return await _repository.GetAsync(Id);
        }

        public async Task<Admin> AddNewAdminAsync(string username, string password,string name)
        {
            if(await _usersRepository.AnyAsync(e=>e.Username==username))return null;
            var admin = new Admin(username, password);
            admin.Name = name;
            await _repository.InsertAsync(admin);
            return admin;
        }

        public async Task<IReadOnlyList<TViewModel>> GetAllInvitationsAsync<TViewModel>()
        {
            return await  _invitationsRepository
                .All()
                .ProjectTo<TViewModel>(AutoMapperConfiguration.MapperConfiguration)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<TViewModel>> GetAllCustomersAsync<TViewModel>()
        {
            return await _customerRepository
                 .All()
                 .ProjectTo<TViewModel>(AutoMapperConfiguration.MapperConfiguration)
                 .ToListAsync();
        }

        public async Task<IReadOnlyList<TViewModel>> GetAllSecurityKeepersAsync<TViewModel>()
        {
            return await _securityKeeperRepository
                .All()
                .ProjectTo<TViewModel>(AutoMapperConfiguration.MapperConfiguration)
                .ToListAsync();
        }

        public async Task<(bool status, string errorMess)> SetCustomerStatus(Guid customerId)
        {
            var customer =await _customerRepository.GetAsync(customerId);
            if (customer is null) return (false, $"The customer with id {customerId} is not found");
            customer.UserStatus = customer.UserStatus == UserStatus.Active
                ? UserStatus.InActive
                : UserStatus.Active;
           await _customerRepository.UpdateAsync(customer);
            return (true, null);
        }

        public async Task<ShowStaticOfDashbordDataViewModel> GetStaticDataOfAdminDashbord()
        {
            return new ShowStaticOfDashbordDataViewModel
            {
                CustomersCount=await _customerRepository.All().CountAsync(),
                InvitationsCount= await _invitationsRepository.All().CountAsync(),
                LoginTriesCount= await _InviteeLoginRepository.All().CountAsync(),
                SecurityKeepersCount= await _securityKeeperRepository.All().CountAsync()
            };
        }
    }
}
