using DotNet_Db_EF_Audit.Application.Provider;
using DotNet_Db_EF_Audit.Application.Service;
using DotNet_Db_EF_Audit.Domain.Interface.Provider;
using DotNet_Db_EF_Audit.Domain.Interface.Repositories;
using DotNet_Db_EF_Audit.Domain.Interface.Service;
using DotNet_Db_EF_Audit.Infra.Db.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace DotNet_Db_EF_Audit.IoC.Module
{
    public static class ApplicationModule
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IAuthorService, AuthorService>();
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthorizationLocalService, AuthorizationLocalService>();

            services.AddSingleton<ICurrentSessionProvider, CurrentSessionProvider>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IAuthorRepository, AuthorRepository>();

            return services;
        }
    }
}
