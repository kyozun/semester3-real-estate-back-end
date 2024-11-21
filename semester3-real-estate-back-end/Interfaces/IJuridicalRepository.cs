using System.Net;
using semester3_real_estate_back_end.DTO.Juridical;
using semester3_real_estate_back_end.Helpers.Query;
using semester3_real_estate_back_end.Models;

namespace semester3_real_estate_back_end.Interfaces;

public interface IJuridicalRepository
{
    Task<IEnumerable<Juridical>> GetJuridicals(JuridicalQuery juridicalQuery);
    Task<Juridical?> GetJuridicalById(string juridicalId);
    Task<HttpStatusCode> CreateJuridical(Juridical juridical);
    Task<HttpStatusCode> UpdateJuridical(UpdateJuridicalDto updateJuridicalDto);
    Task<HttpStatusCode> DeleteJuridical(List<Guid> juridicalIds);
    Task<bool> JuridicalExistsAsync(string juridicalId);
}