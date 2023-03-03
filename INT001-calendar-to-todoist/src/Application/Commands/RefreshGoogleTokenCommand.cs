namespace PD.INT001.Application.Commands;

public record RefreshGoogleTokenCommand : IRequest;

internal sealed class RefreshGoogleTokenHandler : IRequestHandler<RefreshGoogleTokenCommand>
{
    private readonly IAuthService _authService;

    public RefreshGoogleTokenHandler(IAuthService authService)
    {
        _authService = authService;
    }
    
    public async Task Handle(RefreshGoogleTokenCommand request, CancellationToken cancellationToken)
    {
        var token = await _authService.GetTokenAsync(cancellationToken);
        Console.WriteLine(token);
    }
}
