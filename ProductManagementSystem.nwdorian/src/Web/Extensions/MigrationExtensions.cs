using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Web.Extensions;

public static class MigrationExtensions
{
    public static async Task ApplyMigrations(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();
        ApplicationDbContext dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        await dbContext.Database.MigrateAsync();
    }

    public static async Task SeedDatabase(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();
        DataSeeder dataSeeder = scope.ServiceProvider.GetRequiredService<DataSeeder>();

        await dataSeeder.SeedAsync();
    }
}
