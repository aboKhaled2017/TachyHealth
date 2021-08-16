using Interact.GateInvitations.Common.Constants;
using Interact.GateInvitations.Common.Enums;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interact.GateInvitations.WebAPI.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddGateInvitationAuthentication(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
           
           .AddJwtBearer(options =>
           {
               var JWTSection = configuration.GetSection("JWT");
               options.SaveToken = true;
               options.RequireHttpsMetadata = false;//disabled only in developement
               options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
               {
                   ValidateIssuer = true,
                   ValidateAudience = true,
                   ValidIssuer = JWTSection.GetValue<string>("issuer"),
                   ValidAudience = JWTSection.GetValue<string>("audience"),
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWTSection.GetValue<string>("signingKey")))
               };
           });
            return services;
        }
        public static IServiceCollection AddGateInvitationAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(opts =>
            {
                opts.AddPolicy(GateAppConfig.CustomerPolicy, policy => {
                    policy.RequireRole(UserRoles.CustomerRole)
                           .RequireAuthenticatedUser();
                });
                opts.AddPolicy(GateAppConfig.SecurityKeeperPolicy, policy => {
                    policy.RequireRole(UserRoles.SecurityKeeperRole)
                           .RequireAuthenticatedUser();
                });
                opts.AddPolicy(GateAppConfig.AdminPolicy, policy => {
                    policy.RequireRole(UserRoles.AdminRole)
                           .RequireAuthenticatedUser();
                });
            });
            return services;
        }
    }
}
