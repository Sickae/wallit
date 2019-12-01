using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NHibernate;
using WallIT.DataAccess.SessionBuilder;
using WallIT.Logic.Interfaces.Managers;
using WallIT.Logic.Interfaces.Repositories;
using WallIT.Logic.Managers;
using WallIT.Logic.Repositories;
using WallIT.Logic.UnitOfWork;
using WallIT.Shared.Interfaces.UnitOfWork;

namespace WallIT.Web.Infrastructure
{
    public static class IoC
    {
        public static void Setup(IServiceCollection services, IConfiguration configuration)
        {
            SetupSingletons(services, configuration);
            SetupScoped(services);
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

            // Managers
            services.AddScoped<ICreditCardManager, CreditCardManager>();

            // Repositories
            services.AddScoped<ICreditCardRepository, CreditCardRepository>();
        }
    }
}
