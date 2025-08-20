
using DotNet_Db_EF_Audit.Infra.Db;
using DotNet_Db_EF_Audit.Infra.Db.Faker;
using DotNet_Db_EF_Audit.IoC;

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Services.RegisterContainerService(builder.Configuration);

    var app = builder.Build();

    app.RegisterContainerApp();

    app.MapControllers();

    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await DatabaseSeedService.SeedAsync(dbContext);
    }

    await app.RunAsync();
}
catch (Exception ex)
{
    Console.WriteLine(ex.ToString());
}


