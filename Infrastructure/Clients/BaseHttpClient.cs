using Infrastructure.Settings.Options;
using Newtonsoft.Json;

namespace Infrastructure.Clients;

internal class BaseHttpClient
{
    protected readonly HttpClient Client;

    internal BaseHttpClient(HttpClient client, BaseHttpClientConfigurations configurations)
    {
        Client = client;
        BaseAddress = configurations.BaseUrl;
        ConfigHttpClient();
    }

    private void ConfigHttpClient()
    {
        if (string.IsNullOrEmpty(BaseAddress))
        {
            throw new Exception("Base Address Url is missing.");
        }

        Client.BaseAddress = new Uri(BaseAddress);
        Client.Timeout = TimeSpan.FromSeconds(5);
    }
    
    protected string BaseAddress { get; init; }
    
    // Method for making a GET request
    protected async Task<T> GetAsync<T>(string url)
    {
        HttpResponseMessage response = await Client.GetAsync(BaseAddress + url);
        try
        {
            response.EnsureSuccessStatusCode();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return default(T);
        }
        
        string responseString = await response.Content.ReadAsStringAsync();
        
        T data = JsonConvert.DeserializeObject<T>(responseString);
        return data;
    }

    // Dispose of HttpClient when the class is disposed
    public void Dispose()
    {
        Client.Dispose();
    }

}