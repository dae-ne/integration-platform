namespace PD.INT001.Application.Interfaces;

public interface IGoogleWebhookService
{
    Task StartWebhookAsync(CancellationToken cancellationToken);
    
    Task StopWebhookAsync(CancellationToken cancellationToken);
}
