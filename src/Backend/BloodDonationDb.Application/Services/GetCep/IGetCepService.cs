using BloodDonationDb.Application.DTOs;

namespace BloodDonationDb.Application.Services.ConsultaCep;
public interface IGetCepService
{
    Task<EnderecoDTO> GetCepAsync(string cep);
}
