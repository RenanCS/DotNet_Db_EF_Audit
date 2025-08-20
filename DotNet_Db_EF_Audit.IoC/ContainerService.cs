using DotNet_Db_EF_Audit.IoC.Module;
using DotNet_Db_EF_Audit.IoC.Swagger;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DotNet_Db_EF_Audit.IoC
{
    public static class ContainerService
    {
        public static IServiceCollection RegisterContainerService(this IServiceCollection services, IConfigurationManager configuration)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddVersion();
            services.AddSwagger();

            services.AddApplication();
            services.AddInfra(configuration);

            return services;
        }
    }
}
