using System.Net;
using semester3_real_estate_back_end.DTO.Ward;
using semester3_real_estate_back_end.Helpers.Query;
using semester3_real_estate_back_end.Models;

namespace semester3_real_estate_back_end.Interfaces;

public interface IWardRepository
{
    Task<IEnumerable<Ward>> GetWards(WardQuery wardQuery);
    Task<Ward?> GetWardById(int wardId);
    Task<HttpStatusCode> CreateWard(Ward ward);
    Task<HttpStatusCode> UpdateWard(UpdateWardDto updateWardDto);
    Task<HttpStatusCode> DeleteWard(List<int> wardIds);
    Task<bool> WardExistsAsync(int wardId);
}