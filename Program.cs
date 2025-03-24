using badjobs.Jobs;
using Hangfire;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddConfiguredHangfire(builder.Configuration);

var app = builder.Build();

app.UseHttpsRedirection();
app.UseConfiguredHangfire();

app.MapGet("/foo", (int limit) =>
{
    BackgroundJob.Enqueue<DemoJobs>(s =>
        s.Run(CancellationToken.None));

    return Results.NoContent();
});

app.Run();
