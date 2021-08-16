using AutoMapper;
using Interact.GateInvitations.Common.Constants;
using Interact.GateInvitations.Common.Enums;
using Interact.GateInvitations.Core.Infrastructure;
using Interact.GateInvitations.DAL.Infrastructure;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Interact.GateInvitations.AdminPanel
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
                //cfg.AddProfile(typeof(APIsMappingConfigurationProfile));
            });
            services.AddCoreInfrastructure(mapperConfig);
            services.AddDALInfrastructure(Configuration);
            services.AddControllersWithViews()
                .AddRazorRuntimeCompilation();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(CookieBuilder => {
                CookieBuilder.LoginPath = "/auth/signin";
                CookieBuilder.AccessDeniedPath = "/auth/AccessDenied";
                CookieBuilder.LogoutPath = "/Auth/LogOut";
                CookieBuilder.Cookie.Name = "GateInvitationAdminCookie";
            });
            services.AddAuthorization(conf => {
                conf.AddPolicy(AdminConfig.AdminAuthorizationPolicy, policyConfig =>
                {
                    policyConfig.RequireAuthenticatedUser();
                    policyConfig.RequireRole(UserType.Admin.ToString());
                });
            });
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
