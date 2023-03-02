using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PD.INT001.Application.Interfaces;
using PD.INT001.Infrastructure.Authorization;
using PD.INT001.Infrastructure.Configuration;

namespace PD.INT001.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .Configure<AuthTableOptions>(configuration.GetSection(AuthTableOptions.Position))
            .Configure<GoogleOptions>(configuration.GetSection(GoogleOptions.Position));
        
        // TODO: Polly
        services.AddHttpClient<IAuthService, AuthService>(client =>
        {
            client.BaseAddress = new Uri(""); // TODO: get base address from configuration
        });
        
        services.AddAzureClients(builder =>
        {
            // TODO: get connection string from configuration
            builder.AddTableServiceClient("");
        });

        services.AddTransient<IAuthService, AuthService>();

        return services;
    }
}
