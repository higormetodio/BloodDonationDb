using BloodDonationDb.Application.DTOs;
using BloodDonationDb.Application.Services.ConsultaCep;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using System.Text.Json;

namespace BloodDonationDb.Application.Services.GetCep;
public class GetCepService : IGetCepService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;
    private readonly JsonSerializerOptions _jsonSerializerOptions;

    public GetCepService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _httpClientFactory = httpClientFactory;
        _configuration = configuration;
        _jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
    }

    public async Task<EnderecoDTO> GetCepAsync(string cep)
    {
        var client = _httpClientFactory.CreateClient();
       
        var uri = string.Format(_configuration["CepServiceUri:ViaCepApi"]!, cep);

        var response = client.GetAsync(uri);

        if (!response.Result.IsSuccessStatusCode)
        {
            return null;
        }

        await using var responseBody = await response.Result.Content.ReadAsStreamAsync();

        var responseData = await JsonSerializer.DeserializeAsync<EnderecoDTO>(responseBody, _jsonSerializerOptions);

        return responseData!;
    }
}
