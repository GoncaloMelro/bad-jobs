using badjobs.Jobs;
using badjobs.Metrics;
using Hangfire;
using OpenTelemetry.Metrics;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddConfiguredHangfire(builder.Configuration);
builder.Services.AddOpenTelemetry()
    .WithMetrics(m =>
    m.AddMeter("badjobs.metrics")
     .AddPrometheusExporter()
    );

var app = builder.Build();

app.UseHttpsRedirection();
app.UseConfiguredHangfire();

app.MapGet("/foo", () =>
{
    CustomMetrics.AddJobCount("DemoJob");
    BackgroundJob.Enqueue<DemoJobs>(s =>
        s.Run(null!, CancellationToken.None));

    return Results.Ok("200");
});

app.UseOpenTelemetryPrometheusScrapingEndpoint();

app.Run();
