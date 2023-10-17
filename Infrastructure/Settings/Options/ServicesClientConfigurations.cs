namespace Infrastructure.Settings.Options;

internal class ServicesClientConfigurations
{
    public AnotherMicroServiceClientConfigurations AnotherMicroservice { get; set; }

}

internal class BaseHttpClientConfigurations
{
    public string BaseUrl { get; set; }
    public string Timeout { get; set; }
}

internal class AnotherMicroServiceClientConfigurations : BaseHttpClientConfigurations
{
    public string GetInfoEndpoint { get; set; }
}