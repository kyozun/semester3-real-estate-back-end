using System.ComponentModel.DataAnnotations;

namespace semester4.DTO.Rating;

public class UpdateRatingDto
{
    [Required] public Guid? RatingId { get; set; }
    [Range(1, 5)] public required int Score { get; set; }
}