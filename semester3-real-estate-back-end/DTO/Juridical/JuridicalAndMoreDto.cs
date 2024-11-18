using semester3_real_estate_back_end.DTO.District;
using semester3_real_estate_back_end.DTO.Property;

namespace semester3_real_estate_back_end.DTO.Juridical;

public class JuridicalAndMoreDto
{
    public string JuridicalId { get; set; } 
    public string Name { get; set; } 

    public List<PropertyDto> Properties { get; set; }
}