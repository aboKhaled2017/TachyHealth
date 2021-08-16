using AutoMapper;
using AutoMapper.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interact.GateInvitations.Core.Infrastructure
{
    public static class ServicesCollections
    {
        public static IServiceCollection AddCoreInfrastructure(this IServiceCollection services, MapperConfiguration mapperConfig)
        {
            AutoMapperConfiguration.Init(mapperConfig); 
            return services;
        }
    }
}
