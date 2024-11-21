using semester3_real_estate_back_end.DTO.Ward;
using semester3_real_estate_back_end.Models;

namespace semester3_real_estate_back_end.Mapper;

public static class WardMapper
{
    public static WardDto ConvertToWardDto(this Ward ward)
    {
        return new WardDto
        {
            Name = ward.Name,
        };
    }


    public static WardAndMoreDto ConvertToWardAndMoreDto(this Ward ward)
    {
        var dto = new WardAndMoreDto
        {
            WardId = ward.WardId,
            Name = ward.Name,
            Properties = ward.Properties.Select(x => x.ConvertToPropertyDto()).ToList(),
        };

        return dto;
    }


    // Convert to Entity
    public static Ward ConvertToWard(this CreateWardDto createWardDto)
    {
        var ward = new Ward
        {
            Name = createWardDto.Name,
        };

        return ward;
    }
}