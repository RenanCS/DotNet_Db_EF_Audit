using DotNet_Db_EF_Audit.Infra.Db;
using DotNet_Db_EF_Audit.Infra.Db.Interceptor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace DotNet_Db_EF_Audit.IoC.Db
{
    public static class MySql
    {
        public static void AddMySql(this IServiceCollection services, IConfigurationManager configuration)
        {
            var connectionString = configuration.GetConnectionString("MySql");

            services.AddDbContext<ApplicationDbContext>((provider, options) =>
            {
                var interceptor = provider.GetRequiredService<AuditableInterceptor>();

                options
                .EnableSensitiveDataLogging()
                .UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 36)), mySqlOptions =>
                {
                    mySqlOptions.SchemaBehavior(MySqlSchemaBehavior.Ignore);
                    mySqlOptions.MigrationsHistoryTable("__MyMigrationsHistory", "api");
                })
                .AddInterceptors(interceptor);
            });
        }

    }
}
