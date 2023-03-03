namespace PD.INT001.Infrastructure.GoogleWebhook;

internal sealed class GoogleWebhookOptions
{
    public const string Position = "GoogleWebhook";
    
    public string BaseUrl { get; init; } = string.Empty;

    public string WebhookUrl { get; set; } = string.Empty;

    public string Id { get; set; } = string.Empty;

    public string ResourceId { get; set; } = string.Empty;

    public string CalendarId { get; set; } = string.Empty;
}
