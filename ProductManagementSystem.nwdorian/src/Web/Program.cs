using System.Globalization;
using Application;
using Infrastructure;
using Serilog;
using Web.Extensions;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console(formatProvider: CultureInfo.CurrentCulture)
    .CreateBootstrapLogger();

try
{
    Log.Information("Starting application...");

    WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

    builder.Services.AddPresentationServices(builder.Configuration);
    builder.Services.AddInfrastructureServices(builder.Configuration);
    builder.Services.AddApplicationServices();

    WebApplication app = builder.Build();

    await app.ApplyMigrations();
    await app.SeedDatabase();

    app.UseWebApplicationMiddleware();

    await app.RunAsync();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    await Log.CloseAndFlushAsync();
}
