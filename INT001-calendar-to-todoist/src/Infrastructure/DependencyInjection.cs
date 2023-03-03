using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PD.INT001.Infrastructure.GoogleAuth;
using PD.INT001.Infrastructure.GoogleWebhook;

namespace PD.INT001.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var googleAuthTableOptionsSection = configuration.GetSection(GoogleAuthTableOptions.Position);
        var googleAuthTableOptions = googleAuthTableOptionsSection.Get<GoogleAuthTableOptions>();
        
        var googleAuthOptionsSection = configuration.GetSection(GoogleAuthOptions.Position);
        var googleAuthOptions = googleAuthOptionsSection.Get<GoogleAuthOptions>();
        
        var googleWebhookOptionsSection = configuration.GetSection(GoogleWebhookOptions.Position);
        var googleWebhookOptions = googleAuthOptionsSection.Get<GoogleWebhookOptions>();

        services
            .Configure<GoogleAuthTableOptions>(googleAuthTableOptionsSection)
            .Configure<GoogleAuthOptions>(googleAuthTableOptionsSection)
            .Configure<GoogleWebhookOptions>(googleAuthTableOptionsSection);
        
        services.AddAzureClients(builder =>
        {
            builder.AddTableServiceClient(googleAuthTableOptions.ConnectionString);
        });

        // TODO: Polly
        services.AddHttpClient<IGoogleAuthService, GoogleAuthService>(client =>
        {
            client.BaseAddress = new Uri(googleAuthOptions.BaseAuthUrl);
        });
        
        // TODO: Polly
        services.AddHttpClient<IGoogleWebhookService, GoogleWebhookService>(client =>
        {
            client.BaseAddress = new Uri(googleWebhookOptions.BaseUrl);
        });

        return services;
    }
}
