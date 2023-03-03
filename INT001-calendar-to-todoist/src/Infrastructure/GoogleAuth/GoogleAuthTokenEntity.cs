﻿using Azure;
using Azure.Data.Tables;

namespace PD.INT001.Infrastructure.GoogleAuth;

internal sealed class GoogleAuthTokenEntity : ITableEntity
{
    public string PartitionKey { get; set; } = string.Empty;

    public string RowKey { get; set; } = string.Empty;
    
    public DateTimeOffset? Timestamp { get; set; }
    
    public ETag ETag { get; set; }
    
    public string AccessToken { get; set; } = string.Empty;
    
    public string RefreshToken { get; set; } = string.Empty;
}