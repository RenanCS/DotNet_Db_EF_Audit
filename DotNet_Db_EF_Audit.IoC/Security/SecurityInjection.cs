using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;

namespace DotNet_Db_EF_Audit.IoC.Security
{
    public static class SecurityInjection
    {
        public static void AddSecurityHeaders(this IApplicationBuilder app)
        {
            app.UseCsp(options => options
                .DefaultSources(s => s.Self()
                    .CustomSources("data:")
                    .CustomSources("https:"))
                .StyleSources(s => s.Self()
                    .UnsafeInline())
                .ScriptSources(s => s.Self()
                    .UnsafeInline()
                    .UnsafeEval())
                .FontSources(s => s.Self()
                    .CustomSources("data:")
                    .CustomSources("https:"))
                .ImageSources(s => s.Self()
                    .CustomSources("data:"))
                );

            app.UseXfo(options => options.Deny());

            app.Use(async (context, next) =>
            {
                context.Response.Headers.Append("X-Xss-Protection", new StringValues("1; mode=block"));
                context.Response.Headers.Append("X-Permitted-Cross-Domain-Policies", new StringValues("none"));
                context.Response.Headers.Append("X-Content-Type-Options", "nosniff");
                context.Response.Headers[HeaderNames.CacheControl] = "no-cache, no-store, must-revalidate";
                context.Response.Headers[HeaderNames.Expires] = "0";
                context.Response.Headers[HeaderNames.Pragma] = "no-cache";

                await next.Invoke();
            });

        }
    }
}
