﻿using AutoMapper;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using WallIT.Logic.Identity;
using WallIT.Logic.Mapping;
using WallIT.Logic.Mediator.Handlers.QueryHandlers;
using WallIT.Logic.Services;
using WallIT.Web.Infrastructure;
using WallIT.Web.SharedResource;

namespace WallIT.Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(opt =>
            {
                opt.CheckConsentNeeded = context => true;
                opt.MinimumSameSitePolicy = SameSiteMode.None;
            });

            IoC.Setup(services, Configuration);

            services.Configure<IdentityOptions>(opt =>
            {
                opt.Password.RequiredLength = 5;
                opt.Password.RequireDigit = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireNonAlphanumeric = false;

                opt.Lockout.AllowedForNewUsers = true;
                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromHours(1);
                opt.Lockout.MaxFailedAccessAttempts = 5;
            });

            services.AddIdentityCore<AppIdentityUser>()
                .AddDefaultTokenProviders()
                .AddUserStore<AppIdentityUserStore>()
                .AddUserManager<AppIdentityUserManager>()
                .AddSignInManager<AppSignInManager>()
                .AddErrorDescriber<AppIdentityErrorDescriber>();

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;
                opt.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;
                opt.DefaultSignInScheme = IdentityConstants.ApplicationScheme;
            }).AddIdentityCookies();

            services.ConfigureApplicationCookie(opt =>
            {
                opt.Cookie.HttpOnly = true;
                opt.ExpireTimeSpan = TimeSpan.FromDays(365);
                opt.LoginPath = "/Account/Login";
                opt.LogoutPath = "/Account/Logout";
                opt.AccessDeniedPath = "/Home/AccessDenied";
                opt.ReturnUrlParameter = "returnUrl";
            });

            services.Configure<DataProtectionTokenProviderOptions>(opt =>
            {
                opt.TokenLifespan = TimeSpan.FromHours(3);
            });

            services.AddDistributedMemoryCache();

            services.AddSession();

            services.AddLocalization(opt =>
            {
                opt.ResourcesPath = "Resources";
            });

            services.AddMvc(opt => opt.EnableEndpointRouting = false)
                .AddFluentValidation()
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization(opt =>
                {
                    opt.DataAnnotationLocalizerProvider = (type, factory) =>
                    {
                        var assemblyName = new AssemblyName(typeof(SharedResourceDummy).GetTypeInfo().Assembly.FullName);
                        return factory.Create("SharedResource", assemblyName.Name);
                    };
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.Configure<RequestLocalizationOptions>(opt =>
            {
                var supportedCultures = new List<CultureInfo>
                {
                    new CultureInfo("en-US"),
                    new CultureInfo("hu-HU")
                };
                opt.DefaultRequestCulture = new RequestCulture("en-US");
                opt.SupportedCultures = supportedCultures;
                opt.SupportedUICultures = supportedCultures;
            });

            services.AddMediatR(typeof(GetUserByIdQueryHandler)); // handlers are stored in the WallIT.Logic assembly

            services.AddAutoMapper(cfg => SetupAutoMapperConfiguration(cfg), GetAutoMapperProfileAssemblies());
            services.AddScoped<DeleteAccountService>();
            services.AddScoped<EditAccountService>();
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseCookiePolicy(new CookiePolicyOptions
            {
                MinimumSameSitePolicy = SameSiteMode.Strict
            });
            app.UseSession();
            app.UseAuthentication();

            var options = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(options.Value);

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private static void SetupAutoMapperConfiguration(IMapperConfigurationExpression cfg)
        {
            cfg.AllowNullCollections = true;
        }

        private static Assembly[] GetAutoMapperProfileAssemblies()
        {
            var assemblies = new[]
            {
                Assembly.GetAssembly(typeof(AutoMapperBaseProfile))
            };

            return assemblies;
        }
    }
}
