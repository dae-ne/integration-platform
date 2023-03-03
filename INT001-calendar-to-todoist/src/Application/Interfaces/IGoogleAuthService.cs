namespace PD.INT001.Application.Interfaces;

public interface IGoogleAuthService
{
    Task<string> GetTokenAsync(CancellationToken cancellationToken);

    Task<string> RefreshTokenAsync(CancellationToken cancellationToken);
}
