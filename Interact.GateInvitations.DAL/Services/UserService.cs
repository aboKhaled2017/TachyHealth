using Interact.GateInvitations.Common.Enums;
using Interact.GateInvitations.Core.Data;
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
    public class UserService : IUserService
    {
        private readonly IRepository<User, Guid> _usersRepository;
        public UserService(IRepository<User, Guid> usersRepository)
        {
            _usersRepository = usersRepository;
        }
        public async Task<(bool, User)> CheckForUserCredintialsIsValidAsync(string username, string password, UserType userType)
        {
            var user =await _usersRepository.Where(u => u.Username == username && u.Password == password && u.UserType==userType)
                .SingleOrDefaultAsync();
            return (user is null)
                ? (false, null)
                : (true, user);
        }
    }
}
