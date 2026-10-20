using Serilog;

namespace Web.Extensions;

public static class WebApplicationExtensions
{
    public static void UseWebApplicationMiddleware(this WebApplication app)
    {
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseCustomSerilogRequestLogging();

        app.UseHttpsRedirection();
        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapStaticAssets();

        app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}").WithStaticAssets();
    }

    private static void UseCustomSerilogRequestLogging(this WebApplication app)
    {
        app.UseSerilogRequestLogging(opts =>
        {
            opts.GetLevel = (httpContext, elapsed, ex) =>
            {
                PathString path = httpContext.Request.Path;

                if (
                    path.StartsWithSegments("/css")
                    || path.StartsWithSegments("/js")
                    || path.StartsWithSegments("/lib")
                    || path.StartsWithSegments("/favicon.ico")
                )
                {
                    return Serilog.Events.LogEventLevel.Debug;
                }

                return Serilog.Events.LogEventLevel.Information;
            };

            opts.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
            {
                diagnosticContext.Set("CorrelationId", httpContext.TraceIdentifier);
                diagnosticContext.Set("RequestMethod", httpContext.Request.Method);
                diagnosticContext.Set("RequestPath", httpContext.Request.Path);
                diagnosticContext.Set("Endpoint", httpContext.GetEndpoint()?.DisplayName);
            };

            opts.MessageTemplate = "Handled {RequestMethod} {RequestPath} in {Elapsed:0.0000} ms";
        });
    }
}
