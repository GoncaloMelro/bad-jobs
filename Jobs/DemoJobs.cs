namespace badjobs.Jobs;

public class DemoJobs
{
    ILogger<DemoJobs> _logger;
    public DemoJobs(ILogger<DemoJobs> logger)
    {
        _logger = logger;
    }

    public async Task Run(CancellationToken c = default)
    {
        _logger.LogWarning($"Run job started at {DateTime.Now}");

        await Task.Delay(5000);

        _logger.LogWarning($"Run job ended at {DateTime.Now}");

    }

}
