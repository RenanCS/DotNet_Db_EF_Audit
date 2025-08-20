using Asp.Versioning.ApiExplorer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Interfaces;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;

namespace DotNet_Db_EF_Audit.IoC.Swagger
{
    public static class SwaggerInjection
    {
        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerExamplesFromAssemblies(Assembly.GetEntryAssembly());
            services.AddSwaggerGen(options =>
            {
                options.EnableAnnotations();
                options.ExampleFilters();
                options.SwaggerDoc("v1",
                    new Microsoft.OpenApi.Models.OpenApiInfo
                    {
                        Title = "API Cid",
                        Version = "v1",
                        Description = "Projeto auxiliar na consulta do CID10 e CID11",
                        Contact = new Microsoft.OpenApi.Models.OpenApiContact()
                        {
                            Name = "Renan Carvalho de Souza",
                            Email = "renancarvahodesouza@gmail.com@gmail.com",
                        },
                        Extensions = new Dictionary<string, IOpenApiExtension>
                        {
                            {"x-logo", new OpenApiObject
                                {
                                {"url", new OpenApiString("https://e7.pngegg.com/pngimages/259/971/png-clipart-electrician-illustration-electrical-safety-electricity-electrical-engineering-electrician-animation-miscellaneous-electrical-contractor.png")},
                                { "altText", new OpenApiString("Eqtl")}
                                }
                            }
                        }
                    });

                //string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                //string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                //options.IncludeXmlComments(xmlPath);
            });

            services.AddSwaggerExamplesFromAssemblies(Assembly.GetEntryAssembly());
        }

        public static void UseSwaggerDocApi(this IApplicationBuilder app)
        {
            var descriptionProvider = app.ApplicationServices.GetRequiredService<IApiVersionDescriptionProvider>();

            app.UseSwagger(options => { options.RouteTemplate = "swagger/{documentName}/swagger.json"; });
            app.UseSwaggerUI(options =>
            {
                options.RoutePrefix = "";
                foreach (var description in descriptionProvider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                }
            });

            foreach (var description in descriptionProvider.ApiVersionDescriptions)
            {
                app.UseReDoc(options =>
                {
                    options.DocumentTitle = $"API Documentation {description.GroupName}";
                    options.SpecUrl = $"/swagger/{description.GroupName}/swagger.json";
                    options.RoutePrefix = $"doc-{description.GroupName}";
                });
            }
        }

    }
}
