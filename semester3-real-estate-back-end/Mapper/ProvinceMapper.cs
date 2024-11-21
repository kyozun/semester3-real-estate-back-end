using semester3_real_estate_back_end.DTO.Province;
using semester3_real_estate_back_end.Models;

namespace semester3_real_estate_back_end.Mapper;

public static class ProvinceMapper
{
    public static ProvinceDto ConvertToProvinceDto(this Province province)
    {
        return new ProvinceDto
        {
            Name = province.Name,
        };
    }


    public static ProvinceAndMoreDto ConvertToProvinceAndMoreDto(this Province province)
    {
        var dto = new ProvinceAndMoreDto
        {
            ProvinceId = province.ProvinceId,
            Name = province.Name,
            Districts = province.Districts.Select(x => x.ConvertToDistrictDto()).ToList(),
        };

        return dto;
    }


    // Convert to Entity
    public static Province ConvertToProvince(this CreateProvinceDto createProvinceDto)
    {
        var province = new Province
        {
            Name = createProvinceDto.Name,
        };

        return province;
    }
}