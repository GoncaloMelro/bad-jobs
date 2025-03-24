using Hangfire;
using Hangfire.Dashboard;
using Hangfire.PostgreSql;

public static class HangfireExtensions
{
    public static IServiceCollection AddConfiguredHangfire(this IServiceCollection services, IConfiguration configuration)
    {
        string conn = Environment.GetEnvironmentVariable("HANGFIRE_CONNECTION_STRING")
                      ?? configuration.GetConnectionString("hangfire")!;

        services.AddHangfire(config => config
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UsePostgreSqlStorage(options =>
            {
                options.UseNpgsqlConnection(conn);
            }, new PostgreSqlStorageOptions
            {
                SchemaName = "hangfire",
                PrepareSchemaIfNecessary = true
            }));

        services.AddHangfireServer();

        return services;
    }

    public static WebApplication UseConfiguredHangfire(this WebApplication app)
    {
        app.UseHangfireDashboard("/hangfire", new DashboardOptions
        {
            AppPath = "/",
            DashboardTitle = "bad jobs",
            DisplayStorageConnectionString = false,
            IsReadOnlyFunc = context => false,
            Authorization = Array.Empty<IDashboardAuthorizationFilter>()
        });

        return app;
    }
}
