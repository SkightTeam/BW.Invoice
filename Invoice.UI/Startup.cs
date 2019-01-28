﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Invoice.Integration;
using Invoice.Integration.Xero;
using Invoice.Integration.Xero.XeroAuthenticators;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xero.Api;

namespace Invoice.UI
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddOptions();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            //TODO: Suppose to use config like: services.Configure<IXeroApiSettings>(x => Configuration.GetSection("XeroApi"));
            //There is a bug in .net core, probably fixed in later version : https://github.com/aspnet/Docs/issues/867
            services.AddSingleton<IXeroApiSettings>(new XeroApiSettings());
            services.AddSingleton<AuthenticatorFacade, AuthenticatorFacadeImpl>();
            services.AddSingleton<PublicAuthenticator>();
            services.AddSingleton<PartnerAuthenticator>();
            services.AddSingleton<RequestTokenStore>(new MemoryTokenStore());
            services.AddSingleton<AccessTokenStore>(new MemoryTokenStore());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
