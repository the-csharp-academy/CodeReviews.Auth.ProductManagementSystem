using Application.Database;
using Application.Notifications;
using Application.Users;
using Domain.Core.Abstractions;
using Infrastructure.Authentication;
using Infrastructure.Database;
using Infrastructure.Database.Interceptors;
using Infrastructure.Notifications;
using Infrastructure.Time;
using Infrastructure.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDatabase(configuration);
        services.AddScoped<DataSeeder>();
        services.AddScoped<IUserClaimsPrincipalFactory<User>, CustomClaimsFactory>();
        services.AddScoped<IEmailNotificationService, EmailNotificationService>();
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.AddHttpContextAccessor();
        services.AddScoped<IUserContext, UserContext>();
    }

    private static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        string? connectionString =
            configuration.GetConnectionString("Default")
            ?? throw new InvalidOperationException("Connection string 'Default' not found.");

        services.AddScoped<SoftDeleteInterceptor>();
        services.AddScoped<UpdateAuditableInterceptor>();

        services.AddDbContext<ApplicationDbContext>(
            (sp, options) =>
            {
                options.UseSqlServer(connectionString).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

                options.AddInterceptors(sp.GetRequiredService<SoftDeleteInterceptor>());
                options.AddInterceptors(sp.GetRequiredService<UpdateAuditableInterceptor>());
            }
        );

        services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
    }
}
