using Interact.GateInvitations.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Interact.GateInvitations.Common
{
    public static class CommonExtensions
    {
        public static string GetRole(this UserType userType)
        {
            switch (userType)
            {
                case UserType.Customer:
                    return UserRoles.CustomerRole;
                case UserType.SecurityKeeper:
                    return UserRoles.SecurityKeeperRole;
                case UserType.Admin:
                    return UserRoles.AdminRole;
                default:
                    return null;
            }
        }
        public static string GetUserStatus(this UserStatus status)
        {
            switch (status)
            {
                case UserStatus.InActive:
                    return "InActive";
                case UserStatus.Active:
                    return "Activated";
                default:
                    return null;
            }
        }
        public static T GetLoggedInUserId<T>(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            var loggedInUserId = principal.FindFirst(ClaimTypes.NameIdentifier);

            if (typeof(T) == typeof(string))
            {
                return (T)Convert.ChangeType(loggedInUserId.Value, typeof(T));
            }
            else if (typeof(T) == typeof(int) || typeof(T) == typeof(long) )
            {
                return loggedInUserId != null 
                    ? (T)Convert.ChangeType(loggedInUserId.Value, typeof(T)) 
                    : (T)Convert.ChangeType(0, typeof(T));
            }
            else if (typeof(T) == typeof(Guid))
            {
                return loggedInUserId != null
                   ? (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFromInvariantString(loggedInUserId.Value)
                   : (T)Convert.ChangeType(Guid.Empty, typeof(T));
            }
            else
            {
                throw new Exception("Invalid type provided");
            }
        }
    }
}
