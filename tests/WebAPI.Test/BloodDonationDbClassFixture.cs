using Microsoft.AspNetCore.Http.HttpResults;
using System.Net.Http.Json;

namespace WebAPI.Test;
public class BloodDonationDbClassFixture : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _httpClient;

    public BloodDonationDbClassFixture(CustomWebApplicationFactory factory) => _httpClient = factory.CreateClient();

    protected async Task<HttpResponseMessage> PostAsync(string method, object command, string culture = "en")
    {
        ChangeRquestCulture(culture);

        return await _httpClient.PostAsJsonAsync(method, command);
    }

    private void ChangeRquestCulture(string culture)
    {
        if (_httpClient.DefaultRequestHeaders.Contains("Accept-Language"))
        {
            _httpClient.DefaultRequestHeaders.Remove("Accept-Language");
        }

        _httpClient.DefaultRequestHeaders.Add("Accept-Language", culture);
    }
}
