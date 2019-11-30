using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NHibernate;
using WallIT.DataAccess.SessionBuilder;

namespace WallIT.Web.Infrastructure
{
    public static class IoC
    {
        public static void Setup(IServiceCollection services, IConfiguration configuration)
        {
            #region Singletons

            services.AddSingleton(SessionFactory.BuildConfiguration(configuration.GetConnectionString("wallit"))
                .BuildSessionFactory());

            #endregion Singletons

            #region Scoped

            services.AddScoped(x => x.GetService<ISessionFactory>().OpenSession());

            #endregion Scoped
        }
    }
}
