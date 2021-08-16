using Interact.GateInvitations.Core.Data;
using Interact.GateInvitations.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interact.GateInvitations.Core.Services
{
    public interface IAdminService
    {
        Task<Admin> GetAdminByIdAsync(Guid Id);
        Task<Admin> GetAdminByUsernameAsync(string Username);
        Task<Admin> AddNewAdminAsync(string username,string password, string name);
        Task<IReadOnlyList<TViewModel>> GetAllInvitationsAsync<TViewModel>();
        Task<IReadOnlyList<TViewModel>> GetAllCustomersAsync<TViewModel>();
        Task<ShowStaticOfDashbordDataViewModel> GetStaticDataOfAdminDashbord();
        Task<IReadOnlyList<TViewModel>> GetAllSecurityKeepersAsync<TViewModel>();
        Task<(bool status,string errorMess)> SetCustomerStatus(Guid customerId);
    }
}
