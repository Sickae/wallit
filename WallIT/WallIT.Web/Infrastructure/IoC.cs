using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NHibernate;
using WallIT.DataAccess.SessionBuilder;
using WallIT.Logic.Identity;
using WallIT.Logic.Interfaces.Managers;
using WallIT.Logic.Interfaces.Repositories;
using WallIT.Logic.Managers;
using WallIT.Logic.Repositories;
using WallIT.Logic.UnitOfWork;
using WallIT.Shared.DTOs;
using WallIT.Shared.Interfaces.UnitOfWork;
using WallIT.Web.Models;
using WallIT.Web.Validators;

namespace WallIT.Web.Infrastructure
{
    public static class IoC
    {
        public static void Setup(IServiceCollection services, IConfiguration configuration)
        {
            SetupSingletons(services, configuration);
            SetupScoped(services);
            SetupTransient(services);
        }

        private static void SetupSingletons(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(SessionFactory.BuildConfiguration(configuration.GetConnectionString("wallit"))
                .BuildSessionFactory());
        }

        private static void SetupScoped(IServiceCollection services)
        {
            services.AddScoped(x => x.GetService<ISessionFactory>().OpenSession());
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Identity
            services.AddScoped<AppIdentityUserManager, AppIdentityUserManager>();
            services.AddScoped<AppSignInManager, AppSignInManager>();
            services.AddScoped<AppIdentityErrorDescriber, AppIdentityErrorDescriber>();

            // Managers
            services.AddScoped<ICreditCardManager, CreditCardManager>();
            services.AddScoped<IUserManager, UserManager>();
            services.AddScoped<IUserClaimManager, UserClaimManager>();
            services.AddScoped<IRecordManager, RecordManager>();
            services.AddScoped<IAccountManager, AccountManager>();
            services.AddScoped<IRecordCategoryManager, RecordCategoryManager>();
            services.AddScoped<IRecordTemplateManager, RecordTemplateManager>();
            // Repositories
            services.AddScoped<ICreditCardRepository, CreditCardRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserClaimRepository, UserClaimRepository>();
            services.AddScoped<IRecordRepository, RecordRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IRecordCategoryRepository, RecordCategoryRepository>();
            services.AddScoped<IRecordTemplateRepository, RecordTemplateRepository>();
        }

        private static void SetupTransient(IServiceCollection services)
        {
            // Validators
            services.AddTransient<IValidator<UserDTO>, UserDTOValidator>();
            services.AddTransient<IValidator<LoginModel>, LoginModelValidator>();
            services.AddTransient<IValidator<RegisterModel>, RegisterModelValidator>();
        }
    }
}
