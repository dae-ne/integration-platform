using Azure.Data.Tables;

namespace PD.INT001.Infrastructure.GoogleAuth;

internal sealed class GoogleAuthService : IGoogleAuthService
{
    private readonly HttpClient _httpClient;
    private readonly TableClient _tableClient;
    private readonly GoogleAuthTableOptions _googleAuthTableOptions;
    private readonly GoogleAuthOptions _googleAuthOptions;

    public GoogleAuthService(
        HttpClient httpClient,
        TableServiceClient tableStorageClient,
        IOptions<GoogleAuthTableOptions> authTableOptions,
        IOptions<GoogleAuthOptions> googleOptions)
    {
        _httpClient = httpClient;
        _tableClient = tableStorageClient.GetTableClient(authTableOptions.Value.TableName);
        _googleAuthTableOptions = authTableOptions.Value;
        _googleAuthOptions = googleOptions.Value;
    }

    public async Task<string> GetTokenAsync(CancellationToken cancellationToken)
    {
        var select = new[] { "AccessToken" };
        var response = await _tableClient.GetEntityAsync<GoogleAuthTokenEntity>(
            _googleAuthTableOptions.PartitionKey, 
            _googleAuthTableOptions.RowKey,
            select,
            cancellationToken);
        
        // TODO: handle response status code
        
        return response.Value.AccessToken;
    }
    
    public async Task<string> RefreshTokenAsync(CancellationToken cancellationToken)
    {
        var authTableResponse = await _tableClient.GetEntityAsync<GoogleAuthTokenEntity>(
            _googleAuthTableOptions.PartitionKey,
            _googleAuthTableOptions.RowKey,
            cancellationToken: cancellationToken);
        
        // TODO: handle response status code
        
        var data = new KeyValuePair<string, string>[]
        {
            new("client_id", _googleAuthOptions.ClientId),
            new("client_secret", _googleAuthOptions.ClientSecret),
            new("refresh_token", authTableResponse.Value.RefreshToken),
            new("grant_type", "refresh_token")
        };
        
        var refreshRequest = new FormUrlEncodedContent(data);
        var googleResponse = await _httpClient.PostAsync("token", refreshRequest, cancellationToken);
        // // TODO: handle response status code

        var serializerSettings = new JsonSerializerSettings
        {
            ContractResolver = new SnakeCaseContractResolver()
        };
        
        var content = await googleResponse.Content.ReadAsStringAsync(cancellationToken);
        var token = JsonConvert.DeserializeObject<GoogleAuthResponseModel>(content, serializerSettings);
        
        // TODO: if token is null throw exception
        
        authTableResponse.Value.AccessToken = token.AccessToken;
        
        var tokenUpdateResponse = await _tableClient.UpdateEntityAsync(
            authTableResponse.Value,
            authTableResponse.Value.ETag,
            TableUpdateMode.Replace,
            cancellationToken);
        
        // TODO: handle response status code

        return token.AccessToken;
    }
    
    internal sealed record GoogleAuthResponseModel(string AccessToken, int ExpiresIn, string TokenType, string Scope);
}
