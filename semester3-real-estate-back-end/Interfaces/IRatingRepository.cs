using System.Net;
using System.Runtime.InteropServices;
using semester4.DTO.Rating;
using semester4.Helpers.Enums.Include;
using semester4.Helpers.Query;

namespace semester4.Interfaces;

public interface IRatingRepository
{
    Task<IEnumerable<Rating>> GetRatings(RatingQuery ratingQuery);
    Task<Rating?> GetRatingById(string ratingId, [Optional] List<RatingInclude> includes);
    Task<HttpStatusCode> CreateRating(Rating rating);
    Task<HttpStatusCode> UpdateRating(UpdateRatingDto updateRatingDto);
    Task<HttpStatusCode> DeleteRating(List<Guid> ratingIds);

    Task<bool> RatingExistsAsync(string ratingId);
}