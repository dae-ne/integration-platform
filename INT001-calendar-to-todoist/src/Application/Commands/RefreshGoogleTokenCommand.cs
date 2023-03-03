namespace PD.INT001.Application.Commands;

public record RefreshGoogleTokenCommand : IRequest;

internal sealed class RefreshGoogleTokenHandler : IRequestHandler<RefreshGoogleTokenCommand>
{
    private readonly IGoogleAuthService _googleAuthService;

    public RefreshGoogleTokenHandler(IGoogleAuthService googleAuthService)
    {
        _googleAuthService = googleAuthService;
    }
    
    public async Task Handle(RefreshGoogleTokenCommand request, CancellationToken cancellationToken)
    {
        var accessToken = await _googleAuthService.RefreshTokenAsync(cancellationToken);

        if (string.IsNullOrEmpty(accessToken))
        {
            // TODO: logger error
        }
    }
}
