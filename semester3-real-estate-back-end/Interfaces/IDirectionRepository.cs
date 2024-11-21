using System.Net;
using semester3_real_estate_back_end.DTO.Direction;
using semester3_real_estate_back_end.Helpers.Query;
using semester3_real_estate_back_end.Models;

namespace semester3_real_estate_back_end.Interfaces;

public interface IDirectionRepository
{
    Task<IEnumerable<Direction>> GetDirections(DirectionQuery directionQuery);
    Task<Direction?> GetDirectionById(string directionId);
    Task<HttpStatusCode> CreateDirection(Direction direction);
    Task<HttpStatusCode> UpdateDirection(UpdateDirectionDto updateDirectionDto);
    Task<HttpStatusCode> DeleteDirection(List<Guid> directionIds);
    Task<bool> DirectionExistsAsync(string directionId);
}