namespace PD.INT001.Infrastructure.Tests;

public class GlobalTests
{
    [Fact]
    public void ShouldAllClassesBeInternal()
    {
        var nonInternalClassExists = typeof(DependencyInjection).Assembly.DefinedTypes
            .Any(type => type.IsClass &&
                         type.IsPublic &&
                         type.Name != nameof(DependencyInjection));
        
        Assert.False(nonInternalClassExists);
    }
}
