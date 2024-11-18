using semester3_real_estate_back_end.DTO.Property;
using semester3_real_estate_back_end.DTO.User;

namespace semester3_real_estate_back_end.DTO.CustomList;

public class CustomListAndMoreDto
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public UserDto? User { get; set; }
    public PropertyDto? Property { get; set; }
}