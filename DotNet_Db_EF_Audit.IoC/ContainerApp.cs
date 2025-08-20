using DotNet_Db_EF_Audit.IoC.Security;
using DotNet_Db_EF_Audit.IoC.Swagger;
using Microsoft.AspNetCore.Builder;

namespace DotNet_Db_EF_Audit.IoC
{
    public static class ContainerApp
    {
        public static IApplicationBuilder RegisterContainerApp(this IApplicationBuilder app)
        {
            app.UseSwaggerDocApi();
            app.AddSecurityHeaders();
            app.UseHttpsRedirection();
            app.UseAuthorization();

            return app;
        }
    }
}
