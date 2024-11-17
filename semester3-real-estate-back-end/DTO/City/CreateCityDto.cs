using System.ComponentModel.DataAnnotations;

namespace semester4.DTO.City;

public class CreateCityDto
{
    [Required]
    [MinLength(5, ErrorMessage = "Tên Thành phố phải từ 5 kí tự trở lên")]
    [MaxLength(25, ErrorMessage = "Tên Thành phố chỉ được có tối đa 25 kí tự")]
    public string Name { get; set; } = string.Empty;


    [Required] [Range(1, 1000)] public int StateId { get; set; } = 0;

    [Required] [Range(1, 1000)] public int CountryId { get; set; } = 0;
}