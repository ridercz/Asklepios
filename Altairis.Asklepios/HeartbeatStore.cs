using System.Security.Cryptography;
using Microsoft.AspNetCore.Http;

namespace Altairis.Asklepios;

public abstract class HeartbeatStore : IHeartbeatStore {
    // Character set for API key generation
    private const string ApiKeyChars = "ABCDEFGHJKLMNPRSTUVWXYZ23456789";
    // Length of API key in characters
    private const int ApiKeyLength = 40;

    #region IHearthbeatStore Members

    public abstract Task<string> CreateService(string Name);
    public abstract Task DeleteService(string apiKey);
    public abstract Task<IEnumerable<IHeartbeatStore.ServiceInfo>> GetAllServices();
    public abstract Task<IHeartbeatStore.HeartbeatInfo?> GetLastHeartbeat(string apiKey);
    public abstract Task<string> GetServiceName(string apiKey);
    public abstract Task SetServiceName(string apiKey, string newName);
    public abstract Task<bool> UpdateHeartbeat(string apiKey, string? message);

    #endregion

    /// <summary>
    /// Generates random API key.
    /// </summary>
    /// <returns>String containing random API key</returns>
    protected static string GenerateApiKey() {
        var apiKey = new char[ApiKeyLength];
        for (var i = 0; i < ApiKeyLength; i++) {
            apiKey[i] = ApiKeyChars[RandomNumberGenerator.GetInt32(ApiKeyChars.Length)];
        }
        return new string(apiKey);
    }



}
