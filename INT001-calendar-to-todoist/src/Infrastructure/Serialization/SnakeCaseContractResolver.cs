using Newtonsoft.Json.Serialization;

namespace PD.INT001.Infrastructure.Serialization;

internal sealed class SnakeCaseContractResolver : DefaultContractResolver
{
    public SnakeCaseContractResolver()
    {
        NamingStrategy = new SnakeCaseNamingStrategy();
    }
}
