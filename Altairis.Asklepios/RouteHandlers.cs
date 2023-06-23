using Microsoft.AspNetCore.Http;

namespace Altairis.Asklepios;
public static class RouteHandlers {

    public static async Task IncomingHeartbeatHandler(string apiKey, string? message, IHeartbeatStore heartbeatStore, HttpResponse response) {
        // Update last heartbeat
        var result = await heartbeatStore.UpdateHeartbeat(apiKey, message);

        // Check result
        if (result) {
            // Heartbeat updated, return 204 No Content
            response.StatusCode = StatusCodes.Status204NoContent;
        } else {
            // API key not found, return 404 Not Found
            response.StatusCode = StatusCodes.Status404NotFound;
            await response.WriteAsync("API key not found");
        }
    }

    public static async Task<object> ResultsHandler(IHeartbeatStore heartbeatStore, HttpResponse response) {
        // Get all services
        var services = await heartbeatStore.GetAllServices();

        // Create result object to return
        var resultObject = services.Select(service => new {
            service.Name, 
            service.LastHeartbeat
        });

        // Return result to be serialized as JSON
        return resultObject;
    }

}
