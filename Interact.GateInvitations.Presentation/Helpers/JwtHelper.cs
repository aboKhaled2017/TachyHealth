using Interact.GateInvitations.Common;
using Interact.GateInvitations.Common.Constants;
using Interact.GateInvitations.Common.Enums;
using Interact.GateInvitations.Core.Data;
using Interact.GateInvitations.Core.Infrastructure;
using Interact.GateInvitations.Core.Repositories;
using Interact.GateInvitations.Core.Services;
using Interact.GateInvitations.WebAPI.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Interact.GateInvitations.WebAPI.Helpers
{
    public class JwtHelper
    {
        private static readonly IConfigurationSection _JWT = AppServiceProvider.GetService<IConfiguration>().GetSection("JWT");
        public static JwtResponseModel CreateAccessToken(User user, List<(string type, string value)> otherClaimsToInclude = null)
        {
            var claims = new List<Claim>
            {
                new Claim(GateAppConfig.UserClaims.UserID,user.Id.ToString()),
                new Claim(ClaimTypes.Role,user.UserType.GetRole()),
                new Claim(GateAppConfig.UserClaims.UserNameClaim,user.Username),
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.DateOfBirth,DateTime.UtcNow.ToString())
            };
            if (user.UserType == UserType.Customer)
            {
                var customerRepo = AppServiceProvider.GetService<IRepository<Customer, Guid>>();
                var customer = customerRepo.GetAsync(user.Id).ConfigureAwait(true).GetAwaiter().GetResult();
                claims.Add(new Claim(GateAppConfig.UserClaims.IsActive, customer.UserStatus.GetUserStatus()));
            }
            if (otherClaimsToInclude != null && otherClaimsToInclude.Count > 0)
            {
                otherClaimsToInclude.ForEach(c =>
                {
                    claims.Add(new Claim(c.type, c.value));
                });
            }

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_JWT.GetValue<string>("signingKey")));
            var signingCredentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            //addClaims(claims);
            var jwt = CreateSecurityToken(claims, DateTime.UtcNow.AddHours(1), signingCredentials);
            var token = new JwtSecurityTokenHandler().WriteToken(jwt);
            return  new JwtResponseModel { Token = token, Expiry = DateTime.UtcNow.Ticks };
        }
        private static JwtSecurityToken CreateSecurityToken(IEnumerable<Claim> claims, DateTime expiry, SigningCredentials signingCredentials)
             => new JwtSecurityToken(
                 issuer: _JWT.GetValue<string>("issuer"),
                 expires: expiry,
                 audience: _JWT.GetValue<string>("audience"),
                 claims: claims,
                 signingCredentials: signingCredentials
                 );

    }
}
