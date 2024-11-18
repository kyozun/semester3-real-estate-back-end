using System.ComponentModel.DataAnnotations;

namespace semester3_real_estate_back_end.DTO.Ward;

public class UpdateWardDto
{
    [Required] public Guid? WardId { get; set; }

    public string? Name { get; set; }
}