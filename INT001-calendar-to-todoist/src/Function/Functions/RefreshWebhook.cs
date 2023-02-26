using System;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace PD.INT001.Function.Functions;

public static class RefreshWebhook
{
    [FunctionName("refresh-webhook")]
    public static async Task RunAsync(
        [TimerTrigger("0 */5 * * * *")] TimerInfo myTimer,
        ILogger log)
    {
        log.LogInformation($"C# Timer trigger function executed at: {DateTime.UtcNow}");
    }
}
