namespace PD.INT001.Infrastructure.Configuration;

internal sealed class GoogleOptions
{
    public const string Position = "Google";
    
    public string ClientId { get; init; } = string.Empty;
    
    public string ClientSecret { get; init; } = string.Empty;
}
