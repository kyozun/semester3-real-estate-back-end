
using semester3_real_estate_back_end.DTO.Ward;
using semester4.DTO.Genre;

namespace semester3_real_estate_back_end.DTO.District;

public class DistrictAndMoreDto
{
    public int DistrictId { get; set; }
    public string Name { get; set; } = string.Empty;

    public List<WardDto> Wards { get; set; }
}