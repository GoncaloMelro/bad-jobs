using badjobs.Jobs;
using Hangfire;
using OpenTelemetry.Metrics;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddConfiguredHangfire(builder.Configuration);
builder.Services.AddOpenTelemetry()
    .WithMetrics(m => m.AddPrometheusExporter());

var app = builder.Build();

app.UseHttpsRedirection();
app.UseConfiguredHangfire();

app.MapGet("/foo", (int limit) =>
{
    BackgroundJob.Enqueue<DemoJobs>(s =>
        s.Run(CancellationToken.None));

    return Results.NoContent();
});


app.UseOpenTelemetryPrometheusScrapingEndpoint();

app.Run();
