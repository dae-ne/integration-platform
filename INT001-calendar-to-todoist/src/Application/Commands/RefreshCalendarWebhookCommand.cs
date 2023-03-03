namespace PD.INT001.Application.Commands;

public record RefreshCalendarWebhookCommand : IRequest;

internal sealed class RefreshCalendarWebhookHandler : IRequestHandler<RefreshCalendarWebhookCommand>
{
    public Task Handle(RefreshCalendarWebhookCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
