using MediatR;

namespace PD.INT001.Application.Commands;

public record RefreshGoogleTokenCommand : IRequest;

internal sealed class RefreshGoogleTokenHandler : IRequestHandler<RefreshGoogleTokenCommand>
{
    public Task Handle(RefreshGoogleTokenCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
