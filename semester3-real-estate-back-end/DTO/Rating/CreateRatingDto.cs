using System.ComponentModel.DataAnnotations;

namespace semester4.DTO.Rating;

public class CreateRatingDto
{
    [Range(1, 5)] public required int Score { get; set; }

    [Required] public Guid? MangaId { get; set; }
}