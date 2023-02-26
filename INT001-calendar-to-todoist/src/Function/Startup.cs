using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PD.INT001.Application;
using PD.INT001.Infrastructure;

[assembly: FunctionsStartup(typeof(PD.INT001.Function.Startup))]

namespace PD.INT001.Function;

public class Startup : FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        var configuration = builder.Services
            .BuildServiceProvider()
            .GetService<IConfiguration>();
        
        builder.Services
            .AddApplication()
            .AddInfrastructure(configuration!);
    }
}
