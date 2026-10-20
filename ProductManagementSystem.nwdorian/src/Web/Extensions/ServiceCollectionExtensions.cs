using Application.Emails;
using Infrastructure.Authorization;
using Infrastructure.Database;
using Infrastructure.Emails;
using Infrastructure.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Serilog;

namespace Web.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddPresentationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddIdentityServices();
        services.AddAuthorizationPolicies();
        services.AddSerilogServices(configuration);
        services.AddEmailServices();
        services.AddControllersWithViews();

        services.ConfigureIdentityOptions();
        services.ConfigureCookieOptions();
        services.ConfigureTokenOptions();
    }

    private static void AddIdentityServices(this IServiceCollection services)
    {
        services.AddIdentity<User, Role>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
    }

    private static void AddAuthorizationPolicies(this IServiceCollection services)
    {
        services
            .AddAuthorizationBuilder()
            .AddPolicy(Policies.RequireAdmin, policy => policy.RequireRole(Roles.Admin))
            .AddPolicy(Policies.RequireStaff, policy => policy.RequireRole(Roles.Staff, Roles.Admin));
    }

    private static void AddSerilogServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSerilog((services, lc) => lc.ReadFrom.Configuration(configuration));
    }

    private static void AddEmailServices(this IServiceCollection services)
    {
        services
            .AddOptions<SmtpSettings>()
            .BindConfiguration(SmtpSettings.SectionName)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddScoped<IEmailService>(sp =>
        {
            SmtpSettings settings = sp.GetRequiredService<IOptions<SmtpSettings>>().Value;
            return new EmailService(settings);
        });
    }

    private static void ConfigureIdentityOptions(this IServiceCollection services)
    {
        services.Configure<IdentityOptions>(options =>
        {
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireUppercase = true;
            options.Password.RequiredLength = 6;
            options.Password.RequiredUniqueChars = 1;

            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.AllowedForNewUsers = true;

            options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
            options.User.RequireUniqueEmail = true;
            options.SignIn.RequireConfirmedEmail = true;
        });
    }

    private static void ConfigureCookieOptions(this IServiceCollection services)
    {
        services.ConfigureApplicationCookie(options =>
        {
            options.Cookie.HttpOnly = true;
            options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

            options.LoginPath = "/Account/Login";
            options.AccessDeniedPath = "/Account/AccessDenied";
            options.SlidingExpiration = true;
        });
    }

    private static void ConfigureTokenOptions(this IServiceCollection services)
    {
        services.Configure<DataProtectionTokenProviderOptions>(options =>
            options.TokenLifespan = TimeSpan.FromHours(2)
        );
    }
}
