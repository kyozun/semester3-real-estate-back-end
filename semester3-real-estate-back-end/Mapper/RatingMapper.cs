using semester4.DTO.Rating;

namespace semester4.Mapper;

public static class RatingMapper
{
    public static RatingDto ConvertToRatingDto(this Rating rating)
    {
        return new RatingDto
        {
            Score = rating.Score,
            CreatedAt = rating.CreatedAt
        };
    }

    public static RatingAndMoreDto ConvertToRatingAndMoreDto(this Rating rating)
    {
        var dto = new RatingAndMoreDto
        {
            Id = rating.RatingId,
            Score = rating.Score,
            CreatedAt = rating.CreatedAt
        };

        if (rating.Manga != null) dto.Manga = rating.Manga.ConvertToMangaDto();

        if (rating.User != null) dto.User = rating.User.ConvertToUserDto();

        return dto;
    }

    public static Rating ConvertToRating(this CreateRatingDto createRatingDto)
    {
        return new Rating
        {
            RatingId = Guid.NewGuid().ToString(),
            Score = createRatingDto.Score,
            MangaId = createRatingDto.MangaId.ToString()!
        };
    }
}