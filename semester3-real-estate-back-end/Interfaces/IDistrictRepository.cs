using System.Net;
using semester3_real_estate_back_end.DTO.District;
using semester3_real_estate_back_end.Helpers.Query;
using semester3_real_estate_back_end.Models;
using semester4.DTO.Genre;

namespace semester3_real_estate_back_end.Interfaces;

public interface IDistrictRepository
{
    Task<IEnumerable<District>> GetDistricts(DistrictQuery districtQuery);
    Task<District?> GetDistrictById(int districtId);
    Task<HttpStatusCode> CreateDistrict(District district);
    Task<HttpStatusCode> UpdateDistrict(UpdateDistrictDto updateDistrictDto);
    Task<HttpStatusCode> DeleteDistrict(List<int> districtIds);
    Task<bool> DistrictExistsAsync(int districtId);
}