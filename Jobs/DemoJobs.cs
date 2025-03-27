using badjobs.Metrics;
using Hangfire;
using Hangfire.Server;

namespace badjobs.Jobs;

public class DemoJobs
{
    ILogger<DemoJobs> _logger;
    public DemoJobs(ILogger<DemoJobs> logger)
    {
        _logger = logger;
    }


    [AutomaticRetry(Attempts = 0)]
    public async Task Run(PerformingContext context, CancellationToken c = default)
    {
        try{
            await Task.Delay(2000);
            throw new Exception("Simul job fail");
        }catch(Exception ex){

            CustomMetrics.AddJobFail("DemoJob");
            _logger.LogError($"Error on DemoJob: {ex.Message}");
            throw;
        }
    }

}
