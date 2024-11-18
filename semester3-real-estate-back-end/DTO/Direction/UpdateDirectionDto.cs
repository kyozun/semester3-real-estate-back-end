using System.ComponentModel.DataAnnotations;

namespace semester3_real_estate_back_end.DTO.Direction;

public class UpdateDirectionDto
{
    [Required] public Guid? DirectionId { get; set; }
    public required string Name { get; set; }
}