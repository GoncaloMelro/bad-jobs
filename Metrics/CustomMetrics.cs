using System.Diagnostics.Metrics;

namespace badjobs.Metrics;

public class CustomMetrics
{
    private static readonly Meter Meter = new("badjobs.metrics", "1.0");
    public static readonly Counter<long> JobExecutionCounter = Meter.CreateCounter<long>("badjobs_jobs_executed");

    public static readonly Counter<long> JobFailsCounter = Meter.CreateCounter<long>("badjobs_jobs_failed");

    public static void AddJobCount(string jobName)
    {
        JobExecutionCounter.Add(1, new KeyValuePair<string, object>("job_name", jobName)!);
    }

        public static void AddJobFail(string jobName)
    {
        JobFailsCounter.Add(1, new KeyValuePair<string, object>("job_name", jobName)!);
    }

}
