using Asp.Versioning;
using Microsoft.Extensions.DependencyInjection;

namespace DotNet_Db_EF_Audit.IoC.Swagger
{
    public static class ApiVersionInjection
    {
        public static void AddVersion(this IServiceCollection services)
        {
            services.AddApiVersioning(options =>
            {
                options.ApiVersionReader = new MediaTypeApiVersionReader("version");
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
            })
             .AddApiExplorer(options =>
             {
                 options.GroupNameFormat = "'v'VVV";
                 options.SubstituteApiVersionInUrl = true;
             });
        }
    }
}
