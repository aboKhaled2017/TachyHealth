using Interact.GateInvitations.Common.Constants;
using Interact.GateInvitations.Core.Data;
using Interact.GateInvitations.Core.Infrastructure;
using Interact.GateInvitations.Core.Repositories;
using Interact.GateInvitations.Core.Services;
using Interact.GateInvitations.DAL.Repositories;
using Interact.GateInvitations.DAL.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Interact.GateInvitations.DAL.Infrastructure
{
    public static class ServicesCollections
    {
        public static IServiceCollection AddDALInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString(GateAppConfig.SqlServerDatabaseName), config =>
                {
                    config.MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name);
                });
            });
            services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
            RegisterDALServices(services);
            var serviceProvider = services.BuildServiceProvider();
            AppServiceProvider.Init(serviceProvider);
            CreateDefaultAdmin();
            return services;
        }
        private static void RegisterDALServices(IServiceCollection services)
        {
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IAdminService, AdminService>(); 
            services.AddScoped<IInvitationService, InvitationService>();
            services.AddScoped<ISecurityKeeperService, SecurityKeeperService>();
            services.AddScoped<IUserService, UserService>();
        }
        private static void CreateDefaultAdmin()
        {
            var adminService = AppServiceProvider.GetService<IAdminService>();
            adminService.AddNewAdminAsync("faddo", "12345","Faddo").ConfigureAwait(true);
        }
    }
}
