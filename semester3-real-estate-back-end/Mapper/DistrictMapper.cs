using semester3_real_estate_back_end.DTO.District;
using semester3_real_estate_back_end.DTO.Province;
using semester3_real_estate_back_end.Models;

namespace semester3_real_estate_back_end.Mapper;

public static class DistrictMapper
{
    // Convert To Dto
    public static DistrictDto ConvertToDistrictDto(this District district)
    {
        return new DistrictDto
        {
            Name = district.Name
        };
    }

    public static DistrictAndMoreDto ConvertToDistrictAndMoreDto(this District district)
    {
        var dto = new DistrictAndMoreDto
        {
            DistrictId = district.DistrictId,
            Name = district.Name,
            Wards = district.Wards.Select(x => x.ConvertToWardDto()).ToList()
        };

        return dto;
    }

    // Convert to Entity
    public static District ConvertToDistrict(this CreateDistrictDto createDistrictDto)
    {
        return new District
        {
            DistrictId = Guid.NewGuid().ToString(),
            Name = createDistrictDto.Name,
        };
    }
}