using DotNet_Db_EF_Audit.Infra.Db;
using DotNet_Db_EF_Audit.Infra.Db.Interceptor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DotNet_Db_EF_Audit.IoC.Db
{
    public static class SqlServer
    {
        public static void AddSqlServer(this IServiceCollection services, IConfigurationManager configuration)
        {
            var connectionString = configuration.GetConnectionString("SqlServer");

            services.AddDbContext<ApplicationDbContext>((provider, options) =>
            {
                var interceptor = provider.GetRequiredService<AuditableInterceptor>();

                options
                .EnableSensitiveDataLogging()
                .UseSqlServer(connectionString, sqlOptions =>
                {
                    sqlOptions.MigrationsHistoryTable("__MyMigrationsHistory", "api");
                })
                .AddInterceptors(interceptor);
            });
        }
    }
}
