using System.Net.Http.Json;

namespace WebAPI.Test;
public class BloodDonationDbClassFixture : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _httpClient;

    public BloodDonationDbClassFixture(CustomWebApplicationFactory factory) => _httpClient = factory.CreateClient();

    protected async Task<HttpResponseMessage> PostAsync(string method, object command)
    {
        return await _httpClient.PostAsJsonAsync(method, command);
    }
}
