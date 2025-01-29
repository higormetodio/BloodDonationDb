using Microsoft.AspNetCore.Http.HttpResults;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace WebAPI.Test;
public class BloodDonationDbClassFixture : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _httpClient;

    public BloodDonationDbClassFixture(CustomWebApplicationFactory factory) => _httpClient = factory.CreateClient();

    protected async Task<HttpResponseMessage> PostAsync(string method, object command, string token = "", string culture = "en")
    {
        ChangeRquestCulture(culture);
        AuthorizeRequest(token);

        return await _httpClient.PostAsJsonAsync(method, command);
    }

    protected async Task<HttpResponseMessage> GetAsync(string method, string token = "", string culture = "en")
    {
        ChangeRquestCulture(culture);
        AuthorizeRequest(token);

        return await _httpClient.GetAsync(method);
    }

    private void ChangeRquestCulture(string culture)
    {
        if (_httpClient.DefaultRequestHeaders.Contains("Accept-Language"))
        {
            _httpClient.DefaultRequestHeaders.Remove("Accept-Language");
        }

        _httpClient.DefaultRequestHeaders.Add("Accept-Language", culture);
    }

    private void AuthorizeRequest(string token)
    {
        if (string.IsNullOrEmpty(token))
        {
            return;
        }

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }
}
