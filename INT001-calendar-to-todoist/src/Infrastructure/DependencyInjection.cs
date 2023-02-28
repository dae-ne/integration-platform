using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PD.INT001.Application.Interfaces;

namespace PD.INT001.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // TODO: Polly
        services.AddHttpClient<IAuthService, AuthService>(client =>
        {
            client.BaseAddress = new Uri(""); // TODO: get base address from configuration
        });
        
        services.AddAzureClients(builder =>
        {
            builder.AddTableServiceClient(""); // TODO: connection string
        });

        services.AddTransient<IAuthService, AuthService>();

        return services;
    }
}
