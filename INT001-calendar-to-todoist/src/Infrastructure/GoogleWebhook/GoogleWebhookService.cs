using System.Text;

namespace PD.INT001.Infrastructure.GoogleWebhook;

internal sealed class GoogleWebhookService : IGoogleWebhookService
{
    private readonly HttpClient _httpClient;
    private readonly GoogleWebhookOptions _googleWebhookOptions;

    public GoogleWebhookService(HttpClient httpClient, IOptions<GoogleWebhookOptions> googleWebhookOptions)
    {
        _httpClient = httpClient;
        _googleWebhookOptions = googleWebhookOptions.Value;
    }
    
    public async Task StartWebhookAsync(CancellationToken cancellationToken)
    {
        var data = new StartWebhookRequest(_googleWebhookOptions.Id, "web_hook", _googleWebhookOptions.WebhookUrl);
        var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
        var uri = $"calendars/{_googleWebhookOptions.CalendarId}/events/watch";
        var response = await _httpClient.PostAsync(uri, content, cancellationToken);
        // TODO: handle response status code
    }

    public async Task StopWebhookAsync(CancellationToken cancellationToken)
    {
        var data = new StopWebhookRequest(_googleWebhookOptions.Id, _googleWebhookOptions.ResourceId);
        var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync("channels/stop", content, cancellationToken);
        // TODO: handle response status code
    }

    private sealed record StartWebhookRequest(string Id, string Type, string Address);
    
    private sealed record StopWebhookRequest(string Id, string ResourceId);
}
