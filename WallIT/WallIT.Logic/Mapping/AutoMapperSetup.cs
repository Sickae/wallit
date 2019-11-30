using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace WallIT.Logic.Mapping
{
    public static class AutoMapperSetup
    {
        public static void Init(IServiceCollection services)
        {
            services.AddAutoMapper(cfg => SetupConfiguration(cfg), GetProfileAssemblies());
        }

        private static void SetupConfiguration(IMapperConfigurationExpression cfg)
        {
            cfg.AllowNullCollections = true;
        }

        private static Assembly[] GetProfileAssemblies()
        {
            var assemblies = new[]
            {
                Assembly.GetAssembly(typeof(AutoMapperBaseProfile))
            };

            return assemblies;
        }
    }
}
