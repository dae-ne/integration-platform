using System;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace PD.INT001.Function.Functions;

public static class RefreshGoogleToken
{
    [FunctionName("refresh-google-token")]
    public static async Task RunAsync(
        [TimerTrigger("0 */5 * * * *", RunOnStartup = true)]
        TimerInfo myTimer,
        ILogger log)
    {
        log.LogInformation($"C# Timer trigger function executed at: {DateTime.UtcNow}");
    }
}
