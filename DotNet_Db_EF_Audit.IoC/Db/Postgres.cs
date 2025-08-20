using DotNet_Db_EF_Audit.Infra.Db;
using DotNet_Db_EF_Audit.Infra.Db.Interceptor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace DotNet_Db_EF_Audit.IoC.Db
{
    public static class Postgres
    {
        public static void AddPostgres(this IServiceCollection services, IConfigurationManager configuration)
        {
            var connectionString = configuration.GetConnectionString("Postgres");
            var dataSourceBuilder = new NpgsqlDataSourceBuilder(connectionString);
            dataSourceBuilder.EnableDynamicJson();

            services.AddDbContext<ApplicationDbContext>((provider, options) =>
            {
                var interceptor = provider.GetRequiredService<AuditableInterceptor>();

                options
                .EnableSensitiveDataLogging()
                .UseNpgsql(dataSourceBuilder.Build(), npgsqlOptions =>
                {
                    npgsqlOptions.MigrationsHistoryTable("__MyMigrationsHistory", "api");
                })
                .AddInterceptors(interceptor)
                .UseSnakeCaseNamingConvention();
            });
        }
    }
}
