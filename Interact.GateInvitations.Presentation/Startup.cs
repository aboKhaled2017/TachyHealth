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
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Interact.GateInvitations.Presentation
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

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

            services.AddControllersWithViews(options=> {
                options.Filters.Add(new HttpResponseExceptionFilter());
            });
            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
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
            app.UseDefaultFiles();
            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501
              
                spa.Options.SourcePath = "ClientApp";
              
                if (env.IsDevelopment())
                {
                    //spa.UseProxyToSpaDevelopmentServer("http://localhost:4200");
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
