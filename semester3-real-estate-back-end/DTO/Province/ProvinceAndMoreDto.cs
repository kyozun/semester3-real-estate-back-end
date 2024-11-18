using semester3_real_estate_back_end.DTO.District;

namespace semester3_real_estate_back_end.DTO.Province;

public class ProvinceAndMoreDto
{
    public string ProvinceId { get; set; } 
    public string Name { get; set; } 

    public List<DistrictDto> Districts { get; set; }
}