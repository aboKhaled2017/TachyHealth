using Interact.GateInvitations.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interact.GateInvitations.Core.Services
{
    public interface ISecurityKeeperService
    {
        Task<(bool isValid,(string propName,string errorMessage) error)> Register(SecurityKeeper entity);
        Task<(bool isValid,TViewModel InvitationDetails)> ValideQrCodeAsync<TViewModel>(string code) where TViewModel : BaseViewModel;
        Task AddInvitationLoginAsync(InviteeLogin inviteeLogin);
    }
}
