using Azure.Data.Tables;
using Newtonsoft.Json;
using PD.INT001.Application.Interfaces;

namespace PD.INT001.Infrastructure;

internal sealed class AuthService : IAuthService
{
    private const string AuthTableName = ""; // TODO: table name
    
    private readonly HttpClient _httpClient;
    private readonly TableClient _tableClient;

    public AuthService(HttpClient httpClient, TableServiceClient tableStorageClient)
    {
        _httpClient = httpClient;
        _tableClient = tableStorageClient.GetTableClient(AuthTableName);
    }

    public async Task<string> GetTokenAsync()
    {
        throw new NotImplementedException();
    }
    
    public async Task RefreshTokenAsync()
    {
        var response = await _httpClient.PostAsync("token", null);
        // TODO: handle response status code
        var content = await response.Content.ReadAsStringAsync();
        var token = JsonConvert.DeserializeObject<ResponseModel>(content);
        // TODO: save the new token
    }
    
    private sealed record ResponseModel(
        string AccessToken,
        int ExpiresIn,
        string TokenType,
        string Scope,
        string? RefreshToken);
}
