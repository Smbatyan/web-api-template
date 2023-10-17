using Core.Clients.DTO.AnotherMicroservice;

namespace Core.Clients;

public interface IAnotherMicroserviceClient
{
    Task<AnotherMicroserviceResponse> GetDataAsync(AnotherMicroserviceRequest request);
}