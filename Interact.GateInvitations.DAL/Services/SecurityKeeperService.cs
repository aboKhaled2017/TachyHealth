using AutoMapper.QueryableExtensions;
using Interact.GateInvitations.Core;
using Interact.GateInvitations.Core.Data;
using Interact.GateInvitations.Core.Infrastructure;
using Interact.GateInvitations.Core.Repositories;
using Interact.GateInvitations.Core.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interact.GateInvitations.DAL.Services
{
    public class SecurityKeeperService : ISecurityKeeperService
    {
        private readonly IRepository<SecurityKeeper, Guid> _repository;
        private readonly IRepository<InviteeLogin, Guid> _inviteLoginRepository;
        private readonly IRepository<User, Guid> _userRepository;
        private readonly IRepository<Invitation, Guid> _invitationRepository;
        private readonly IRepository<SecurityKeeper, Guid> _securityKeeperRepository;
        public SecurityKeeperService(IRepository<SecurityKeeper, Guid> repository, IRepository<Invitation, Guid> invitationRepository, IRepository<InviteeLogin, Guid> inviteLoginRepository, IRepository<SecurityKeeper, Guid> securityKeeperRepository, IRepository<User, Guid> userRepository)
        {
            _repository = repository;
            _invitationRepository = invitationRepository;
            _inviteLoginRepository = inviteLoginRepository;
            _securityKeeperRepository = securityKeeperRepository;
            _userRepository = userRepository;
        }

        public async Task AddInvitationLoginAsync(InviteeLogin inviteeLogin)
        {
            if(!await _invitationRepository.AnyAsync(e => e.Id == inviteeLogin.RelatedInvitationId))
            {
                throw new Exception($"The invitation with id {inviteeLogin.RelatedInvitationId} is not found");
            }
            if (!await _securityKeeperRepository.AnyAsync(e => e.Id == inviteeLogin.HandlerSecurityKeeperId))
            {
                throw new Exception($"The security keeper with id {inviteeLogin.HandlerSecurityKeeperId} is not found");
            }
            await _inviteLoginRepository.InsertAsync(inviteeLogin);
        }

        public async Task<(bool isValid, (string propName, string errorMessage) error)> Register(SecurityKeeper entity)
        {
            var user = await _userRepository
                .Where(e => e.Username == entity.User.Username)
                .FirstOrDefaultAsync();
            if (user != null)
            {
                return (false, (nameof(User.Username), "Username is already exists"));
            }
            await _repository.InsertAsync(entity);
            return (true,default);
        }

        public async Task<(bool isValid, TViewModel InvitationDetails)> ValideQrCodeAsync<TViewModel>(string code) where TViewModel : BaseViewModel
        {
            if(!await _invitationRepository.AnyAsync(e=>e.QRCode==code))
            return (false,null);
            var detailsModel = _invitationRepository.Where(e => e.QRCode == code)
                .ProjectTo<TViewModel>(AutoMapperConfiguration.MapperConfiguration)
                .FirstOrDefault();
            return (true, detailsModel);
        }
    }
}
