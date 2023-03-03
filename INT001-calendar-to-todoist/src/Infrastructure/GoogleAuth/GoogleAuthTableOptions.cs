namespace PD.INT001.Infrastructure.GoogleAuth;

internal sealed class GoogleAuthTableOptions
{
    public const string Position = "GoogleAuthTable";
    
    public string TableName { get; init; } = string.Empty;
    
    public string PartitionKey { get; init; } = string.Empty;
    
    public string RowKey { get; init; } = string.Empty;
    
    public string ConnectionString { get; init; } = string.Empty;
}
