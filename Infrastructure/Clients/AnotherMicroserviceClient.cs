using Core.Clients;
using Core.Clients.DTO.AnotherMicroservice;
using Infrastructure.Settings.Options;
using Microsoft.Extensions.Options;

namespace Infrastructure.Clients;

internal class AnotherMicroserviceClient : BaseHttpClient, IAnotherMicroserviceClient
{
    private readonly ServicesClientConfigurations _options;

    internal AnotherMicroserviceClient(IOptions<ServicesClientConfigurations> options, HttpClient client) : base(client,
        options.Value.AnotherMicroservice)

    {
        _options = options.Value;
    }

    public async Task<AnotherMicroserviceResponse> GetDataAsync(AnotherMicroserviceRequest request)
    {
        AnotherMicroserviceResponse response =
            await GetAsync<AnotherMicroserviceResponse>(_options.AnotherMicroservice.GetInfoEndpoint);

        return response;
    }
}