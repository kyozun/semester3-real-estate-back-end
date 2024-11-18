using semester3_real_estate_back_end.DTO.Juridical;
using semester3_real_estate_back_end.Models;

namespace semester3_real_estate_back_end.Mapper;

public static class JuridicalMapper
{
    public static JuridicalDto ConvertToJuridicalDto(this Juridical juridical)
    {
        return new JuridicalDto
        {
            Name = juridical.Name
        };
    }

    public static JuridicalAndMoreDto ConvertToJuridicalAndMoreDto(this Juridical juridical)
    {
        var dto = new JuridicalAndMoreDto
        {
            JuridicalId = juridical.JuridicalId,
            Name = juridical.Name,
            Properties = juridical.Properties.Select(x=> x.ConvertToPropertyDto()).ToList()
        };

        return dto;
    }

    public static Juridical ConvertToJuridical(this CreateJuridicalDto createJuridicalDto)
    {
        return new Juridical
        {
            JuridicalId = Guid.NewGuid().ToString(),
            Name = createJuridicalDto.Name,
        };
    }
}