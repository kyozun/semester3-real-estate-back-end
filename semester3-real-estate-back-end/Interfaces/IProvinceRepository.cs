using System.Net;
using semester3_real_estate_back_end.DTO.Province;
using semester3_real_estate_back_end.Helpers.Query;
using semester3_real_estate_back_end.Models;

namespace semester3_real_estate_back_end.Interfaces;

public interface IProvinceRepository
{
    Task<IEnumerable<Province>> GetProvinces(ProvinceQuery provinceQuery);
    Task<Province?> GetProvinceById(int provinceId);
    Task<HttpStatusCode> CreateProvince(Province province);
    Task<HttpStatusCode> UpdateProvince(UpdateProvinceDto updateProvinceDto);
    Task<HttpStatusCode> DeleteProvince(List<int> provinceIds);
    Task<bool> ProvinceExistsAsync(int provinceId);
}