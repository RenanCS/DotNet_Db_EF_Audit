using DotNet_Db_EF_Audit.Domain.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace DotNet_Db_EF_Audit.IoC.Jwt
{
    public static class JwtService
    {
        public static void AddJwt(this IServiceCollection services, IConfigurationManager configuration)
        {
            var authConfiguration = configuration.GetSection("AuthConfiguration").Get<AuthConfiguration>();
            services.AddSingleton(authConfiguration);

            services.AddOptions<AuthConfiguration>()
                    .Bind(configuration.GetSection(nameof(AuthConfiguration)));

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = authConfiguration.Issuer,
                    ValidAudience = authConfiguration.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authConfiguration.Key!))
                };
            });
        }
    }
}
