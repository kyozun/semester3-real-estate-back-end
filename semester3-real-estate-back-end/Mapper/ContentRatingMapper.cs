using semester4.DTO.ContentRating;

namespace semester4.Mapper;

public static class ContentRatingMapper
{
    public static ContentRatingDto ConvertToContentRatingDto(this ContentRating contentRating)
    {
        return new ContentRatingDto
        {
            Name = contentRating.Name
        };
    }
}