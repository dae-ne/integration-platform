namespace PD.INT001.Application.Interfaces;

public interface IAuthService
{
    Task RefreshTokenAsync();
}
