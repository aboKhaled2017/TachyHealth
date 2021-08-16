using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interact.GateInvitations.Core.Infrastructure
{
    public static class AppServiceProvider
    {
        private static IServiceProvider _serviceProvider { get; set; }
        private static IServiceScope _serviceScope { get; set; }
        public static void Init(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _serviceScope = _serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
        }
        public static IService GetService<IService>()
        {
            return _serviceScope.ServiceProvider.GetService<IService>();
        }
        public static IService GetRequiredService<IService>()
        {
            return _serviceScope.ServiceProvider.GetRequiredService<IService>();
        }
    }
}
