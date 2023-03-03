using Azure.Data.Tables;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace PD.INT001.Infrastructure.Authorization;

internal sealed class AuthService : IAuthService
{
    private readonly HttpClient _httpClient;
    private readonly TableClient _tableClient;
    private readonly AuthTableOptions _authTableOptions;
    private readonly GoogleOptions _googleOptions;

    public AuthService(
        HttpClient httpClient,
        TableServiceClient tableStorageClient,
        IOptions<AuthTableOptions> authTableOptions,
        IOptions<GoogleOptions> googleOptions)
    {
        _httpClient = httpClient;
        _tableClient = tableStorageClient.GetTableClient(authTableOptions.Value.TableName);
        _authTableOptions = authTableOptions.Value;
        _googleOptions = googleOptions.Value;
    }

    public async Task<string> GetTokenAsync(CancellationToken cancellationToken)
    {
        var select = new[] { "AccessToken" };
        var response = await _tableClient.GetEntityAsync<AuthTokenEntity>(
            _authTableOptions.PartitionKey, 
            _authTableOptions.RowKey,
            select,
            cancellationToken);
        
        // TODO: handle response status code
        
        return response.Value.AccessToken;
    }
    
    public async Task RefreshTokenAsync(CancellationToken cancellationToken)
    {
        // TODO: move to configuration
        const string partitionKey = "1CB3204D-220B-4A60-A267-30DA61BD2C5C";
        const string rowKey = "06A902A7-16F6-4F1E-8544-0B215B056594";
        
        var response = await _tableClient.GetEntityAsync<AuthTokenEntity>(partitionKey, rowKey, cancellationToken: cancellationToken);
        
        // TODO: handle response status code
        
        var data = new KeyValuePair<string, string>[]
        {
            new("client_id", ""),
            new("client_secret", ""),
            new("refresh_token", response.Value.RefreshToken),
            new("grant_type", "refresh_token")
        };
        
        var refreshRequest = new FormUrlEncodedContent(data);
        var response2 = await _httpClient.PostAsync("token", refreshRequest, cancellationToken);
        // // TODO: handle response status code
        var content = await response2.Content.ReadAsStringAsync(cancellationToken);
        var token = JsonConvert.DeserializeObject<GoogleAuthResponseModel>(content);
        
        // TODO: if token is null throw exception
        
        response.Value.AccessToken = token.AccessToken;
        var xd = await _tableClient.UpdateEntityAsync(
            response.Value,
            response.Value.ETag,
            TableUpdateMode.Replace,
            cancellationToken);
        
        // TODO: handle response status code
    }
    
    private sealed record GoogleAuthResponseModel(string AccessToken, int ExpiresIn, string TokenType, string Scope);
}
