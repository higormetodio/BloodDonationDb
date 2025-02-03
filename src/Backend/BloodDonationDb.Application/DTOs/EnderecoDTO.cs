using BloodDonationDb.Domain.ValueObjects;

namespace BloodDonationDb.Application.DTOs;
public record EnderecoDTO
{
    public string? CEP { get; init; }
    public string? Logradouro { get; init; }
    public string? Localidade { get; init; }
    public string? Uf { get; init; }

    public Address ToEntity(string number, string country)
        => new(Logradouro!, number, Localidade!, Uf!, CEP!, country);
}
