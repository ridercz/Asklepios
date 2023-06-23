namespace Altairis.Asklepios;

public interface IHeartbeatStore {

    Task<string> CreateService(string Name);

    Task DeleteService(string apiKey);

    Task<string> GetServiceName(string apiKey);

    Task SetServiceName(string apiKey, string newName);

    Task<bool> UpdateHeartbeat(string apiKey, string? message);

    Task<HeartbeatInfo?> GetLastHeartbeat(string apiKey);
    public record struct HeartbeatInfo(DateTimeOffset Timestamp, string? Message);

    Task<IEnumerable<ServiceInfo>> GetAllServices();
    public record struct ServiceInfo(string ApiKey, string Name, HeartbeatInfo? LastHeartbeat);

}

