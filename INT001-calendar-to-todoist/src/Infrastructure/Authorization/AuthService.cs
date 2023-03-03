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
        var authTableResponse = await _tableClient.GetEntityAsync<AuthTokenEntity>(
            _authTableOptions.PartitionKey,
            _authTableOptions.RowKey,
            cancellationToken: cancellationToken);
        
        // TODO: handle response status code
        
        var data = new KeyValuePair<string, string>[]
        {
            new("client_id", _googleOptions.ClientId),
            new("client_secret", _googleOptions.ClientSecret),
            new("refresh_token", authTableResponse.Value.RefreshToken),
            new("grant_type", "refresh_token")
        };
        
        var refreshRequest = new FormUrlEncodedContent(data);
        var googleResponse = await _httpClient.PostAsync("token", refreshRequest, cancellationToken);
        // // TODO: handle response status code
        var content = await googleResponse.Content.ReadAsStringAsync(cancellationToken);
        var token = JsonConvert.DeserializeObject<GoogleAuthResponseModel>(content);
        
        // TODO: if token is null throw exception
        
        authTableResponse.Value.AccessToken = token.AccessToken;
        
        var tokenUpdateResponse = await _tableClient.UpdateEntityAsync(
            authTableResponse.Value,
            authTableResponse.Value.ETag,
            TableUpdateMode.Replace,
            cancellationToken);
        
        // TODO: handle response status code
    }
    
    private sealed record GoogleAuthResponseModel(string AccessToken, int ExpiresIn, string TokenType, string Scope);
}
