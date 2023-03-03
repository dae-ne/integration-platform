namespace PD.INT001.Infrastructure.GoogleAuth;

internal sealed class GoogleAuthOptions
{
    public const string Position = "GoogleAuth";
    
    public string BaseAuthUrl { get; init; } = string.Empty;
    
    public string ClientId { get; init; } = string.Empty;
    
    public string ClientSecret { get; init; } = string.Empty;
}
