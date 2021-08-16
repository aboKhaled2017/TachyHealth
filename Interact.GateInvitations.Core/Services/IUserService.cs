using Interact.GateInvitations.Common.Enums;
using Interact.GateInvitations.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interact.GateInvitations.Core.Services
{
    public interface IUserService
    {
        Task<(bool isValid,User validatedUser)> CheckForUserCredintialsIsValidAsync(string username,string password,UserType userType);
    }
}
