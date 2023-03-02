namespace PD.INT001.Infrastructure.Configuration;

internal sealed class AuthTableOptions
{
    public const string Position = "AuthTable";
    
    public string TableName { get; init; } = string.Empty;
    
    public string PartitionKey { get; init; } = string.Empty;
    
    public string RowKey { get; init; } = string.Empty;
}
