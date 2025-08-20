using DotNet_Db_EF_Audit.Infra.Db.Interceptor;
using DotNet_Db_EF_Audit.IoC.Db;
using DotNet_Db_EF_Audit.IoC.Jwt;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DotNet_Db_EF_Audit.IoC.Module
{
    public static class InfraModule
    {
        public static IServiceCollection AddInfra(this IServiceCollection services, IConfigurationManager configuration)
        {
            services.AddHttpClient();
            services.AddHttpContextAccessor();
            services.AddSingleton<AuditableInterceptor>();
            services.AddJwt(configuration);

            // ------ TO POSTEGRES ----
            //services.AddPostgres(configuration);

            // ------ TO SQL SERVER ----
            //services.AddSqlServer(configuration);

            // ------ TO SQL SERVER ----
            services.AddMySql(configuration);

            return services;
        }
    }
}
