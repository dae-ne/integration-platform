namespace PD.INT001.Application.Interfaces;

public interface IAuthService
{
    Task<string> GetTokenAsync(CancellationToken cancellationToken);

    Task RefreshTokenAsync(CancellationToken cancellationToken);
}
