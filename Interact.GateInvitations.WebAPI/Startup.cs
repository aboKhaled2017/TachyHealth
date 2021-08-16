using AutoMapper;
using Interact.GateInvitations.Core.Infrastructure;
using Interact.GateInvitations.DAL.Infrastructure;
using Interact.GateInvitations.WebAPI.Filters;
using Interact.GateInvitations.WebAPI.Helpers;
using Interact.GateInvitations.WebAPI.Infrastructure.Extensions;
using Interact.GateInvitations.WebAPI.Infrastructure.Mapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Interact.GateInvitations.WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public IConfiguration Configuration { get; }
        public IServiceProvider ServiceProvider { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Startup));
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(typeof(APIsMappingConfigurationProfile));
            });
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddCoreInfrastructure(mapperConfig);            
            services.AddDALInfrastructure(Configuration);

            services.AddGateInvitationAuthentication(Configuration);
            services.AddGateInvitationAuthorization();


            services.AddControllers(options =>
                       options.Filters.Add(new HttpResponseExceptionFilter()));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCustomErrors(env);
            app.UseCors(conf =>
            {
                conf.AllowAnyMethod();
                conf.AllowAnyHeader();
                conf.AllowAnyOrigin();
            });
            app.UseRouting();
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
